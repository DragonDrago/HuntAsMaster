using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IArrow
{
    public enum WeaponType { none, bow, wand, axe };

    public WeaponType weaponType = WeaponType.none;

    [SerializeField]
    private Weapon weaponPrefab;
    public Weapon GetArrowPrefab { get { return weaponPrefab; } }

    private BoxCollider boxCollider;

    private Vector3 targetTransform;

    private float rotation_speed = 1f;

    private float topHight;


    private void Awake()
    {
        if(weaponType != WeaponType.bow)
        {
            boxCollider = GetComponent<BoxCollider>();
        }
        if(weaponType == WeaponType.none || weaponType == WeaponType.wand)
        {
            rotation_speed = 7.5f;
        }
        else
        {
            rotation_speed = 60f;
        }
    }


    internal void SetMovement(Vector3 target, float height, Vector3 arrowPoint, Vector3 direction, float v0, float angle, float time)
    {
        topHight = height;

        if (weaponType == WeaponType.axe)
            transform.GetChild(0).transform.localEulerAngles = new Vector3(180f, -90f, 270f);

        if (weaponType == WeaponType.wand)
            transform.GetChild(0).transform.localEulerAngles = Vector3.zero;

        if (weaponType == WeaponType.none)
        {
            transform.GetChild(0).transform.localPosition = Vector3.zero;
            transform.GetChild(0).transform.localEulerAngles = Vector3.zero;
        }

        targetTransform = target;

        if (weaponType != WeaponType.bow)
          StartCoroutine(Coroutine_Movement(arrowPoint, direction, v0, angle, time));
    }


    private IEnumerator Coroutine_Movement(Vector3 arrowPoint, Vector3 direction, float v0, float angle, float time)
    {
        float t = 0;

        if (weaponType == WeaponType.none || weaponType == WeaponType.wand)
        {
            rotation_speed = 7f - time;
        }

        bool isH = false;
        int count = 0;

        while (t < time)
        {
            float x = v0 * t * Mathf.Cos(angle);

            float y = v0 * t * Mathf.Sin(angle) - (1f / 2f) * -Physics.gravity.y * Mathf.Pow(t, 2);

            transform.position = arrowPoint + direction * x + Vector3.up * y;

            if (weaponType == WeaponType.none || weaponType == WeaponType.wand)
            {
                Vector3 projectileXZPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
                float R = Vector3.Distance(projectileXZPos, targetTransform);
                float G = Physics.gravity.y;
                float tanAlpha = Mathf.Tan(angle * Mathf.Deg2Rad);
                float H = targetTransform.y - transform.position.y;

                float Vz = Mathf.Sqrt(G * R * R / (rotation_speed * (H - R * tanAlpha)));
                float Vy = tanAlpha * Vz;

                Vector3 localVelocity = new Vector3(0, Vy, Vz);
                Vector3 globalVelocity = transform.TransformDirection(localVelocity);
                float xRot = 0;

                if (transform.position.y >= topHight || isH)
                {
                    isH = true;

                    if (count < 35)
                        count++;

                    xRot = 70 + count + globalVelocity.z;
                }
                else if (!isH)
                {

                    xRot = 70 - globalVelocity.z;
                }

                transform.LookAt(targetTransform);
                transform.eulerAngles = new Vector3(xRot, transform.eulerAngles.y, transform.eulerAngles.z);

            }
            else
            {
                transform.LookAt(targetTransform);
                transform.rotation = Quaternion.AngleAxis(v0 * t * Mathf.Sin(angle) * rotation_speed, Vector3.left);
                
            }

            t += Time.deltaTime;

            yield return null;
        }


        boxCollider.enabled = false;

    }


    public void DestroyArrow()
    {
        Destroy(gameObject);
    }



}

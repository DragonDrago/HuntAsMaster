using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : MonoBehaviour, IController
{

    [SerializeField]
    private LineRenderer _lineRenderer;

    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private Transform _ArrowPoint;

    [SerializeField]
    private Transform _arrowParentTransform;

    [SerializeField]
    private float _step;

    [SerializeField]
    private UIView uiReloadView;

    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Slider slider1;

    [SerializeField]
    private VariableJoystick joystick;

    [SerializeField]
    private float targetSpeed = 10;

    [SerializeField]
    private GameObject dotPrefab;

    [SerializeField]
    private int numberOfPoints;

    [SerializeField]
    private AudioClip bowAudioClip;

    [SerializeField]
    private AudioClip axeAndWandAudioClip;

    private GameObject[] pointDots;

    private List<IArrow> arrowList = new List<IArrow>();

    private Weapon weaponPrefab;

    private Animator animator;
    private AudioSource audioSource;

    private bool isPlay, isDown;

    private float strongSpeed = 3f;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        GameManager.Instance.AddController(this);

        pointDots = new GameObject[numberOfPoints];

        for(int i = 0; i < numberOfPoints; i++)
        {
            pointDots[i] = Instantiate(dotPrefab, _ArrowPoint);
            pointDots[i].transform.position = new Vector3(0f, -100f, 0f);
        }

    }

    public void Set(Animator animator, Weapon weaponPrefab)
    {
        this.animator = animator;
        this.weaponPrefab = weaponPrefab;

        if(weaponPrefab.weaponType == Weapon.WeaponType.none)
        {
            this.animator.SetTrigger("bow");
        }
        else
        {
            this.animator.SetTrigger("axe");
        }
    }

    public void AddArrow(IArrow arrow)
    {
        if (!arrowList.Contains(arrow))
        {
            arrowList.Add(arrow);
        }
    }

    public void RemoveAllArrow()
    {
        foreach (IArrow arrow in arrowList)
        {
            arrow.DestroyArrow();
        }

        arrowList.Clear();
    }

    private void Update()
    {
        if (Constants.isFinish || !isPlay || !isDown)
            return;


        // target control
        float horizOffset = joystick.Horizontal * targetSpeed * Time.deltaTime;
        float vertiOffset = joystick.Vertical * targetSpeed * Time.deltaTime;

        float rawHorizPos = targetTransform.localPosition.x + horizOffset;
        float rawVertiPos = targetTransform.localPosition.z + vertiOffset;

        float clampHorizPos = Mathf.Clamp(rawHorizPos, -15f, 15f);
        float clampVertiPos = Mathf.Clamp(rawVertiPos, -40f, 40f);

        targetTransform.localPosition = new Vector3(clampHorizPos, 0.5f, clampVertiPos);



        Vector3 direction = targetTransform.position - _ArrowPoint.position;
        Vector3 groundDirection = new Vector3(direction.x, 0f, direction.z);
        Vector3 targetPos = new Vector3(groundDirection.magnitude, direction.y, 0f);

        float height = targetPos.y + targetPos.magnitude / 4f;
        height = Mathf.Max(0.01f, height);
        float angle = 0;
        float v0 = 0;
        float time = 0;

        CalculatePathWithHeight(targetPos, height, out v0, out angle, out time);

        if (Input.GetMouseButtonDown(0))
        {
            targetTransform.gameObject.SetActive(true);
            targetTransform.DOScale(new Vector3(2f, 0.1f, 2f), 1f).From(new Vector3(1f, 0.1f, 1f)).SetLoops(-1, LoopType.Yoyo);
        }

        if (Input.GetMouseButton(0))
        {
            DrawPath(groundDirection.normalized, v0, angle, time, _step);
        }


        if (Input.GetMouseButtonUp(0))
        {

            isDown = false;
            // weapon create
            Weapon weapon = Instantiate(weaponPrefab, _arrowParentTransform);

            weapon.SetMovement(targetTransform.position, height, _ArrowPoint.position, groundDirection.normalized, v0, angle, time);

            if (!arrowList.Contains(weapon))
            {
                arrowList.Add(weapon);
            }

            if (Constants.isVibration)
            {
                MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
            }

            if(Constants.isSound)
            {
                if(audioSource != null)
                {
                    if(weapon.weaponType == Weapon.WeaponType.bow)
                    {
                        audioSource.clip = bowAudioClip;
                    }
                    else
                    {
                        audioSource.clip = axeAndWandAudioClip;
                    }

                    audioSource.Play();
                }
            }

            StartCoroutine(WaitForShoot());

        }
    }



    private IEnumerator WaitForShoot()
    {
        joystick.gameObject.SetActive(false);

        for (int i = 0; i < numberOfPoints; i++)
        {
            pointDots[i].transform.position = new Vector3(0f, -100f, 0f);
        }

        targetTransform.DOKill();
      //  targetTransform.gameObject.SetActive(false);
        _lineRenderer.positionCount = 0;

        uiReloadView.Show();
        slider.value = 1;
        slider1.value = 1;

        slider.DOValue(0f, strongSpeed);
        slider1.DOValue(0f, strongSpeed);

        yield return new WaitForSeconds(strongSpeed);

        uiReloadView.Hide();

        if(!Constants.isFinish)
        joystick.gameObject.SetActive(true);
    }


    private void DrawPath(Vector3 direction, float v0, float angle, float time, float step)
    {
        step = Mathf.Max(0.01f, step);

        _lineRenderer.positionCount = (int)(time / step) + 2;

        int count = 0;

        for (float i = 0; i < time; i += step)
        {
            float x = v0 * i * Mathf.Cos(angle);
            float y = v0 * i * Mathf.Sin(angle) - 0.5f * -Physics.gravity.y * Mathf.Pow(i, 2);

            _lineRenderer.SetPosition(count, _ArrowPoint.position + direction * x + Vector3.up * y);
            pointDots[count].transform.position = _lineRenderer.GetPosition(count);

            count++;

        }

        float xfinal = v0 * time * Mathf.Cos(angle);
        float yfinal = v0 * time * Mathf.Sin(angle) - 0.5f * -Physics.gravity.y * Mathf.Pow(time, 2);

        _lineRenderer.SetPosition(count, _ArrowPoint.position + direction * xfinal + Vector3.up * yfinal);
        pointDots[count].transform.position = _lineRenderer.GetPosition(count);

        for(int i = _lineRenderer.positionCount; i < numberOfPoints; i++)
        {
            pointDots[i].transform.position = new Vector3(0f, -100f, 0f);
        }
    }


    private float QuadraticEquation(float a, float b, float c, float sign)
    {
        return (-b + sign * Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
    }

    private void CalculatePathWithHeight(Vector3 targetPos, float h, out float v0, out float angle, out float time)
    {
        float xt = targetPos.x;
        float yt = targetPos.y;
        float g = -Physics.gravity.y;

        float b = Mathf.Sqrt(2 * g * h);
        float a = (-0.5f * g);
        float c = yt;

        float tplus = QuadraticEquation(a, b, c, 1);
        float tmin = QuadraticEquation(a, b, c, -1);

        time = tplus > tmin ? tplus : tmin;

        angle = Mathf.Atan(b * time / xt);

        v0 = b / Mathf.Sin(angle);

    }

    public void OnLoaded()
    {

    }

    public void OnPlay()
    {
        strongSpeed = 3 - Constants.currentStrongUpgradeLevel * 0.01f;

        if (strongSpeed < 0.5f)
            strongSpeed = 0.5f;

        if(Constants.currentTargetUpgradeLevel < 201)
            targetSpeed = 10 + Constants.currentTargetUpgradeLevel * 0.01f;

        if (targetSpeed > 12)
            targetSpeed = 12;

        joystick.gameObject.SetActive(false);
        uiReloadView.Hide();
        StartCoroutine(WaitPlay());
    }

    private IEnumerator WaitPlay()
    {
        yield return new WaitForSeconds(1f);


        isPlay = true;
        joystick.gameObject.SetActive(true);
    }

    public void OnPause()
    {
        
    }

    public void OnTargetBegin()
    {
        isDown = true;
    }

    public void OnTargeting()
    {
        isDown = true;
    }

    public void OnTargetEnd()
    {
        animator.SetTrigger("attack");   
    }

    public void OnFinish()
    {
        targetTransform.gameObject.SetActive(false);
        _lineRenderer.positionCount = 0;

        uiReloadView.Hide();

        joystick.gameObject.SetActive(false);
        isPlay = false;

        gameObject.SetActive(false);
    }

    public void OnFaild()
    {
        targetTransform.gameObject.SetActive(false);
        _lineRenderer.positionCount = 0;

        uiReloadView.Hide();

        joystick.gameObject.SetActive(false);

        gameObject.SetActive(false);
    }

    public void OnVectory()
    {
        animator.SetTrigger("vectory");

        targetTransform.gameObject.SetActive(false);
        _lineRenderer.positionCount = 0;

        uiReloadView.Hide();

        joystick.gameObject.SetActive(false);
        isPlay = false;

        gameObject.SetActive(false);
    }

    public void OnBackward()
    {
        gameObject.SetActive(true);

        Set(animator, weaponPrefab);
    }

    public void OnRestart()
    {
        StopAllCoroutines();

        targetTransform.gameObject.SetActive(false);
        _lineRenderer.positionCount = 0;
        uiReloadView.Hide();

        for (int i = 0; i < numberOfPoints; i++)
        {
            pointDots[i].transform.position = new Vector3(0f, -100f, 0f);
        }

        gameObject.SetActive(true);
        RemoveAllArrow();

        if(weaponPrefab.weaponType == Weapon.WeaponType.none)
        {
            animator.SetTrigger("bow");
        }
        else
        {
            animator.SetTrigger("axe");
        }
    }
}

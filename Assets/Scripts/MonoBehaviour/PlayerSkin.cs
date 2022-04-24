using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    public Animator GetAnimator { get { return animator; } }

    [SerializeField]
    private Transform rightHandTransform;
    public Transform GetRightHandTransform { get { return rightHandTransform; } }

    [SerializeField]
    private Transform leftHandTransform;
    public Transform GetLeftHandTransform { get { return leftHandTransform; } }


}

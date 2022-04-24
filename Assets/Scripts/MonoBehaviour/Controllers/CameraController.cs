using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour, IController
{

    [SerializeField]
    private Transform followTransform;

    [SerializeField]
    private CinemachineVirtualCamera playerCamera;

    [SerializeField]
    private GiftSkin giftSkin;

    //idle
    private Vector3 idlePos = new Vector3(1f, 0.5f, 1f);
    private Vector3 idleRot = new Vector3(-10f, -150f, 0f);

    //play
    private Vector3 playPos = new Vector3(0f, 2f, -1f);
    private Vector3 playRot = new Vector3(15f, 0f, 0f);

    //target
    private Vector3 targetPos = new Vector3(0f, 2f, -0.5f);
    private Vector3 targetRot = new Vector3(0f, 0f, 0f);

    //top
    private Vector3 topPos = new Vector3(0f, 20f, 10f);
    private Vector3 topRot = new Vector3(40f, 0f, 0f);

    //finish
    private Vector3 finishPos = new Vector3(0f, 27, -7.5f);
    private Vector3 finishRot = new Vector3(45f, 0f, 0f);


    private void Start()
    {
        GameManager.Instance.AddController(this);

        IdleCamera();
    }


    public void IdleCamera()
    {
        followTransform.DOLocalMove(idlePos, 1f);
        followTransform.DORotate(idleRot, 1f);
    }


    public void PlayCamera()
    {
        followTransform.DOLocalMove(playPos, 1f);
        followTransform.DORotate(playRot, 1f);
    }


    public void TargetCamera()
    {
        followTransform.DOLocalMove(targetPos, 1f);
       // followTransform.DORotate(targetRot, 1f);

        playerCamera.Priority = 1;
    }


    public void TopCamera()
    {
        followTransform.DOLocalMove(topPos, 1f);
       // followTransform.DORotate(topRot, 1f);

        playerCamera.Priority = -1;
    }

    public void FinishCamera()
    {
        followTransform.DOLocalMove(idlePos, 1f);
        followTransform.DORotate(idleRot, 1f);
    }

    public void OnLoaded()
    {

    }

    public void OnPlay()
    {
        PlayCamera();
    }

    public void OnPause()
    {
        
    }

    public void OnTargetBegin()
    {
        TargetCamera();
    }

    public void OnTargeting()
    {
        TopCamera();
    }

    public void OnTargetEnd()
    {
        TargetCamera();
    }

    public void OnFaild()
    {
        PlayCamera();
    }

    public void OnVectory()
    {
        IdleCamera();
        giftSkin.Show(false);
    }

    public void OnFinish()
    {
        IdleCamera();
        giftSkin.Show(true);
    }

    public void OnBackward()
    {
        TargetCamera();
    }

    public void OnRestart()
    {
        DOTween.KillAll();
        IdleCamera();
        giftSkin.Hide();
    }
}

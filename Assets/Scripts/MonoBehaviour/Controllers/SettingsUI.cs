using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Doozy.Engine.Soundy;
using MoreMountains.NiceVibrations;

public class SettingsUI : MonoBehaviour
{
    private const string key_sound = "keySound";
    private const string key_music = "keyMusic";
    private const string key_vibration = "keyVibration";

    [SerializeField]
    private RectTransform panelTransform;

    [SerializeField]
    private Image soundOnImage, soundOffImage;

    [SerializeField]
    private Image musicOnImage, musicOffImage;

    [SerializeField]
    private Image vibrationOnImage, vibrationOffImage;

    private bool isSetting = true;
    private bool isSound = true;
    private bool isMusic = true;
    private bool isVibration = true;

    private bool isStart = false;

    private void Awake()
    {
        isSound = PlayerPrefs.GetInt(key_sound) == 1 ? false : true;
        isMusic = PlayerPrefs.GetInt(key_music) == 1 ? false : true;
        isVibration = PlayerPrefs.GetInt(key_vibration) == 1 ? false : true;
    }

    private void Start()
    {
        isStart = true;
        SoundUpdat();
        MusicUpdate();
        VibrationUpdate();
    }

    public void OnClickSetting()
    {
        isSetting = !isSetting;

        panelTransform.DOKill();

        if(isSetting)
        {
            panelTransform.DOAnchorPosY(2000f, 0.3f).SetEase(Ease.InOutSine);
        }
        else
        {
            ClosePanel();
        }
    }

    public void HideSetting()
    {
        if (isSetting)
            panelTransform.DOAnchorPosY(0f, 0.03f).SetEase(Ease.InOutSine);
    }

    private void ClosePanel()
    {
        panelTransform.DOAnchorPosY(0f, 0.3f).SetEase(Ease.InOutSine);
    }

    public void OnClickSound()
    {
        isSound = !isSound;
        SoundUpdat();

        SendEvent();
    }

    private void SoundUpdat()
    {
        Constants.isSound = isSound;

        if (isSound)
        {
            PlayerPrefs.SetInt(key_sound, 0);
            SoundyManager.UnmuteAllSounds();
            soundOnImage.DOFade(1f, 0.1f);
            soundOffImage.DOFade(0f, 0.1f);
        }
        else
        {
            PlayerPrefs.SetInt(key_sound, 1);
            SoundyManager.MuteAllSounds();
            soundOnImage.DOFade(0f, 0.1f);
            soundOffImage.DOFade(1f, 0.1f);
        }
    }

    public void OnClickMusic()
    {
        isMusic = !isMusic;
        MusicUpdate();

        SendEvent();
    }

    private void MusicUpdate()
    {
        Constants.isMusic = isMusic;

        if (isMusic)
        {
            PlayerPrefs.SetInt(key_music, 0);
            musicOnImage.DOFade(1f, 0.1f);
            musicOffImage.DOFade(0f, 0.1f);

            GameManager.Instance.PlayMenuMusic();
        }
        else
        {
            PlayerPrefs.SetInt(key_music, 1);
            musicOnImage.DOFade(0f, 0.1f);
            musicOffImage.DOFade(1f, 0.1f);

            GameManager.Instance.StopMusic();
        }
    }

    public void OnClickVibration()
    {
        isVibration = !isVibration;
        VibrationUpdate();
        SendEvent();
    }

    private void VibrationUpdate()
    {
        Constants.isVibration = isVibration;

        if (isVibration)
        {
            if(!isStart)
                MMVibrationManager.Haptic(HapticTypes.HeavyImpact);

            PlayerPrefs.SetInt(key_vibration, 0);
            vibrationOnImage.DOFade(1f, 0.1f);
            vibrationOffImage.DOFade(0f, 0.1f);
        }
        else
        {
            PlayerPrefs.SetInt(key_vibration, 1);
            vibrationOnImage.DOFade(0f, 0.1f);
            vibrationOffImage.DOFade(1f, 0.1f);
        }

        isStart = false;
    }


    public void OnClickJoystickControl()
    {
        Constants.OpenPopup(Constants.popup_joystick_control);
    }

    public void OnClickLanguage()
    {
        Constants.OpenPopup(Constants.popup_language);

    }

    public void OnClickPrivate()
    {

    }


    private void SendEvent()
    {
        AppMetricaSendEventContrrol.Settings(isMusic ? 1 : 0,
                                             isSound ? 1 : 0,
                                             isVibration ? 1 : 0,
       Constants.joystickControl == 0 ? "right" : "left",
       Constants.language_current);
    }

}

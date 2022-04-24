using System.Collections;
using System.Collections.Generic;
using Lean.Common;
using UnityEngine;
using UnityEngine.UI;

public enum ENUM_Device_Type
{
    Tablet,
    Phone
}

public class ScreenResize : MonoBehaviour
{

    [SerializeField]
    private CanvasScaler canvas;

    private void Awake()
    {
        SetSize();
    }


    public bool isTablet;

    private float DeviceDiagonalSizeInInches()
    {
        float screenWidth = Screen.width / Screen.dpi;
        float screenHeight = Screen.height / Screen.dpi;
        float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));

        return diagonalInches;
    }

    public ENUM_Device_Type GetDeviceType()
    {
#if UNITY_IOS
    bool deviceIsIpad = UnityEngine.iOS.Device.generation.ToString().Contains("iPad");
            if (deviceIsIpad)
            {
                return ENUM_Device_Type.Tablet;
            }
            bool deviceIsIphone = UnityEngine.iOS.Device.generation.ToString().Contains("iPhone");
            if (deviceIsIphone)
            {
                return ENUM_Device_Type.Phone;
            }
#endif
        //#elif UNITY_ANDROID

        float aspectRatio = Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height);
        bool isTablet = (DeviceDiagonalSizeInInches() > 6.5f && aspectRatio < 2f);

        if (isTablet)
        {
            return ENUM_Device_Type.Tablet;
        }
        else
        {
            return ENUM_Device_Type.Phone;
        }
        //#endif
    }





    private void SetSize()
    {
        if (GetDeviceType() == ENUM_Device_Type.Phone)
        {
            // it's a phone
            canvas.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

            canvas.referenceResolution = new Vector2(2560, 1440);
        }
        else
        {
            // it's tablet
            
            canvas.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;

            float temp = 1f - ((Screen.width) / (float)Screen.height);

            if (temp <= 0.1f)
                temp = 0.5f;

            #if UNITY_IOS
                temp = 0.5f;
            #endif


            canvas.scaleFactor = temp;
        }
    }

    //public static bool IsTablet()
    //{

    //    float ssw;
    //    if (Screen.width > Screen.height) { ssw = Screen.width; } else { ssw = Screen.height; }

    //    if (ssw < 800) return false;

    //    if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
    //    {
    //        float screenWidth = Screen.width / Screen.dpi;
    //        float screenHeight = Screen.height / Screen.dpi;
    //        float size = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));
    //        if (size >= 6.5f) return true;
    //    }

    //    return false;
    //}

    //calculate physical inches with pythagoras theorem
    //public float DeviceDiagonalSizeInInches()
    //{
    //    float screenWidth = Screen.width / Screen.dpi;
    //    float screenHeight = Screen.height / Screen.dpi;
    //    float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));

    //    Debug.Log("Getting device inches: " + diagonalInches);

    //    return diagonalInches;
    //}



}
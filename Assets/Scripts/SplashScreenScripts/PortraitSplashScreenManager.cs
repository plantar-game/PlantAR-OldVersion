/*==============================================================================
Copyright (c) 2012-2013 QUALCOMM Austria Research Center GmbH.
All Rights Reserved.
==============================================================================*/
using UnityEngine;
using System.Collections;

/// <summary>
/// Splash screen manager.
/// 
/// Draws a SplashScreen with AutoRotation enabled
/// using a GUI Texture for different devices.
/// After 2 seconds of visibility it calls the
/// AboutScreen Scene.
/// </summary>
public class PortraitSplashScreenManager : MonoBehaviour
{
    #region PUBLIC_MEMBER_VARIABLES

    public Texture PortraitTextureAndroid;
    
    public Texture PortraitTextureIPad;
    
    public Texture PortraitTextureIPhone;

    public Texture PortraitTextureIPhone5;

    public Texture LandscapeTexturePlaymode;

    public float SecondsVisible = 2.0f;

    #endregion // PUBLIC_MEMBER_VARIABLES



    #region UNITY_MONOBEHAVIOUR_METHODS
    
    void Start ()
    {
        // on Unity 4 Android, the first ~3.5sec nothing is rendered...
        if ((Application.platform == RuntimePlatform.Android) && (int.Parse(Application.unityVersion.Substring(0, 1)) >= 4))
            SecondsVisible += 3.5f;
        // Loads the About Scene after N seconds
        Invoke("LoadAboutScene", SecondsVisible);
    }

    private void OnGUI()
    {
        if (QCARRuntimeUtilities.IsPlayMode())
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), LandscapeTexturePlaymode);
        }
        else
        {

#if UNITY_IPHONE

            if (iPhone.generation == iPhoneGeneration.iPhone5)
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), PortraitTextureIPhone5);
            }
            else if (iPhone.generation == iPhoneGeneration.iPhone)
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), PortraitTextureIPhone);
            }
            else
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), PortraitTextureIPad);
            }

#else

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), PortraitTextureAndroid);

#endif

        }
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS



    #region PRIVATE_METHODS

    /// <summary>
    /// Loads the about scene.
    /// </summary>
    private void LoadAboutScene()
    {
        Application.LoadLevel("Vuforia-2-AboutScreen");
    }

    #endregion // PRIVATE_METHODS
}

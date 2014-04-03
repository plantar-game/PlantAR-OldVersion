/*==============================================================================
            Copyright (c) 2010-2013 QUALCOMM Austria Research Center GmbH.
            All Rights Reserved.
            Qualcomm Confidential and Proprietary
==============================================================================*/

using UnityEngine;
using System;

/// <summary>
/// Menu that appears on double tap, enables and disables the AutoFocus on the camera.
/// </summary>
public class CameraDeviceMenu : MonoBehaviour, ITrackerEventHandler
{
    #region PRIVATE_MEMBER_VARIABLES

    // Check if a menu button has been pressed.
    private bool mButtonPressed = false;

    // If the menu is currently open
    private bool mMenuOpen = false;

    // Contains if the device supports continous autofocus
    private bool mContinousAFSupported = true;

    // Contains the currently set auto focus mode.
    private CameraDevice.FocusMode mFocusMode =
        CameraDevice.FocusMode.FOCUS_MODE_NORMAL;

    // Contains the rectangle for the camera options menu.
    private Rect mAreaRect;

    // this is used to distinguish single and double taps
    private bool mWaitingForSecondTap;
    private Vector3 mFirstTapPosition;
    private DateTime mFirstTapTime;
    // the maximum distance that is allowed between two taps to make them count as a double tap
    // (relative to the screen size)
    private const float MAX_TAP_DISTANCE_SCREEN_SPACE = 0.1f;
    private const int MAX_TAP_MILLISEC = 500;

    #endregion // PRIVATE_MEMBER_VARIABLES



    #region UNTIY_MONOBEHAVIOUR_METHODS

    public void Start()
    {
        // register for the OnInitialized event at the QCARBehaviour
        QCARBehaviour qcarBehaviour = (QCARBehaviour)FindObjectOfType(typeof(QCARBehaviour));
        if (qcarBehaviour)
        {
            qcarBehaviour.RegisterTrackerEventHandler(this);
        }

        // Setup position and size of the camera menu.
        ComputePosition();
    }

	public void Update()
	{
		 if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("Vuforia-2-AboutScreen");
        }
		
        mFocusMode = CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO;
	}

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS

    #region ITrackerEventHandler_IMPLEMENTATION

    /// <summary>
    /// This method is called when QCAR has finished initializing
    /// </summary>
    public void OnInitialized()
    {
        // try to set continous auto focus as default
        if (CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO))
        {
            mFocusMode = CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO;
        }
        else
        {
            Debug.LogError("could not switch to continuous autofocus");
            mContinousAFSupported = false;
        }
    }

    public void OnTrackablesUpdated()
    {
        // not used
    }

    #endregion //ITrackerEventHandler_IMPLEMENTATION



    #region PRIVATE_METHODS

    private void HandleSingleTap()
    {
        mWaitingForSecondTap = false;

        if (mMenuOpen)
            mMenuOpen = false;
        else
        {
            // trigger focus once
            if (CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO))
                mFocusMode = CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO;
        }
    }

    private void HandleDoubleTap()
    {
        mWaitingForSecondTap = false;
        mMenuOpen = true;
    }

    /// Compute the coordinates of the menu depending on the current orientation.
    private void ComputePosition()
    {
        int areaWidth = Screen.width;
        int areaHeight = (Screen.height / 5) * 2;
        int areaLeft = 0;
        int areaTop = Screen.height - areaHeight;
        mAreaRect = new Rect(areaLeft, areaTop, areaWidth, areaHeight);
    }

    #endregion // PRIVATE_METHODS
}

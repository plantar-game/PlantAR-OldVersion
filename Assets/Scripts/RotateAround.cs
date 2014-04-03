/*==============================================================================
            Copyright (c) 2010-2013 QUALCOMM Austria Research Center GmbH.
            All Rights Reserved.
            Qualcomm Confidential and Proprietary
==============================================================================*/

using UnityEngine;

/// <summary>
/// A simple behaviour to rotate the game object this script is attached to
/// </summary>
public class RotateAround : MonoBehaviour
{
    #region UNTIY_MONOBEHAVIOUR_METHODS
    
    void Update () 
    {
        Transform parentTransform = transform.parent;
        transform.RotateAround(parentTransform.position, parentTransform.up, -60 * Time.deltaTime);
    }

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS
}

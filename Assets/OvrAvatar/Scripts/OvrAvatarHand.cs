using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class OvrAvatarHand : MonoBehaviour, IAvatarPart
{
    public Hands handScript;

    float alpha = 1.0f;

    public void UpdatePose(OvrAvatarDriver.ControllerPose pose)
    {
        if (GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().detectCollisions = 
                handScript.mHandState == Hands.State.EMPTY && pose.handTrigger >= 0.75f;
        }
    }

    public void SetAlpha(float alpha)
    {
        this.alpha = alpha;
    }

    public void OnAssetsLoaded()
    {
        SetAlpha(this.alpha);
    }
}
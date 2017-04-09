using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{

    public enum State
    {
        EMPTY,
        TOUCHING,
        HOLDING
    };

    public OVRInput.Controller Controller = OVRInput.Controller.LTouch;
    public State mHandState = State.EMPTY;
    public Rigidbody AttachPoint = null;
    public bool IgnoreContactPoint = false;
    private Rigidbody mHeldObject;
    private FixedJoint mTempJoint;

    void Start()
    {
        if (AttachPoint == null)
        {
            AttachPoint = GetComponent<Rigidbody>();
        }
    }

    //void OnTriggerEnter(Collider collider)
    //{
    //    if (mHandState == State.EMPTY)
    //    {
    //        GameObject temp = collider.gameObject;
    //        if (temp != null && temp.layer == LayerMask.NameToLayer("grabbable") && temp.GetComponent<Rigidbody>() != null)
    //        {
    //            mHeldObject = temp.GetComponent<Rigidbody>();
    //            mHandState = State.TOUCHING;
    //        }
    //    }
    //}

    void OnTriggerEnter(Collider collider)
    {
        if (mHandState == State.EMPTY && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, Controller) < 0.5f)
        {
            GameObject temp = collider.gameObject;
            if (temp != null && temp.layer == LayerMask.NameToLayer("grabbable") && temp.GetComponent<Rigidbody>() != null)
            {
                mHeldObject = temp.GetComponent<Rigidbody>();
                mHandState = State.TOUCHING;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (mHandState != State.HOLDING)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("grabbable"))
            {
                mHeldObject = null;
                mHandState = State.EMPTY;
            }
        }
    }

    void Update()
    {
        switch (mHandState)
        {
            case State.TOUCHING:
                if (mTempJoint == null && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, Controller) >= 0.5f)
                {
                    mHeldObject.velocity = Vector3.zero;
                    mTempJoint = mHeldObject.gameObject.AddComponent<FixedJoint>();
                    mTempJoint.connectedBody = AttachPoint;
                    mHandState = State.HOLDING;
                }
                break;
            case State.HOLDING:
                if (mTempJoint != null && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, Controller) < 0.5f)
                {
                    Object.DestroyImmediate(mTempJoint);
                    mTempJoint = null;
                    throwObject();
                    mHandState = State.EMPTY;
                }
                break;
        }
    }

    private void throwObject()
    {
        mHeldObject.velocity = OVRInput.GetLocalControllerVelocity(Controller);
        mHeldObject.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(Controller).eulerAngles * Mathf.Deg2Rad;
        mHeldObject.maxAngularVelocity = mHeldObject.angularVelocity.magnitude;
    }
}
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MyHandJointEventHandler : MonoBehaviour, IMixedRealityHandJointHandler, IMixedRealityGestureHandler
{
    public Handedness myHandedness;

    public void OnGestureCanceled(InputEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnGestureCompleted(InputEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnGestureStarted(InputEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnGestureUpdated(InputEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    void IMixedRealityHandJointHandler.OnHandJointsUpdated(InputEventData<IDictionary<TrackedHandJoint, MixedRealityPose>> eventData)
    {
        if (eventData.Handedness == myHandedness)
        {
            if (eventData.InputData.TryGetValue(TrackedHandJoint.IndexTip, out MixedRealityPose pose))
            {
            }
        }
    }
}

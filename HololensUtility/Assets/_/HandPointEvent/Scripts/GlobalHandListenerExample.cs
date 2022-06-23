using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections.Generic;
using UnityEngine;

public class GlobalHandListenerExample : MonoBehaviour, IMixedRealityHandJointHandler 
{
    private MixedRealityPose IndexTip, ThumbTip;
    private float gap = 0.015f;

    public GameObject MyPrefab;


    private void OnEnable()
    {
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityHandJointHandler>(this);
    }

    private void OnDisable()
    {
        CoreServices.InputSystem?.UnregisterHandler<IMixedRealityHandJointHandler>(this);
    }

    /// <summary>
    /// 抓取手部各節點座標
    /// </summary>
    public void OnHandJointsUpdated(
                InputEventData<IDictionary<TrackedHandJoint, MixedRealityPose>> eventData)
    {
        if (eventData.Handedness != Handedness.Right) return;

        eventData.InputData.TryGetValue(TrackedHandJoint.IndexTip, out IndexTip);
        eventData.InputData.TryGetValue(TrackedHandJoint.ThumbTip, out ThumbTip);

        if (IndexTip != null && ThumbTip != null
            && Vector3.Distance(IndexTip.Position, ThumbTip.Position) < gap)
        {
            var spawnPosition = IndexTip.Position;
            var spawnRotation = IndexTip.Rotation;
            Instantiate(MyPrefab, spawnPosition, spawnRotation);
        }
    }
}

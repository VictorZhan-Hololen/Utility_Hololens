using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections.Generic;
using UnityEngine;

public class AirDrawUtility : MonoBehaviour, IMixedRealityHandJointHandler
{
    private MixedRealityPose IndexTip, ThumbTip;
    private float gap = 0.015f;

    public GameObject MyPrefab;

    public void Start()
    {
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityHandJointHandler>(this);
    }

    public void Stop()
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

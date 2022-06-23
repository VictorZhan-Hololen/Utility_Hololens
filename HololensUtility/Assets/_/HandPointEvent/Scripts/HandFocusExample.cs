using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

/// <summary>
/// 手勢控制，近距離和遠距射線的操作皆會觸發
/// </summary>
public class HandFocusExample : MonoBehaviour, IMixedRealityFocusHandler, IMixedRealityPointerHandler
{
    /// <summary>
    /// 手部射線開關
    /// </summary>
    public void HandRayPointerSetting(bool isOn)
    {
        PointerUtils.SetHandRayPointerBehavior(
            (isOn) ? PointerBehavior.Default : PointerBehavior.AlwaysOff);
    }

    public Color onIdle = Color.cyan;
    public Color onHover = Color.red;
    public Color onDown = Color.blue;
    public Color onDrag = Color.yellow;
    public Color onSelect = Color.green;

    private Material material;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    void IMixedRealityFocusHandler.OnFocusEnter(FocusEventData eventData)
    {
        Debug.Log("OnFocusEnter");
    }

    void IMixedRealityFocusHandler.OnFocusExit(FocusEventData eventData)
    {
        Debug.Log("OnFocusUp");
        material.color = onIdle;
    }

    void IMixedRealityPointerHandler.OnPointerDown(MixedRealityPointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        material.color = onDown;
    }

    void IMixedRealityPointerHandler.OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        Debug.Log("OnPointerDragged");
        material.color = onDrag;
    }

    void IMixedRealityPointerHandler.OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        Debug.Log("OnPointerClicked");
        material.color = onSelect;

        
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        Debug.Log("OnPointerUp");
        material.color = onIdle;
    }
}
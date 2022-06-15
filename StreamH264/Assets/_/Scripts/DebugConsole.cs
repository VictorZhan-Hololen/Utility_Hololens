using System;
using UnityEngine;
using UnityEngine.UI;

public class DebugConsole : MonoBehaviour
{
    [SerializeField] private Text console;
    [SerializeField] private ScrollRect scrollRect;

    void OnEnable()
    {
        Application.logMessageReceived += LogMessage;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= LogMessage;
    }

    public void LogMessage(string message, string stackTrace, LogType type)
    {
        string colorStr = "FFFFFF";
        if (console != null)
        {
            if (console.text.Length > 3000) console.text = $"<color=#{colorStr}> Console Cls </color>\n";

            if (type == LogType.Error || type == LogType.Exception) colorStr = "FF0000";
            console.text += $"<color=#{colorStr}>{message}</color>\n";
            ScrollToBottom(scrollRect);
        }
    }
    private void ScrollToBottom(ScrollRect scrollRect)
    {
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }
}
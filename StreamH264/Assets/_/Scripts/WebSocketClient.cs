using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using WebSocketSharp;

public class WebSocketClient
{
    public Action<WebSocket, System.EventArgs> OnOpen;
    public Action<WebSocket, MessageEventArgs> OnReceiveMessage;
    public Action<WebSocket> OnClose;

    private readonly WebSocket ws;

    public bool isConnect => ws.IsAlive;

    public WebSocketClient(string url)
    {
        ws = new WebSocket(url);
        ws.OnOpen += WS_OnOpen;
        ws.OnMessage += WS_OnReceiveMessage;
    }

    public void AddListener(Action<WebSocket, System.EventArgs> OnOpen, Action<WebSocket, MessageEventArgs> OnReceiveMessage, Action<WebSocket> OnClose)
    {
        this.OnOpen += OnOpen;
        this.OnReceiveMessage += OnReceiveMessage;
        this.OnClose += OnClose;
    }
    public void RemoveListener(Action<WebSocket, System.EventArgs> OnOpen, Action<WebSocket, MessageEventArgs> OnReceiveMessage, Action<WebSocket> OnClose)
    {
        this.OnOpen -= OnOpen;
        this.OnReceiveMessage -= OnReceiveMessage;
        this.OnClose -= OnClose;
    }

    public void Connect() => ws.Connect();

    private void WS_OnOpen(object sender, System.EventArgs e)
    {
        Debug.Log($"OnOpen >>> {e}");
        OnOpen?.Invoke((WebSocket)sender, e);
    }

    private void WS_OnReceiveMessage(object sender, MessageEventArgs e)
    {
        OnReceiveMessage?.Invoke((WebSocket)sender, e);
    }

    public void Send(string data)
    {
        Debug.Log($"SendData: {data} / {new DateTime().Second}");
        ws?.Send(data);
    }

    public void Send(byte[] data)
    {
        Debug.Log($"SendData: {data.Length}");
        ws?.Send(data);
    }

    public void Close()
    {
        OnClose?.Invoke(ws);
        ws.OnOpen -= WS_OnOpen;
        ws.OnMessage -= WS_OnReceiveMessage;
        RemoveListener(OnOpen, OnReceiveMessage, null);
        ws?.Close();
    }
}

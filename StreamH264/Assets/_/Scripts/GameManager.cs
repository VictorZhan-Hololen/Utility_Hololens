using Newtonsoft.Json;
using System;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Config config;
    [SerializeField] private RawImageScreen rawImage;

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private AudioSource audioSource;

    private WebSocketClient webSocketClient;
    private CameraManager cameraManager;
    private StreamingHandler streamingHandler;

    void Start()
    {
        Debug.Log("Start");
        webSocketClient = new WebSocketClient(config.URL.WebSocket_URL);
        webSocketClient.AddListener(OnOpen, OnReceiveMessage, OnClose);
        webSocketClient.Connect();

        cameraManager = new CameraManager(rawImage.LocalCamera);
        cameraManager.StartCamera();
        streamingHandler = new StreamingHandler(meshRenderer, rawImage.RemoteVideo, audioSource);
    }
    /// <summary> 連上WebSocket時 </summary>
    private void OnOpen(WebSocket ws, EventArgs arg2)
    {
        webSocketClient.Send(JsonToString(config.Join));
    }

    /// <summary> 收到WebSocket的資料時 </summary>
    private void OnReceiveMessage(WebSocket ws, MessageEventArgs e)
    {
        streamingHandler?.OnReceiveMessage(e);
    }

    /// <summary> 斷線WebSocket時 </summary>
    private void OnClose(WebSocket ws)
    {
        webSocketClient.Send(JsonToString(config.Leave));
        webSocketClient.RemoveListener(OnOpen, OnReceiveMessage, OnClose);
    }

    private void Update()
    {
        if (streamingHandler != null)
        {
            streamingHandler?.OnUpdate();
            if (streamingHandler.IsRemoteConnected && false)
            {
                webSocketClient.Send(StreamingHandler.EncodeCameraData(cameraManager.CameraTexture));
            }
        }
    }

    private void OnDestroy()
    {
        webSocketClient.Close();
        streamingHandler?.OnDestory();
        cameraManager?.OnDestory();
    }

    private void OnApplicationQuit() => OnDestroy();

    private string JsonToString(object obj) => JsonConvert.SerializeObject(obj, Formatting.Indented);


    [Serializable]
    public struct Config
    {
        public Config_URL URL;
        public Config_Action Join, Leave;
    }

    [Serializable]
    public struct RawImageScreen
    {
        public RawImage RemoteVideo;
        public RawImage LocalCamera;
    }


}

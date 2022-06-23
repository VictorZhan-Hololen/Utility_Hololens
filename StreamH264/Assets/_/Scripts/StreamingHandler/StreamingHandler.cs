using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class StreamingHandler
{
    private VideoDecodeHandler videoDecodeHandler;
    private VideoEncodeHandler videoEncodeHandler;
    private AudioDecodeHandler audioDecodeHandler;

    private string receiveMediaType;

    public bool IsRemoteConnected
    {
        get
        {
            return receiveMediaType == "1" || receiveMediaType == "2";
        }
    }

    public StreamingHandler(MeshRenderer meshRenderer, RawImage remoteCameraRawImage, AudioSource audioSource, RawImage localCameraImage)
    {
        videoDecodeHandler = new VideoDecodeHandler(meshRenderer, remoteCameraRawImage);
        videoEncodeHandler = new VideoEncodeHandler(localCameraImage);
        // audioDecodeHandler = new AudioDecodeHandler(audioSource);
    }

    /// <summary> 從WebSocket收到資料 </summary>
    public void OnReceiveMessage(WebSocketSharp.MessageEventArgs e)
    {
        string msg = Encoding.UTF8.GetString(e.RawData);
        receiveMediaType = AsciiToString(e.RawData[0]);
        if (receiveMediaType == "1") videoDecodeHandler.Decode(e.RawData);
        else if (receiveMediaType == "2") audioDecodeHandler?.Play(e.RawData);
    }
    private string AsciiToString(int value) => ((char)value).ToString();

    public void OnUpdate()
    {
        videoDecodeHandler.OnUpdate();
        audioDecodeHandler?.OnUpdate();
    }

    public void OnDestory()
    {
        videoDecodeHandler.OnDestory();
        audioDecodeHandler?.OnDestory();
    }

    /// <summary> 本地端資料 to byte[] 
    /// <para>Byte[] SumData = Sum (To Byte array) Type(長度1)+Data(長度N)，不足10碼左邊補0</para>
    /// <para>Byte[] TypeData  = TypeData  (To Byte array) 1:Video 2:Audio 3:Image</para>
    /// <para>Byte[] PData = Data (To Byte array)</para>
    /// <para>Byte[] push = SumData + TypeData + PData</para>
    /// </summary>
    private byte[] EncodeToPackage(string type, byte[] data)
    {
        //return videoEncodeHandler.Encode(data);
        return data;
    }

    public byte[] EncodeCameraData()
    {
        return videoEncodeHandler.Encode();
    }

    /*public byte[] EncodeCameraData(Texture2D rawImageTexture)
    {
        return EncodeToPackage("1", rawImageTexture.GetRawTextureData());
    }*/
    /* public static byte[] EncodeVoiceData()
     {
         byte[] data = null;
         return EncodeToPackage("2", data);
     }*/


}

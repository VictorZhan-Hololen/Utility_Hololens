using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class VideoDecodeHandler
{
    private readonly MeshRenderer meshRenderer;
    private readonly RawImage remoteVideoRawImage;

    private readonly int width = 640, height = 480;
    private readonly TextureFormat textureFormat = TextureFormat.BGRA32;

    private readonly Texture2D texture;
    private readonly VideoDecorder videoDecorder;

    private byte[] videoImageData;

    public VideoDecodeHandler(MeshRenderer meshRenderer, RawImage remoteVideoRawImage)
    {
        this.meshRenderer = meshRenderer;
        this.remoteVideoRawImage = remoteVideoRawImage;

        videoDecorder = new VideoDecorder();
        texture = new Texture2D(width, height, textureFormat, false);
        try
        {
            videoDecorder.StartDecoder(width, height);
        }
        catch (Exception exp)
        {
            Debug.LogWarning($"StartDecoder Exception:{exp}");
        }
    }

    /// <summary>
    /// H.264¸Ñ½X 
    /// </summary>
    public void Decode(byte[] data)
    {
        try
        {
            int inputSize = data.Length - 1;
            int outputSize = width * height * 4;

            IntPtr inputData = Marshal.AllocHGlobal(inputSize);
            Marshal.Copy(data, 1, inputData, inputSize);

            int resultValue;

            IntPtr outputData = Marshal.AllocHGlobal(outputSize);
            resultValue = videoDecorder.DecodeH264Frame(inputData, inputSize, outputData);

            //Debug.Log($"DecodeH264Frame:{resultValue}");

            videoImageData = ImageUtil.GetBytes(outputData, outputSize);

            Marshal.FreeHGlobal(inputData);
            Marshal.FreeHGlobal(outputData);
        }
        catch (Exception exp)
        {
            Debug.LogWarning($"Decode Exception:{exp}");
        }
    }

    public void OnUpdate()
    {
        if (videoImageData != null)
        {
            texture.SetPixelData(videoImageData, 0, 0);
            texture.Apply();
            meshRenderer.material.mainTexture = texture;
            meshRenderer.material.mainTextureScale = new Vector2(-1, 1);
            remoteVideoRawImage.texture = texture;
            remoteVideoRawImage.color = Color.white;
        }
    }
    public void OnDestory()
    {
        try
        {
            videoDecorder?.StopDecoder();
#if UNITY_EDITOR_WIN == false
            videoDecorder?.DestroyVideoDecoder();
#endif
        }
        catch (Exception exp)
        {
            Debug.LogWarning($"Desotry Exception:{exp}");
        }
    }
}

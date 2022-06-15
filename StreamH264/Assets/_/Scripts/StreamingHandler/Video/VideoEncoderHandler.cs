using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class VideoEncodeHandler
{
    private readonly RawImage localCameraImage;

    private readonly int width = 640, height = 480;
    private readonly TextureFormat textureFormat = TextureFormat.BGRA32;

    private readonly VideoEncorder videoEncorder;

    public VideoEncodeHandler(RawImage localCameraImage)
    {
        this.localCameraImage = localCameraImage;

        videoEncorder = new VideoEncorder();
        try
        {
            videoEncorder.StartEncoder(width, height);
        }
        catch (Exception exp)
        {
            Debug.LogWarning($"VideoEncorder.StartEncoder Exception:{exp}");
        }
    }

    /// <summary>
    /// ¼v¹³¥[±K
    /// </summary>
    public void Encode(Texture2D texture, byte[] data)
    {
        try
        {
            int inputSize = data.Length - 1;
            int outputSize = width * height * 4;

            IntPtr inputData = Marshal.AllocHGlobal(inputSize);
            Marshal.Copy(data, 1, inputData, inputSize);

            int resultValue;

            IntPtr outputData = Marshal.AllocHGlobal(outputSize);

            int ms = 0;

            resultValue = videoEncorder.EncodeYUV420P(inputData, inputSize, outputData, ms);


            //videoImageData = ImageUtil.GetBytes(outputData, outputSize);

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
       /* if (videoImageData != null)
        {
            texture.SetPixelData(videoImageData, 0, 0);
            texture.Apply();
            meshRenderer.material.mainTexture = texture;
            remoteVideoRawImage.texture = texture;
        }*/
    }
    public void OnDestory()
    {
        try
        {
            videoEncorder?.StopEncoder();
#if UNITY_EDITOR_WIN == true
            videoEncorder?.DestroyVideoEncoder();
#endif
        }
        catch (Exception exp)
        {
            Debug.LogWarning($"VideoEncorder.DestroyVideoEncoder Exception:{exp}");
        }
    }
}

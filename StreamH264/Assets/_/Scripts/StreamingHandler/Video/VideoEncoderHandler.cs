using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using Victor.Utility;
using Video.Utility;

public class VideoEncodeHandler
{
    private readonly RawImage localCameraImage;

    private readonly int width = 640, height = 480;
    private readonly TextureFormat textureFormat = TextureFormat.BGRA32;

    private readonly VideoEncorder videoEncorder;
    private byte[] videoImageData;


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
    /// 影像加密
    /// </summary>
    public byte[] Encode()
    {
        byte[] data = TextureUtility.TextureToTexture2D(localCameraImage.texture).GetRawTextureData();
        string resultStr;

        try
        {
            int inputSize = data.Length;
            int outputSize = width * height * 4;

            IntPtr inputData = Marshal.AllocHGlobal(inputSize);
            Marshal.Copy(data, 0, inputData, inputSize);

            int resultValue;

            IntPtr outputData = Marshal.AllocHGlobal(outputSize);


            int ms = (int)DateTimeUtility.NowToMS;
            Debug.Log($"ms(int):{ms}/ms(long):{DateTimeUtility.NowToMS}");

            resultValue = videoEncorder.EncodeARGB(inputData, inputSize, outputData, ms);

            if (resultValue == -2)
            {
                Debug.LogError($"resultValue:{resultValue}");
                return null;
            }
            Debug.Log($"resultValue:{resultValue}");

            videoImageData = new byte[outputSize];
            Marshal.Copy(outputData, videoImageData, 0, outputSize);
            // 測試點
            Marshal.FreeHGlobal(inputData);
            Marshal.FreeHGlobal(outputData);

            string sum, type = "1", dataStr;
            sum = MathUtility.ToFullNumberString(type + videoImageData.Length, 10);
            dataStr = System.Text.Encoding.UTF8.GetString(videoImageData);
            resultStr = sum + type + dataStr;
            return System.Text.Encoding.UTF8.GetBytes(resultStr);
        }
        catch (Exception exp)
        {
            Debug.LogWarning($"Decode Exception:{exp}");
        }
        return null;
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

using System;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager
{
    private readonly int width = 640, height = 480;
    private Texture2D resultTexture;

    private RawImage localVideoRawImage { get; set; }
    public Texture2D CameraTexture
    {
        get
        {
            try
            {
                resultTexture.SetPixel(width, height, webcamTexture.GetPixel(width, height));
                return resultTexture;

            }
            catch (Exception e)
            {
                Debug.Log($"Exception:{e}");
            }
            return null;
        }
    }

    private WebCamTexture webcamTexture { get; set; }

    public CameraManager(RawImage localCameraRawImage)
    {
        this.localVideoRawImage = localCameraRawImage;

        webcamTexture = new WebCamTexture(width, height);
        localCameraRawImage.texture = webcamTexture;

        resultTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);
    }

    public void StartCamera() => webcamTexture.Play();
    public void StopCamera() => webcamTexture.Stop();

    public void OnDestory() => StopCamera();



    public Texture2D ToTexture2D(Texture texture)
    {
        return Texture2D.CreateExternalTexture(
            texture.width,
            texture.height,
            TextureFormat.RGBA32,
            false, false,
            texture.GetNativeTexturePtr());
    }
}

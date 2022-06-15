using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamManager : MonoBehaviour
{
    public RawImage rawImage;

    private WebCamTexture camTexture;
    void Start()
    {
        camTexture = new WebCamTexture(640,480);
        camTexture.Play();
        rawImage.texture = camTexture;
    }
}

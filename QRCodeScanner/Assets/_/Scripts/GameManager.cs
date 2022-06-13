using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Victor.Hololens.Managers;

public class GameManager : MonoBehaviour
{
    public TextMesh textMesh;

    private QRCodeDataHandler qrCodeHandler;
    private string reciveQRCode;

    void Start()
    {
        QRCodeManager.Instance.OnQRCodeUpdated += OnQRCodeUpdated;
        qrCodeHandler = new QRCodeDataHandler(this);
        qrCodeHandler.OnResultQRCode += OnResultQRCode;
        qrCodeHandler.OnResetQRCode += OnResetQRCode;
    }

    private void OnQRCodeUpdated(object sender, Microsoft.MixedReality.QR.QRCode e)
    {
        reciveQRCode = e.Data;
    }

    private void OnResultQRCode(object sender, string data)
    {
        Debug.Log($"OnResultQRCode:{data}");
    }
    private void OnResetQRCode(object sender, EventArgs e)
    {
        reciveQRCode = "";
    }


    // Update is called once per frame
    void Update()
    {
        qrCodeHandler.HandlerQRCode(reciveQRCode);
    }
}

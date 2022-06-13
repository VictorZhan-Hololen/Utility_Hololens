using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Victor.Hololens.Managers;

public class GameManager : MonoBehaviour
{
    public TextMesh textMesh;

    private string reciveQRCode;

    private IEnumerator resetQRCodeCoroutine;
    private int resetQRCodeSec = 3;
    private string currentQRCode;

    void Start()
    {
        QRCodeManager.Instance.OnQRCodeUpdated += OnQRCodeUpdated;
    }

    private void OnQRCodeUpdated(object sender, Microsoft.MixedReality.QR.QRCode e)
    {
        reciveQRCode = e.Data;
    }

    // Update is called once per frame
    void Update()
    {
        OnReciveQRCode();
    }

    private void OnReciveQRCode()
    {
        if (string.IsNullOrEmpty(reciveQRCode)) return;
        if (currentQRCode == reciveQRCode) return;
        currentQRCode = reciveQRCode;

        textMesh.text = currentQRCode;
        Debug.Log($"OnReciveQRCode:{currentQRCode}");

        if (resetQRCodeCoroutine != null) StopCoroutine(resetQRCodeCoroutine);
        resetQRCodeCoroutine = ResetQRCode();
        StartCoroutine(resetQRCodeCoroutine);
    }

    private IEnumerator ResetQRCode()
    {
        yield return new WaitForSeconds(resetQRCodeSec);
        currentQRCode = "";
        reciveQRCode = "";
    }
}

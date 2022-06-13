using System;
using System.Collections;
using UnityEngine;

public class QRCodeDataHandler
{
    private MonoBehaviour monoBehaviour;
    private IEnumerator resetCoroutine;
    private int resetInterval = 3;
    private string currentQRCode;

    public event EventHandler<string> OnResultQRCode;
    public event EventHandler OnResetQRCode;

    public QRCodeDataHandler(MonoBehaviour monoBehaviour) => this.monoBehaviour = monoBehaviour;

    public void HandlerQRCode(string receiveQRCode)
    {
        if (string.IsNullOrEmpty(receiveQRCode)) return;
        if (currentQRCode == receiveQRCode) return;
        currentQRCode = receiveQRCode;

        OnResultQRCode?.Invoke(this, currentQRCode);

        if (resetCoroutine != null) monoBehaviour.StopCoroutine(resetCoroutine);
        resetCoroutine = ResetQRCode();
        monoBehaviour.StartCoroutine(resetCoroutine);
    }

    private IEnumerator ResetQRCode()
    {
        yield return new WaitForSeconds(resetInterval);
        currentQRCode = "";
        OnResetQRCode?.Invoke(this, null);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CppBackend : MonoBehaviour
{
    //[DllImport("backend", EntryPoint = "SimpleReturnFun")]
    [DllImport("backend", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern int SimpleReturnFun();

    private void Start()
    {
        Debug.Log(SimpleReturnFun());
    }

}

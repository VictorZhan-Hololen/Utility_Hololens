using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="URL_", menuName ="Config/WebSocket Config")]
public class Config_URL : ScriptableObject
{
    [TextArea(5,10)]
    public string WebSocket_URL;
}

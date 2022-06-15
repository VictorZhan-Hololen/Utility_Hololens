using UnityEngine;

[CreateAssetMenu(fileName = "Action_", menuName = "Config/WebSocket Action")]
public class Config_Action : ScriptableObject
{
    public string action;
    public string data, dataType;
    public string fileName;
    public string nick;
    public string receiveUser;
    public string room;
    public string sendUserId;
}

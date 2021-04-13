using System;
using UnityEngine;
using TMPro;

public class OnlineClientManager : MonoBehaviour
{
    
    
    public  QualityQuestWebSocket qualityQuestWebSocket;
    private OnlineClientManager onlineClientManager;

    public TMP_InputField port;
    public TMP_InputField ip;
    public TMP_InputField password;


    void Awake()
    {
        if (onlineClientManager == null)
        {
            onlineClientManager = this;
            DontDestroyOnLoad(gameObject);
        } else if (onlineClientManager != null)
        {
            Destroy(gameObject);
        }
    }

    public void Connect()
    {
        //qualityQuestWebSocket.StartConnection(ip.text, Convert.ToInt32(port.text));
        qualityQuestWebSocket.StartConnection("127.0.0.1", 8181);
    }


    public void SendTestMessage()
    {
        MessageContainer.Messages.RequestOpenSessionMessage requestOpenSessionMessage = new MessageContainer.Messages.RequestOpenSessionMessage(new Guid(), "!Password123#");

        qualityQuestWebSocket.SendMessage(requestOpenSessionMessage);
    }

}

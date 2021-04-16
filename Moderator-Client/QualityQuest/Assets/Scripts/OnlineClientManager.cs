using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEditor;
using MessageContainer.Messages;

public class OnlineClientManager : MonoBehaviour
{
    public  QualityQuestWebSocket qualityQuestWebSocket;
    public ActiveScreenManager activeScreen;
    public GameStory story;
    
    private Guid moderatorClientGuid;
    

    void Awake()
    {
        moderatorClientGuid = Guid.NewGuid();
    }

    public void Connect()
    {
        //qualityQuestWebSocket.StartConnection(ip.text, Convert.ToInt32(port.text));
        qualityQuestWebSocket.StartConnection("127.0.0.1", 8181);
    }

    public void SendRequestOpenSessionMessage()
    {
        //MessageContainer.Messages.RequestOpenSessionMessage requestOpenSessionMessage = new MessageContainer.Messages.RequestOpenSessionMessage(new Guid(), password.text);
        // for testing with a default password
        var requestOpenSessionMessage = new RequestOpenSessionMessage(moderatorClientGuid, "!Password123#");

        qualityQuestWebSocket.SendMessage(requestOpenSessionMessage);
    }

    

    public void SendRequestGameStartMessage()
    {
        var requestGameStartMessage = new RequestGameStartMessage(moderatorClientGuid);
        qualityQuestWebSocket.SendMessage(requestGameStartMessage);
    }

    

    public void ReceivedSessionOpenedMessage(SessionOpenedMessage sessionOpenedMessage)
    {
        activeScreen.ShowQrCodePanel(sessionOpenedMessage.DirectURL.ToString(), sessionOpenedMessage.SessionKey);
    }

    public void ReceivedAudienceStatusMessage(AudienceStatusMessage audienceStatusMessage)
    {
        activeScreen.UpdateAudienceCount(audienceStatusMessage.AudienceCount);
    }

    public void ReceivedGameStartedMessage(GameStartedMessage gameStartedMessage)
    {
        activeScreen.ShowCharacterSelection();

        var requestStartVotingMessage = new RequestStartVotingMessage(moderatorClientGuid, 30, new KeyValuePair<Guid, string>(), new KeyValuePair<Guid, string>[story.playThrough.Root.Children.Count]);
        requestStartVotingMessage.VotingPrompt = new KeyValuePair<Guid, string>(story.playThrough.Root.EventId, story.playThrough.Root.Description);

        var options = story.playThrough.Root.Children.ToArray();
        for (var i = 0; i < options.Length; i++)
        {
            requestStartVotingMessage.VotingOptions[i] = new KeyValuePair<Guid, string>(options[i].EventId, options[i].Description);
        }
        qualityQuestWebSocket.SendMessage(requestStartVotingMessage);
    }

    public void Test(VotingEndedMessage votingEndedMessage)
    {
        Debug.Log("haha");
    }


    public void ReceivedVotingStartedMessage(VotingStartedMessage votingStartedMessage)
    {
        // TODO: Set Timer
    }
}

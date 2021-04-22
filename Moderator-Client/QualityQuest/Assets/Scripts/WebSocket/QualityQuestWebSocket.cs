using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;
using System;
using MessageContainer;
using MessageContainer.Messages;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class to realize the connection to ServerLogic. 
/// For the WebSockets the solution of WebSocket-sharp is used. 
/// For more information: https://github.com/PingmanTools/websocket-sharp/
/// </summary>
public class QualityQuestWebSocket : MonoBehaviour
{

    public OnlineClientManager onlineClientManager;
    public MainThreadWorker mainThreadWorker;
    public WebSocket webSocket;
    
    public void StartConnection(string ip, string port)
    {
        // Connect ws://127.0.0.1:8181
        
        webSocket = new WebSocket("ws://" + ip +":" + port.ToString());

        /*
        // Logic to connect with a secure websocket

        webSocket = new WebSocket("wss://" + ip +":" + port.ToString());
        // Check the certificate
        webSocket.SslConfiguration.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
        {
            // If desired you can change the certificate validation

            return true;
        }; */

        // Event when the WebSocket connection is established.
        webSocket.OnOpen += (sender, e) =>
        {
            GameState.gameIsOnline = true;
            onlineClientManager.StartOnlineMode();
            onlineClientManager.ConnectionEstablished();
            Debug.Log("Connection established.");
        };

        webSocket.EmitOnPing = true;
        // Event when the WebSocket recieves a message.
        webSocket.OnMessage += (sender, e) =>
        {
            // Check if the data is a string.
            if (e.IsText)
            {
                Debug.Log("String data");
                mainThreadWorker.AddAction(() =>
                {
                    Read(e.Data);
                });
            }
            // Check if the data is binary.
            if (e.IsBinary)
            {
                mainThreadWorker.AddAction(() =>
                {
                    Read(e.RawData);
                });
                Debug.Log("Binary Data");
            }
            if (e.IsPing)
            {
                Debug.Log("Ping.");
                return;
            }
        };

        //Event when the WebSockets gets an Error.
        webSocket.OnError += (sender, e) =>
        {
            Debug.Log("Error: " + e.Message);
        };

        webSocket.OnClose += (sender, e) =>
        {
            GameState.gameIsOnline = false;
            mainThreadWorker.AddAction(() =>
            {
                onlineClientManager.ServerIssues(e.Code);
            });
            Debug.Log("Connection is closed. Reason: " + e.Reason + ", ErrorCode: " + e.Code);
        };

        //Connect the WebSocket
        webSocket.ConnectAsync();
    }

    /// <summary>
    /// Method to read/parse the incoming string message.
    /// </summary>
    /// <param name="msg">The incoming data</param>
    void Read(string msg)
    {
        Debug.Log("RECEIVING => " + msg);
        try
        {
            MessageContainer.MessageContainer container =
                    JsonConvert.DeserializeObject<MessageContainer.MessageContainer>(msg);
                
                switch (container.Type)
                {
                    case MessageType.SessionOpened:
                        onlineClientManager.ReceivedSessionOpenedMessage(JsonConvert.DeserializeObject<SessionOpenedMessage>(msg));
                        break;
                    case MessageType.AudienceStatus:
                        onlineClientManager.ReceivedAudienceStatusMessage(JsonConvert.DeserializeObject<AudienceStatusMessage>(msg));
                        break;
                    case MessageType.GameStarted:
                        onlineClientManager.ReceivedGameStartedMessage(JsonConvert.DeserializeObject<GameStartedMessage>(msg));
                        break;
                    case MessageType.VotingStarted:
                        onlineClientManager.ReceivedVotingStartedMessage(JsonConvert.DeserializeObject<VotingStartedMessage>(msg));
                        break;
                    case MessageType.VotingEnded:
                        onlineClientManager.ReceivedVotingEndedMessage(JsonConvert.DeserializeObject<VotingEndedMessage>(msg));
                        break;
                    case MessageType.Error:
                        onlineClientManager.ReceivedErrorMessage(JsonConvert.DeserializeObject<ErrorMessage>(msg));
                        break;
                    case MessageType.GamePausedStatus:
                        onlineClientManager.ReceivedGamePausedStatusChange(JsonConvert.DeserializeObject<GamePausedStatusMessage>(msg));
                        break;
                    case MessageType.SessionClosed:
                        break;
                    case MessageType.ReconnectSuccessful:
                        onlineClientManager.ReceivedReconnectSuccessfulMessage(JsonConvert.DeserializeObject<ReconnectSuccessfulMessage>(msg));
                        break;
                    default:
                        Debug.Log(container.Type + " is not a valid messageType."); 
                        break;
                }
        } catch (JsonSerializationException jse)
        {
            Debug.Log("Can't read message, JSON parse error: " + jse);
        } catch (NullReferenceException nullReferenceException)
        {
            Debug.Log("RECIEVED messages is null:" + nullReferenceException);
        }
    }

    /// <summary>
    /// Method the read/parse the incoming binary data.
    /// </summary>
    /// <param name="msg"></param>
    void Read(byte[] msg)
    {
      
    }

    /// <summary>
    /// Method to send a message to the server.
    /// </summary>
    /// <typeparam name="T">The type of the message</typeparam>
    /// <param name="msg">The message</param>
    public void SendMessage<T>(T message)
    {
        try
        {
            string jsonMessage = JsonConvert.SerializeObject(message);
            Debug.Log("SENDING => " + jsonMessage);
            webSocket.Send(jsonMessage);
        } catch (JsonSerializationException jse)
        {
            Debug.Log("Can't send Message, JSON parse error: " + jse);
        }
    }

    public void CloseConnection()
    {
        webSocket.CloseAsync();
    }

    public void CloseConnectionWithReason(CloseStatusCode closeStatusCode, string reason)
    {
        webSocket.CloseAsync(closeStatusCode, reason);
    }
}

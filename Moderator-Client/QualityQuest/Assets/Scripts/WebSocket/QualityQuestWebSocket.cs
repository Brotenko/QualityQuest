using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;
using System;
using System.Threading;
using MessageContainer;
using MessageContainer.Messages;

/// <summary>
/// Class to realize the connection to ServerLogic. 
/// For the WebSockets the solution of WebSocket-sharp is used. 
/// For more information: https://github.com/PingmanTools/websocket-sharp/
/// </summary>
public class QualityQuestWebSocket : MonoBehaviour
{
    [SerializeField]
    private Client client;
    [SerializeField]
    private MainThreadWorker mainThreadWorker;
    public WebSocket webSocket;

    /// <summary>
    /// Method to start the WebSocket connection.
    /// </summary>
    /// <param name="ip">Ip of the webSocket.</param>
    /// <param name="port">Port of the webSocket.</param>
    public void StartConnection(string ip, string port)
    {
        // Logic to connect with a secure websocket
        webSocket = new WebSocket("wss://" + ip + ":" + port.ToString());
        webSocket.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;

        // Check the certificate
        webSocket.SslConfiguration.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
        {
            // If desired: change the certificate validation
            return true;
        }; 

        // Event when the WebSocket connection is established.
        webSocket.OnOpen += (sender, e) =>
        {
            Debug.Log("Connection established.");
            GameState.gameIsOnline = true;
            client.ConnectionEstablished();
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
                    //Ignores all messages if the game is in offline mode.
                    if (GameState.gameIsOnline)
                    {
                        Read(e.Data);
                    }
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
            }
        };

        //Event when the WebSockets gets an Error.
        webSocket.OnError += (sender, e) =>
        {
            Debug.Log("Error: " + e.Message);
        };

        webSocket.OnClose += (sender, e) =>
        {
            mainThreadWorker.AddAction(() =>
            {
                client.ServerIssues(e.Code);
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
    private void Read(string msg)
    {
        Debug.Log("RECEIVING => " + msg);
        try
        {
            var container = JsonConvert.DeserializeObject<MessageContainer.MessageContainer>(msg);

            switch (container.Type)
            {
                case MessageType.SessionOpened:
                    client.ReceivedSessionOpenedMessage(JsonConvert.DeserializeObject<SessionOpenedMessage>(msg));
                    break;
                case MessageType.AudienceStatus:
                    client.ReceivedAudienceStatusMessage(JsonConvert.DeserializeObject<AudienceStatusMessage>(msg)); 
                    break;
                case MessageType.GameStarted:
                    client.ReceivedGameStartedMessage(JsonConvert.DeserializeObject<GameStartedMessage>(msg));
                    break;
                case MessageType.VotingStarted:
                    client.ReceivedVotingStartedMessage(JsonConvert.DeserializeObject<VotingStartedMessage>(msg));
                    break;
                case MessageType.VotingEnded:
                    client.ReceivedVotingEndedMessage(JsonConvert.DeserializeObject<VotingEndedMessage>(msg));
                    break;
                case MessageType.Error:
                    client.ReceivedErrorMessage(JsonConvert.DeserializeObject<ErrorMessage>(msg));
                    break;
                case MessageType.GamePausedStatus:
                    client.ReceivedGamePausedStatusChange(JsonConvert.DeserializeObject<GamePausedStatusMessage>(msg));
                    break;
                case MessageType.SessionClosed:
                    break;
                case MessageType.ReconnectSuccessful:
                    client.ReceivedReconnectSuccessfulMessage(JsonConvert.DeserializeObject<ReconnectSuccessfulMessage>(msg));
                    break;
                default:
                    Debug.Log(container.Type + " is not a valid messageType.");
                    break;
            }
        }
        catch (JsonSerializationException jse)
        {
            Debug.Log("Can't read message, JSON parse error: " + jse);
        }
        catch (NullReferenceException nullReferenceException)
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
        }
        catch (JsonSerializationException jse)
        {
            Debug.Log("Can't send Message, JSON parse error: " + jse);
        }
    }

    /// <summary>
    /// Method to close the connection.
    /// </summary>
    public void CloseConnection()
    {
        webSocket.CloseAsync();
    }

    /// <summary>
    /// Method to close the connection with a closeStatusCode and a closing reason.
    /// </summary>
    /// <param name="closeStatusCode">The closeStatusCode.</param>
    /// <param name="reason">The close reason.</param>
    public void CloseConnectionWithReason(CloseStatusCode closeStatusCode, string reason)
    {
        webSocket.CloseAsync(closeStatusCode, reason);
    }
}

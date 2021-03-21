using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;
using System;

/// <summary>
/// Class to realize the connection to ServerLogic. 
/// For the WebSockets the solution of WebSocket-sharp is used. 
/// For more information: https://github.com/PingmanTools/websocket-sharp/
/// </summary>
public class Client : MonoBehaviour
{

    WebSocket webSocket;

    public void StartConnection()
    {
        // Connect 
        webSocket = new WebSocket("ws://127.0.0.1:8181");

        /*
        // Check the certificate
        webSocket.SslConfiguration.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
        {
            // If desired you can change the certificate validation

            return true;
        }; */

        // Event when the WebSocket connection is established.
        webSocket.OnOpen += (sender, e) =>
        {
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
                Read(e.Data);
            }
            // Check if the data is binary.
            if (e.IsBinary)
            {
                Read(e.RawData);
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
            //Debug.Log("Log1");
            MessageContainer.MessageContainer container = JsonConvert.DeserializeObject<MessageContainer.MessageContainer>(msg);
            //Debug.Log("Log2");
            switch (container.Type)
            {
                case MessageContainer.MessageType.SessionOpened:
                    break;
                case MessageContainer.MessageType.AudienceStatus:
                    break;
                case MessageContainer.MessageType.ServerStatus:
                    break;
                case MessageContainer.MessageType.ReconnectSuccessful:
                    break;
                case MessageContainer.MessageType.GameStarted:
                    //Debug.Log("Log3");
                    //Debug.Log("GameStartedMessage");
                    break;
                case MessageContainer.MessageType.VotingStarted:
                    break;
                case MessageContainer.MessageType.VotingEnded:
                    break;
                case MessageContainer.MessageType.Error:
                    break;
                case MessageContainer.MessageType.GamePausedStatus:
                    break;
                case MessageContainer.MessageType.SessionClosed:
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
    void SendMessage<T>(T message)
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

    /// <summary>
    /// TestMessage
    /// </summary>
    public void SendTestMessage()
    {

        MessageContainer.Messages.RequestGameStartMessage test = new MessageContainer.Messages.RequestGameStartMessage(new Guid());

        SendMessage(test);   
    }
}

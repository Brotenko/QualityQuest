using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        // Connect ws://127.0.0.1:8181
        
        webSocket = new WebSocket("ws://" + ip.text + ":" + port.text.ToString());

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

                // Sets the Message. Only for test purpose.
                message = e.Data;
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

    /******************************* TEST MESSAGES / TEST METHODS FOR TEST PURPOSE *******************************/
    // Only for Testing purposes, will be deleted in the near future.

    // For testing
    public Text recievedMessage;
    public InputField ip;
    public InputField port;
    public string message;
    Guid testGuid = new Guid();

    void Update()
    {
        recievedMessage.text = message;
    }

    public void RequestOpenSessionMessage()
    {
        MessageContainer.Messages.RequestOpenSessionMessage test = new MessageContainer.Messages.RequestOpenSessionMessage(testGuid, "Passw0rd");

        SendMessage(test);
    }

    public void RequestServerStatusMessage()
    {
        MessageContainer.Messages.RequestServerStatusMessage test = new MessageContainer.Messages.RequestServerStatusMessage(testGuid);

        SendMessage(test);
    }

    public void ReconnectMessage()
    {
        MessageContainer.Messages.ReconnectMessage test = new MessageContainer.Messages.ReconnectMessage(testGuid);

        SendMessage(test);
    }

    public void RequestStartVotingMessage()
    {
        MessageContainer.Messages.RequestStartVotingMessage test = new MessageContainer.Messages.RequestStartVotingMessage(testGuid, 30, new KeyValuePair<Guid, string>(), new KeyValuePair<Guid, string>[3]);
        test.VotingPrompt = new KeyValuePair<Guid, string>(Guid.NewGuid(), "Was machst du?");
        test.VotingOptions[0] = new KeyValuePair<Guid, string>(Guid.NewGuid(), "Du hast keine Lust auf zusätzliche Arbeit und legst deshalb einfach während dem Telefonat auf.");
        test.VotingOptions[1] = new KeyValuePair<Guid, string>(Guid.NewGuid(), "Nach dem Telefonat setzt du die gewünschten Änderungen um.");
        test.VotingOptions[2] = new KeyValuePair<Guid, string>(Guid.NewGuid(), "Nach dem Telefonat fällt dir auf, dass die Änderungswünsche technisch nicht umsetzbar sind.");

        SendMessage(test);
    } 


    public void RequestGamePausedStatusChangeTrueMessage()
    {
        MessageContainer.Messages.RequestGamePausedStatusChangeMessage test = new MessageContainer.Messages.RequestGamePausedStatusChangeMessage(testGuid, true);

        SendMessage(test);
    }

    
    public void RequestGamePausedStatusChangeFalseMessage()
    {
        MessageContainer.Messages.RequestGamePausedStatusChangeMessage test = new MessageContainer.Messages.RequestGamePausedStatusChangeMessage(testGuid, false);

        SendMessage(test);
    }

    public void RequestCloseSessionMessage()
    {
        MessageContainer.Messages.RequestCloseSessionMessage test = new MessageContainer.Messages.RequestCloseSessionMessage(testGuid, "A12A2A");

        SendMessage(test);
    }


    public void RequestGameStartMessage()
    {

        MessageContainer.Messages.RequestGameStartMessage test = new MessageContainer.Messages.RequestGameStartMessage(testGuid);

        SendMessage(test);   
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
    
    public void CloseConnection()
    {
        webSocket.Close();
    }

    public void SetIp()
    {
        Debug.Log("Ip is: " + ip.text);
    }

    public void SetPort()
    {
        Debug.Log("Port is: " + port.text);
    }
}

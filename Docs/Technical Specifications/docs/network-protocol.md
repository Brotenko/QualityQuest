<!---
- Fix FR 25:
  - If the server does not react within 5 seconds after recieving the Moderator-Client's message, the connection from the Moderator-Client to the server shall be interrupted. In this case the Moderator can either continue playing in Offline-Mode or try to re-establish the connection to the server.
- Add "Offline-Mode" to Glossary.
- Add "Online-Mode" to Glossary.
- Scan document and add new Requirements and Glossary entries.
- Create Sequence diagrams.
- Add changelog entry for "Added network protocol" (and everything else).
-->

# Network protocol

## General information

The "WebSocket" class provided by C# is used for communication between the Moderator-Client and the server. When starting a session, the moderator is asked for a URL and password. The URL leads to the game server and is then used to establish a WebSocket connection with the server. Once the connection is established, the Moderator-Client sends a [RequestOpenSession](#requestopensession) message to the server, which contains the Moderator-Client's Guid and the entered password. If this password is incorrect or if the Moderator-Client takes too long to send the [RequestOpenSession](#requestopensession) message, the WebSocket connection is automatically terminated. If not, a session is opened, and the PlayerAudience can join. The messages sent back and forth between Moderator-Client and server are all in JSON format. </br>
The security and persistence of communication is guaranteed by the use of WebSockets in combination with the HTTPS protocol. In addition, for communication integral data is stored on the server in hashed form. This includes the server password and the moderator's Guid.

## Server logs

The server log stores the Moderator-Client's Guid and the sessionKey. If the server should at any time lose the connection to the internet or have to close the current session, this will provide the following advantages:

- The logged Moderator-Client's Guid can still be used to send a [Reconnect](#reconnect) message to the server without requiring the password to be re-entered. This allows the session to resume easily without having to change sessionKey or re-entering the password.
- The logged sessionKey allows the PlayerAudience-Clients to quickly and easily reconnect to the session without having to enter a new sessionKey.

## Differences between starting in Online-Mode and Offline-Mode

When starting the application, the game offers the possibility to start the game either in form of an online session with a server, or in Offline-Mode. Attention must be paid to a few important points here:

- If you start the game as an online session, you can switch between online and offline at any time.
- If you start the game in Offline-Mode, the game remains in Offline-Mode until the moderator starts an online session via the main menu. This decision was made because one wants to avoid that the moderator has to enter the URL to the server and the password during the game.
- For network protocol purposes, a flag is set to distinguish whether the game was initialised online or offline, so that sending and receiving messages is disregarded right from the start of the game.

## Behaviour in the event of connection loss

A clear distinction must be made between three different cases. First of all, we refer here to the view of a single, specific PlayerAudience-Client, which can lose the connection to the server, but this behaviour is not exclusive to single PlayerAudience-Client and can also happen to multiple PlayerAudience-Clients, but due to the same reasons listed below. Secondly, the server can also lose the connection to all clients - in other words, a complete loss of connection to the server. Finally, it is possible for only the Moderator-Client to lose its connection to the server, while all other clients can maintain their connection to the server. </br>
Since this can have very different effects, the following section specifies how, these cases may occur and which actors have to do what in order to deal correctly with the loss of connection.

### Moderator-Client connection loss

A selection of possible causes for the loss of connection from the Moderator-Client to the server are the following:

1. The moderator closes the client, and thus the connection to the server.
2. The moderator's end device loses the connection to the network, and thus to the server.
3. The server does not react within 5 seconds after recieving the Moderator-Client's message.

If the Moderator-Client should at any time lose the connection to the server, it automatically switches to Offline-Mode and notifies the moderator. The moderator can then continue to play the game in Offline-Mode. In the meantime, the Moderator-Client continuously sends a [RequestServerStatus](#requestserverstatus) message to the server to determine whether the server is back online. When the Moderator-Client receives a [ServerStatus](#serverstatus) message, it informs the moderator that the session can be resumed. In this case, the moderator can either go back online via an UI element or continue playing in Offline-Mode. This can result in the following three scenarios:

- The server is reachable again and the connection can be re-established. Furthermore, the session on the server was not closed and the PlayerAudience-Clients are still connected to the server. In that case the Moderator-Client only has to send a [Reconnect](#reconnect) message to return to normal gameplay, since the session is still going.
- The server is reachable again and the connection can be re-established, but the session on the server has been closed and the PlayerAudience-Clients are no longer connected to the server. In that case the Moderator-Client only has to send a [Reconnect](#reconnect) message, since the logs of the server still hold the Guid of the Moderator-Client. This way the session can be restored without entering the password again and the PlayerAudience-Clients can simply reconnect to the server, through the same QR-code, URL and sessionKey, to be able to participate in the game again. 
- The Moderator-Client still cannot reach the server and the game continues in Offline-Mode.

If the moderator ever returns to the main menu, the session must be started anew by connecting to the server via password again.

### PlayerAudience-Client connection loss

A selection of possible causes for the loss of connection from the PlayerAudience-Client to the server are the following:

1. The audience member closes the website, and thus the connection to the server.
2. The audience member's end device loses the connection to the network, and thus to the server.
3. The server goes offline or loses its connection to the Internet.

Since no data of the audience member has to be saved and it has absolutely no relevance for the course of the game whether the spectator has been part of the session before or not, he can connect to the server again at any time and does not have to suffer any disadvantages compared to the other PlayerAudience-Clients. Even audience members who were not connected to the server at all before the start of the session can join in this way without any problems.

### Server connection loss

A selection of possible causes for the loss of connection from the server to all clients are the following:

1. The network carrying the server breaks down.
2. The physical server on which the logic resides is not reachable (either because of internet problems or because the server crashed).
3. The logic of the game server has crashed.

Although all these issues boil down to the same result - the inaccessibility of the server - in the first case, one can hope that the network the server is located on will recover in the course of the game. </br>
Though, in the second and third case, nothing can be done for the time being and the game must be played offline by the moderator. However, it should then be checked whether and how the logic of the game server was related to the problem and how this problem can be avoided or fixed in the future.

## What happens if a Moderator-Client wants to connect to the server when a Moderator-Client is already connected to the server?

If the new Moderator-Client has entered the required password correctly, the old Moderator-Client is disconnected from the server and a new session is started. This has the following reasons:

1. Since the password is only known to the server owners, the moderator cannot be thrown out by a random person. 
2. The moderator can start a new session at any time and does not have to worry about whether there is still a session going on somewhere.
3. In the event that the server mistakenly thinks that a session is already in progress, the server will not be blocked and can continue to be used without problems. 

## What happens in case of an illegal message being recieved?

Moderator-Client and server should never send [illegal messages](#errortypeenum), as this is a sign of a damaged architecture or insufficient network protocol. Should this marginal case occur nevertheless, the connection between Moderator-Client and server should be cut and the [illegal message](#errortypeenum) ignored. The game is then continued in Offline-Mode, unless the moderator chooses to go back online and reconenct to the server.

## What happens in case of a pause?

Breaks are always initiated by the Moderator-Client and cannot be initiated by the server under any circumstances. If the Moderator-Client initiates a pause, the following events will occur:

- The game on the Moderator-Client is paused completely and a pause screen appears. 
- Despite the pause, the server continues to communicate with the Moderator-Client and the PlayerAudience-Clients.
- If a vote is in progress, the server will pause the voting timer.

## What about the communication between PlayerAudience-Clients and server?

Communication between the server and the PlayerAudience-Clients is not specified as part of the network protocol because the back-end of the PlayerAudience-Clients and the general server logic are on the same physical server and can thus communicate directly with each other. This communication happens locally, through different functions, and does not need pre-defined message types, like the communication between Moderator-Client and server. Since the front-end of the PlayerAudience-Clients is a web page, nothing more specific needs to be defined here either, as preimplemented solutions of HTML and JavaScript can be used to implement the communication between PlayerAudience-Clients and server. </br>
Apart from that, the Observer-pattern is used to make the communication simple and efficient. Here the PlayerAudience-Clients subscribe to the server to observe it.

## MessageContainer

Defines the container format for a message. All following fields can be found in every network message, whereas a debugMessage is purely optional.

``` csharp
class MessageContainer {
    Guid moderatorId; 
    MessageTypeEnum type;
    Date creationDate;
    string debugMessage;
}
```

- **moderatorId:** The individual identifier assigned to the Moderator-Client. Only the Moderator-Client sends this id to the server to identify itself. The server leaves this field empty.
- **type:** Specifies the type of the message to be able to parse it accordingly.
- **creationDate:** The timestamp of the message.
- **debugMessage:** Can be used during development to transport additional data between server and Moderator-Client. This way, in case of a non parseable message, or an error occuring, information can be carried to the Moderator-Client directly for quick access, without the need to search through the logs.

## MessageTypeEnum

Lists all message types. The structuring by comments is only for overview and has no semantic meaning whatsoever. All messages are identified by the [MessageContainer](#messagecontainer).

``` csharp
enum MessageTypeEnum
{
    // Initialisation
    RequestOpenSession,
    SessionOpened,
    RequestServerStatus,
    ServerStatus,
    Reconnect,
    RequestGameStart,
    GameStarted,
    // Voting
    RequestStartVoting,
    VotingStarted,
    VotingEnded,
    // Control messages
    Error,
    RequestPauseGameStatusChange,
    GamePauseStatus,
    // Postgame
    RequestCloseSession,
    SessionClosed
}
```

## Who can send which MessageType?

Listing which participant may send which message, the order of the listing is based on the [MessageTypeEnum](#messagetypeenum).

<table style="width:100%">
    <tr>
        <th>Who can send which MessageType?</th>
    </tr>
    <tr>
        <td>
            <table style="width:100%">
                <tr>
                    <th style="width:60%">Messages</th>
                    <th style="width:28%">Moderator-Client</th>
                    <th style="width:12%">Server</th>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <th>Initialisation</th>
    </tr>
    <tr>
        <td>
            <table style="width:100%">
                <tr>
                    <th style="width:60%; font-weight: normal"><a href="#requestopensession">RequestOpenSession</a></th>
                    <th style="width:28%">✓</th>
                    <th style="width:12%"></th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#sessionopened">SessionOpened</a></th>
                    <th></th>
                    <th>✓</th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#requestserverstatus">RequestServerStatus</a></th>
                    <th>✓</th>
                    <th></th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#serverstatus">ServerStatus</a></th>
                    <th></th>
                    <th>✓</th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#reconnect">Reconnect</a></th>
                    <th>✓</th>
                    <th></th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#requestgamestart">RequestGameStart</a></th>
                    <th>✓</th>
                    <th></th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#gamestarted">GameStarted</a></th>
                    <th></th>
                    <th>✓</th>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <th>Voting</th>
    </tr>
    <tr>
        <td>
            <table style="width:100%">
                <tr>
                    <th style="width:60%; font-weight: normal"><a href="#requeststartvoting">RequestStartVoting</a></th>
                    <th style="width:28%">✓</th>
                    <th style="width:12%"></th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#votingstarted">VotingStarted</a></th>
                    <th></th>
                    <th>✓</th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#votingended">VotingEnded</a></th>
                    <th></th>
                    <th>✓</th>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <th>Control messages</th>
    </tr>
    <tr>
        <td>
            <table style="width:100%">
                <tr>
                    <th style="width:60%; font-weight: normal"><a href="#error">Error</a></th>
                    <th style="width:28%"></th>
                    <th style="width:12%">✓</th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#requestpausegamestatuschange">RequestPauseGameStatusChange</a></th>
                    <th>✓</th>
                    <th></th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#gamepausestatus">GamePauseStatus</a></th>
                    <th></th>
                    <th>✓</th>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <th>Postgame</th>
    </tr>
    <tr>
        <td>
            <table style="width:100%">
                <tr>
                    <th style="width:60%; font-weight: normal"><a href="#requestclosesession">RequestCloseSession</a></th>
                    <th style="width:28%">✓</th>
                    <th style="width:12%"></th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#sessionclosed">SessionClosed</a></th>
                    <th></th>
                    <th>✓</th>
                </tr>
            </table>
        </td>
    </tr>
</table>

</br>

## ErrorTypeEnum

All possible causes for an [Error](#error) message, which can occur in the context of communication between server and Moderator-Client. These apply both when establishing the connection and during the general course of the game.

``` csharp
enum ErrorTypeEnum
{
    WrongPassword,
    UnknownGuid,
    IllegalPauseAction,
    SessionDoesNotExist,
    IllegalMessage
}
```

- **WrongPassword:** Is triggered when a [RequestOpenSession](#requestopensession) message contains the wrong password.
- **UnknownGuid:** Is triggered when a message with an unknown moderatorId is sent to the server.
- **IllegalPauseAction:** Is triggered if one of the following cases applies:
    - A request to pause the game reaches the server even though the game is already paused.
    - A request to continue the game reaches the server even though the game has not been paused previously.
- **SessionDoesNotExist:** Is triggered when an attempt is made to interact with a session that does not exist.
- **IllegalMessage:** Triggers when an unknown message type is received, or when a message arrives at the server out of order. More precise details are to be specified in the errorMessage.

## Detailed message definitions

### Initialisation

#### RequestOpenSession

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::RequestOpenSession**. </br>
This message is sent from the Moderator-Client to the server when the moderator wants to connect to the server. The password confirms that the moderator is allowed to use the server and the Guid of the moderator will be saved in the logs henceforth, for further communication. In addition, the creation of a session is also requested from the server at the same time.

``` csharp
class RequestOpenSession : MessageContainer 
{
    string password;
}
```

The server responds with a **[SessionOpened](#sessionopened)** message.

#### SessionOpened

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::SessionOpened**. </br>
This message is sent from the server to the Moderator-Client in response to a **[RequestOpenSession](#requestopensession)** message to provide the Moderator-Client with all necessary data to allow the audience to join the session.

``` csharp
class SessionOpened : MessageContainer 
{
    string sessionKey;
    string directURL;
    Bitmap qrCode;
}
```

- **sessionKey:** A randomly generated session key required by the audience to join the session, after connecting to the server.
- **directURL:** A direct URL that the audience can use to connect to the server via the PlayerAudience-Client should the QR code not be usable.
- **qrCode:** A QR-code automatically generated by the server which can be scanned by the audience to connect to the server. 

#### RequestServerStatus

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::RequestServerStatus**. </br>
This message is sent from the Moderator-Client to the server if there is currently no connection to a server. This message is sent to the server at regular intervals until the server returns a response in form of a [ServerStatus](#serverstatus). If [ServerStatus](#serverstatus) is received by the Moderator-Client at any given time, the moderator is notified that a connection to the server is possible, and at the same time, RequestServerStatus messages are stopped being sent to the server.

``` csharp
class RequestServerStatus : MessageContainer 
{
    // No extra fields needed
}
```

The server responds with a **[ServerStatus](#serverstatus)** message.

#### ServerStatus

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::ServerStatus**. </br>
This message is sent from the server to the Moderator-Client in response to a **[RequestServerStatus](#requestserverstatus)** message to confirm that the server is available for a connection.

``` csharp
class ServerStatus : MessageContainer 
{
    // No extra fields needed
}
```

#### Reconnect

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::Reconnect**. </br>
This message is sent from the Moderator-Client to the server to reestablish a lost connection. For this purpose, the Moderator-Client's Guid is required for comparison with the previously saved Moderator-Client Guid. This message shall only be sent when the Moderator-Client is still in-game, otherwise a new session has to be opened through a [RequestOpenSession](#requestopensession) message.

``` csharp
class Reconnect : MessageContainer 
{
    // No extra fields needed
}
```

#### RequestGameStart

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::RequestGameStart**. </br>
This message is sent from the Moderator-Client to the server to request the start of the game with the current session. 

``` csharp
class RequestGameStart : MessageContainer 
{
    // No extra fields needed
}
```

The server responds with a [GameStarted](#gamestarted) message.

#### GameStarted

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::GameStarted**. </br>
This message is sent from the server to the Moderator-Client in response to a **[RequestGameStart](#requestgamestart)** message to inform the Moderator-Client that the game has started. This results in the Moderator-Client starting the game locally and the server awaiting further communication.

``` csharp
class GameStarted : MessageContainer 
{
    // No extra fields needed
}
```

### Voting

#### RequestStartVoting

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::RequestStartVoting**. </br>
This message is sent from the Moderator-Client to the server to request the start of a voting phase. For this purpose the Moderator-Client provides the server with different options for the audience to choose from. It also provides the server with a timelimit on how long the PlayerAudience-Clients may vote on the topic.

``` csharp
class RequestStartVoting : MessageContainer 
{
    int votingTime;
    string[] votingOptions;
}
```

- **votingTime:** The time in seconds that PlayerAudience-Clients have to cast their vote.
- **votingOptions:** Contains the textual description of the voting options.

The server responds with a [VotingStarted](#votingstarted) message and some time after with a [VotingEnded](#votingended) message.

#### VotingStarted

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::VotingStarted**. </br>
This message is sent from the server to the Moderator-Client in response to a **[RequestStartVoting](#requeststartvoting)** message to confirm the start of a voting phase with the provided voting options.

``` csharp
class VotingStarted : MessageContainer 
{
    // No extra fields needed
}
```

#### VotingEnded

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::VotingEnded**. </br>
This message is sent from the server to the Moderator-Client in response to a **[RequestStartVoting](#requeststartvoting)** message, after the voting time has expired. The winning option and the statistical results of the vote are sent back to the Moderator-Client. 

``` csharp
class VotingEnded : MessageContainer 
{
    int winningOption;
    Dictionary<int, int> votingResults;
}
```

- **winningOption:** The id of the option that got the most votes from the PlayerAudience.
- **votingResults:** Contains the id of the option, which is generated by hashing the description, as the key and the respective amount of recieved votes as the value.

### Control messages

#### Error

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::Error**. </br>
This message is sent from the server to the Moderator-Client in case of a disconnection initiated by the server and explains the reason for the disconnection.

``` csharp
class Error : MessageContainer 
{
    ErrorTypeEnum errorType;
    string errorMessage;
}
```

- **errorType:** Specifies the reason for the occurred error.
- **errorMessage:** Optional, more detailed description of the occurred error.

#### RequestPauseGameStatusChange

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::RequestPauseGameStatusChange**. </br>
This message is sent from the Moderator-Client to the server to switch the game between running and being paused.

``` csharp
class RequestPauseGameStatusChange : MessageContainer 
{
    bool gamePausedStatus;
}
```

- **gamePausedStatus:** Specifies whether the game is to be paused or whether the already paused game is to be continued. With _true_ indicating that the game is to be paused, and _false_ indicating that the game is to be continued.

The server responds with a **[GamePauseStatus](#gamepausestatus)** message.

#### GamePauseStatus

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::GamePauseStatus**. </br>
This message is sent from the server to the Moderator-Client in response to a **[RequestPauseGameStatusChange](#requestpausegamestatuschange)** message, to confirm that the game is now either continuing or being paused.

``` csharp
class GamePauseStatus : MessageContainer 
{
    bool gamePausedStatus;
}
```

- **gamePausedStatus:** Specifies whether the game is being paused or whether the already paused game is being continued. With _true_ indicating that the game has been paused, and _false_ indicating that the game is continuing.

### Postgame

#### RequestCloseSession

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::RequestCloseSession**. </br>
This message is sent from the Moderator-Client to the server to tell the server to close the session and with that the connection to the PlayerAudience-Clients. It also commands the server to clear the logs.

``` csharp
class RequestCloseSession : MessageContainer 
{
    string sessionKey;
}
```

- **sessionKey:** The key of the to be closed session.

The server responds with a **[SessionClosed](#sessionclosed)** message.

#### SessionClosed

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::SessionClosed**. </br>
This message is sent from the server to the Moderator-Client in response to a **[RequestCloseSession](#requestclosesession)** message, to confirm that the session has been successfully closed and that the logs have been cleared completely. In addition to that, the statistics of the session are returned to the Moderator-Client, which can be used to display every conducted vote and which option got how many votes.

``` csharp
class SessionClosed : MessageContainer 
{
    Dictionary<int, int> statistics;
}
```

- **statistics:** Contains the id of the option as the key and the respective amount of recieved votes as the value.

## Sequence diagrams of typical communicational processes

ToDo
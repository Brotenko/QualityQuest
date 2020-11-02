<!---
ToDo:
- Verbindung geht verloren während Kommunikation/Abstimmung, Server bewahrt Daten auf (log), bei Reconnect werden relevante Nachrichten erneut gesendet.
- Server und M-Client führen Log bzgl Sachen die gesendet/empfangen wurden und können diese abgleichen wenn die Verbindung verloren gegangen ist.
  - Alle Logs werden prinzipiell am Ende einer Session und dem Start einer neuen Session restlos gelöscht
  - Nutzer-Cookies wahrscheinlich gleichermaßen logen 
- Server braucht einen Timer der nach X Minuten sagt "Okay, Verbindung zum M-Client ist zu lange weg, Zeit Logs zu löschen und die Session wieder freizugeben".
  - Wie regeln wir es, wenn der Moderator den Client neustartet und von vorne anfangen will, aber der Server blockiert, weil die Session noch läuft? 
  - Stattdessen Passwort-System auf Serverseite damit der Moderator machen kann was er will?
- 
-->

# Network protocol

## Nachrichten
- **RequestOpenSessionMessage**: Client sendet Nachricht an den Server um eine Session zu starten. Der Client wird dabei als Moderator festgelegt.
- **SessionOpenedMessage**: Der Server bestätigt die Anfrage und öffnet eine Session und setzt den Client als Moderator-Client fest.
- **RequestGameStartMessage**: Der Moderator-Client sendet eine Anfrage an den Server das Spiel zu starten.
- **GameStartedMessage**: Der Server bestätigt die Anfrage und startet das Spiel.
- **RequestStartVotingMessage**: Der Moderator-Client sendet eine Anfrage an den Server eine Umfrage mit Parametern XYZ zu starten.
- **VotingStartedMessage**: Der Server teilt dem Moderator-Client mit, dass eine Umfrage mit den Parametern XYZ gestartet ist.
- **VotingEndedMessage**: Der Server teilt dem Moderator-Client mit, dass die Abstimmung vorüber ist, und XYZ, UVW Stimmen bekommen hat.
- **ErrorMessage**: Wenn Problem zwischen Server und M-Client, Anfrage senden neu.
- **RequestPauseGameMessage**: Der M-Client sendet eine Anfrage an den Server das Spiel und die Abstimmungen zu pausieren.
- **GamePausedMessage**: Der Server bestätigt die Anfrage und pausiert alles.
- **RequestCloseSessionMessage**: Der M-Client sendet eine Anfrage an den Server das Spiel/die Session zu beenden.
- **SessionClosedMessage**: Der Server bestätigt die Anfrage und teilt allen Clients mit, dass die Session endet bevor er die Verbindung zu allen Clients trennt.
- **ModeratorAckMessage**: Der M-Client teilt dem Server am Ende eine Kommunikation mit, dass alles gepasst hat. Im Falle, dass der M-Ack nicht beim Server ankommt, ist ein Fehler aufgetreten.

## Verhalten bei Verbindungsverlust:
FR 25 (Ggf Zeit anpassen auf 2-5 Sekunden)

## Wer darf was senden

<table style="width:100%">
    <tr>
        <th>Messages</th>
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
                    <th style="font-weight: normal"><a href="#requestreconnect">RequestReconnect</a></th>
                    <th>✓</th>
                    <th></th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#reconnected">Reconnected</a></th>
                    <th></th>
                    <th>✓</th>
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
                <tr>
                    <th style="font-weight: normal"><a href="#moderatorack">ModeratorAck</a></th>
                    <th>✓</th>
                    <th></th>
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

## Was passiert im Falle einer illegalen Nachricht
- M-Client will resend wenn nur shit/falscher ACK ankommt.
- Server richtet sich stur nach den Nachrichten des M-Client (anhand des M-Ack).

## MessageContainer


``` csharp
class MessageContainer {
    Guid moderatorId; 
    MessageTypeEnum type;
    Date creationDate;
    String debugMessage;
}
```

## MessageTypeEnum

Lists all message types. The structuring by comments is only for overview and has no semantic meaning whatsoever. All messages are identified by the [MessageContainer](#messagecontainer).

``` csharp
enum MessageTypeEnum
{
    // Initialisation
    RequestOpenSession,
    SessionOpened,
    RequestReconnect,
    Reconnected,
    RequestGameStart,
    GameStarted,
    // Voting
    RequestStartVoting,
    VotingStarted,
    VotingEnded,
    // Control messages
    Error,
    RequestPauseGame,
    GamePaused,
    RequestContinueGame,
    GameContinued,
    ModeratorAck,
    // Postgame
    RequestCloseSession,
    SessionClosed
}
```

## ErrorTypeEnum

TODO

``` csharp
enum ErrorTypeEnum
{
    AlreadyServing,
    SessionDoesNotExist,
    IllegalMessage,
    UnnknownError
}
```

## Detailed message definitions

### Initialisation

#### RequestOpenSession

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::RequestOpenSession**. </br>
This message is sent from the Moderator-Client to the server when the moderator wants to connect to the server. In addition, the creation of a session is also requested from the server at the same time.

``` csharp
class RequestOpenSession : MessageContainer 
{
    // No extra fields needed
}
```

The server responds with a **[SessionOpened](#sessionopened)** message.

#### SessionOpened

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::SessionOpened**. </br>
This message is sent from the server to the Moderator-Client in response to a **[RequestOpenSession](#requestopensession)** message to provide the Moderator-Client with all necessary data to allow the audience to join the session.

``` csharp
class SessionOpened : MessageContainer 
{
    string roomKey;
    string directLink;
    Bitmap qrCode;
}
```

- **roomKey:** A randomly generated room key required by the audience to join the session, after connecting to the server.
- **directLink:** A direct link that the audience can use to connect to the server via the PlayerAudience-Client should the QR code not be usable.
- **qrCode:** A QR-code automatically generated by the server which can be scanned by the audience to connect to the server. 

#### RequestReconnect

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::RequestReconnect**. </br>
This message is sent from the Moderator-Client to the server to reestablish a lost connection. For this purpose, the Moderator-Client's Guid is required for comparison with the previously saved Moderator-Client Guid.

``` csharp
class RequestReconnect : MessageContainer 
{
    // No extra fields needed
}
```



#### RequestGameStart

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::RequestGameStart**. </br>
TODO

``` csharp
class RequestGameStart : MessageContainer 
{
    // No extra fields needed
}
```

The server responds with a [GameStarted](#gamestarted) messages.

#### GameStarted

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::GameStarted**. </br>
TODO

``` csharp
class GameStarted : MessageContainer 
{
    // No extra fields needed
}
```

### Voting

#### RequestStartVoting

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::RequestStartVoting**. </br>
TODO

``` csharp
class RequestStartVoting : MessageContainer 
{
    string[] votingOptions;
}
```

The server responds with a [VotingStarted](#votingstarted) messages.

#### VotingStarted

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::VotingStarted**. </br>
TODO

``` csharp
class VotingStarted : MessageContainer 
{
    // No extra fields needed
}
```

#### VotingEnded

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::VotingEnded**. </br>
TODO

``` csharp
class VotingEnded : MessageContainer 
{
    string winningOption;
    int[] votingResults;
}
```

### Control messages

#### Error

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::Error**. </br>
TODO

``` csharp
class Error : MessageContainer 
{
    ErrorTypeEnum e;
}
```

#### RequestPauseGameStatusChange

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::RequestPauseGameStatusChange**. </br>
TODO

``` csharp
class RequestPauseGameStatusChange : MessageContainer 
{
    bool gamePausedStatus;
}
```

#### GamePauseStatus

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::GamePauseStatus**. </br>
TODO

``` csharp
class GamePauseStatus : MessageContainer 
{
    bool gamePausedStatus;
}
```

#### ModeratorAck

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::ModeratorAck**. </br>
TODO

``` csharp
class ModeratorAck : MessageContainer 
{
    // No extra fields needed
}
```

### Postgame

#### RequestCloseSession

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::RequestCloseSession**. </br>
TODO

``` csharp
class RequestCloseSession : MessageContainer 
{
    string roomKey;
}
```

#### SessionClosed

Specification of a **[MessageContainer](#messagecontainer)** with the type **[MessageTypeEnum](#messagetypeenum)::SessionClosed**. </br>
TODO

``` csharp
class SessionClosed : MessageContainer 
{
    bool sessionRunning;
}
```

## Sequenzdiagramme zu typischen Abläufen
ToDo
## Nachrichten:
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
- **RequestContinueGameMessage**: Der M-Client sendet eine Anfrage an den Server das Spiel und die Abstimmungen wieder zu starten.
- **GameContinuedMessage**: Der Server bestätigt die Anfrage und startet das Spiel wieder.
- **RequestCloseSessionMessage**: Der M-Client sendet eine Anfrage an den Server das Spiel/die Session zu beenden.
- **SessionClosedMessage**: Der Server bestätigt die Anfrage und teilt allen Clients mit, dass die Session endet bevor er die Verbindung zu allen Clients trennt.
- **ModeratorAckMessage**: Der M-Client teilt dem Server am Ende eine Kommunikation mit, dass alles gepasst hat. Im Falle, dass der M-Ack nicht beim Server ankommt, ist ein Fehler aufgetreten.

## Verhalten bei Verbindungsverlust:
FR 25 (Ggf Zeit anpassen auf 2-5 Sekunden)

## Wer darf was senden:

<table style="width:100%">
    <tr>
        <th>Messages</th>
    </tr>
    <tr>
        <table style="width:100%">
            <tr>
                <th style="width:60%">Messages</th>
                <th style="width:28%">Moderator-Client</th>
                <th style="width:12%">Server</th>
            </tr>
        </table>
    </tr>
    <tr>
        <table style="width:100%">
            <tr>
                <th>Initialisation</th>
            </tr>
        </table>
    </tr>
    <tr>
        <table style="width:100%">
            <tr>
                <th style="width:60%; font-weight: normal">RequestOpenSession</th>
                <th style="width:28%">O</th>
                <th style="width:12%">O</th>
            </tr>
            <tr>
                <th style="font-weight: normal">SessionOpened</th>
                <th>O</th>
                <th>O</th>
            </tr>
            <tr>
                <th style="font-weight: normal">RequestGameStart</th>
                <th>O</th>
                <th>O</th>
            </tr>
            <tr>
                <th style="font-weight: normal">GameStarted</th>
                <th>O</th>
                <th>O</th>
            </tr>
        </table>
    </tr>
    <tr>
        <table style="width:100%">
            <tr>
                <th>Voting</th>
            </tr>
        </table>
    </tr>
    <tr>
        <table style="width:100%">
            <tr>
                <th style="width:60%; font-weight: normal">RequestStartVoting</th>
                <th style="width:28%">O</th>
                <th style="width:12%">O</th>
            </tr>
            <tr>
                <th style="font-weight: normal">VotingStarted</th>
                <th>O</th>
                <th>O</th>
            </tr>
            <tr>
                <th style="font-weight: normal">VotingEnded</th>
                <th>O</th>
                <th>O</th>
            </tr>
        </table>
    </tr>
    <tr>
        <table style="width:100%">
            <tr>
                <th>Control messages</th>
            </tr>
        </table>
    </tr>
    <tr>
        <table style="width:100%">
            <tr>
                <th style="width:60%; font-weight: normal">Error</th>
                <th style="width:28%">O</th>
                <th style="width:12%">O</th>
            </tr>
            <tr>
                <th style="font-weight: normal">RequestPauseGame</th>
                <th>O</th>
                <th>O</th>
            </tr>
            <tr>
                <th style="font-weight: normal">GamePaused</th>
                <th>O</th>
                <th>O</th>
            </tr>
            <tr>
                <th style="font-weight: normal">RequestContinueGame</th>
                <th>O</th>
                <th>O</th>
            </tr>
            <tr>
                <th style="font-weight: normal">GameContinued</th>
                <th>O</th>
                <th>O</th>
            </tr>
            <tr>
                <th style="font-weight: normal">ModeratorAck</th>
                <th>O</th>
                <th>O</th>
            </tr>
        </table>
    </tr>
    <tr>
        <table style="width:100%">
            <tr>
                <th>Postgame</th>
            </tr>
        </table>
    </tr>
    <tr>
        <table style="width:100%">
            <tr>
                <th style="width:60%; font-weight: normal">RequestCloseSession</th>
                <th style="width:28%">O</th>
                <th style="width:12%">O</th>
            </tr>
            <tr>
                <th style="font-weight: normal">SessionClosed</th>
                <th>O</th>
                <th>O</th>
            </tr>
        </table>
    </tr>
</table>

</br>

## Was passiert im Falle einer illegalen Nachricht:
- M-Client will resend wenn nur shit/falscher ACK ankommt.
- Server richtet sich stur nach den Nachrichten des M-Client (anhand des M-Ack).

## MessageContainer:
ToDo

## MessageTypeEnum:

Lists all message types. The structuring by comments is only for overview and has no semantic meaning whatsoever. All messages are identified by the [MessageContainer](#messagecontainer).

``` csharp
enum MessageTypeEnum
{
    // Initialisation
    RequestOpenSession,
    SessionOpened,
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

## Messages im Detail:
ToDo

## Sequenzdiagramme zu typischen Abläufen:
ToDo
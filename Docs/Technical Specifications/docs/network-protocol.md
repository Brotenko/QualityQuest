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

|sdfsf |
|:-:|
|<table style="width:100%"><tr><th style="width:60%">Messages</th><th style="width:28%">Moderator-Client</th><th style="width:12%">Server</th></tr></table> |

</br>

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
                    <th style="width:28%">O</th>
                    <th style="width:12%">O</th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#sessionopened">SessionOpened</a></th>
                    <th>O</th>
                    <th>O</th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#requestgamestart">RequestGameStart</a></th>
                    <th>O</th>
                    <th>O</th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#gamestarted">GameStarted</a></th>
                    <th>O</th>
                    <th>O</th>
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
                    <th style="width:28%">O</th>
                    <th style="width:12%">O</th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#votingstarted">VotingStarted</a></th>
                    <th>O</th>
                    <th>O</th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#votingended">VotingEnded</a></th>
                    <th>O</th>
                    <th>O</th>
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
                    <th style="width:28%">O</th>
                    <th style="width:12%">O</th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#requestpausegame">RequestPauseGame</a></th>
                    <th>O</th>
                    <th>O</th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#gamepaused">GamePaused</a></th>
                    <th>O</th>
                    <th>O</th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#requestcontinuegame">RequestContinueGame</a></th>
                    <th>O</th>
                    <th>O</th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#gamecontinued">GameContinued</a></th>
                    <th>O</th>
                    <th>O</th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#moderatorack">ModeratorAck</a></th>
                    <th>O</th>
                    <th>O</th>
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
                    <th style="width:28%">O</th>
                    <th style="width:12%">O</th>
                </tr>
                <tr>
                    <th style="font-weight: normal"><a href="#sessionclosed">SessionClosed</a></th>
                    <th>O</th>
                    <th>O</th>
                </tr>
            </table>
        </td>
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

### RequestOpenSession

## Sequenzdiagramme zu typischen Abläufen:
ToDo
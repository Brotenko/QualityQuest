## Nachrichten:
- **RequestOpenSessionMessage**: Client sendet Nachricht an den Server um eine Session zu starten. Der Client wird dabei als Moderator festgelegt.
- **SessionOpenedMessage**: Der Server bestätigt die Anfrage und öffnet eine Session und setzt den Client als Moderator-Client fest.
- **HelloMessage**: Client sendet Nachricht an den Server um sich für das Spiel anzumelden. 
- **HelloReplyMessage**: Der Server bestätigt die Anmeldung des Clients.
- **ReconnectMessage**: Hat ein Client die Verbindung zwischenzeitig verloren, kann er diese anhand der Übermittlung der UUID wieder aufnehmen.
- **RequestGameStartMessage**: Der Moderator-Client sendet eine Anfrage an den Server das Spiel zu starten.
- **GameStartedMessage**: Der Server bestätigt die Anfrage und startet das Spiel.
- **RequestStartVotingMessage**: Der Moderator-Client sendet eine Anfrage an den Server eine Umfrage mit Parametern XYZ zu starten.
- **VotingStartedMessage**: Der Server teilt den Clients mit, dass eine Umfrage mit den Parametern XYZ gestartet ist.
- **VoteMessage**: Ein PlayerAudience-Client teilt dem Server mit wofür dieser gestimmt hat. 
- _**VoteReplyMessage**: Der PlayerAudience-Client bekommt vom Server eine Rückmeldung, dass die Abstimmung erfolgreich war. (An Nachrichten an PA-Clients wird die UUID angehangen und vom Backend nur an den jeweiligen Client weitergeleitet)_
- **VoteStatusMessage**: Der Server teilt mit wie der Zwischenstand der Abstimmung ist.
- **VotingEndedMessage**: Der Server teilt den Clients mit, dass die Abstimmung vorüber ist. (PA-Clients schließen die Abstimmung und M-Client gibt das Ergebnis wieder)
- _**StrikeMessage**: Entweder ignorieren wir einfach faulty Messages oder schmeißen den Client raus, wenn er 3x hintereinander mist redet._
- _**ErrorMessage**: Siehe oben_
- **RequestPauseGameMessage**: Der M-Client sendet eine Anfrage an den Server das Spiel und die Abstimmungen zu pausieren.
- **GamePausedMessage**: Der Server bestätigt die Anfrage und pausiert alles.
- **RequestContinueGameMessage**: Der M-Client sendet eine Anfrage an den Server das Spiel und die Abstimmungen wieder zu starten.
- **GameContinuedMessage**: Der Server bestätigt die Anfrage und startet das Spiel wieder.
- **RequestCloseSessionMessage**: Der M-Client sendet eine Anfrage an den Server das Spiel/die Session zu beenden.
- **SessionClosedMessage**: Der Server bestätigt die Anfrage und teilt allen Clients mit, dass die Session endet bevor er die Verbindung zu allen Clients trennt.

## Verhalten bei Verbindungsverlust:

## Wer darf was senden:

## Was passiert im Falle einer illegalen Nachricht:

## MessageContainer:

## MessageTypeEnum:

## Messages im Detail:

## Sequenzdiagramme zu typischen Abläufen:
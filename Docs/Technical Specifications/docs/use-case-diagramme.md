# Use-case diagrams



## PlayerAudience-Client
![PlayerAudience-Client](diagrams/UseCase/PlayerAudience_Client.svg)

Via the PlayerAudience-Client, the individual participant, as part of the audience, has the possibility to vote on the decisions that occur in the game. 
The PlayerAudience-Client also acts as an actor for the server.

## Server
![Server](diagrams/UseCase/Server.svg)

Server receives the respective decisions from the PlayerAudience-Client, collects and evaluates them in relation to the number of votes.
The server also acts as an actor for the Moderator-Client.

## Moderator-Client

### Start Application
![Start Application](diagrams/UseCase/Moderator_Client_A.svg)

The moderator is responsible for starting the application. While starting the game an intro is shown. From the main menu the moderator can change settings or close the application directly. The moderator can establish a connection to the server to play the game with online polling or the moderator can start the game in Offline-Mode.


### Play Game

![Play Game](diagrams/UseCase/Moderator_Client_B.svg)

If there is a connection to the server, the server is gathering data from the audience and telling the Moderator-Client what is to be done depending on the data collected. The moderator can pause the game, when the game is paused by the moderator a message is sent to the server, so the voting process can also be paused by the server. If there is no connection, the moderator plays the game.

### End Application

![End Application](diagrams/UseCase/Moderator_Client_C.svg)

The moderator is responsible for ending the application. When the moderator ends the application, a message is sent to the server so that the server can end the current session with the PlayerAudience.



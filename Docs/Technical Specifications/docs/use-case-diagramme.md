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

To start the game the Moderator-Client has to be launched by the moderator first.
During the start of the application a flash screen with the NewTec logo is shown after which the main menu is loaded.
In the main menu, a connection to the server must first be established to begin playing the game with online voting.
If a connection has been established the game can be started in the main menu.
There is also an option to start the game in Offline-Mode in case no connection to the server can be established or online voting is not needed.
In the main menu a submenu which allows the moderator to set options like volume, resolution and language can also be accessed.
The main menu also allows the moderator to close the game.


### Play Game

![Play Game](diagrams/UseCase/Moderator_Client_B.svg)

If there is a connection to the server, the server is gathering data from the audience and telling the Moderator-Client what is to be done depending on the data collected. The moderator can pause the game locally, which simultaneously sends a message to the server to stop the election process on the server side. If there is no connection, the moderator plays the game.

### End Application

![End Application](diagrams/UseCase/Moderator_Client_C.svg)

The moderator is responsible for ending the application. When the moderator ends the application, a message is sent to the server so that the server can end the current session with the PlayerAudience.

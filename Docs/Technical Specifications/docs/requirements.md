# Requirements 

The requirements are divided into different priorities, whose meaning should be clear from the following table:

| PRIORITY | DESCRIPTION                                                                                                                                                       |
| -------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| +        | The requirement must be fulfilled in any case so that the product can be accepted.                                                                                |
| 0        | The fulfillment of the requirement is optional and therefore not necessarily a prerequisite for acceptance, but would have a very positive effect on the product. |
| -        | The fulfillment of the requirement is also optional and therefore not a prerequisite for the acceptance.                                                          |

</span>

## Functional requirements

This section contains all requirements that specify the basic actions of the software system.

<h4 style="margin-bottom: 0em"; id="game-type">Game type</h4>
| ID          | FR1                                                                                                                                                                                                                                                                                                   |
| ----------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                                                                                                                                                                                     |
| DESCRIPTION | QualityQuest shall be a 2D Visual-Novel-RPG.                                                                                                                                                                                                                                                          |
| EXPLANATION | The PlayerAudience makes decisions over the PlayerCharacter in a fictional, high-fantasy world of a software engineer. The PlayerAudience plays the game only through StoryFlowDecisions, comparable to a Visual Novel in which the PlayerAudience can actively decide on certain parts of the story. |

</span>

<h4 style="margin-bottom: 0em"; id="game-presentation">Game presentation</h4>
| ID          | FR2                                                                                                                                  |
| ----------- | ------------------------------------------------------------------------------------------------------------------------------------ |
| PRIORITY    | +                                                                                                                                    |
| DESCRIPTION | QualityQuest shall be a visual-based 2D RPG.                                                                                         |
| EXPLANATION | This means that QualityQuest shall not be a purely text-based game, but text may be an element of the visual appearance of the game. |

</span>

<h4 style="margin-bottom: 0em"; id="newtec-branding">NewTec branding</h4>
| ID          | FR3                                                                           |
| ----------- | ----------------------------------------------------------------------------- |
| PRIORITY    | +                                                                             |
| DESCRIPTION | QualityQuest shall display the NewTec logo clearly visible at any given time. |
| EXPLANATION | -                                                                             |

</span>

<h4 style="margin-bottom: 0em"; id="game-language">Game language</h4>
| ID          | FR4                                                                                                                                                                                                      |
| ----------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                                                                                        |
| DESCRIPTION | The main language of QualityQuest shall be German.                                                                                                                                                       |
| EXPLANATION | The majority of the to be used in-game language shall be german, but typical software engineering terms that are not german, but are commonly used in the german language, do not need to be translated. |

</span>

<h4 style="margin-bottom: 0em"; id="game-language-options">Game language options</h4>
| ID          | FR5                                             |
| ----------- | ----------------------------------------------- |
| PRIORITY    | 0                                               |
| DESCRIPTION | QualityQuest should support multiple languages. |
| EXPLANATION | -                                               |

</span>

<h4 style="margin-bottom: 0em"; id="music">Music</h4>
| ID          | FR6                                                                                                |
| ----------- | -------------------------------------------------------------------------------------------------- |
| PRIORITY    | -                                                                                                  |
| DESCRIPTION | QualityQuest may be accompanied by a suitable musical background to enhance the player experience. |
| EXPLANATION | -                                                                                                  |

</span>

<h4 style="margin-bottom: 0em"; id="sound-effects">Sound effects</h4>
| ID          | FR7                                                                                 |
| ----------- | ----------------------------------------------------------------------------------- |
| PRIORITY    | 0                                                                                   |
| DESCRIPTION | QualityQuest should emphasize important events of the StoryFlow with sound effects. |
| EXPLANATION | -                                                                                   |

</span>

<h4 style="margin-bottom: 0em"; id="game-content">Game content</h4>
| ID          | FR8                                                                                                               |
| ----------- | ----------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                 |
| DESCRIPTION | QualityQuest shall tell a story which mainly consists of typical elements of the software engineering profession. |
| EXPLANATION | -                                                                                                                 |

</span>

<h4 style="margin-bottom: 0em"; id="storyflow">StoryFlow</h4>
| ID          | FR9                                                                                                                                                                              |
| ----------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                                                                |
| DESCRIPTION | The story of QualityQuest shall be non-linear.                                                                                                                                   |
| EXPLANATION | The story shall contain elements where the PlayerAudience needs to make a StoryFlowDecision. Depending on the decision, the StoryFlow shall continue in different StoryBranches. |

</span>

<h4 style="margin-bottom: 0em"; id="influence-on-the-storyflow-by-the-player">Influence on the StoryFlow by the player</h4>
| ID          | FR10                                                                                              |
| ----------- | ------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                 |
| DESCRIPTION | The PlayerAudience shall influence the selection of StoryBranches by means of StoryFlowDecisions. |
| EXPLANATION | -                                                                                                 |

</span>

<h4 style="margin-bottom: 0em"; id="participation-of-a-larger-playeraudience">Participation of a larger PlayerAudience</h4>
| ID          | FR11                                                                                                                            |
| ----------- | ------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                               |
| DESCRIPTION | QualityQuest shall have the option to let a larger audience participate in StoryFlowDecisions by means of online voting.        |
| EXPLANATION | The online voting feature is directly embedded into the game, together with an offline backup in case the server can't be used. |

</span>

<h4 style="margin-bottom: 0em"; id="random-element-of-storyflow-control">Random element of StoryFlow control</h4>
| ID          | FR12                                                                                  |
| ----------- | ------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                     |
| DESCRIPTION | The selection of a StoryBranch after a StoryFlowDecision shall be generated randomly. |
| EXPLANATION | Randomness can be either determined through ZeroRandomness or DiceRandomness.         |

</span>

<h4 style="margin-bottom: 0em"; id="visualizing-the-randomness">Visualizing the randomness</h4>
| ID          | FR13                                                                                                                                                                         |
| ----------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                                                            |
| DESCRIPTION | If the selection of a StoryBranch after a StoryFlowDecision is generated with DiceRandomness, QualityQuest shall display a clear visualization of the randomization process. |
| EXPLANATION | -                                                                                                                                                                            |

</span>

<h4 style="margin-bottom: 0em"; id="character-status-values">Character status values</h4>
| ID          | FR14                                                                                                                             |
| ----------- | -------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                |
| DESCRIPTION | The PlayerCharacter shall have all of the following PlayerCharacterStatusValue: Programming, Analytics, Communication, Partying. |
| EXPLANATION | -                                                                                                                                |

</span>

<h4 style="margin-bottom: 0em"; id="selecting-a-character">Selecting a character</h4>
| ID          | FR15                                                                                                                                          |
| ----------- | --------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                             |
| DESCRIPTION | At the start of the game the PlayerAudience shall choose a PlayerCharacter from a selection of 4 possible PlayerCharacters via online voting. |
| EXPLANATION | -                                                                                                                                             |

</span>

<h4 style="margin-bottom: 0em"; id="presentation-of-character-status-values">Presentation of character status values</h4>
| ID          | FR16                                                                                                     |
| ----------- | -------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                        |
| DESCRIPTION | QualityQuest shall display a PlayerCharacterStatusBox with all PlayerCharacterStatusValues at all times. |
| EXPLANATION | -                                                                                                        |

</span>

<h4 style="margin-bottom: 0em"; id="portrait-of-the-playercharacter">Portrait of the PlayerCharacter</h4>
| ID          | FR17                                                                                                               |
| ----------- | ------------------------------------------------------------------------------------------------------------------ |
| PRIORITY    | +                                                                                                                  |
| DESCRIPTION | QualityQuest shall display a portrait of the PlayerCharacter as part of the PlayerCharacterStatusBox all the time. |
| EXPLANATION | -                                                                                                                  |

</span>

<h4 style="margin-bottom: 0em"; id="character-levelling">Character levelling</h4>
| ID          | FR18                                                                                                                    |
| ----------- | ----------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                       |
| DESCRIPTION | The PayerCharacter shall level up and level down its PlayerCharacterStatusValues based on events or StoryFlowDecisions. |
| EXPLANATION | -                                                                                                                       |

</span>

<h4 style="margin-bottom: 0em"; id="visual-presentation-of-playercharacter-status-changes">Visual presentation of PlayerCharacter status changes</h4>
| ID          | FR19                                                                                            |
| ----------- | ----------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                               |
| DESCRIPTION | The change of PlayerCharacterStatusValues of the PlayerCharacter shall be highlighted visually. |
| EXPLANATION | -                                                                                               |

</span>

<h4 style="margin-bottom: 0em"; id="acoustic-presentation-of-playercharacter-status-changes">Acoustic presentation of PlayerCharacter status changes</h4>
| ID          | FR20                                                                                                 |
| ----------- | ---------------------------------------------------------------------------------------------------- |
| PRIORITY    | 0                                                                                                    |
| DESCRIPTION | The change of PlayerCharacterStatusValues of the PlayerCharacter should be highlighted acoustically. |
| EXPLANATION | -                                                                                                    |

</span>

<h4 style="margin-bottom: 0em"; id="operating-system">Operating system</h4>
| ID          | FR21                                                             |
| ----------- | ---------------------------------------------------------------- |
| PRIORITY    | +                                                                |
| DESCRIPTION | QualityQuest shall run on Microsoft Windows 10 operating system. |
| EXPLANATION | -                                                                |

</span>

<h4 style="margin-bottom: 0em"; id="pause-game">Pause Game</h4>
| ID          | FR22                                                                             |
| ----------- | -------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                |
| DESCRIPTION | The moderator shall have the possibility to pause the game with the PauseButton. |
| EXPLANATION | -                                                                                |

</span>

<h4 style="margin-bottom: 0em"; id="pausebutton-location">PauseButton location</h4>
| ID          | FR23                                                    |
| ----------- | ------------------------------------------------------- |
| PRIORITY    | +                                                       |
| DESCRIPTION | The PauseButton shall be around the lower right corner. |
| EXPLANATION | -                                                       |

</span>

<h4 style="margin-bottom: 0em"; id="moderator-game-control">Moderator game control</h4>
| ID          | FR24                                                                                                                                                     |
| ----------- | -------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                                        |
| DESCRIPTION | Once the Moderator-Client established the connection to the ServerLogic, the moderator shall have the option to start or interrupt the game at any time. |
| EXPLANATION | -                                                                                                                                                        |

</span>

<h4 style="margin-bottom: 0em"; id="connection-timeout">Connection Timeout</h4>
| ID          | FR25                                                                                                                                                                                                                                                                                                                  |
| ----------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                                                                                                                                                                                                     |
| DESCRIPTION | If the Moderator-Client does not react within 5 seconds after receiving the ServerLogic's message, the connection from the Moderator-Client to the ServerLogic shall be interrupted. In this case the Moderator can either continue playing in Offline-Mode or try to re-establish the connection to the ServerLogic. |
| EXPLANATION | This serves as a failsafe, in case of corrupted messages or connection loss.                                                                                                                                                                                                                                          |

</span>

<h4 style="margin-bottom: 0em"; id="serverlogic-connection-loss">ServerLogic connection loss</h4>
| ID          | FR26                                                                                                                                                                                                                                        |
| ----------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                                                                                                                           |
| DESCRIPTION | If a Moderator-Client or PlayerAudience-Client loses its connection to the ServerLogic, its GUID shall be stored in the ServerLogic. In this case, the respective client can reconnect to the ServerLogic to participate in the game again. |
| EXPLANATION | -                                                                                                                                                                                                                                           |

</span>

<h4 style="margin-bottom: 0em"; id="data-exchange-file-format">Data exchange file format</h4>
| ID          | FR27                                                                                      |
| ----------- | ----------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                         |
| DESCRIPTION | The file format for data exchange between Moderator-Client and ServerLogic shall be JSON. |
| EXPLANATION | -                                                                                         |

</span>

<h4 style="margin-bottom: 0em"; id="playeraudience-client-guid">PlayerAudience-Client GUID</h4>
| ID          | FR28                                                                                                                                                                                                                                                                                                                       |
| ----------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                                                                                                                                                                                                          |
| DESCRIPTION | Every PlayerAudience-Client shall be assigned a GUID in the form of a SignalR connectionId.                                                                                                                                                                                                                                |
| EXPLANATION | This ensures the following points: </br></br><ul><li>The SignalR-Hub can ensure a PlayerAudience-Client can't vote several times per vote.</li><li>The ServerLogic can count the amount of PlayerAudience-Clients connected.</li><li>The SignalR-Hub can keep exact track of every active PlayerAudience-Client.</li></ul> |

</span>

<h4 style="margin-bottom: 0em"; id="offline-mode">Offline-Mode</h4>
| ID          | FR29                                                                                                                                                                                                                                                                                   |
| ----------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                                                                                                                                                                      |
| DESCRIPTION | If any of the following conditions apply: </br></br><ul><li>The server is not functional</li><li>The network infrastructure slows down significantly</li><li>The connection between Moderator-Client and Server is problematic</li></ul>the Moderator shall continue the game offline. |
| EXPLANATION | -                                                                                                                                                                                                                                                                                      |

</span>

<h4 style="margin-bottom: 0em"; id="offline-mode-transition">Offline-Mode transition</h4>
| ID          | FR30                                                                                                                  |
| ----------- | --------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                     |
| DESCRIPTION | The Offline-Mode must ensure a smooth transition between online and offline and shall be able to step in at any time. |
| EXPLANATION | -                                                                                                                     |

</span>

<h4 style="margin-bottom: 0em"; id="network-protocol-violation">Network protocol violation</h4>
| ID          | FR31                                                                                                                                          |
| ----------- | --------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                             |
| DESCRIPTION | If the Moderator-Client does not adhere to the network protocol 3 times in a row, the connection to the Moderator-Client shall be terminated. |
| EXPLANATION | This ensures that it is not easily possible to tinker with the game through an altered client.                                                |

</span>

<h4 style="margin-bottom: 0em"; id="unique-voting-option-identifier">Unique voting option identifier</h4>
| ID          | FR32                                                                                       |
| ----------- | ------------------------------------------------------------------------------------------ |
| PRIORITY    | +                                                                                          |
| DESCRIPTION | Every voting option shall be assigned a unique voting option identifier in form of a GUID. |
| EXPLANATION | -                                                                                          |

</span>

<h4 style="margin-bottom: 0em"; id="game-relevant-serverlogic-logging">Game-relevant ServerLogic logging</h4>
| ID          | FR33                                                                                                                                       |
| ----------- | ------------------------------------------------------------------------------------------------------------------------------------------ |
| PRIORITY    | +                                                                                                                                          |
| DESCRIPTION | Data that is needed for the general course of the game or for communication between clients and server shall be logged by the ServerLogic. |
| EXPLANATION | Relevant data is: </br></br><ul><li>The Moderator-Client GUID</li><li>Online-Session key</li></ul>                                         |

</span>

<h4 style="margin-bottom: 0em"; id="general-serverlogic-logging">General ServerLogic logging</h4>
| ID          | FR34                                                                                                                                  |
| ----------- | ------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                     |
| DESCRIPTION | The most important and communication relevant operations of the ServerLogic shall be logged on the server side for debugging reasons. |
| EXPLANATION | The general data logged by the ServerLogic is neither integral nor personal user data.                                                |

</span>

<h4 style="margin-bottom: 0em"; id="serverlogic-log-deletion">ServerLogic log deletion</h4>
| ID          | FR35                                                                                                                                                                                                                                                                               |
| ----------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                                                                                                                                                                  |
| DESCRIPTION | Game-relevant logs shall be cleared under following circumstances: </br></br><ul><li>No Moderator-Client has been connected to the ServerLogic in the last 30 minutes.</li><li>A new Online-Session is being started.</li><li>An ongoing Online-Session is being closed.</li></ul> |
| EXPLANATION | This ensures that no user-specific data is being saved on server side longer than it has to be.                                                                                                                                                                                    |

</span>

<h4 style="margin-bottom: 0em"; id="serverlogic-access-password">ServerLogic access-password</h4>
| ID          | FR36                                                                                  |
| ----------- | ------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                     |
| DESCRIPTION | The ServerLogic shall require a password to connect to it through a Moderator-Client. |
| EXPLANATION | -                                                                                     |

</span>

<h4 style="margin-bottom: 0em"; id="encryption-of-integral-data">Encryption of integral data</h4>
| ID          | FR37                                                                                           |
| ----------- | ---------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                              |
| DESCRIPTION | Integral data shall be hashed and salted on the server side.                                   |
| EXPLANATION | An example for integral data is, but is not limited to being, the ServerLogic access-password. |

</span>

<h4 style="margin-bottom: 0em"; id="online-mode">Online-Mode</h4>
| ID          | FR38                                                                                                                                            |
| ----------- | ----------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                               |
| DESCRIPTION | If the Moderator-Client is connected to a server, and the moderator is not currently playing in Offline-Mode, the game shall be in Online-Mode. |
| EXPLANATION | -                                                                                                                                               |

</span>

<h4 style="margin-bottom: 0em"; id="online-mode-flag">Online-Mode flag</h4>
| ID          | FR39                                                                                                                                                                                                                 |
| ----------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                                                                                                    |
| DESCRIPTION | A flag shall be set at the initialization of the game to distinguish between a pure Offline-Session, and an Online-Session that can switch between Offline-Mode and Online-Mode.                                     |
| EXPLANATION | For network protocol purposes, a flag is set to distinguish whether the game was initialized in Online-Mode or Offline-Mode, so that sending and receiving messages is disregarded right from the start of the game. |

</span>

<h4 style="margin-bottom: 0em"; id="moderator-client-guid">Moderator-Client GUID</h4>
| ID          | FR40                                                                                                    |
| ----------- | ------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                       |
| DESCRIPTION | The Moderator-Client shall be assigned a GUID by the ServerLogic.                                       |
| EXPLANATION | The GUID serves to identify as the Moderator-Client during on-going communication with the ServerLogic. |

</span>

<h4 style="margin-bottom: 0em"; id="playeraudience-client-count">PlayerAudience-Client count</h4>
| ID          | FR41                                                                                                          |
| ----------- | ------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | 0                                                                                                             |
| DESCRIPTION | The ServerLogic should keep count of the number of PlayerAudience-Clients being connected to the ServerLogic. |
| EXPLANATION | -                                                                                                             |

</span>

<h4 style="margin-bottom: 0em"; id="playeraudience-client-count-live-update">PlayerAudience-Client count live update</h4>
| ID          | FR42                                                                                                                                                                                     |
| ----------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | 0                                                                                                                                                                                        |
| DESCRIPTION | The ServerLogic should inform the Moderator-Client in 3 seconds intervals about the amount of PlayerAudience-Clients connected to the ServerLogic, as long as the game didn't start yet. |
| EXPLANATION | -                                                                                                                                                                                        |

</span>

<h4 style="margin-bottom: 0em"; id="online-session-permanence">Online-Session permanence</h4>
| ID          | FR43                                                                                                                                                                                                                                                                                                             |
| ----------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                                                                                                                                                                                                |
| DESCRIPTION | An Online-Session shall persist until one of the following conditions apply: </br></br><ul><li>A new Moderator-Client connects to the ServerLogic.</li><li>A new Online-Session is started by the moderator.</li><li>No Moderator-Client has been connected to the ServerLogic in the last 30 minutes.</li></ul> |
| EXPLANATION | -                                                                                                                                                                                                                                                                                                                |

</span>

<h4 style="margin-bottom: 0em"; id="switch-between-moderator-clients">Switch between Moderator-Clients</h4>
| ID          | FR44                                                                                                                                                                                                                       |
| ----------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                                                                                                          |
| DESCRIPTION | If a Moderator-Client connects to the ServerLogic while another Moderator-Client is already connected to the ServerLogic, then the old Moderator-Client shall be kicked and the new Moderator-Client shall stay connected. |
| EXPLANATION | -                                                                                                                                                                                                                          |

</span>

<h4 style="margin-bottom: 0em"; id="voting-timer-stop-on-pause">Voting-Timer stop on pause</h4>
| ID          | FR45                                                                        |
| ----------- | --------------------------------------------------------------------------- |
| PRIORITY    | +                                                                           |
| DESCRIPTION | The Voting-Timer shall pause when the Moderator-Client initializes a pause. |
| EXPLANATION | -                                                                           |

</span>

<h4 style="margin-bottom: 0em"; id="communication-during-pauses">Communication during pauses</h4>
| ID          | FR46                                                                                                                                       |
| ----------- | ------------------------------------------------------------------------------------------------------------------------------------------ |
| PRIORITY    | +                                                                                                                                          |
| DESCRIPTION | Communication between Moderator-Client and ServerLogic, and between PlayerAudience-Clients and ServerLogic shall persist during the pause. |
| EXPLANATION | -                                                                                                                                          |

</span>

<h4 style="margin-bottom: 0em"; id="playeraudience-connection-method">PlayerAudience connection method</h4>
| ID          | FR47                                                                                                                               |
| ----------- | ---------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                  |
| DESCRIPTION | The PlayerAudience shall be able to join an Online-Session using a dynamically generated session key, provided by the ServerLogic. |
| EXPLANATION | -                                                                                                                                  |

</span>

<h4 style="margin-bottom: 0em"; id="playeraudience-connection-option">PlayerAudience connection option</h4>
| ID          | FR48                                                                                                                            |
| ----------- | ------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                               |
| DESCRIPTION | The PlayerAudience shall be provided with an URL, at the start of the Online-Session, to be able to connect to the ServerLogic. |
| EXPLANATION | -                                                                                                                               |

</span>

<h4 style="margin-bottom: 0em"; id="additional-playeraudience-connection-options">Additional PlayerAudience connection options</h4>
| ID          | FR49                                                                                                                                     |
| ----------- | ---------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | 0                                                                                                                                        |
| DESCRIPTION | The PlayerAudience should also be provided with a QR code, at the start of the Online-Session, to be able to connect to the ServerLogic. |
| EXPLANATION | -                                                                                                                                        |

</span>

<h4 style="margin-bottom: 0em"; id="pause-menu">Pause menu</h4>
| ID          | FR50                                                            |
| ----------- | --------------------------------------------------------------- |
| PRIORITY    | +                                                               |
| DESCRIPTION | The pause menu shall pop-up after a pause has been initialized. |
| EXPLANATION | -                                                               |

</span>

<h4 style="margin-bottom: 0em"; id="pause-menu-contents">Pause menu contents</h4>
| ID          | FR51                                                                                                                                                                                                                                     |
| ----------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | 0                                                                                                                                                                                                                                        |
| DESCRIPTION | The pause menu should display the following elements: </br></br><ul><li>A banner that reads "Pause"</li><li>A button to unpause the game</li><li>The PlayerAudience connection method</li><li>All PlayerAudience connection options</li> |
| EXPLANATION | -                                                                                                                                                                                                                                        |

</span>

<h4 style="margin-bottom: 0em"; id="vote-indexing-procedure">Vote indexing procedure</h4>
| ID          | FR52                                                                         |
| ----------- | ---------------------------------------------------------------------------- |
| PRIORITY    | +                                                                            |
| DESCRIPTION | The procedure used for indexing the different voting options shall be GUIDs. |
| EXPLANATION | -                                                                            |

</span>

<h4 style="margin-bottom: 0em"; id="cryptographic-hashing-procedure">Cryptographic hashing procedure</h4>
| ID          | FR53                                                                    |
| ----------- | ----------------------------------------------------------------------- |
| PRIORITY    | +                                                                       |
| DESCRIPTION | The hashing procedure used for cryptographic purposes shall be SHA-256. |
| EXPLANATION | -                                                                       |

</span>

<h4 style="margin-bottom: 0em"; id="sessionkey-format">SessionKey format</h4>
| ID          | FR54                                                                                                  |
| ----------- | ----------------------------------------------------------------------------------------------------- |
| PRIORITY    | -                                                                                                     |
| DESCRIPTION | The SessionKey shall consist of six uppercase, alphanumerical characters.                             |
| EXPLANATION | For example: <code>F8G21Z</code> or <code>8IB2P4</code> <br> Key-Pattern: <code>@"[A-Z0-9]{6}"</code> |

</span>

<h4 style="margin-bottom: 0em"; id="playeraudience-client-count-display">PlayerAudience-Client count display</h4>
| ID          | FR55                                                                                                                                                           |
| ----------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                                              |
| DESCRIPTION | The PlayerAudience-Client count shall be displayed in the Moderator-Client, as long as the PlayerAudience-Client count is transmitted to the Moderator-Client. |
| EXPLANATION | For example: <code>F8G21Z</code> or <code>8IB2P4</code>                                                                                                        |

</span>

<h4 style="margin-bottom: 0em"; id="multi-game-support">Multi-Game support</h4>
| ID          | FR56                                                                  |
| ----------- | --------------------------------------------------------------------- |
| PRIORITY    | -                                                                     |
| DESCRIPTION | The ServerLogic may support several Online-Sessions at the same time. |
| EXPLANATION | -                                                                     |

</span>

<h4 style="margin-bottom: 0em"; id="playeraudience-client-persistence">ServerLogic persistence</h4>
| ID          | FR57                                                                                                   |
| ----------- | ------------------------------------------------------------------------------------------------------ |
| PRIORITY    | +                                                                                                      |
| DESCRIPTION | The ServerLogic shall not crash or terminate a session upon receiving a faulty message or faulty data. |
| EXPLANATION | -                                                                                                      |

</span>

<h4 style="margin-bottom: 0em"; id="playeraudience-client-content">PlayerAudience-Client content</h4>
| ID          | FR58                                                                                                                                                                                                                                                                                                                                         |
| ----------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                                                                                                                                                                                                                            |
| DESCRIPTION | The PlayerAudience-Client shall contain the following pages and links: </br></br><ul><li>A landing page for cookies and Terms of Service.</li><li>A main homepage that turns into the game.</li><li>A link to the NewTec website.</li><li>A link to the Github Repository.</li><li>An about page.</li><li>A Terms of Service page.</li></ul> |
| EXPLANATION | -                                                                                                                                                                                                                                                                                                                                            |

</span>

<h4 style="margin-bottom: 0em"; id="playeraudience-client-cookies-and-terms-of-service">PlayerAudience-Client cookies and Terms of Service</h4>
| ID          | FR59                                                                                |
| ----------- | ----------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                   |
| DESCRIPTION | The PlayerAudience-Client shall inform the user about cookies and Terms of Service. |
| EXPLANATION | -                                                                                   |

</span>

<h4 style="margin-bottom: 0em"; id="playeraudience-client-landing-page">PlayerAudience-Client landing page</h4>
| ID          | FR60                                                                                                              |
| ----------- | ----------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                 |
| DESCRIPTION | The PlayerAudience-Client shall display a cookies and Terms of Service pop-up which the user needs to consent to. |
| EXPLANATION | -                                                                                                                 |

</span>

<h4 style="margin-bottom: 0em"; id="playeraudience-wrong-sessionkey-feedback">PlayerAudience-Client wrong SessionKey feedback</h4>
| ID          | FR61                                                                                                       |
| ----------- | ---------------------------------------------------------------------------------------------------------- |
| PRIORITY    | 0                                                                                                          |
| DESCRIPTION | The user should receive instant feedback upon entering a faulty SessionKey into the PlayerAudience-Client. |
| EXPLANATION | -                                                                                                          |

</span>

<h4 style="margin-bottom: 0em"; id="playeraudience-client-error-logging">PlayerAudience-Client error logging</h4>
| ID          | FR62                                                                                                  |
| ----------- | ----------------------------------------------------------------------------------------------------- |
| PRIORITY    | 0                                                                                                     |
| DESCRIPTION | Errors caused by faulty communication between PlayerAudience-Client and ServerLogic should be logged. |
| EXPLANATION | -                                                                                                     |

</span>

<h4 style="margin-bottom: 0em"; id="playeraudience-client-persistent-connection">PlayerAudience-Client persistent connection</h4>
| ID          | FR63                                                                              |
| ----------- | --------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                 |
| DESCRIPTION | The connection between PlayerAudience-Client and ServerLogic shall be persistent. |
| EXPLANATION | -                                                                                 |

</span>

<h4 style="margin-bottom: 0em"; id="asp-net-and-signalr">ASP.NET and SignalR</h4>
| ID          | FR64                                                                                                                                         |
| ----------- | -------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                            |
| DESCRIPTION | The ServerLogic and PlayerAudience-Client shall use ASP.NET and SignalR for the implementation of the website and the persistent connection. |
| EXPLANATION | -                                                                                                                                            |

</span>

<h4 style="margin-bottom: 0em"; id="playeraudience-client-statistics-display">PlayerAudience-Client statistics display</h4>
| ID          | FR65                                                                       |
| ----------- | -------------------------------------------------------------------------- |
| PRIORITY    | -                                                                          |
| DESCRIPTION | The PlayerAudience-Client may display the voting results of the last poll. |
| EXPLANATION | -                                                                          |

</span>

<h4 style="margin-bottom: 0em"; id="playeraudience-client-language">PlayerAudience-Client language</h4>
| ID          | FR66                                                             |
| ----------- | ---------------------------------------------------------------- |
| PRIORITY    | +                                                                |
| DESCRIPTION | The main language of the PlayerAudience-Client shall be English. |
| EXPLANATION | -                                                                |

</span>

<h4 style="margin-bottom: 0em"; id="additional-playeraudience-client-languages">Additional PlayerAudience-Client languages</h4>
| ID          | FR67                                                       |
| ----------- | ---------------------------------------------------------- |
| PRIORITY    | -                                                          |
| DESCRIPTION | The PlayerAudience-Client may support the german language. |
| EXPLANATION | -                                                          |

</span>



## Non-functional Requirements

This section specifies the non-functional requirements for the software system.

<h4 style="margin-bottom: 0em"; id="documents-to-be-delivered">Documents to be delivered</h4>
| ID          | NFR1                                                                                                                                                                                                                                                                       |
| ----------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                                                                                                                                                          |
| DESCRIPTION | A Technical Specification, which comprises use case diagrams, use case descriptions and a static view of the software architecture and Software Design Specification for each software component, which describes both the static and the dynamic view shall be delivered. |
| EXPLANATION | -                                                                                                                                                                                                                                                                          |

</span>

<h4 style="margin-bottom: 0em"; id="in-code-documentation-style">In-code documentation style</h4>
| ID          | NFR2                                                                                                                 |
| ----------- | -------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                    |
| DESCRIPTION | The source code shall be documented by means of XML comment documentation generation provided by Visual Studio 2019. |
| EXPLANATION | -                                                                                                                    |

</span>

<h4 style="margin-bottom: 0em"; id="in-code-documentation-content">In-code documentation content</h4>
| ID          | NFR3                                                                                                                                                                                                                                            |
| ----------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                                                                                                                               |
| DESCRIPTION | All of the following source code elements shall be documented: Constants, variables and defines. Classes and class members. Methods and method signatures, including return values. Functions and function signatures, including return values. |
| EXPLANATION | -                                                                                                                                                                                                                                               |

</span>

<h4 style="margin-bottom: 0em"; id="documentation-style-for-diagrams">Documentation style for diagrams</h4>
| ID          | NFR4                                                      |
| ----------- | --------------------------------------------------------- |
| PRIORITY    | +                                                         |
| DESCRIPTION | All documentation diagrams shall follow the UML standard. |
| EXPLANATION | -                                                         |

</span>

<h4 style="margin-bottom: 0em"; id="delivery-of-uml-diagrams">Delivery of UML diagrams</h4>
| ID          | NFR5                                                                              |
| ----------- | --------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                 |
| DESCRIPTION | All UML diagrams shall be delivered in the form of a diagram and a PlantUML link. |
| EXPLANATION | -                                                                                 |

</span>

<h4 style="margin-bottom: 0em"; id="adherence-to-project-coding-styleguide">Adherence to project Coding Styleguide</h4>
| ID          | NFR6                                                                                                     |
| ----------- | -------------------------------------------------------------------------------------------------------- |
| PRIORITY    | 0                                                                                                        |
| DESCRIPTION | The software code should adhere to the code quality rules and .NET API usage rules Microsoft recommends. |
| EXPLANATION | -                                                                                                        |

</span>

<h4 style="margin-bottom: 0em"; id="adherence-to-clean-code-principles">Adherence to Clean Code Principles</h4>
| ID          | NFR7                                                                          |
| ----------- | ----------------------------------------------------------------------------- |
| PRIORITY    | +                                                                             |
| DESCRIPTION | The software code shall adhere to Grade 1 (Red) of the Clean Code Principles. |
| EXPLANATION | -                                                                             |

</span>

<h4 style="margin-bottom: 0em"; id="target-audience">Target audience</h4>
| ID          | NFR8                                                                                                          |
| ----------- | ------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                             |
| DESCRIPTION | QualityQuest shall address a target audience of university students with interest in a SW engineering career. |
| EXPLANATION | -                                                                                                             |

</span>

<h4 style="margin-bottom: 0em"; id="playing-time">Playing time</h4>
| ID          | NFR9                                                                                      |
| ----------- | ----------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                         |
| DESCRIPTION | The complete story of QualityQuest shall be playable in a time frame of 15 to 20 minutes. |
| EXPLANATION | -                                                                                         |

</span>

<h4 style="margin-bottom: 0em"; id="playing-fun">Playing fun</h4>
| ID          | NFR10                                         |
| ----------- | --------------------------------------------- |
| PRIORITY    | 0                                             |
| DESCRIPTION | The story of QualityQuest should be humorous. |
| EXPLANATION | -                                             |

</span>

<h4 style="margin-bottom: 0em"; id="player-motivation">Player motivation</h4>
| ID          | NFR11                                                                                          |
| ----------- | ---------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                              |
| DESCRIPTION | The audience of QualityQuest shall be encouraged to follow the story by motivational elements. |
| EXPLANATION | Motivational elements could be for example rewards, achievement & level upgrades.              |

</span>

<h4 style="margin-bottom: 0em"; id="deliverable-artefacts">Deliverable artefacts</h4>
| ID          | NFR12                                                                                          |
| ----------- | ---------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                              |
| DESCRIPTION | Documentation, Source Code and a running version of QualityQuest shall be delivered to NewTec. |
| EXPLANATION | -                                                                                              |

</span>

<h4 style="margin-bottom: 0em"; id="type-of-delivery">Type of delivery</h4>
| ID          | NFR13                                                                                                                                      |
| ----------- | ------------------------------------------------------------------------------------------------------------------------------------------ |
| PRIORITY    | +                                                                                                                                          |
| DESCRIPTION | All deliverable artifacts shall be delivered digitally.                                                                                    |
| EXPLANATION | The delivery can be by depositing the deliverable artefacts in a public version control system. Documents should be delivered in HTML/CSS. |

</span>

<h4 style="margin-bottom: 0em"; id="deadline">Deadline</h4>
| ID          | NFR14                                              |
| ----------- | -------------------------------------------------- |
| PRIORITY    | +                                                  |
| DESCRIPTION | The deadline for the final delivery is 2021-04-28. |
| EXPLANATION | -                                                  |

</span>

<h4 style="margin-bottom: 0em"; id="open-source-development">Open source development</h4>
| ID          | NFR15                                                                                                          |
| ----------- | -------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | -                                                                                                              |
| DESCRIPTION | The Source Code of QualityQuest may be published open source under CreativeCommons CC BY-NC 4.0 license terms. |
| EXPLANATION | -                                                                                                              |

</span>

<h4 style="margin-bottom: 0em"; id="stand-alone-game">Stand-alone game</h4>
| ID          | NFR16                                                                                                                                                                                                                     |
| ----------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                                                                                                         |
| DESCRIPTION | QualityQuest shall be a stand-alone game.                                                                                                                                                                                 |
| EXPLANATION | The final binaries shall include everything that is needed to run the game. Any possibly needed frameworks have to be included in the delivery. The installation of additional frameworks or libraries is not acceptable. |

</span>

<h4 style="margin-bottom: 0em"; id="programming-language">Programming language</h4>
| ID          | NFR17                                   |
| ----------- | --------------------------------------- |
| PRIORITY    | +                                       |
| DESCRIPTION | QualityQuest shall be programmed in C#. |
| EXPLANATION | -                                       |

</span>

<h4 style="margin-bottom: 0em"; id="development-environment">Development environment</h4>
| ID          | NFR18                                                                                                                                                                                        |
| ----------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                                                                            |
| DESCRIPTION | Both the source code and the build solution of QualityQuest shall be buildable in one of the following development environments: Microsoft Visual Studio 2019, Microsoft Visual Studio Code. |
| EXPLANATION | -                                                                                                                                                                                            |

</span>

<h4 style="margin-bottom: 0em"; id="usage-of-online-voting-solutions">Usage of online voting solutions</h4>
| ID          | NFR19                                                                                                                                                                   |
| ----------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | -                                                                                                                                                                       |
| DESCRIPTION | A self-made online voting solution shall be created by the team in order to implement the requirements of the tool and ensure the compatibility as optimal as possible. |
| EXPLANATION | -                                                                                                                                                                       |

</span>

<h4 style="margin-bottom: 0em"; id="amount-of-supported-connections">Amount of supported connections</h4>
| ID          | NFR20                                                                                           |
| ----------- | ----------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                               |
| DESCRIPTION | The Server shall allow up to 200 PlayerAudience-Clients to connect to the game via the network. |
| EXPLANATION | -                                                                                               |

</span>

<h4 style="margin-bottom: 0em"; id="exclusive-moderator-client-connection">Exclusive Moderator-Client connection</h4>
| ID          | NFR21                                                                                                       |
| ----------- | ----------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                           |
| DESCRIPTION | The ServerLogic shall only allow a single Moderator-Client to connect to the ServerLogic at any given time. |
| EXPLANATION | -                                                                                                           |

</span>

<h4 style="margin-bottom: 0em"; id="game-engine">Game Engine</h4>
| ID          | NFR22                                                                                                                                                                                                                                                                                                                       |
| ----------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                                                                                                                                                                                                                           |
| DESCRIPTION | As a game engine the project shall use Unity.                                                                                                                                                                                                                                                                               |
| EXPLANATION | The license conditions of the game engine allow the source code of QualityQuest to be open source. The license conditions of the game engine allow the usage of the game engine without license fees. The license conditions of the game engine allow the usage of QualityQuest as intended by NewTec without license fees. |

</span>

<h4 style="margin-bottom: 0em"; id="communication-security">Communication security</h4>
| ID          | NFR23                                                                                                                |
| ----------- | -------------------------------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                                                    |
| DESCRIPTION | The safe communication between Moderator-Client and the ServerLogic shall be guaranteed through WebSocket and HTTPS. |
| EXPLANATION | -                                                                                                                    |

</span>

<h4 style="margin-bottom: 0em"; id="postgame-statistics">Postgame statistics</h4>
| ID          | NFR24                                                                                     |
| ----------- | ----------------------------------------------------------------------------------------- |
| PRIORITY    | 0                                                                                         |
| DESCRIPTION | The Moderator-Client should display postgame statistics at the end of the Online-Session. |
| EXPLANATION | -                                                                                         |

</span>

<h4 style="margin-bottom: 0em"; id="postgame-statistics-contents">Postgame statistic contents</h4>
| ID          | NFR25                                                                    |
| ----------- | ------------------------------------------------------------------------ |
| PRIORITY    | 0                                                                        |
| DESCRIPTION | The postgame statistics should display which option was voted how often. |
| EXPLANATION | -                                                                        |

</span>

<h4 style="margin-bottom: 0em"; id="unit-test-coverage">Unit test coverage</h4>
| ID          | NFR26                                                                                        |
| ----------- | -------------------------------------------------------------------------------------------- |
| PRIORITY    | +                                                                                            |
| DESCRIPTION | Moderator-Client, PlayerAudience-Client and ServerLogic shall have a 60% unit test coverage. |
| EXPLANATION | -                                                                                            |

</span>

<h4 style="margin-bottom: 0em"; id="integration-test-coverage">Integration test coverage</h4>
| ID          | NFR27                                                     |
| ----------- | --------------------------------------------------------- |
| PRIORITY    | +                                                         |
| DESCRIPTION | The Moderator-Client shall have a 60% unit test coverage. |
| EXPLANATION | -                                                         |

</span>
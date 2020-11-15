# Requirements 

The requirements are divided into different priorities, whose meaning should be clear from the following table:

| PRIORITY | DESCRIPTION                                                                                                                                                       |
| -------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| +        | The requirement must be fulfilled in any case so that the product can be accepted.                                                                                |
| 0        | The fulfillment of the requirement is optional and therefore not necessarily a prerequisite for acceptance, but would have a very positive effect on the product. |
| -        | The fulfillment of the requirement is also optional and therefore not a prerequisite for the acceptance.                                                          |

</span>

## Functional requirements

This section contains all requirements that specify the basic actions of the software system.

| REQUIREMENT | Game type                                                                                                                                                                                                                                                                                     |
| ----------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FR1                                                                                                                                                                                                                                                                                           |
| PRIORITY    | +                                                                                                                                                                                                                                                                                             |
| DESCRIPTION | QualityQuest shall be a 2D RPG.                                                                                                                                                                                                                                                               |
| EXPLANATION | The PlayerAudience takes over the decision of a character in a fictional world of a software engineer. The PlayerAudience plays the game only through StoryFlowDecisions, for example the game plays like a movie in which the PlayerAudience takes over the decisions of the main character. |

</span>

| REQUIREMENT | Game presentation                                                                                                                    |
| ----------- | :----------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FR2                                                                                                                                  |
| PRIORITY    | +                                                                                                                                    |
| DESCRIPTION | QualityQuest shall be a visual-based 2D RPG.                                                                                         |
| EXPLANATION | This means that QualityQuest shall not be a purely text-based game, but text may be an element of the visual appearance of the game. |

</span>

| REQUIREMENT | NewTec branding                                                          |
| ----------- | :----------------------------------------------------------------------- |
| ID          | FR3                                                                      |
| PRIORITY    | +                                                                        |
| DESCRIPTION | QualityQuest shall display the NewTec logo clearly visible all the time. |
| EXPLANATION | -                                                                        |

</span>

| REQUIREMENT | Game language                                                                                                                                                                |
| ----------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FR4                                                                                                                                                                          |
| PRIORITY    | +                                                                                                                                                                            |
| DESCRIPTION | The main language of QualityQuest shall be German.                                                                                                                           |
| EXPLANATION | The majority of in-game language shall be German, but typical software engineering terms that are not German, but are commonly used in Germany do not need to be translated. |

</span>

| REQUIREMENT | Game language options                           |
| ----------- | :---------------------------------------------- |
| ID          | FR5                                             |
| PRIORITY    | 0                                               |
| DESCRIPTION | QualityQuest should support multiple languages. |
| EXPLANATION | -                                               |

</span>

| REQUIREMENT | Music                                                                                              |
| ----------- | :------------------------------------------------------------------------------------------------- |
| ID          | FR6                                                                                                |
| PRIORITY    | -                                                                                                  |
| DESCRIPTION | QualityQuest may be accompanied by a suitable musical background to enhance the player experience. |
| EXPLANATION | -                                                                                                  |

</span>

| REQUIREMENT | Sound effects                                                                       |
| ----------- | :---------------------------------------------------------------------------------- |
| ID          | FR7                                                                                 |
| PRIORITY    | 0                                                                                   |
| DESCRIPTION | QualityQuest should emphasize important events of the StoryFlow with sound effects. |
| EXPLANATION | -                                                                                   |

</span>

| REQUIREMENT | Game content                                                                                                      |
| ----------- | :---------------------------------------------------------------------------------------------------------------- |
| ID          | FR8                                                                                                               |
| PRIORITY    | +                                                                                                                 |
| DESCRIPTION | QualityQuest shall tell a story which mainly consists of typical elements of the software engineering profession. |
| EXPLANATION | -                                                                                                                 |

</span>

| REQUIREMENT | StoryFlow                                                                                                                                                                        |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FR9                                                                                                                                                                              |
| PRIORITY    | +                                                                                                                                                                                |
| DESCRIPTION | The story of QualityQuest shall be non-linear.                                                                                                                                   |
| EXPLANATION | The story shall contain elements where the PlayerAudience needs to make a StoryFlowDecision. Depending on the decision, the StoryFlow shall continue in different StoryBranches. |

</span>

| REQUIREMENT | Influence on the StoryFlow by the player                                                          |
| ----------- | :------------------------------------------------------------------------------------------------ |
| ID          | FR10                                                                                              |
| PRIORITY    | +                                                                                                 |
| DESCRIPTION | The PlayerAudience shall influence the selection of StoryBranches by means of StoryFlowDecisions. |
| EXPLANATION | -                                                                                                 |

</span>

| REQUIREMENT | Participation of a larger PlayerAudience                                                                                                                                                                                      |
| ----------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FR11                                                                                                                                                                                                                          |
| PRIORITY    | +                                                                                                                                                                                                                             |
| DESCRIPTION | QualityQuest shall have the option to let a larger audience participate in StoryFlowDecisions by means of OnlineVoting.                                                                                                       |
| EXPLANATION | It would be highly desirable that the OnlineVoting feature is directly embedded into the game, together with an offline backup in case the server can't be used. Other methods are acceptable depending on the circumstances. |

</span>

| REQUIREMENT | Random element of StoryFlow control                                                   |
| ----------- | :------------------------------------------------------------------------------------ |
| ID          | FR12                                                                                  |
| PRIORITY    | +                                                                                     |
| DESCRIPTION | The selection of a StoryBranch after a StoryFlowDecision shall be generated randomly. |
| EXPLANATION | Randomness can be either determined through ZeroRandomness or DiceRandomness.         |

</span>

| REQUIREMENT | Visualizing the randomness                                                                                                                                                   |
| ----------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FR13                                                                                                                                                                         |
| PRIORITY    | +                                                                                                                                                                            |
| DESCRIPTION | If the selection of a StoryBranch after a StoryFlowDecision is generated with DiceRandomness, QualityQuest shall display a clear visualization of the randomization process. |
| EXPLANATION | -                                                                                                                                                                            |

</span>

| REQUIREMENT | Character status values                                                                                             |
| ----------- | :------------------------------------------------------------------------------------------------------------------ |
| ID          | FR14                                                                                                                |
| PRIORITY    | +                                                                                                                   |
| DESCRIPTION | The PlayerCharacter shall have all of the following status values: Programming, Analytics, Communication, Partying. |
| EXPLANATION | -                                                                                                                   |

</span>

| REQUIREMENT | Selecting a character                                                                                                                             |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------ |
| ID          | FR15                                                                                                                                              |
| PRIORITY    | +                                                                                                                                                 |
| DESCRIPTION | At the start of the game the PlayerAudience shall choose a PlayerCharacter from a selection of 4 possible PlayerCharacters via the voting system. |
| EXPLANATION | -                                                                                                                                                 |

</span>

| REQUIREMENT | Presentation of character status values                                                    |
| ----------- | :----------------------------------------------------------------------------------------- |
| ID          | FR16                                                                                       |
| PRIORITY    | +                                                                                          |
| DESCRIPTION | QualityQuest shall display a PlayerCharacterStatusBox with all status values at all times. |
| EXPLANATION | -                                                                                          |

</span>

| REQUIREMENT | Portrait of the PlayerCharacter                                                                                    |
| ----------- | :----------------------------------------------------------------------------------------------------------------- |
| ID          | FR17                                                                                                               |
| PRIORITY    | +                                                                                                                  |
| DESCRIPTION | QualityQuest shall display a portrait of the PlayerCharacter as part of the PlayerCharacterStatusBox all the time. |
| EXPLANATION | -                                                                                                                  |

</span>

| REQUIREMENT | Character levelling                                                                                       |
| ----------- | :-------------------------------------------------------------------------------------------------------- |
| ID          | FR18                                                                                                      |
| PRIORITY    | +                                                                                                         |
| DESCRIPTION | The PayerCharacter shall level up and level down its status values based on events or StoryFlowDecisions. |
| EXPLANATION | -                                                                                                         |

</span>

| REQUIREMENT | Visual presentation of PlayerCharacter status changes                             |
| ----------- | :-------------------------------------------------------------------------------- |
| ID          | FR19                                                                              |
| PRIORITY    | +                                                                                 |
| DESCRIPTION | The change of status values of the PlayerCharacter shall be highlighted visually. |
| EXPLANATION | -                                                                                 |

</span>

| REQUIREMENT | Acoustic presentation of PlayerCharacter status changes                                |
| ----------- | :------------------------------------------------------------------------------------- |
| ID          | FR20                                                                                   |
| PRIORITY    | 0                                                                                      |
| DESCRIPTION | The change of status values of the PlayerCharacter should be highlighted acoustically. |
| EXPLANATION | -                                                                                      |

</span>

| REQUIREMENT | Operating system                                                 |
| ----------- | :--------------------------------------------------------------- |
| ID          | FR21                                                             |
| PRIORITY    | +                                                                |
| DESCRIPTION | QualityQuest shall run on Microsoft Windows 10 operating system. |
| EXPLANATION | -                                                                |

</span>

| REQUIREMENT | Pause Game                                                                       |
| ----------- | :------------------------------------------------------------------------------- |
| ID          | FR22                                                                             |
| PRIORITY    | +                                                                                |
| DESCRIPTION | The moderator shall have the possibility to pause the game with the PauseButton. |
| EXPLANATION | -                                                                                |

</span>

| REQUIREMENT | PauseButton location                                  |
| ----------- | :---------------------------------------------------- |
| ID          | FR23                                                  |
| PRIORITY    | +                                                     |
| DESCRIPTION | The PauseButton shall be around the lower right edge. |
| EXPLANATION | -                                                     |

</span>

| REQUIREMENT | Moderator game control                                                                                                                                   |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FR24                                                                                                                                                     |
| PRIORITY    | +                                                                                                                                                        |
| DESCRIPTION | Once the Moderator-Client established the connection to the ServerLogic, the Moderator shall have the option to start or interrupt the game at any time. |
| EXPLANATION | -                                                                                                                                                        |

</span>

| REQUIREMENT | Connection Timeout                                                                                                                                                                                                                                                                                                    |
| ----------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FR25                                                                                                                                                                                                                                                                                                                  |
| PRIORITY    | +                                                                                                                                                                                                                                                                                                                     |
| DESCRIPTION | If the Moderator-Client does not react within 5 seconds after receiving the ServerLogic's message, the connection from the Moderator-Client to the ServerLogic shall be interrupted. In this case the Moderator can either continue playing in Offline-Mode or try to re-establish the connection to the ServerLogic. |
| EXPLANATION | This serves as a failsafe, in case of corrupted messages or connection loss.                                                                                                                                                                                                                                          |

</span>

| REQUIREMENT | ServerLogic connection loss                                                                                                                                                                                                                 |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| ID          | FR26                                                                                                                                                                                                                                        |
| PRIORITY    | +                                                                                                                                                                                                                                           |
| DESCRIPTION | If a Moderator-Client or PlayerAudience-Client loses its connection to the ServerLogic, its GUID shall be stored in the ServerLogic. In this case, the respective client can reconnect to the ServerLogic to participate in the game again. |
| EXPLANATION | -                                                                                                                                                                                                                                           |

</span>

| REQUIREMENT | Data exchange file format                                                                 |
| ----------- | :---------------------------------------------------------------------------------------- |
| ID          | FR27                                                                                      |
| PRIORITY    | +                                                                                         |
| DESCRIPTION | The file format for data exchange between Moderator-Client and ServerLogic shall be JSON. |
| EXPLANATION | -                                                                                         |

</span>

| REQUIREMENT | PlayerAudience-Client GUID                                                                                                                                                                                                                                                                                                                |
| ----------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FR28                                                                                                                                                                                                                                                                                                                                      |
| PRIORITY    | +                                                                                                                                                                                                                                                                                                                                         |
| DESCRIPTION | Every PlayerAudience-Client shall be assigned a GUID in the form of a web-cookie.                                                                                                                                                                                                                                                         |
| EXPLANATION | This ensures the following points: </br></br><ul><li>Participants can rejoin the game after leaving the game or losing the connection to the ServerLogic.</li><li>The ServerLogic can ensure a PlayerAudience-Client can't vote several times per vote.</li><li>The ServerLogic can count the amount of PlayerAudience-Clients connected. |

</span>

| REQUIREMENT | Offline-Mode                                                                                                                                                                                                                                                                           |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FR29                                                                                                                                                                                                                                                                                   |
| PRIORITY    | +                                                                                                                                                                                                                                                                                      |
| DESCRIPTION | If any of the following conditions apply: </br></br><ul><li>The server is not functional</li><li>The network infrastructure slows down significantly</li><li>The connection between Moderator-Client and Server is problematic</li></ul>the Moderator shall continue the game offline. |
| EXPLANATION | -                                                                                                                                                                                                                                                                                      |

</span>

| REQUIREMENT | Offline-Mode transition                                                                                               |
| ----------- | :-------------------------------------------------------------------------------------------------------------------- |
| ID          | FR30                                                                                                                  |
| PRIORITY    | +                                                                                                                     |
| DESCRIPTION | The Offline-Mode must ensure a smooth transition between online and offline and shall be able to step in at any time. |
| EXPLANATION | -                                                                                                                     |

</span>

| REQUIREMENT | Communication protocol violation                                                                                                             |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FR31                                                                                                                                         |
| PRIORITY    | +                                                                                                                                            |
| DESCRIPTION | If a client does not adhere to the communication protocol 3 times, the GUID of the participant should be excluded from the rest of the game. |
| EXPLANATION | This ensures that it is not easily possible to tinker with the game through an altered client.                                               |

</span>

| REQUIREMENT | Unique voting option identifier                                                                                                                                                                               |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| ID          | FR32                                                                                                                                                                                                          |
| PRIORITY    | +                                                                                                                                                                                                             |
| DESCRIPTION | Every voting option shall be assigned a unique voting option identifier by means of hashing the option.                                                                                                       |
| EXPLANATION | The option <code>You choose to not go to work today.</code> would result in the unique voting option identifier <code>dfe177d53212181e392cadda7d3972eee21d290b180720df6c43309e0ffb5d70</code>, using SHA-256. |

</span>

| REQUIREMENT | Game-relevant ServerLogic logging                                                                                                       |
| ----------- | :-------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FR33                                                                                                                                    |
| PRIORITY    | +                                                                                                                                       |
| DESCRIPTION | Data that is needed for the general course of the game or for communication between clients and servers shall be logged by ServerLogic. |
| EXPLANATION | Relevant data is: </br></br><ul><li>The Moderator-Client GUID</li><li>PlayerAudience-Client GUIDs</li><li>Online-Session key</li></ul>  |

</span>

| REQUIREMENT | General ServerLogic logging                                                                                                           |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------ |
| ID          | FR34                                                                                                                                  |
| PRIORITY    | +                                                                                                                                     |
| DESCRIPTION | The most important and communication relevant operations of the ServerLogic shall be logged on the server side for debugging reasons. |
| EXPLANATION | The general data logged by the ServerLogic is neither integral nor personal user data.                                                |

</span>

| REQUIREMENT | ServerLogic log deletion                                                                                                                                                                                                        |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| ID          | FR35                                                                                                                                                                                                                            |
| PRIORITY    | +                                                                                                                                                                                                                               |
| DESCRIPTION | Game-relevant logs shall be cleared under following circumstances: </br></br><ul><li>No Moderator-Client has been connected to the ServerLogic in the last 30 minutes.</li><li>A new Online-Session is being started.</li></ul> |
| EXPLANATION | This ensures that no user-specific data is being saved on server side longer than it has to be.                                                                                                                                 |

</span>

| REQUIREMENT | ServerLogic access-password                                                           |
| ----------- | :------------------------------------------------------------------------------------ |
| ID          | FR36                                                                                  |
| PRIORITY    | +                                                                                     |
| DESCRIPTION | The ServerLogic shall require a password to connect to it through a Moderator-Client. |
| EXPLANATION | -                                                                                     |

</span>

| REQUIREMENT | Encryption of integral data                                                                    |
| ----------- | :--------------------------------------------------------------------------------------------- |
| ID          | FR37                                                                                           |
| PRIORITY    | +                                                                                              |
| DESCRIPTION | Integral data shall be hashed and salted on the server side.                                   |
| EXPLANATION | An example for integral data is, but is not limited to being, the ServerLogic access-password. |

</span>

| REQUIREMENT | Online-Mode                                                                                                                                     |
| ----------- | :---------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FR38                                                                                                                                            |
| PRIORITY    | +                                                                                                                                               |
| DESCRIPTION | If the Moderator-Client is connected to a server, and the moderator is not currently playing in Offline-Mode, the game shall be in Online-Mode. |
| EXPLANATION | -                                                                                                                                               |

</span>

| REQUIREMENT | Online-Mode flag                                                                                                                                                                                                     |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FR39                                                                                                                                                                                                                 |
| PRIORITY    | +                                                                                                                                                                                                                    |
| DESCRIPTION | A flag shall be set at the initialization of the game to distinguish between a pure Offline-Session, and an Online-Session that can switch between Offline-Mode and Online-Mode.                                     |
| EXPLANATION | For network protocol purposes, a flag is set to distinguish whether the game was initialized in Online-Mode or Offline-Mode, so that sending and receiving messages is disregarded right from the start of the game. |

</span>

| REQUIREMENT | Moderator-Client GUID                                                                                   |
| ----------- | :------------------------------------------------------------------------------------------------------ |
| ID          | FR40                                                                                                    |
| PRIORITY    | +                                                                                                       |
| DESCRIPTION | The Moderator-Client shall be assigned a GUID.                                                          |
| EXPLANATION | The GUID serves to identify as the Moderator-Client during on-going communication with the ServerLogic. |

</span>

| REQUIREMENT | PlayerAudience-Client count                                                                                   |
| ----------- | :------------------------------------------------------------------------------------------------------------ |
| ID          | FR41                                                                                                          |
| PRIORITY    | 0                                                                                                             |
| DESCRIPTION | The ServerLogic should keep count of the number of PlayerAudience-Clients being connected to the ServerLogic. |
| EXPLANATION | -                                                                                                             |

</span>

| REQUIREMENT | PlayerAudience-Client count live update                                                                                                                                                  |
| ----------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FR42                                                                                                                                                                                     |
| PRIORITY    | 0                                                                                                                                                                                        |
| DESCRIPTION | The ServerLogic should inform the Moderator-Client in 3 seconds intervals about the amount of PlayerAudience-Clients connected to the ServerLogic, as long as the game didn't start yet. |
| EXPLANATION | -                                                                                                                                                                                        |

</span>

| REQUIREMENT | Online-Session permanence                                                                                                                                                                                                                                                                                        |
| ----------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FR43                                                                                                                                                                                                                                                                                                             |
| PRIORITY    | +                                                                                                                                                                                                                                                                                                                |
| DESCRIPTION | An Online-Session shall persist until one of the following conditions apply: </br></br><ul><li>A new Moderator-Client connects to the ServerLogic.</li><li>A new Online-Session is started by the moderator.</li><li>No Moderator-Client has been connected to the ServerLogic in the last 30 minutes.</li></ul> |
| EXPLANATION | -                                                                                                                                                                                                                                                                                                                |

</span>

| REQUIREMENT | Switch between Moderator-Clients                                                                                                                                                                                           |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FR44                                                                                                                                                                                                                       |
| PRIORITY    | +                                                                                                                                                                                                                          |
| DESCRIPTION | If a Moderator-Client connects to the ServerLogic while another Moderator-Client is already connected to the ServerLogic, then the old Moderator-Client shall be kicked and the new Moderator-Client shall stay connected. |
| EXPLANATION | -                                                                                                                                                                                                                          |

</span>

| REQUIREMENT | Voting-Timer stop on pause                                                  |
| ----------- | :-------------------------------------------------------------------------- |
| ID          | FR45                                                                        |
| PRIORITY    | +                                                                           |
| DESCRIPTION | The Voting-Timer shall pause when the Moderator-Client initializes a pause. |
| EXPLANATION | -                                                                           |

</span>

| REQUIREMENT | Communication during pauses                                                                                                                |
| ----------- | :----------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FR46                                                                                                                                       |
| PRIORITY    | +                                                                                                                                          |
| DESCRIPTION | Communication between Moderator-Client and ServerLogic, and between PlayerAudience-Clients and ServerLogic shall persist during the pause. |
| EXPLANATION |                                                                                                                                            |

</span>

| REQUIREMENT | PlayerAudience connection method                                                                                                   |
| ----------- | :--------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FR47                                                                                                                               |
| PRIORITY    | +                                                                                                                                  |
| DESCRIPTION | The PlayerAudience shall be able to join an Online-Session using a dynamically generated session key, provided by the ServerLogic. |
| EXPLANATION | -                                                                                                                                  |

</span>

| REQUIREMENT | PlayerAudience connection option                                                                                                |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------ |
| ID          | FR48                                                                                                                            |
| PRIORITY    | +                                                                                                                               |
| DESCRIPTION | The PlayerAudience shall be provided with an URL, at the start of the Online-Session, to be able to connect to the ServerLogic. |
| EXPLANATION | -                                                                                                                               |

</span>

| REQUIREMENT | Additional PlayerAudience connection options                                                                                             |
| ----------- | :--------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FR49                                                                                                                                     |
| PRIORITY    | 0                                                                                                                                        |
| DESCRIPTION | The PlayerAudience should also be provided with a QR code, at the start of the Online-Session, to be able to connect to the ServerLogic. |
| EXPLANATION | -                                                                                                                                        |

</span>

| REQUIREMENT | Pause menu                                                      |
| ----------- | :-------------------------------------------------------------- |
| ID          | FR50                                                            |
| PRIORITY    | +                                                               |
| DESCRIPTION | The pause menu shall pop-up after a pause has been initialized. |
| EXPLANATION | -                                                               |

</span>

| REQUIREMENT | Pause menu contents                                                                                                                                                                                                                       |
| ----------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FR51                                                                                                                                                                                                                                      |
| PRIORITY    | 0                                                                                                                                                                                                                                         |
| DESCRIPTION | The pause menu should display the following elements: </br></br><ul><li>A banner that reads "Paused"</li><li>A button to unpause the game</li><li>The PlayerAudience connection method</li><li>All PlayerAudience connection options</li> |
| EXPLANATION | -                                                                                                                                                                                                                                         |

</span>



## Non-functional Requirements

This section specifies the non-functional requirements for the software system.

| REQUIREMENT | Documents to be delivered                                                                                                                                                                                                                                               |
| ----------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | NFR1                                                                                                                                                                                                                                                                    |
| PRIORITY    | +                                                                                                                                                                                                                                                                       |
| DESCRIPTION | A System Specification, which comprises use case diagrams, use case descriptions and a static view of the software architecture and Software Design Specification for each software component, which describes both the static and the dynamic view shall be delivered. |
| EXPLANATION | -                                                                                                                                                                                                                                                                       |

</span>

| REQUIREMENT | In-code documentation style                                                   |
| ----------- | :---------------------------------------------------------------------------- |
| ID          | NFR2                                                                          |
| PRIORITY    | +                                                                             |
| DESCRIPTION | The source code shall be documented by means of Doxygen and in JavaDoc style. |
| EXPLANATION | -                                                                             |

</span>

| REQUIREMENT | In-code documentation content                                                                                                                                                                                                                   |
| ----------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | NFR3                                                                                                                                                                                                                                            |
| PRIORITY    | +                                                                                                                                                                                                                                               |
| DESCRIPTION | All of the following source code elements shall be documented: Constants, variables and defines. Classes and class members. Methods and method signatures, including return values. Functions and function signatures, including return values. |
| EXPLANATION | -                                                                                                                                                                                                                                               |

</span>

| REQUIREMENT | Documentation style for diagrams                          |
| ----------- | :-------------------------------------------------------- |
| ID          | NFR4                                                      |
| PRIORITY    | +                                                         |
| DESCRIPTION | All documentation diagrams shall follow the UML standard. |
| EXPLANATION | -                                                         |

</span>

| REQUIREMENT | Delivery of UML diagrams                                                          |
| ----------- | :-------------------------------------------------------------------------------- |
| ID          | NFR5                                                                              |
| PRIORITY    | +                                                                                 |
| DESCRIPTION | All UML diagrams shall be delivered in the form of a diagram and a PlantUML link. |
| EXPLANATION | -                                                                                 |

</span>

| REQUIREMENT | Adherence to project Coding Styleguide                            |
| ----------- | :---------------------------------------------------------------- |
| ID          | NFR6                                                              |
| PRIORITY    | 0                                                                 |
| DESCRIPTION | The software code should adhere to the Project Coding Styleguide. |
| EXPLANATION | -                                                                 |

</span>

| REQUIREMENT | Adherence to Clean Code Principles                                            |
| ----------- | :---------------------------------------------------------------------------- |
| ID          | NFR7                                                                          |
| PRIORITY    | +                                                                             |
| DESCRIPTION | The software code shall adhere to Grade 1 (Red) of the Clean Code Principles. |
| EXPLANATION | -                                                                             |

</span>

| REQUIREMENT | Target PlayerAudience                                                                                         |
| ----------- | :------------------------------------------------------------------------------------------------------------ |
| ID          | NFR8                                                                                                          |
| PRIORITY    | +                                                                                                             |
| DESCRIPTION | QualityQuest shall address a target audience of university students with interest in a SW engineering career. |
| EXPLANATION | -                                                                                                             |

</span>

| REQUIREMENT | Playing time                                                                              |
| ----------- | :---------------------------------------------------------------------------------------- |
| ID          | NFR9                                                                                      |
| PRIORITY    | +                                                                                         |
| DESCRIPTION | The complete story of QualityQuest shall be playable in a time frame of 15 to 20 minutes. |
| EXPLANATION | -                                                                                         |

</span>

| REQUIREMENT | Playing fun                                   |
| ----------- | :-------------------------------------------- |
| ID          | NFR10                                         |
| PRIORITY    | 0                                             |
| DESCRIPTION | The story of QualityQuest should be humorous. |
| EXPLANATION | -                                             |

</span>

| REQUIREMENT | Player motivation                                                                              |
| ----------- | :--------------------------------------------------------------------------------------------- |
| ID          | NFR11                                                                                          |
| PRIORITY    | +                                                                                              |
| DESCRIPTION | The audience of QualityQuest shall be encouraged to follow the story by motivational elements. |
| EXPLANATION | Motivational elements could be for example rewards, achievement & level upgrades.              |

</span>

| REQUIREMENT | Deliverable artefacts                                                                          |
| ----------- | :--------------------------------------------------------------------------------------------- |
| ID          | NFR12                                                                                          |
| PRIORITY    | +                                                                                              |
| DESCRIPTION | Documentation, Source Code and a running version of QualityQuest shall be delivered to NewTec. |
| EXPLANATION | -                                                                                              |

</span>

| REQUIREMENT | Type of delivery                                                                                                                                        |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------------ |
| ID          | NFR13                                                                                                                                                   |
| PRIORITY    | +                                                                                                                                                       |
| DESCRIPTION | All deliverable artifacts shall be delivered digitally.                                                                                                 |
| EXPLANATION | The delivery can be by depositing the deliverable artefacts in a public version control system. Documents should be delivered in both PDF and HTML/CSS. |

</span>

| REQUIREMENT | Deadline                                           |
| ----------- | :------------------------------------------------- |
| ID          | NFR14                                              |
| PRIORITY    | +                                                  |
| DESCRIPTION | The deadline for the final delivery is 2021-04-28. |
| EXPLANATION | -                                                  |

</span>

| REQUIREMENT | Open source development                                                                                        |
| ----------- | :------------------------------------------------------------------------------------------------------------- |
| ID          | NFR15                                                                                                          |
| PRIORITY    | -                                                                                                              |
| DESCRIPTION | The Source Code of QualityQuest may be published open source under CreativeCommons CC BY-NC 4.0 license terms. |
| EXPLANATION | -                                                                                                              |

</span>

| REQUIREMENT | Stand-alone game                                                                                                                                                                                                          |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| ID          | NFR16                                                                                                                                                                                                                     |
| PRIORITY    | +                                                                                                                                                                                                                         |
| DESCRIPTION | QualityQuest shall be a stand-alone game.                                                                                                                                                                                 |
| EXPLANATION | The final binaries shall include everything that is needed to run the game. Any possibly needed frameworks have to be included in the delivery. The installation of additional frameworks or libraries is not acceptable. |

</span>

| REQUIREMENT | Programming language                                            |
| ----------- | :-------------------------------------------------------------- |
| ID          | NFR17                                                           |
| PRIORITY    | +                                                               |
| DESCRIPTION | QualityQuest shall be programmed in a C dialect (C, C++ or C#). |
| EXPLANATION | -                                                               |

</span>

| REQUIREMENT | Development environment                                                                                                                                                                 |
| ----------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | NFR18                                                                                                                                                                                   |
| PRIORITY    | +                                                                                                                                                                                       |
| DESCRIPTION | Both the source code and the build solution of QualityQuest shall be buildable in one of the following development environments: Microsoft Visual Studio, Microsoft Visual Studio Code. |
| EXPLANATION | -                                                                                                                                                                                       |

</span>

| REQUIREMENT | Usage of online voting solutions                                                                                                                                        |
| ----------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | NFR19                                                                                                                                                                   |
| PRIORITY    | -                                                                                                                                                                       |
| DESCRIPTION | A self-made online voting solution shall be created by the team in order to implement the requirements of the tool and ensure the compatibility as optimal as possible. |
| EXPLANATION | -                                                                                                                                                                       |

</span>

| REQUIREMENT | Amount of supported connections                                                                 |
| ----------- | :---------------------------------------------------------------------------------------------- |
| ID          | NFR20                                                                                           |
| PRIORITY    | +                                                                                               |
| DESCRIPTION | The Server shall allow up to 200 PlayerAudience-Clients to connect to the game via the network. |
| EXPLANATION | -                                                                                               |

</span>

| REQUIREMENT | Exclusive Moderator-Client connection                                                                       |
| ----------- | :---------------------------------------------------------------------------------------------------------- |
| ID          | NFR21                                                                                                       |
| PRIORITY    | +                                                                                                           |
| DESCRIPTION | The ServerLogic shall only allow a single Moderator-Client to connect to the ServerLogic at any given time. |
| EXPLANATION | -                                                                                                           |

</span>

| REQUIREMENT | Game Engine                                                                                                                                                                                                                                                                                                                 |
| ----------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | NFR22                                                                                                                                                                                                                                                                                                                       |
| PRIORITY    | +                                                                                                                                                                                                                                                                                                                           |
| DESCRIPTION | As a game engine the project shall use Unity.                                                                                                                                                                                                                                                                               |
| EXPLANATION | The license conditions of the game engine allow the source code of QualityQuest to be open source. The license conditions of the game engine allow the usage of the game engine without license fees. The license conditions of the game engine allow the usage of QualityQuest as intended by NewTec without license fees. |

</span>

| REQUIREMENT | Communication security                                                                                               |
| ----------- | :------------------------------------------------------------------------------------------------------------------- |
| ID          | NFR23                                                                                                                |
| PRIORITY    | +                                                                                                                    |
| DESCRIPTION | The safe communication between Moderator-Client and the ServerLogic shall be guaranteed through WebSocket and HTTPS. |
| EXPLANATION | -                                                                                                                    |

</span>

| REQUIREMENT | Postgame statistics                                                                       |
| ----------- | :---------------------------------------------------------------------------------------- |
| ID          | NFR24                                                                                     |
| PRIORITY    | 0                                                                                         |
| DESCRIPTION | The Moderator-Client should display postgame statistics at the end of the Online-Session. |
| EXPLANATION | -                                                                                         |

</span>

| REQUIREMENT | Postgame statistic contents                                              |
| ----------- | :----------------------------------------------------------------------- |
| ID          | NFR25                                                                    |
| PRIORITY    | 0                                                                        |
| DESCRIPTION | The postgame statistics should display which option was voted how often. |
| EXPLANATION | -                                                                        |

</span>
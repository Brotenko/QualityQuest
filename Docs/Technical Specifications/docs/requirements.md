# Requirements 

The requirements are divided into different priorities, whose meaning should be clear from the following table:

| PRIORITY | DESCRIPTION                                                                                                                                                       |
| -------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| +        | The requirement must be fulfilled in any case so that the product can be accepted.                                                                                |
| 0        | The fulfillment of the requirement is optional and therefore not necessarily a prerequisite for acceptance, but would have a very positive effect on the product. |
| -        | The fulfilment of the requirement is also optional and therefore not a prerequisite for the acceptance.                                                           |

</br>

## Functional Requirements

This section contains all requirements that specify the basic actions of the software system.

| REQUIREMENT | Game type                                                                                                                                                                                                                                                                                     |
| ----------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FA1                                                                                                                                                                                                                                                                                           |
| PRIORITY    | +                                                                                                                                                                                                                                                                                             |
| DESCRIPTION | QualityQuest shall be a 2D RPG.                                                                                                                                                                                                                                                               |
| EXPLANATION | The PlayerAudience takes over the decision of a character in a fictional world of a software engineer. The PlayerAudience plays the game only through StoryFlowDecisions, for example the game plays like a movie in which the PlayerAudience takes over the decisions of the main character. |

</br>

| REQUIREMENT | Stand-alone game                                                                                                                                                                                                        |
| ----------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FA2                                                                                                                                                                                                                     |
| PRIORITY    | +                                                                                                                                                                                                                       |
| DESCRIPTION | QualityQuest shall be a stand-alone game.                                                                                                                                                                               |
| EXPLANATION | is means that the final binaries shall include everything that is needed to run the game. Any possibly needed framework needs to be included. The installation of additional frameworks or libraries is not acceptable. |

</br>

| REQUIREMENT | Game presentation                                                                                                                    |
| ----------- | :----------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FA3                                                                                                                                  |
| PRIORITY    | +                                                                                                                                    |
| DESCRIPTION | QualityQuest shall be a visual-based 2D RPG.                                                                                         |
| EXPLANATION | This means that QualityQuest shall not be a purely text-based game, but text may be an element of the visual appearance of the game. |

</br>

| REQUIREMENT | NewTec branding                                                          |
| ----------- | :----------------------------------------------------------------------- |
| ID          | FA4                                                                      |
| PRIORITY    | +                                                                        |
| DESCRIPTION | QualityQuest shall display the NewTec logo clearly visible all the time. |
| EXPLANATION | -                                                                        |

</br>

| REQUIREMENT | Game language                                                                                                                                                                |
| ----------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FA5                                                                                                                                                                          |
| PRIORITY    | +                                                                                                                                                                            |
| DESCRIPTION | The main language of QualityQuest shall be German.                                                                                                                           |
| EXPLANATION | The majority of in-game language shall be German, but typical software engineering terms that are not German, but are commonly used in Germany do not need to be translated. |

</br>

| REQUIREMENT | Game language options                           |
| ----------- | :---------------------------------------------- |
| ID          | FA6                                             |
| PRIORITY    | -                                               |
| DESCRIPTION | QualityQuest should support multiple languages. |
| EXPLANATION | -                                               |

</br>

| REQUIREMENT | Music                                                              |
| ----------- | :----------------------------------------------------------------- |
| ID          | FA7                                                                |
| PRIORITY    | -                                                                  |
| DESCRIPTION | QualityQuest may be accompanied by a suitable musical background to enhance the player experience. |
| EXPLANATION | -                                                                  |

</br>

| REQUIREMENT | Sound effects                                                                       |
| ----------- | :---------------------------------------------------------------------------------- |
| ID          | FA8                                                                                 |
| PRIORITY    | 0                                                                                   |
| DESCRIPTION | QualityQuest should emphasize important events of the StoryFlow with sound effects. |
| EXPLANATION | -                                                                                   |

</br>

| REQUIREMENT | Game content                                                                                                      |
| ----------- | :---------------------------------------------------------------------------------------------------------------- |
| ID          | FA9                                                                                                               |
| PRIORITY    | +                                                                                                                 |
| DESCRIPTION | QualityQuest shall tell a story which mainly consists of typical elements of the software engineering profession. |
| EXPLANATION | -                                                                                                                 |

</br>

| REQUIREMENT | StoryFlow                                                                                                                                                                        |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FA10                                                                                                                                                                             |
| PRIORITY    | +                                                                                                                                                                                |
| DESCRIPTION | The story of QualityQuest shall be non-linear.                                                                                                                                   |
| EXPLANATION | The story shall contain elements where the PlayerAudience needs to make a StoryFlowDecision. Depending on the decision, the StoryFlow shall continue in different StoryBranches. |

</br>

| REQUIREMENT | Influence on the StoryFlow by the player                                                          |
| ----------- | :------------------------------------------------------------------------------------------------ |
| ID          | FA11                                                                                              |
| PRIORITY    | +                                                                                                 |
| DESCRIPTION | The PlayerAudience shall influence the selection of StoryBranches by means of StoryFlowDecisions. |
| EXPLANATION | -                                                                                                 |

</br>

| REQUIREMENT | Participation of a larger PlayerAudience                                                                                                                    |
| ----------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FA12                                                                                                                                                        |
| PRIORITY    | +                                                                                                                                                           |
| DESCRIPTION | QualityQuest shall enable the PlayerAudience to let a larger audience participate in StoryFlowDecisions by means of OnlineVoting.                           |
| EXPLANATION | It would be highly desirable that the OnlineVoting feature is directly embedded into the game. Other methods are acceptable depending on the circumstances. |

</br>

| REQUIREMENT | Random element of StoryFlow control                                                   |
| ----------- | :------------------------------------------------------------------------------------ |
| ID          | FA13                                                                                  |
| PRIORITY    | +                                                                                     |
| DESCRIPTION | The selection of a StoryBranch after a StoryFlowDecision shall be generated randomly. |
| EXPLANATION | Randomness can be either determined through ZeroRandomness or DiceRandomness.         |

</br>

| REQUIREMENT | Visualizing the randomness                                                                                                                                                  |
| ----------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FA14                                                                                                                                                                        |
| PRIORITY    | +                                                                                                                                                                           |
| DESCRIPTION | If the selection of a StoryBranch after a StoryFlowDecision is generated with DiceRandomness, QualityQuest shall display a clear visualization of the randomizationprocess. |
| EXPLANATION | -                                                                                                                                                                           |

</br>

| REQUIREMENT | Character status values                                                                                                                                                                                                          |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FA15                                                                                                                                                                                                                             |
| PRIORITY    | +                                                                                                                                                                                                                                |
| DESCRIPTION | The PlayerCharacter shall have different status values, which can improve or worsen during the game. The PlayerCharacter shall have all of the following status values: Gender, Programming, Analytics, Communication, Partying. |
| EXPLANATION | -                                                                                                                                                                                                                                |

</br>

| REQUIREMENT | Selecting a character                                                                                                                           |
| ----------- | :---------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FA16                                                                                                                                            |
| PRIORITY    | +                                                                                                                                               |
| DESCRIPTION | At the start of the game the PlayerAudience shall choose a PlayerCharacter from a selection of possible PlayerCharacters via the voting system. |
| EXPLANATION | -                                                                                                                                               |

</br>

| REQUIREMENT | Presentation of character status values                                                  |
| ----------- | :--------------------------------------------------------------------------------------- |
| ID          | FA17                                                                                     |
| PRIORITY    | +                                                                                        |
| DESCRIPTION | The first StoryFlowDecision shall be the selection of the gender of the PlayerCharacter. |
| EXPLANATION | This shall be a StoryFlowDecision with ZeroRandomness.                                   |

</br>

| REQUIREMENT | Portrait of the PlayerCharacter                                                                                    |
| ----------- | :----------------------------------------------------------------------------------------------------------------- |
| ID          | FA18                                                                                                               |
| PRIORITY    | +                                                                                                                  |
| DESCRIPTION | QualityQuest shall display a portrait of the PlayerCharacter as part of the PlayerCharacterStatusBox all the time. |
| EXPLANATION | -                                                                                                                  |

</br>

| REQUIREMENT | Character levelling                                                                        |
| ----------- | :----------------------------------------------------------------------------------------- |
| ID          | FA19                                                                                       |
| PRIORITY    | +                                                                                          |
| DESCRIPTION | The PayerCharacter shall level up its status values based on events or StoryFlowDecisions. |
| EXPLANATION | -                                                                                          |

</br>

| REQUIREMENT | Visual presentation of PlayerCharacter status changes                             |
| ----------- | :-------------------------------------------------------------------------------- |
| ID          | FA20                                                                              |
| PRIORITY    | +                                                                                 |
| DESCRIPTION | The change of status values of the PlayerCharacter shall be highlighted visually. |
| EXPLANATION | -                                                                                 |

</br>

| REQUIREMENT | Acoustic presentation of PlayerCharacter status changes                                |
| ----------- | :------------------------------------------------------------------------------------- |
| ID          | FA21                                                                                   |
| PRIORITY    | 0                                                                                      |
| DESCRIPTION | The change of status values of the PlayerCharacter should be highlighted acoustically. |
| EXPLANATION | -                                                                                      |

</br>

| REQUIREMENT | Programming language                                            |
| ----------- | :-------------------------------------------------------------- |
| ID          | FA22                                                            |
| PRIORITY    | +                                                               |
| DESCRIPTION | QualityQuest shall be programmed in a C dialect (C, C++ or C#). |
| EXPLANATION | -                                                               |

</br>

| REQUIREMENT | Development environment                                                                                                                                                                 |
| ----------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FA23                                                                                                                                                                                    |
| PRIORITY    | +                                                                                                                                                                                       |
| DESCRIPTION | Both the source code and the build solution of QualityQuest shall be buildable in one of the following development environments: Microsoft Visual Studio, Microsoft Visual Studio Code. |
| EXPLANATION | -                                                                                                                                                                                       |

</br>

| REQUIREMENT | Operating system                                                 |
| ----------- | :--------------------------------------------------------------- |
| ID          | FA24                                                             |
| PRIORITY    | +                                                                |
| DESCRIPTION | QualityQuest shall run on Microsoft Windows 10 operating system. |
| EXPLANATION | -                                                                |

</br>

| REQUIREMENT | Usage of game engines                                                                                                                                                                                                                                                                                                                                                                                      |
| ----------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FA25                                                                                                                                                                                                                                                                                                                                                                                                       |
| PRIORITY    | -                                                                                                                                                                                                                                                                                                                                                                                                          |
| DESCRIPTION | An existing game engine may be used, if all of the following conditions apply: The license conditions of the game engine allow the source code of QualityQuest to be open source. The license conditions of the game engine allow the usage of the game engine without license fees. The license conditions of the game engine allow the usage of QualityQuest as intended by NewTec without license fees. |
| EXPLANATION | -                                                                                                                                                                                                                                                                                                                                                                                                          |

</br>

| REQUIREMENT | Usage of online voting solutions                                                                                                                                                                                                 |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FA26                                                                                                                                                                                                                             |
| PRIORITY    | -                                                                                                                                                                                                                                |
| DESCRIPTION | An existing online voting solution may be used, if the license conditions of the online voting solution allow the usage of the online voting solution in the context of QualityQuest as intended by NewTec without license fees. |
| EXPLANATION | -                                                                                                                                                                                                                                |

</br>

| REQUIREMENT | Pause Game                                                                                                                             |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FA25                                                                                                                                   |
| PRIORITY    | +                                                                                                                                      |
| DESCRIPTION | The moderator shall have the possibility to pause the game with the PauseButton. The PauseButton shall be around the lower right edge. |
| EXPLANATION | -                                                                                                                                      |

</br>

| REQUIREMENT | Connection Setup                                                                                                                                                                                                                                                                                                                                                   |
| ----------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FA26                                                                                                                                                                                                                                                                                                                                                               |
| PRIORITY    | +                                                                                                                                                                                                                                                                                                                                                                  |
| DESCRIPTION | The server shall allow as many PlayerAudience-Clients as possible to connect to the game via the network. However, the server should only allow a single Moderator-Client to connect to the server at any given time. Once the Moderator-Client established the connection to the server, the Moderator has the option to start or interrupt the game at any time. |
| EXPLANATION | -                                                                                                                                                                                                                                                                                                                                                                  |

</br>

| REQUIREMENT | Connection Timeout                                                                                                                                                                                                                                                                                 |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FA27                                                                                                                                                                                                                                                                                               |
| PRIORITY    | +                                                                                                                                                                                                                                                                                                  |
| DESCRIPTION | If the Moderator-Client does not send back an ACK within a certain time-frame after recieving the server's message, the connection to the server shall be interrupted. In this case the Moderator can either continue playing in offline mode or try to re-establish the connection to the server. |
| EXPLANATION | This serves as a fail-save, for the case that messages/ACKs could be corrupted or the connection to the server is lost.                                                                                                                                                                            |

</br>

| REQUIREMENT | Server connection loss                                                                                                                                                                                                                                |
| ----------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FA28                                                                                                                                                                                                                                                  |
| PRIORITY    | +                                                                                                                                                                                                                                                     |
| DESCRIPTION | If a Moderator-Client or PlayerAudience-Client loses its connection to the server, its Unique User Identifier (UUID) shall be stored in the system. In this case, the respective client can reconnect to the server to participate in the game again. |
| EXPLANATION | -                                                                                                                                                                                                                                                     |

</br>

| REQUIREMENT | Data exchange file format                                                   |
| ----------- | :-------------------------------------------------------------------------- |
| ID          | FA29                                                                        |
| PRIORITY    | +                                                                           |
| DESCRIPTION | The file format for data exchange between clients and server shall be JSON. |
| EXPLANATION | -                                                                           |

</br>

| REQUIREMENT | Unique User Identifier (UUID)                                                                                         |
| ----------- | :-------------------------------------------------------------------------------------------------------------------- |
| ID          | FA30                                                                                                                  |
| PRIORITY    | +                                                                                                                     |
| DESCRIPTION | Every participant shall be assigned an Unique User Identifier (UUID) based on either their IP-address or MAC-address. |
| EXPLANATION | This ensures participants can rejoin the game after leaving the game or losing the connection to the server.          |

</br>

| REQUIREMENT | Offline-Mode                                                                                                                                                                                                                                                                                                                                           |
| ----------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FA32                                                                                                                                                                                                                                                                                                                                                   |
| PRIORITY    | +                                                                                                                                                                                                                                                                                                                                                      |
| DESCRIPTION | In the event that the server is not functional, the network infrastructure slows significantly down or there being a problem with the connection between clients and server, the Moderator shall have the option to continue the game offline. This Offline-Mode must ensure a smooth transition between online and offline and shall be able to step in at any time. |
| EXPLANATION | -                                                                                                                                                                                                                                                                                                                                                      |

</br>

| REQUIREMENT | Game Engine                                   |
| ----------- | :-------------------------------------------- |
| ID          | FA32                                          |
| PRIORITY    | +                                             |
| DESCRIPTION | As a game engine the project shall use Unity. |
| EXPLANATION | -                                             |

</br>

| REQUIREMENT | Communication protocol                                                                                                                                                                                                                                                         |
| ----------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | FA33                                                                                                                                                                                                                                                                           |
| PRIORITY    | +                                                                                                                                                                                                                                                                              |
| DESCRIPTION | The communication protocol shall define clearly and well-defined how clients and server shall communicate with each other in order to accept messages. If a client increasingly does not adhere to the communication protocol, a communication protocol violation is detected. |
| EXPLANATION | This ensures that it is not easily possible to tinker with the game through an altered client.                                                                                                                                                                                 |

</br>

| REQUIREMENT | Communication protocol violation                                                                                                                  |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------ |
| ID          | FA34                                                                                                                                              |
| PRIORITY    | +                                                                                                                                                 |
| DESCRIPTION | If a client increasingly does not adhere to the communication protocol, the UUID of the participant should be excluded from the rest of the game. |
| EXPLANATION | This ensures that it is not easily possible to tinker with the game through an altered client.                                                    |


## Non-functional Requirements

This section specifies the non-functional requirements for the software system.

| REQUIREMENT | Documents to be delivered                                                                                                                                                                                                                                              |
| ----------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | QA1                                                                                                                                                                                                                                                                    |
| PRIORITY    | +                                                                                                                                                                                                                                                                      |
| DESCRIPTION | A System Specification, which comprises use case diagrams, use case descriptions and a static view of the software architecture and Software Design Specification for each software component, which describes both the static and the dynamic view shall be delivered. |
| EXPLANATION | -                                                                                                                                                                                                                                                                      |

</br>

| REQUIREMENT | In-code documentation style                                                   |
| ----------- | :---------------------------------------------------------------------------- |
| ID          | QA1                                                                           |
| PRIORITY    | +                                                                             |
| DESCRIPTION | The source code shall be documented by means of Doxygen and in Javadoc style. |
| EXPLANATION | -                                                                             |

</br>

| REQUIREMENT | In-code documentation content                                                                                                                                                                                                                   |
| ----------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | QA3                                                                                                                                                                                                                                             |
| PRIORITY    | +                                                                                                                                                                                                                                               |
| DESCRIPTION | All of the following source code elements shall be documented: Constants, variables and defines. Classes and class members. Methods and method signatures, including return values. Functions and function signatures, including return values. |
| EXPLANATION | -                                                                                                                                                                                                                                               |

</br>

| REQUIREMENT | Documentation style for diagrams                          |
| ----------- | :-------------------------------------------------------- |
| ID          | QA4                                                       |
| PRIORITY    | +                                                         |
| DESCRIPTION | All documentation diagrams shall follow the UML standard. |
| EXPLANATION | -                                                         |

</br>

| REQUIREMENT | Delivery of UML diagrams                                                          |
| ----------- | :-------------------------------------------------------------------------------- |
| ID          | QA5                                                                               |
| PRIORITY    | +                                                                                 |
| DESCRIPTION | All UML diagrams shall be delivered in the form of a diagram and a PlantUML link. |
| EXPLANATION | -                                                                                 |

</br>

| REQUIREMENT | Adherence to project Coding Styleguide                            |
| ----------- | :---------------------------------------------------------------- |
| ID          | QA6                                                               |
| PRIORITY    | 0                                                                 |
| DESCRIPTION | The software code should adhere to the Project Coding Styleguide. |
| EXPLANATION | -                                                                 |

</br>

| REQUIREMENT | Adherence to Clean Code Principles                                             |
| ----------- | :----------------------------------------------------------------------------- |
| ID          | QA7                                                                            |
| PRIORITY    | 0                                                                              |
| DESCRIPTION | The software code should adhere to Grade 1 (Red) of the Clean Code Principles. |
| EXPLANATION | -                                                                              |

</br>

| REQUIREMENT | Target PlayerAudience                                                                                         |
| ----------- | :------------------------------------------------------------------------------------------------------------ |
| ID          | QA8                                                                                                           |
| PRIORITY    | +                                                                                                             |
| DESCRIPTION | QualityQuest shall address a target audience of university students with interest in a SW engineering career. |
| EXPLANATION | -                                                                                                             |

</br>

| REQUIREMENT | Playing time                                                                              |
| ----------- | :---------------------------------------------------------------------------------------- |
| ID          | QA9                                                                                       |
| PRIORITY    | +                                                                                         |
| DESCRIPTION | The complete story of QualityQuest shall be playable in a time frame of 15 to 20 minutes. |
| EXPLANATION | -                                                                                         |

</br>

| REQUIREMENT | Playing fun                                   |
| ----------- | :-------------------------------------------- |
| ID          | QA10                                          |
| PRIORITY    | 0                                             |
| DESCRIPTION | The story of QualityQuest should be humorous. |
| EXPLANATION | -                                             |

</br>

| REQUIREMENT | Player motivation                                                                              |
| ----------- | :--------------------------------------------------------------------------------------------- |
| ID          | QA11                                                                                           |
| PRIORITY    | +                                                                                              |
| DESCRIPTION | The audience of QualityQuest shall be encouraged to follow the story by motivational elements. |
| EXPLANATION | Motivational elements could be for example rewards, achievement & level upgrades.              |

</br>

| REQUIREMENT | Deliverable artefacts                                                                          |
| ----------- | :--------------------------------------------------------------------------------------------- |
| ID          | QA12                                                                                           |
| PRIORITY    | +                                                                                              |
| DESCRIPTION | Documentation, Source Code and a running version of QualityQuest shall be delivered to NewTec. |
| EXPLANATION | -                                                                                              |

</br>

| REQUIREMENT | Type of delivery                                                                                                                                   |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------- |
| ID          | QA13                                                                                                                                               |
| PRIORITY    | +                                                                                                                                                  |
| DESCRIPTION | All deliverable artifacts shall be delivered digitally.                                                                                            |
| EXPLANATION | The delivery can be by depositing the deliverable artefacts in a public version control system. Documents should be delivered in both PDS and DOCX. |

</br>

| REQUIREMENT | Deadline                                           |
| ----------- | :------------------------------------------------- |
| ID          | QA14                                               |
| PRIORITY    | +                                                  |
| DESCRIPTION | The deadline for the final delivery is 2021-04-28. |
| EXPLANATION | -                                                  |

</br>

| REQUIREMENT | Open source development                                                                                        |
| ----------- | :------------------------------------------------------------------------------------------------------------- |
| ID          | QA15                                                                                                           |
| PRIORITY    | -                                                                                                              |
| DESCRIPTION | The Source Code of QualityQuest may be published open source under CreativeCommons CC BY-NC 4.0 license terms. |
| EXPLANATION | -                                                                                                              |





# Glossary

The glossary contains a list of specific terms and their meaning in the context of the project. They can also contain examples, a description of what they are/can be.

## Actors and roles

This section includes all actors involved in the system. Actors are people, but also third party technical systems involved in the system.

| Term        | Moderator                                                                                                                                   |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------ |
| DESCRIPTION | Observe and comment on the game. If the online voting system fails, the moderator can take over the decisions and bring the game to an end. |
| IS-A        | Human                                                                                                                                       |
| CAN-BE      | -                                                                                                                                           |
| EXAMPLE     | -                                                                                                                                           |

</span>

| Term        | PlayerAudience                                       |
| ----------- | :--------------------------------------------------- |
| DESCRIPTION | Viewers playing the game through StoryFlowDecisions. |
| IS-A        | Human                                                |
| CAN-BE      | -                                                    |
| EXAMPLE     | -                                                    |

</span>

| Term        | Participants                                                       |
| ----------- | :----------------------------------------------------------------- |
| DESCRIPTION | A person that participates and interacts with the game in any way. |
| IS-A        | Human                                                              |
| CAN-BE      | Moderator, PlayerAudience                                          |
| EXAMPLE     | -                                                                  |

</span>

| Term        | Customer                                                                                                                        |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------ |
| DESCRIPTION | The customer requires that the product meets certain requirements and is the first point of contact for questions and feedback. |
| IS-A        | Human                                                                                                                           |
| CAN-BE      | -                                                                                                                               |
| EXAMPLE     | -                                                                                                                               |

</span>

| Term        | Moderator-Client                                                                                                                                                                                                                                                                                                                                                                                              |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| DESCRIPTION | The Moderator-Client offers a graphical interface through which Moderator and PlayerAudience can interact with the game. The Moderator-Client can establish a connection to the server, to include the PlayerAudience in the game, or play the game locally without connecting to a server. In case of a server connection the Moderator-Client visualises and logically implements the output of the server. |
| IS-A        | Component                                                                                                                                                                                                                                                                                                                                                                                                     |
| CAN-BE      | -                                                                                                                                                                                                                                                                                                                                                                                                             |
| EXAMPLE     | -                                                                                                                                                                                                                                                                                                                                                                                                             |

</span>

| Term        | PlayerAudience-Client                                                                                                                                                                                                                    |
| ----------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | The PlayerAudience-Client provides a graphical interface through which PlayerAudience can interact with the server to participate in polls. The PlayerAudience-Client establishes a direct connection to the server via a web interface. |
| IS-A        | Component                                                                                                                                                                                                                                |
| CAN-BE      | -                                                                                                                                                                                                                                        |
| EXAMPLE     | -                                                                                                                                                                                                                                        |

</span>

| Term        | Client                                                                                                          |
| ----------- | :-------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | A client serves as a graphical interface through which a participant can interact with the server and the game. |
| IS-A        | Component                                                                                                       |
| CAN-BE      | Moderator-Client, PlayerAudience-Client                                                                         |
| EXAMPLE     | -                                                                                                               |

</span>

| Term        | ServerLogic                                                                                                                                                                                                             |
| ----------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | The ServerLogic serves as an interface for the communication between the Moderator-Client and the PlayerAudience-Clients. The ServerLogic contains a logical unit responsible for setting up votes and evaluating them. |
| IS-A        | Component                                                                                                                                                                                                               |
| CAN-BE      | -                                                                                                                                                                                                                       |
| EXAMPLE     | -                                                                                                                                                                                                                       |

</span>


| Term        | Server                                                                                                           |
| ----------- | :--------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | The server is a piece of computer hardware or software that hosts the ServerLogic and the PlayerAudience-Client. |
| IS-A        | -                                                                                                                |
| CAN-BE      | -                                                                                                                |
| EXAMPLE     | -                                                                                                                |

</span>

## Expertise

This section contains a collection of information regarding technical terms that are used in the context of the project.

| Term        | StoryFlowDecision                                                                                                                                                                                                                                                                                                                                 |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| DESCRIPTION | A special event within the game where the PlayerAudience needs to take a decision which influences the further StoryFlow. After a StoryFlowDecision the PlayerCharacterStatusValues can increase by several levels. The PlayerAudience decides through OnlineVoting. If the server is not available, the moderator decides the StoryFlowDecision. |
| IS-A        | -                                                                                                                                                                                                                                                                                                                                                 |
| CAN-BE      | -                                                                                                                                                                                                                                                                                                                                                 |
| EXAMPLE     | -                                                                                                                                                                                                                                                                                                                                                 |

</span>

| Term        | StoryFlow                                                                                                                                                                                                     |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| DESCRIPTION | The actual flow of the game-story. The Story contains elements where the PlayerAudience needs to make a StoryFlowDecision and depending on the decision, the StoryFlow progresses in different StoryBranches. |
| IS-A        | -                                                                                                                                                                                                             |
| CAN-BE      | -                                                                                                                                                                                                             |
| EXAMPLE     | -                                                                                                                                                                                                             |

</span>

| Term        | StoryBranch                                       |
| ----------- | :------------------------------------------------ |
| DESCRIPTION | A branch of the non-linear StoryFlow of the game. |
| IS-A        | -                                                 |
| CAN-BE      | -                                                 |
| EXAMPLE     | -                                                 |

</span>

| Term        | PlayerCharacterStatusValue                                                                                                                                                                     |
| ----------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | The PlayerCharacter has different character status values which improve or change during the course of the game. The PlayerCharacterStatusValues are displayed via a PlayerCharacterStatusBox. |
| IS-A        | -                                                                                                                                                                                              |
| CAN-BE      | Programming, Analytics, Communication, Partying                                                                                                                                                |
| EXAMPLE     | -                                                                                                                                                                                              |

</span>

| Term        | Role-playing game                                                                                                                          |
| ----------- | :----------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | QualityQuest is a role-playing game. A role-playing game is a game in which players assume the roles of characters in a fictional setting. |
| IS-A        | -                                                                                                                                          |
| CAN-BE      | QualityQuest                                                                                                                               |
| EXAMPLE     | -                                                                                                                                          |

</span>

| Term        | PlayerCharacterStatusBox                                                                                                                                                    |
| ----------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | An info box that displays the different PlayerCharacterStatusValues and the portrait of the PlayerCharacter. The box can be displayed for example in the lower left corner. |
| IS-A        | -                                                                                                                                                                           |
| CAN-BE      | -                                                                                                                                                                           |
| EXAMPLE     | -                                                                                                                                                                           |

</span>

| Term        | PlayerCharacter                                                                                                                                                                                                                                                                                                    |
| ----------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | The virtual representation of the PlayerAudience in the game. At the beginning of the game the PlayerAudience chooses a PlayerCharacter from a collection of predefined PlayerCharacters with different PlayerCharacterStatusValues. The PlayerCharacter has different PlayerCharacterStatusValues and a portrait. |
| IS-A        | -                                                                                                                                                                                                                                                                                                                  |
| CAN-BE      | -                                                                                                                                                                                                                                                                                                                  |
| EXAMPLE     | -                                                                                                                                                                                                                                                                                                                  |

</span>

| Term        | Randomness                                                                                                                                                                         |
| ----------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | The progression in the StoryFlow after a StoryFlowDecision is decided by on of the following randomness options: </br></br><ul><li>ZeroRandomness</li><li>DiceRandomness</li></ul> |
| IS-A        | -                                                                                                                                                                                  |
| CAN-BE      | ZeroRandomness, DiceRandomness                                                                                                                                                     |
| EXAMPLE     | -                                                                                                                                                                                  |

</span>

| Term        | ZeroRandomness                                                                            |
| ----------- | :---------------------------------------------------------------------------------------- |
| DESCRIPTION | The StoryFlowDecision leads directly to the next StoryBranch. The random element is zero. |
| IS-A        | Randomness                                                                                |
| CAN-BE      | -                                                                                         |
| EXAMPLE     | -                                                                                         |

</span>

| Term        | DiceRandomness                                                                                                                |
| ----------- | :---------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | After a StoryFlowDecision a six-sided dice is rolled, which initiates the further StoryFlow and selects the next StoryBranch. |
| IS-A        | Randomness                                                                                                                    |
| CAN-BE      | -                                                                                                                             |
| EXAMPLE     | -                                                                                                                             |

</span>

| Term        | Programming                                                                                                                             |
| ----------- | :-------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | A status value of the PlayerCharacter. Influences how well the character can program, for example less time is needed to program tests. |
| IS-A        | CharacterStatusValue                                                                                                                    |
| CAN-BE      | -                                                                                                                                       |
| EXAMPLE     | PlayerCharacter James has the programming-skill at 8.                                                                                   |

</span>

| Term        | Analytics                                                                                                  |
| ----------- | :--------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | Determines how well the character can analyze situations and tasks, which increases the chance of success. |
| IS-A        | CharacterStatusValue                                                                                       |
| CAN-BE      | -                                                                                                          |
| EXAMPLE     | 4 of 6 DiceRandomness possibilities lead to a positive event, because of high analytic stats.              |

</span>

| Term        | Communication                                                                                                                                                                             |
| ----------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | Communication is a StatusValue of the PlayerCharacter. Communication influences how eloquent the PlayerCharacter is, e.g how well he works in a team or how well he deals with customers. |
| IS-A        | CharacterStatusValue                                                                                                                                                                      |
| CAN-BE      | -                                                                                                                                                                                         |
| EXAMPLE     | -                                                                                                                                                                                         |

</span>

| Term        | Partying                                                                               |
| ----------- | :------------------------------------------------------------------------------------- |
| DESCRIPTION | A character with a good partying skill can make more contacts at a party more quickly. |
| IS-A        | CharacterStatusValue                                                                   |
| CAN-BE      | -                                                                                      |
| EXAMPLE     | -                                                                                      |

</span>

| Term        | Online voting                                                                                                                                                                                         |
| ----------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | The PlayerAudience selects its decisions for a StoryFlowDecision via the ServerLogic. The Connection with the ServerLogic is established over the PlayerAudience-Client by entering a QR-Code or URL. |
| IS-A        | -                                                                                                                                                                                                     |
| CAN-BE      | -                                                                                                                                                                                                     |
| EXAMPLE     | -                                                                                                                                                                                                     |

</span>

| Term        | Sidekick-Pet                                                                                                                                 |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | Can be unlocked by the PlayerAudience through a StoryFlowDecision. Helps the player in StoryFlowDecisions with helpful tips and suggestions. |
| IS-A        | -                                                                                                                                            |
| CAN-BE      | -                                                                                                                                            |
| EXAMPLE     | -                                                                                                                                            |

</span>

| Term        | Play-Time                                                                                              |
| ----------- | :----------------------------------------------------------------------------------------------------- |
| DESCRIPTION | The time it takes to finish a game. The time needed for QualityQuest should be about 15 to 20 minutes. |
| IS-A        | -                                                                                                      |
| CAN-BE      | -                                                                                                      |
| EXAMPLE     | -                                                                                                      |

</span>

| Term        | Voting-Timer                                                                                                                                                       |
| ----------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | Timer that is triggered by a StoryFlowDecision. While the timer is running the PlayerAudience has to vote. The moderator can stop the timer with the pause button. |
| IS-A        | -                                                                                                                                                                  |
| CAN-BE      | -                                                                                                                                                                  |
| EXAMPLE     | The PlayerAudience has 30 seconds to vote on a StoryFlowDecision.                                                                                                  |

</span>

| Term        | Pause-Button                                       |
| ----------- | :------------------------------------------------- |
| DESCRIPTION | Button with which the Voting-Timer can be stopped. |
| IS-A        | -                                                  |
| CAN-BE      | -                                                  |
| EXAMPLE     | -                                                  |

</span>

| Term        | Network protocol                                                                                                                                                                                                                                                         |
| ----------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | The network protocol shall define clearly and well-defined how clients and server shall communicate with each other in order to accept messages. If a client increasingly does not adhere to the communication protocol, a communication protocol violation is detected. |
| IS-A        | -                                                                                                                                                                                                                                                                        |
| CAN-BE      | -                                                                                                                                                                                                                                                                        |
| EXAMPLE     | -                                                                                                                                                                                                                                                                        |

</span>

| Term        | Online-Session                                                                                                                                                                                                       |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | The process of playing the game in Online-Mode, including a PlayerAudience. The moderator is always responsible for creating the Online-Session and the PlayerAudience joins the Online-Session via the ServerLogic. |
| IS-A        | -                                                                                                                                                                                                                    |
| CAN-BE      | -                                                                                                                                                                                                                    |
| EXAMPLE     | -                                                                                                                                                                                                                    |

</span>

| Term        | Offline-Session                                                                                                                                                           |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| DESCRIPTION | The process of playing the game in Offline-Mode, without a PlayerAudience. The moderator creates a local Offline-Session on their device and plays the game on their own. |
| IS-A        | -                                                                                                                                                                         |
| CAN-BE      | -                                                                                                                                                                         |
| EXAMPLE     | -                                                                                                                                                                         |

</span>

| Term        | Globally Unique Identifier (GUID) |
| ----------- | :--- |
| DESCRIPTION | A globally unique identifier (GUID) is a 128-bit number used to identify information in computer systems.     |
| IS-A        | -     |
| CAN-BE      | -     |
| EXAMPLE     | A random GUID could have the following form: <code>0f8fad5b-d9cb-469f-a165-70867728950e</code>     |

</span>

| Term        | Offline-Mode                                                                                                 |
| ----------- | :----------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | The Moderator-Client is not connected to a server and thus also not connected to any PlayerAudience-Clients. |
| IS-A        | -                                                                                                            |
| CAN-BE      | -                                                                                                            |
| EXAMPLE     | -                                                                                                            |

</span>

| Term        | Online-Mode                                                                                                                   |
| ----------- | :---------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | The Moderator-Client is connected to a server which enables the option for PlayerAudience-Clients to join the Online-Session. |
| IS-A        | -                                                                                                                             |
| CAN-BE      | -                                                                                                                             |
| EXAMPLE     | -                                                                                                                             |

</span>
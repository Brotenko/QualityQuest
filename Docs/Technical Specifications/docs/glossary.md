# Glossary

The glossary contains a list of specific terms and their meaning in the context of the project. They can also contain examples, a description of what they are/can be.

## Actors and Roles

This section includes all actors involved in the system. Actors are people, but also third party technical systems involved in the system.

| Term        | Moderator                                                                                                                                   |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------ |
| DESCRIPTION | Observe and comment on the game. If the online voting system fails, the moderator can take over the decisions and bring the game to an end. |
| IS-A        | Human                                                                                                                                       |
| CAN-BE      | -                                                                                                                                           |
| EXAMPLE     | -                                                                                                                                           |

</br>

| Term        | PlayerAudience                                       |
| ----------- | :--------------------------------------------------- |
| DESCRIPTION | Viewers playing the game through StoryFlowDecisions. |
| IS-A        | Human                                                |
| CAN-BE      | -                                                    |
| EXAMPLE     | -                                                    |

</br>

| Term        | Participants                                                       |
| ----------- | :----------------------------------------------------------------- |
| DESCRIPTION | A person that participants and interacts with the game in any way. |
| IS-A        | Human                                                              |
| CAN-BE      | Moderator, PlayerAudience                                          |
| EXAMPLE     | -                                                                  |

</br>

| Term        | Customer                                                                                                                        |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------ |
| DESCRIPTION | The customer requires that the product meets certain requirements and is the first point of contact for questions and feedback. |
| IS-A        | Human                                                                                                                           |
| CAN-BE      | -                                                                                                                               |
| EXAMPLE     | -                                                                                                                               |

</br>

| Term        | Moderator-Client                                                                                                                                                                                                                                                                                     |
| ----------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | The Moderator-Client offers a graphical interface through which Moderator and PlayerAudience can interact with the game. The Moderator-Client establishes a connection to the server, which receives input from the PlayerAudience and visualises and logically implements the output of the server. |
| IS-A        | Component                                                                                                                                                                                                                                                                                            |
| CAN-BE      | -                                                                                                                                                                                                                                                                                                    |
| EXAMPLE     | -                                                                                                                                                                                                                                                                                                    |

</br>

| Term        | PlayerAudience-Client                                                                                                                                                                                                                    |
| ----------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | The PlayerAudience-Client provides a graphical interface through which PlayerAudience can interact with the server to participate in polls. The PlayerAudience-Client establishes a direct connection to the server via a web interface. |
| IS-A        | Component                                                                                                                                                                                                                                |
| CAN-BE      | -                                                                                                                                                                                                                                        |
| EXAMPLE     | -                                                                                                                                                                                                                                        |

| Term        | Client                                                                                                          |
| ----------- | :-------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | A client serves as a graphical interface through which a participant can interact with the server and the game. |
| IS-A        | Component                                                                                                       |
| CAN-BE      | Moderator-Client, PlayerAudience-Client                                                                         |
| EXAMPLE     | -                                                                                                               |

</br>

| Term        | Server                                                                                                                                                    |
| ----------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | The server serves as an interface for the communication between the Moderator-Client and the PlayerAudience-Clients. The server contains the Voting-Tool. |
| IS-A        | Component                                                                                                                                                 |
| CAN-BE      | -                                                                                                                                                         |
| EXAMPLE     | -                                                                                                                                                         |

</br>

| Term        | Voting-Tool                                                                                                                                                             |
| ----------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | The voting tool is a logical unit, and part of the server, which decides which StoryBranch of the StroyFlow is chosen based on the input of the PlayerAudience-Clients. |
| IS-A        | -                                                                                                                                                                       |
| CAN-BE      | -                                                                                                                                                                       |
| EXAMPLE     | -                                                                                                                                                                       |

</br>

## Expertise

This section contains a collection of information regarding technical terms that are used in the context of the project.

| Term        | StoryFlowDecision                                                                                                                                                                                                                                                    |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | A special event within the game where the PlayerAudience needs to take a decision which influences the further StoryFlow. After a StoryFlowDecision the PlayerCharacterStatusValues can increase by several levels. The PlayerAudience decides through OnlineVoting. |
| IS-A        | -                                                                                                                                                                                                                                                                    |
| CAN-BE      | -                                                                                                                                                                                                                                                                    |
| EXAMPLE     | -                                                                                                                                                                                                                                                                    |

</br>

| Term        | StoryFlow                                                                                                                                                                                                     |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| DESCRIPTION | The actual flow of the game-story. The Story contains elements where the PlayerAudience needs to make a StoryFlowDecision and depending on the decision, the StoryFlow progresses in different StoryBranches. |
| IS-A        | -                                                                                                                                                                                                             |
| CAN-BE      | -                                                                                                                                                                                                             |
| EXAMPLE     | -                                                                                                                                                                                                             |

</br>

| Term        | StoryBranch                                       |
| ----------- | :------------------------------------------------ |
| DESCRIPTION | A branch of the non-linear StoryFlow of the game. |
| IS-A        | -                                                 |
| CAN-BE      | -                                                 |
| EXAMPLE     | -                                                 |

</br>

| Term        | PlayerCharacterStatusValue                                                                                                                                                                     |
| ----------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | The PlayerCharacter has different character status values which improve or change during the course of the game. The PlayerCharacterStatusValues are displayed via a PlayerCharacterStatusBox. |
| IS-A        | -                                                                                                                                                                                              |
| CAN-BE      | Gender, Programming, Analytics, Communication, Partying                                                                                                                                        |
| EXAMPLE     | -                                                                                                                                                                                              |

</br>

| Term        | Role-playing game                                                                                                                          |
| ----------- | :----------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | QualityQuest is a role-playing game. A role-playing game is a game in which players assume the roles of characters in a fictional setting. |
| IS-A        | -                                                                                                                                          |
| CAN-BE      | QualityQuest                                                                                                                               |
| EXAMPLE     | -                                                                                                                                          |

</br>

| Term        | PlayerCharacterStatusBox                                                                                                                                                    |
| ----------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | An info box that displays the different PlayerCharacterStatusValues and the portrait of the PlayerCharacter. The box can be displayed for example in the lower left corner. |
| IS-A        | -                                                                                                                                                                           |
| CAN-BE      | -                                                                                                                                                                           |
| EXAMPLE     | -                                                                                                                                                                           |

</br>

| Term        | PlayerCharacter                                                                                                                             |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------ |
| DESCRIPTION | The virtual representation of the PlayerAudience in the game. The PlayerCharacter has different PlayerCharacterStatusValues and a portrait. |
| IS-A        | -                                                                                                                                           |
| CAN-BE      | -                                                                                                                                           |
| EXAMPLE     | -                                                                                                                                           |

</br>

| Term        | Randomness                                                                                                                                                         |
| ----------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | The progression in the StoryFlow after a StoryFlowDecision is decided by on of the following randomness options: <br> <br>  - ZeroRandomness <br> - DiceRandomness |
| IS-A        | -                                                                                                                                                                  |
| CAN-BE      | ZeroRandomness, DiceRandomness                                                                                                                                     |
| EXAMPLE     | -                                                                                                                                                                  |

</br>

| Term        | ZeroRandomness                                                                            |
| ----------- | :---------------------------------------------------------------------------------------- |
| DESCRIPTION | The StoryFlowDecision leads directly to the next StoryBranch. The random element is zero. |
| IS-A        | Randomness                                                                                |
| CAN-BE      | -                                                                                         |
| EXAMPLE     | -                                                                                         |

</br>

| Term        | DiceRandomness                                                                                                     |
| ----------- | :----------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | After a StoryFlowDecision a die is rolled, which initiates the further StoryFlow and selects the next StoryBranch. |
| IS-A        | Randomness                                                                                                         |
| CAN-BE      | -                                                                                                                  |
| EXAMPLE     | -                                                                                                                  |

</br>

| Term        | Programming                                                                                                                              |
| ----------- | :--------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | A status value of the PlayerCharacter. Influences how well the character can programm, for example less time is needed to program tests. |
| IS-A        | CharacterStatusValue                                                                                                                     |
| CAN-BE      | -                                                                                                                                        |
| EXAMPLE     | PlayerCharacter James has the programming-skill at 8.                                                                                    |

</br>

| Term        | Analytics                                                                                                  |
| ----------- | :--------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | Determines how well the character can analyze situations and tasks, which increases the chance of success. |
| IS-A        | CharacterStatusValue                                                                                       |
| CAN-BE      | -                                                                                                          |
| EXAMPLE     | 4 of 6 DiceRandomness possibilities lead to a positive event, because of high analytic stats.              |

</br>

| Term        | Communication                                                                                                                                                                             |
| ----------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | Communication is a StatusValue of the PlayerCharacter. Communication influences how eloquent the PlayerCharacter is, e.g how well he works in a team or how well he deals with customers. |
| IS-A        | CharacterStatusValue                                                                                                                                                                      |
| CAN-BE      | -                                                                                                                                                                                         |
| EXAMPLE     | -                                                                                                                                                                                         |

</br>

| Term        | Partying                                                                               |
| ----------- | :------------------------------------------------------------------------------------- |
| DESCRIPTION | A character with a good partying skill can make more contacts at a party more quickly. |
| IS-A        | CharacterStatusValue                                                                   |
| CAN-BE      | -                                                                                      |
| EXAMPLE     | -                                                                                      |

</br>

| Term        | OnlineVoting                                                                                                                                                    |
| ----------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | The PlayerAudience selects its decisions for a StoryFLowDecision via an online voting system. The Connection with the OnlineVoting is established by a QR-Code. |
| IS-A        | -                                                                                                                                                               |
| CAN-BE      | -                                                                                                                                                               |
| EXAMPLE     | -                                                                                                                                                               |

</br>

| Term        | Sidekick-Pet                                                                                                                                 |
| ----------- | :------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | Can be unlocked by the PlayerAudience through a StoryFlowDecision. Helps the player in StoryFlowDecisions with helpful tips and suggestions. |
| IS-A        | -                                                                                                                                            |
| CAN-BE      | -                                                                                                                                            |
| EXAMPLE     | -                                                                                                                                            |

</br>

| Term        | Play-Time                                                                                              |
| ----------- | :----------------------------------------------------------------------------------------------------- |
| DESCRIPTION | The time it takes to finish a game. The time needed for QualityQuest should be about 15 to 20 minutes. |
| IS-A        |                                                                                                        |
| CAN-BE      | -                                                                                                      |
| EXAMPLE     | -                                                                                                      |

</br>

| Term        | Voting-Timer                                                                                                                                                       |
| ----------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| DESCRIPTION | Timer that is triggered by a StoryFlowDecision. While the timer is running the PlayerAudience has to vote. The moderator can stop the timer with the pause button. |
| IS-A        | -                                                                                                                                                                  |
| CAN-BE      | -                                                                                                                                                                  |
| EXAMPLE     | The PlayerAudience has 60 seconds to vote on a StoryFlowDecision.                                                                                                  |

</br>

| Term        | Pause-Button                                       |
| ----------- | :------------------------------------------------- |
| DESCRIPTION | Button with which the Voting-Timer can be stopped. |
| IS-A        | -                                                  |
| CAN-BE      | -                                                  |
| EXAMPLE     | -                                                  |

</br>
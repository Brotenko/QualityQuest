# Glossar

| Term       | Moderator                                                                    |
|------------|:-----------------------------------------------------------------------------|
| DESCRIPTION| Observe and comment on the game. If the online voting system fails, the moderator plays the game to the end, so he takes over all decisions. |
| IS-A       | Human                                                                        |
| CAN-BE     | -                                                                            |
| EXAMPLE    | -                                                                            |



| Term       | PlayerAudience                                                                     |
|------------|:-----------------------------------------------------------------------------|
| DESCRIPTION| Viewers playing the game through StoryFlowDecisions. |
| IS-A       | Human                                                                        |
| CAN-BE     | -                                                                            |
| EXAMPLE    | -                                                                            |


| Term       | Gender                                                                                      |
|------------|:--------------------------------------------------------------------------------------------|
| DESCRIPTION|  The gender of the PlayerCharacter. The PlayerAudience selects the gender at the beginning of the game using a StoryFlowDecision. The gender remains the same during a game run. |
| IS-A       | PlayerCharacterStatusValue                                                                  |
| CAN-BE     | male, female, diverse                                                                       |
| EXAMPLE    | -                                                                                           |


| Term       |   StoryFlowDecision                                                          |
|------------|:-----------------------------------------------------------------------------|
| DESCRIPTION| A special event within the game where the PlayerAudience needs to take a decision which influences the further StoryFlow. After a StoryFlowDecision the PlayerCharacterStatusValues can increase by several levels. The PlayerAudience decides through OnlineVoting.  |
| IS-A       | -                                                                            |
| CAN-BE     | -                                                                            |
| EXAMPLE    | -                                                                            |


| Term       | StoryFlow                                                                    |
|------------|:-----------------------------------------------------------------------------|
| DESCRIPTION| The actual flow of the game-story. The Story contains elements where the PlayerAudience needs to make a StoryFlowDecision, depending on the decision, the StoryFlow continuous in diffrent StoryBranches.  |
| IS-A       | -                                                                            |
| CAN-BE     | -                                                                            |
| EXAMPLE    | -                                                                            |


| Term       | StoryBranch                                                                  |
|------------|:-----------------------------------------------------------------------------|
| DESCRIPTION|  A branch of the non-linear StoryFlow of the game. |
| IS-A       | -                                                                            |
| CAN-BE     | -                                                                            |
| EXAMPLE    | -                                                                            |


| Term       | PlayerCharacterStatusValue                                                   |
|------------|:-----------------------------------------------------------------------------|
| DESCRIPTION| The PlayerCharacter has diffrent character status values which improve or change during the course of the game. The PlayerCharacterStatusValues are displayed via a PlayerCharacterStatusBox.   |
| IS-A       | -                                                                            |
| CAN-BE     | Gender, Programming, Analytics, Communication, Partying                      |
| EXAMPLE    | -                                                                            |


| Term       | Role-playing game                                                            |
|------------|:-----------------------------------------------------------------------------|
| DESCRIPTION| QualityQuest is a role-playing game. A role-playing game is a game in which players assume the roles of characters in a fictional setting.   |
| IS-A       | -                                                                            |
| CAN-BE     | QualityQuest                                                                 |
| EXAMPLE    | -                                                                            |


| Term       | PlayerCharacterStatusBox                                                     |
|------------|:-----------------------------------------------------------------------------|
| DESCRIPTION| An info box that displays the diffrent PlayerCharacterStatusValues and the portrait of the PlayerCharacter. The box can be displayed for example in the lower left corner.  |
| IS-A       | -                                                                            |
| CAN-BE     | -                                                                            |
| EXAMPLE    | -                                                                            |


| Term       | PlayerCharacter                                                              |
|------------|:-----------------------------------------------------------------------------|
| DESCRIPTION| The virtual representation of the PlayerAudience in the game. The PlayerCharacter has diffrent PlayerCharacterStatusValues and a portrait.  |
| IS-A       | -                                                                            |
| CAN-BE     | -                                                                            |
| EXAMPLE    | -                                                                            |


| Term       | Randomness                                                                   |
|------------|:-----------------------------------------------------------------------------|
| DESCRIPTION| The progression in the StoryFlow after a StoryFlowDecision is decided by on of the following randomness options: <br> <br>  - ZeroRandomness <br> - DiceRandomness |
| IS-A       | -                                                                            |
| CAN-BE     | ZeroRandomness, DiceRandomness                                               |
| EXAMPLE    | -                                                                            |



| Term       | ZeroRandomness                                                               |
|------------|:-----------------------------------------------------------------------------|
| DESCRIPTION| The StoryFlowDecision leads directly to the next StoryBranch. The random element is zero.  |
| IS-A       | Randomness                                                                   |
| CAN-BE     | -                                                                            |
| EXAMPLE    | -                                                                            |


| Term       | DiceRandomness                                                               |
|------------|:-----------------------------------------------------------------------------|
| DESCRIPTION| After a StoryFlowDecision a die is rolled, which initiates the further StoryFlow and selects the next StoryBranch.  |
| IS-A       |  Randomness                                                                  |
| CAN-BE     | -                                                                            |
| EXAMPLE    | -                                                                            |


| Term       | Programming                                                                  |
|------------|:-----------------------------------------------------------------------------|
| DESCRIPTION| A status value of the PlayerCharacter. Influences how well the character can programm, for example less time is needed to program tests.  |
| IS-A       | CharacterStatusValue                                                         |
| CAN-BE     | -                                                                            |
| EXAMPLE    | PlayerCharacter James has the programming-skill at 8.                        |

| Term       | Analytics                                                                    |
|------------|:-----------------------------------------------------------------------------|
| DESCRIPTION|   |
| IS-A       | CharacterStatusValue                                                         |
| CAN-BE     | -                                                                            |
| EXAMPLE    | -                                                                            |


| Term       | Communication                                                                |
|------------|:-----------------------------------------------------------------------------|
| DESCRIPTION| Communication is a StatusValue of the PlayerCharacter. Communication influences how eloquent the PlayerCharacter is, e.g how well he works in a team or how well he deals with customers.   |
| IS-A       | CharacterStatusValue                                                         |
| CAN-BE     | -                                                                            |
| EXAMPLE    | -                                                                            |


| Term       | Partying                                                                     |
|------------|:-----------------------------------------------------------------------------|
| DESCRIPTION|   |
| IS-A       | CharacterStatusValue                                                         |
| CAN-BE     | -                                                                            |
| EXAMPLE    | -                                                                            |


| Term       | OnlineVoting                                                                 |
|------------|:-----------------------------------------------------------------------------|
| DESCRIPTION| The PlayerAudience selects its decisions for a StoryFLowDecision via an online voting system. The Connection with the OnlineVoting is established by a QR-Code.  |
| IS-A       | -                                                                            |
| CAN-BE     | -                                                                            |
| EXAMPLE    | -                                                                            |


| Term       | Sidekick-Pet                                                                 |
|------------|:-----------------------------------------------------------------------------|
| DESCRIPTION| Can be unlocked by the PlayerAudience through a StoryFlowDecision. Helps the player in StoryFlowDecisions with helpful tips and suggestions.  |
| IS-A       | -                                                                            |
| CAN-BE     | -                                                                            |
| EXAMPLE    | -                                                                            |


| Term       | Play-Time                                                                    |
|------------|:-----------------------------------------------------------------------------|
| DESCRIPTION| The time it takes to finish a game. The time needed for QualityQuest should be about 15 to 20 minutes.   |
| IS-A       |                                                                              |
| CAN-BE     | -                                                                            |
| EXAMPLE    | -                                                                            |



| Term       | Voting-Timer                                                                 |
|------------|:-----------------------------------------------------------------------------|
| DESCRIPTION| Timer that is triggered by a StoryFlowDecision. While the timer is running the audience has to vote. The moderator can stop the timer with the pause button.  |
| IS-A       | -                                                                            |
| CAN-BE     | -                                                                            |
| EXAMPLE    | The audience has 60 seconds to vote on a StoryFlowDecision.                  |


| Term       | Pause-Button                                                                 |
|------------|:-----------------------------------------------------------------------------|
| DESCRIPTION| Button with which the Voting-Timer can be stopped.                           |
| IS-A       | -                                                                            |
| CAN-BE     | -                                                                            |
| EXAMPLE    | -                                                                            |
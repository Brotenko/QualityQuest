# Requirements 

The requirements are divided into different priorities, whose meaning should be clear from the following table:

| PRIORITY      | DESCRIPTION                                                                  |
|------------|:--------------------------------------------------------------------------------|
| +          | The requirement must be fulfilled in any case so that the product can be accepted. |
| 0          | The fulfillment of the requirement is optional and therefore not necessarily a prerequisite for acceptance, but would have a very positive effect on the product. |
| -          | The fulfilment of the requirement is also optional and therefore not a prerequisite for the acceptance. |

## Functional Requirements

This section contains all requirements that specify the basic actions of the software system.

| REQUIREMENT    | Game type                                                                    |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA1                                                                          |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | QualityQuest shall be a 2D RPG.                                   |
| EXPLANATION    | The PlayerAudience takes over the decision of a character in a fictional world of a software engineer. The PlayerAudience plays the game only through StoryFlowDecisions, for example the game plays like a movie in which the PlayerAudience takes over the decisions of the main character.                                                                          |


| REQUIREMENT    | Stand-alone game                                                             |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA2                                                                          |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | QualityQuest shall be a stand-alone game.                                    |
| EXPLANATION    | is means that the final binaries shall include everything that is needed to run the game. Any possibly needed framework needs to be included. The installation of additional frameworks or libraries is not acceptable.                                           |


| REQUIREMENT    | Game presentation                                                            |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA3                                                                          |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | QualityQuest shall be a visual-based 2D RPG.                                |
| EXPLANATION    | This means that QualityQuest shall not be a purely text-based game, but text max be an element of the visual appearance of the game.                                                                           |


| REQUIREMENT    | NewTec branding                                                              |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA4                                                                          |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | QualityQuest shall display the NewTec logo clearly visible all the time.     |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Game language                                                                |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA5                                                                          |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | The main language of QualityQuest shall be German.|
| EXPLANATION    | The majority of in-game language shall be German, but typical software engineering terms that are not German, but are commonly used in Germany do not need to be translated.                                                                                     |


| REQUIREMENT    | Game language options                                                        |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA6                                                                          |
| PRIORITY       | -                                                                            |
| DESCRIPTION    | QualityQuest should support multiple languages.                              |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Music                                                                        |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA7                                                                          |
| PRIORITY       | -                                                                            |
| DESCRIPTION    | QualityQuest may play mood music to enhance the player experience.           |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Sound effects                                                                |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA8                                                                          |
| PRIORITY       | 0                                                                            |
| DESCRIPTION    | QualityQuest should emphasize important events of the StoryFlow with sound effects. |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Game content                                                                 |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA9                                                                          |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | QualityQuest shall tell a story which mainly consists of typical elements of the software engineering profession. |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Story flow                                                                   |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA10                                                                         |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | The story of QualityQuest shall be non-linear.                               |
| EXPLANATION    | The story shall contain elements where the PlayerAudience needs to make a StoryFlowDecision. Depending on the decision, the StoryFlow shall continue in different StoryBranches.                                                                                  |


| REQUIREMENT    | Influence on the story flow by the player                                    |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA11                                                                         |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | The PlayerAudience shall influence the selection of StoryBranches by means of StoryFlowDecisions. |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Participation of a larger PlayerAudience                                           |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA12                                                                         |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | QualityQuest shall enable the PlayerAudience to let a larger audience participate in StoryFlowDecisions by means of OnlineVoting.|
| EXPLANATION    | It would be highly desirable that the OnlineVoting feature is directly embedded into the game. Other methods are acceptable depending on the circumstances.                                                                            |


| REQUIREMENT    | Random element of story flow control                                         |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA13                                                                         |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | The selection of a StoryBranch after a StoryFlowDecision shall be generated randomly.|
| EXPLANATION    | Randomness can be either determined through ZeroRandomness or DiceRandomness.   |


| REQUIREMENT    | Visualizing the randomness                                                   |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA14                                                                         |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | If the selection of a StoryBranch after a StoryFlowDecision is generated with DiceRandomness, QualityQuest shall display a clear visualization of the randomizationprocess.                                   |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Character status values                                                      |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA15                                                                         |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | The PlayerCharacter shall have different status values, which can improve or worsen during the game. The PlayerCharacter shall have all of the following status values: Gender, Programming, Analytics, Communication, Partying.           |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Selecting the gender                                                         |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA16                                                                         |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | At the start of the game the PlayerAudience shall choose the gender of the PlayerCharacter via the voting system.   |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Presentation of character status values                                      |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA17                                                                         |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | The first StoryFlowDecision shall be the selection of the gender of the PlayerCharacter.|
| EXPLANATION    | This shall be a StoryFlowDecision with ZeroRandomness.|


| REQUIREMENT    | Portrait of the PlayerCharacter                                              |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA18                                                                         |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | QualityQuest shall display a portrait of the PlayerCharacter as part of the PlayerCharacterStatusBox all the time.   |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Character levelling                                                          |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA19                                                                         |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | The PayerCharacter shall level up its status values based on events or StoryFlowDecisions.|
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Visual presentation of PlayerCharacter status changes                        |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA20                                                                         |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | The change of status values of the PlayerCharacter shall be highlighted visually.   |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Acoustic presentation of PlayerCharacter status changes                      |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA21                                                                         |
| PRIORITY       | 0                                                                            |
| DESCRIPTION    | The change of status values of the PlayerCharacter should be highlighted acoustically. |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Programming language|
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA22                                                                         |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | QualityQuest shall be programmed in a C dialect (C, C++ or C#).|
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Development environment                                                      |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA23                                                                         |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | Both the source code and the build solution of QualityQuest shall be buildable in one of the following development environments: Microsoft Visual Studio, Microsoft Visual Studio Code.                                   |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Operating system                                                             |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA24                                                                         |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | QualityQuest shall run on Microsoft Windows 10 operating system.             |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Usage of game engines                                                        |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA25                                                                         |
| PRIORITY       | -                                                                            |
| DESCRIPTION    | An existing game engine may be used, if all of the following conditions apply: The license conditions of the game engine allow the source code of QualityQuest to be open source. The license conditions of the game engine allow the usage of the game engine without license fees. The license conditions of the game engine allow the usage of QualityQuest as intended by NewTec without license fees.                                  |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Usage of online voting solutions                                             |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA26                                                                         |
| PRIORITY       | -                                                                            |
| DESCRIPTION    | An existing online voting solution may be used, if the license conditions of the online voting solution allow the usage of the online voting solution in the context of QualityQuest as intended by NewTec without license fees.             |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Pause Game                                                             |
|----------------|:-----------------------------------------------------------------------------|
| ID             | FA25                                                                         |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | The moderator shall have the possibility to pause the game with the PauseButton. The PauseButton shall be around the lower right edge.             |
| EXPLANATION    | -                                                                            |

## Non-functional Requirements

This section specifies the non-functional requirements for the software system.

| REQUIREMENT    | Documents to be delivered                                                    |
|----------------|:-----------------------------------------------------------------------------|
| ID             | QA1                                                                          |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | A System Specification, which comprises use case diagrams, use case descriptions and a static view of the software architecture and Software Design Specification for each software component which describes both the static and the dynamic view shall be delivered.            |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | In-code documentation style                                                  |
|----------------|:-----------------------------------------------------------------------------|
| ID             | QA1                                                                          |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | The source code shall be documented by means of Doxygen and in Javadoc style.             |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | In-code documentation content                                                |
|----------------|:-----------------------------------------------------------------------------|
| ID             | QA3                                                                          |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | All of the following source code elements shall be documented: Constants, variables and defines. Classes and class members. Methods and method signatures, including return values. Functions and function signatures, including return values.            |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Documentation style for diagrams                                             |
|----------------|:-----------------------------------------------------------------------------|
| ID             | QA4                                                                          |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | All documentation diagrams shall follow the UML standard.            |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Delivery of UML diagrams                                                     |
|----------------|:-----------------------------------------------------------------------------|
| ID             | QA5                                                                          |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | All UML diagrams shall be delivered in the form of a diagram and a PlantUML link.           |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Adherence to project Coding Styleguide                                       |
|----------------|:-----------------------------------------------------------------------------|
| ID             | QA6                                                                          |
| PRIORITY       | 0                                                                            |
| DESCRIPTION    | The software code should adhere to the Project Coding Styleguide.            |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Adherence to Clean Code Principles                                           |
|----------------|:-----------------------------------------------------------------------------|
| ID             | QA7                                                                          |
| PRIORITY       | 0                                                                            |
| DESCRIPTION    | The software code should adhere to Grade 1 (Red) of the Clean Code Principles.            |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Target PlayerAudience                                                              |
|----------------|:-----------------------------------------------------------------------------|
| ID             | QA8                                                                          |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | QualityQuest shall address a target audience of university students with interest in a SW engineering career.            |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Playing time                                                                 |
|----------------|:-----------------------------------------------------------------------------|
| ID             | QA9                                                                          |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | The complete story of QualityQuest shall be playable in a time frame of 15 to 20 minutes.|
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Playing fun                                                                  |
|----------------|:-----------------------------------------------------------------------------|
| ID             | QA10                                                                         |
| PRIORITY       | 0                                                                            |
| DESCRIPTION    | The story of QualityQuest should be humorous.                                |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Player motivation                                                            |
|----------------|:-----------------------------------------------------------------------------|
| ID             | QA11                                                                         |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | The audience of QualityQuest shall be encouraged to follow the story by motivational elements.            |
| EXPLANATION    | Motivational elements could be for example rewards, achievement & level upgrades.  |


| REQUIREMENT    | Deliverable artefacts                                                        |
|----------------|:-----------------------------------------------------------------------------|
| ID             | QA12                                                                         |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | Documentation, Source Code and a running version of QualityQuest shall be delivered to NewTec.            |
| EXPLANATION    | -                                                                            |


| REQUIREMENT    | Type of delivery                                                             |
|----------------|:-----------------------------------------------------------------------------|
| ID             | QA13                                                                         |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | All deliverable artifacts shall be delivered digitally.                      |
| EXPLANATION    | he delivery can be by depositing the deliverable artefacts in a public version control system. Documents should be delivered in both PDS and DOCX.|


| REQUIREMENT    | Deadline                                                                     |
|----------------|:-----------------------------------------------------------------------------|
| ID             | QA14                                                                         |
| PRIORITY       | +                                                                            |
| DESCRIPTION    | The deadline for the final delivery is 2021-04-28.                           |
| EXPLANATION    | -                                                                            |

| REQUIREMENT    | Open source development                                                      |
|----------------|:-----------------------------------------------------------------------------|
| ID             | QA15                                                                         |
| PRIORITY       | -                                                                            |
| DESCRIPTION    | The Source Code of QualityQuest may be published open source under CreativeCommons CC BY-NC 4.0 license terms.           |
| EXPLANATION    | -                                                                            |





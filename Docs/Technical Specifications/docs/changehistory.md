# Change history

The change history is a chronologically ordered list of all changes between different documentation versions. The different versions are listed together with the release date and a link to the changelog of the version.

## Modification types

| Type  | Description                              |
| :---: | :--------------------------------------- |
|   +   | An addition to a document.               |
|   -   | A removal from a document.               |
|   *   | An alteration of something pre-existing. |

</span>

## Examples

| Type  | Description                                                                                                |
| :---: | :--------------------------------------------------------------------------------------------------------- |
|   +   | **[_Section that has the change in it:_](#examples) _What has been changed:_** What exactly has been done. |
|   -   | **[_Change History:_](#examples) _Example:_** Removed an example.                                          |
|   *   | **[_Glossary:_](/glossary) _Moderator-Client:_** Clarification regarding server backup.                    |

</span>

## Table of contents

| Version | Quick Description                          | Date       | Link                                                            |
| ------- | :----------------------------------------- | :--------- | --------------------------------------------------------------- |
| 0.1.0   | Architecture design                        | 2020-10-09 | [Link](#version-010-architecture-design)                        |
| 0.1.1   | First revision of the architecture design  | 2020-10-27 | [Link](#version-011-first-revision-of-the-architecture-design)  |
| 0.1.2   | Second revision of the architecture design | TBA        | [Link](#version-012-second-revision-of-the-architecture-design) |
| 0.2.0   | Component design                           | TBA        | [Link](#version-020-component-design)                           |

</span>

## Version 0.2.0 - Component design

| Type  | Description |
| :---: | :---------- |
|+|**[_Class diagrams:_](/architectureDiagrams#Classdiagrams) _Server Class diagram:_** Added a joined class diagram for AudienceClient and Server|
| + | **[_Class diagrams:_](/architectureDiagrams#Classdiagrams) _Server class diagrams:_**  Added description          |
| * | **[_Class diagrams:_](/architectureDiagrams#Classdiagrams) _Class diagrams:_**  Updated class diagram with the new Server diagram         |


</span>

## Version 0.1.2 - Second revision of the architecture design

| Type  | Description                                                                                                                                     |
| :---: | :---------------------------------------------------------------------------------------------------------------------------------------------- |
|   *   | **_[Change history:](#table-of-contents) Table of contents:_** Fixed the links to properly work when exported to HTML/CSS.                      |
|   *   | **_[Glossary:](/glossary) DiceRandomness:_** Changed "die" to "dice" for clarification.                                                         |
|   *   | **_[Functional Requirements:](/requirements/#functional-requirements) Offline-Mode:_** Update the description to say "shall" instead of "must". |
|   *   | **_[Used Plugins:](/usedtools#used-plugins) Markdown All in One:_** Fixed typo in description.                                                  |
|   *   | **_[Used Plugins:](/usedtools#used-plugins) PlantUML:_** Fixed typo in description.                                                             |
|   +   | **_[Glossary:](/glossary#expertise) Session:_** Added new term.                                                                                 |
|   *   | **_[Change history:](#) Change history:_** Renamed change history versions descriptors.                                                         |

</span>

## Version 0.1.1 - First revision of the architecture design

This is the revised version of the Technical Specifications according to the feedback provided by the customer.

| Type  | Description                                                                                                                                                                                                                                                                                                                    |
| :---: | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
|   *   | **[_Glossary:_](/glossary) _Moderator-Client:_** Clarification regarding server backup.                                                                                                                                                                                                                                        |
|   *   | **[_Glossary:_](/glossary) _StoryFlowDecision:_** Clarification regarding server backup.                                                                                                                                                                                                                                       |
|   *   | **[_Glossary:_](/glossary) _DiceRandomness:_** Clarified that the die is six-sided.                                                                                                                                                                                                                                            |
|   *   | **[_Glossary:_](/glossary) _Voting-Timer:_** Updated voting time from "60 seconds" to "30 seconds".                                                                                                                                                                                                                            |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Stand-alone game:_** Moved to non-functional requirements.                                                                                                                                                                                             |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Game language options:_** Changed priority from "-" to "0".                                                                                                                                                                                            |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Participation of a larger PlayerAudience:_** Clarification regarding server backup.                                                                                                                                                                    |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Character status values:_** Resolved redundancy with requirement "Character levelling".                                                                                                                                                                |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Selecting a character:_** Concrete selection of characters added.                                                                                                                                                                                      |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Presentation of character status values:_** Fixed description and explanation.                                                                                                                                                                         |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Character levelling:_** Resolved redundancy with requirement "Selecting a character" and specified levelling up and down.                                                                                                                              |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Programming language:_** Moved to non-functional requirements.                                                                                                                                                                                         |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Development environment:_** Moved to non-functional requirements.                                                                                                                                                                                      |
|   -   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Usage of game engines:_** Requirement removed.                                                                                                                                                                                                         |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Usage of online voting solutions:_** Moved to non-functional requirements.                                                                                                                                                                             |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Pause Game:_** Division into functional requirements "Pause Game" and "PauseButton location".                                                                                                                                                          |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _PauseButton location:_** Added requirement.                                                                                                                                                                                                            |
|   -   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Connection Setup:_** Removed requirement.                                                                                                                                                                                                              |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Amount of supported connections:_** Added requirement.                                                                                                                                                                                                 |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Exclusive Moderator-Client connection:_** Added requirement.                                                                                                                                                                                           |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Moderator game control:_** Added requirement.                                                                                                                                                                                                          |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Connection Timeout:_** Clarification regarding communication participants and technical terminology.                                                                                                                                                   |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Server connection loss:_** Clarification regarding "system" being the "server".                                                                                                                                                                        |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Unique User Identifier (UUID):_** Specified the UUID. Changed "participant" to "client".                                                                                                                                                               |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Offline-Mode:_** Restructured the structure of the requirement. Clarified that the Moderator should "always" have the option to continue the game in Offline-Mode. Division into functional requirements "Offline-Mode" and "Offline-Mode transition". |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Offline-Mode transition:_** Added requirement.                                                                                                                                                                                                         |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Game Engine:_** Moved to non-functional requirements. Resolved redundancy with requirement "Game Engine".                                                                                                                                              |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Communication protocol:_** Moved to glossary.                                                                                                                                                                                                          |
|   *   | **[_Non-functional Requirements:_](/requirements/#non-functional-requirements) _Adherence to Clean Code Principles:_** Changed priority from "0" to "+" and updated the description accordingly.                                                                                                                               |
|   *   | **[_Non-functional Requirements:_](/requirements/#non-functional-requirements) _Type of delivery:_** Fixed typo in description. Changed "DOCX" to "HTML".                                                                                                                                                                      |
|   *   | **[_Requirements:_](/requirements) _Functional Requirements:_** Changed Id from "FA" to "FR".                                                                                                                                                                                                                                  |
|   *   | **[_Requirements:_](/requirements) _Non-functional Requirements:_** Changed Id from "QA" to "NFR".                                                                                                                                                                                                                             |
|   *   | **[_Component diagrams:_](/architectureDiagrams#component-diagrams) _Component diagrams:_** Updated notation and changed inconsistent interface labels.                                                                                                                                                                        |
|   *   | **[_Component diagrams:_](/architectureDiagrams#component-diagrams) _Component diagrams:_** Updated descripitons for all component diagrams.                                                                                                                                                                                   |
|   +   | **[_Used plugins:_](/usedtools#used-plugins) _Live Share:_** Added used plugin.                                                                                                                                                                                                                                                |
|   *   | **[_Use-case diagrams:_](/use-case-diagramme#Moderator-Client) _Moderator-Client:_** Division into "Start Application", "Play Game" and "End Application" diagrams.                                                                                                                                                            |
|   -   | **[_StoryFlow diagram:_](/storyflow) _StoryFlow diagram:_** Removed obsolete StoryFlow diagram.                                                                                                                                                                                                                                |

</span>

## Version 0.1.0 - Architecture design

This is the initial version of the Technical Specifications and thus has no changelog. The next version, which will focus on the component/detailed design, will be the first version with a changelog.

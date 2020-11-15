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
| 0.1.2   | Second revision of the architecture design | 2020-11-12 | [Link](#version-012-second-revision-of-the-architecture-design) |
| 0.2.0   | Component design                           | 2020-11-17 | [Link](#version-020-component-design)                           |

</span>

## Version 0.2.0 - Component design

| Type  | Description                                                                                                                                                                               |
| :---: | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
|   +   | **[_Class diagrams:_](/architectureDiagrams#server-class-diagrams) _Server class diagram:_** Added a joined class diagram for PlayerAudience-Client and Server, along with a description. |
|   *   | **[_Class diagrams:_](/architectureDiagrams#Classdiagrams) _Class diagrams:_**  Updated class diagram with the new Server diagram                                                         |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _Edited term "Server" to "ServerLogic":_** Was changed in FR24, FR25, FR26, FR27, FR28 and FR29.                   |
|   *   | **[_Non-functional Requirements:_](/requirements/#non-functional-requirements) _Edited term "Server" to "ServerLogic":_** Was changed in NFR20 and NFR21.                                 |
|   +   | **[_Glossary:_](/glossary) _ServerLogic:_** Added new entry.                                                                                                                              |
|   *   | **[_Glossary:_](/glossary) _Server:_** Changed description.                                                                                                                               |
|   *   | **[_Component diagrams:_](/architectureDiagrams#component-diagrams) _All component diagrams:_** Updated diagrams and descriptions.                                                        |
|   +   | **_[StoryFlow diagram:](/storyflow#detailed-storyflow-diagrams) Detailed StoryFlow Diagrams:_** Added detailed StoryFlow diagrams for every StoryFlowDecision.                            |
|   +   | **[_Network Protocol:_](/network-protocol) _Network protocol:_** Added the network protocol to the documentation.                                                                         |
|   -   | **[_Glossary:_](/glossary#actors-and-roles) _Voting-Tool:_** Removed entry.                                                                                                               |
|   *   | **[_Glossary:_](/glossary#actors-and-roles) _ServerLogic:_** Changed description to specify that the ServerLogic is responsible for realising voting.                                     |
|   +   | **[_Glossary:_](/glossary#expertise) _Online-Mode:_** Added new entry.                                                                                                                    |
|   *   | **[_Glossary:_](/glossary#expertise) _Online voting:_** Changed description and changed term from "OnlineVoting" to "Online voting".                                                      |
|   *   | **[_Glossary:_](/glossary#expertise) _Network protocol:_** Changed term from "Communication protocol" to "Network protocol".                                                              |
|   *   | **[_Glossary:_](/glossary#expertise) _Online-Session:_** Changed description to specify the session being online and changed term from "Session" to "Online-Session".                     |
|   +   | **[_Glossary:_](/glossary#expertise) _Offline-Session:_** Added new entry.                                                                                                                |
|   +   | **[_Glossary:_](/glossary#expertise) _GUID:_** Added new entry.                                                                                                                           |
|   +   | **[_Glossary:_](/glossary#expertise) _Offline-Mode:_** Added new entry.                                                                                                                   |
|   +   | **[_Used Plugins:_](/usedtools#used-plugins) _Code Spell Checker:_** Added used plugin.                                                                                                   |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR25:_** Changed the reaction time from "10 seconds" to "5 seconds".                                              |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR26:_** Changed "UUID" to "GUID".                                                                                |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR28:_** Changed "client" to "PlayerAudience-Client".                                                             |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR28:_** Changed name to "PlayerAudience-Client GUID" and changed the description accordingly.                    |
|   *   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR31:_** Changed "UUID" to "GUID".                                                                                |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR32:_** Added requirement "Unique voting option identifier".                                                     |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR33:_** Added requirement "Game-relevant ServerLogic logging".                                                   |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR34:_** Added requirement "General ServerLogic logging".                                                         |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR35:_** Added requirement "ServerLogic log deletion".                                                            |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR36:_** Added requirement "ServerLogic access-password".                                                         |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR37:_** Added requirement "Hashing of integral data".                                                            |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR38:_** Added requirement "Online-Mode".                                                                         |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR39:_** Added requirement "Online-Mode flag".                                                                    |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR40:_** Added requirement "Moderator-Client GUID".                                                               |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR41:_** Added requirement "PlayerAudience-Client count".                                                         |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR42:_** Added requirement "PlayerAudience-Client count live update".                                             |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR43:_** Added requirement "Online-Session permanence".                                                           |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR44:_** Added requirement "Switch between Moderator-Clients".                                                    |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR45:_** Added requirement "Voting-Timer stop on pause".                                                          |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR46:_** Added requirement "Communication during pauses".                                                         |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR47:_** Added requirement "PlayerAudience connection method".                                                    |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR48:_** Added requirement "PlayerAudience connection option".                                                    |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR49:_** Added requirement "Additional PlayerAudience connection options".                                        |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR50:_** Added requirement "Pause menu".                                                                          |
|   +   | **[_Functional Requirements:_](/requirements/#functional-requirements) _FR51:_** Added requirement "Pause menu contents".                                                                 |
|   +   | **[_Non-functional Requirements:_](/requirements/#non-functional-requirements) _NFR23:_** Added requirement "Communication security".                                                     |
|   +   | **[_Non-functional Requirements:_](/requirements/#non-functional-requirements) _NFR24:_** Added requirement "Postgame statistics".                                                        |
|   +   | **[_Non-functional Requirements:_](/requirements/#non-functional-requirements) _NFR25:_** Added requirement "Postgame statistic contents".                                                |                                                                                                         |
|   *   | **[_Change history:_](/changehistory) _Change history:_** Fixed typos.                                                                                                                    |
|   *   | **[_Glossary:_](/glossary) _Glossary:_** Fixed typos.                                                                                                                                     |
|   *   | **[_Requirements:_](/requirements) __Requirements:_** Fixed typos.                                                                                                                        |
|   *   | **[_Table of content:_](/toc) _Table of content:_** Fixed typos.                                                                                                                          |
|   *   | **[_Used tools, plugins and libraries:_](/usedtools) _Used tools, plugins and libraries:_** Fixed typos.                                                                                  |

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
|   *   | **[_Component diagrams:_](/architectureDiagrams#component-diagrams) _Component diagrams:_** Updated descriptions for all component diagrams.                                                                                                                                                                                   |
|   +   | **[_Used plugins:_](/usedtools#used-plugins) _Live Share:_** Added used plugin.                                                                                                                                                                                                                                                |
|   *   | **[_Use-case diagrams:_](/use-case-diagramme#Moderator-Client) _Moderator-Client:_** Division into "Start Application", "Play Game" and "End Application" diagrams.                                                                                                                                                            |
|   -   | **[_StoryFlow diagram:_](/storyflow) _StoryFlow diagram:_** Removed obsolete StoryFlow diagram.                                                                                                                                                                                                                                |
|   *   | **[_Non-functional Requirements:_](/requirements/#non-functional-requirements) _Usage of online voting solutions:"_** Updated the description to clarify that the voting tool will be made by the team.                                                                                                                        |

</span>

## Version 0.1.0 - Architecture design

This is the initial version of the Technical Specifications and thus has no changelog. The next version, which will focus on the component/detailed design, will be the first version with a changelog.

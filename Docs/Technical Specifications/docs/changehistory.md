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
|   *   | **[_Glossary:_](../glossary) _Moderator-Client:_** Clarification regarding server backup.                    |

</span>

## Table of contents

| Version | Quick Description                          | Date       | Link                                                            |
| ------- | :----------------------------------------- | :--------- | --------------------------------------------------------------- |
| 0.1.0   | Architecture design                        | 2020-10-09 | [Link](#version-010-architecture-design)                        |
| 0.1.1   | First revision of the architecture design  | 2020-10-27 | [Link](#version-011-first-revision-of-the-architecture-design)  |
| 0.1.2   | Second revision of the architecture design | 2020-11-12 | [Link](#version-012-second-revision-of-the-architecture-design) |
| 0.2.0   | Component design                           | 2020-11-17 | [Link](#version-020-component-design)                           |
| 0.2.1   | First revision of the component design     | 2020-11-26 | [Link](#version-021-first-revision-of-the-component-design)     |
| 0.2.2   | Second revision of the component design    | TBA        | [Link](#version-022-second-revision-of-the-component-design)    |

</span>

## Version 0.2.3 - Changes during Implementation

| Type  | Description                                                                                                                                     |
| :---: | :---------------------------------------------------------------------------------------------------------------------------------------------- |
|   -   | **[_Network Protocol:_](../network-protocol#messagecontainer) _MessageContainer:_** Removed Debug-Field from MessageContainer.         |
|   -   | **[_Network Protocol:_](../network-protocol#sessionopenedmessage) _SessionOpenedMessage:_** Removed field for QR-Code. |
|   -   | **[_Network Protocol:_](../network-protocol#sessionopenedmessage) _SessionClosedMessage:_** Removed field for game-statistics. |
|   -   | **[_Network Protocol:_](../network-protocol#errortypeenum) _ErrorTypeEnum:_** Removed entry for NewModerator-ErrorType. |
|   +   | **[_Network Protocol:_](../network-protocol#errortypeenum) _ErrorTypeEnum:_** Added entry and explanation for GuidAlreadyExists-ErrorType. |
|   *   | **[_Network Protocol:_](../network-protocol#errortypeenum) _ErrorTypeEnum:_** Renamed SessionDoesNotExist-ErrorType to WrongSession. |
|   -   | **[_Network Protocol:_](../network-protocol#whocansendwhichmessagetype) _MessageOverview:_** Removed entries for ServerStatus-Messages. |
|   -   | **[_Network Protocol:_](../network-protocol#detailedmessagedefinitions) _MessageOverview:_** Removed detailed definitions for ServerStatus-Messages. |
|   *   | **[_Network Protocol:_](../network-protocol#detailedmessagedefinitions) _MessageOverview:_** Changed VotingEndedMessage to include WinningOption as string, VotingResults as <Guid,int> Dictionary and TotalVotes as an int. |




</span>

## Version 0.2.2 - Second revision of the component design

| Type  | Description                                                                                                                                     |
| :---: | :---------------------------------------------------------------------------------------------------------------------------------------------- |
|   +   | **[_Used Tools:_](../usedtools#used-tools) _Visual Studio 2019:_** Added used tool.                                                               |
|   +   | **[_Used Tools:_](../usedtools#used-tools) _Adobe XD:_** Added used tool.                                                                         |
|   +   | **[_Used Libraries:_](../usedtools#used-libraries) _Microsoft.NET.Test.Sdk:_** Added used library.                                                |
|   +   | **[_Used Libraries:_](../usedtools#used-libraries) _MSTest.TestAdapter:_** Added used library.                                                    |
|   +   | **[_Used Libraries:_](../usedtools#used-libraries) _MSTest.TestFramework:_** Added used library.                                                  |
|   +   | **[_Used Libraries:_](../usedtools#used-libraries) _System.Drawing.Common:_** Added used library.                                                 |
|   +   | **[_Used Libraries:_](../usedtools#used-libraries) _QRCoder:_** Added used library.                                                               |
|   +   | **[_Used Libraries:_](../usedtools#used-libraries) _coverlet.collector:_** Added used library.                                                    |
|   *   | **[_Non-functional Requirements:_](../requirements#in-code-documentation-style) _NFR2:_** Updated description.                                    |
|   *   | **[_Non-functional Requirements:_](../requirements#adherence-to-project-coding-styleguide) _NFR6:_** Updated description.                         |
|   *   | **[_Non-functional Requirements:_](../requirements#development-environment) _NFR18:_** Updated description.                                       |
|   +   | **[_Functional Requirements:_](../requirements#sessionkey-length) _FR54:_** Added requirement "SessionKey length".                                |
|   *   | **[_Network Protocol:_](../network-protocol#messagetype-enum) _MessageType Enum:_** Renamed from "MessageTypeEnum" to "MessageType Enum".         |
|   *   | **[_Network Protocol:_](../network-protocol#errortype-enum) _ErrorType Enum:_** Renamed from "ErrorTypeEnum" to "ErrorType Enum".                 |
|   *   | **[_Network Protocol:_](../network-protocol) _Network protocol:_** Renamed occurrences of "MessageTypeEnum" to "MessageType".                     |
|   *   | **[_Network Protocol:_](../network-protocol) _Network protocol:_** Renamed occurrences of "ErrorTypeEnum" to "ErrorType".                         |
|   *   | **[_Network Protocol:_](../network-protocol) _Network protocol:_** All messages now end on "[...]Message".                                        |
|   *   | **[_Network Protocol:_](../network-protocol) _Network protocol:_** All fields and methods are now uppercase.                                      |
|   +   | **[_Network Protocol:_](../network-protocol#requestopensessionmessage) _RequestOpenSessionMessage:_** Added description for the field "Password". |
|   *   | **[_Network Protocol:_](../network-protocol#messagecontainer) _MessageContainer:_** Updated type of "CreationDate" to "DateTime".                 |
|   *   | **[_Network Protocol:_](../network-protocol#sessionopenedmessage) _SessionOpenedMessage:_** Updated type of "DirectURL" to "Uri".                 |
|   *   | **[_Network Protocol:_](../network-protocol#votingendedmessage) _VotingEndedMessage:_** Updated type of "WinningOption" to "Guid".                |
|   *   | **[_Network Protocol:_](../network-protocol#errormessage) _ErrorMessage:_** Renamed field "ErrorType" to "ErrorMessageType".                      |
|   *   | **[_Network Protocol:_](../network-protocol#errormessage) _ErrorMessage:_** Renamed field "ErrorMessage" to "ErrorMessageText".                   |

</span>

## Version 0.2.1 - First revision of the component design

| Type  | Description                                                                                                                                                                                      |
| :---: | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
|   *   | **[Quality Quest:_](../index) _Quality Quest:_** Updated all texts.                                                                                                                                |
|   *   | **[_Table of contents:_](../toc) _Table of contents:_** Extension and reordering of the table of contents.                                                                                         |
|   *   | **[_Glossary:_](../glossary#actors-and-roles) _Actors and Roles:_** Updated description.                                                                                                           |
|   *   | **[_Glossary:_](../glossary-expertise) _Expertise:_** Updated description.                                                                                                                         |
|   *   | **[_Glossary:_](../glossary) _Glossary:_** Updated all entries to have the new style.                                                                                                              |
|   *   | **[_Glossary:_](../glossary#moderator) _Moderator:_** Updated description.                                                                                                                         |
|   *   | **[_Glossary:_](../glossary#server) _Server:_** Updated description.                                                                                                                               |
|   *   | **[_Glossary:_](../glossary#moderator-client) _Moderator-Client:_** Updated description.                                                                                                           |
|   *   | **[_Glossary:_](../glossary#playeraudience-client) _PlayerAudience-Client:_** Updated description.                                                                                                 |
|   *   | **[_Glossary:_](../glossary#client) _Client:_** Updated description.                                                                                                                               |
|   *   | **[_Glossary:_](../glossary#storyflowdecision) _StoryFlowDecision:_** Updated description.                                                                                                         |
|   *   | **[_Glossary:_](../glossary#network-protocol) _Network protocol:_** Updated description.                                                                                                           |
|   *   | **[_Glossary:_](../glossary#online-mode) _Online-Mode:_** Updated description.                                                                                                                     |
|   *   | **[_Glossary:_](../glossary#offline-mode) _Offline-Mode:_** Updated description.                                                                                                                   |
|   *   | **[_Glossary:_](../glossary#online-voting) _Online voting:_** Updated description.                                                                                                                 |
|   *   | **[_Glossary:_](../glossary#pause-button) _PauseButton:_** Updated description and changed name from "Pause-Button" to "PauseButton".                                                              |
|   +   | **[_Glossary:_](../glossary#storygraph) _StoryGraph:_** Added new entry.                                                                                                                           |
|   *   | **[_Functional Requirements:_](../requirements#game) _FR1:_** Updated description.                                                                                                                 |
|   *   | **[_Functional Requirements:_](../requirements#newtec-branding) _FR3:_** Updated description.                                                                                                      |
|   *   | **[_Functional Requirements:_](../requirements#participation-of-a-larger-playeraudience) _FR11:_** Updated description and explanation.                                                            |
|   *   | **[_Functional Requirements:_](../requirements#character-status-values) _FR14:_** Updated description.                                                                                             |
|   *   | **[_Functional Requirements:_](../requirements#selecting-a-character) _FR15:_** Updated description.                                                                                               |
|   *   | **[_Functional Requirements:_](../requirements#presentation-of-character-status-values) _FR16:_** Updated description.                                                                             |
|   *   | **[_Functional Requirements:_](../requirements#character-levelling) _FR18:_** Updated description.                                                                                                 |
|   *   | **[_Functional Requirements:_](../requirements#visual-presentation-of-playercharacter-status-changes) _FR19:_** Updated description.                                                               |
|   *   | **[_Functional Requirements:_](../requirements#acoustic-presentation-of-playercharacter-status-changes) _FR20:_** Updated description.                                                             |
|   *   | **[_Functional Requirements:_](../requirements#pausebutton-location) _FR23:_** Updated description.                                                                                                |
|   *   | **[_Functional Requirements:_](../requirements#network-protocol-violation) _FR31:_** Updated description and changed name from "Communication protocol violation" to "Network protocol violation". |
|   *   | **[_Functional Requirements:_](../requirements#game-relevant-serverlogic-logging) _FR33:_** Updated description.                                                                                   |
|   *   | **[_Functional Requirements:_](../requirements#serverlogic-log-deletion) _FR35:_** Updated description.                                                                                            |
|   *   | **[_Functional Requirements:_](../requirements#moderator-client-guid) _FR40:_** Updated description.                                                                                               |
|   *   | **[_Non-functional Requirements:_](../requirements#target-audience) _NFR8:_** Changed name from "Target PlayerAudience" to "Target audience".                                                      |
|   *   | **[_Requirements:_](../requirements) _Requirements:_** Updated all entries to have the new style.                                                                                                  |
|   *   | **[_Requirements:_](../requirements) _Requirements:_** Fixed typos.                                                                                                                                |
|   *   | **[_Use-case diagrams:_](../use-case-diagrams) _PlayerAudience-Client:_** Updated description.                                                                                                     |
|   *   | **[_Use-case diagrams:_](../use-case-diagrams) _ServerLogic:_** Changed name from "Server" to "ServerLogic".                                                                                       |
|   *   | **[_Use-case diagrams:_](../use-case-diagrams) _Start application:_** Updated description and changed name from "Start Application" to "Start application".                                        |
|   *   | **[_Use-case diagrams:_](../use-case-diagrams) _Play game:_** Updated description and changed name from "Play Game" to "Play game".                                                                |
|   *   | **[_Use-case diagrams:_](../use-case-diagrams) _End application:_** Updated description and changed name from "End Application" to "End application".                                              |
|   *   | **[_Architecture diagrams:_](../architecture-diagrams) _Architecture diagrams:_** Updated description.                                                                                             |
|   *   | **[_Architecture diagrams:_](../architecture-diagrams#class-diagrams) _Class diagrams:_** Updated description.                                                                                     |
|   *   | **[_Architecture diagrams:_](../architecture-diagrams#moderator-client) _Moderator-Client:_** Updated description.                                                                                 |
|   *   | **[_Architecture diagrams:_](../architecture-diagrams#server-class-diagrams) _Server class diagrams:_** Updated description.                                                                       |
|   *   | **[_Architecture diagrams:_](../architecture-diagrams#component-overview) _Component-Overview:_** Updated description.                                                                             |
|   *   | **[_Architecture diagrams:_](../architecture-diagrams#playeraudience-client#playeraudience-client) _PlayerAudience-Client:_** Updated description.                                                 |
|   *   | **[_Architecture diagrams:_](../architecture-diagrams#component-diagrams) _Component diagrams:_** Updated description.                                                                             |
|   *   | **[_Architecture diagrams:_](../architecture-diagrams#serverlogic) _ServerLogic:_** Updated description and changed name from "Server" to "ServerLogic".                                           |
|   +   | **[_StoryFlow:_](../storyflow) _StoryFlow overview:_** Added new section.                                                                                                                          |
|   *   | **[_StoryFlow:_](../storyflow) _Detailed StoryFlow diagrams:_** Changed name from "Detailed-StoryFlow diagrams" to "Detailed StoryFlow diagrams".                                                  |
|   *   | **[_StoryFlow:_](../storyflow) _StoryFlow overview:_** Updated description.                                                                                                                        |
|   *   | **[_StoryFlow:_](../storyflow) _Detailed StoryFlow diagrams:_** Updated description.                                                                                                               |
|   *   | **[_Change history:_](../changehistory) _Change history:_** Fixed typos.                                                                                                                           |
|   +   | **[_StoryFlow:_](../storyflow#storyflowdecision-1) _StoryFlowDecision 1:_** Added description.                                                                                                     |
|   +   | **[_StoryFlow:_](../storyflow#storyflowdecision-2) _StoryFlowDecision 2:_** Added description.                                                                                                     |
|   +   | **[_StoryFlow:_](../storyflow#storyflowdecision-3) _StoryFlowDecision 3:_** Added description.                                                                                                     |
|   +   | **[_StoryFlow:_](../storyflow#storyflowdecision-3) _StoryFlowDecision 3:_** Added description.                                                                                                     |
|   +   | **[_StoryFlow:_](../storyflow#storyflowdecision-4) _StoryFlowDecision 4:_** Added description.                                                                                                     |
|   +   | **[_StoryFlow:_](../storyflow#storyflowdecision-5) _StoryFlowDecision 6:_** Added description.                                                                                                     |
|   +   | **[_StoryFlow:_](../storyflow#storyflowdecision-6) _StoryFlowDecision 6:_** Added description.                                                                                                     |
|   +   | **[_StoryFlow:_](../storyflow#storyflowdecision-7) _StoryFlowDecision 7:_** Added description.                                                                                                     |
|   +   | **[_StoryFlow:_](../storyflow#storyflowdecision-8) _StoryFlowDecision 8:_** Added description.                                                                                                     |
|   +   | **[_StoryFlow:_](../storyflow#storyflowdecision-9) _StoryFlowDecision 9:_** Added description.                                                                                                     |
|   +   | **[_StoryFlow:_](../storyflow#storyflowdecision-10) _StoryFlowDecision 10:_** Added description.                                                                                                   |
|   +   | **[_StoryFlow:_](../storyflow#storyflowdecision-11) _StoryFlowDecision 11:_** Added description.                                                                                                   |
|   +   | **[_StoryFlow:_](../storyflow#storyflowdecision-12) _StoryFlowDecision 12:_** Added description.                                                                                                   |
|   +   | **[_StoryFlow:_](../storyflow#storyflowdecision-13) _StoryFlowDecision 13:_** Added description.                                                                                                   |
|   +   | **[_StoryFlow:_](../storyflow#storyflowdecision-14) _StoryFlowDecision 14:_** Added description.                                                                                                   |
|   +   | **[_StoryFlow:_](../storyflow#storyflowdecision-15) _StoryFlowDecision 15:_** Added description.                                                                                                   |
|   *   | **[_Architecture diagrams:_](../architecture-diagrams#serverlogic) _ServerLogic:_** Updated diagram: Changed "Websocket" to "WebSocket".                                                           |
|   *   | **[_Architecture diagrams:_](../architecture-diagrams#moderator-client) _Moderator-Client:_** Updated diagram: Changed "Websocket" to "WebSocket".                                                 |
|   *   | **[_Architecture diagrams:_](../architecture-diagrams#component-overview) _Component-Overview:_** Updated diagram: Changed "Websocket" to "WebSocket".                                             |
|   *   | **[_Architecture diagrams:_](../architecture-diagrams#class-diagrams) _Class diagrams:_** Updated diagram: Changed "websocket" to "webSocket".                                                     |
|   *   | **[_Use-case diagrams:_](../use-case-diagrams#playerAudience-client) _PlayerAudience-Client:_** Updated diagram: Changed "Server" to "ServerLogic".                                                |
|   *   | **[_Use-case diagrams:_](../use-case-diagrams#Serverlogic) _ServerLogic:_** Updated diagram: Changed "Server" to "ServerLogic".                                                                    |
|   *   | **[_Use-case diagrams:_](../use-case-diagrams#moderator-client) _Moderator-Client:_** Updated diagram: Changed "Server" to "ServerLogic".                                                          |
|   *   | **[_Use-case diagrams:_](../use-case-diagrams#play-game) _Play game:_** Updated diagram: Changed "Server" to "ServerLogic".                                                                        |
|   *   | **[_Use-case diagrams:_](../use-case-diagrams#end-application) _End application:_** Updated diagram: Changed "Server" to "ServerLogic".                                                            |

</span>

## Version 0.2.0 - Component design

| Type  | Description                                                                                                                                                                                |
| :---: | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
|   +   | **[_Class diagrams:_](../architecture-diagrams#server-class-diagrams) _Server class diagram:_** Added a joined class diagram for PlayerAudience-Client and Server, along with a description. |
|   *   | **[_Class diagrams:_](../architecture-diagrams#Classdiagrams) _Class diagrams:_**  Updated class diagram with the new Server diagram                                                         |
|   *   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Edited term "Server" to "ServerLogic":_** Was changed in FR24, FR25, FR26, FR27, FR28 and FR29.                    |
|   *   | **[_Non-functional Requirements:_](../requirements/#non-functional-requirements) _Edited term "Server" to "ServerLogic":_** Was changed in NFR20 and NFR21.                                  |
|   +   | **[_Glossary:_](../glossary#serverlogic) _ServerLogic:_** Added new entry.                                                                                                                   |
|   *   | **[_Glossary:_](../glossary#server) _Server:_** Changed description.                                                                                                                         |
|   *   | **[_Component diagrams:_](../architecture-diagrams#component-diagrams) _All component diagrams:_** Updated diagrams and descriptions.                                                        |
|   +   | **_[StoryFlow diagram:](../storyflow#detailed-storyflow-diagrams) Detailed StoryFlow Diagrams:_** Added detailed StoryFlow diagrams for every StoryFlowDecision.                             |
|   +   | **[_Network Protocol:_](../network-protocol) _Network protocol:_** Added the network protocol to the documentation.                                                                          |
|   -   | **[_Glossary:_](../glossary#actors-and-roles) _Voting-Tool:_** Removed entry.                                                                                                                |
|   *   | **[_Glossary:_](../glossary#serverlogic) _ServerLogic:_** Changed description to specify that the ServerLogic is responsible for realising voting.                                           |
|   +   | **[_Glossary:_](../glossary#online-mode) _Online-Mode:_** Added new entry.                                                                                                                   |
|   *   | **[_Glossary:_](../glossary#online-voting) _Online voting:_** Changed description and changed term from "OnlineVoting" to "Online voting".                                                   |
|   *   | **[_Glossary:_](../glossary#network-protocol) _Network protocol:_** Changed term from "Communication protocol" to "Network protocol".                                                        |
|   *   | **[_Glossary:_](../glossary#online-session) _Online-Session:_** Changed description to specify the session being online and changed term from "Session" to "Online-Session".                 |
|   +   | **[_Glossary:_](../glossary#offline-session) _Offline-Session:_** Added new entry.                                                                                                           |
|   +   | **[_Glossary:_](../glossary#globally-unique-identifier-guid) _Globally Unique Identifier (GUID):_** Added new entry.                                                                         |
|   +   | **[_Glossary:_](../glossary#offline-mode) _Offline-Mode:_** Added new entry.                                                                                                                 |
|   +   | **[_Used Plugins:_](../usedtools#used-plugins) _Code Spell Checker:_** Added used plugin.                                                                                                    |
|   *   | **[_Functional Requirements:_](../requirements/#connection-timeout) _FR25:_** Changed the reaction time from "10 seconds" to "5 seconds".                                                    |
|   *   | **[_Functional Requirements:_](../requirements/#serverlogic-connection-loss) _FR26:_** Changed "UUID" to "GUID".                                                                             |
|   *   | **[_Functional Requirements:_](../requirements/#playeraudience-client-guid) _FR28:_** Changed "client" to "PlayerAudience-Client".                                                           |
|   *   | **[_Functional Requirements:_](../requirements/#playeraudience-client-guid) _FR28:_** Changed name to "PlayerAudience-Client GUID" and changed the description accordingly.                  |
|   *   | **[_Functional Requirements:_](../requirements/#network-protocol-violation) _FR31:_** Changed "UUID" to "GUID".                                                                              |
|   +   | **[_Functional Requirements:_](../requirements/#unique-voting-option-identifier) _FR32:_** Added requirement "Unique voting option identifier".                                              |
|   +   | **[_Functional Requirements:_](../requirements/#game-relevant-serverlogic-logging) _FR33:_** Added requirement "Game-relevant ServerLogic logging".                                          |
|   +   | **[_Functional Requirements:_](../requirements/#general-serverlogic-logging) _FR34:_** Added requirement "General ServerLogic logging".                                                      |
|   +   | **[_Functional Requirements:_](../requirements/#serverlogic-log-deletion) _FR35:_** Added requirement "ServerLogic log deletion".                                                            |
|   +   | **[_Functional Requirements:_](../requirements/#serverlogic-access-password) _FR36:_** Added requirement "ServerLogic access-password".                                                      |
|   +   | **[_Functional Requirements:_](../requirements/#encryption-of-integral-data) _FR37:_** Added requirement "Hashing of integral data".                                                         |
|   +   | **[_Functional Requirements:_](../requirements/#online-mode) _FR38:_** Added requirement "Online-Mode".                                                                                      |
|   +   | **[_Functional Requirements:_](../requirements/#online-mode-flag) _FR39:_** Added requirement "Online-Mode flag".                                                                            |
|   +   | **[_Functional Requirements:_](../requirements/#moderator-client-guid) _FR40:_** Added requirement "Moderator-Client GUID".                                                                  |
|   +   | **[_Functional Requirements:_](../requirements/#playeraudience-client-count) _FR41:_** Added requirement "PlayerAudience-Client count".                                                      |
|   +   | **[_Functional Requirements:_](../requirements/#playeraudience-client-count-live-updates) _FR42:_** Added requirement "PlayerAudience-Client count live update".                             |
|   +   | **[_Functional Requirements:_](../requirements/#online-session-permanence) _FR43:_** Added requirement "Online-Session permanence".                                                          |
|   +   | **[_Functional Requirements:_](../requirements/#switch-between-moderator-clients) _FR44:_** Added requirement "Switch between Moderator-Clients".                                            |
|   +   | **[_Functional Requirements:_](../requirements/#voting-timer-stop-on-pause) _FR45:_** Added requirement "Voting-Timer stop on pause".                                                        |
|   +   | **[_Functional Requirements:_](../requirements/#communication-during-pauses) _FR46:_** Added requirement "Communication during pauses".                                                      |
|   +   | **[_Functional Requirements:_](../requirements/#playeraudience-connection-method) _FR47:_** Added requirement "PlayerAudience connection method".                                            |
|   +   | **[_Functional Requirements:_](../requirements/#playeraudience-connection-option) _FR48:_** Added requirement "PlayerAudience connection option".                                            |
|   +   | **[_Functional Requirements:_](../requirements/#additional-playeraudience-connection-options) _FR49:_** Added requirement "Additional PlayerAudience connection options".                    |
|   +   | **[_Functional Requirements:_](../requirements/#pause-menu) _FR50:_** Added requirement "Pause menu".                                                                                        |
|   +   | **[_Functional Requirements:_](../requirements/#pause-menu-contentsunctional-requirements) _FR51:_** Added requirement "Pause menu contents".                                                |
|   +   | **[_Functional Requirements:_](../requirements/#vote-indexing-procedure) _FR52:_** Added requirement "Index hashing procedure".                                                              |
|   +   | **[_Functional Requirements:_](../requirements/#cryptographic-hashing-procedure) _FR53:_** Added requirement "Cryptographic hashing procedure".                                              |
|   +   | **[_Non-functional Requirements:_](../requirements/#communication-security) _NFR23:_** Added requirement "Communication security".                                                           |
|   +   | **[_Non-functional Requirements:_](../requirements/#postgame-statistics) _NFR24:_** Added requirement "Postgame statistics".                                                                 |
|   +   | **[_Non-functional Requirements:_](../requirements/#postgame-statistics-contents) _NFR25:_** Added requirement "Postgame statistic contents".                                                |  |
|   *   | **[_Change history:_](../changehistory) _Change history:_** Fixed typos.                                                                                                                     |
|   *   | **[_Glossary:_](../glossary) _Glossary:_** Fixed typos.                                                                                                                                      |
|   *   | **[_Requirements:_](../requirements) _Requirements:_** Fixed typos.                                                                                                                          |
|   *   | **[_Table of content:_](../toc) _Table of content:_** Fixed typos.                                                                                                                           |
|   *   | **[_Used tools, plugins and libraries:_](../usedtools) _Used tools, plugins and libraries:_** Fixed typos.                                                                                   |
|   *   | **[_Class diagrams:_](../architecture-diagrams#class-diagrams) _Class diagrams:_**  Updated class diagrams with GUIDs and further Adaptions.                                                 |


</span>

## Version 0.1.2 - Second revision of the architecture design

| Type  | Description                                                                                                                                        |
| :---: | :------------------------------------------------------------------------------------------------------------------------------------------------- |
|   *   | **_[Change history:](#table-of-contents) Table of contents:_** Fixed the links to properly work when exported to HTML/CSS.                         |
|   *   | **_[Glossary:](../glossary) DiceRandomness:_** Changed "die" to "dice" for clarification.                                                            |
|   *   | **_[Functional Requirements:](../requirements/#functional-requirements) Offline-Mode:_** Update the description to say "shall" instead of "must".    |
|   *   | **_[Used Plugins:](../usedtools#used-plugins) Markdown All in One:_** Fixed typo in description.                                                     |
|   *   | **_[Used Plugins:](../usedtools#used-plugins) PlantUML:_** Fixed typo in description.                                                                |
|   +   | **_[Glossary:](../glossary#expertise) Session:_** Added new term.                                                                                    |
|   *   | **_[Change history:](#) Change history:_** Renamed change history versions descriptors.                                                            |
|   *   | **_[Use-Case diagrams:](../use-case-diagrams#moderator-client) Moderator-Client:_** Updated all Moderator-Client use-case diagrams and descriptions. |

</span>

## Version 0.1.1 - First revision of the architecture design

This is the revised version of the Technical Specifications according to the feedback provided by the customer.

| Type  | Description                                                                                                                                                                                                                                                                                                                    |
| :---: | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
|   *   | **[_Glossary:_](../glossary) _Moderator-Client:_** Clarification regarding server backup.                                                                                                                                                                                                                                        |
|   *   | **[_Glossary:_](../glossary) _StoryFlowDecision:_** Clarification regarding server backup.                                                                                                                                                                                                                                       |
|   *   | **[_Glossary:_](../glossary) _DiceRandomness:_** Clarified that the die is six-sided.                                                                                                                                                                                                                                            |
|   *   | **[_Glossary:_](../glossary) _Voting-Timer:_** Updated voting time from "60 seconds" to "30 seconds".                                                                                                                                                                                                                            |
|   *   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Stand-alone game:_** Moved to non-functional requirements.                                                                                                                                                                                             |
|   *   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Game language options:_** Changed priority from "-" to "0".                                                                                                                                                                                            |
|   *   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Participation of a larger PlayerAudience:_** Clarification regarding server backup.                                                                                                                                                                    |
|   *   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Character status values:_** Resolved redundancy with requirement "Character levelling".                                                                                                                                                                |
|   *   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Selecting a character:_** Concrete selection of characters added.                                                                                                                                                                                      |
|   *   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Presentation of character status values:_** Fixed description and explanation.                                                                                                                                                                         |
|   *   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Character levelling:_** Resolved redundancy with requirement "Selecting a character" and specified levelling up and down.                                                                                                                              |
|   *   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Programming language:_** Moved to non-functional requirements.                                                                                                                                                                                         |
|   *   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Development environment:_** Moved to non-functional requirements.                                                                                                                                                                                      |
|   -   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Usage of game engines:_** Requirement removed.                                                                                                                                                                                                         |
|   *   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Usage of online voting solutions:_** Moved to non-functional requirements.                                                                                                                                                                             |
|   *   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Pause Game:_** Division into functional requirements "Pause Game" and "PauseButton location".                                                                                                                                                          |
|   +   | **[_Functional Requirements:_](../requirements/#functional-requirements) _PauseButton location:_** Added requirement.                                                                                                                                                                                                            |
|   -   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Connection Setup:_** Removed requirement.                                                                                                                                                                                                              |
|   +   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Amount of supported connections:_** Added requirement.                                                                                                                                                                                                 |
|   +   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Exclusive Moderator-Client connection:_** Added requirement.                                                                                                                                                                                           |
|   +   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Moderator game control:_** Added requirement.                                                                                                                                                                                                          |
|   *   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Connection Timeout:_** Clarification regarding communication participants and technical terminology.                                                                                                                                                   |
|   *   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Server connection loss:_** Clarification regarding "system" being the "server".                                                                                                                                                                        |
|   *   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Unique User Identifier (UUID):_** Specified the UUID. Changed "participant" to "client".                                                                                                                                                               |
|   *   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Offline-Mode:_** Restructured the structure of the requirement. Clarified that the Moderator should "always" have the option to continue the game in Offline-Mode. Division into functional requirements "Offline-Mode" and "Offline-Mode transition". |
|   +   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Offline-Mode transition:_** Added requirement.                                                                                                                                                                                                         |
|   *   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Game Engine:_** Moved to non-functional requirements. Resolved redundancy with requirement "Game Engine".                                                                                                                                              |
|   *   | **[_Functional Requirements:_](../requirements/#functional-requirements) _Communication protocol:_** Moved to glossary.                                                                                                                                                                                                          |
|   *   | **[_Non-functional Requirements:_](../requirements/#non-functional-requirements) _Adherence to Clean Code Principles:_** Changed priority from "0" to "+" and updated the description accordingly.                                                                                                                               |
|   *   | **[_Non-functional Requirements:_](../requirements/#non-functional-requirements) _Type of delivery:_** Fixed typo in description. Changed "DOCX" to "HTML".                                                                                                                                                                      |
|   *   | **[_Requirements:_](../requirements) _Functional Requirements:_** Changed Id from "FA" to "FR".                                                                                                                                                                                                                                  |
|   *   | **[_Requirements:_](../requirements) _Non-functional Requirements:_** Changed Id from "QA" to "NFR".                                                                                                                                                                                                                             |
|   *   | **[_Component diagrams:_](../architecture-diagrams#component-diagrams) _Component diagrams:_** Updated notation and changed inconsistent interface labels.                                                                                                                                                                       |
|   *   | **[_Component diagrams:_](../architecture-diagrams#component-diagrams) _Component diagrams:_** Updated descriptions for all component diagrams.                                                                                                                                                                                  |
|   +   | **[_Used plugins:_](../usedtools#used-plugins) _Live Share:_** Added used plugin.                                                                                                                                                                                                                                                |
|   *   | **[_Use-case diagrams:_](../use-case-diagrams#Moderator-Client) _Moderator-Client:_** Division into "Start Application", "Play Game" and "End Application" diagrams.                                                                                                                                                             |
|   -   | **[_StoryFlow diagram:_](../storyflow) _StoryFlow diagram:_** Removed obsolete StoryFlow diagram.                                                                                                                                                                                                                                |
|   *   | **[_Non-functional Requirements:_](../requirements/#non-functional-requirements) _Usage of online voting solutions:"_** Updated the description to clarify that the voting tool will be made by the team.                                                                                                                        |

</span>

## Version 0.1.0 - Architecture design

This is the initial version of the Technical Specifications and thus has no changelog. The next version, which will focus on the component/detailed design, will be the first version with a changelog.

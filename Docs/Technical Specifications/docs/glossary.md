# Glossary

The glossary contains a list of specific terms and their meaning in the context of the project. Furthermore they may also contain examples or descriptions of what they are or can be.

## Actors and roles

This section includes all actors and roles involved in the system. Actors can be people, but also specifically created software and hardware or third party technical systems.

<h4 style="margin-bottom: 0em"; id="moderator">Moderator</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">Observes and comments on the game. In case of a ServerLogic outage, the moderator is also responsible for taking over the game and finishing it offline.</div> |
| -------------------------------------------------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | Human                                                                                                                                                                                           |
| CAN-BE                                             | -                                                                                                                                                                                               |
| EXAMPLE                                            | -                                                                                                                                                                                               |

</span>

<h4 style="margin-bottom: 0em"; id="playeraudience">PlayerAudience</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">Viewers playing the game through StoryFlowDecisions.</div> |
| -------------------------------------------------- | :------------------------------------------------------------------------------------------ |
| IS-A                                               | Human                                                                                       |
| CAN-BE                                             | -                                                                                           |
| EXAMPLE                                            | -                                                                                           |

</span>

<h4 style="margin-bottom: 0em"; id="participants">Participants</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">A person that participates and interacts with the game in any way.</div> |
| -------------------------------------------------- | :-------------------------------------------------------------------------------------------------------- |
| IS-A                                               | Human                                                                                                     |
| CAN-BE                                             | Moderator, PlayerAudience                                                                                 |
| EXAMPLE                                            | -                                                                                                         |

</span>

<h4 style="margin-bottom: 0em"; id="customer">Customer</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">The customer requires that the product meets certain requirements and is the first point of contact for questions and feedback.</div> |
| -------------------------------------------------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | Human                                                                                                                                                                  |
| CAN-BE                                             | -                                                                                                                                                                      |
| EXAMPLE                                            | -                                                                                                                                                                      |

</span>

<h4 style="margin-bottom: 0em"; id="client">Client</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">A client serves as a graphical interface through which a participant can interact with the ServerLogic and the game.</div> |
| -------------------------------------------------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | Component                                                                                                                                                   |
| CAN-BE                                             | Moderator-Client, PlayerAudience-Client                                                                                                                     |
| EXAMPLE                                            | -                                                                                                                                                           |

</span>

<h4 style="margin-bottom: 0em"; id="moderator-client">Moderator-Client</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">The Moderator-Client offers a graphical interface through which Moderator and PlayerAudience can interact with the game. The Moderator-Client can establish a connection to the ServerLogic, to include the PlayerAudience in the game, or play the game locally without connecting to a ServerLogic. In case of a ServerLogic connection the Moderator-Client visualizes and logically implements the output of the ServerLogic.</div> |
| -------------------------------------------------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | Component                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
| CAN-BE                                             | -                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |
| EXAMPLE                                            | -                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |

</span>

<h4 style="margin-bottom: 0em"; id="playeraudience-client">PlayerAudience-Client</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">The PlayerAudience-Client provides a graphical interface through which the PlayerAudience can interact with the ServerLogic to participate in polls. The PlayerAudience-Client is directly embedded into and hosted by the ServerLogic.</div> |
| -------------------------------------------------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | Component                                                                                                                                                                                                                                                                      |
| CAN-BE                                             | -                                                                                                                                                                                                                                                                              |
| EXAMPLE                                            | -                                                                                                                                                                                                                                                                              |

</span>

<h4 style="margin-bottom: 0em"; id="serverlogic">ServerLogic</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">The ServerLogic serves as an interface for the communication between the Moderator-Client and the PlayerAudience-Clients. The ServerLogic contains a logical unit responsible for hosting the PlayerAudience-Client and evaluating its data.</div> |
| -------------------------------------------------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | Component                                                                                                                                                                                                                                                                           |
| CAN-BE                                             | -                                                                                                                                                                                                                                                                                   |
| EXAMPLE                                            | -                                                                                                                                                                                                                                                                                   |

</span>

<h4 style="margin-bottom: 0em"; id="server">Server</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">The server is a piece of physical computer hardware that hosts the ServerLogic and the PlayerAudience-Client.</div> |
| -------------------------------------------------- | :--------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | -                                                                                                                                                    |
| CAN-BE                                             | -                                                                                                                                                    |
| EXAMPLE                                            | -                                                                                                                                                    |

</span>

## Expertise

This section contains a collection of information regarding technical terms that are used in the context of the project.

<h4 style="margin-bottom: 0em"; id="storyflowdecision">StoryFlowDecision</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">A special event within the game where the PlayerAudience needs to take a decision through online voting which influences the further StoryFlow. After a StoryFlowDecision the PlayerCharacterStatusValues may increase or decrease by several levels. If the ServerLogic is not available, the moderator decides the StoryFlowDecision.</div> |
| -------------------------------------------------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | -                                                                                                                                                                                                                                                                                                                                                                              |
| CAN-BE                                             | -                                                                                                                                                                                                                                                                                                                                                                              |
| EXAMPLE                                            | -                                                                                                                                                                                                                                                                                                                                                                              |

</span>

<h4 style="margin-bottom: 0em"; id="storyflow">StoryFlow</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">The actual flow of the game-story. The Story contains elements where the PlayerAudience needs to make a StoryFlowDecision and depending on the decision, the StoryFlow progresses in different StoryBranches.</div> |
| -------------------------------------------------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | -                                                                                                                                                                                                                                                    |
| CAN-BE                                             | -                                                                                                                                                                                                                                                    |
| EXAMPLE                                            | -                                                                                                                                                                                                                                                    |

</span>

<h4 style="margin-bottom: 0em"; id="storybranch">StoryBranch</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">A branch of the non-linear StoryFlow of the game.</div> |
| -------------------------------------------------- | :--------------------------------------------------------------------------------------- |
| IS-A                                               | -                                                                                        |
| CAN-BE                                             | -                                                                                        |
| EXAMPLE                                            | -                                                                                        |

</span>

<h4 style="margin-bottom: 0em"; id="playercharacterstatusvalue">PlayerCharacterStatusValue</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">The PlayerCharacter has different character status values which improve or change during the course of the game. The PlayerCharacterStatusValues are displayed via a PlayerCharacterStatusBox.</div> |
| -------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| IS-A                                               | -                                                                                                                                                                                                                                     |
| CAN-BE                                             | Programming, Analytics, Communication, Partying                                                                                                                                                                                       |
| EXAMPLE                                            | -                                                                                                                                                                                                                                     |

</span>

<h4 style="margin-bottom: 0em"; id="role-playing-game">Role-playing game</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">QualityQuest is a role-playing game. A role-playing game is a game in which players assume the roles of characters in a fictional setting.</div> |
| -------------------------------------------------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | -                                                                                                                                                                                 |
| CAN-BE                                             | QualityQuest                                                                                                                                                                      |
| EXAMPLE                                            | -                                                                                                                                                                                 |

</span>

<h4 style="margin-bottom: 0em"; id="playercharacterstatusbox">PlayerCharacterStatusBox</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">An info box that displays the different PlayerCharacterStatusValues and the portrait of the PlayerCharacter. The box can be displayed for example in the lower left corner.</div> |
| -------------------------------------------------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | -                                                                                                                                                                                                                  |
| CAN-BE                                             | -                                                                                                                                                                                                                  |
| EXAMPLE                                            | -                                                                                                                                                                                                                  |

</span>

<h4 style="margin-bottom: 0em"; id="playercharacter">PlayerCharacter</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">The virtual representation of the PlayerAudience in the game. At the beginning of the game the PlayerAudience chooses a PlayerCharacter from a collection of predefined PlayerCharacters with different PlayerCharacterStatusValues. The PlayerCharacter has different PlayerCharacterStatusValues and a portrait.</div> |
| -------------------------------------------------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | -                                                                                                                                                                                                                                                                                                                                                         |
| CAN-BE                                             | -                                                                                                                                                                                                                                                                                                                                                         |
| EXAMPLE                                            | -                                                                                                                                                                                                                                                                                                                                                         |

</span>

<h4 style="margin-bottom: 0em"; id="randomness">Randomness</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">The progression in the StoryFlow after a StoryFlowDecision is decided by on of the following randomness options: </br></br><ul><li>ZeroRandomness</li><li>DiceRandomness</li></ul></div> |
| -------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| IS-A                                               | -                                                                                                                                                                                                                         |
| CAN-BE                                             | ZeroRandomness, DiceRandomness                                                                                                                                                                                            |
| EXAMPLE                                            | -                                                                                                                                                                                                                         |

</span>

<h4 style="margin-bottom: 0em"; id="zerorandomness">ZeroRandomness</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">The StoryFlowDecision leads directly to the next StoryBranch. The random element is zero.</div> |
| -------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | Randomness                                                                                                                       |
| CAN-BE                                             | -                                                                                                                                |
| EXAMPLE                                            | -                                                                                                                                |

</span>

<h4 style="margin-bottom: 0em"; id="dicerandomness">DiceRandomness</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">After a StoryFlowDecision a six-sided dice is rolled, which initiates the further StoryFlow and selects the next StoryBranch.</div> |
| -------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | Randomness                                                                                                                                                           |
| CAN-BE                                             | -                                                                                                                                                                    |
| EXAMPLE                                            | -                                                                                                                                                                    |

</span>

<h4 style="margin-bottom: 0em"; id="programming">Programming</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">A status value of the PlayerCharacter. Influences how well the character can program, for example less time is needed to program tests.</div> |
| -------------------------------------------------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | CharacterStatusValue                                                                                                                                                           |
| CAN-BE                                             | -                                                                                                                                                                              |
| EXAMPLE                                            | PlayerCharacter James has the programming-skill at 8.                                                                                                                          |

</span>

<h4 style="margin-bottom: 0em"; id="analytics">Analytics</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">Determines how well the character can analyze situations and tasks, which increases the chance of success.</div> |
| -------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------ |
| IS-A                                               | CharacterStatusValue                                                                                                                              |
| CAN-BE                                             | -                                                                                                                                                 |
| EXAMPLE                                            | 4 of 6 DiceRandomness possibilities lead to a positive event, because of high analytic stats.                                                     |

</span>

<h4 style="margin-bottom: 0em"; id="communication">Communication</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">Communication is a StatusValue of the PlayerCharacter. Communication influences how eloquent the PlayerCharacter is, e.g how well he works in a team or how well he deals with customers.</div> |
| -------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | CharacterStatusValue                                                                                                                                                                                                             |
| CAN-BE                                             | -                                                                                                                                                                                                                                |
| EXAMPLE                                            | -                                                                                                                                                                                                                                |

</span>

<h4 style="margin-bottom: 0em"; id="partying">Partying</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">A character with a good partying skill can make more contacts at a party more quickly.</div> |
| -------------------------------------------------- | :---------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | CharacterStatusValue                                                                                                          |
| CAN-BE                                             | -                                                                                                                             |
| EXAMPLE                                            | -                                                                                                                             |

</span>

<h4 style="margin-bottom: 0em"; id="online-voting">Online voting</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">The PlayerAudience selects its decisions for a StoryFlowDecision via the PlayerAudience-Client through the ServerLogic. The connection with the ServerLogic is established over the PlayerAudience-Client by entering a QR-Code or URL.</div> |
| -------------------------------------------------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | -                                                                                                                                                                                                                                                                              |
| CAN-BE                                             | -                                                                                                                                                                                                                                                                              |
| EXAMPLE                                            | -                                                                                                                                                                                                                                                                              |

</span>

<h4 style="margin-bottom: 0em"; id="play-time">Play-Time</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">The time it takes to finish a game. The time needed for QualityQuest should be about 15 to 20 minutes.</div> |
| -------------------------------------------------- | :-------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | -                                                                                                                                             |
| CAN-BE                                             | -                                                                                                                                             |
| EXAMPLE                                            | -                                                                                                                                             |

</span>

<h4 style="margin-bottom: 0em"; id="voting-timer">Voting-Timer</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">Timer that is triggered by a StoryFlowDecision. While the timer is running the PlayerAudience has to vote. The moderator can set the voting time as desired and stop the timer with the pause button, at any given time.</div> |
| -------------------------------------------------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | -                                                                                                                                                                                                                                                               |
| CAN-BE                                             | -                                                                                                                                                                                                                                                               |
| EXAMPLE                                            | The PlayerAudience has 30 seconds to vote on a StoryFlowDecision.                                                                                                                                                                                               |

</span>

<h4 style="margin-bottom: 0em"; id="pausebutton">PauseButton</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">Button with which the game, and the Voting-Timer, can be paused.</div> |
| -------------------------------------------------- | :------------------------------------------------------------------------------------------------------ |
| IS-A                                               | -                                                                                                       |
| CAN-BE                                             | -                                                                                                       |
| EXAMPLE                                            | -                                                                                                       |

</span>

<h4 style="margin-bottom: 0em"; id="network-protocol">Network protocol</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">The network protocol shall define clearly and well-defined how clients and ServerLogic shall communicate with each other in order to accept messages. If a client increasingly does not adhere to the communication protocol, a communication protocol violation is detected.</div> |
| -------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | -                                                                                                                                                                                                                                                                                                                    |
| CAN-BE                                             | -                                                                                                                                                                                                                                                                                                                    |
| EXAMPLE                                            | -                                                                                                                                                                                                                                                                                                                    |

</span>

<h4 style="margin-bottom: 0em"; id="online-session">Online-Session</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">The process of playing the game in Online-Mode, including a PlayerAudience. The moderator is always responsible for creating the Online-Session and the PlayerAudience joins the Online-Session via the ServerLogic.</div> |
| -------------------------------------------------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | -                                                                                                                                                                                                                                                           |
| CAN-BE                                             | -                                                                                                                                                                                                                                                           |
| EXAMPLE                                            | -                                                                                                                                                                                                                                                           |

</span>

<h4 style="margin-bottom: 0em"; id="offline-session">Offline-Session</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">The process of playing the game in Offline-Mode, without a PlayerAudience. The moderator creates a local Offline-Session on their device and plays the game on their own.</div> |
| -------------------------------------------------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | -                                                                                                                                                                                                                |
| CAN-BE                                             | -                                                                                                                                                                                                                |
| EXAMPLE                                            | -                                                                                                                                                                                                                |

</span>

<h4 style="margin-bottom: 0em"; id="globally-unique-identifier-guid">Globally Unique Identifier (GUID)</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">A globally unique identifier (GUID) is a 128-bit number used to identify information in computer systems.</div> |
| -------------------------------------------------- | :----------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | -                                                                                                                                                |
| CAN-BE                                             | -                                                                                                                                                |
| EXAMPLE                                            | A random GUID could have the following form: <code>0f8fad5b-d9cb-469f-a165-70867728950e</code>                                                   |

</span>

<h4 style="margin-bottom: 0em"; id="offline-mode">Offline-Mode</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">The Moderator-Client is not connected to a ServerLogic and thus also not connected to any PlayerAudience-Clients. Alternatively, a connection may exist that is intentionally ignored in order to play offline. </div> |
| -------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| IS-A                                               | -                                                                                                                                                                                                                                                       |
| CAN-BE                                             | -                                                                                                                                                                                                                                                       |
| EXAMPLE                                            | -                                                                                                                                                                                                                                                       |

</span>

<h4 style="margin-bottom: 0em"; id="online-mode">Online-Mode</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">The Moderator-Client is connected to a ServerLogic which enables the option for PlayerAudience-Clients to join the Online-Session.</div> |
| -------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| IS-A                                               | -                                                                                                                                                                         |
| CAN-BE                                             | -                                                                                                                                                                         |
| EXAMPLE                                            | -                                                                                                                                                                         |

</span>

<h4 style="margin-bottom: 0em"; id="storygraph">StoryGraph</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">A graph-based representation of the StoryFlow and the different StoryFlowDecisions that can occur during the course of the game and can lead to the different StoryBranches.</div> |
| -------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| IS-A                                               | -                                                                                                                                                                                                                   |
| CAN-BE                                             | -                                                                                                                                                                                                                   |
| EXAMPLE                                            | -                                                                                                                                                                                                                   |

</span>

<h4 style="margin-bottom: 0em"; id="visual-novel">Visual Novel</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">An interactive narrative story-telling medium often characterized by music, sound effects, and art of the setting and/or characters that are in the present scene. They sometimes but not always have non-linear plots which are driven by player choices, and sometimes also have more traditional gameplay elements interspersed. </div> |
| -------------------------------------------------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | Role-playing game                                                                                                                                                                                                                                                                                                                                                           |
| CAN-BE                                             | Quality Quest                                                                                                                                                                                                                                                                                                                                                               |
| EXAMPLE                                            | -                                                                                                                                                                                                                                                                                                                                                                           |

</span>

<h4 style="margin-bottom: 0em"; id="asp-net">ASP.NET</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">ASP.NET is an open-source, server-side web-application framework designed for web development to produce dynamic web pages.</div> |
| -------------------------------------------------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | -                                                                                                                                                                  |
| CAN-BE                                             | -                                                                                                                                                                  |
| EXAMPLE                                            | -                                                                                                                                                                  |

</span>

<h4 style="margin-bottom: 0em"; id="signalr">SignalR</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">SignalR is a free and open-source software library for Microsoft ASP.NET that allows server code to send asynchronous notifications to client-side web applications.</div> |
| -------------------------------------------------- | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | -                                                                                                                                                                                                           |
| CAN-BE                                             | -                                                                                                                                                                                                           |
| EXAMPLE                                            | -                                                                                                                                                                                                           |

</span>

<h4 style="margin-bottom: 0em"; id="signalr-hub">SignalR-Hub</h4>
| <div style="font-weight: normal">DESCRIPTION</div> | <div style="font-weight: normal">The SignalR Hubs API enables you to call methods on connected clients from the server. In the server code, you define methods that are called by client. In the client code, you define methods that are called from the server. SignalR takes care of everything behind the scenes that makes real-time client-to-server and server-to-client communications possible.</div> |
| -------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| IS-A                                               | -                                                                                                                                                                                                                                                                                                                                                                                                              |
| CAN-BE                                             | -                                                                                                                                                                                                                                                                                                                                                                                                              |
| EXAMPLE                                            | -                                                                                                                                                                                                                                                                                                                                                                                                              |

</span>
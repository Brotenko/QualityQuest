# Quality Quest

## Prerequisites

This software includes a web-based client which will be hosted directly by the software in form of a website. This website will inevitably need to save game-specific data, requiring the host to provide legal texts regarding `cookies` and general `terms of service` (see [GDPR](https://gdpr.eu/)). Moreover the host might also need to provide a `Legal Notice` or `About` page, depending on the hosts location. 
We provide placeholders for these purposes which can be replaced, or removed, depending on your needs. Said placeholders are located as following:
- **Cookies and Terms of Service:** `../ServerLogic/PAClient/Shared/_Layout.cshtml`
- **Terms of Service:** `../ServerLogic/PAClient/TermsOfService.cshtml`
- **Privacy:** `../ServerLogic/PAClient/Privacy.cshtml`
- **About:** `../ServerLogic/PAClient/About.cshtml`

**We highly recommend setting everything up according to legal requirements, regardless of the location of the host and the userbase, to circumvent possible future issues with the law and law enforcement.**


## Setup

Make sure that Python3 is installed and Docker is running. Place a valid .pfx certificate in `QualityQuest/ServerLogic`. Run `install_and_run.py` with administrator privileges. The script offers you three options to start QualityQuest: 
1. (Install) Must have been run once to enable the other two options. Creates a new Docker image, the requested parameters are used to start a Docker container and run QualityQuest directly afterwards.
2. (Configurate) Start QualityQuest from an existing Docker image and change parameters such as port, URL and certificate. Resets parameters such as LogLevel and password from previous sessions.
3. (Continue) Start QualityQuest and apply settings from previous sessions.


## Maintenance / Known issues
In general it is sufficient to use option one in the script only once to generate a docker image. From there on, options two and three are perfectly sufficient for normal operation.

Possible issues and possible fixes:
* Website or WebSocket not available?
  - Specify 'https://' explicitly for the website.
  - Check whether the sockets on your device are enabled or occupied by another service. 
* `[Warn] Failed to Authenticate System.AggregateException: One or more errors occurred.`
  - This Exception is thrown when a PlayerAudience-Client tries to connect using HTTP, TLS 1.1 or other "outdated" protocols.
  - The Exception is non-critical and only warns the host of a connection that is not using modern standards. SignalR does support these connections anyway by default.
* The website looks broken or is not working correctly!
  - A big part of the website's communication is handled through JavaScript - Any add-ons or extensions blocking JavaScript need to be disabled to play the game.

If you have any problems, please write us an issue, we will be happy to help you.


## How to use the Server

The server-shell contains the following commands:
```
  - password       Show or set the server-access password
  - start          Start the server
  - stop           Stop the server
  - sess         	 Shows the currently active sessions
  - log          	 Show the logs
  - exit         	 Exit the shell

Use 'command --help' to read about a specific command.
```
The settings made in the shell persist beyond the end of the container and are thus also available after a restart via the script if option three is selected. The same applies to the log file, which can be found in `/Saves`, although unlike the parameters, this is not reset with any of the three options. This should be done, if desired, via the server shell with `log --clear`.


## How to use the PlayerAudience-Client

The PlayerAudience-Client is designed to be easy to be used by the audience, as they only need to enter the `SessionKey` provided by the Moderator-Client into the provided textfield. Alternatively the audience may scan the QR-Code provided by the Moderator-Client to skip entering the `SessionKey` altogether.

After the joining the `Session` the audience can lay back and wait for the next poll to start, where they can vote by simply pressing one of the buttons appearing on their screen.


## How to use the Moderator-Client
  
The Moderator-Client is a simple to use client written in C# using the Unity engine. For an optimal experience we suggest playing the game with a resolution of 1080p/FULL-HD or higher! 

### Options menu:

The options menu gives the user to seperately change audio options, choose a different resolution and switch between windowed mode and fullscreen, using simple buttons, sliders and dropdown menus.

### Online mode:

After clicking on `Online spielen` the user is prompted to enter an IP, a port and a password, which are the informations the user had to enter when setting up the server using the handy Python-Setup-Script.

### Pause menu:

While online, the user may press the button in the upper right-hand corner to pause the game at any given timer - Effectively stopping the ongoing Online-Session and thus stopping the Voting-Timer.

### In-game menu:

The user may presst he button in the upper left-hand corner, while in-game, to open up an option menu at any given time. Here the user may adjust the game-options, return to the main menu or close down the game completely.


## Credits

### Menu Theme:
"Easy Lemon" by Kevin MacLeod (incompetech.com) </br>
Licensed under Creative Commons: By Attribution 3.0 </br>
https://creativecommons.org/licenses/by/3.0/ </br>

### Standard Background Theme 1:
"Wallpaper" by Kevin MacLeod (incompetech.com) </br>
Licensed under Creative Commons: By Attribution 3.0 </br>
https://creativecommons.org/licenses/by/3.0/ </br>

### Standard Background Theme 2:
"Shades of Spring" by Kevin MacLeod (incompetech.com) </br>
Licensed under Creative Commons: By Attribution 3.0 </br>
https://creativecommons.org/licenses/by/3.0/ </br>

### Party Background Theme 1:
"Blue Ska" by Kevin MacLeod (incompetech.com) </br>
Licensed under Creative Commons: By Attribution 3.0 </br>
https://creativecommons.org/licenses/by/3.0/ </br>

### Party Background Theme 2:
"Upbeat Forever" by Kevin MacLeod (incompetech.com) </br>
Licensed under Creative Commons: By Attribution 3.0 </br>
https://creativecommons.org/licenses/by/3.0/ </br>

### Hawaii Background Theme:
"Beach Party" by Kevin MacLeod (incompetech.com) </br>
Licensed under Creative Commons: By Attribution 3.0 </br>
https://creativecommons.org/licenses/by/3.0/ </br>

### Hawaii Background Theme:
"Beach Party" by Kevin MacLeod (incompetech.com) </br>
Licensed under Creative Commons: By Attribution 3.0 </br>
https://creativecommons.org/licenses/by/3.0/ </br>

### Internship Background Noise:
"Office ambience" by Nightwatcher98 (freesound.org) </br>
Licensed under Creative Commons: By Attribution-NonCommercial 3.0 </br>
https://creativecommons.org/licenses/by-nc/3.0/ </br>

### Office Background Noise:
"computer keyboard typing" by FREE_SOUND_ENTERTAINMENT (freesound.org) </br>
Licensed under Creative Commons: By Attribution 3.0 </br>
https://creativecommons.org/licenses/by/3.0/ </br>

### Party Background Noise:
"Party Crowd 1" by Kolezan (freesound.org) </br>
Licensed under Creative Commons: By Attribution 3.0 </br>
https://creativecommons.org/licenses/by/3.0/ </br>

### Hawaii Background Noise:
"Beach Ambience" by bone666138 (freesound.org) </br>
Licensed under Creative Commons: By Attribution 3.0 </br>
https://creativecommons.org/licenses/by/3.0/ </br>

### Button Click Sound Effect:
"Button Click 3" by Mellau (freesound.org) </br>
Licensed under Creative Commons: By Attribution-NonCommercial 3.0 </br>
https://creativecommons.org/licenses/by-nc/3.0/ </br>

### Skillchange Sound Effect:
"Electro win sound" by Mativve (freesound.org) </br>
Licensed under Creative Commons: By Attribution 3.0 </br>
https://creativecommons.org/licenses/by/3.0/ </br>


## Licensing

This work is licensed under a <a rel="license" href="http://creativecommons.org/licenses/by-nc/4.0/">Creative Commons Attribution-NonCommercial 4.0 International License</a>. </br>
<a rel="license" href="http://creativecommons.org/licenses/by-nc/4.0/"><img alt="Creative Commons License" style="border-width:0" src="https://i.creativecommons.org/l/by-nc/4.0/88x31.png" /></a>

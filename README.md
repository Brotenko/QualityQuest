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
  
@elias


## Credits

@alex

```
### Example for you - Please delete afterwards:

- For everything related to Kevin MacLeod you can use this template by replacing the "Title" and the place where the piece was used. Just make sure to credit him for every piece independently. 
- For the soundeffects you gotta look at who made it and the CC license they have on the SFX (I put those next to the SFX download links I provided you with). Same template so to say - You just have to change more
- "CC0" means no credits required, "CC BY" means you gotta credit them here.
```

#### Titlescreen Theme:
"Title" by Kevin MacLeod (incompetech.com)
Licensed under Creative Commons: By Attribution 3.0
http://creativecommons.org/licenses/by/3.0/


## Licensing

This work is licensed under a <a rel="license" href="http://creativecommons.org/licenses/by-nc/4.0/">Creative Commons Attribution-NonCommercial 4.0 International License</a>. </br>
<a rel="license" href="http://creativecommons.org/licenses/by-nc/4.0/"><img alt="Creative Commons License" style="border-width:0" src="https://i.creativecommons.org/l/by-nc/4.0/88x31.png" /></a>

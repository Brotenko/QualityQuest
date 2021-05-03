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
1. Must have been run once to enable the other two options. Creates a new Docker image, the requested parameters are used to start a Docker container and run QualityQuest directly afterwards.
2. Start QualityQuest from an existing Docker image and change parameters such as port, URL and certificate. Resets parameters such as LogLevel and password from previous sessions.
3. Start QualityQuest and apply settings from previous sessions.


## How to use the Server

_Do your thing again Elias - Oh and we should probably put the `help` thingy from the shell here once we are 100% certain the commands won't change anymore_

## Maintenance / Known issues

_Idk, is there stuff that could easily go wrong and some "easy" fixes or whatever? Couldn't hurt to list them here then!_

## Licensing

This work is licensed under a <a rel="license" href="http://creativecommons.org/licenses/by-nc/4.0/">Creative Commons Attribution-NonCommercial 4.0 International License</a>. </br>
<a rel="license" href="http://creativecommons.org/licenses/by-nc/4.0/"><img alt="Creative Commons License" style="border-width:0" src="https://i.creativecommons.org/l/by-nc/4.0/88x31.png" /></a>

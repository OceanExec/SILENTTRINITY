![](https://github.com/byt3bl33d3r/SILENTTRINITY/workflows/SILENTTRINITY%20Tests/badge.svg)

# SILENTTRINITY

<p align="center">
  <img src="https://user-images.githubusercontent.com/5151193/45964397-e462e280-bfe2-11e8-88a7-69212e0f0355.png" width=400 height=400 alt="ST"/>
</p>

SILENTTRINITY is modern, asynchronous, multiplayer & multiserver C2/post-exploitation framework powered by Python 3 and .NETs DLR. It's the culmination of an extensive amount of research into using embedded third-party .NET scripting languages to dynamically call .NET API's, a technique the author coined as BYOI (Bring Your Own Interpreter). The aim of this tool and the BYOI concept is to shift the paradigm back to PowerShell style like attacks (as it offers much more flexibility over traditional C# tradecraft) only without using PowerShell in anyway.

Some of the main features that distinguish SILENTTRINITY are:
- **Multi-User & Multi-Server** - Supports multi-user collaboration. Additionally, the client can connect to and control multiple Teamservers.
- **Client and Teamserver Built in Python 3.7** - Latest and greatest features of the Python language are used, heavy use of Asyncio provides ludicrous speeds.
- **Real-time Updates and Communication** - Use of Websockets allow for real-time communication and updates between the Client and Teamserver.
- **Focus on Usability with an Extremely Modern CLI** - Powered by [prompt-toolkit](https://github.com/prompt-toolkit/python-prompt-toolkit).
- **Dynamic Evaluation/Compilation Using .NET Scripting Languages** - The SILENTTRINITY implant [Naga](https://github.com/byt3bl33d3r/Naga), is somewhat unique as it uses embedded third-party .NET scripting languages (e.g. [Boolang](https://github.com/boo-lang/boo)) to dynamically compile/evaluate tasks, this removes the need to compile tasks server side, allows for real-time editing of modules, provides greater flexibilty and stealth over traditional C# based payloads and makes everything much more light-weight.
- **ECDHE Encrypted C2 Communication** - SILENTTRINITY uses Ephemeral Elliptic Curve Diffie-Hellman Key Exchange to encrypt all C2 traffic between the Teamserver and its implant.
- **Fully Modular** - Listeners, Modules, Stagers and C2 Channels are fully modular allowing operators to easily build their own.
- **Extensive logging** - Every action is logged to a file.
- **Future proof** - HTTPS/HTTP listeners are built on [Quart](https://gitlab.com/pgjones/quart) & [Hypercorn](https://gitlab.com/pgjones/hypercorn) which also support HTTP2 & Websockets.

## Getting Involved

Join the #silenttrinity channel in the [BloodHoundGang](https://bloodhoundgang.herokuapp.com/) Slack!

## Call for Contributions

I'm just one person developing this mostly in my spare time, I do need to have a life outside of computers (radical idea, I know).

This means that if anyone finds this tool useful and would like to see X functionality added, the best way to get it added is to submit a Pull Request.

Be the change you want to see in the world!

As of the time of writing the most useful thing you can contribute are post-ex modules: this would allow me to concentrate efforts on the framework itself, user experience, QOL features etc...

To do this, you're going to have to learn the Boo programming language (the Boo [wiki](https://github.com/boo-lang/boo/wiki) is amazing and has everything you'd need to get started), if you know Python you'll find yourself at home :).

Check out some of the existing [modules](../master/core/teamserver/modules/boo), if you've written an [Empire](https://github.com/EmpireProject/Empire) module before you'll see its very similar.
Finally you can start porting over post-ex modules from other C2 frameworks such as [Empire](https://github.com/EmpireProject/Empire).

## Setup & Requirements

- Python >= 3.7 is required.
- Client & Teamserver have only been tested on Ubuntu, they are under testing on Mac & *unix systems and they *should* work on Windows as well.

Clone the repo and build with [shiv](https://github.com/linkedin/shiv) the st binary for the Client & Teamserver:

```bash
git clone https://github.com/byt3bl33d3r/SILENTTRINITY
cd SILENTTRINITY
pip install shiv
make build
```

Start a Teamserver, the default port is 5000:
```bash
./bin/st teamserver <teamserver_ip> <teamserver_password>
```

Connect to a Teamserver:

**Note the wss:// (two s's) in the URL which indicates an encrypted websocket connection (TLS), without this all traffic from the client to the teamserver will be in cleartext!**

```bash
./bin/st client wss://username:<teamserver_password>@<teamserver_ip>:5000
```

Alternatively, run ```./bin/st client``` without any arguments and connect to a Teamserver manually using the CLI menu:
```
~# ./bin/st
[0] ST ≫ teamservers
[0] ST (teamservers) ≫ connect -h
Connect to the specified teamserver(s)

Usage: connect [-h] <URL>...

Arguments:
    URL   teamserver url(s)

[0] ST (teamservers) ≫ connect wss://username:<teamserver_password>@<teamserver_ip>:5000
```

## Documentation

The documentation is a work in progress but some is already available in the [Wiki](https://github.com/byt3bl33d3r/SILENTTRINITY/wiki)

I recommend making wild use the ```help``` command and the ```-h``` flag :)

## Author

Marcello Salvati ([@byt3bl33d3r](https://twitter.com/byt3bl33d3r))

## Acknowledgments, Contributors & Involuntary Contributors

**(In no particular order)**

- [@devinmadewell](https://github.com/devinmadewell) for some awesome modules.
- [@RemiEscourrou](https://github.com/RemiEscourrou) for some awesome modules.
- [@nicolas_dbresse](https://twitter.com/nicolas_dbresse) a.k.a [@Daudau](https://github.com/Daudau) for contributing an insane amount of modules.
- [@C_Sto](https://twitter.com/C__Sto) for helping me with some of the .NET ECDHE implementation details and keeping my sanity.
- [@davidtavarez](https://twitter.com/davidtavarez) for making some amazing contributions including a cross-platform stager.
- [@mcohmi](https://twitter.com/mcohmi) a.k.a daddycocoaman, for being awesome and making code contributions including modules.
- [@SkelSec](https://twitter.com/SkelSec) for the amazing work on [Pypykatz](https://github.com/skelsec/pypykatz) and for being a general inspiration.
- [@cobbr_io](https://twitter.com/cobbr_io) for writing [SharpSploit](https://github.com/cobbr/SharpSploit) which was heavily used as a reference throughout building a lot of the implant code & modules.
- [@harmj0y](https://twitter.com/harmj0y) for writing [SeatBelt](https://github.com/GhostPack/Seatbelt) which I stole (won't be the least thing I'll steal from him either!) and ported over to Boolang and for being a general inspiration.
- [@TheWover](https://twitter.com/TheRealWover) & Odzhan for writing [Donut](https://github.com/TheWover/donut) which SILENTTRINITY makes heavy use of.
- [modexp](https://modexp.wordpress.com) for helping me with a bunch of low level windows stuff which one day I'll learn when I decide to grow up.
- [@_xpn_](https://twitter.com/_xpn_) for making an AMSI Patch which I stole and for being a general inspiration.
- [@_RastaMouse](https://twitter.com/_RastaMouse) for making an AMSI Patch which I stole and for being a general inspiration.
- [@r3dQu1nn](https://twitter.com/r3dQu1nn) for writing his awesome [AggressorScripts](https://github.com/harleyQu1nn/AggressorScripts), decent amount of SILENTTRINITY modules are based off of those scripts.
- [@matterpreter](https://twitter.com/matterpreter) for stealing a bunch of stuff from his [OffensiveCSharp](https://github.com/matterpreter/OffensiveCSharp) repository.
- [@hackabean](https://github.com/hackabean) for some awesome modules.

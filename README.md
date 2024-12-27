# Off World Fix

[![🧪 Tested with 7DTD 1.2 (b27)](https://img.shields.io/badge/🧪%20Tested%20with-7DTD%201.2%20(b27)-blue.svg)](https://7daystodie.com/)
[![✅ Dedicated Servers Supported ServerSide](https://img.shields.io/badge/✅%20Dedicated%20Servers-Supported%20Serverside-blue.svg)](https://7daystodie.com/)
[![✅ Single Player and P2P Supported](https://img.shields.io/badge/✅%20Single%20Player%20and%20P2P-Supported-blue.svg)](https://7daystodie.com/)
[![📦 Automated Release](https://github.com/jonathan-robertson/off-world-fix/actions/workflows/release.yml/badge.svg)](https://github.com/jonathan-robertson/off-world-fix/actions/workflows/release.yml)

## Summary

7 Days to Die mod: Fix 'entities have fallen off the world' warning by moving those entities to the surface.

### Support

🗪 If you would like support for this mod, please feel free to reach out via [Discord](https://discord.gg/hYa2sNHXya).

## Features

When 'entities have fallen off the world' triggers and those entities are marked for removal, this mod will instead move those entities to the surface for player retrieval.

Here are some real examples of those warning messages:

```log
2024-08-03T09:22:30 26471.969 WRN Entity [type=EntityZombie, name=zombieSteve, id=34172] fell off the world, id=34172 pos=(-1683.7, -0.6, 490.3)
...
2024-10-10T19:21:28 19270.540 WRN Entity [type=EntityLootContainer, name=?] fell off the world, id=1164 pos=(-198.7, -20321.9, -2266.2)
...
2024-12-26T19:29:15 19732.984 WRN Entity [type=EntityLootContainer, name=?] fell off the world, id=110720 pos=(400.2, -950.3, 1138.6)
...
2024-12-26T22:21:11 30048.522 WRN Entity [type=EntityLootContainer, name=?] fell off the world, id=109489 pos=(352.4, -8179.0, 779.8)
...
2024-12-27T03:37:59 5801.977 WRN Entity [type=EntityLootContainer, name=?] fell off the world, id=104416 pos=(1036.5, -312.4, 1807.5)
```

### Admin Commands

> ℹ️ You can always search for this command or any command by running:
>
> - `help * <partial or complete command name>`
> - or get details about this (or any) command and its options by running `help <command>`

|   primary   | alternate |     params     | description                                                     |
| :---------: | :-------: | :------------: | --------------------------------------------------------------- |
| offworldfix |    owf    | `debug` / `dm` | enable/disable debug logging for this mod (disabled by default) |

*Note that leaving debug mode on can have a negative impact on performance. It is therefore recommended to only turn it on while troubleshooting and then disable it afterwards.*

## Setup

Without proper installation, this mod will not work as expected. Using this guide should help to complete the installation properly.

If you have trouble getting things working, you can reach out to me for support via [Support](#support).

### Environment / EAC / Hosting Requirements

| Environment          | Compatible | Does EAC Need to be Disabled? | Who needs to install? |
| -------------------- | ---------- | ----------------------------- | --------------------- |
| Dedicated Server     | Yes        | no                            | only server           |
| Peer-to-Peer Hosting | Yes        | only on the host              | only the host         |
| Single Player Game   | Yes        | Yes                           | self (of course)      |

> 🤔 If you aren't sure what some of this means, details steps are provided below to walk you through the setup process.

### Map Considerations for Installation or UnInstallation

- Does **adding** this mod require a fresh map?
  - No, you can drop this mod into an ongoing map without any trouble.
- Does **removing** this mod require a fresh map?
  - No, you can remove this mod without causing any trouble in an ongoing map.

### Windows PC (Single Player or Hosting P2P)

> ℹ️ If you plan to host a multiplayer game, only the host PC will need to install this mod. Other players connecting to your session do not need to install anything for this mod to work 😉

1. 📦 Download the latest release by navigating to [this link](https://github.com/jonathan-robertson/off-world-fix/releases/latest/) and clicking the link for `off-world-fix.zip`
2. 📂 Unzip this file to a folder named `off-world-fix` by right-clicking it and choosing the `Extract All...` option (you will find Windows suggests extracting to a new folder named `off-world-fix` - this is the option you want to use)
3. 🕵️ Locate and create your mods folder (if missing): in another window, paste this address into to the address bar: `%APPDATA%\7DaysToDie`, then enter your `Mods` folder by double-clicking it. If no `Mods` folder is present, you will first need to create it, then enter your `Mods` folder after that
4. 🚚 Move this new `off-world-fix` folder into your `Mods` folder by dragging & dropping or cutting/copying & pasting, whichever you prefer
5. ♻️ Stop the game if it's currently running, then start the game again without EAC by navigating to your install folder and running `7DaysToDie.exe`
    - running from Steam or other launchers usually starts 7 Days up with the `7DaysToDie_EAC.exe` program instead, but running 7 Days directly will skip EAC startup

#### Critical Reminders

- ⚠️ it is **NECESSARY** for the host to run with EAC disabled or the DLL file in this mod will not be able to run
- 😉 other players **DO NOT** need to disable EAC in order to connect to your game session, so you don't need to walk them through these steps
- 🔑 it is also **HIGHLY RECOMMENDED** to add a password to your game session
  - while disabling EAC is 100% necessary (for P2P or single player) to run this mod properly, it also allows other players to run any mods they want on their end (which could be used to gain access to admin commands and/or grief you or your other players)
  - please note that *dedicated servers* do not have this limitation and can have EAC fully enabled; we have setup guides for dedicated servers as well, listed in the next 2 sections: [Windows/Linux Installation (Server via FTP from Windows PC)](#windowslinux-installation-server-via-ftp-from-windows-pc) and [Linux Server Installation (Server via SSH)](#linux-server-installation-server-via-ssh)

### Windows/Linux Installation (Server via FTP from Windows PC)

1. 📦 Download the latest release by navigating to [this link](https://github.com/jonathan-robertson/off-world-fix/releases/latest/) and clicking the link for `off-world-fix.zip`
2. 📂 Unzip this file to a folder named `off-world-fix` by right-clicking it and choosing the `Extract All...` option (you will find Windows suggests extracting to a new folder named `off-world-fix` - this is the option you want to use)
3. 🕵️ Locate and create your mods folder (if missing):
    - Windows PC or Server: in another window, paste this address into to the address bar: `%APPDATA%\7DaysToDie`, then enter your `Mods` folder by double-clicking it. If no `Mods` folder is present, you will first need to create it, then enter your `Mods` folder after that
    - FTP: in another window, connect to your server via FTP and navigate to the game folder that should contain your `Mods` folder (if no `Mods` folder is present, you will need to create it in the appropriate location), then enter your `Mods` folder. If you are confused about where your mods folder should go, reach out to your host.
4. 🚚 Move this new `off-world-fix` folder into your `Mods` folder by dragging & dropping or cutting/copying & pasting, whichever you prefer
5. ♻️ Restart your server to allow this mod to take effect and monitor your logs to ensure it starts successfully:
    - you can search the logs for the word `OffWorldFix`; the name of this mod will appear with that phrase and all log lines it produces will be presented with this prefix for quick reference

### Linux Server Installation (Server via SSH)

1. 🔍 [SSH](https://www.digitalocean.com/community/tutorials/how-to-use-ssh-to-connect-to-a-remote-server) into your server and navigate to the `Mods` folder on your server
    - if you installed 7 Days to Die with [LinuxGSM](https://linuxgsm.com/servers/sdtdserver/) (which I'd highly recommend), the default mods folder will be under `~/serverfiles/Mods` (which you may have to create)
2. 📦 Download the latest `off-world-fix.zip` release from [this link](https://github.com/jonathan-robertson/off-world-fix/releases/latest/) with whatever tool you prefer
    - example: `wget https://github.com/jonathan-robertson/off-world-fix/releases/latest/download/off-world-fix.zip`
3. 📂 Unzip this file to a folder by the same name: `unzip off-world-fix.zip -d off-world-fix`
    - you may need to install `unzip` if it isn't already installed: `sudo apt-get update && sudo apt-get install unzip`
    - once unzipped, you can remove the off-world-fix download with `rm off-world-fix.zip`
4. ♻️ Restart your server to allow this mod to take effect and monitor your logs to ensure it starts successfully:
    - you can search the logs for the word `OffWorldFix`; the name of this mod will appear with that phrase and all log lines it produces will be presented with this prefix for quick reference
    - rather than monitoring telnet, I'd recommend viewing the console logs directly because mod and DLL registration happens very early in the startup process and you may miss it if you connect via telnet after this happens
    - you can reference your server config file to identify your logs folder
    - if you installed 7 Days to Die with [LinuxGSM](https://linuxgsm.com/servers/sdtdserver/), your console log will be under `log/console/sdtdserver-console.log`
    - I'd highly recommend using `less` to open this file for a variety of reasons: it's safe to view active files with, easy to search, and can be automatically tailed/followed by pressing a keyboard shortcut so you can monitor logs in realtime
      - follow: `SHIFT+F` (use `CTRL+C` to exit follow mode)
      - exit: `q` to exit less when not in follow mode
      - search: `/OffWorldFix` [enter] to enter search mode for the lines that will be produced by this mod; while in search mode, use `n` to navigate to the next match or `SHIFT+n` to navigate to the previous match

### Troubleshooting / Common Issues

⚠️ Because OffWorldFix contains a DLL file, you may have trouble uploading to a 7 Days to Die dedicated host. Some hosts will silently *remove* DLL files from mods or prevent them from being overwritten (i.e. updated) when they are uploaded via FTP or various other methods. Please be sure to double-check that the `OffWorldFix.DLL` file is found within the `off-world-fix` folder within your server if you don't see a reference to this DLL file in the logs on startup. In those cases, you can reach out to your host and explain the problem; most hosts will allow you to upload DLL mods and may have a special set of steps for you to follow or they may need to simply enable this functionality on your account.

## Special Thanks

Several people in the community have offered feedback, identified bugs, and have worked to provide me with debug logs to help move this project forward. Quantum Elevators is a cool idea and has been a lot of fun to work on, but would not be what it is today without the added effort from these incredible admins, modders, and players. *(Discord Usernames)*

- `Shavick#8511` performed early testing and identified various issues that arose when client and server were both running the mod.
- `Grandpa Minion#2643` and the NAPVP Community who helped with hardcore testing and several bug reports.
- `Blight#7410` of Pimp my House, Tea Lounge, Juggernaut, and Ragnarok submitted bug reports that helped identify and resolve a critical bug.
- `O C#2804` reviewed and offered suggestions and advice that led to the resolution of a critical bug.
- `vivo#0815` identified how to recreate a bug that occurred only on initial launch of a new map.
- `Oggy#9577` identified an issue related to height detection of multi-dimensional block rotation.

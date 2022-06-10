# oggReplacer

Lighten CM or COM .arc files by replacing every .ogg files by empty ones.

## _Warning_
### This tool is destructive! This is what it is made for.

### What does it do?

- Remove update .arc you do not need.
The updater comes with all *_2.arc ever released by Kiss and install them even if you don't have the corresponding DLCs.
The tool will simply find those you do not need and delete them.
To install them back simply use the last update again.

- Scan through each .arc and replace every .ogg with an empty one.  
This will greatly reduce the space .arc will occupy at the expense of EVERY sounds the game has.
for example voice_a.arc goes from 1.46Gb to 56Mb.
By default, for security, it will make a backup of each modified .arc


### How to use.

- Make sure you have .NetFramework 4.8 installed (should be if your windows is up to date)
- Launch the tool
- Enter CM or COM's folder and press enter.
- Select your options and press enter to start.
- Let it run.
- If you choose to keep a backup your original .arc will be in GameData as file.arc.bak

### Notes

- Your game will work like it used to, only without sounds.
- You can update it as usual.
- Mods are not affected.
- You can rerun it after an update or dlc install. It's entirely up to you.

This tool uses Guest's CM3D2.Toolkit branch:
<https://github.com/JustAGuest4168/CM3D2.Toolkit>

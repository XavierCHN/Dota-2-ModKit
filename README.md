# Dota 2 ModKit

A GUI comprised of useful tools to help with Dota 2 modding.

## Features:
* **Particle Forker**. Allows easy copying of decompiled particles into your addons. This will automatically change the child references after the particles are copied over.
  * **Easy Re-Coloring, Re-Naming, and Re-Sizing**. Color, rename, and/or resize whole particle systems at just the click of a button!
* **Tooltips Generator**. Parses all the files in the scripts\npc folder of your addon, and creates tooltips for abilities, items, modifiers, units, and heroes, which you can easily copy over to addon_english.txt or another language file.
* **Templates**. Create templates for commonly used abilities, items, heroes, and units, which you can easily copy into your addon.
* **Fork Addon From Barebones**. Create your own unique addon based off of BMD's Barebones (https://github.com/bmddota/barebones).
* **Wiki Generator**. Generate wikis for your addon, for every language you've defined, based off the scripts/npc files. Currently incomplete.
* **Copy Addon to another folder**. Copies the game and content directories of your addon to another folder.

![Alt text](http://i.imgur.com/2tYo5Qi.png)
![Alt text](http://i.imgur.com/Zerrlb3.png)

## Installation

1. [Download here](https://github.com/Myll/Dota-2-ModKit/releases), or you can build from the .sln.

2. If you already have the decompiled particles, move them over to the D2ModKit folder. **Rename the decompiled particles folder to decompiled_particles**. If you don't have the decompiled particles, [Click here to DL](https://mega.co.nz/#!cpgkSQbY!_xjYFGgkL2yhv0l8MPjEfESjN7B1S0cVP-QXsx3c-7M).

3. Open D2ModKit.exe

## How-Tos

I hope you find every feature in D2ModKit intuitive and easy to use. I will outline how to use some of the features below.

**Templates:** To edit templates just edit the .txt files in the D2ModKit\Templates folder. To add a new entry just preface the entry with //+Template as shown in the example entries.

**Particles:** After you copy particles to an addon, you may have to restart the Workshop Tools to see them in the asset browser. I sometimes have to do this.

## Credits

* Thanks to [ToraxXx](https://github.com/toraxxx) for decompiling all of the particles.
* Thanks to [RoyAwesome](https://github.com/RoyAwesome) for making KVLib, which allows easy parsing of Valve KV files.
* Thanks to [Noya](https://github.com/MNoya) for giving me the idea of a Tooltips generator.
* Thanks to [penguinwizzard](https://github.com/Penguinwizzard) for doing the icon.
* Thanks to [SebRut](https://github.com/sebrut) for ideas and C# tips.

## Notes

Have ideas for new features? Let me know! Best way to contact me is through the Dota 2 modding IRC or by email: stephennf@gmail.com.

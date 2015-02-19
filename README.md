# Dota 2 ModKit

A GUI comprised of useful tools to help with Dota 2 modding.

### Features:
* [**Particle Forker & Designer**](http://gfycat.com/IdioticMarvelousBee). Allows easy copying of decompiled particles into your addons. This will automatically change the child references after the particles are copied over.
  * Re-color, rename, and/or resize whole particle systems at just the click of a button!
* [**Tooltips Generator**](http://gfycat.com/ImpeccablePassionateFirefly). Parses all the files in the scripts\npc folder of your addon, and creates tooltips for abilities, items, modifiers, units, and heroes, which you can easily copy over to addon_english.txt or another language file.
* [**Fork Addon From Barebones**](http://gfycat.com/NarrowIncredibleBongo). Create your own unique addon based off of the Barebones template.
* **Templates**. Create templates for commonly used abilities, items, heroes, and units, which you can easily copy into your addon.
* **Wiki Generator**. Generate wikis for your addon, for every language you've defined, based off the scripts/npc files. Currently incomplete.
* **Copy Addon to another folder**. Copies the game and content directories of your addon to another folder.

![Alt text](http://i.imgur.com/7eukrzj.png)
![Alt text](http://i.imgur.com/rjvUrDe.png)

## Installation

1. [Download the lastest version here](https://github.com/Myll/Dota-2-ModKit/releases), or you can build from the .sln. Extract the contents to a new folder, and ensure that folder is on the same drive as your Dota 2 installation. You can make a shortcut to D2ModKit.exe so you can run it from your main drive.

2. [Download the decompiled particles](http://moddota.com/resources/decompiled_particles.zip) and extract the folder over to the D2ModKit folder.

3. Run D2ModKit.exe

## Usage

I hope you find every feature in D2ModKit intuitive and easy to use. I will outline how to use some of the features below.

**Templates:** To edit templates just edit the .txt files in the D2ModKit\Templates folder. To add a new entry just preface the entry with //+Template as shown in the example entries.

## Credits

* Thanks to [ToraxXx](https://github.com/toraxxx) for decompiling all of the particles.
* Thanks to [RoyAwesome](https://github.com/RoyAwesome) for making KVLib, which allows easy parsing of Valve KV files.
* Thanks to [Noya](https://github.com/MNoya) for giving me the idea of a Tooltips generator and helping a lot with testing.
* Thanks to [penguinwizzard](https://github.com/Penguinwizzard) for doing the icon.
* Thanks to [SebRut](https://github.com/sebrut) for ideas and C# tips.
* Thanks to [XavierCHN](https://github.com/XavierCHN) for contributing to VTEX Compiling/Decompiling.

## Notes

Find bugs or have ideas for new features? [Let me know!](https://github.com/Myll/Dota-2-ModKit/issues/new)

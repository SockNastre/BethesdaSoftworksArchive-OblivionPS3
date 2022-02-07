## BethesdaSoftworksArchive OblivionPS3
Requires .NET Framework 4.8

### Features
 - Can pack BSA archives for PS3 properly.
 - Drag & drop for files and folders to add quickly into archive.
 - Allows for modification of textures on Oblivion PS3.
 - Allows for mods to have working textures alongside meshes.
 - Can choose to compress assets inside of BSA or not.
 - Normal maps from PC are converted to PS3 variant by default.
 - Tool checks if asset data is already converted for PS3, if it is, nothing is done to it pre-packing; this allows repacking main game BSAs and seeing no differences.
 - CLI tool allows for dragging folder onto executable and getting archives packed with PS3 settings straight away.
 - NormalMapConverter tool allowing DDS normal maps from PC to be converted to PS3. This tool is used by default by the Packer GUI but I included it as a executable in case someone wants to use it separately.

### F.A.Q

Q: _How can we **open BSA**?_  
A: The [BSA Browser](https://www.github.com/AlexxEG/BSA_Browser) tool by AlexxEG (see credits) supports opening Oblivion PS3 BSA.  

Q: _What settings should I be using to pack on PS3?_  
A: In order to make a BSA that is proper for PS3 make sure "Use PS3 file flags", "Extend data", and "Convert normal maps" are turned on. If they are turned off BSA may not work right on PS3.

Q: _Should I turn on the setting to compress assets in the archive?_  
A: This one is up to you, compression may increase load times but decrease BSA size.

Q: _Why is my game crashing after adding a BSA from this tool?_
A: The game may crash if the files inside of the BSA are bad or unexpected from Oblivion's perspective. This one varies, for textures you can see the question below. For other file types inside of BSA it is important to remember that there will always be differences between PC and PS3 Oblivion, some differences may cause issues with loading files.

Q: _Why are my 2048x2048 or higher-resolution textures crashing the game?_
A: From my testing any textures that are 2048x2048 or higher ends up crashing game. While these resolutions may seem appealing it is important to remember Oblivion PS3 is a PS3 game and cannot handle too much. It is advised you downscale the textures below 2048x2048.

Q: _How can I downscale my textures?_
A: The [Ordenador](https://www.nexusmods.com/skyrim/mods/12801) tool by AdPipino supports mass-downscaling textures. It is advised you downscale first then pack with the tool.

Q: _Why was my DXT3/A8R8B8G8 normal map converted to DXT5?_  
A: When "Convert normal maps" is turned on in settings all alpha-containing normal maps are turned into DXT5. If this causes some kind of issues and you are sure it's this feature that is messing it up please create an issue for it.

Q: _Should I pack BSAs for PC with this?_  
A: No. For PC there are other tools specifically made for it which can be Googled. If you decide to use my tool for packing on PC I hold no responsibility for bad BSAs being made. The reason why certain options can be turned off in order to make BSAs more like PC is because in PS3 Oblivion the main mesh BSA (and possibly others I do not know of) have PC file flags, and sometimes extending DDS data isn't used.

### Credits
 - Thanks to [AlexxEG](https://www.github.com/AlexxEG) for helping me understand buffers.
 - Thanks to Spawnkiller for helping me understand how games handle textures and helping me with mesh-related work.
 - Alberto M. for [.NET Zlib Implementation](https://www.codeproject.com/Tips/830793/NET-ZLib-Implementation) library for zlib compression.
 - Dirk Lemstra (dlemstra) for [Magick.NET](https://www.github.com/dlemstra/Magick.NET) library for dealing with image files.
 - Josip Medved for the OpenFolderDialog.

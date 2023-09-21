# [UnityWebTools.NET](https://github.com/kiraio-moe/UnityWebTools.NET "UnityWebTools.NET GitHub repository")

## Table of Contents

- [Overview](#overview)
- [Key Features](#key-features)
- [Usage](#usage)
- [Contributing](#contributing)
- [Credits](#credits)
- [License](#license)
- [Disclaimer](#disclaimer)

## Overview

UnityWebTools.NET is a C# library designed to simplify the handling of `UnityWebData` _(*.data)_ files within Unity WebGL game by providing unpack and repack functionality to modify the game assets. This library serves as a supplementary addition to [AssetsTools.NET](https://github.com/nesrak1/AssetsTools.NET "AssetsTools.NET GitHub repository") library, enhancing your capabilities when working with Unity game data.

## Key Features

- **Unpack UnityWebData**: UnityWebTools simplifies the process of unpacking `UnityWebData` files, allowing you to effortlessly access their contents.
- **Repack UnityWebData**: With UnityWebTools, repacking `UnityWebData` files is a breeze, making it convenient to modify game's assets.

## Usage

Using UnityWebTools.NET is simple!

- **Install** UnityWebTools.NET:

  ```cli
  dotnet add package Kiraio.UnityWebTools.NET --version <latest_version_available>
  ```

- **Start** using it:

  ```csharp
  // Important! Import the namespace.
  using Kiraio.UnityWebTools;

  // Check if the file is a valid UnityWebData file.
  bool valid = UnityWebToolUtils.IsUnityWebData("WebGL.data");

  // Extract the UnityWebData (*.data) file.
  // Unpack(string webDataFile, string outputDirectory (Optional)).
  // Return output directory path.
  string output = UnityWebTool.Unpack("WebGL.data");

  // Compress folder as UnityWebData (*.data) file.
  // Pack(string sourceFolder, string outputFile (Optional)).
  // Return output file path.
  UnityWebTool.Pack(output);
  ```

## Contributing

Contributions are welcome! If you have suggestions, bug reports, or would like to contribute code, feel free to make Issues/Pull Requests.

## Credits

- [ChrisX930](https://forum.xentax.com/memberlist.php?mode=viewprofile&u=44998 "ChrisX930 at XeNTaX forum") for [UnityWebData file structure](https://forum.xentax.com/viewtopic.php?f=21&p=187239).
- [Kaitai Struct](https://kaitai.io/ "Kaitai Struct").

## License

This project is licensed under GNU GPL 3.0.

For more information about the GNU General Public License version 3.0 (GNU GPL 3.0), please refer to the official GNU website: https://www.gnu.org/licenses/gpl-3.0.html

## Disclaimer

This tool is intentionally created as a modding tool. I didn't condone any piracy in any forms such as taking the game visual and sell it illegally which harm the original developer. Use the tool at your own risk (DWYOR - Do What You Own Risk).

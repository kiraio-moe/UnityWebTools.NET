# UnityWebTools.NET

## Table of Contents

- [Overview](#overview)
- [Key Features](#key-features)
- [Usage](#usage)
- [Contributing](#contributing)
- [Credits](#credits)
- [License](#license)

## Overview

UnityWebTools.NET is a C# library designed to simplify the handling of `UnityWebData` _(*.data)_ files within Unity WebGL game by providing unpack and repack functionality to modify the game assets. This library serves as a supplementary addition to [AssetsTools.NET](https://github.com/nesrak1/AssetsTools.NET "AssetsTools.NET GitHub repository") library, enhancing your capabilities when working with Unity game data.

## Key Features

- **Unpack UnityWebData**: UnityWebDataTools simplifies the process of unpacking `UnityWebData` files, allowing you to effortlessly access their contents.
- **Repack UnityWebData**: With UnityWebDataTools, repacking `UnityWebData` files is a breeze, making it convenient to modify game's assets.

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

  // Check if the file is valid UnityWebData file.
  bool valid = UnityWebToolUtils.IsUnityWebData("WebGL.data");

  // Extract the UnityWebData (*.data) file.
  // Unpack(string webDataFile, string outputDirectory (Optional)).
  // Return output directory path.
  string output = UnityWebTool.Unpack("WebGL.data");

  // Compress folder as UnityWebData (*.data) file.
  // Pack(string sourceFolder, string outputFile (Optional)).
  // Return output file path.
  Pack(output);
  ```

## Contributing

Contributions are welcome! If you have suggestions, bug reports, or would like to contribute code, feel free to make Issues/Pull Requests.

## Credits

- [ChrisX930](https://forum.xentax.com/memberlist.php?mode=viewprofile&u=44998 "ChrisX930 at XeNTaX forum") for [UnityWebData file structure](https://forum.xentax.com/viewtopic.php?f=21&p=187239).

## License

UnityWebTools.NET are licensed under the [GNU General Public License v3.0 License](https://www.gnu.org/licenses/gpl-3.0.en.html).

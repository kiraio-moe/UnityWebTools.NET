using System.Text;
using Kaitai;

namespace Kiraio.UnityWebTools
{
#region UnityWebData File Structure
    struct WebData
    {
        public byte[] Magic;
        public uint FirstFileOffset;
        public List<FileEntry> FileEntries;
        public List<byte[]> FileContents;
    }

    struct FileEntry
    {
        public uint FileOffset;
        public uint FileSize;
        public uint FileNameSize;
        public byte[] Name;
    }
#endregion

    /// <summary>
    /// Main class to handle `UnityWebData`.
    /// </summary>
    public static class UnityWebTool
    {
        /// <summary>
        /// UnityWebData signature
        /// </summary>
        internal const string MAGIC_HEADER = "UnityWebData1.0";

        /// <summary>
        /// Unpack UnityWebData (*.data) to files.
        /// </summary>
        /// <param name="webDataFile">UnityWebData (*.data) file.</param>
        /// <param name="outputDirectory">Optional, default to <paramref name="webDataFile"/> name directory in the current working directory.</param>
        /// <returns>Output directory.</returns>
        public static string Unpack(string? webDataFile, string? outputDirectory = null)
        {
            if (!File.Exists(webDataFile))
                throw new FileNotFoundException($"{webDataFile} didn\'t exist!");

            // Set output directory default path
            outputDirectory ??= Path.Combine(
                Path.GetDirectoryName(webDataFile) ?? string.Empty,
                Path.GetFileNameWithoutExtension(webDataFile)
            );

            if (!Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);

            // Create the Kaitai stream and the root object from the parsed data
            UnityWebData? unityWebData = UnityWebData.FromFile(webDataFile);

            foreach (UnityWebData.FileEntry fileEntry in unityWebData.Files)
            {
                string? fileName = fileEntry?.Filename;

                // Create file entry directory
                string? fileNameDirectory = Path.Combine(
                    outputDirectory,
                    Path.GetDirectoryName(fileName) ?? string.Empty
                );

                if (!Directory.Exists(fileNameDirectory))
                    Directory.CreateDirectory(fileNameDirectory);

                string? outputFile = Path.Combine(outputDirectory, fileName ?? string.Empty);

                using FileStream? outputFileStream = new(outputFile, FileMode.Create);
                outputFileStream?.Write(fileEntry?.Data);
            }

            return outputDirectory;
        }

        /// <summary>
        /// Pack a folder as UnityWebData (*.data) file.
        /// </summary>
        /// <param name="sourceFolder">The source folder.</param>
        /// <param name="outputFile">Optional, default as `<paramref name="sourceFolder"/>_name.data`.</param>
        /// <returns>Output file path.</returns>
        public static string Pack(string sourceFolder, string? outputFile = null)
        {
            // Set output file default path
            outputFile ??= $"{sourceFolder}.data";

            // Get all files recursively
            List<string> files = UnityWebToolUtils.GetFilesRecursive(sourceFolder).ToList();

            // Get files in root directory by sorting from `files`
            List<string> rootFolderFiles = files
                .Where(f => Path.GetDirectoryName(f) == sourceFolder)
                .ToList();

            // Get files inside subdirectories
            List<string> subdirectoryFiles = files.Except(rootFolderFiles).ToList();

            // Sort the subdirectory files in descending order
            subdirectoryFiles.Sort((a, b) => b.CompareTo(a));

            // Combine the lists and print the result
            files = subdirectoryFiles.Concat(rootFolderFiles).ToList();
            List<string>? filesName = new();

            foreach (string file in files)
                filesName.Add(
                    file.Replace(sourceFolder, "")
                        .Trim(Path.DirectorySeparatorChar)
                        .Replace(@"\", @"/")
                );

            using MemoryStream tempStream = new();
            using BinaryWriter tempWriter = new(tempStream);
            byte[] magic = UnityWebToolUtils.AddNullTerminate(Encoding.UTF8.GetBytes(MAGIC_HEADER));
            List<FileEntry> fileEntries = new();
            List<byte[]> fileContents = new();
            List<long> fileOffsetValues = new();
            List<long> fileOffsetEntryPosition = new();

            // Collect file entries
            for (int i = 0; i < files.Count; i++)
            {
                FileInfo fileInfo = new(files[i]);
                byte[] fileNameBytes = Encoding.UTF8.GetBytes(filesName[i]);

                fileEntries.Add(
                    new FileEntry
                    {
                        FileOffset = 0,
                        FileSize = (uint)fileInfo.Length,
                        FileNameSize = (uint)fileNameBytes.Length,
                        Name = fileNameBytes
                    }
                );

                fileContents.Add(File.ReadAllBytes(files[i]));
            }

            try
            {
                WebData webData =
                    new()
                    {
                        Magic = magic,
                        FirstFileOffset = 0,
                        FileEntries = fileEntries,
                        FileContents = fileContents
                    };

                // Write Magic bytes
                tempWriter.Write(webData.Magic);

                // Write a placeholder for FirstFileOffset
                fileOffsetEntryPosition.Add(tempStream.Position);
                tempWriter.Write(webData.FirstFileOffset);

                // Write each FileEntry
                foreach (FileEntry entry in webData.FileEntries)
                {
                    // Write FileOffset
                    fileOffsetEntryPosition.Add(tempStream.Position);
                    tempWriter.Write(entry.FileOffset);

                    // Write FileSize
                    tempWriter.Write(entry.FileSize);

                    // Write FileNameSize
                    tempWriter.Write(entry.FileNameSize);

                    // Write Name char bytes
                    tempWriter.Write(entry.Name);
                }

                foreach (byte[] content in webData.FileContents)
                {
                    // Add current offset to a list to be used later
                    fileOffsetValues.Add(tempStream.Position);

                    // Write the actual data
                    tempWriter.Write(content);
                }

                // Go back to WebData.FirstFileOffset and write the first file offset
                tempStream.Seek(fileOffsetEntryPosition[0], SeekOrigin.Begin);
                tempWriter.Write((uint)fileOffsetValues[0]);

                // Go back to each FileEntry.FileOffset and write the file offset
                for (int i = 0; i < fileOffsetValues.Count; i++)
                {
                    tempStream.Seek(fileOffsetEntryPosition[i + 1], SeekOrigin.Begin);
                    tempWriter.Write((uint)fileOffsetValues[i]);
                }

                // Write the entire contents of the temporary stream to the actual file
                using FileStream fileStream = new(outputFile, FileMode.Create);
                tempStream.WriteTo(fileStream);
            }
            catch
            {
                throw new Exception($"Failed to write {outputFile}");
            }

            return outputFile;
        }
    }
}

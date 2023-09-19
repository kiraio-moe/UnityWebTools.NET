using System.Text;

namespace Kiraio.UnityWebTools
{
    /// <summary>
    /// Utility class for `UnityWebTools`.
    /// </summary>
    public static class UnityWebToolUtils
    {
        /// <summary>
        /// Check if a file is a valid `UnityWebData` file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Validity of the file.</returns>
        public static bool IsUnityWebData(string filePath)
        {
            return IsFile(filePath, Encoding.UTF8.GetBytes(UnityWebTool.MAGIC_HEADER));
        }

        /// <summary>
        /// Check the file signature if it's a valid file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileSignature"></param>
        /// <returns>The file are valid or not.</returns>
        internal static bool IsFile(string filePath, byte[] fileSignature)
        {
            try
            {
                byte[] sourceFileSignature = new byte[fileSignature.Length];

                using FileStream file = File.OpenRead(filePath);
                file.Read(sourceFileSignature, 0, fileSignature.Length);

                for (int i = 0; i < fileSignature.Length; i++)
                {
                    if (sourceFileSignature[i] != fileSignature[i])
                        return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error when reading file signature on {Path.GetFileName(filePath)}. {ex.Message}"
                );
                return false;
            }
        }

        /// <summary>
        /// Get files path recursively.
        /// </summary>
        /// <param name="sourceFolder"></param>
        /// <returns></returns>
        internal static string[] GetFilesRecursive(string sourceFolder)
        {
            return Directory.GetFiles(sourceFolder, "*.*", SearchOption.AllDirectories);
        }

        /// <summary>
        /// Add null terminator at the end of bytes.
        /// </summary>
        /// <param name="originalArray"></param>
        /// <returns>Modified array of bytes.</returns>
        internal static byte[] AddNullTerminate(byte[] originalArray)
        {
            // Create a new array with one extra element to accommodate the null byte
            byte[] newArray = new byte[originalArray.Length + 1];

            // Copy the original array to the new array
            Array.Copy(originalArray, newArray, originalArray.Length);

            // Set the last element to be the null byte
            newArray[^1] = 0;

            return newArray;
        }
    }
}

using System;
using System.IO;

namespace FancyJson
{
    public class Utility
    {
        /// <summary>
        /// Returns the base path.
        /// </summary>
        /// <returns>base path</returns>
        public static string GetPath()
        {
            return GetPath(EFolderType.baseFolder);
        }

        /// <summary>
        /// Returns the json folder path.
        /// </summary>
        /// <returns>path of the json folder.</returns>
        public static string GetPathJson()
        {
            return GetPath(EFolderType.jsonFolder);
        }

        /// <summary>
        /// Returns the path based on the given args.
        /// </summary>
        /// <param name="folder">enum for what path to return</param>
        /// <returns>requested path</returns>
        public static string GetPath(EFolderType folder)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            if (folder == EFolderType.baseFolder) return basePath;
            if (folder == EFolderType.jsonFolder) return Path.Combine(basePath, "json");
            return null;
        }

    }
}


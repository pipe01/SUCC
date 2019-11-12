﻿using System;
using System.IO;

namespace SUCC
{
    /// <summary>
    /// A read-only version of DataFile. Data can be read from disk, but not saved to disk.
    /// </summary>
    public class MemoryReadOnlyDataFile : ReadableDataFile
    {
        /// <summary>
        /// Creates an empty ReadOnlyDataFile in memory.
        /// </summary>
        public MemoryReadOnlyDataFile() : this(String.Empty) { }

        /// <summary>
        /// Creates a ReadOnlyDataFile in memory with some preexisting SUCC content.
        /// </summary>
        public MemoryReadOnlyDataFile(string rawFileText)
        {
            MemoryTextData = rawFileText;
        }


        private string MemoryTextData = "";

        protected override string GetSavedText()
            => MemoryTextData;


        /// <summary>
        /// Saves the contents of this MemoryDataFile to disk and returns a disk DataFile corresponding to the new file.
        /// </summary>
        /// <param name="relativeOrAbsolutePath"> The path of the new file. </param>
        /// <param name="overwrite"> If this is false, don't save the data if the file already exists on disk. </param>
        /// <returns> Null if overwrite was set to false and a file already existed </returns>
        public ReadOnlyDataFile ConvertToFileOnDisk(string relativeOrAbsolutePath, bool overwrite = true)
        {
            if (!overwrite && Utilities.SuccFileExists(relativeOrAbsolutePath))
                return null;

            string path = Utilities.AbsolutePath(relativeOrAbsolutePath);
            File.WriteAllText(path, this.GetRawText());
            return new ReadOnlyDataFile(path);
        }
    }
}
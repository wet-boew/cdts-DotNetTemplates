using System;
using System.Collections.Generic;
using System.IO;
using DiffCopy.Extensions;

namespace DiffCopy
{
    /// <summary>
    /// Provides an object that compares files based on the contents of the file.
    /// </summary>
    public class ByteStreamComparer : DirectoryComparer
    {
        private const int BufferSize = 4096; // 4k buffer size.

        /// <summary>
        /// 	<para>Initializes an instance of the <see cref="ByteStreamComparer"/> class.</para>
        /// </summary>
        public ByteStreamComparer() { }

        /// <summary>
        /// Compares the file contents for two directories and returns information about the differences.
        /// </summary>
        /// <param name="source">The source directory to compare to the destination.</param>
        /// <param name="destination">The destination directory where, ultimately, files would be copied.</param>
        /// <returns>A <see cref="ComparisonResult"/> object that contains the result of the comparison operation.</returns>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public override ComparisonResult Compare(string source, string destination)
        {
            base.Scan(source, destination);

            var modifiedFiles = new List<string>();

            byte[] sourceBytes;
            byte[] destinationBytes;
            BinaryReader sourceReader = null;
            BinaryReader destinationReader = null;

            // compare the overlap files
            foreach (var file in base.FilesToCompare)
            {
                using (var sourceFile = new FileStream(string.Concat(source, file), FileMode.Open, FileAccess.Read))
                {
                    using (var destinationFile = new FileStream(string.Concat(destination, file), FileMode.Open, FileAccess.Read))
                    {
                        if (sourceFile.Length.Equals(destinationFile.Length))
                        {
                            // if they are the same size, compare them.
                            sourceReader = new BinaryReader(sourceFile);
                            destinationReader = new BinaryReader(destinationFile);
                            do
                            {
                                sourceBytes = sourceReader.ReadBytes(ByteStreamComparer.BufferSize);
                                destinationBytes = destinationReader.ReadBytes(ByteStreamComparer.BufferSize);

                                if (sourceBytes.Length > 0)
                                {
                                    // if the arrays of bytes aren't equal, then the files are different. add them to the return set.
                                    if (!sourceBytes.ByteArrayCompare(destinationBytes))
                                    {
                                        modifiedFiles.Add(string.Concat(source, file));
                                        break;
                                    }
                                }
                            } while (sourceBytes.Length > 0);
                        }
                        else
                        {
                            // if the files aren't the same size, obviously they are different. add them to the return set.
                            modifiedFiles.Add(string.Concat(source, file));
                        }
                    }
                }
            }

            var newFiles = new List<string>(base.NewFiles);
            var notInSourceFiles = new List<string>(base.NotInSource);
            var result = new ComparisonResult(newFiles, modifiedFiles, notInSourceFiles);

            return result;
        }
    }
}

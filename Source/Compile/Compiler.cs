﻿using System;
using System.IO;
using Optional;

namespace Apos.Content.Compile {
    /// <summary>
    /// Base class for building content.
    /// </summary>
    public class Compiler<T> {
        /// <summary>
        /// Implicitly creates FileStreams on the paths to build the content.
        /// The paths can be relative or absolute.
        /// </summary>
        public virtual void Build(string inputPath, string outputPath, Settings<T> settings) {
            Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
            using (FileStream input = new FileStream(inputPath, FileMode.Open))
            using (FileStream output = new FileStream(outputPath, FileMode.Create)) {
                Build(input, output, settings);
            }
        }
        /// <summary>
        /// Builds content.
        /// </summary>
        public virtual void Build(Stream input, Stream output, Settings<T> settings) { }
    }
}
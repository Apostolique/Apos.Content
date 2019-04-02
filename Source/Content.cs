using System;
using System.IO;
using Optional;

namespace Apos.Content {
    /// <summary>
    /// Base class for building and reading content.
    /// </summary>
    public class Content<T> {
        /// <summary>
        /// Implicitly creates FileStreams on the paths to build the content.
        /// The paths can be relative or absolute.
        /// </summary>
        public virtual void Build(string inputPath, string outputPath, Settings<T> settings) {
            using (FileStream input = new FileStream(inputPath, FileMode.Open))
            using (FileStream output = new FileStream(outputPath, FileMode.Create)) {
                Build(input, output, settings);
            }
        }
        /// <summary>
        /// Builds content.
        /// </summary>
        public virtual void Build(Stream input, Stream output, Settings<T> settings) { }
        /// <summary>
        /// Implicitly creates a FileStream on the given path.
        /// The path can be relative or absolute.
        /// </summary>
        /// <returns>
        /// Returns something only if the content can be parsed.
        /// </returns>
        public virtual Option<T> Read(string path, Context context) {
            using (FileStream input = new FileStream(path, FileMode.Open)) {
                return Read(input, context);
            }
        }
        /// <summary>
        /// Reads content.
        /// </summary>
        /// <returns>
        /// Returns something only if the content can be parsed.
        /// </returns>
        public virtual Option<T> Read(Stream input, Context context) {
            return Option.None<T>();
        }
    }
}
using System;
using System.IO;
using Optional;

namespace Apos.Content.Read {
    /// <summary>
    /// Base class for building and reading content.
    /// </summary>
    public class Reader<T> {
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
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Optional;

namespace Apos.Content.Compile {
    /// <summary>
    /// Text file in the json format.
    /// </summary>
    public class CompileJson<T> : Compiler<T, Settings<T>> {
        /// <summary>
        /// Builds a string content.
        /// </summary>
        public override void Build(Stream input, Stream output, Settings<T> settings) {
            using (StreamReader sr = new StreamReader(input))
            using (BsonDataWriter bw = new BsonDataWriter(output)) {
                JsonSerializer serializer = new JsonSerializer();
                string text = sr.ReadToEnd();
                serializer.Serialize(bw, text);
            }
        }
    }
}
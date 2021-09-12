using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace Apos.Content.Read {
    /// <summary>
    /// A json content is simply a file with text.
    /// </summary>
    public class JsonReader<T> : Reader<T> {
        /// <summary>
        /// Reads some json content into a specific class type.
        /// </summary>
        /// <returns>
        /// Returns something only if the content can be parsed.
        /// </returns>
        public override T Read(Stream input, Context context) {
            using (BsonDataReader br = new BsonDataReader(input)) {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<T>(br);
            }
        }
    }
}

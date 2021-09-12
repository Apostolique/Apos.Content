namespace Apos.Content.Compile {
    /// <summary>
    /// Base class used to define settings for specific content types.
    /// </summary>
    public class Settings<T> {
        /// <summary>
        /// Initializes a new Settings class to help build various content types.
        /// </summary>
        public Settings(Target target) {
            Target = target;
        }

        /// <summary>
        /// Target platform and graphics backend that the content should be built for.
        /// </summary>
        public Target Target { get; set; }
    }
}

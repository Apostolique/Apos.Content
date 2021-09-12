namespace Apos.Content.Compile {
    /// <summary>
    /// Target platform and graphics backend.
    /// </summary>
    public class Target {
        /// <summary>
        /// Initializes a target for building the content.
        /// </summary>
        public Target(TargetPlatform platform, TargetGraphicsBackend graphicsBackend) {
            Platform = platform;
            GraphicsBackend = graphicsBackend;
        }

        /// <summary>
        /// The environment for which the content should be built for.
        /// </summary>
        public TargetPlatform Platform { get; set; }
        /// <summary>
        /// The graphics backend for which the content should be built for.
        /// </summary>
        public TargetGraphicsBackend GraphicsBackend { get; set; }
    }

    /// <summary>
    /// Platforms supported by MonoGame.
    /// </summary>
    public enum TargetPlatform {
        Windows,
        Linux,
        MacOS,
        Android,
        iOS,
        PS4,
        XboxOne,
        Switch
    }
    /// <summary>
    /// Graphics backends supported by MonoGame.
    /// </summary>
    public enum TargetGraphicsBackend {
        OpenGL,
        DirectX,
        UWP
    }
}

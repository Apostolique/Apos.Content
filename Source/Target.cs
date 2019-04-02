namespace Apos.Content {
    public class Target {
        public Target(TargetPlatform platform, TargetGraphicsBackend graphicsBackend) {
            Platform = platform;
            GraphicsBackend = graphicsBackend;
        }

        public TargetPlatform Platform {
            get;
            set;
        }
        public TargetGraphicsBackend GraphicsBackend {
            get;
            set;
        }

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
        public enum TargetGraphicsBackend {
            OpenGL,
            DirectX,
            UWP
        }
    }
}
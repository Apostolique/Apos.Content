namespace Apos.Content {
    public class Settings<T> {
        public Settings(Target target) {
            Target = target;
        }

        public Target Target {
            get;
            set;
        }
    }
}
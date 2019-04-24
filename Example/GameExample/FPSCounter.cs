using System;

namespace GameExample {
    /// <summary>
    /// Compute the game's FPS and time per frame.
    /// </summary>
    class FPSCounter {
        public FPSCounter() { }

        public int FramesPerSecond {
            get;
            private set;
        } = 0;
        public int UpdatePerSecond {
            get;
            private set;
        } = 0;
        public double TimePerFrame {
            get;
            private set;
        } = 0;
        public double TimePerUpdate {
            get;
            private set;
        } = 0;

        public void Update(long elapsedTime) {
            _updateCounter++;

            timer += elapsedTime;
            if (timer <= _oneSecond) {
                return;
            }

            UpdatePerSecond = _updateCounter;
            FramesPerSecond = _framesCounter;
            _updateCounter = 0;
            _framesCounter = 0;
            timer -= _oneSecond;

            if (UpdatePerSecond > 0) {
                TimePerUpdate = Math.Truncate(1000d / UpdatePerSecond * 10000) / 10000;
            }
            if (FramesPerSecond > 0) {
                TimePerFrame = Math.Truncate(1000d / FramesPerSecond * 10000) / 10000;
            }
        }
        public void Draw() {
            _framesCounter++;
        }

        private int _oneSecond = 1000;
        private long timer = 0;
        private int _framesCounter = 0;
        private int _updateCounter = 0;
    }
}
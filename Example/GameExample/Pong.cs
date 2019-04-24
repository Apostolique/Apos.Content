using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Apos.Input;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System.Diagnostics;
using Optional;
using System;
using SpriteFontPlus;

namespace GameExample {
    public class Pong {
        public Pong() {
            _realGametime = new Stopwatch();
            _realGametime.Start();

            _up = new ConditionComposite();
            _up.AddSet(Keys.Up);

            _down = new ConditionComposite();
            _down.AddSet(Keys.Down);

            _reverse = new ConditionComposite();
            _reverse.AddSet(Keys.Enter);

            _pause = new ConditionComposite();
            _pause.AddSet(Keys.Space);

            _paddle1 = new RectangleF(440, 400, Assets.Paddle1.Width, Assets.Paddle1.Height);
            _paddle2 = new RectangleF(1470, 600, Assets.Paddle2.Width, Assets.Paddle2.Height);
            _ball = new RectangleF(713, 327, Assets.Ball.Width, Assets.Ball.Height);
            _oldBall = _ball;
            _bounds = new Rectangle(340, 205, Assets.Board.Width, Assets.Board.Height);
        }

        public void Update() {
            updateTime();
            _fps.Update(ElapsedTime);

            if (_pause.Pressed()) {
                _isPaused = !_isPaused;
            }

            if (!_isPaused) {
                if (_up.Held()) {
                    _paddle1.Y -= (float)(_paddleSpeed * ElapsedTime);
                }
                if (_down.Held()) {
                    _paddle1.Y += (float)(_paddleSpeed * ElapsedTime);
                }
                if (_reverse.Pressed()) {
                    reverseBall();
                }

                _oldBall.Position = _ball.Position;
                _ball.Position += _direction * (float)(_ballSpeed * ElapsedTime);

                _paddle2.Y = _ball.Y + _ball.Height / 2 - _paddle2.Height / 2;

                keepWithinBounds(ref _paddle1);
                keepWithinBounds(ref _paddle2);

                bounceBall(ref _ball, _oldBall, ref _direction);

                checkScore(ref _ball);
            }

        }
        public void Draw(SpriteBatch s) {
            _fps.Draw();
            s.GraphicsDevice.Clear(Color.Black);
            s.Begin();
            s.Draw(Assets.Background, Vector2.Zero, Color.White);
            s.Draw(Assets.Board, new Vector2(340, 205), Color.White);
            s.Draw(Assets.Paddle1, _paddle1.Position, Color.White);
            s.Draw(Assets.Paddle2, _paddle2.Position, Color.White);
            s.Draw(Assets.Ball, _oldBall.Position, Color.Red);
            s.Draw(Assets.Ball, _ball.Position, Color.White);
            s.DrawString(Assets.Font, "fps: " + _fps.FramesPerSecond, new Vector2(20, 20), Color.White);
            s.End();
        }

        ConditionComposite _up;
        ConditionComposite _down;
        ConditionComposite _reverse;
        ConditionComposite _pause;
        RectangleF _paddle1;
        RectangleF _paddle2;
        Rectangle _bounds;
        RectangleF _ball;
        RectangleF _oldBall;
        Vector2 _direction = Vector2.Normalize(new Vector2(1, 1));
        float _ballSpeed = 0.50f;
        float _paddleSpeed = 0.50f;
        bool _isPaused = false;

        private Stopwatch _realGametime;
        private long _lastUpdateTime = 0;
        private long _currentUpdateTime = 0;
        private FPSCounter _fps = new FPSCounter();

        private long ElapsedTime => _currentUpdateTime - _lastUpdateTime;
        private long TotalTime => _currentUpdateTime;

        private void reverseBall() {
            _direction = Vector2.Negate(_direction);
        }
        private void keepWithinBounds(ref RectangleF r) {
            if (r.Top < _bounds.Top) {
                r.Y = _bounds.Y;
            }
            if (r.Bottom > _bounds.Bottom) {
                r.Y = _bounds.Bottom - r.Height;
            }
        }
        private void bounceBall(ref RectangleF b, RectangleF oldB, ref Vector2 direction) {
            if (b.Top < _bounds.Top) {
                float diff = _bounds.Top - b.Top;
                b.Y += diff;
                direction *= new Vector2(1, -1);
                direction.Normalize();
            }
            if (b.Bottom > _bounds.Bottom) {
                float diff = b.Bottom - _bounds.Bottom;
                b.Y -= diff;
                direction *= new Vector2(1, -1);
                direction.Normalize();
            }

            if (b.Left < _paddle1.Right && oldB.Right > _paddle1.Left) {
                Vector2 a = b.Center + Vector2.Negate(direction) * 30f;
                Vector2 paddle1TopRight = new Vector2(_paddle1.Right, _paddle1.Top - b.Height / 2);
                Vector2 paddle1BottomRight = new Vector2(_paddle1.Right, _paddle1.Bottom + b.Height / 2);
                Vector2 paddle1Origin = _paddle1.Center - new Vector2(50, 0);

                bool found = false;
                Vector2 intersection = Vector2.Zero;
                findLineIntersection(a, b.Center, paddle1TopRight, paddle1BottomRight).MatchSome(v => {
                    found = true;
                    intersection = v;
                });
                if (found) {
                    direction = Vector2.Normalize(intersection - paddle1Origin);
                }
            }
            if (b.Right > _paddle2.Left && oldB.Left < _paddle2.Right) {
                Vector2 a = b.Center + Vector2.Negate(direction) * 30f;
                Vector2 paddle2TopRight = new Vector2(_paddle2.Right, _paddle2.Top - b.Height / 2);
                Vector2 paddle2BottomRight = new Vector2(_paddle2.Right, _paddle2.Bottom + b.Height / 2);
                Vector2 paddle2Origin = _paddle2.Center + new Vector2(50, 0);

                bool found = false;
                Vector2 intersection = Vector2.Zero;
                findLineIntersection(a, b.Center, paddle2TopRight, paddle2BottomRight).MatchSome(v => {
                    found = true;
                    intersection = v;
                });
                if (found) {
                    direction = Vector2.Normalize(intersection - paddle2Origin);
                }
            }
        }
        private void checkScore(ref RectangleF b) {
            if (b.Left < _bounds.Left) {
                //paddle1 lose.
                resetBall(ref b);
            }
            if (b.Right > _bounds.Right) {
                //paddle2 lose.
                resetBall(ref b);
            }
        }
        private void resetBall(ref RectangleF b) {
            b.Position = _bounds.Center - b.Size / 2;
            reverseBall();
        }
        /// <summary>
        /// A line is defined by two points. The first line is an infinite line. The second line is just a line segment.
        /// A line segment means it has a beginning and an end.
        /// </summary>
        private Option<Vector2> findLineIntersection(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3) {
            Vector2 s1 = new Vector2(p1.X - p0.X, p1.Y - p0.Y);
            Vector2 s2 = new Vector2(p3.X - p2.X, p3.Y - p2.Y);

            float s = (-s1.Y * (p0.X - p2.X) + s1.X * (p0.Y - p2.Y)) / (-s2.X * s1.Y + s1.X * s2.Y);
            float t = (s2.X * (p0.Y - p2.Y) - s2.Y * (p0.X - p2.X)) / (-s2.X * s1.Y + s1.X * s2.Y);

            //TODO: We need to handle when lines are parallel. We get a big rounding error mistake in that case.
            if (s >= 0 && s <= 1) {
                Vector2 a1 = new Vector2(p0.X + (t * s1.X), p0.Y + (t * s1.Y));
                Vector2 b1 = new Vector2(p2.X + (s * s2.X), p2.Y + (s * s2.Y));

                //This is a simple way to solve for precision errors.
                Point a2 = new Point((int)Math.Round(a1.X), (int)Math.Round(a1.Y));
                Point b2 = new Point((int)Math.Round(b1.X), (int)Math.Round(b1.Y));

                if (a2 == b2) {
                    return Option.Some(b1);
                }
            }
            return Option.None<Vector2>();
        }
        private void updateTime() {
            _lastUpdateTime = _currentUpdateTime;
            _currentUpdateTime = _realGametime.ElapsedMilliseconds;
        }
    }
}
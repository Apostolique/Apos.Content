﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Apos.Content.Read;
using Apos.Input;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameExample {
    public class Core : Game {
        public Core() {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1850;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.SynchronizeWithVerticalRetrace = false;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public static long ElapsedTime => _currentUpdateTime - _lastUpdateTime;
        public static long TotalTime => _currentUpdateTime;

        protected override void Initialize() {
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += WindowClientChanged;

            InputHelper.Game = this;

            base.Initialize();
        }
        private string AssemblyDirectory {
            get {
                string codeBase = AppDomain.CurrentDomain.BaseDirectory;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        protected override void LoadContent() {
            _realGametime = new Stopwatch();
            _realGametime.Start();

            _s = new SpriteBatch(GraphicsDevice);

            _context = new Context(Path.Combine(AssemblyDirectory, "Content"), GraphicsDevice);
            Assets.LoadLoadingAssets(_context);
            _loading = new Loading();
            _go = _loading;

            Thread thread = new Thread(LoadAssets);
            thread.Start();
        }
        private void LoadAssets() {
            Assets.LoadAssets(_context, DoneLoading);
        }
        private void DoneLoading() {
            _pong = new Pong();
            _go = _pong;
        }
        private void WindowClientChanged(object sender, EventArgs e) { }
        protected override void Update(GameTime gameTime) {
            UpdateTime();
            InputHelper.UpdateSetup();
            _fps.Update(ElapsedTime);
            _go.Update();
            InputHelper.UpdateCleanup();

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime) {
            _fps.Draw();
            _s.GraphicsDevice.Clear(Color.Black);
            _s.Begin();
            _go.Draw(_s);
            _s.DrawString(Assets.Font, $"fps: {_fps.FramesPerSecond}", new Vector2(20, 20), Color.White);
            _s.End();

            base.Draw(gameTime);
        }

        GameObject _go;
        Loading _loading;
        Pong _pong;
        GraphicsDeviceManager _graphics;
        SpriteBatch _s;
        Context _context;
        FPSCounter _fps = new FPSCounter();

        private Stopwatch _realGametime;
        private static long _lastUpdateTime = 0;
        private static long _currentUpdateTime = 0;

        private void UpdateTime() {
            _lastUpdateTime = _currentUpdateTime;
            _currentUpdateTime = _realGametime.ElapsedMilliseconds;
        }
    }
}

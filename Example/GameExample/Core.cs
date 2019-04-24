using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Optional;
using Apos.Content.Read;
using System.IO;
using System.Threading;
using System.Reflection;
using Apos.Input;
using System.Runtime.InteropServices;

namespace GameExample {
    public class Core : Game {
        public Core() {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1850;
            _graphics.PreferredBackBufferHeight = 900;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += WindowClientChanged;

            InputHelper.Game = this;

            base.Initialize();
        }
        private string AssemblyDirectory {
            get {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        protected override void LoadContent() {
            s = new SpriteBatch(GraphicsDevice);

            _context = new Context(Path.Combine(AssemblyDirectory, "Content"), GraphicsDevice);
            Assets.LoadLoadingAssets(_context);
            _loading = new Loading();
            _update = _loading.Update;
            _draw = _loading.Draw;

            Thread thread = new Thread(loadAssets);
            thread.Start();
        }
        private void loadAssets() {
            Assets.LoadAssets(_context, doneLoading);
        }
        private void doneLoading() {
            _pong = new Pong();
            _update = _pong.Update;
            _draw = _pong.Draw;
        }
        private void WindowClientChanged(object sender, EventArgs e) { }
        protected override void Update(GameTime gameTime) {
            InputHelper.UpdateSetup();
            _update();
            InputHelper.Update();

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime) {
            _draw(s);

            base.Draw(gameTime);
        }

        Action _update;
        Action<SpriteBatch> _draw;

        Loading _loading;
        Pong _pong;
        GraphicsDeviceManager _graphics;
        SpriteBatch s;
        Context _context;
    }
}
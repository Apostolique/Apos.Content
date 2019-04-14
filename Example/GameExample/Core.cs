using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Optional;
using Apos.Content.Read;
using System.IO;
using System.Threading;

namespace GameExample {
    public class Core : Game {
        public Core() {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += WindowClientChanged;

            base.Initialize();
        }
        protected override void LoadContent() {
            s = new SpriteBatch(GraphicsDevice);

            _context = new Context("bin", GraphicsDevice);
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
            _update();

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
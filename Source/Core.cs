﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Optional;

namespace Apos.Content {
    public class Core : Game {
        public Core() {
            _graphics = new GraphicsDeviceManager(this);
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

            ContentTexture ct = new ContentTexture();
            ContentString cs = new ContentString();

            Target target = new Target(Target.TargetPlatform.Windows, Target.TargetGraphicsBackend.OpenGL);
            Context context = new Context(target, GraphicsDevice);

            string buildPath = "bin/";
            string redImageFile = "RedImage";
            string helloFile = "Hello";

            string redImagePath = buildPath + redImageFile + ".xnb";
            string helloPath = buildPath + helloFile + ".xnb";

            // Read texture content.
            Option<Texture2D> texture = ct.Read(redImagePath, context);
            texture.MatchSome(t => {
                _redImage = t;
            });

            // Read string content.
            Option<string> textObject = cs.Read(helloPath, context);
            textObject.MatchSome(t => {
                Console.WriteLine(t);
            });
        }

        private void WindowClientChanged(object sender, EventArgs e) { }

        protected override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            s.Begin();
            s.Draw(_redImage, Vector2.Zero, Color.White);
            s.End();

            base.Draw(gameTime);
        }

        GraphicsDeviceManager _graphics;
        SpriteBatch s;
        Texture2D _redImage;
    }
}
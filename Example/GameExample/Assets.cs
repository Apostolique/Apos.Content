using System;
using System.Collections.Generic;
using System.IO;
using Apos.Content.Read;
using FontStashSharp;
using Microsoft.Xna.Framework.Graphics;

namespace GameExample {
    public static class Assets {
        public static void LoadAssets(Context context, Action done) {
            LoadTextures(context);
            done();
        }

        public static void LoadLoadingAssets(Context context) {
            Texture2DReader rt = new Texture2DReader();
            string loadingImageFile = "Loading";
            string loadingImagePath = Path.Combine(context.ContentPath, Path.ChangeExtension(loadingImageFile, ".xnb"));

            LoadingImage = rt.Read(loadingImagePath, context);

            LoadFont(context);
        }
        public static void LoadFont(Context context) {
            Apos.Content.Read.BinaryReader rb = new Apos.Content.Read.BinaryReader();
            string fontFile = "SourceCodePro-Medium";
            string fontPath = Path.Combine(context.ContentPath, Path.ChangeExtension(fontFile, ".xnb"));

            var fontSystem = new FontSystem();
            fontSystem.AddFont(rb.Read(fontPath, context));

            Font = fontSystem.GetFont(30);
        }
        public static void LoadTextures(Context context) {
            // Read texture content.
            Texture2DReader rt = new Texture2DReader();

            List<(string, Action<Texture2D>)> files = new List<(string, Action<Texture2D>)>() {
                ("RedImage", o => { RedImage = o; }),
                ("Background", o => { Background = o; }),
                ("Board", o => { Board = o; }),
                ("Ball", o => { Ball = o; }),
                ("Paddle1", o => { Paddle1 = o; }),
                ("Paddle2", o => { Paddle2 = o; }),
            };

            foreach ((string, Action<Texture2D>) file in files) {
                string path = Path.Combine(context.ContentPath, Path.ChangeExtension(file.Item1, ".xnb"));

                file.Item2(rt.Read(path, context));
            }
        }
        public static void LoadStrings(Context context) {
            // Read string content.
            Apos.Content.Read.StringReader cs = new Apos.Content.Read.StringReader();
            string helloFile = "Hello";
            string helloPath = Path.Combine(context.ContentPath, Path.ChangeExtension(helloFile, ".xnb"));

            Console.WriteLine(cs.Read(helloPath, context));
        }

        public static Texture2D LoadingImage;
        public static Texture2D RedImage;
        public static Texture2D Background;
        public static Texture2D Board;
        public static Texture2D Ball;
        public static Texture2D Paddle1;
        public static Texture2D Paddle2;
        public static DynamicSpriteFont Font;
    }
}

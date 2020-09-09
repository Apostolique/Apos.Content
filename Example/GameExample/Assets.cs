using System;
using System.Collections.Generic;
using System.IO;
using Apos.Content.Read;
using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;

namespace GameExample {
    public static class Assets {
        public static void LoadAssets(Context context, Action done) {
            LoadTextures(context);
            done();
        }

        public static void LoadLoadingAssets(Context context) {
            ReadTexture2D rt = new ReadTexture2D();
            string loadingImageFile = "Loading";
            string loadingImagePath = Path.Combine(context.ContentPath, Path.ChangeExtension(loadingImageFile, ".xnb"));

            rt.Read(loadingImagePath, context).MatchSome(t => {
                LoadingImage = t;
            });

            LoadFont(context);
        }
        public static void LoadFont(Context context) {
            ReadBinary rb = new ReadBinary();
            string fontFile = "SourceCodePro-Medium";
            string fontPath = Path.Combine(context.ContentPath, Path.ChangeExtension(fontFile, ".xnb"));

            rb.Read(fontPath, context).MatchSome(bs => {
                Font = DynamicSpriteFont.FromTtf(bs, 30);
            });
        }
        public static void LoadTextures(Context context) {
            // Read texture content.
            ReadTexture2D rt = new ReadTexture2D();

            List<Tuple<string, Action<Texture2D>>> files = new List<Tuple<string, Action<Texture2D>>>() {
                new Tuple<string, Action<Texture2D>>("RedImage", o => {RedImage = o;}),
                new Tuple<string, Action<Texture2D>>("Background", o => {Background = o;}),
                new Tuple<string, Action<Texture2D>>("Board", o => {Board = o;}),
                new Tuple<string, Action<Texture2D>>("Ball", o => {Ball = o;}),
                new Tuple<string, Action<Texture2D>>("Paddle1", o => {Paddle1 = o;}),
                new Tuple<string, Action<Texture2D>>("Paddle2", o => {Paddle2 = o;}),
            };

            foreach (Tuple<string, Action<Texture2D>> file in files) {
                string path = Path.Combine(context.ContentPath, Path.ChangeExtension(file.Item1, ".xnb"));

                rt.Read(path, context).MatchSome(t => {
                    file.Item2(t);
                });
            }
        }
        public static void LoadStrings(Context context) {
            // Read string content.
            ReadString cs = new ReadString();
            string helloFile = "Hello";
            string helloPath = Path.Combine(context.ContentPath, Path.ChangeExtension(helloFile, ".xnb"));

            cs.Read(helloPath, context).MatchSome(t => {
                Console.WriteLine(t);
            });
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

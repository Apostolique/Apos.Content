using System;
using System.IO;
using System.Threading;
using Apos.Content.Read;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Optional;

namespace GameExample {
    public static class Assets {
        public static void LoadAssets(Context context, Action done) {
            LoadTextures(context);
            done();
        }

        public static void LoadLoadingAssets(Context context) {
            ReadTexture2D ct = new ReadTexture2D();
            string loadingImageFile = "Loading";
            string loadingImagePath = Path.Combine(context.BuildPath, Path.ChangeExtension(loadingImageFile, ".xnb"));

            ct.Read(loadingImagePath, context).MatchSome(t => {
                LoadingImage = t;
            });
        }
        public static void LoadTextures(Context context) {
            // Read texture content.
            ReadTexture2D ct = new ReadTexture2D();
            string redImageFile = "RedImage";
            string redImagePath = Path.Combine(context.BuildPath, Path.ChangeExtension(redImageFile, ".xnb"));

            ct.Read(redImagePath, context).MatchSome(t => {
                RedImage = t;
            });
        }
        public static void LoadStrings(Context context) {
            // Read string content.
            ReadString cs = new ReadString();
            string helloFile = "Hello";
            string helloPath = Path.Combine(context.BuildPath, Path.ChangeExtension(helloFile, ".xnb"));

            cs.Read(helloPath, context).MatchSome(t => {
                Console.WriteLine(t);
            });
        }


        public static Texture2D LoadingImage;
        public static Texture2D RedImage;
    }
}
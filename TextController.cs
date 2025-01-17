using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class TextController : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            // Fonts
            FontGenerator mediumFont = LoadFont("sb/font/m", new FontDescription()
            {
                FontPath = "THEBOLDFONT-FREEVERSION.otf",
                FontSize = 48,
                Color = Color4.White,
            }
            // new FontShadow()
            // {
            //     Color = new Color4(0, 0, 0, 60),
            //     Thickness = 2
            // }
            );

            FontGenerator smallFont = LoadFont("sb/font/s", new FontDescription()
            {
                FontPath = "THEBOLDFONT-FREEVERSION.otf",
                FontSize = 24,
                Color = Color4.White,
            }
            // new FontShadow()
            // {
            //     Color = new Color4(0, 0, 0, 60),
            //     Thickness = 2
            // }
            );

            FontGenerator extraBigFont = LoadFont("sb/font/ex", new FontDescription()
            {
                FontPath = "THEBOLDFONT-FREEVERSION.otf",
                FontSize = 512,
                Color = Color4.White,
            }
            // new FontShadow()
            // {
            //     Color = new Color4(0, 0, 0, 60),
            //     Thickness = 2
            // }
            );

            // layers
            StoryboardLayer layer = GetLayer("Text Layer");
            StoryboardLayer layerG = GetLayer("Ghost Layer");
            StoryboardLayer layerD = GetLayer("Text Layer -- Diff Specific");

            //
            List<OsbSprite> introArtist = LyricLineSpriteFactoryHorizontal(layerD, mediumFont, $"MILKU's THINKING OF [{Beatmap.Name}]", new Vector2(320, 240));

            LyricInitializer(introArtist, 529, new Color4(218, 46, 127, 255));
            introArtist[0].MoveY(OsbEasing.OutBack, 706, 1061, 272, 240);
            SimpleFadeIn(introArtist, 706, 884);
            SimpleFadeOut(introArtist, 1061, 1328);

            List<OsbSprite> title = LyricLineSpriteFactoryHorizontal(layerG, mediumFont, "THINKING OF", new Vector2(320, 305));
            LyricInitializer(title, 1772, new Color4(218, 46, 127, 255));
            SimpleFadeIn(title, 1772, 2127);
            title[0].MoveY(OsbEasing.InOutExpo, 35144, 35854, 305, 110);
            title[0].MoveY(OsbEasing.InOutExpo, 45085, 45795, 110, 305);
            title[0].MoveY(OsbEasing.InOutExpo, 91949, 92659, 305, 110);
            title[0].MoveY(OsbEasing.InOutExpo, 101890, 102600, 110, 305);
            SimpleFadeOut(title, 126387, 128162);

            List<OsbSprite> artist = LyricLineSpriteFactoryHorizontal(layerG, smallFont, "THE MUSICAL GHOST", new Vector2(320, 330));
            LyricInitializer(artist, 1772, new Color4(50, 43, 59, 255));
            SimpleFadeIn(artist, 1772, 2127);
            SimpleFadeOut(artist, 126387, 128162);

            List<OsbSprite> three = LyricSpritesFactoryHorizontal(layer, extraBigFont, "3", new Vector2(0, 240));
            LyricInitializer(three, 45263, new Color4(218, 46, 127, 255));
            SimpleFadeIn(three, 45263, 45440, 1);
            three[0].MoveX(OsbEasing.OutBack, 45263, 45440, -25, 0);
            SimpleFadeOut(three, 45618, 45795);

            List<OsbSprite> two = LyricSpritesFactoryHorizontal(layer, extraBigFont, "2", new Vector2(640, 240));
            LyricInitializer(two, 45618, new Color4(218, 46, 127, 255));
            SimpleFadeIn(two, 45618, 45795, 1);
            two[0].MoveX(OsbEasing.OutBack, 45618, 45795, 665, 640);
            SimpleFadeOut(two, 45973, 46150);

            List<OsbSprite> one = LyricSpritesFactoryHorizontal(layer, extraBigFont, "1", new Vector2(0, 240));
            LyricInitializer(one, 45973, new Color4(218, 46, 127, 255));
            SimpleFadeIn(one, 45973, 46150, 1);
            one[0].MoveX(OsbEasing.OutBack, 45973, 46150, -25, 0);
            SimpleFadeOut(one, 46328, 46505);

            List<OsbSprite> yeah = LyricLineSpriteFactoryHorizontal(layer, extraBigFont, "YEAH!", new Vector2(640, 240));
            LyricInitializer(yeah, 46328, new Color4(218, 46, 127, 255));
            SimpleFadeIn(yeah, 46328, 46505, 0.5);
            yeah[0].MoveX(OsbEasing.OutBack, 46328, 46505, 665, 320);
            SimpleFadeOut(yeah, 46683, 46860);

            List<OsbSprite> get = LyricLineSpriteFactoryHorizontal(layerG, extraBigFont, "GET", new Vector2(0, 165));
            LyricInitializer(get, 102245, new Color4(218, 46, 127, 255));
            SimpleFadeIn(get, 102245, 102600, 0.25);
            get[0].MoveX(OsbEasing.OutExpo, 102245, 102600, -25, 320);
            get[0].Scale(OsbEasing.InExpo, 103310, 103488, 0.5, 1);
            SimpleFadeOut(get, 103310, 103488);

            List<OsbSprite> ready = LyricLineSpriteFactoryHorizontal(layerG, extraBigFont, "READY", new Vector2(640, 395));
            LyricInitializer(ready, 102245, new Color4(218, 46, 127, 255));
            SimpleFadeIn(ready, 102422, 102777, 0.25);
            ready[0].MoveX(OsbEasing.OutExpo, 102422, 102777, 665, 320);
            ready[0].Scale(OsbEasing.InExpo, 103310, 103488, 0.5, 1);
            SimpleFadeOut(ready, 103310, 103488);



        }

        public static void LyricInitializer(List<OsbSprite> sprites, int time, Color4 color)
        {
            float fontScale = 0.5f;
            foreach (OsbSprite sprite in sprites)
            {
                sprite.Fade(time, 0);
                sprite.Scale(time, fontScale);
                sprite.Color(time, color);
            }
        }

        public static void FadeNoTransition(List<OsbSprite> sprites, int time, double opacity)
        {
            foreach (OsbSprite sprite in sprites)
            {
                sprite.Fade(time, opacity);
            }
        }

        public static void SimpleFadeIn(List<OsbSprite> sprites, int startTime, int endTime, double opacity = 1)
        {
            foreach (OsbSprite sprite in sprites)
            {
                sprite.Fade(startTime, endTime, 0, opacity);
            }
        }

        public static void SimpleFadeOut(List<OsbSprite> sprites, int startTime, int endTime)
        {

            foreach (OsbSprite sprite in sprites)
            {
                sprite.Fade(startTime, endTime, sprite.OpacityAt(startTime), 0);
            }
        }

        public static List<OsbSprite> LyricSpritesFactoryHorizontal(StoryboardLayer layer, FontGenerator font, string lyric, Vector2 pos)
        {
            float letterX = pos.X;
            float letterY = pos.Y;
            float fontScale = 0.5f;

            float lineWidth = 0f;
            float lineHeight = 0f;

            foreach (char letter in lyric)
            {
                FontTexture texture = font.GetTexture(letter.ToString());

                lineWidth += texture.BaseWidth * fontScale;
                lineHeight = Math.Max(lineHeight, texture.BaseHeight * fontScale);
            }

            List<OsbSprite> sprites = [];

            foreach (char letter in lyric)
            {
                FontTexture texture = font.GetTexture(letter.ToString());

                if (!texture.IsEmpty)
                {
                    // Vector2 position = new Vector2(letterX, letterY) + texture.OffsetFor(OsbOrigin.Centre) * fontScale;
                    Vector2 position = new(letterX - texture.BaseWidth / 4 + texture.OffsetFor(OsbOrigin.Centre).X - lineWidth * fontScale, letterY);

                    OsbSprite sprite = layer.CreateSprite(texture.Path, OsbOrigin.Centre, position);

                    sprites.Add(sprite);
                }
                letterX += texture.BaseWidth * fontScale;
            }
            return sprites;
        }

        public static List<OsbSprite> LyricLineSpriteFactoryHorizontal(StoryboardLayer layer, FontGenerator font, string lyric, Vector2 pos)
        {
            float fontScale = 0.5f;
            OsbOrigin origin = OsbOrigin.Centre;

            var texture = font.GetTexture(lyric);
            var position = new Vector2(pos.X - texture.BaseWidth * fontScale * 0.5f + texture.OffsetFor(origin).X * fontScale, pos.Y);

            var sprite = layer.CreateSprite(texture.Path, origin, position);

            // for compatibility with other effects + to bring me less stress, i put the sprite in a list 
            return [sprite];
        }
    }
}

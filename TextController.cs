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
            },
            new FontShadow()
            {
                Color = new Color4(0, 0, 0, 60),
                Thickness = 2
            }
            );

            // layers
            StoryboardLayer layer = GetLayer("Text Layer");

            //
            List<OsbSprite> introArtist = LyricLineSpriteFactoryHorizontal(layer, mediumFont, "THEMUSICALGHOST", new Vector2(320, 240));

            LyricInitializer(introArtist, 529, new Color4(218, 46, 127, 255));
            introArtist[0].MoveY(OsbEasing.OutBack, 706, 1061, 272, 240);
            SimpleFadeIn(introArtist, 706, 884);
            SimpleFadeOut(introArtist, 1061, 1328);





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

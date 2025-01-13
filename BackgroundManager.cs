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
    public class BackgroundManager : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            GetLayer("Remove BG").CreateSprite(Beatmap.BackgroundPath).Fade(0, 0);

            StoryboardLayer layer = GetLayer("BG Layer");

            OsbSprite bg = layer.CreateSprite("sb/p.png", OsbOrigin.Centre, new Vector2(320, 240));
            bg.Fade(0, 1);
            bg.Fade(128517, 0);
            bg.Scale(0, 854.0f / GetMapsetBitmap(bg.TexturePath).Width);
            bg.Color(0, Color4.White);
            bg.Color(1417, new Color4(232, 222, 254, 255));


            OsbSprite circle = layer.CreateSprite("sb/circle.png", OsbOrigin.Centre, new Vector2(320, 240));
            circle.Fade(1061, 1239, 0, 1);
            circle.Fade(1417, 0);
            circle.Scale(OsbEasing.InExpo, 1061, 1417, 0, 854.0f / GetMapsetBitmap(circle.TexturePath).Width * 2);



        }
    }
}

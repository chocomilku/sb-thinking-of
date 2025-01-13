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
        public static double BeatDuration;
        public override void Generate()
        {
            BeatDuration = Beatmap.TimingPoints.First().BeatDuration;
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

            // outline
            OsbSprite outline = layer.CreateSprite("sb/outline.png", OsbOrigin.Centre, new Vector2(320, 240));
            outline.Fade(12422, 0);
            OutlineJumpscare(outline, 12422);

            // GhostController
            GhostController ghost = new GhostController(this, layer, "sb/ghost.png", new Vector2(320, 200));
            ghost.InitSprite(1061);
            ghost.FadeIn(1417, 2038);
            ghost.BopMovement(1417, 126387, (int)BeatDuration * 8, 15);
            ghost.FadeOut(126387);
        }

        public void OutlineJumpscare(OsbSprite sprite, int time)
        {
            sprite.Fade(time, time + BeatDuration / 2, 0, 1);
            sprite.Fade(time + BeatDuration, 0);
            sprite.Scale(OsbEasing.In, time, time + BeatDuration, 0, 854.0f / GetMapsetBitmap(sprite.TexturePath).Width * 2);
        }

        class GhostController
        {
            private readonly StoryboardObjectGenerator ctx;
            private readonly StoryboardLayer layer;
            private readonly OsbSprite sprite;
            private readonly float scale = 1f;

            public GhostController(StoryboardObjectGenerator ctx, StoryboardLayer layer, string path, Vector2 initPos)
            {
                this.ctx = ctx;
                this.layer = layer;

                sprite = layer.CreateSprite(path, OsbOrigin.Centre, initPos);
            }

            public void InitSprite(int time)
            {
                sprite.Fade(time, 0);
                sprite.Scale(time, 854.0f / ctx.GetMapsetBitmap(sprite.TexturePath).Width * 0.125f * scale);
            }

            public void FadeIn(int startTime, int endTime)
            {
                sprite.Fade(startTime, endTime, 0, 1);
            }
            public void FadeOut(int time)
            {
                sprite.Fade(time, 0);
            }

            public void BopMovement(int startTime, int endTime, int interval, int amount)
            {
                int loopCount = (endTime - startTime) / interval;
                int dur = interval / 4;
                float initialY = sprite.PositionAt(startTime).Y;

                sprite.StartLoopGroup(startTime, loopCount);
                sprite.MoveY(OsbEasing.OutSine, 0, 0 + dur * 1, initialY, initialY + amount);
                sprite.MoveY(OsbEasing.InOutSine, 0 + dur * 1, 0 + dur * 3, initialY + amount, initialY - amount);
                sprite.MoveY(OsbEasing.InSine, 0 + dur * 3, 0 + dur * 4, initialY - amount, initialY);
                sprite.EndGroup();
            }

            public OsbSprite GetSprite()
            {
                return sprite;
            }
        }
    }
}

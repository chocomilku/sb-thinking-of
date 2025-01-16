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
        static float ScreenWidth = OsuHitObject.WidescreenStoryboardSize.X;
        static float ScreenHeight = OsuHitObject.WidescreenStoryboardSize.Y;

        static float PlayfieldHeight = OsuHitObject.PlayfieldSize.Y;
        static float PlayfieldWidth = OsuHitObject.PlayfieldSize.X;

        static float ScreenBoundsLeft = OsuHitObject.WidescreenStoryboardBounds.Left;
        static float ScreenBoundsRight = OsuHitObject.WidescreenStoryboardBounds.Right;
        static float ScreenBoundsBottom = OsuHitObject.WidescreenStoryboardBounds.Bottom;
        static float ScreenBoundsTop = OsuHitObject.WidescreenStoryboardBounds.Top;

        static float PlayfieldBoundsTop = OsuHitObject.PlayfieldToStoryboardOffset.Y;
        static float PlayfieldBoundsBottom = PlayfieldHeight + OsuHitObject.PlayfieldToStoryboardOffset.Y;
        static float PlayfieldBoundsLeft = OsuHitObject.PlayfieldToStoryboardOffset.X;
        static float PlayfieldBoundsRight = PlayfieldWidth + OsuHitObject.PlayfieldToStoryboardOffset.X;
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
            OutlineJumpscare(outline, 114848);

            // GhostController
            StoryboardLayer ghostLayer = GetLayer("Ghost Layer");
            GhostController ghost = new GhostController(this, ghostLayer, "sb/ghost.png", new Vector2(320, 200));
            ghost.InitSprite(1061);
            ghost.FadeIn(1417, 2038);
            ghost.BopMovement(1417, 126387, (int)BeatDuration * 8, 15);
            ghost.FadeOut(126387);

            List<(int, int)> ghostBeams = [(24138, 25203), (25559, 25914), (26091, 26446), (26624, 27689), (28399, 28754), (28931, 29286), (29819, 30884), (31239, 31594), (31772, 32127), (32304, 33369), (34079, 34434), (34612, 34967), (35144, 35499), (46860, 47570), (47748, 47925), (48103, 48458), (48990, 49168), (49345, 49523), (49701, 50411), (50588, 50766), (50943, 51298), (51831, 52008), (52186, 52363), (52541, 53073), (53783, 54138), (54671, 54848), (55026, 55203), (55381, 55736), (55914, 56269), (56446, 56801), (56979, 57334), (57511, 57866), (57866, 58221), (59109, 59286), (59464, 59996), (60706, 60884), (61061, 61416), (62304, 62659), (63192, 63369), (63547, 63724), (63902, 64257), (64612, 64789), (64908, 65085), (65144, 65677), (66032, 66387), (66742, 67097), (67274, 67630), (67807, 68162), (68340, 68695), (68872, 69050), (69227, 69405)];

            bool goLeft = true;
            foreach ((int, int) beam in ghostBeams)
            {
                ghost.GhostBeam(beam.Item1, beam.Item2, goLeft);
                goLeft = !goLeft;
            }


            // particles
            StoryboardLayer particleLayer = GetLayer("Particle Layer");
            ParticleGoingUp(particleLayer, "sb/icons/star.png", 12777, 24138, 16, 60, 0, (int)BeatDuration * 2, new Color4(218, 46, 127, 255));
            ParticleGoingUp(particleLayer, "sb/icons/circle.png", 12777, 24138, 16, 60, 0, (int)BeatDuration * 2, new Color4(218, 46, 127, 255));
            ParticleGoingUp(particleLayer, "sb/icons/rectangle.png", 12777, 24138, 16, 60, 0, (int)BeatDuration * 2, new Color4(218, 46, 127, 255));
            ParticleGoingUp(particleLayer, "sb/icons/triangle.png", 12777, 24138, 16, 60, 0, (int)BeatDuration * 2, new Color4(218, 46, 127, 255));

            ParticleGoingUp(particleLayer, "sb/icons/clap.png", 69582, 80943, 64, 40, 0, (int)BeatDuration * 1, new Color4(218, 46, 127, 255));
            ParticleGoingUp(particleLayer, "sb/icons/drum.png", 80943, 92304, 64, 40, 0, (int)BeatDuration * 1, new Color4(218, 46, 127, 255));

            ParticleGoingUp(particleLayer, "sb/icons/triangle.png", 103665, 126387, 32, 40, 0, (int)BeatDuration * 1, new Color4(218, 46, 127, 255));
            ParticleGoingUp(particleLayer, "sb/icons/circle.png", 103665, 126387, 32, 40, 0, (int)BeatDuration * 1, new Color4(218, 46, 127, 255));
            ParticleGoingUp(particleLayer, "sb/icons/star.png", 103665, 126387, 32, 40, 0, (int)BeatDuration * 1, new Color4(218, 46, 127, 255));
            ParticleGoingUp(particleLayer, "sb/icons/rectangle.png", 103665, 126387, 32, 40, 0, (int)BeatDuration * 1, new Color4(218, 46, 127, 255));
            ParticleGoingUp(particleLayer, "sb/icons/drum.png", 103665, 126387, 32, 40, 0, (int)BeatDuration * 1, new Color4(218, 46, 127, 255));
            ParticleGoingUp(particleLayer, "sb/icons/clap.png", 103665, 126387, 32, 40, 0, (int)BeatDuration * 1, new Color4(218, 46, 127, 255));
        }

        void ParticleGoingUp(StoryboardLayer layer, string particlePath, int startTime, int endTime, int particleCount, int posYAmount, int posXRange, int delay, Color4 color)
        {
            OsbEasing Easing = OsbEasing.OutCubic;
            float ColorVariance = 0.3f;

            int randomFactor = endTime - startTime - delay;
            List<int> delayValues = [];

            for (int j = 0; j <= randomFactor; j += delay)
            {
                delayValues.Add(j);
            }

            for (int i = 0; i < particleCount; i++)
            {
                int selectedDelayValue = delayValues[Random(-10, delayValues.Count) < 0 ? 0 : Random(0, delayValues.Count)];
                int currentTime = selectedDelayValue + startTime;

                float startX = Random(ScreenBoundsLeft, ScreenBoundsRight);
                float startY;

                if (selectedDelayValue <= 0)
                {

                    startY = Random(ScreenBoundsBottom, ScreenBoundsTop);
                }
                else
                {
                    startY = ScreenBoundsBottom;
                }

                Log($"CurrentTime: {currentTime}, d: {selectedDelayValue}, StartX: {startX}, StartY: {startY}");

                OsbSprite p = layer.CreateSprite(particlePath, OsbOrigin.Centre, new Vector2(startX, startY));

                double Scale;
                if (i % 10 == 0) Scale = Random(0.4, 0.55);
                else Scale = Random(0.2, 0.35);

                p.Fade(currentTime, currentTime + BeatDuration / 2, 0, 0.15);
                p.Scale(currentTime, Scale);
                // p.Additive(currentTime);

                Color4 newColor = color;
                if (ColorVariance > 0)
                {
                    ColorVariance = MathHelper.Clamp(ColorVariance, 0, 1);

                    var hsba = Color4.ToHsl(color);
                    var sMin = Math.Max(0, hsba.Y - ColorVariance * 0.5f);
                    var sMax = Math.Min(sMin + ColorVariance, 1);
                    var vMin = Math.Max(0, hsba.Z - ColorVariance * 0.5f);
                    var vMax = Math.Min(vMin + ColorVariance, 1);

                    newColor = Color4.FromHsl(new Vector4(
                         hsba.X,
                         (float)Random(sMin, sMax),
                         (float)Random(vMin, vMax),
                         hsba.W));
                }

                p.Color(currentTime, newColor);

                while (!(currentTime >= endTime))
                {
                    // Log($"CurrentTime: {currentTime}, NextTime: {currentTime + delay}");
                    if (p.PositionAt(currentTime).Y - posYAmount < ScreenBoundsTop - PlayfieldBoundsTop)
                    {
                        p.Fade(currentTime, 0);
                        break;
                    }

                    if (currentTime + delay >= endTime)
                    {
                        p.Fade(endTime - BeatDuration / 2, endTime, p.OpacityAt(endTime - BeatDuration / 2), 0);
                    }

                    p.MoveX(OsbEasing.None, currentTime, currentTime + delay, p.PositionAt(currentTime).X, p.PositionAt(currentTime).X + Random(-posXRange, +posXRange));
                    p.MoveY(Easing, currentTime, currentTime + delay, p.PositionAt(currentTime).Y, p.PositionAt(currentTime).Y - posYAmount);

                    currentTime += delay;

                }

            }
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

            public void BopMovement(OsbSprite s, int startTime, int endTime, int interval, int amount)
            {
                int loopCount = (endTime - startTime) / interval;
                int dur = interval / 4;
                float initialY = s.PositionAt(startTime).Y;
                float X = s.PositionAt(startTime).X;

                s.StartLoopGroup(startTime, loopCount);
                s.Move(OsbEasing.OutSine, 0, 0 + dur * 1, new Vector2(X, initialY), new Vector2(X, initialY + amount));
                s.Move(OsbEasing.InOutSine, 0 + dur * 1, 0 + dur * 3, new Vector2(X, initialY + amount), new Vector2(X, initialY - amount));
                s.Move(OsbEasing.InSine, 0 + dur * 3, 0 + dur * 4, new Vector2(X, initialY - amount), new Vector2(X, initialY));
                s.EndGroup();
            }

            public void InitSprite(OsbSprite s, int time, float scale)
            {
                s.Scale(time, 854.0f / ctx.GetMapsetBitmap(s.TexturePath).Width * 0.125f * scale);
            }

            public void GhostBeam(int startTime, int endTime, bool toLeft = true, bool additive = false)
            {
                float beam1Opacity = 0.2f;
                float beam2Opacity = 0.1f;
                Vector2 beam1Pos = toLeft ? new Vector2(150, 220) : new Vector2(490, 220);
                Vector2 beam2Pos = toLeft ? new Vector2(-75, 220) : new Vector2(715, 220);


                OsbSprite beam1 = layer.CreateSprite(sprite.TexturePath, OsbOrigin.Centre);
                InitSprite(beam1, startTime - (int)BeatDuration / 4, 1.5f);
                if (additive) beam1.Additive(startTime - BeatDuration / 4);
                beam1.Move(OsbEasing.InSine, startTime - BeatDuration / 4, startTime, sprite.PositionAt(startTime - BeatDuration / 4), beam1Pos);
                beam1.Fade(OsbEasing.InExpo, startTime - BeatDuration / 4, startTime, 0, beam1Opacity);
                BopMovement(beam1, startTime, endTime, (int)BeatDuration / 4, 5);
                beam1.Fade(endTime - BeatDuration / 4, endTime, beam1Opacity, 0);

                OsbSprite beam2 = layer.CreateSprite(sprite.TexturePath, OsbOrigin.Centre);
                InitSprite(beam2, startTime - (int)BeatDuration / 4, 3f);
                if (additive) beam2.Additive(startTime - BeatDuration / 4);
                beam2.Move(OsbEasing.InSine, startTime - BeatDuration / 4, startTime, sprite.PositionAt(startTime - BeatDuration / 4), beam2Pos);
                beam2.Fade(OsbEasing.InExpo, startTime - BeatDuration / 4, startTime, 0, beam2Opacity);
                BopMovement(beam2, startTime, endTime, (int)BeatDuration / 4, 5);
                beam2.Fade(endTime - BeatDuration / 4, endTime, beam2Opacity, 0);


            }

            public OsbSprite GetSprite()
            {
                return sprite;
            }
        }
    }
}

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
            bg.Fade(AudioDuration, 0);
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
            ghost.BopMovement(1417, 35499, (int)BeatDuration * 8, 15);
            ghost.InitBeamSprites(23783);
            ghost.FadeOut(35144, 35499);

            ghost.FadeIn(46683, 46860);
            ghost.BopMovement(46683, 92304, (int)BeatDuration * 8, 15);
            ghost.FadeOut(91949, 92304);

            ghost.FadeIn(103310, 103665);
            ghost.BopMovement(103488, 128162, (int)BeatDuration * 8, 15);
            ghost.FadeOut(126387, 128162);


            List<(int, int)> ghostBeams1 = [(24138, 25203), (25559, 25914), (26091, 26446), (26624, 27689), (28399, 28754), (28931, 29286), (29819, 30884), (31239, 31594), (31772, 32127), (32304, 33369), (34079, 34434), (34612, 34967), (35144, 35499)];

            List<(int, int)> ghostBeams2 = [(46860, 47570), (47748, 47925), (48103, 48458), (48990, 49168), (49345, 49523), (49701, 50411), (50588, 50766), (50943, 51298), (51831, 52008), (52186, 52363), (52541, 53073), (53783, 54138), (54671, 54848), (55026, 55203), (55381, 55736), (55914, 56269), (56446, 56801), (56979, 57334), (57511, 57689), (57866, 58221)];

            List<(int, int)> ghostBeams3 = [(59109, 59286), (59464, 59996), (60706, 60884), (61061, 61416), (62304, 62659), (63192, 63369), (63547, 63724), (63902, 64257), (64612, 64789), (64878, 65056), (65144, 65677), (66032, 66387), (66742, 67097), (67274, 67630), (67807, 68162), (68340, 68695), (68872, 69050), (69227, 69405)];

            bool goLeft = true;
            foreach ((int, int) beam in ghostBeams1)
            {
                ghost.GhostBeam(beam.Item1, beam.Item2, goLeft);
                goLeft = !goLeft;
            }

            foreach ((int, int) beam in ghostBeams2)
            {
                ghost.GhostBeam(beam.Item1, beam.Item2, goLeft);
                goLeft = !goLeft;
            }

            foreach ((int, int) beam in ghostBeams3)
            {
                ghost.GhostBeam(beam.Item1, beam.Item2, goLeft);
                goLeft = !goLeft;
            }


            // particles
            StoryboardLayer particleLayer = GetLayer("Particle Layer");
            ParticleGoingUp(particleLayer, "sb/icons/star.png", 13132, 24138, 16, 60, 0, (int)BeatDuration * 2, new Color4(218, 46, 127, 255));
            ParticleGoingUp(particleLayer, "sb/icons/circle.png", 13132, 24138, 16, 60, 0, (int)BeatDuration * 2, new Color4(218, 46, 127, 255));
            ParticleGoingUp(particleLayer, "sb/icons/rectangle.png", 13132, 24138, 16, 60, 0, (int)BeatDuration * 2, new Color4(218, 46, 127, 255));
            ParticleGoingUp(particleLayer, "sb/icons/triangle.png", 13132, 24138, 16, 60, 0, (int)BeatDuration * 2, new Color4(218, 46, 127, 255));

            ParticleGoingUp(particleLayer, "sb/icons/clap.png", 69582, 80943, 75, 40, 0, (int)BeatDuration * 1, new Color4(218, 46, 127, 255));
            ParticleGoingUp(particleLayer, "sb/icons/drum.png", 80943, 92304, 60, 40, 0, (int)BeatDuration * 1, new Color4(218, 46, 127, 255));

            ParticleGoingUp(particleLayer, "sb/icons/triangle.png", 103665, 126387, 50, 60, 5, (int)BeatDuration * 1, new Color4(218, 46, 127, 255));
            ParticleGoingUp(particleLayer, "sb/icons/circle.png", 103665, 126387, 50, 60, 5, (int)BeatDuration * 1, new Color4(218, 46, 127, 255));
            ParticleGoingUp(particleLayer, "sb/icons/star.png", 103665, 126387, 50, 60, 5, (int)BeatDuration * 1, new Color4(218, 46, 127, 255));
            ParticleGoingUp(particleLayer, "sb/icons/rectangle.png", 103665, 126387, 50, 60, 5, (int)BeatDuration * 1, new Color4(218, 46, 127, 255));
            ParticleGoingUp(particleLayer, "sb/icons/drum.png", 103665, 126387, 50, 60, 5, (int)BeatDuration * 1, new Color4(218, 46, 127, 255));
            ParticleGoingUp(particleLayer, "sb/icons/clap.png", 103665, 126387, 50, 60, 5, (int)BeatDuration * 1, new Color4(218, 46, 127, 255));

            // frames
            StoryboardLayer frameLayer = GetLayer("Frame Layer");

            FrameGenerator(frameLayer, "sb/frames/1/.jpg", 35499, 41180, (int)BeatDuration * 2, true, false);
            FrameGenerator(frameLayer, "sb/frames/1/.jpg", 41180, 44020, (int)BeatDuration * 1, false, false);
            FrameGenerator(frameLayer, "sb/frames/1/.jpg", 44020, 44553, (int)BeatDuration / 2, false, false);
            FrameGenerator(frameLayer, "sb/frames/1/.jpg", 44553, 45440, (int)BeatDuration / 4, false, true);

            FrameGenerator(frameLayer, "sb/frames/2/.jpg", 92304, 97985, (int)BeatDuration * 2, true, false);
            FrameGenerator(frameLayer, "sb/frames/2/.jpg", 97985, 100825, (int)BeatDuration * 1, false, false);
            FrameGenerator(frameLayer, "sb/frames/2/.jpg", 100825, 101357, (int)BeatDuration / 2, false, false);
            FrameGenerator(frameLayer, "sb/frames/2/.jpg", 101357, 102245, (int)BeatDuration / 4, false, true);


        }

        void FrameGenerator(StoryboardLayer layer, string path, int startTime, int endTime, int interval, bool startFade = true, bool endFade = true)
        {
            OsbAnimation frame = layer.CreateAnimation(path, 4, interval, OsbLoopType.LoopForever, OsbOrigin.Centre, new Vector2(320, 220));
            if (startFade)
            {
                frame.Fade(startTime, startTime + BeatDuration, 0, 1);
            }
            else
            {
                frame.Fade(startTime, 1);
            }
            frame.Scale(startTime, 480.0 / 1080 * 0.33);

            if (endFade)
            {
                frame.Fade(endTime - BeatDuration / 2, endTime, 1, 0);
            }
            else
            {
                frame.Fade(endTime, 0);
            }
        }



        void ParticleGoingUp(StoryboardLayer layer, string particlePath, int startTime, int endTime, int particleCount, int posYAmount, int posXRange, int delay, Color4 color)
        {
            OsbEasing Easing = OsbEasing.OutCubic;
            float ColorVariance = 0f;

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

                OsbSprite p = layer.CreateSprite(particlePath, OsbOrigin.Centre, new Vector2(startX, startY));

                double Scale;
                if (i % 10 == 0) Scale = Random(0.4, 0.55);
                else Scale = Random(0.2, 0.35);

                p.Fade(currentTime, currentTime + BeatDuration / 2, 0, 0.25);
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
            private readonly OsbSprite sprite;
            private readonly float scale = 1f;
            private readonly OsbSprite beam1;
            private readonly OsbSprite beam2;

            public GhostController(StoryboardObjectGenerator ctx, StoryboardLayer layer, string path, Vector2 initPos)
            {
                this.ctx = ctx;

                sprite = layer.CreateSprite(path, OsbOrigin.Centre, initPos);
                beam1 = layer.CreateSprite(sprite.TexturePath, OsbOrigin.Centre);
                beam2 = layer.CreateSprite(sprite.TexturePath, OsbOrigin.Centre);
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
            public void FadeOut(int startTime, int endTime)
            {
                sprite.Fade(startTime, endTime, 1, 0);
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

            public static void BopMovement(OsbSprite s, int startTime, int endTime, int interval, int amount)
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

            public void InitBeamSprites(int time)
            {
                beam1.Scale(time, 854.0f / ctx.GetMapsetBitmap(beam1.TexturePath).Width * 0.125f * 1.5f);

                beam2.Scale(time, 854.0f / ctx.GetMapsetBitmap(beam2.TexturePath).Width * 0.125f * 3f);
            }

            public void GhostBeam(int startTime, int endTime, bool toLeft = true, bool additive = false)
            {
                float beam1Opacity = 0.25f;
                float beam2Opacity = 0.15f;
                Vector2 beam1Pos = toLeft ? new Vector2(150, 220) : new Vector2(515, 220);
                Vector2 beam2Pos = toLeft ? new Vector2(-75, 220) : new Vector2(745, 220);

                int actualStartTime = startTime - (int)BeatDuration / 4;

                ctx.Log($"startTime: {startTime}, actualStartTime: {actualStartTime}, endTime: {endTime}");

                if (additive) beam1.Additive(actualStartTime);
                beam1.Move(OsbEasing.InSine, actualStartTime, startTime, sprite.PositionAt(actualStartTime), beam1Pos);
                beam1.Fade(OsbEasing.InExpo, actualStartTime, startTime, 0, beam1Opacity);
                BopMovement(beam1, startTime, endTime, (int)BeatDuration / 4, 5);
                beam1.Fade(endTime - BeatDuration / 4, endTime, beam1Opacity, 0);

                if (additive) beam2.Additive(actualStartTime);
                beam2.Move(OsbEasing.InSine, actualStartTime, startTime, sprite.PositionAt(actualStartTime), beam2Pos);
                beam2.Fade(OsbEasing.InExpo, actualStartTime, startTime, 0, beam2Opacity);
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

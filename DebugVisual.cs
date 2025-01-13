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
using System.Net.Security;

namespace StorybrewScripts
{
    public class DebugVisual : StoryboardObjectGenerator
    {
        public double Starttime = 0;
        public double Endtime = 0;

        [Group("FilePath")]
        [Configurable]
        public string FilePath = "sb/p.png";



        [Group("Centre Playfield Dot")]

        [Description("Enable Dot on center of playfield.")]
        [Configurable]
        public bool CenterPlayfieldDot = false;


        [Configurable]
        public Color4 PlayfieldColor = new Color4(1, 0, 0, 1);

        [Group("Centre Screen Dot")]
        [Description("Enable Dot on center of the Screen.")]
        [Configurable]
        public bool CenterScreenDot = false;

        [Configurable]
        public Color4 ScreenColor = new Color4(1, 0, 0, 1);

        [Group("Grid Lines Playfield")]
        [Description("Enable grid lines aligned at the center of the playfield. The default setting is a 2x2 grid.")]
        [Configurable]
        public bool GridLinePlayfields = false;
        [Configurable]
        public int XGridPlayfield = 2;

        [Configurable]
        public int YGridPlayfield = 2;

        [Configurable]
        public Color4 GridColorPlayfield = new Color4(1, 0, 0, 1);

        [Group("Grid Lines Screen")]
        [Description("Enable grid lines aligned at the center of the screen. The default setting is a 2x2 grid.")]
        [Configurable]
        public bool GridLineScreens = false;
        [Configurable]
        public int XGridScreen = 2;

        [Configurable]
        public int YGridScreen = 2;

        [Configurable]
        public Color4 GridColorScreen = new Color4(1, 0, 0, 1);

        [Group("Background")]

        [Description("Remove's map background from storyboard. Decrease SB load. Credit to Mamat")]
        [Configurable]
        public bool RemoveBackground = false;




        StoryboardLayer layer;
        public void Debugdot(bool x)
        {
            if (x == true)
            {
                layer = GetLayer("DebugVisual");
                var p = layer.CreateSprite(FilePath, OsbOrigin.Centre);
                p.Scale(Starttime, 5);
                p.Move(Starttime, 320f, 240 + 24);
                p.Fade(Starttime, Endtime, 1, 1);
                try
                {
                    p.Color(Starttime, PlayfieldColor);
                }
                catch (Exception)
                {
                    p.Color(Starttime, "FF0000");
                }

            }
        }

        public void DebugdotScreen(bool x)
        {
            if (x == true)
            {
                layer = GetLayer("DebugVisual");
                var p = layer.CreateSprite(FilePath, OsbOrigin.Centre);
                p.Scale(Starttime, 5);
                p.Move(Starttime, 320f, 240 + 0.1);
                p.Fade(Starttime, Endtime, 1, 1);
                try
                {
                    p.Color(Starttime, ScreenColor);
                }
                catch (Exception)
                {
                    p.Color(Starttime, "FF0000");
                }
            }
        }

        public void GridLineScreen(bool z, int x, int y)
        {
            if (z == true)
            {

                layer = GetLayer("DebugVisual");
                double X_length = 853.33;
                double Y_length = 480 - 0;


                double X_avg;
                double Y_avg;

                try
                {
                    X_avg = X_length / (x + 1);
                    Y_avg = Y_length / (y + 1);
                }
                catch (DivideByZeroException)
                {
                    X_avg = X_length / (2 + 1);
                    Y_avg = Y_length / (2 + 1);
                }

                double tmp = -106.66;
                for (int i = 0; i < x; i++) // X
                {

                    var p = layer.CreateSprite(FilePath, OsbOrigin.Centre);
                    p.Fade(Starttime, Endtime, 1, 1);
                    p.ScaleVec(Starttime, 2, 1000);
                    p.MoveX(Starttime, tmp + X_avg);

                    try
                    {
                        p.Color(Starttime, GridColorScreen);
                    }
                    catch (Exception)
                    {
                        p.Color(Starttime, "FF0000");
                    }

                    tmp += X_avg;
                }

                tmp = 0;
                for (int i = 0; i < y; i++) // Y
                {

                    var p = layer.CreateSprite(FilePath, OsbOrigin.Centre);
                    p.Fade(Starttime, Endtime, 1, 1);
                    p.ScaleVec(Starttime, 2, 1000);
                    p.MoveY(Starttime, tmp + Y_avg);
                    p.Rotate(Starttime, 1.571);
                    try
                    {
                        p.Color(Starttime, GridColorScreen);
                    }
                    catch (Exception)
                    {
                        p.Color(Starttime, "FF0000");
                    }

                    tmp += Y_avg;
                }

            }

        }

        public void GridLinePlayfield(bool z, int x, int y)
        {
            if (z == true)
            {

                layer = GetLayer("DebugVisual");
                double X_length = 853.33;
                double Y_length = 480 + ((480 - 384) / 2);


                double X_avg;
                double Y_avg;

                try
                {
                    X_avg = X_length / (x + 1);
                    Y_avg = Y_length / (y + 1);
                }
                catch (DivideByZeroException)
                {
                    X_avg = X_length / (2 + 1);
                    Y_avg = Y_length / (2 + 1);
                }

                double tmp = -106.66;
                for (int i = 0; i < x; i++) // X
                {

                    var p = layer.CreateSprite(FilePath, OsbOrigin.Centre);
                    p.Fade(Starttime, Endtime, 1, 1);
                    p.ScaleVec(Starttime, 2, 1000);
                    p.MoveX(Starttime, tmp + X_avg);

                    try
                    {
                        p.Color(Starttime, GridColorPlayfield);
                    }
                    catch (Exception)
                    {
                        p.Color(Starttime, "FF0000");
                    }

                    tmp += X_avg;
                }

                tmp = 0;
                for (int i = 0; i < y; i++) // Y
                {

                    var p = layer.CreateSprite(FilePath, OsbOrigin.Centre);
                    p.Fade(Starttime, Endtime, 1, 1);
                    p.ScaleVec(Starttime, 2, 1000);
                    p.MoveY(Starttime, tmp + Y_avg);
                    p.Rotate(Starttime, 1.571);
                    try
                    {
                        p.Color(Starttime, GridColorPlayfield);
                    }
                    catch (Exception)
                    {
                        p.Color(Starttime, "FF0000");
                    }

                    tmp += Y_avg;
                }

            }

        }

        public void Removethebackground(bool x)
        {
            try
            {
                GetLayer("").CreateSprite(Beatmap.BackgroundPath).Fade(0, 0);
            }
            catch (ArgumentNullException)
            {
                // Do Nothing
            }

        }
        public override void Generate()
        {
            foreach (var hitobject in Beatmap.HitObjects)
            {
                Endtime = hitobject.EndTime;
            }

            Debugdot(CenterPlayfieldDot);
            GridLineScreen(GridLineScreens, XGridScreen, YGridScreen);
            DebugdotScreen(CenterScreenDot);
            Removethebackground(RemoveBackground);
            GridLinePlayfield(GridLinePlayfields, XGridPlayfield, YGridPlayfield);
        }
    }
}
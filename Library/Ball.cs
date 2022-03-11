using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Ball
    {
        public string ID { get; set; }
        public int Radius { get; set; }
        public int Diameter { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double PreviousX { get; set; }
        public double PreviousY { get; set; }
        public string Colour { get; set; }
        public int Opacity { get; set; }
        public double XDelta { get; set; }
        public double YDelta { get; set; }

        public Random Rand = new Random();

        public Ball (int canvasWidth, int canvasHeight)
        {
            Radius = 20;
            Diameter = Radius * 2;
            X = canvasWidth / 2;
            Y = canvasHeight / 2;
            PreviousX = X;
            PreviousY = Y;
            Colour = "";
        }

        public void StartBallRandomDirection()
        {
            // Assumes ball has just been created at the centre of the canvas
            // Picks a random direction and moves it one point in that direction
            PreviousX = X;
            PreviousY = Y;
            int RandomDirDegrees = Rand.Next(0, 361);
            float RandomAngleRadians = (float)(RandomDirDegrees / (2 * Math.PI));
            //range of x degrees around previous direction. Modulo 360 is prob not needed, but wtf
            XDelta = (float)(1 * Math.Cos(RandomAngleRadians));
            YDelta = (float)(1 * Math.Sin(RandomAngleRadians));

            X = (float)(X + XDelta);
            Y = (float)(Y + YDelta);
        }

        public void MoveBallStraightLine(int canvasWidth, int canvasHeight)
        {
            // Moves the ball in a straight line unless it hits a side
            // Simple reflection
            if (X >= canvasWidth - Radius)
            {
                XDelta = -Math.Abs(XDelta); // Make sure it's negative
            }
            else if ( X <= Radius)
            {
                XDelta = Math.Abs(XDelta); // Make sure it's positive
            }
            if (Y >= canvasHeight - Radius)
            {
                YDelta = -Math.Abs(YDelta);
            }
            else if (Y <= Radius)
            {
                YDelta = Math.Abs(YDelta);
            }
            PreviousX = X;
            PreviousY = Y;
            X = X + XDelta;
            Y = Y + YDelta;
        }

    }
}

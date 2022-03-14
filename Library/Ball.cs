using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


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
        
        public double Speed { get; set; } // Pixels per second

        public byte R, G, B;
        public double Opacity { get; set; }
        public double XDelta { get; set; }
        public double YDelta { get; set; }

        public Random Rand = new Random();



        

        public Ball (int canvasWidth, int canvasHeight)
        {
            Random rand = new Random();
            Radius = rand.Next(0, 21) + 10;
            Diameter = Radius * 2;
            X = canvasWidth / 2;
            Y = canvasHeight / 2;
            PreviousX = X;
            PreviousY = Y;
            Opacity = (double)rand.Next(4, 11) / 10;
            R = (byte)rand.Next(50, 255);
            G = (byte)rand.Next(50, 255);
            B = (byte)rand.Next(50, 255);

            Speed = rand.Next(10,501); // Pixels per second
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

        public void MoveBallStraightLine(int canvasWidth, int canvasHeight, TimeSpan elapsedTime)
        {
            // Moves the ball in a straight line unless it hits a side
            // Simple reflection

            double SpeedMultiplier = elapsedTime.TotalMilliseconds * Speed / 1000; //Speed is set in pixels per second

            if (X >= canvasWidth -Radius*2) //No - radius needed as the ball is drawn from top left
            {
                XDelta = -Math.Abs(XDelta); // Make sure it's negative
            }
            else if (X <= 0)
            {
                XDelta = Math.Abs(XDelta); // Make sure it's positive
            }
            if (Y >= canvasHeight - Radius*2)
            {
                YDelta = -Math.Abs(YDelta);
            }
            else if (Y <= 0)
            {
                YDelta = Math.Abs(YDelta);
            }
            PreviousX = X;
            PreviousY = Y;
            X = X + XDelta * SpeedMultiplier;
            Y = Y + YDelta * SpeedMultiplier;
        }

    }
}

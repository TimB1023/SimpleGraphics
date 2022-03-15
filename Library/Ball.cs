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
        public string ID { get; set; } // used to match logical object Ball to canvas object, Ballshape
        public int Radius { get; set; }
       
        public double X { get; set; }
        public double Y { get; set; }
        public double PreviousX { get; set; }
        public double PreviousY { get; set; }
        
        public double Speed { get; set; } // Pixels per second
        public double XDelta { get; set; }
        public double YDelta { get; set; }

        public byte R, G, B;
        public double Opacity { get; set; }

        public Random Rand = new Random(); // Didn't want this within the class, but couldn't work out a simple way to avoid it
        // Needed for the initialisation parameters

        // ==================  CONSTRUCTOR ====================
        public Ball (int canvasWidth, int canvasHeight)
        {
            Random rand = new Random();
            Radius = rand.Next(0, 21) + 10;
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
            // The speed will be handled in the next TimerDispatcher cycle.
            PreviousX = X;
            PreviousY = Y;

            // Random direction in degrees chosen
            // Deltas calculated on a basis of a movement of one pixel at chosen angle
            int RandomDirDegrees = Rand.Next(0, 361);
            float RandomAngleRadians = (float)(RandomDirDegrees / (2 * Math.PI));
            XDelta = (float)(1 * Math.Cos(RandomAngleRadians));
            YDelta = (float)(1 * Math.Sin(RandomAngleRadians));

            X = (float)(X + XDelta);
            Y = (float)(Y + YDelta);
        }

        public void MoveBallStraightLine(int canvasWidth, int canvasHeight, TimeSpan elapsedTime)
        {
            // Moves the ball in a straight line unless it hits a side
            // Simple reflection
            // Impact calculations take account of ellipses being drawn from top left, not centre
            // Speed based on time since this method was last called

            double SpeedMultiplier = elapsedTime.TotalMilliseconds * Speed / 1000; //Speed is set in pixels per second
            double OffScreenXAdjustment = 0;
            double OffScreenYAdjustment = 0;

            if (X >= canvasWidth -Radius * 2) //No - radius needed as the ball is drawn from top left
            {
                XDelta = - Math.Abs(XDelta); // Make sure it's negative
            }

            if (X >= canvasWidth)
            {
                OffScreenXAdjustment = X - canvasWidth;
            }

            if (X <= 0)
            {
                XDelta = Math.Abs(XDelta); // Make sure it's positive
            }

            if (Y >= canvasHeight - Radius * 2)
            {
                YDelta = - Math.Abs(YDelta);
            }

            if (Y >= canvasHeight)
            {
                OffScreenYAdjustment = Y - canvasHeight;
            }
            if (Y <= 0)
            {
                YDelta = Math.Abs(YDelta);
            }
            PreviousX = X;
            PreviousY = Y;
            X += XDelta * SpeedMultiplier - OffScreenXAdjustment;
            Y += YDelta * SpeedMultiplier - OffScreenYAdjustment;
            
        }
    }
}

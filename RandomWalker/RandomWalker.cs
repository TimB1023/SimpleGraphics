using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RandomWalkerNS
{
    class RandomWalker
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Random Rand = new Random();

        public float WindowHeight { get; set; }
        public float WindowWidth { get; set; }

        public int LastDirection { get; set; } //Should be between 0 and 359 degrees

        // ===================== Constructor =====================
        public RandomWalker(float inX, float inY)
        {
            this.X = inX;
            this.Y = inY;
            this.LastDirection = Rand.Next(0, 361);
            this.WindowWidth = inX * 2;
            this.WindowHeight = inY * 2;
        }

        public void MoveWalker() //random angle around previous angle
        {

            int RandomDirection = Rand.Next(0, 5) - 2 + LastDirection;
            float RandomAngle = (float)(RandomDirection / (2 * Math.PI));
            //range of x degrees around previous direction. Modulo 360 is prob not needed, but wtf
            this.X = (float)(this.X + (1 * Math.Cos(RandomAngle))); // screen wrap
            this.Y = (float)(this.Y + (1 * Math.Sin(RandomAngle)));
            this.LastDirection = RandomDirection;
        }


        public void MoveWalker1()
        {

            //float RandomAngle = (float)(Rand.Next(0, 360) / (2*Math.PI));
            //this.X = (float)(this.X + (1 * Math.Cos(RandomAngle)));
            //this.Y = (float)(this.Y + (1 * Math.Sin(RandomAngle)));


            //int Choice = Rand.Next(4);
            switch (Rand.Next(4))
            {
                case 0:
                    this.X++;
                    break;
                case 1:
                    this.X--;
                    break;
                case 2:
                    this.Y++;
                    break;
                case 3:
                    this.Y--;
                    break;
                default: //Should never happen
                    break;
            }


            //this.X = (float)(this.X + (2 * (Rand.Next(3) - 1))); // 3 because the highest number is never selected, so this give 0,1,2
            //this.Y = (float)(this.Y + (2 * (Rand.Next(3) - 1)));

        } // Simple up, down, left, right

        public void DisplayWalkerMethod()
        {
            //Rectangle blueRect = new Rectangle();
            //blueRect.Fill = Brushes.Blue;
            //blueRect.Width = 5;
            //blueRect.Height = 5;

            //Canvas.Children.Add(blueRect);
            //Canvas.SetTop(blueRect, Y);
            //Canvas.SetLeft(blueRect, X);
        }
    }
}

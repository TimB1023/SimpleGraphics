using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Library;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MovingBall
{
    /// <summary>
    /// Interaction logic for MovingBallWindow.xaml
    /// </summary>
    public partial class MovingBallWindow : Window
    {
        public int Iteration = 0;
        public int NumberOfColumns = 60;
        public int LowerRange = 0;
        public int UpperRange = 100;
        public int StandardDeviation = 60;
        public int Baseline = 5;
        public int MyCanvasWidth = 790; //Just starting size, as the parameters can be changed by mouse
        public int MyCanvasHeight = 440;
        public int MaxLine = 500;
        public int VerticalStep = 5;
        public bool Pause = false;
        public int CurrentID = 0;
        public ObservableCollection<Ball> Balls = new ObservableCollection<Ball>();
        public ObservableCollection<Ellipse> BallShapes = new ObservableCollection<Ellipse>();
        public Random rand = new Random();
        public Stopwatch Stopwatch = new Stopwatch();
        

        public MovingBallWindow()
        {
            InitializeComponent();
            canvas.Children.Clear();
        }


        public void AddBall()
        {
            Ball newBall = new Ball((int)canvas.ActualWidth, (int)canvas.ActualHeight); //temp moved to top
            Balls.Add(newBall);
            int ObjectIndex = Balls.IndexOf(newBall);
            newBall.ID = ObjectIndex.ToString();  //CurrentID.ToString();
            //CurrentID++;
            newBall.StartBallRandomDirection();
            newBall.R = (byte)rand.Next(50, 255);
            newBall.G = (byte)rand.Next(50, 255);
            newBall.B = (byte)rand.Next(50, 255);
            
            AddBallToCanvas(newBall, newBall.ID);
            
        }
        public void AddBallToCanvas(Ball newBall, string ID)
        {
            //Create graphics object and set parameters
            Ellipse BallShape = new Ellipse();
            SolidColorBrush scb = new SolidColorBrush(Color.FromRgb(newBall.R, newBall.G, newBall.B));
            BallShape.Fill = scb;
            BallShape.Width = 50; //newBall.Radius * 2;
            BallShape.Height = 50; //newBall.Radius * 2;
            BallShape.Opacity = newBall.Opacity;
            BallShape.Uid = ID;

            BallShapes.Add(BallShape);
            int ObjectIndex = BallShapes.IndexOf(BallShape);

            // Add to canvas and display

            canvas.Children.Add(BallShape);
            Canvas.SetTop(BallShape, newBall.Y - newBall.Radius); //The ball is drawn from top-left, not centre
            Canvas.SetLeft(BallShape, newBall.X - newBall.Radius);
            
        }

        private void UpdateChart() // move and display ball
        {
            foreach (Ball ball in Balls)
            {
                ball.MoveBallStraightLine((int)canvas.ActualWidth, (int)canvas.ActualHeight);
            }

            foreach (Ellipse ballshape in BallShapes)
            {
                int currentIDInt = int.Parse( ballshape.Uid); // Should be the index of the ballshape
                double thisX = (double)Balls[currentIDInt].X;
                double thisY = (double)Balls[currentIDInt].Y;

                Canvas.SetTop(ballshape, thisY );
                Canvas.SetLeft(ballshape, thisX );
            }

            //newBall.MoveBallStraightLine((int)canvas.ActualWidth, (int)canvas.ActualHeight);
            //BallShape.Fill = Brushes.Gray;
            //BallShape.Width = 50; //newBall.Radius * 2;
            //BallShape.Height = 50; //newBall.Radius * 2;
            //BallShape.Opacity = 1; //newBall.Opacity;
            //Canvas.SetTop(BallShape, newBall.Y - newBall.Radius); //The ball is drawn from top-left, not centre
            //Canvas.SetLeft(BallShape, newBall.X - newBall.Radius);

        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            //if (!Pause)
            //{
            //    Iteration += VerticalStep;

            UpdateChart();
            //}
            //if (Iteration == MaxLine) { Pause = true; }
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            AddBall();
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(1);
            dt.Tick += Dt_Tick;
            dt.Start();
        }



    }
}

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
        public int MyCanvasWidth = 800;
        public int MaxLine = 500;
        public int VerticalStep = 5;
        public bool Pause = false;
        public int CurrentID = 1;
        public List<Ball> Balls = new List<Ball>();
        public Random rand = new Random();
        Ball newBall = new Ball(800, 450);
        Ellipse BallShape = new Ellipse();

        public MovingBallWindow()
        {
            InitializeComponent();
            canvas.Children.Clear();
        }


        public void AddBall()
        {
            //Ball newBall = new Ball((int)canvas.ActualWidth, (int)canvas.ActualHeight); //temp moved to top
            //newBall.ID = CurrentID;
            newBall.StartBallRandomDirection();
            //Balls.Add(newBall);
            AddBallToCanvas(newBall);
        }
        public void AddBallToCanvas(Ball newBall)
        {
            // Create graphics object and set parameters
            //Ellipse BallShape = new Ellipse(); // temp moved to top
            BallShape.Fill = Brushes.Gray;
            BallShape.Width = 50; //newBall.Radius * 2;
            BallShape.Height = 50; //newBall.Radius * 2;
            BallShape.Opacity = newBall.Opacity;
            // Add to canvas and display
            canvas.Children.Add(BallShape);
            Canvas.SetTop(BallShape, newBall.Y - newBall.Radius); //The ball is drawn from top-left, not centre
            Canvas.SetLeft(BallShape, newBall.X - newBall.Radius);
            newBall.ID = BallShape.Uid;
        }

        private void UpdateChart() // move and display ball
        {
            //foreach (Ball ball in Balls)
            //{
            //    ball.MoveBallStraightLine((int)canvas.ActualWidth, (int)canvas.ActualHeight);
            //    Ellipse.
            //    canvas.SetTop(BallShape, ball.Y);
            //    canvas.SetLeft(BallShape, ball.X);
            //}

            newBall.MoveBallStraightLine((int)canvas.ActualWidth, (int)canvas.ActualHeight);
            BallShape.Fill = Brushes.Gray;
            BallShape.Width = 50; //newBall.Radius * 2;
            BallShape.Height = 50; //newBall.Radius * 2;
            BallShape.Opacity = 1; //newBall.Opacity;
            Canvas.SetTop(BallShape, newBall.Y - newBall.Radius); //The ball is drawn from top-left, not centre
            Canvas.SetLeft(BallShape, newBall.X - newBall.Radius);

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
            dt.Interval = TimeSpan.FromTicks(10000);  //.FromMilliseconds(0);
            dt.Tick += Dt_Tick;
            dt.Start();
        }



    }
}

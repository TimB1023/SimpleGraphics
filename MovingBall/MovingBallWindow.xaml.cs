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
    // Multiple balls spawned by user, which then bounce around the canvas
    public partial class MovingBallWindow : Window
    {
        //public int Iteration = 0;
        //public int NumberOfColumns = 60;
        //public int LowerRange = 0;
        //public int UpperRange = 100;
        //public int StandardDeviation = 60;
        //public int Baseline = 5;
        public int MyCanvasWidth = 790; //Just starting size, as the parameters can be changed by mouse
        public int MyCanvasHeight = 440;
        public bool Pause = false;
        public int CurrentID = 0;
        // These could have been lists. Might work out optimisation using ObservableCollections later
        // These collections should have parallel members
        // Could have created a third class, with Balls and BallShapes as properties
        public ObservableCollection<Ball> Balls = new ObservableCollection<Ball>();
        public ObservableCollection<Ellipse> BallShapes = new ObservableCollection<Ellipse>();
        public Random rand = new Random();
        public DateTimeOffset lastTime = new DateTimeOffset();


        // ======================= CONSTRUCTOR =====================
        public MovingBallWindow()
        {
            InitializeComponent();
            canvas.Children.Clear();
        }

        // Adds logical object Ball after user click and adds the associated graphical object to the canvas
        public void AddBall()
        {
            // Actual height and width to spawn in the centre of the canvas
            Ball newBall = new Ball((int)canvas.ActualWidth, (int)canvas.ActualHeight);
            
            Balls.Add(newBall); //Added to observable collection
            int ObjectIndex = Balls.IndexOf(newBall); // Used for matchin Ball object to Ellipse object
            newBall.ID = ObjectIndex.ToString();  //CurrentID.ToString();

            newBall.StartBallRandomDirection();
            AddBallToCanvas(newBall, newBall.ID);
            
        }
        public void AddBallToCanvas(Ball newBall, string ID)
        {
            //Create graphics object and set parameters
            Ellipse BallShape = new Ellipse();
            SolidColorBrush scb = new SolidColorBrush(Color.FromRgb(newBall.R, newBall.G, newBall.B));
            BallShape.Fill = scb;
            BallShape.Opacity = newBall.Opacity;

            BallShape.Width = newBall.Radius * 2;
            BallShape.Height = newBall.Radius * 2;
            
            BallShape.Uid = ID;

            BallShapes.Add(BallShape); // Add ellipse to ObservableCollection BallShapes

            int ObjectIndex = BallShapes.IndexOf(BallShape); // To match with parallel Balls Observable collection

            // Add to canvas and display

            canvas.Children.Add(BallShape);
            // Unclear why the instance canvas is used above, but Class canvas us used below
            Canvas.SetTop(BallShape, newBall.Y - newBall.Radius); //The ball is drawn from top-left, not centre
            Canvas.SetLeft(BallShape, newBall.X - newBall.Radius);
            BallCounter.Text = canvas.Children.Count.ToString(); 
            
        }

        private void UpdateChart(TimeSpan elapsedTime) // move and display ball
        {
            // Update logical objects, Balls, then graphical objects, BallShapes
            foreach (Ball ball in Balls)
            {
                ball.MoveBallStraightLine((int)canvas.ActualWidth, (int)canvas.ActualHeight, elapsedTime);
                // Note that there is also Application.Current.MainWindow.ActualWidth / .Width
            }

            // Update graphical BallShapes objects matching the logical Balls objects
            foreach (Ellipse ballshape in BallShapes)
            {
                int currentIDInt = int.Parse( ballshape.Uid); // Should be the index of the ballshape
                double thisX = (double)Balls[currentIDInt].X;
                double thisY = (double)Balls[currentIDInt].Y;

                Canvas.SetTop(ballshape, thisY );
                Canvas.SetLeft(ballshape, thisX );
            }
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            // Method run each Tick until program is closed
            //if (!Pause)
            DateTimeOffset time = DateTimeOffset.Now; // Calculates time from previous Tick to provide consistent speeds
            TimeSpan elapsedTime = time - lastTime; // In milliseconds
            lastTime = time;
            UpdateChart(elapsedTime); // elapsed time passed in for speed calulations
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            // Kicks off Tick process and adds first Ball object
            // Does not display the corresponding BallShape as this will be handled in the next Tick
            AddBall();
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(1);
            dt.Tick += Dt_Tick;
            dt.Start();
            lastTime = DateTimeOffset.Now;
        }
    }
}

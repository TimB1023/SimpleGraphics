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
using System.Windows.Threading;
using Library;

namespace PerlinNoiseDot
{
    /// <summary>
    /// Interaction logic for PerlinDotWindow.xaml
    /// </summary>
    public partial class PerlinDotWindow : Window
    {
        public int Iteration = 0;
        public int NumberOfColumns = 60;
        public int LowerRange = 0;
        public int UpperRange = 100;
        public int StandardDeviation = 10;
        public int Baseline = 20;
        public int MyCanvasWidth = 600;
        public int MaxLine = 500;
        public bool Pause = false;
        public Perlin2dDot Dot = new Perlin2dDot();
        public Random rand = new Random();
        public PerlinDotWindow()
        {
            InitializeComponent();
            canvas.Children.Clear();
            Dot.X = 400;
            Dot.PreviousX = 400;
        }

        private void CanvasLoaded(object sender, RoutedEventArgs e)
        {
            //Now started on on the press of a button, not on Canvas Load
            //DispatcherTimer dt = new DispatcherTimer();
            //dt.Interval = TimeSpan.FromTicks(1);  //.FromMilliseconds(0);
            //dt.Tick += Dt_Tick;
            //dt.Start();
        }
        private void Dt_Tick(object sender, EventArgs e)
        {
            if (!Pause)
            {
                Iteration++;
                
                UpdateChart();
            }
            if (Iteration == MaxLine) { Pause = true; }
        }

        private void UpdateChart()
        {
            int xMovement = rand.Next(0, 11) - 5;
            Dot.X = Dot.PreviousX + xMovement;
            Dot.PreviousX = Dot.X;


            // Display dot
            Ellipse greyCircle = new Ellipse();
            greyCircle.Fill = Brushes.Gray;
            greyCircle.Width = 3;
            greyCircle.Height = 3;
            greyCircle.Opacity = 1;

            canvas.Children.Add(greyCircle);
            Canvas.SetBottom(greyCircle, Baseline + Iteration);
            Canvas.SetLeft(greyCircle, Dot.X);
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {

        
        DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromTicks(1);  //.FromMilliseconds(0);
            dt.Tick += Dt_Tick;
            dt.Start();
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            Dot.X = 400;
            Dot.PreviousX = 400;
            Iteration = 0;
            Pause = false;
        }
    }
}

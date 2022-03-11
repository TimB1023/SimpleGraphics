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
        public int StandardDeviation = 60;
        public int Baseline = 5;
        public int MyCanvasWidth = 800;
        public int MaxLine = 500;
        public int VerticalStep = 5;
        public bool Pause = false;
        public Perlin2dDot Dot = new Perlin2dDot();
        public Random rand = new Random();
        Ellipse greyCircle = new Ellipse();
        public PerlinDotWindow()
        {
            InitializeComponent();
            canvas.Children.Clear();
            Dot.X = (int)(MyCanvasWidth / 2);
            Dot.PreviousX = (int)(MyCanvasWidth / 2);
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
                Iteration += VerticalStep;
                
                UpdateChart();
            }
            if (Iteration == MaxLine) { Pause = true; }
        }

        private void UpdateChart()
        {
            int xMovement = rand.Next(0, StandardDeviation) - (StandardDeviation/2);
            Dot.X = Dot.PreviousX + xMovement;

            // Display dot
            int CircleSize = (int)(Iteration / 10);
            greyCircle.Fill = Brushes.Gray;
            greyCircle.Width = CircleSize;
            greyCircle.Height = CircleSize;
            greyCircle.Opacity = 1;
            

            //greyCircle.RenderTransform.Transform
            if (!canvas.Children.Contains(greyCircle))
            {
                canvas.Children.Add(greyCircle);
            }

            Canvas.SetZIndex(greyCircle, 10);
            Canvas.SetTop(greyCircle, Baseline + Iteration - (CircleSize / 2));
            Canvas.SetLeft(greyCircle, Dot.X - (CircleSize / 2));

            //SolidColorBrush scb = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffaacc"));

            SolidColorBrush scb = new SolidColorBrush(Color.FromRgb(245, 145 , 50));

            Line Line = new Line();
            Line.Stroke = scb;
            Line.StrokeThickness = 2;
            Line.X1 = Dot.PreviousX;
            Line.Y1 = Baseline + Iteration - VerticalStep;
            Line.X2 = Dot.X;
            Line.Y2 = Baseline + Iteration;
            canvas.Children.Add(Line);

            Dot.PreviousX = Dot.X;

        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {

        
        DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(500);  //.FromMilliseconds(0);
            dt.Tick += Dt_Tick;
            dt.Start();
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            //canvas.Children.Clear();
            Dot.X = 400;
            Dot.PreviousX = 400;
            Iteration = 0;
            Pause = false;
        }
    }
}

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

namespace GrowingColumnChart
{
    /// <summary>
    /// Interaction logic for ColumnsWindow.xaml
    /// </summary>
    public partial class ColumnsWindow : Window
    {   
        public int Iteration = 0;
        public int NumberOfColumns = 60;
        public int LowerRange = 0;
        public int UpperRange = 100;
        public int StandardDeviation = 10;
        public int Baseline = 20;
        public int MyCanvasWidth = 600;
        public int DotsLine = 500;
        public bool Pause = false;

        Random rand = new Random();
        private List<Column> Columns { get; set; } = new List<Column>();
        private List<Rectangle> Rectangles { get; set; } = new List<Rectangle>();
        public ColumnsWindow()
        {
            InitializeComponent();
            canvas.Children.Clear();
        }

        private void GenerateColumns()
        {
            int ColumnWidth = MyCanvasWidth / NumberOfColumns;
            for (int i = 0; i < NumberOfColumns; i++)
            {
                Column Column = new Column();
                Column.Width = ColumnWidth;
                Column.BottomOfBin = Math.Round((double)(i * (UpperRange-LowerRange)) / NumberOfColumns,3);
                Column.TopOfBin = Math.Round((double)(Column.BottomOfBin + (UpperRange - LowerRange) / NumberOfColumns),3);
                Column.BinNumber = i;
                Columns.Add(Column);

                ;
                //MessageBox.Show($"Created column {Column.BottomOfBin} to {Column.TopOfBin}");
            }
            canvas.Width = MyCanvasWidth;
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
                count.Text = $"{Iteration}";
                UpdateChart();
            }
        }

        private void UpdateChart()
        {
            if (Columns.Count !=0)
            {
                //MessageBox.Show("Update");
                //double NewSample = (double)rand.Next(LowerRange, UpperRange + 1);
                double NewSample = Library.GaussianGenerator.NextGaussian((UpperRange - LowerRange) / 2, StandardDeviation);
                NewSample = Math.Round(NewSample, 4); //One more place for rounding so that numbers don't fall between bins
                RandomNumber.Text = $"{NewSample}";
                bool SampleCaught = false;
                foreach (Column Column in Columns)
                {
                    if (NewSample >= Column.BottomOfBin && NewSample <= Column.TopOfBin)
                    {
                        Column.Count++;
                        SampleCaught = true;
                        if (Column.Count >= DotsLine-20)
                        {
                            Pause = true;
                        }
                        ;
                    }
                }
                if (!SampleCaught)
                {
                    // MessageBox.Show($"{NewSample} fell through the gaps"); //Can't seem to prevent this at the moment
                    ;
                }
                
                DisplayAllColumns();

                Ellipse greyCircle = new Ellipse();
                greyCircle.Fill = Brushes.Gray;
                greyCircle.Width = 10;
                greyCircle.Height = 10;
                greyCircle.Opacity = 0.01;

                canvas.Children.Add(greyCircle);
                Canvas.SetBottom(greyCircle, DotsLine);
                Canvas.SetLeft(greyCircle, NewSample * MyCanvasWidth / (UpperRange-LowerRange));
            }
        }

        private void DisplayAllColumns()
        {
            //canvas.Children.Clear();

            for (int index = canvas.Children.Count - 1; index >= 0; index--)
            {
                if (canvas.Children[index] is Rectangle)
                {
                    canvas.Children.RemoveAt(index);
                }
            }

            foreach (Column col in Columns)
            {
                Rectangle greyRect = new Rectangle();
                greyRect.Fill = Brushes.Gray;
                greyRect.Width = col.Width;
                greyRect.Height = col.Count;
                greyRect.Stroke = Brushes.Black;
                greyRect.StrokeThickness = 1;

                canvas.Children.Add(greyRect);
                
                Canvas.SetBottom(greyRect, Baseline);
                Canvas.SetLeft(greyRect, col.BinNumber * col.Width);
                ;
            }
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateColumns();
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromTicks(1);  //.FromMilliseconds(0);
            dt.Tick += Dt_Tick;
            dt.Start();
        }
    }
}

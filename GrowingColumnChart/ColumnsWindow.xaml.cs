﻿using System;
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

namespace GrowingColumnChart
{
    /// <summary>
    /// Interaction logic for ColumnsWindow.xaml
    /// </summary>
    public partial class ColumnsWindow : Window
    {
        public int Iteration = 0;
        public int NumberOfColumns = 10;
        public int LowerRange = 0;
        public int UpperRange = 100;
        public int StandardDeviation = 20;
        public int Baseline = 20;
        public int MyCanvasWidth = 600;
        Random rand = new Random();
        private List<Column> Columns { get; set; } = new List<Column>();
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
                Column.BottomOfBin = i * UpperRange / NumberOfColumns;
                Column.TopOfBin = Column.BottomOfBin + UpperRange / NumberOfColumns - 1;
                Column.BinNumber = i;
                Columns.Add(Column);

                ;
                //MessageBox.Show($"Created column {Column.BottomOfBin} to {Column.TopOfBin}");
            }
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
            Iteration++;
            count.Text = $"{Iteration}";
            UpdateChart();
            
        }

        private void UpdateChart()
        {
            if (Columns.Count !=0)
            {
                //MessageBox.Show("Update");
                double NewSample = (double)rand.Next(LowerRange, UpperRange + 1);
                RandomNumber.Text = $"{NewSample}";
                Boolean SampleCaught = false;
                foreach (Column Column in Columns)
                {
                    if (NewSample >= Column.BottomOfBin && NewSample <= Column.TopOfBin)
                    {
                        Column.Count++;
                        SampleCaught = true
                        ;
                    }
                }
                if (!SampleCaught)
                {
                    //MessageBox.Show($"{NewSample} fell through the gaps");
                }
                DisplayAllColumns();

                Ellipse greyCircle = new Ellipse();
                greyCircle.Fill = Brushes.Gray;
                greyCircle.Width = 10;
                greyCircle.Height = 10;
                greyCircle.Opacity = 0.1;

                canvas.Children.Add(greyCircle);
                Canvas.SetBottom(greyCircle, 500);
                Canvas.SetLeft(greyCircle, NewSample * MyCanvasWidth / (UpperRange-LowerRange));
            }
 
        }

        private void DisplayAllColumns()
        {
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
            dt.Interval = TimeSpan.FromMilliseconds(10);  //.FromMilliseconds(0);
            dt.Tick += Dt_Tick;
            dt.Start();
        }
    }
}

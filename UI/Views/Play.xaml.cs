using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UI.ViewModels;
using System;
using System.Diagnostics;
using UI.Models;
using Windows.UI.Xaml.Navigation;
using Entities;
using System.Collections.Generic;
using UI.ViewModels.Utilities;

namespace UI.Views
{

    public sealed partial class Play : Page
    {
        private PlayVM playVM;
        private Stopwatch stopwatch;
        DispatcherTimer dispatcherTimer;
        public Play()
        {
            InitializeComponent();
            playVM = (PlayVM)DataContext;
            dispatcherTimer = new DispatcherTimer();
            stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();
            dispatcherTimer.Tick += timer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();
            
        }
        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {

         Window.Current.Content.KeyDown += playVM.determinarMovilidadFantasma;
            
        }

        private void timer_Tick(object sender, object e)
        {
            string currentTime;
            TimeSpan ts;
            if (!SharedData.FinPartida)//Si no termino la partida
            {
                ts = stopwatch.Elapsed;
                currentTime = string.Format("{0:00}:{1:00}:{2:00}", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                cronometro.Text = currentTime;
            }
            else {
                stopwatch.Stop();
                dispatcherTimer.Stop(); 
                SharedData.CompletionTime = cronometro.Text;
            }
        }
    }
}

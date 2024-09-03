using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Timer_demo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private MediaPlayer mediaPlayer;
        private string defaultMusicFilePath;
        private string specialMusicFilePath;
        private readonly TimeSpan[] scheduledTimes = new TimeSpan[]
        {
            new TimeSpan(8, 30, 0),
            new TimeSpan(9, 15, 0),
            new TimeSpan(9, 20, 0),
            new TimeSpan(10, 5, 0),
            new TimeSpan(10, 20, 0),
            new TimeSpan(11, 0, 0),
            new TimeSpan(11, 5, 0),
            new TimeSpan(11, 55, 0),
            new TimeSpan(12, 25, 0),
            new TimeSpan(13, 10, 0),
            new TimeSpan(13, 15, 0),
            new TimeSpan(14, 0, 0),
            new TimeSpan(14, 10, 0),
            new TimeSpan(14, 55, 0),
            new TimeSpan(15, 0, 0),
            new TimeSpan(15, 45, 0),
            new TimeSpan(16, 0, 0),
            new TimeSpan(16, 45, 0),
            new TimeSpan(16, 50, 0),
            new TimeSpan(17, 35, 0),
            new TimeSpan(17, 45, 0),
            new TimeSpan(18, 30, 0),
            new TimeSpan(18, 35, 0),
            new TimeSpan(19, 20, 0)
        };

        private readonly TimeSpan[] specialTimes = new TimeSpan[]
        {
            new TimeSpan(10, 5, 0),
            new TimeSpan(11, 55, 0),
            new TimeSpan(14, 0, 0),
            new TimeSpan(15, 45, 0),
            new TimeSpan(17, 35, 0),
            new TimeSpan(19, 20, 0)
        };

        public MainWindow()
        {
            InitializeComponent();

            mediaPlayer = new MediaPlayer();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(defaultMusicFilePath) || string.IsNullOrEmpty(specialMusicFilePath)) return;

            TimeSpan now = DateTime.Now.TimeOfDay;
            foreach (var scheduledTime in scheduledTimes)
            {
                if (now.Hours == scheduledTime.Hours && now.Minutes == scheduledTime.Minutes && now.Seconds == 0)
                {
                    if (Array.Exists(specialTimes, time => time == scheduledTime))
                    {
                        PlayMusic(specialMusicFilePath);
                    }
                    else
                    {
                        PlayMusic(defaultMusicFilePath);
                    }
                    break;
                }
            }
        }

        private void PlayMusic(string filePath)
        {
            mediaPlayer.Open(new Uri(filePath, UriKind.RelativeOrAbsolute));
            mediaPlayer.Play();
        }

        private void SelectDefaultMusicFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                defaultMusicFilePath = openFileDialog.FileName;
            }
        }

        private void SelectSpecialMusicFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                specialMusicFilePath = openFileDialog.FileName;
            }
        }
    }
}
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Timer_demo
{
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

        public ObservableCollection<Schedule> Schedules { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            mediaPlayer = new MediaPlayer();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            Schedules = new ObservableCollection<Schedule>
        {
            new Schedule { Pair = "1 пара", Start = "8:30", End = "9:15" },
            new Schedule { Pair = "", Start = "9:20", End = "10:05" },
            new Schedule { Pair = "2 пара", Start = "10:20", End = "11:00" },
            new Schedule { Pair = "", Start = "11:05", End = "11:55" },
            new Schedule { Pair = "3 пара", Start = "12:25", End = "13:10" },
            new Schedule { Pair = "", Start = "13:15", End = "14:00" },
            new Schedule { Pair = "4 пара", Start = "14:10", End = "14:55" },
            new Schedule { Pair = "", Start = "15:00", End = "15:45" },
            new Schedule { Pair = "5 пара", Start ="16:00", End = "16:45" },
            new Schedule { Pair = "", Start = "16:50", End = "17:35"},
            new Schedule { Pair = "6 пара", Start = "17:45", End ="18:30"},
            new Schedule { Pair = "", Start = "18:35", End = "19:20"}
        };

            DataContext = this;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

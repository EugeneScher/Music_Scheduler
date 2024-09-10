using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Microsoft.Win32;
using System.Windows.Media; // Добавьте для использования MediaPlayer

namespace Timer_demo
{
    public partial class MainWindow : Window
    {
        private DateTime startTime;
        private DateTime endTime;
        private readonly DispatcherTimer timer;
        private MediaPlayer player; // Используем MediaPlayer вместо SoundPlayer

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1) // Проверять каждую секунду
            };
            timer.Tick += Timer_Tick;
        }

        private void SelectMelodyButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Audio Files|*.mp3;*.wav"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                selectedMelodyTextBox.Text = openFileDialog.FileName;
            }
        }

        private void SetStartTimeButton_Click(object sender, RoutedEventArgs e)
        {
            if (startDatePicker.SelectedDate != null && startTimeComboBox.SelectedItem != null)
            {
                string selectedTime = (startTimeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                startTime = startDatePicker.SelectedDate.Value.Add(TimeSpan.Parse(selectedTime));
                MessageBox.Show($"Время начала установлено: {startTime}");
                StartTimer(); // Запускаем таймер после установки времени
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите дату и время начала.");
            }
        }

        private void SetEndTimeButton_Click(object sender, RoutedEventArgs e)
        {
            if (endDatePicker.SelectedDate != null && endTimeComboBox.SelectedItem != null)
            {
                string selectedTime = (endTimeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                endTime = endDatePicker.SelectedDate.Value.Add(TimeSpan.Parse(selectedTime));
                MessageBox.Show($"Время окончания установлено: {endTime}");
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите дату и время окончания.");
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;

            // Проверка на начало воспроизведения
            if (currentTime >= startTime && currentTime < endTime)
            {
                if (player == null)
                {
                    player = new MediaPlayer();
                    player.Open(new Uri(selectedMelodyTextBox.Text, UriKind.Absolute));
                    player.Play(); // Начало воспроизведения мелодии
                }
            }

            // Проверка на окончание воспроизведения
            if (currentTime >= endTime)
            {
                if (player != null)
                {
                    player.Stop(); // Остановка воспроизведения
                    player = null;
                }

                timer.Stop(); // Остановить таймер
                MessageBox.Show("Воспроизведение завершено.");
            }
        }

        private void StartTimer()
        {
            if (startTime != DateTime.MinValue && endTime != DateTime.MinValue)
            {
                timer.Start(); // Запуск таймера
            }
        }
    }
}

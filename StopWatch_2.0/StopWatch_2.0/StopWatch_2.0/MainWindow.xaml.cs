using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
namespace StopWatch_2._0
{
    public partial class MainWindow : Window
    {
        bool Flag = false; // Флаг пуска и остановки секундомера.
        DateTime tBeg; // Начало отрезка (интервала) времени.
        TimeSpan tDel; // Продолжительность интервала.
        int i = 1; // Переменная для хранения номера круга.

        /// <summary>
        /// Конструктор главного окна. Подписываю отображение времени на событие отрисовки окна - 60 раз в секунду.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            CyclesInformation.Text = $"Информация о кругах{Environment.NewLine}(ограничение - 15)";
            CompositionTarget.Rendering += (ss, ee) =>
            {
                if (!Flag) // Если таймер не активирован.
                { 
                    return;
                }
                tDel = DateTime.Now - tBeg;
                DeltaTime1.Text = $"{tDel.TotalHours:F0}ч {tDel.TotalMinutes:F0}мин ";
                DeltaTime.Text = $"{tDel.TotalSeconds:F0}с {tDel.Milliseconds / 10:F0}мс";
                rotateSecond.Angle = 6 * (tDel.TotalMilliseconds / 1000.0);
            };
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку "Старт"("Стоп").
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartStop_Click(object sender, RoutedEventArgs e)
        {
            if (!Flag) // При активации секундомера.
            {
                StartStop.Content = "Стоп";
                tBeg = DateTime.Now; // Начало интервала.
                Cycle.IsEnabled = true;
                CyclesInformation.Text = "";
                i = 1;
            }
            else // При остановке секундомера.
            {
                StartStop.Content = "Старт";
                Cycle.IsEnabled = false;
            }
            Flag = !Flag; // Изменение состояния секундомера.
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Круг".
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cycle_Click(object sender, RoutedEventArgs e)
        {
            if (i < 16) // Добавляем информацию на текстовое поле, пока кругов не больше пятнадцати.
            {
                CyclesInformation.Text += $"{i++} - {tDel.TotalHours:F0}ч {tDel.TotalMinutes:F0}мин " +
                    $"{tDel.TotalSeconds:F0}с {tDel.Milliseconds / 10:F0}мс{Environment.NewLine}";
            }
        }
    }
}
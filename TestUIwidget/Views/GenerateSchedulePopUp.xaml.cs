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
using TestUIwidget.Models;

namespace TestUIwidget.Views
{
    /// <summary>
    /// Логика взаимодействия для GenerateSchedulePopUp.xaml
    /// </summary>
    public partial class GenerateSchedulePopUp : Window
    {
        GenerateSettings settings;
        public GenerateSchedulePopUp()
        {
            InitializeComponent();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public GenerateSettings GetResult()
        {
            return settings;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            settings = new GenerateSettings();
            try
            {
                settings.ItemCount = Convert.ToInt32(ItemCountTextBox.Text);
                settings.Period = Convert.ToInt32(PeriodTextBox.Text);
                settings.RowCount = Convert.ToInt32(RowCountTextBox.Text);
                if (settings.ItemCount < settings.RowCount)
                {
                    MessageBox.Show("The number of elements is less than the number of lines", "Error", MessageBoxButton.OK);
                    return;
                }
                DialogResult = true;
                this.Close();
            }
            catch
            {
                MessageBox.Show("Fill in all fields with numerical values", "Error", MessageBoxButton.OK);
            }
        }
    }
}

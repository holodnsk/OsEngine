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

namespace OsEngine.OsTrader.Panels.PanelsGui
{
    /// <summary>
    /// Interaction logic for NewRobot5Ui.xaml
    /// </summary>
    public partial class NewRobot5Ui : Window
    {

        private NewRobot5 _myRobot;
        public NewRobot5Ui(NewRobot5 myRobot)
        {
            InitializeComponent();
            _myRobot = myRobot;

            TexBoxVolume.Text = _myRobot.Volume.ToString();
            TexBoxStop.Text = _myRobot.Stop.ToString();
            TexBoxProfit.Text = _myRobot.Profit.ToString();
            TexBoxSleepage.Text = _myRobot.Sleepage.ToString();
            CheckBoxOnOff.IsChecked = _myRobot.IsOn;

            ButtonSave.Click += ButtonSave_Click;

        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            _myRobot.Volume = Convert.ToInt32(TexBoxVolume.Text);
            _myRobot.Stop = Convert.ToInt32(TexBoxStop.Text);
            _myRobot.Profit = Convert.ToInt32(TexBoxProfit.Text);
            _myRobot.Sleepage = Convert.ToInt32(TexBoxSleepage.Text);
            _myRobot.IsOn = CheckBoxOnOff.IsChecked.Value;

            // запрос сохранениея настроек в файл
            _myRobot.Save();

            // закрытие окна
            Close();
        }
    }
}

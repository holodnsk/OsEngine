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
    /// Interaction logic for HomeWorkRobotBearPatternWithSettingsUi.xaml
    /// </summary>
    public partial class HomeWorkRobotBearPatternWithSettingsUi : Window
    {
        private HomeWorkRobotBearPatternWithSettings _myRobot;
        public HomeWorkRobotBearPatternWithSettingsUi(HomeWorkRobotBearPatternWithSettings myRobot)
        {
            InitializeComponent();
            InitializeComponent();
            _myRobot = myRobot;

            TexBoxVolume.Text = _myRobot.Volume.ToString();
            TexBoxStop.Text = _myRobot.Stop.ToString();
            TexBoxProfit.Text = _myRobot.Profit.ToString();
            TexBoxSleepageOpen.Text = _myRobot.SleepageOpenPosition.ToString();
            TexBoxSleepageClose.Text = _myRobot.SleepageClosePosition.ToString();
            CheckBoxOnOff.IsChecked = _myRobot.IsOn;

            ButtonSave.Click += ButtonSave_Click;

        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            _myRobot.Volume = Convert.ToInt32(TexBoxVolume.Text);
            _myRobot.Stop = Convert.ToInt32(TexBoxStop.Text);
            _myRobot.Profit = Convert.ToInt32(TexBoxProfit.Text);
            _myRobot.SleepageOpenPosition = Convert.ToInt32(TexBoxSleepageOpen.Text);
            _myRobot.SleepageClosePosition = Convert.ToInt32(TexBoxSleepageClose.Text);
            _myRobot.IsOn = CheckBoxOnOff.IsChecked.Value;

            // запрос сохранениея настроек в файл
            _myRobot.Save();

            // закрытие окна
            Close();
        }
    }
}

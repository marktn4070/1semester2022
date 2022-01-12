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

namespace _1Semprojekt2022_Golf
{
    /// <summary>
    /// Interaction logic for Create_runner.xaml
    /// </summary>
    public partial class Create_runner : Window
    {
        public Administrator admin = null;
        public Create_runner(Administrator admn)
        {
            admin = admn;
            InitializeComponent();
        }

        private void Tilfoej_deltager_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                admin.MakeNewRunner(Name.Text, Mail.Text, int.Parse(Phone.Text), Address.Text, int.Parse(Zip.Text), City.Text);
                MessageBox.Show("Løber gemt"); //måske behøver vi ikke dette......
                Name.Clear();
                Mail.Clear();
                Phone.Clear();
                Address.Clear();
                Zip.Clear();
                City.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

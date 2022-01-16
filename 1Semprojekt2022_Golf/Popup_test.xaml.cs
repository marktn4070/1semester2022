using System.Windows;

namespace _1Semprojekt2022_Golf
{
    /// <summary>
    /// Interaction logic for Popup_test.xaml
    /// </summary>
    public partial class Popup_test : Window
    {
        public Popup_test(string Id_string, string Name_string, string Mail_string, string Phone_string, string Address_string, string Zip_string, string City_string)
        {
            InitializeComponent();

            P_id_txt.Text = Id_string;
            P_name_txt.Text = Name_string;
            P_mail_txt.Text = Mail_string;
            P_phone_txt.Text = Phone_string;
            P_address_txt.Text = Address_string;
            P_zip_txt.Text = Zip_string;
            P_city_txt.Text = City_string;
        }
    }
}
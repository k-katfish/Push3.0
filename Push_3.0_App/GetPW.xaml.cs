using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

namespace Push
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GetPW : Window
    {
        SecureString lpassword;
        //string lusername;

        public SecureString Password { get => this.lpassword; }
        //public string Username { get => this.lusername; }

        public GetPW()
        {
            InitializeComponent();

            UserNameTextBox.Text = $"{Environment.UserDomainName}\\{Environment.UserName}";
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            //string pw = PasswordPBox.Password;
            SecureString securepassword = new SecureString();
            foreach (char c in PasswordPBox.Password)
            {
                securepassword.AppendChar(c);
            }
            this.lpassword = securepassword;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Management.Infrastructure;
using Microsoft.Management.Infrastructure.Options;

namespace CsCredential
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CimCredential credential;

        public CimCredential Credential { get { return credential; } }

        public MainWindow()
        {
            InitializeComponent();
            UsernameTBox.Text = $"{Environment.UserDomainName}\\{Environment.UserName}";
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            string plaintextpassword;
            plaintextpassword = PasswordPBox.Password;

            SecureString securepassword = new SecureString();
            foreach (char c in plaintextpassword)
            {
                securepassword.AppendChar(c);
            }

            credential = new CimCredential(PasswordAuthenticationMechanism.CredSsp, Environment.UserDomainName, Environment.UserName, securepassword);

            this.Close();
        }
    }
}

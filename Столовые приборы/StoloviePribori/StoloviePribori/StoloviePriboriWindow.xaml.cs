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

namespace StoloviePribori
{
    /// <summary>
    /// Interaction logic for StoloviePriboriWindow.xaml
    /// </summary>
    public partial class StoloviePriboriWindow : Window
    {
        private Base.User user;
        public StoloviePriboriWindow(Base.User user)
        {
            InitializeComponent();
            this.user = user;
            if(user != null)
            {
                userNameLabel.Content = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
            }
            else
            {
                userNameLabel.Content = "Вы вошли как Гость!";
            }
        }

        private void BackToAuth_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void openProductsPage_Click(object sender, RoutedEventArgs e)
        {
            frameForPages.Navigate(new Pages.ProductsPage(user));
        }
    }
}

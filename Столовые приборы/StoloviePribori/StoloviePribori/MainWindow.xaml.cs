using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace StoloviePribori
{
    /// <summary>
    /// Класс для реализации логики авторизации
    /// </summary>
    public partial class MainWindow : Window
    {
        private Base.User user;
        private int _second;
        private DispatcherTimer timer;
        private bool isFirstConnect = true;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LogQuest_Click(object sender, RoutedEventArgs e)
        {
            new StoloviePriboriWindow(null).Show();
            Close();
        }

        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text;
            string password = passwordTextBox.Text;
            user = SourceCore.entities1.User.FirstOrDefault(i => i.UserLogin.Equals(login) && i.UserPassword.Equals(password));
            if (user != null && captchaReadTextBox.Text.Equals(captchaWriteTextBox.Text))
            {
                new StoloviePriboriWindow(user).Show();
                Close();
            }
            else
            {
                if (isFirstConnect)
                {
                    isFirstConnect = false;
                    MessageBox.Show("Неудачный вход!");
                }
                else
                {
                    _second = 0;
                    AuthorizationBtn.IsEnabled = false;
                    timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(1);
                    timer.Tick += Timer_Tick;
                    timer.Start();
                }

                String allowchar = " ";
                allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
                allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
                allowchar += "1,2,3,4,5,6,7,8,9,0";
                char[] a = { ',' };
                String[] ar = allowchar.Split(a);
                String pwd = "";
                string temp = " ";
                Random r = new Random();
                for (int i = 0; i < 6; i++)
                {
                    temp = ar[(r.Next(0, ar.Length))];
                    pwd += temp;
                }
                captchaBlock.Visibility = Visibility;
                captchaReadTextBox.Text = pwd;

            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(_second > 10)
            {
                _second = 0;
                AuthorizationBtn.IsEnabled = true;
                timer.Stop();
            }
            else
            {
                _second++;
            }
        }
    }
}

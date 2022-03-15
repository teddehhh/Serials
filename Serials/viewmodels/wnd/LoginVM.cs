using Serials.cmd;
using Serials.data;
using Serials.viewmodels.users;
using Serials.views;
using System.Security;
using System.Windows;

namespace Serials.viewmodels.wnd
{
    internal class LoginVM : BaseViewModel
    {
        public string Login { get; set; }
        public SecureString Password { get; set; }
        private RelayCommand _loginCommand;
        public RelayCommand LoginCommand => _loginCommand ?? new(obj =>
        {
            try
            {
                DataProvider.Default = new DataProvider(Login, Password);
            }
            catch (System.Exception)
            {
                MessageBox.Show("Пользователь не найден!");
                return;
            }
            string role = DataProvider.Default.GetRole();
            MainVM.SelectedViewModel = role == "db_owner" ? new AdminVM() : new ReaderVM();
            MainWindow mainWindow = new MainWindow()
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            Application.Current.MainWindow.Close();
            mainWindow.Show();
        });
    }
}

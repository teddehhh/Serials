using System.Windows.Controls;

namespace Serials.views.controls
{
    /// <summary>
    /// Логика взаимодействия для AdminControl.xaml
    /// </summary>
    public partial class AdminControl : UserControl
    {
        public AdminControl()
        {
            InitializeComponent();
        }
        private void PasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            ((dynamic)DataContext).Password = ((PasswordBox)sender).SecurePassword;
        }
    }
}

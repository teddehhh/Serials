using Serials.data;
using Serials.viewmodels.wnd;
using System;
using System.Windows;

namespace Serials.views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainVM();
        }
        protected override void OnClosed(EventArgs e)
        {
            DataProvider.Default.ConnectionClose();
            base.OnClosed(e);
        }
    }
}

using Serials.cmd;
using Serials.data;
using Serials.models;
using Serials.viewmodels.lists;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Serials.viewmodels.users
{
    internal class AdminVM : BaseRoleVM
    {
        public override string Name => "Admin";
        public SerialActorListVM SerialActorList { get; set; }
        public ActorListVM ActorList { get; set; }
        public CountryListVM CountryList { get; set; }
        public SerialListVM SerialList { get; set; }
        public VisitorListVM VisitorList { get; set; }
        public CommentListVM CommentList { get; set; }
        public GenreListVM GenreList { get; set; }
        public AdminVM()
        {
            SerialActorList = new();
            ActorList = SerialActorList.ActorListVM;
            CountryList = ActorList.CountryListVM;
            SerialList = SerialActorList.SerialListVM;
            GenreList = SerialList.GenreListVM;
            CommentList = new();
            VisitorList = CommentList.VisitorListVM;
            CommentList.SerialListVM = SerialActorList.SerialListVM;
        }
        public string Login { get; set; }
        public SecureString Password { get; set; }
        public int GenreID { get; set; }
        private RelayCommand addusercmd;
        private RelayCommand openconstcmd;
        public RelayCommand AddUserCmd => addusercmd ?? new(obj =>
        {
            if (string.IsNullOrEmpty(Login) || Login.Contains(' ') || Password.Length == 0 || GenreID is 0)
            {
                MessageBox.Show("Ошибка");
                return;
            }
            try
            {
                DataProvider.Default.AddUser(Login, Password, GenreID);
            }
            catch (System.Exception)
            {
                MessageBox.Show("Выбрите другое имя");
                return;
            }
            MessageBox.Show("Успешная регистрация");
            ClearFields();
        });
        public void ClearFields()
        {
            Login = string.Empty;
            GenreID = 0;
            OnPropertyChanged(nameof(Login));
            OnPropertyChanged(nameof(GenreID));
        }
        public RelayCommand OpenConstructorCmd => openconstcmd ?? new(obj =>
        {
            OpenQConstructor();
        });
        private void OpenQConstructor()
        {
            using (Process p = new())
            {
                p.StartInfo.FileName = @"C:\Users\ted\source\repos\teddehhh\Query\bin\Debug\Query.exe";
                _ = p.Start();
            }
        }
    }
}

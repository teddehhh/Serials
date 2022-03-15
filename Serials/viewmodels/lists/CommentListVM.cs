using Serials.cmd;
using Serials.data;
using Serials.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Serials.viewmodels.lists
{
    internal class CommentListVM : BaseViewModel
    {
        public static CommentListVM Instance { get; set; }
        public ObservableCollection<CommentVM> Collection { get; set; }
        public CommentVM SelectedItem { get; set; }
        public VisitorListVM VisitorListVM { get; set; }
        public SerialListVM SerialListVM { get; set; }
        public CommentListVM()
        {
            Collection = new();
            var items = DataProvider.Default.GetComments() as List<Comment>;
            foreach (var item in items)
            {
                Collection.Add(new(item, this));
            }
            VisitorListVM = new();
            SerialListVM = SerialActorListVM.Instance.SerialListVM;
            Instance = this;
        }
        public int SerialID { get; set; }
        public int VisitorID { get; set; }
        public DateTime CommentDate { get; set; } = DateTime.Now;
        public string CommentText { get; set; }

        private RelayCommand addcmd;
        private RelayCommand delcmd;
        public RelayCommand AddCmd => addcmd ?? new(obj =>
        {
            int id;
            if (string.IsNullOrEmpty(CommentText) || SerialID == 0 || VisitorID == 0)
            {
                MessageBox.Show("Проверьте вводимые данные!");
                return;
            }
            try
            {
                id = DataProvider.Default.AddComment(SerialID, VisitorID, CommentDate, CommentText);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка добавления!");
                return;
            }
            CommentVM item = new(new() { CommentID = id, CommentDate = CommentDate, SerialID = SerialID, CommentText = CommentText, VisitorID = VisitorID }, this);
            Collection.Add(item);
            ClearFields();
        });

        private void ClearFields()
        {
            CommentText = String.Empty;
            SerialID = VisitorID = 0;
            OnPropertyChanged(nameof(CommentText));
            OnPropertyChanged(nameof(SerialID));
            OnPropertyChanged(nameof(CommentDate));
            OnPropertyChanged(nameof(VisitorID));
        }

        public RelayCommand DelCmd => delcmd ?? new(obj =>
        {
            if (SelectedItem is null)
            {
                MessageBox.Show("Строка для удаления не выбрана!");
                return;
            }
            DataProvider.Default.DeleteItem("Comment", SelectedItem.item.CommentID);
            Collection.Remove(SelectedItem);
        });
    }
}

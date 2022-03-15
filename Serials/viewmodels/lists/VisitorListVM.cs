using Serials.cmd;
using Serials.data;
using Serials.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Serials.viewmodels.lists
{
    internal class VisitorListVM : BaseViewModel
    {
        public static VisitorListVM Instance { get; set; }
        public ObservableCollection<VisitorVM> Collection { get; set; }
        public VisitorVM SelectedItem { get; set; }
        public VisitorListVM()
        {
            Collection = new();
            var items = DataProvider.Default.GetVisitors() as List<Visitor>;
            foreach (var item in items)
            {
                Collection.Add(new(item, this));
            }
            Instance = this;
        }
        public string Name { get; set; }
        public DateTime Birthday { get; set; } = DateTime.Now;

        private RelayCommand addcmd;
        private RelayCommand delcmd;
        public RelayCommand AddCmd => addcmd ?? new(obj =>
        {
            int id;
            if (string.IsNullOrEmpty(Name))
            {
                MessageBox.Show("Проверьте вводимые данные!");
                return;
            }
            try
            {
                id = DataProvider.Default.AddVisitor(Birthday, Name);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка добавления!");
                return;
            }
            VisitorVM item = new(new() { VisitorID = id, Birthday = Birthday, Name = Name }, this);
            Collection.Add(item);
            ClearFields();
        });

        private void ClearFields()
        {
            Name = String.Empty;
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Birthday));
        }

        public RelayCommand DelCmd => delcmd ?? new(obj =>
        {
            if (SelectedItem is null)
            {
                MessageBox.Show("Строка для удаления не выбрана!");
                return;
            }
            DataProvider.Default.DeleteItem("Visitor", SelectedItem.item.VisitorID);
            Collection.Remove(SelectedItem);
        });
    }
}

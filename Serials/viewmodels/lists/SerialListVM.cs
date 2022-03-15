using Serials.cmd;
using Serials.data;
using Serials.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Serials.viewmodels.lists
{
    internal class SerialListVM : BaseViewModel
    {
        public static SerialListVM Instance { get; set; }
        public ObservableCollection<SerialVM> Collection { get; set; }
        public SerialVM SelectedItem { get; set; }
        public GenreListVM GenreListVM { get; set; }
        public SerialListVM()
        {
            Collection = new();
            var items = DataProvider.Default.GetSerials() as List<Serial>;
            foreach (var item in items)
            {
                Collection.Add(new(item, this));
            }
            GenreListVM = new();
            Instance = this;
        }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
        public int GenreID { get; set; }

        private RelayCommand addcmd;
        private RelayCommand delcmd;
        public RelayCommand AddCmd => addcmd ?? new(obj =>
        {
            int id;
            if (string.IsNullOrEmpty(Title) || GenreID == 0)
            {
                MessageBox.Show("Проверьте вводимые данные!");
                return;
            }
            try
            {
                id = DataProvider.Default.AddSerial(Title, ReleaseDate, GenreID);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка добавления!");
                return;
            }
            SerialVM item = new(new() { SerialID = id, Title = Title, GenreID = GenreID }, this);
            Collection.Add(item);
            ClearFields();
        });

        private void ClearFields()
        {
            Title = String.Empty;
            GenreID = 0;
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(ReleaseDate));
            OnPropertyChanged(nameof(GenreID));
        }

        public RelayCommand DelCmd => delcmd ?? new(obj =>
        {
            if (SelectedItem is null)
            {
                MessageBox.Show("Строка для удаления не выбрана");
                return;
            }
            DataProvider.Default.DeleteItem("Serial", SelectedItem.item.SerialID);
            Collection.Remove(SelectedItem);
        });
    }
}

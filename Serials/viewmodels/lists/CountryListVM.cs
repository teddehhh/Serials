using Serials.cmd;
using Serials.data;
using Serials.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Serials.viewmodels.lists
{
    internal class CountryListVM : BaseViewModel
    {
        public ObservableCollection<CountryVM> Collection { get; set; }
        public CountryVM SelectedItem { get; set; }
        public CountryListVM()
        {
            Collection = new();
            var items = DataProvider.Default.GetCountries() as List<Country>;
            foreach (var item in items)
            {
                Collection.Add(new(item, this));
            }
        }
        public string Name { get; set; }

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
                id = DataProvider.Default.AddCountry(Name);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка добавления!");
                return;
            }
            CountryVM item = new(new() { CountryID = id, Name = Name }, this);
            Collection.Add(item);
            ClearFields();
        });

        private void ClearFields()
        {
            Name = String.Empty;
            OnPropertyChanged(nameof(Name));
        }

        public RelayCommand DelCmd => delcmd ?? new(obj =>
        {
            if (SelectedItem is null)
            {
                MessageBox.Show("Строка для удаления не выбрана");
                return;
            }
            DataProvider.Default.DeleteItem("Country", SelectedItem.item.CountryID);
            Collection.Remove(SelectedItem);
        });
    }
}

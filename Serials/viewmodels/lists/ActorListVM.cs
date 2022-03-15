using Serials.cmd;
using Serials.data;
using Serials.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Serials.viewmodels.lists
{
    internal class ActorListVM : BaseViewModel
    {
        public static ActorListVM Instance { get; set; }
        public ObservableCollection<ActorVM> Collection { get; set; }
        public ActorVM SelectedItem { get; set; }
        public CountryListVM CountryListVM { get; set; }
        public ActorListVM()
        {
            Collection = new();
            var items = DataProvider.Default.GetActors() as List<Actor>;
            foreach (var item in items)
            {
                Collection.Add(new(item, this));
            }
            CountryListVM = new();
            Instance = this;
        }
        public string Name { get; set; }
        public int CountryID { get; set; }

        private RelayCommand addcmd;
        private RelayCommand delcmd;
        public RelayCommand AddCmd => addcmd ?? new(obj =>
        {
            int id;
            if (string.IsNullOrEmpty(Name) || CountryID == 0)
            {
                MessageBox.Show("Проверьте вводимые данные!");
                return;
            }
            try
            {
                id = DataProvider.Default.AddActor(Name, CountryID);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка добавления!");
                return;
            }
            ActorVM item = new(new() { ActorID = id, Name = Name, CountryID = CountryID }, this);
            Collection.Add(item);
            ClearFields();
        });

        private void ClearFields()
        {
            Name = String.Empty;
            CountryID = 0;
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(CountryID));
        }

        public RelayCommand DelCmd => delcmd ?? new(obj =>
        {
            if (SelectedItem is null)
            {
                MessageBox.Show("Строка для удаления не выбрана");
                return;
            }
            DataProvider.Default.DeleteItem("Actor", SelectedItem.item.ActorID);
            Collection.Remove(SelectedItem);
        });
    }
}

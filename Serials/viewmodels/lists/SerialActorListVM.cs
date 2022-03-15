using Serials.cmd;
using Serials.data;
using Serials.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Serials.viewmodels.lists
{
    internal class SerialActorListVM : BaseViewModel
    {
        public static SerialActorListVM Instance { get; set; }
        public ObservableCollection<SerialActorVM> Collection { get; set; }
        public SerialActorVM SelectedItem { get; set; }
        public SerialListVM SerialListVM { get; set; }
        public ActorListVM ActorListVM { get; set; }
        public SerialActorListVM()
        {
            Collection = new();
            var items = DataProvider.Default.GetSerialActors() as List<SerialActor>;
            foreach (var item in items)
            {
                Collection.Add(new(item, this));
            }
            SerialListVM = new();
            ActorListVM = new();
            Instance = this;
        }
        public int SerialID { get; set; }
        public int ActorID { get; set; }

        private RelayCommand addcmd;
        private RelayCommand delcmd;
        public RelayCommand AddCmd => addcmd ?? new(obj =>
        {
            int id;
            if (SerialID == 0 || ActorID == 0)
            {
                MessageBox.Show("Проверьте вводимые данные!");
                return;
            }
            try
            {
                DataProvider.Default.AddSerialActor(SerialID, ActorID);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка добавления!");
                return;
            }
            SerialActorVM item = new(new() { SerialID = SerialID, ActorID = ActorID }, this);
            Collection.Add(item);
            ClearFields();
        });

        private void ClearFields()
        {
            SerialID = ActorID = 0;
            OnPropertyChanged(nameof(SerialID));
            OnPropertyChanged(nameof(ActorID));
        }

        public RelayCommand DelCmd => delcmd ?? new(obj =>
        {
            if (SelectedItem is null)
            {
                MessageBox.Show("Строка для удаления не выбрана!");
                return;
            }
            DataProvider.Default.DeleteSerialActor(SelectedItem.item.SerialID, SelectedItem.item.ActorID);
            Collection.Remove(SelectedItem);
        });
        private string actorName;
        public string ActorName
        {
            get => actorName;
            set
            {
                actorName = value;
                OnPropertyChanged(nameof(CollectionView));
            }
        }
        public ICollectionView CollectionView
        {
            get
            {
                var cv = CollectionViewSource.GetDefaultView(Collection);
                cv.Filter = string.IsNullOrEmpty(ActorName) ? null : x =>
                {
                    SerialActorVM item = x as SerialActorVM;
                    return ActorListVM.Collection.Where(y => y.item.ActorID == item.ActorID).First().Name.Contains(ActorName);
                };
                return cv;
            }
        }
    }
}

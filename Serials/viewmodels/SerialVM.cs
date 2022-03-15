using Serials.data;
using Serials.models;
using Serials.viewmodels.lists;
using System;
using System.Collections.ObjectModel;

namespace Serials.viewmodels
{
    internal class SerialVM : BaseViewModel
    {
        public Serial item { get; set; }
        public SerialListVM ParentList { get; set; }
        public ObservableCollection<GenreVM> GenreList => ParentList.GenreListVM.Collection;
        public SerialVM(Serial item, SerialListVM parent)
        {
            this.item = item;
            ParentList = parent;
        }
        public string Title
        {
            get
            {
                return item.Title;
            }
            set
            {
                item.Title = value;
                DataProvider.Default.Update(value, nameof(Serial), item.SerialID);
                OnPropertyChanged(nameof(Title));
            }
        }
        public DateTime ReleaseDate
        {
            get
            {
                return item.ReleaseDate;
            }
            set
            {
                item.ReleaseDate = value;
                DataProvider.Default.Update(value, nameof(Serial), item.SerialID);
                OnPropertyChanged(nameof(ReleaseDate));
            }
        }
        public int GenreID
        {
            get
            {
                return item.GenreID;
            }
            set
            {
                item.GenreID = value;
                DataProvider.Default.Update(value, nameof(Serial), item.SerialID);
                OnPropertyChanged(nameof(GenreID));
            }
        }
    }
}

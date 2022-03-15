using Serials.data;
using Serials.models;
using Serials.viewmodels.lists;
using System.Collections.ObjectModel;

namespace Serials.viewmodels
{
    internal class ActorVM : BaseViewModel
    {
        public Actor item { get; set; }
        public ActorListVM ParentList { get; set; }
        public ObservableCollection<CountryVM> CountryList => ParentList.CountryListVM.Collection;
        public ActorVM(Actor item, ActorListVM parent)
        {
            this.item = item;
            ParentList = parent;
        }
        public string Name
        {
            get
            {
                return item.Name;
            }
            set
            {
                item.Name = value;
                DataProvider.Default.Update(value, nameof(Actor), item.ActorID);
                OnPropertyChanged(nameof(Name));
            }
        }
        public int CountryID
        {
            get
            {
                return item.CountryID;
            }
            set
            {
                item.CountryID = value;
                DataProvider.Default.Update(value, nameof(Actor), item.ActorID);
                OnPropertyChanged(nameof(CountryID));
            }
        }
    }
}

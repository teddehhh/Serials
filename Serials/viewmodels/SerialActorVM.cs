using Serials.data;
using Serials.models;
using Serials.viewmodels.lists;
using System.Collections.ObjectModel;

namespace Serials.viewmodels
{
    internal class SerialActorVM : BaseViewModel
    {
        public SerialActor item { get; set; }
        public SerialActorListVM ParentList { get; set; }
        public ObservableCollection<SerialVM> SerialList => ParentList.SerialListVM.Collection;
        public ObservableCollection<ActorVM> ActorList => ParentList.ActorListVM.Collection;
        public SerialActorVM(SerialActor item, SerialActorListVM parent)
        {
            this.item = item;
            ParentList = parent;
        }
        public int SerialID
        {
            get
            {
                return item.SerialID;
            }
            set
            {
                DataProvider.Default.UpdateSerialActor(value, item.SerialID, item.ActorID);
                item.SerialID = value;
                OnPropertyChanged(nameof(SerialID));
            }
        }
        public int ActorID
        {
            get
            {
                return item.ActorID;
            }
            set
            {
                DataProvider.Default.UpdateSerialActor(value, item.SerialID, item.ActorID);
                item.ActorID = value;
                OnPropertyChanged(nameof(ActorID));
            }
        }
    }
}

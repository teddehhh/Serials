using Serials.data;
using Serials.models;
using Serials.viewmodels.lists;
using System;

namespace Serials.viewmodels
{
    internal class VisitorVM : BaseViewModel
    {
        public Visitor item { get; set; }
        public VisitorListVM ParentList { get; set; }
        public VisitorVM(Visitor item, VisitorListVM parent)
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
                DataProvider.Default.Update(value, nameof(Visitor), item.VisitorID);
                OnPropertyChanged(nameof(Name));
            }
        }
        public DateTime Birthday
        {
            get
            {
                return item.Birthday;
            }
            set
            {
                item.Birthday = value;
                DataProvider.Default.Update(value, nameof(Visitor), item.VisitorID);
                OnPropertyChanged(nameof(Birthday));
            }
        }
    }
}

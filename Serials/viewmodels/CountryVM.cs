using Serials.data;
using Serials.models;
using Serials.viewmodels.lists;

namespace Serials.viewmodels
{
    internal class CountryVM : BaseViewModel
    {
        public Country item { get; set; }
        public CountryListVM ParentList { get; set; }
        public CountryVM(Country item, CountryListVM parent)
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
                DataProvider.Default.Update(value, nameof(Country), item.CountryID);
                OnPropertyChanged(nameof(Name));
            }
        }
    }
}

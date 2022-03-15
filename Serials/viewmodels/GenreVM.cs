using Serials.data;
using Serials.models;
using Serials.viewmodels.lists;

namespace Serials.viewmodels
{
    internal class GenreVM : BaseViewModel
    {
        public Genre item { get; set; }
        public GenreListVM ParentList { get; set; }
        public GenreVM(Genre item, GenreListVM parent)
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
                DataProvider.Default.Update(value, nameof(Genre), item.GenreID);
                OnPropertyChanged(nameof(Name));
            }
        }
    }
}

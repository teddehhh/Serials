using Serials.data;
using Serials.models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Serials.viewmodels.lists
{
    internal class SerialViewListVM : BaseViewModel
    {
        public static SerialViewListVM Instance { get; set; }
        public ObservableCollection<SerialViewVM> Collection { get; set; }
        public SerialViewListVM()
        {
            Collection = new();
            var items = DataProvider.Default.GetSerialsView() as List<SerialView>;
            foreach (var item in items)
            {
                Collection.Add(new(item, this));
            }
            Instance = this;
        }
    }
}

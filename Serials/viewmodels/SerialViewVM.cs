using Serials.models;
using Serials.viewmodels.lists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serials.viewmodels
{
    internal class SerialViewVM : BaseViewModel
    {
        public SerialView item { get; set; }
        public SerialViewListVM ParentList { get; set; }
        public SerialViewVM(SerialView item, SerialViewListVM parent)
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
        }
        public DateTime ReleaseDate
        {
            get
            {
                return item.ReleaseDate;
            }
        }
        public string Genre
        {
            get
            {
                return item.Genre;
            }
        }
    }
}

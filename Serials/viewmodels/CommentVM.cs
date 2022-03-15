using Serials.data;
using Serials.models;
using Serials.viewmodels.lists;
using System;
using System.Collections.ObjectModel;

namespace Serials.viewmodels
{
    internal class CommentVM : BaseViewModel
    {
        public Comment item { get; set; }
        public CommentListVM ParentList { get; set; }
        public ObservableCollection<SerialVM> SerialList => ParentList.SerialListVM.Collection;
        public ObservableCollection<VisitorVM> VisitorList => ParentList.VisitorListVM.Collection;
        public CommentVM(Comment item, CommentListVM parent)
        {
            this.item = item;
            ParentList = parent;
        }
        public string CommentText
        {
            get
            {
                return item.CommentText;
            }
            set
            {
                item.CommentText = value;
                DataProvider.Default.Update(value, nameof(Comment), item.CommentID);
                OnPropertyChanged(nameof(CommentText));
            }
        }
        public int SerialID
        {
            get
            {
                return item.SerialID;
            }
            set
            {
                item.SerialID = value;
                DataProvider.Default.Update(value, nameof(Comment), item.CommentID);
                OnPropertyChanged(nameof(SerialID));
            }
        }
        public int VisitorID
        {
            get
            {
                return item.VisitorID;
            }
            set
            {
                item.SerialID = value;
                DataProvider.Default.Update(value, nameof(Comment), item.CommentID);
                OnPropertyChanged(nameof(VisitorID));
            }
        }
        public DateTime CommentDate
        {
            get
            {
                return item.CommentDate;
            }
            set
            {
                item.CommentDate = value;
                DataProvider.Default.Update(value, nameof(Comment), item.CommentID);
                OnPropertyChanged(nameof(VisitorID));
            }
        }
    }
}

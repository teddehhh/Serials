using Serials.viewmodels.lists;

namespace Serials.viewmodels.users
{
    internal class ReaderVM : BaseRoleVM
    {
        public override string Name => "Reader";
        public SerialViewListVM SerialViewList { get; set; }
        public ReaderVM()
        {
            SerialViewList = new();
        }
    }
}

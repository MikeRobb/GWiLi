using GWiLi.Data;
using GWiLi.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace GWiLi.Areas.List.Models
{
    public class ListViewModel
    {
        #region Property
        public IList<ListResultViewModel> Results { get; set; }
        #endregion

        #region Constructor
        public ListViewModel()
        {
            Results = new List<ListResultViewModel>();
        }
        #endregion

        #region Method
        public void Populate(IList<EntityFramework.List> lists)
        {
            Results = lists.Select(x => new ListResultViewModel(x)).ToList();
        }
        #endregion

        #region SubClass
        public class ListResultViewModel
        {
            public int ListId { get; set; }
            public string Name { get; set; }
            public string Owner { get; set; }
            public string Status { get; set; }
            public string StatusPanelClass { get; set; }

            public ListResultViewModel(EntityFramework.List obj)
            {
                ListId = obj.ListId;
                Name = obj.Name;
                Owner = obj.OwnerId.ToString();
                Status = obj.ListStatus.DisplayName;
                StatusPanelClass = GetStatusPanelClass(obj.ListStatusId);
            }

            private string GetStatusPanelClass(int listStatusId)
            {
                switch (listStatusId)
                {
                    case (int)ListStatusEnum.Active:
                        return "panel-primary";
                    case (int)ListStatusEnum.Inactive:
                        return "panel-warning";
                    case (int)ListStatusEnum.Deleted:
                        return "panel-danger";
                    default:
                        return "panel-default";
                }
            }
        }
        #endregion
    }
}
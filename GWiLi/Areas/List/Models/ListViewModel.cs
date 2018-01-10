using GWiLi.EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GWiLi.Areas.List.Models
{
    public class ListViewModel
    {
        #region Properties

        public int ListId { get; set; }
        public string DisplayName { get; set; }
        public string Owner { get; set; }
        public string Status { get; set; }
        public string Privacy { get; set; }
        public IList<ItemViewModel> Items { get; set; }

        #endregion

        #region Select Lists

        public SelectList CategorySelectList { get; set; }

        #endregion

        #region Constructors

        public ListViewModel()
        {
            Items = new List<ItemViewModel>();
        }

        public ListViewModel(EntityFramework.List list) : this()
        {
            ListId = list.Id;
            DisplayName = list.DisplayName;
            if(string.IsNullOrEmpty(DisplayName))
            {
                DisplayName = list.Owner.GetDisplayName() + "'s List";
            }
            Owner = list.Owner.GetDisplayName();
            Status = list.Status.DisplayName;
            Privacy = list.Privacy.DisplayName;
            Items = list.Items.Select(x => new ItemViewModel(x)).ToList();
        }

        #endregion

        #region Methods

        public void PopulateSelectLists(IList<EntityFramework.Category> categories)
        {
            CategorySelectList = new SelectList(categories.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.DisplayName }), "Value", "Text");
        }

        #endregion

        #region Subclasses

        public class ItemViewModel
        {
            #region Properties

            public int ItemId { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public int Quantity { get; set; }
            public string Price { get; set; }
            public string LocationDisplay { get; set; }
            public string LocationUri { get; set; }
            public string LocationHref
            {
                get
                {
                    if(!string.IsNullOrEmpty(LocationUri))
                    {
                        LocationDisplay = !string.IsNullOrEmpty(LocationDisplay) ? LocationDisplay : LocationUri;
                        return $"<a href='{LocationUri}' onClick='return confirm(\"You are potentially leaving GWiLi, please confirm you are willing to visit the following URL: {LocationUri}\")' target='_blank' class='gwili_external'>{LocationDisplay}</a>";
                    }

                    return LocationDisplay;
                }
            }
            public string Metadata { get; set; }

            #endregion

            #region Constructors

            public ItemViewModel(EntityFramework.Item item)
            {
                ItemId = item.Id;
                Category = item.Category.GetParentalDisplayName();
                Name = item.DisplayName;
                var activities = item.ItemActivities;
                Quantity = Math.Max(item.Quantity - activities.Sum(x => x.Quantity), 0);
                Price = item.Price ?? string.Empty;
                LocationDisplay = item.LocationDisplayName;
                LocationUri = item.LocationUri;
                Metadata = item.Metadata;
            }

            #endregion

            #region Methods

            #endregion

            #region Subclasses

            #endregion
        }

        public class AddItemModel
        {
            #region Properties
            
            public int newCategory { get; set; }
            public string newName { get; set; }
            public int newQuantity { get; set; }
            public string newLink { get; set; }
            public string newUri { get; set; }
            public string newPrice { get; set; }
            public string newMetadata { get; set; }

            #endregion

            #region Constructors

            #endregion

            #region Methods

            #endregion

            #region Subclasses

            #endregion
        }

        #endregion
    }
}
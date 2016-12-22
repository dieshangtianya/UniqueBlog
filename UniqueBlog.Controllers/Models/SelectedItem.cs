using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Controllers.Models
{
    /// <summary>
    /// Used to bind to the UI selected list
    /// </summary>
    public class SelectedItem
    {
        public string ItemName { get; set; }

        public string ItemId { get; set; }

        public bool IsSelected { get; set; }

        public SelectedItem()
        {

        }

        public SelectedItem(string itemId,string itemName)
        {
            this.ItemId = itemId;
            this.ItemName = itemName;
            this.IsSelected = false;
        }
    }
}

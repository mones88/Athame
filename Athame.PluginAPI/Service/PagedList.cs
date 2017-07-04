using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athame.PluginAPI.Service
{
    public class PagedList<T>
    {
        /// <summary>
        /// A list of every item that has been retrieved so far.
        /// </summary>
        public virtual IList<T> AllItems { get; set; }

        /// <summary>
        /// The amount of items that are retrieved per page.
        /// </summary>
        public virtual int ItemsPerPage { get; set; }

        /// <summary>
        /// The total amount of items that can be retrieved.
        /// </summary>
        public virtual int TotalNumberOfItems { get; set; }

        /// <summary>
        /// Whether the current method instance has more items that can be loaded.
        /// </summary>
        public virtual bool HasMoreItems { get; set; }
    }
}

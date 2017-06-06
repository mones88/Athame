using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athame.PluginAPI.Service
{
    /// <summary>
    /// Represents a collection of items that are retrieved as pages from a service.
    /// </summary>
    /// <typeparam name="T">The type of each item.</typeparam>
    public abstract class PagedMethod<T>
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="itemsPerPage">The amount of items per page to load.</param>
        protected PagedMethod(int itemsPerPage)
        {
            ItemsPerPage = itemsPerPage;
        }

        /// <summary>
        /// A list of every item that has been retrieved so far.
        /// </summary>
        public IList<T> AllItems { get; set; }

        /// <summary>
        /// The amount of items that are retrieved per page.
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// The total amount of items that can be retrieved.
        /// </summary>
        public int TotalNumberOfItems { get; set; }

        /// <summary>
        /// Whether the current method instance has more items that can be loaded.
        /// </summary>
        public abstract bool HasMoreItems { get; }

        /// <summary>
        /// Retrieves the next page asynchronously and appends the result to <see cref="AllItems"/>.
        /// </summary>
        /// <returns>The next page's contents.</returns>
        public abstract Task<IList<T>> GetNextPageAsync();

        /// <summary>
        /// Retrieves each page sequentially until there are none left.
        /// </summary>
        public virtual async Task LoadAllPagesAsync()
        {
            while (HasMoreItems)
            {
                await GetNextPageAsync();
            }
        }
    }
}

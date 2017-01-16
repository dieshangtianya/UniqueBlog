using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueBlog.Controllers.Models
{
    /// <summary>
    /// Pagination information
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// Get the total amount of items
        /// </summary>
        public int TotalItems { get; private set; }

        /// <summary>
        /// Get the current page requested
        /// </summary>
        public int CurrentPage { get; private set; }

        /// <summary>
        /// Get the page size of the page can contain
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// Get the total page number
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// Get the start page number(Generally is 1)
        /// </summary>
        public int StartPage { get; private set; }

        /// <summary>
        /// Get the end page number
        /// </summary>
        public int EndPage { get; private set; }


        #region construction
        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalItems"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public Pagination(int totalItems,int? pageIndex,int pageSize=10)
        {
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = pageIndex == null ? 1 : (int)pageIndex;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;

            //because the start page less than 0, so the start page will move to right about (|startpage|+1)
            //so the end page also should also move right about (|startpage|+1)
            //this logic comes from http://jasonwatmore.com/post/2015/10/30/aspnet-mvc-pagination-example-with-logic-like-google
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            this.TotalItems = totalItems;
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
            this.TotalPages = totalPages;
            this.StartPage = startPage;
            this.EndPage = endPage;
        }

        #endregion
    }
}

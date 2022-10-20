using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Common.Pagination
{
    public class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
        private int firstRowOnPage;
        private int lastRowOnPage;



        public int FirstRowOnPage
        {
            set { firstRowOnPage = value; }
            get { return (CurrentPage - 1) * PageSize + 1; }

        }

        public int LastRowOnPage
        {
            set { lastRowOnPage = value; }
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }
    }
}

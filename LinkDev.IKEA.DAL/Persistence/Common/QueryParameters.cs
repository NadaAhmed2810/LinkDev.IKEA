using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DAL.Persistence.Common
{
    public class QueryParameters
    {
        private const int MaxPageSize = 20;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 10;
        public string? SearchTerm { get; set; }

        public int PageSize {
            get=> _pageSize;
            set
            {
                if (value > MaxPageSize)
                {
                    _pageSize = MaxPageSize;
                }
                else if (value < 1)
                {
                    _pageSize = 1;
                }
                else
                {
                    _pageSize = value;
                }
            }
        }

    }
}

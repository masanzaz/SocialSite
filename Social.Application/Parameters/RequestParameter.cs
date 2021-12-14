using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Application.Parameters
{
    public class RequestParameter
    {
        const int MaxPageSize = 100;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public RequestParameter()
        {
            this.PageNumber = 1;
            this.PageSize = MaxPageSize;
        }
        public RequestParameter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
        }
    }
}

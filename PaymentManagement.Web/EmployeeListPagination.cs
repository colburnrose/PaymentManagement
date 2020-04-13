using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentManagement.Web
{
    public class EmployeeListPagination<T> : List<T>
    {
        public int PageIndex { get; }
        public int TotalPages { get; }

        public EmployeeListPagination(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        // Enable or Disable our paging button
        public bool IsPreviousPage => PageIndex > 1;
        public bool IsNextPage => PageIndex < TotalPages;

        public static EmployeeListPagination<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var list = source.ToList();
            var count = list.Count;
            var items = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new EmployeeListPagination<T>(items, count, pageIndex, pageSize);
        }
    }
}

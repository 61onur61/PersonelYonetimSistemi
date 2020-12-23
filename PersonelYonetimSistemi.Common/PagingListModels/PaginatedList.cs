using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonelYonetimSistemi.Common.PagingListModels
{
    public class PaginatedList<T> : List<T>
    {
        public int SayfaIndex { get; private set; }

        public int ToplamSayfaSayisi { get; set; }

        public PaginatedList(List<T> items , int count, int pageIndex, int pageSize)
        {
            SayfaIndex = pageIndex;
            ToplamSayfaSayisi =(int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public bool PreviousPage 
        {
            get
            {
                return (SayfaIndex > 1);
            }
        }

        public bool NextPage
        {
            get
            {
                return (SayfaIndex < ToplamSayfaSayisi);
            }
        }

        public static PaginatedList<T> Create(List<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}

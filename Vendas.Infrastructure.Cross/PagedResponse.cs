using System;
using System.Collections.Generic;

namespace Vendas.Infrastructure.Cross
{
    public class PagedResponse<TEntity> where TEntity : class
    {
        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public long? Count { get; private set; }

        public IEnumerable<TEntity> Data { get; private set; }

        public PagedResponse(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }

        public PagedResponse(int pageIndex, int pageSize, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;            
            Data = data;
        }
    }
}

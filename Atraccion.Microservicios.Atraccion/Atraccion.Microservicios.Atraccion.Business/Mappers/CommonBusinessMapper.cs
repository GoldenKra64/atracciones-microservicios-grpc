using Atraccion.Microservicios.Atraccion.Business.DTOs;
using Atraccion.Microservicios.Atraccion.DataManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.Mappers
{
    public static class CommonBusinessMapper
    {
        public static PagedResponse<TResponse> ToPagedResponse<TModel, TResponse>(
            DataPagedResult<TModel> data,
            Func<TModel, TResponse> mapper)
        {
            return new PagedResponse<TResponse>
            {
                Items = data.Items.Select(mapper),
                TotalRecords = data.TotalRecords,
                PageNumber = data.PageNumber,
                PageSize = data.PageSize,
                TotalPages = data.TotalPages,
                HasPreviousPage = data.HasPreviousPage,
                HasNextPage = data.HasNextPage
            };
        }
    }
}

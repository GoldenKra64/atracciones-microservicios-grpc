using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataManagement.Models
{
    public class DataPagedResult<T>
    {
        // ===============================
        // DATA
        // ===============================
        public IEnumerable<T> Items { get; set; } = new List<T>();

        // ===============================
        // PAGINACIÓN
        // ===============================
        public int TotalRecords { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        // ===============================
        // DERIVADOS (útiles)
        // ===============================
        public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}

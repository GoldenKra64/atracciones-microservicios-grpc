using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataManagement.Models.Reserva
{
    public class ReservaCreateModel
    {
        public string HorarioGuid { get; set; } = null!;
        public int? ClienteId { get; set; }
        public string Canal { get; set; } = null!;
        public List<DetalleReservaCreateModel> Lineas { get; set; } = new();
    }
}

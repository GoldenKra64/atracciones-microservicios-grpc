using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.DataManagement.Models.Horario
{
    public class HorarioUpdateModel
    {
        public int Id { get; set; }
        public int AtraccionId { get; set; }
        public string Fecha { get; set; }           // "yyyy-MM-dd"
        public string HoraInicio { get; set; }      // "HH:mm"
        public string? HoraFin { get; set; }        // "HH:mm"
        public int Cupos { get; set; }
    }
}

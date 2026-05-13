using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Reserva.DataManagement.Integrations
{
    public class HorarioIntegrationDto
    {
        public int HorId { get; set; }
        public string HorGuid { get; set; }
        public int CuposDisponibles { get; set; }
        public string HorFecha { get; set; }
        public string HorHoraInicio { get; set; }
        public string HorHoraFin { get; set; }
    }
}

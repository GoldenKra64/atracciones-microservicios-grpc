using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Horario
{
    public class CreateHorarioRequest
    {
        public string AtraccionId { get; set; }
        public string Fecha { get; set; }
        public string HoraInicio { get; set; }
        public string? HoraFin { get; set; }
        public int Cupos { get; set; }
    }
}

using Atraccion.Microservicios.Atraccion.Business.DTOs.Horario;
using Atraccion.Microservicios.Atraccion.Business.DTOs.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Atraccion
{
    public class AtraccionDetalleDto
    {
        // 🆔 Identificación
        public string id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public List<string> imagenes { get; set; } = new();
        public List<string> incluye { get; set; } = new();
        public List<string> no_incluye { get; set; } = new();
        public string punto_encuentro { get; set; }
        public Boolean incluye_transporte { get; set; }
        public Boolean incluye_acompaniante { get; set; }
        public List<TicketDto> tickets { get; set; } = new();
        public List<HorarioDto> horarios_proximos { get; set; } = new();


        // 🔗 HATEOAS
        public LinksDto Links { get; set; }
    }
}

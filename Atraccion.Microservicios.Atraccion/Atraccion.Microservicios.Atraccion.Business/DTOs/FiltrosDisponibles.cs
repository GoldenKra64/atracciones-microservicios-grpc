using Atraccion.Microservicios.Atraccion.Business.DTOs.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs
{
    public class FiltrosDisponibles
    {
        public List<OpcionFiltro> destinationFilters { get; set; } = new List<OpcionFiltro>();
        public List<OpcionFiltro> typeFilters { get; set; } = new List<OpcionFiltro>();
        public List<OpcionFiltro> labelFilters { get; set; } = new List<OpcionFiltro>();
        public List<OpcionFiltro> minRatingFilter { get; set; } = new List<OpcionFiltro>();
        public List<OpcionFiltro> timeOfDayFilter { get; set; } = new List<OpcionFiltro>();
        public List<OpcionFiltro> supportedLanguageFilters { get; set; } = new List<OpcionFiltro>();
        public List<OpcionFiltro> ufiFilters { get; set; } = new List<OpcionFiltro>();
    }
}
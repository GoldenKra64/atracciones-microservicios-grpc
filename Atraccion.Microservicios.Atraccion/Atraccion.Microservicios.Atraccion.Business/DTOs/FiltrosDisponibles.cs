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
        public OpcionFiltro destinationFilters;
        public OpcionFiltro typeFilters;
        public OpcionFiltro labelFilters;
        public OpcionFiltro minRatingFilter;
        public OpcionFiltro timeOfDayFilter;
        public OpcionFiltro supportedLanguageFilters;
        public OpcionFiltro ufiFilters;
    }
}
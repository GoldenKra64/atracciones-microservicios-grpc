using Atraccion.Microservicios.Atraccion.Business.DTOs.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Atraccion.Microservicios.Atraccion.Business.DTOs.Filters;
using System.Text.Json.Serialization;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs
{
    public class FiltrosDisponibles
    {
        public List<OpcionFiltro> destinationFilters { get; set; } = new List<OpcionFiltro>();
        public List<OpcionFiltro> typeFilters { get; set; } = new List<OpcionFiltro>();
        public List<OpcionFiltro> labelFilters { get; set; } = new List<OpcionFiltro>();
        
        [JsonPropertyName("minRatingFilters")]
        public List<OpcionFiltro> minRatingFilter { get; set; } = new List<OpcionFiltro>();
        
        [JsonPropertyName("timeOfDayFilters")]
        public List<OpcionFiltro> timeOfDayFilter { get; set; } = new List<OpcionFiltro>();
        
        public List<OpcionFiltro> supportedLanguageFilters { get; set; } = new List<OpcionFiltro>();
    }
}
using System;
using System.Collections.Generic;

namespace Atraccion.Microservicios.Atraccion.Business.DTOs.Atraccion
{
    public class FiltrosDisponiblesDto
    {
        public List<string> Ciudades { get; set; } = new();
        public List<string> Tipos { get; set; } = new();
        public List<string> Subtipos { get; set; } = new();
        public List<string> Etiquetas { get; set; } = new();
        public List<string> Idiomas { get; set; } = new();
        public List<string> Ordenamientos { get; set; } = new() 
        { 
            "trending", 
            "lowest_price", 
            "highest_weighted_rating" 
        };
    }
}

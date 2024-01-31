using Dominio.Entidades;
using Dominio.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.ViewModels
{
    public class NovaEspecieModel
    {
        public string? Especie { get; set; }
        public string? Vida { get; set; }
        public string? Energia { get; set; }
        public string? TempoVida { get; set; }
        public string? SelectedCaracteristica { get; set; }
        public List<EnumCaracteristicas> Caracteristicas { get; set; }
    }
}

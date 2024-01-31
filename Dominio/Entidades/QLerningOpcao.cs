using Dominio.Enumeradores;

namespace Dominio.Entidades
{
    public class QLerningOpcao
    {
        public EstadoIndividuo Estado { get; set; }
        public EnumCaracteristicas Caracteristica { get; set; }
        public double Peso { get; set; }
    }
}

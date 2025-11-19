using Acorazados.Enums;

namespace Acorazados.ClassData;

public class DatosTripulacionJugadores
{
    public List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> tripulacionJugadorUno { get; set; } = new();
    public List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> tripulacionJugadorDos { get; set; } = new();

}
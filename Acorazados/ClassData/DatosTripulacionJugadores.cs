using Acorazados.Enums;

namespace Acorazados.ClassData;

public class DatosTripulacionJugadores
{
    public List<(List<(int x, int y)> coordenadas, TipoBarco)> tripulacionJugadorUno { get; set; } = new();
    public List<(List<(int x, int y)> coordenadas, TipoBarco)> tripulacionJugadorDos { get; set; } = new();

}
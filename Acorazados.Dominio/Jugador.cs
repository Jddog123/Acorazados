using Acorazados.Dominio.Barcos;
using Acorazados.Dominio.Records;

namespace Acorazados.Dominio;

public class Jugador
{
    public string _nombre { private set; get; }
    public Tablero _tablero { private set; get; }
    private int _totalDisparos = 0;
    private int _totalFallos = 0;
    private int _totalAciertos = 0;
    private List<Barco> _barcosHundidos;
    
    public Jugador(string nombre, Tablero tablero)
    {
        _nombre = nombre;
        _tablero = tablero;
        _barcosHundidos = new List<Barco>();
    }

    public void SumarDisparoAEstadistica() => _totalDisparos++;
    public void SumarFallosAEstadistica() => _totalFallos++;
    public void SumarAciertosAEstadistica() => _totalAciertos++;
    public void AgregarBarcoHundidoAEstadistica(Barco barco) => _barcosHundidos.Add(barco);
    public EstadisticasJugador ObtenerEstadisticasJugador() => new(_totalDisparos, _totalFallos, _totalAciertos, _barcosHundidos);
}
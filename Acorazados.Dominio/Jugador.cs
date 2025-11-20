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

    public void SumarDisparo()
    {
        _totalDisparos++;
    }
    
    public void SumarFallos()
    {
        _totalFallos++;
    }
    
    public void SumarAciertos()
    {
        _totalAciertos++;
    }
    
    public void AgregarBarcoHundido(Barco barco)
    {
        _barcosHundidos.Add(barco);
    }

    public EstadisticasJugador ObtenerEstadisticasJugador()
    {
        return new EstadisticasJugador(_totalDisparos, _totalFallos, _totalAciertos, _barcosHundidos);
    }
}
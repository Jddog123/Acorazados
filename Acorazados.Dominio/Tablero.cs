using Acorazados.Dominio.Barcos;

namespace Acorazados.Dominio;

public class Tablero
{
    private const char LetraTableroBarcoAcertado = 'x';
    private const char LetraTableroBarcoHundido = 'X';
    private const char LetraTableroDisparoAlMar = 'o';
    private const char LetraTableroCanonero = 'g';
    private const char LetraTableroDestructor = 'd';
    private const char LetraTableroPortaaviones = 'c';
    private char[,] _casillas;

    public Tablero(char[,] casillas)
    {
        _casillas = casillas;
    }
    
    public void AsignarTripulacion(List<Barco> barcosJugador)
    {
        foreach (var barco in barcosJugador)
        {
            if (barco is Canonero canonero)
            {
                _casillas[canonero.CoordenadaX, canonero.CoordenadaY] = LetraTableroCanonero;
            }
            else if (barco is Destructor destructor)
            {
                foreach (var coordenadas in destructor.Coordenadas)
                {
                    _casillas[coordenadas.x, coordenadas.y] = LetraTableroDestructor;
                }
            }
            else if (barco is Portaaviones portaaviones)
            {
                foreach (var coordenadas in portaaviones.Coordenadas)
                {
                    _casillas[coordenadas.x, coordenadas.y] = LetraTableroPortaaviones;
                }
            }
        }
    }

    public char ObtenerValorCasilla(int coordenadaX, int coordenadaY) => _casillas[coordenadaX, coordenadaY];

    public void MarcaCoordenadaAcertadaEnPlataforma(int coordenadaX, int coordenadaY) =>
        _casillas[coordenadaX, coordenadaY] = LetraTableroBarcoAcertado;
    
    public void MarcaDisparoAlMarEnPlataforma(int coordenadaX, int coordenadaY) =>
        _casillas[coordenadaX, coordenadaY] = LetraTableroDisparoAlMar;
    
    public void MarcaCanoneroHundidoEnPlataforma(int coordenadaX, int coordenadaY) =>
        _casillas[coordenadaX, coordenadaY] = LetraTableroBarcoHundido;
    
    public void MarcaDestructorHundidoEnPlataforma(Destructor destructor)
    {
        foreach (var destructorCoordenada in destructor.Coordenadas)
        {
            _casillas[destructorCoordenada.x, destructorCoordenada.y] = LetraTableroBarcoHundido;
        }
    }
    
    public void MarcaPortaAvionHundidoEnPlataforma(Portaaviones portaAvion)
    {
        foreach (var destructorCoordenada in portaAvion.Coordenadas)
        {
            _casillas[destructorCoordenada.x, destructorCoordenada.y] = LetraTableroBarcoHundido;
        }
    }
}
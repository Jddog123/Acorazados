using Acorazados.Dominio.Barcos;
using Acorazados.Dominio.Constantes;

namespace Acorazados.Dominio;

public class Tablero
{
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
                AsignarLetraCanonero(canonero);
            }
            else if (barco is Destructor destructor)
            {
                AsignarLetrasDestructor(destructor);
            }
            else if (barco is Portaaviones portaaviones)
            {
                AsignarLetrasPortaavion(portaaviones);
            }
        }
    }

    public char ObtenerValorCasilla(int coordenadaX, int coordenadaY) => _casillas[coordenadaX, coordenadaY];

    public void MarcaCoordenadaAcertadaEnPlataforma(int coordenadaX, int coordenadaY) =>
        _casillas[coordenadaX, coordenadaY] = ConstantesTablero.LetraTableroBarcoAcertado;
    
    public void MarcaDisparoAlMarEnPlataforma(int coordenadaX, int coordenadaY) =>
        _casillas[coordenadaX, coordenadaY] = ConstantesTablero.LetraTableroDisparoAlMar;
    
    public void MarcaCanoneroHundidoEnPlataforma(int coordenadaX, int coordenadaY) =>
        _casillas[coordenadaX, coordenadaY] = ConstantesTablero.LetraTableroBarcoHundido;
    
    private void AsignarLetrasPortaavion(Portaaviones portaaviones)
    {
        foreach (var coordenadas in portaaviones.Coordenadas)
        {
            _casillas[coordenadas.x, coordenadas.y] = ConstantesTablero.LetraTableroPortaaviones;
        }
    }

    private void AsignarLetrasDestructor(Destructor destructor)
    {
        foreach (var coordenadas in destructor.Coordenadas)
        {
            _casillas[coordenadas.x, coordenadas.y] = ConstantesTablero.LetraTableroDestructor;
        }
    }

    private void AsignarLetraCanonero(Canonero canonero) => _casillas[canonero.CoordenadaX, canonero.CoordenadaY] = ConstantesTablero.LetraTableroCanonero;

    public void MarcaDestructorHundidoEnPlataforma(Destructor destructor)
    {
        foreach (var destructorCoordenada in destructor.Coordenadas)
        {
            _casillas[destructorCoordenada.x, destructorCoordenada.y] = ConstantesTablero.LetraTableroBarcoHundido;
        }
    }
    
    public void MarcaPortaAvionHundidoEnPlataforma(Portaaviones portaAvion)
    {
        foreach (var destructorCoordenada in portaAvion.Coordenadas)
        {
            _casillas[destructorCoordenada.x, destructorCoordenada.y] = ConstantesTablero.LetraTableroBarcoHundido;
        }
    }
}
using Acorazados.Dominio.Barcos;
using Acorazados.Dominio.Enums;

namespace Acorazados.Dominio;

public class Juego
{
    private const int LimiteSuperiorPlataforma = 9;
    private const int LimiteInferiorPlataforma = 0;
    private const int CantidadMaximaPorJugadorCanonero = 4;
    private const int CantidadMaximaPorJugadorDestructor = 2;
    private const int CantidadMaximaPorJugadorPortaaviones = 1;
    private const string MensajeBarcoFueraDelLimiteDeLaPlataforma = "Barco fuera del limite de la plataforma";
    private const string MensajeCantidadBarcosCanoneros1 = "Deben existir 4 cañoreros en la plataforma";
    private const string MensajeCantidadBarcosDestructores = "Deben existir 2 destructores en la plataforma";
    private const string MensajeCantidadBarcosPortaaviones = "Deben existir 1 portaavion en la plataforma";
    private const string MensajeBarcoHundido = "Barco hundido";
    private const char LetraTableroCanonero = 'g';
    private const char LetraTableroDestructor = 'd';
    private const char LetraTableroPortaaviones = 'c';
    private char[,] _tableroTurnoDisparar;
    private char[,] _tableroPrimerJugador;
    private char[,] _tablero;
    private string _nombreJugadorActual;
    private string _nombreJugadorUno;
    private string _nombreJugadorDos;
    private List<Barco> _tripulacionJugadorUno;
    private List<Barco> _tripulacionJugadorDos;


    public Juego()
    {
        _tablero = new char[10, 10];
    }

    public void AgregarJugador(TipoJugador tipoJugador, string nombre)
    {
        if (tipoJugador == TipoJugador.Uno)
        {
            _nombreJugadorUno = nombre;
            _tableroPrimerJugador = new char[10, 10];
        }

        if (tipoJugador == TipoJugador.Dos)
            _nombreJugadorDos = nombre;
    }

    public void Iniciar(List<Barco> tripulacionJugadorUno, List<Barco> tripulacionJugadorDos)
    {
        _tripulacionJugadorUno = tripulacionJugadorUno;
        _tripulacionJugadorDos = tripulacionJugadorDos;

        ValidacionesTripulacionJugadores();
        AsignarTripulacionTablero();

        _tableroTurnoDisparar = _tablero;
        _nombreJugadorActual = _nombreJugadorDos;
    }

    public string Disparar(int coordenadaX, int coordenadaY)
    {
        Barco barco = null;

        if (_nombreJugadorActual.Equals(_nombreJugadorDos))
        {
            barco = _tripulacionJugadorDos.FirstOrDefault(barco =>
                barco.SeEncuentraEnCoordenada(coordenadaX, coordenadaY));
        }
        else if (_nombreJugadorActual.Equals(_nombreJugadorUno))
        {
            barco = _tripulacionJugadorUno.FirstOrDefault(barco =>
                barco.SeEncuentraEnCoordenada(coordenadaX, coordenadaY));
        }

        if (barco != null)
        {
            if (barco is Canonero)
                MarcaCanoneroHundidoEnPlataforma(coordenadaX, coordenadaY);

            if (barco is Destructor or Portaaviones)
                MarcaCoordenadaAcertadaEnPlataforma(coordenadaX, coordenadaY);

            barco.RegistrarDisparo();

            if (barco.EstaHundido())
            {
                if (barco is Destructor destructor) MarcaDestructorHundidoEnPlataforma(destructor);

                if (barco is Portaaviones portaAvion) MarcaPortaAvionHundidoEnPlataforma(portaAvion);

                return MensajeBarcoHundido;
            }
        }

        return "";
    }

    public string Imprimir()
    {
        var tablero = new System.Text.StringBuilder();
        tablero.AppendLine();
        tablero.Append(" |0|1|2|3|4|5|6|7|8|9|");
        tablero.AppendLine();

        for (int fila = 0; fila < 10; fila++)
        {
            tablero.Append(fila + "|");
            for (int columna = 0; columna < 10; columna++)
            {
                char valorCasilla = (_tableroTurnoDisparar[columna, fila] != '\0' ? _tableroTurnoDisparar[columna, fila] : ' ');
                tablero.Append(valorCasilla + "|");
            }

            tablero.AppendLine();
        }

        return tablero.ToString();
    }

    public void FinalizarTurno()
    {
        if (_nombreJugadorActual.Equals(_nombreJugadorUno))
        {
            _tableroTurnoDisparar = _tablero;
            _nombreJugadorActual = _nombreJugadorDos;
        }
        else
        {
            _tableroTurnoDisparar = _tableroPrimerJugador;
            _nombreJugadorActual = _nombreJugadorUno;
        }
    }

    private void AsignarTripulacionTablero()
    {
        foreach (var barco in _tripulacionJugadorUno)
        {
            if (barco is Canonero canonero)
            {
                _tableroPrimerJugador[canonero.CoordenadaX, canonero.CoordenadaY] = LetraTableroCanonero;
            }
            else if (barco is Destructor destructor)
            {
                foreach (var coordenadas in destructor.Coordenadas)
                {
                    _tableroPrimerJugador[coordenadas.x, coordenadas.y] = LetraTableroDestructor;
                }
            }
            else if (barco is Portaaviones portaaviones)
            {
                foreach (var coordenadas in portaaviones.Coordenadas)
                {
                    _tableroPrimerJugador[coordenadas.x, coordenadas.y] = LetraTableroPortaaviones;
                }
            }
        }
        
        foreach (var barco in _tripulacionJugadorDos)
        {
            if (barco is Canonero canonero)
            {
                _tablero[canonero.CoordenadaX, canonero.CoordenadaY] = LetraTableroCanonero;
            }
            else if (barco is Destructor destructor)
            {
                foreach (var coordenadas in destructor.Coordenadas)
                {
                    _tablero[coordenadas.x, coordenadas.y] = LetraTableroDestructor;
                }
            }
            else if (barco is Portaaviones portaaviones)
            {
                foreach (var coordenadas in portaaviones.Coordenadas)
                {
                    _tablero[coordenadas.x, coordenadas.y] = LetraTableroPortaaviones;
                }
            }
        }
    }

    private void ValidacionesTripulacionJugadores()
    {
        if (ValidarLimitesPlataforma(_tripulacionJugadorUno))
            throw new ArgumentException(MensajeBarcoFueraDelLimiteDeLaPlataforma);

        if (ValidarCantidadBarcosCanoneros(_tripulacionJugadorUno))
            throw new ArgumentException(MensajeCantidadBarcosCanoneros1);

        if (ValidarCantidadBarcosDestructores(_tripulacionJugadorUno))
            throw new ArgumentException(MensajeCantidadBarcosDestructores);

        if (ValidarCantidadBarcosPortaaviones(_tripulacionJugadorUno))
            throw new ArgumentException(MensajeCantidadBarcosPortaaviones);

        if (ValidarLimitesPlataforma(_tripulacionJugadorDos))
            throw new ArgumentException(MensajeBarcoFueraDelLimiteDeLaPlataforma);

        if (ValidarCantidadBarcosCanoneros(_tripulacionJugadorDos))
            throw new ArgumentException(MensajeCantidadBarcosCanoneros1);

        if (ValidarCantidadBarcosDestructores(_tripulacionJugadorDos))
            throw new ArgumentException(MensajeCantidadBarcosDestructores);

        if (ValidarCantidadBarcosPortaaviones(_tripulacionJugadorDos))
            throw new ArgumentException(MensajeCantidadBarcosPortaaviones);
    }

    private bool ValidarCantidadBarcosPortaaviones(List<Barco> barcos) =>
        barcos.Count(barco => barco.GetType() == typeof(Portaaviones)) != CantidadMaximaPorJugadorPortaaviones;

    private bool ValidarCantidadBarcosDestructores(List<Barco> barcos) =>
        barcos.Count(barco => barco.GetType() == typeof(Destructor)) != CantidadMaximaPorJugadorDestructor;

    private bool ValidarCantidadBarcosCanoneros(List<Barco> barcos) =>
        barcos.Count(barco => barco.GetType() == typeof(Canonero)) != CantidadMaximaPorJugadorCanonero;

    private bool ValidarLimitesPlataforma(List<Barco> barcos) =>
        barcos.Any(barco => barco.EstaFueraDeLimites(LimiteInferiorPlataforma, LimiteSuperiorPlataforma));

    private void MarcaCoordenadaAcertadaEnPlataforma(int coordenadaX, int coordenadaY) =>
        _tableroTurnoDisparar[coordenadaX, coordenadaY] = 'x';

    private void MarcaCanoneroHundidoEnPlataforma(int coordenadaX, int coordenadaY) =>
        _tableroTurnoDisparar[coordenadaX, coordenadaY] = 'X';

    private void MarcaPortaAvionHundidoEnPlataforma(Portaaviones portaAvion)
    {
        foreach (var destructorCoordenada in portaAvion.Coordenadas)
        {
            _tableroTurnoDisparar[destructorCoordenada.x, destructorCoordenada.y] = 'X';
        }
    }

    private void MarcaDestructorHundidoEnPlataforma(Destructor destructor)
    {
        foreach (var destructorCoordenada in destructor.Coordenadas)
        {
            _tableroTurnoDisparar[destructorCoordenada.x, destructorCoordenada.y] = 'X';
        }
    }
}
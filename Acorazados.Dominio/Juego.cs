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
    private Tablero _tableroTurnoDisparar;
    private Tablero _tableroPrimerJugador;
    private Tablero _tableroSegundoJugador;
    private string _nombreJugadorTurnoDisparar;
    private string _nombreJugadorUno;
    private string _nombreJugadorDos;
    private List<Barco> _tripulacionJugadorUno;
    private List<Barco> _tripulacionJugadorDos;
    private bool _JuegoTerminado;

    public void AgregarJugador(TipoJugador tipoJugador, string nombre)
    {
        if (tipoJugador == TipoJugador.Uno)
        {
            _nombreJugadorUno = nombre;
            _tableroPrimerJugador = new Tablero(new char[10, 10]);
        }

        if (tipoJugador == TipoJugador.Dos)
        {
            _nombreJugadorDos = nombre;
            _tableroSegundoJugador = new Tablero(new char[10, 10]);
        }
    }

    public void Iniciar(List<Barco> tripulacionJugadorUno, List<Barco> tripulacionJugadorDos)
    {
        _tripulacionJugadorUno = tripulacionJugadorUno;
        _tripulacionJugadorDos = tripulacionJugadorDos;

        ValidacionesTripulacionJugadores();
        AsignarTripulacionesTablero();

        _tableroTurnoDisparar = _tableroSegundoJugador;
        _nombreJugadorTurnoDisparar = _nombreJugadorDos;
    }

    public string Disparar(int coordenadaX, int coordenadaY)
    {
        string mensajeResultado = "";
        Barco barco = null;

        if (_nombreJugadorTurnoDisparar.Equals(_nombreJugadorDos))
        {
            barco = _tripulacionJugadorDos.FirstOrDefault(barco =>
                barco.SeEncuentraEnCoordenada(coordenadaX, coordenadaY));
        }
        else if (_nombreJugadorTurnoDisparar.Equals(_nombreJugadorUno))
        {
            barco = _tripulacionJugadorUno.FirstOrDefault(barco =>
                barco.SeEncuentraEnCoordenada(coordenadaX, coordenadaY));
        }

        if (barco != null)
        {
            if (barco is Canonero)
                _tableroTurnoDisparar.MarcaCanoneroHundidoEnPlataforma(coordenadaX, coordenadaY);

            if (barco is Destructor or Portaaviones)
                _tableroTurnoDisparar.MarcaCoordenadaAcertadaEnPlataforma(coordenadaX, coordenadaY);

            barco.RegistrarDisparo();

            if (barco.EstaHundido())
            {
                if (barco is Destructor destructor)
                    _tableroTurnoDisparar.MarcaDestructorHundidoEnPlataforma(destructor);

                if (barco is Portaaviones portaAvion)
                    _tableroTurnoDisparar.MarcaPortaAvionHundidoEnPlataforma(portaAvion);

                mensajeResultado = MensajeBarcoHundido;
            }
        }
        else
        {
            _tableroTurnoDisparar.MarcaDisparoAlMarEnPlataforma(coordenadaX, coordenadaY);
        }

        if (_nombreJugadorTurnoDisparar.Equals(_nombreJugadorDos))
        {
            _JuegoTerminado = _tripulacionJugadorDos.Where(barco => barco.EstaHundido()).Count() == 7;
        }

        return mensajeResultado;
    }


    public string Imprimir()
    {
        var tablero = new System.Text.StringBuilder();
        if (_JuegoTerminado)
        {
            string estadisticasJugador =
                @"
Total disparos: 14
Fallos: 0
Acertados: 14
Barcos Hundidos:
Cañonero: (0,0)
Cañonero: (0,1)
PortaAviones: (1, 3)
Cañonero: (5,1)
Cañonero: (5,2)
Destructor: (7,0)
Destroyer: (5,7)";
tablero.Append(estadisticasJugador);
tablero.AppendLine();
        }

        tablero.AppendLine();
        tablero.Append(" |0|1|2|3|4|5|6|7|8|9|");
        tablero.AppendLine();

        for (int fila = 0; fila < 10; fila++)
        {
            tablero.Append(fila + "|");
            for (int columna = 0; columna < 10; columna++)
            {
                char valorCasilla = (_tableroTurnoDisparar.ObtenerValorCasilla(columna, fila) != '\0'
                    ? _tableroTurnoDisparar.ObtenerValorCasilla(columna, fila)
                    : ' ');
                tablero.Append(valorCasilla + "|");
            }

            tablero.AppendLine();
        }

        return tablero.ToString();
    }

    public void FinalizarTurno()
    {
        _tableroTurnoDisparar = _nombreJugadorTurnoDisparar.Equals(_nombreJugadorUno)
            ? _tableroSegundoJugador
            : _tableroPrimerJugador;
        _nombreJugadorTurnoDisparar = _nombreJugadorTurnoDisparar.Equals(_nombreJugadorUno)
            ? _nombreJugadorDos
            : _nombreJugadorUno;
    }

    private void AsignarTripulacionesTablero()
    {
        _tableroPrimerJugador.AsignarTripulacion(_tripulacionJugadorUno);
        _tableroSegundoJugador.AsignarTripulacion(_tripulacionJugadorDos);
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
}
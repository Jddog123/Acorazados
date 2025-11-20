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
    private const int CantidadBarcosHundidosParaGanar = 7;
    private const string MensajeBarcoFueraDelLimiteDeLaPlataforma = "Barco fuera del limite de la plataforma";
    private const string MensajeCantidadBarcosCanoneros1 = "Deben existir 4 cañoreros en la plataforma";
    private const string MensajeCantidadBarcosDestructores = "Deben existir 2 destructores en la plataforma";
    private const string MensajeCantidadBarcosPortaaviones = "Deben existir 1 portaavion en la plataforma";
    private const string MensajeBarcoHundido = "Barco hundido";
    private List<Barco> _tripulacionJugadorADisparar;
    private List<Barco> _tripulacionJugadorUno;
    private List<Barco> _tripulacionJugadorDos;
    private Jugador _jugadorUno;
    private Jugador _jugadorDos;
    private Jugador _jugadorTurnoActual;
    private Jugador _jugadorTurnoADisparar;
    private bool _JuegoTerminado;

    public void AgregarJugador(TipoJugador tipoJugador, string nombre)
    {
        if (tipoJugador == TipoJugador.Uno)
        {
            _jugadorUno = new Jugador(nombre, new Tablero(new char[10, 10]));
        }

        if (tipoJugador == TipoJugador.Dos)
        {
            _jugadorDos = new Jugador(nombre, new Tablero(new char[10, 10]));
        }
    }

    public void Iniciar(List<Barco> tripulacionJugadorUno, List<Barco> tripulacionJugadorDos)
    {
        _tripulacionJugadorUno = tripulacionJugadorUno;
        _tripulacionJugadorDos = tripulacionJugadorDos;

        ValidacionesTripulacionJugadores();
        AsignarTripulacionesTablero();

        _jugadorTurnoActual = _jugadorUno;
        _jugadorTurnoADisparar = _jugadorDos;
        _tripulacionJugadorADisparar = tripulacionJugadorDos;
    }

    public string Disparar(int coordenadaX, int coordenadaY)
    {
        string mensajeResultado = "";
        
        _jugadorTurnoActual.SumarDisparo();
        
        var barco = _tripulacionJugadorADisparar.FirstOrDefault(barco =>
            barco.SeEncuentraEnCoordenada(coordenadaX, coordenadaY));

        if (barco != null)
        {
            _jugadorTurnoActual.SumarAciertos();
                
            if (barco is Canonero)
                _jugadorTurnoADisparar._tablero.MarcaCanoneroHundidoEnPlataforma(coordenadaX, coordenadaY);

            if (barco is Destructor or Portaaviones)
                _jugadorTurnoADisparar._tablero.MarcaCoordenadaAcertadaEnPlataforma(coordenadaX, coordenadaY);

            barco.RegistrarDisparo();

            if (barco.EstaHundido())
            {
                _jugadorTurnoActual.AgregarBarcoHundido(barco);
                
                if (barco is Destructor destructor)
                    _jugadorTurnoADisparar._tablero.MarcaDestructorHundidoEnPlataforma(destructor);

                if (barco is Portaaviones portaAvion)
                    _jugadorTurnoADisparar._tablero.MarcaPortaAvionHundidoEnPlataforma(portaAvion);

                mensajeResultado = MensajeBarcoHundido;
            }
        }
        else
        {
            _jugadorTurnoActual.SumarFallos();
            _jugadorTurnoADisparar._tablero.MarcaDisparoAlMarEnPlataforma(coordenadaX, coordenadaY);
        }
        
        TerminarJuego();

        return mensajeResultado;
    }


    public string Imprimir()
    {
        var tablero = new System.Text.StringBuilder();
        
        if (_JuegoTerminado)
        {
            var estadisticasJugador = _jugadorTurnoActual.ObtenerEstadisticasJugador();
            tablero.AppendLine();
            tablero.Append("Jugador "+_jugadorTurnoActual._nombre);
            tablero.AppendLine();
            tablero.Append("Total disparos: " + estadisticasJugador.totalDisparos);
            tablero.AppendLine();
            tablero.Append("Fallos: " + estadisticasJugador.totalFallos);
            tablero.AppendLine();
            tablero.Append("Acertados: " + estadisticasJugador.totalAciertos);
            tablero.AppendLine();
            tablero.Append("Barcos Hundidos:");
            tablero.AppendLine();

            foreach (var barcoHundido in estadisticasJugador.barcosHundidos)
            {
                var coordenadasMinimas = barcoHundido.ObtenerCoordenadaMinima();
                tablero.Append(barcoHundido.ObtenerDescripcion() + ": (" + coordenadasMinimas.x + "," + coordenadasMinimas.y + ")");
                tablero.AppendLine();
            }
        }

        tablero.AppendLine();
        tablero.Append(" |0|1|2|3|4|5|6|7|8|9|");
        tablero.AppendLine();

        for (int fila = 0; fila < 10; fila++)
        {
            tablero.Append(fila + "|");
            for (int columna = 0; columna < 10; columna++)
            {
                char valorCasilla = (_jugadorTurnoADisparar._tablero.ObtenerValorCasilla(columna, fila) != '\0'
                    ? _jugadorTurnoADisparar._tablero.ObtenerValorCasilla(columna, fila)
                    : ' ');
                tablero.Append(valorCasilla + "|");
            }

            tablero.AppendLine();
        }

        return tablero.ToString();
    }

    public void FinalizarTurno()
    {
        if (_jugadorTurnoActual._nombre.Equals(_jugadorUno._nombre))
        {
            _jugadorTurnoActual = _jugadorDos;
            _jugadorTurnoADisparar = _jugadorUno;
            _tripulacionJugadorADisparar = _tripulacionJugadorUno;
        }
        else
        {
            _jugadorTurnoActual = _jugadorUno;
            _jugadorTurnoADisparar = _jugadorDos;
            _tripulacionJugadorADisparar = _tripulacionJugadorDos;
        }
    }
    
    private void TerminarJuego()
    {
        _JuegoTerminado = _tripulacionJugadorUno.Where(barco => barco.EstaHundido()).Count() ==
                          CantidadBarcosHundidosParaGanar ||
                          _tripulacionJugadorDos.Where(barco => barco.EstaHundido()).Count() ==
                          CantidadBarcosHundidosParaGanar;

    }

    private void AsignarTripulacionesTablero()
    {
        _jugadorUno._tablero.AsignarTripulacion(_tripulacionJugadorUno);
        _jugadorDos._tablero.AsignarTripulacion(_tripulacionJugadorDos);
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
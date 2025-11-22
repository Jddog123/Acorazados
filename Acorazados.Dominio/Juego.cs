using System.Text;
using Acorazados.Dominio.Barcos;
using Acorazados.Dominio.Constantes;
using Acorazados.Dominio.Enums;
using Acorazados.Dominio.Records;

namespace Acorazados.Dominio;

public class Juego
{
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
        if (tipoJugador == TipoJugador.Uno) InstanciarJugadorUno(nombre);

        if (tipoJugador == TipoJugador.Dos) InstanciarJugadorDos(nombre);
    }

    public void Iniciar(List<Barco> tripulacionJugadorUno, List<Barco> tripulacionJugadorDos)
    {
        if (_jugadorUno == null)
            throw new ArgumentException(ConstantesJuego.MensajeJugadorUnoRequerido);

        if (_jugadorDos == null)
            throw new ArgumentException(ConstantesJuego.MensajeJugadorDosRequerido);

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

        if (_JuegoTerminado)
            throw new Exception(ConstantesJuego.MensajeJuegoTerminado);

        if (DisparoEstaFueraDelMapa(coordenadaX, coordenadaY))
            throw new ArgumentException(ConstantesJuego.MensajeDisparoFueraDelTablero);

        _jugadorTurnoActual.SumarDisparoAEstadistica();

        var barco = ObtenerBarcoPor(coordenadaX, coordenadaY);
        if (barco != null)
        {
            mensajeResultado = ConstantesJuego.MensajeTiroAcertado;

            _jugadorTurnoActual.SumarAciertosAEstadistica();

            if (barco is Canonero)
                HundirCañoneroEnTablero(coordenadaX, coordenadaY);

            if (barco is Destructor or Portaaviones)
                AcertarDisparoDestructorOPortaavionesEnTablero(coordenadaX, coordenadaY);

            barco.RegistrarDañoRecibido();

            if (barco.EstaHundido())
            {
                _jugadorTurnoActual.AgregarBarcoHundidoAEstadistica(barco);

                if (barco is Destructor destructor)
                    HundirDestructorEnTablero(destructor);

                if (barco is Portaaviones portaavion)
                    HundirPortaavionEnTablero(portaavion);

                mensajeResultado = ConstantesJuego.MensajeBarcoHundido;
            }
        }
        else
        {
            _jugadorTurnoActual.SumarFallosAEstadistica();
            DispararAlMarEnElTablero(coordenadaX, coordenadaY);
        }

        JuegoEstaTerminado();

        return mensajeResultado;
    }

    public string Imprimir()
    {
        var informe = new System.Text.StringBuilder();

        if (_JuegoTerminado)
        {
            AgregarEstadisticaJugadorAInforme(informe, true);
            AgregarTableroAInforme(informe);
            informe.AppendLine();
            FinalizarTurno();
            AgregarEstadisticaJugadorAInforme(informe, false);
            AgregarTableroAInforme(informe);
        }
        else
        {
            AgregarTableroAInforme(informe);
        }

        return informe.ToString();
    }

    public void FinalizarTurno()
    {
        if (JugadorTurnoActualEsJugadorUno())
        {
            AsignarTurnoActualJugadorDos();
        }
        else
        {
            AsignarTurnoActualJugadorUno();
        }
    }


    private void AsignarTurnoActualJugadorUno()
    {
        _jugadorTurnoActual = _jugadorUno;
        _jugadorTurnoADisparar = _jugadorDos;
        _tripulacionJugadorADisparar = _tripulacionJugadorDos;
    }

    private void AsignarTurnoActualJugadorDos()
    {
        _jugadorTurnoActual = _jugadorDos;
        _jugadorTurnoADisparar = _jugadorUno;
        _tripulacionJugadorADisparar = _tripulacionJugadorUno;
    }

    private bool JugadorTurnoActualEsJugadorUno() => _jugadorTurnoActual._nombre.Equals(_jugadorUno._nombre);

    private void AsignarTripulacionesTablero()
    {
        _jugadorUno._tablero.AsignarTripulacion(_tripulacionJugadorUno);
        _jugadorDos._tablero.AsignarTripulacion(_tripulacionJugadorDos);
    }

    private static bool DisparoEstaFueraDelMapa(int coordenadaX, int coordenadaY) =>
        coordenadaX < ConstantesJuego.LimiteInferiorPlataforma ||
        coordenadaX > ConstantesJuego.LimiteSuperiorPlataforma ||
        coordenadaY < ConstantesJuego.LimiteInferiorPlataforma ||
        coordenadaY > ConstantesJuego.LimiteSuperiorPlataforma;

    private void HundirCañoneroEnTablero(int coordenadaX, int coordenadaY) =>
        _jugadorTurnoADisparar._tablero.MarcaCanoneroHundidoEnPlataforma(coordenadaX, coordenadaY);

    private void AcertarDisparoDestructorOPortaavionesEnTablero(int coordenadaX, int coordenadaY) =>
        _jugadorTurnoADisparar._tablero.MarcaCoordenadaAcertadaEnPlataforma(coordenadaX, coordenadaY);

    private static void AgregarSaltoDeLinea(StringBuilder informe) => informe.AppendLine();

    private void HundirPortaavionEnTablero(Portaaviones barco) =>
        _jugadorTurnoADisparar._tablero.MarcaPortaAvionHundidoEnPlataforma(barco);

    private void HundirDestructorEnTablero(Destructor barco) =>
        _jugadorTurnoADisparar._tablero.MarcaDestructorHundidoEnPlataforma(barco);

    private void DispararAlMarEnElTablero(int coordenadaX, int coordenadaY) =>
        _jugadorTurnoADisparar._tablero.MarcaDisparoAlMarEnPlataforma(coordenadaX, coordenadaY);

    private void InstanciarJugadorDos(string nombre) => _jugadorDos = new Jugador(nombre,
        new Tablero(new char[ConstantesJuego.LongitudPlataforma, ConstantesJuego.LongitudPlataforma]));

    private void InstanciarJugadorUno(string nombre) => _jugadorUno = new Jugador(nombre,
        new Tablero(new char[ConstantesJuego.LongitudPlataforma, ConstantesJuego.LongitudPlataforma]));

    private void ValidacionesTripulacionJugadores()
    {
        if (ValidarLimitesPlataforma(_tripulacionJugadorUno))
            throw new ArgumentException(ConstantesJuego.MensajeBarcoFueraDelLimiteDeLaPlataforma);

        if (ValidarCantidadBarcosCanoneros(_tripulacionJugadorUno))
            throw new ArgumentException(ConstantesJuego.MensajeCantidadBarcosCanoneros1);

        if (ValidarCantidadBarcosDestructores(_tripulacionJugadorUno))
            throw new ArgumentException(ConstantesJuego.MensajeCantidadBarcosDestructores);

        if (ValidarCantidadBarcosPortaaviones(_tripulacionJugadorUno))
            throw new ArgumentException(ConstantesJuego.MensajeCantidadBarcosPortaaviones);

        if (ValidarBarcosEnMismaPosicion(_tripulacionJugadorUno))
            throw new ArgumentException(ConstantesJuego.MensajeBarcosEnMismaPosicion);

        if (ValidarLimitesPlataforma(_tripulacionJugadorDos))
            throw new ArgumentException(ConstantesJuego.MensajeBarcoFueraDelLimiteDeLaPlataforma);

        if (ValidarCantidadBarcosCanoneros(_tripulacionJugadorDos))
            throw new ArgumentException(ConstantesJuego.MensajeCantidadBarcosCanoneros1);

        if (ValidarCantidadBarcosDestructores(_tripulacionJugadorDos))
            throw new ArgumentException(ConstantesJuego.MensajeCantidadBarcosDestructores);

        if (ValidarCantidadBarcosPortaaviones(_tripulacionJugadorDos))
            throw new ArgumentException(ConstantesJuego.MensajeCantidadBarcosPortaaviones);

        if (ValidarBarcosEnMismaPosicion(_tripulacionJugadorDos))
            throw new ArgumentException(ConstantesJuego.MensajeBarcosEnMismaPosicion);

        if (ValidarBarcoEnDiagonal(_tripulacionJugadorUno) || ValidarBarcoEnDiagonal(_tripulacionJugadorDos))
            throw new ArgumentException(ConstantesJuego.MensajeBarcosEnDiagonal);
    }

    private bool ValidarCantidadBarcosPortaaviones(List<Barco> barcos) =>
        barcos.Count(barco => barco.GetType() == typeof(Portaaviones)) !=
        ConstantesJuego.CantidadMaximaPorJugadorPortaaviones;

    private bool ValidarCantidadBarcosDestructores(List<Barco> barcos) =>
        barcos.Count(barco => barco.GetType() == typeof(Destructor)) !=
        ConstantesJuego.CantidadMaximaPorJugadorDestructor;

    private bool ValidarCantidadBarcosCanoneros(List<Barco> barcos) =>
        barcos.Count(barco => barco.GetType() == typeof(Canonero)) != ConstantesJuego.CantidadMaximaPorJugadorCanonero;

    private bool ValidarLimitesPlataforma(List<Barco> barcos) =>
        barcos.Any(barco =>
            barco.EstaFueraDeLimites(ConstantesJuego.LimiteInferiorPlataforma,
                ConstantesJuego.LimiteSuperiorPlataforma));

    private bool ValidarBarcosEnMismaPosicion(List<Barco> barcos)
    {
        var todasCoordenadas = new List<(int x, int y)>();
        ObtenerCoordenadasTripulacion(barcos, todasCoordenadas);

        return todasCoordenadas.GroupBy(coordenada => (coordenada.x, coordenada.y)).Any(group => group.Count() > 1);
    }

    private void JuegoEstaTerminado()
    {
        _JuegoTerminado = _tripulacionJugadorUno.Where(barco => barco.EstaHundido()).Count() ==
                          ConstantesJuego.CantidadBarcosHundidosParaGanar ||
                          _tripulacionJugadorDos.Where(barco => barco.EstaHundido()).Count() ==
                          ConstantesJuego.CantidadBarcosHundidosParaGanar;
    }

    private void AgregarTableroAInforme(StringBuilder informe)
    {
        AgregarSaltoDeLinea(informe);
        informe.Append(" |0|1|2|3|4|5|6|7|8|9|");
        AgregarSaltoDeLinea(informe);

        for (int fila = 0; fila < 10; fila++)
        {
            informe.Append(fila + "|");
            for (int columna = 0; columna < 10; columna++)
            {
                char valorCasilla = ObtenerValorCasilla(columna, fila);
                informe.Append(valorCasilla + "|");
            }

            AgregarSaltoDeLinea(informe);
        }
    }
    
    private void AgregarEstadisticaJugadorAInforme(StringBuilder informe, bool ganador)
    {
        var estadisticasJugador = _jugadorTurnoActual.ObtenerEstadisticasJugador();
        informe.Append(ganador ? "JUGADOR GANADOR" : "JUGADOR PERDEDOR");
        AgregarSaltoDeLinea(informe);
        informe.Append("Jugador " + _jugadorTurnoActual._nombre);
        AgregarSaltoDeLinea(informe);
        informe.Append("Total disparos: " + estadisticasJugador.totalDisparos);
        AgregarSaltoDeLinea(informe);
        informe.Append("Fallos: " + estadisticasJugador.totalFallos);
        AgregarSaltoDeLinea(informe);
        informe.Append("Acertados: " + estadisticasJugador.totalAciertos);
        AgregarSaltoDeLinea(informe);
        informe.Append("Barcos Hundidos:");
        AgregarSaltoDeLinea(informe);

        foreach (var barcoHundido in estadisticasJugador.barcosHundidos)
        {
            var coordenadasMinimas = barcoHundido.ObtenerCoordenadaMinima();
            informe.Append(barcoHundido.ObtenerDescripcion() + ": (" + coordenadasMinimas.x + "," +
                           coordenadasMinimas.y + ")");
            informe.AppendLine();
        }
    }

    private Barco? ObtenerBarcoPor(int coordenadaX, int coordenadaY)
    {
        var barco = _tripulacionJugadorADisparar.FirstOrDefault(barco =>
            barco.SeEncuentraEnCoordenada(coordenadaX, coordenadaY));
        return barco;
    }

    private char ObtenerValorCasilla(int columna, int fila) =>
        (_jugadorTurnoADisparar._tablero.ObtenerValorCasilla(columna, fila) != '\0'
            ? _jugadorTurnoADisparar._tablero.ObtenerValorCasilla(columna, fila)
            : ' ');

    private bool ValidarBarcoEnDiagonal(List<Barco> barcos)
    {
        bool barcoEnDiagonal = false;
        foreach (var barco in barcos.Where(b => b is Destructor or Portaaviones))
        {
            var todasCoordenadas = new List<(int x, int y)>();
            ObtenerCoordenadasTripulacion([barco], todasCoordenadas);

            var mismaFila = todasCoordenadas.All(coordenada => coordenada.x == todasCoordenadas[0].x);
            var mismaColumna = todasCoordenadas.All(coordenada => coordenada.y == todasCoordenadas[0].y);

            if (!mismaFila && !mismaColumna)
                barcoEnDiagonal = true;
        }

        return barcoEnDiagonal;
    }

    private static void ObtenerCoordenadasTripulacion(List<Barco> barcos, List<(int x, int y)> todasCoordenadas)
    {
        foreach (var barco in barcos)
        {
            var coordenadas = barco switch
            {
                Destructor destructor => destructor.Coordenadas.ToList(),
                Portaaviones portaavion => portaavion.Coordenadas.ToList(),
                Canonero canonero => new List<(int x, int y)> { (canonero.CoordenadaX, canonero.CoordenadaY) }
            };

            todasCoordenadas.AddRange(coordenadas);
        }
    }
}
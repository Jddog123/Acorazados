using System.Text;
using Acorazados.Dominio.Barcos;
using Acorazados.Dominio.Enums;
using Acorazados.Dominio.Records;

namespace Acorazados.Dominio;

public class Juego
{
    private const int LongitudPlataforma = 10;
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
    private const string MensajeTiroAcertado = "Tiro acertado";
    private const string MensajeBarcosEnMismaPosicion = "Ya Existe un Barco en la misma posición";

    private const string MensajeBarcosEnDiagonal =
        "Solo se pueden colocar barcos en posiciones horizontales o verticales";

    private const string MensajeJuegoTerminado = "El juego ya termino";
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
            _jugadorUno = new Jugador(nombre, new Tablero(new char[LongitudPlataforma, LongitudPlataforma]));
        }

        if (tipoJugador == TipoJugador.Dos)
        {
            _jugadorDos = new Jugador(nombre, new Tablero(new char[LongitudPlataforma, LongitudPlataforma]));
        }
    }

    public void Iniciar(List<Barco> tripulacionJugadorUno, List<Barco> tripulacionJugadorDos)
    {
        if (_jugadorDos == null)
            throw new ArgumentException("Debe agregar al jugador dos");
        
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
            throw new Exception(MensajeJuegoTerminado);

        _jugadorTurnoActual.SumarDisparoAEstadistica();

        var barco = ObtenerBarcoPor(coordenadaX, coordenadaY);
        if (barco != null)
        {
            mensajeResultado = MensajeTiroAcertado;

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

                mensajeResultado = MensajeBarcoHundido;
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
            AgregarEstadisticaJugadorAInforme(informe);
        }

        AgregarTableroAInforme(informe);

        return informe.ToString();
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

    private void JuegoEstaTerminado()
    {
        _JuegoTerminado = _tripulacionJugadorUno.Where(barco => barco.EstaHundido()).Count() ==
                          CantidadBarcosHundidosParaGanar ||
                          _tripulacionJugadorDos.Where(barco => barco.EstaHundido()).Count() ==
                          CantidadBarcosHundidosParaGanar;
    }

    private void AgregarTableroAInforme(StringBuilder informe)
    {
        informe.AppendLine();
        informe.Append(" |0|1|2|3|4|5|6|7|8|9|");
        informe.AppendLine();

        for (int fila = 0; fila < 10; fila++)
        {
            informe.Append(fila + "|");
            for (int columna = 0; columna < 10; columna++)
            {
                char valorCasilla = (_jugadorTurnoADisparar._tablero.ObtenerValorCasilla(columna, fila) != '\0'
                    ? _jugadorTurnoADisparar._tablero.ObtenerValorCasilla(columna, fila)
                    : ' ');
                informe.Append(valorCasilla + "|");
            }

            informe.AppendLine();
        }
    }

    private void AgregarEstadisticaJugadorAInforme(StringBuilder informe)
    {
        var estadisticasJugador = _jugadorTurnoActual.ObtenerEstadisticasJugador();
        informe.AppendLine();
        informe.Append("Jugador " + _jugadorTurnoActual._nombre);
        informe.AppendLine();
        informe.Append("Total disparos: " + estadisticasJugador.totalDisparos);
        informe.AppendLine();
        informe.Append("Fallos: " + estadisticasJugador.totalFallos);
        informe.AppendLine();
        informe.Append("Acertados: " + estadisticasJugador.totalAciertos);
        informe.AppendLine();
        informe.Append("Barcos Hundidos:");
        informe.AppendLine();

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

    private void AsignarTripulacionesTablero()
    {
        _jugadorUno._tablero.AsignarTripulacion(_tripulacionJugadorUno);
        _jugadorDos._tablero.AsignarTripulacion(_tripulacionJugadorDos);
    }

    private void HundirCañoneroEnTablero(int coordenadaX, int coordenadaY) =>
        _jugadorTurnoADisparar._tablero.MarcaCanoneroHundidoEnPlataforma(coordenadaX, coordenadaY);

    private void AcertarDisparoDestructorOPortaavionesEnTablero(int coordenadaX, int coordenadaY) =>
        _jugadorTurnoADisparar._tablero.MarcaCoordenadaAcertadaEnPlataforma(coordenadaX, coordenadaY);

    private void HundirPortaavionEnTablero(Portaaviones barco) =>
        _jugadorTurnoADisparar._tablero.MarcaPortaAvionHundidoEnPlataforma(barco);

    private void HundirDestructorEnTablero(Destructor barco) =>
        _jugadorTurnoADisparar._tablero.MarcaDestructorHundidoEnPlataforma(barco);

    private void DispararAlMarEnElTablero(int coordenadaX, int coordenadaY) =>
        _jugadorTurnoADisparar._tablero.MarcaDisparoAlMarEnPlataforma(coordenadaX, coordenadaY);

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

        if (ValidarBarcosEnMismaPosicion(_tripulacionJugadorUno))
            throw new ArgumentException(MensajeBarcosEnMismaPosicion);

        if (ValidarLimitesPlataforma(_tripulacionJugadorDos))
            throw new ArgumentException(MensajeBarcoFueraDelLimiteDeLaPlataforma);

        if (ValidarCantidadBarcosCanoneros(_tripulacionJugadorDos))
            throw new ArgumentException(MensajeCantidadBarcosCanoneros1);

        if (ValidarCantidadBarcosDestructores(_tripulacionJugadorDos))
            throw new ArgumentException(MensajeCantidadBarcosDestructores);

        if (ValidarCantidadBarcosPortaaviones(_tripulacionJugadorDos))
            throw new ArgumentException(MensajeCantidadBarcosPortaaviones);

        if (ValidarBarcosEnMismaPosicion(_tripulacionJugadorDos))
            throw new ArgumentException(MensajeBarcosEnMismaPosicion);

        if (ValidarBarcoEnDiagonal(_tripulacionJugadorUno) || ValidarBarcoEnDiagonal(_tripulacionJugadorDos))
            throw new ArgumentException(MensajeBarcosEnDiagonal);
    }

    private bool ValidarCantidadBarcosPortaaviones(List<Barco> barcos) =>
        barcos.Count(barco => barco.GetType() == typeof(Portaaviones)) != CantidadMaximaPorJugadorPortaaviones;

    private bool ValidarCantidadBarcosDestructores(List<Barco> barcos) =>
        barcos.Count(barco => barco.GetType() == typeof(Destructor)) != CantidadMaximaPorJugadorDestructor;

    private bool ValidarCantidadBarcosCanoneros(List<Barco> barcos) =>
        barcos.Count(barco => barco.GetType() == typeof(Canonero)) != CantidadMaximaPorJugadorCanonero;

    private bool ValidarLimitesPlataforma(List<Barco> barcos) =>
        barcos.Any(barco => barco.EstaFueraDeLimites(LimiteInferiorPlataforma, LimiteSuperiorPlataforma));

    private bool ValidarBarcosEnMismaPosicion(List<Barco> barcos)
    {
        var todasCoordenadas = new List<(int x, int y)>();
        ObtenerCoordenadasTripulacion(barcos, todasCoordenadas);
        
        return todasCoordenadas.GroupBy(coordenada => (coordenada.x, coordenada.y)).Any(group => group.Count() > 1);
    }

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
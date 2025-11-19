using Acorazados.Enums;

namespace Acorazados;

public class Juego
{
    private const int LimiteSuperiorPlataforma = 9;
    private const int LimiteInferiorPlataforma = 0;
    private const string MensajeBarcoFueraDelLimiteDeLaPlataforma = "Barco fuera del limite de la plataforma";
    private const string MensajeLogitudBarcoCanonero = "El tipo de barco cañonero es de una coordenada";
    private const string MensajeLogitudBarcoDestructor = "El tipo de barco destructor es de tres coordenadas";
    private const string MensajeLogitudBarcoPortaaviones = "El tipo de barco portaaviones es de cuatro coordenadas";
    private const string MensajeCantidadBarcosCanoneros1 = "Deben existir 4 cañoreros en la plataforma";
    private const string MensajeCantidadBarcosDestructores = "Deben existir 2 destructores en la plataforma";
    private const string MensajeCantidadBarcosPortaaviones = "Deben existir 1 portaavion en la plataforma";
    private char[,] _plataforma;
    private List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> _tripulacionJugadorUno;
    private List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> _tripulacionJugadorDos;

    public Juego()
    {
        _plataforma = new char[10, 10];
    }

    public void AgregarJugadorUno(List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> listaBarcosJugadorUno)
    {
        _tripulacionJugadorUno = listaBarcosJugadorUno;
    }

    public void AgregarJugadorDos(List<(List<(int x, int y)> coordenadas, TipoBarco)> listaBarcosJugadorDos)
    {
        _tripulacionJugadorDos = listaBarcosJugadorDos;
    }

    public void Iniciar()
    {
        ValidacionesTripulacionJugadorUno();
        ValidacionTripulacionJugadorDos();

        foreach (var barcoCanonero in _tripulacionJugadorDos)
        {
            if (barcoCanonero.tipoBarco == TipoBarco.Canonero)
            {
                _plataforma[barcoCanonero.coordenadas[0].x, barcoCanonero.coordenadas[0].y] = 'g';
            }
        }
        
        if (_tripulacionJugadorDos[4].tipoBarco == TipoBarco.Destructor)
        {
            _plataforma[_tripulacionJugadorDos[4].coordenadas[0].x, _tripulacionJugadorDos[4].coordenadas[0].y] = 'd';
            _plataforma[_tripulacionJugadorDos[4].coordenadas[1].x, _tripulacionJugadorDos[4].coordenadas[1].y] = 'd';
            _plataforma[_tripulacionJugadorDos[4].coordenadas[2].x, _tripulacionJugadorDos[4].coordenadas[2].y] = 'd';
        }
        
        if (_tripulacionJugadorDos[5].tipoBarco == TipoBarco.Destructor)
        {
            _plataforma[_tripulacionJugadorDos[5].coordenadas[0].x, _tripulacionJugadorDos[5].coordenadas[0].y] = 'd';
            _plataforma[_tripulacionJugadorDos[5].coordenadas[1].x, _tripulacionJugadorDos[5].coordenadas[1].y] = 'd';
            _plataforma[_tripulacionJugadorDos[5].coordenadas[2].x, _tripulacionJugadorDos[5].coordenadas[2].y] = 'd';
        }
    }
    
    public string Disparar(int coordenadaX, int coordenadaY)
    {
        if (_plataforma[coordenadaX, coordenadaY] == 'g')
        {
            _plataforma[coordenadaX, coordenadaY] = 'X';
            return "Barco hundido";
        }
        
        if (_plataforma[coordenadaX, coordenadaY] == 'd')
        {
            _plataforma[coordenadaX, coordenadaY] = 'x';

            if (_plataforma[9, 0] == 'x' && _plataforma[8, 0] == 'x' && _plataforma[7, 0] == 'x')
            {
                _plataforma[9, 0] = 'X';
                _plataforma[8, 9] = 'X';
                _plataforma[7, 0] = 'X';
                return "Barco hundido";
            }
            if (_plataforma[5, 7] == 'x' && _plataforma[4, 7] == 'x' && _plataforma[3, 7] == 'x')
            {
                _plataforma[3, 7] = 'X';
                _plataforma[4, 7] = 'X';
                _plataforma[5, 7] = 'X';
                return "Barco hundido";
            }
        }
        
        return "";
    }

    private void ValidacionTripulacionJugadorDos()
    {
        if (ValidarLimitesPlataforma(_tripulacionJugadorDos))
            throw new ArgumentException(MensajeBarcoFueraDelLimiteDeLaPlataforma);

        if (ValidarLongitudBarcoCanonero(_tripulacionJugadorDos))
            throw new ArgumentException(MensajeLogitudBarcoCanonero);

        if (ValidarLongitudBarcoDestructor(_tripulacionJugadorDos))
            throw new ArgumentException(MensajeLogitudBarcoDestructor);

        if (ValidarLongitudBarcoPortaAviones(_tripulacionJugadorDos))
            throw new ArgumentException(MensajeLogitudBarcoPortaaviones);

        if (ValidarCantidadBarcosCanoneros(_tripulacionJugadorDos))
            throw new ArgumentException(MensajeCantidadBarcosCanoneros1);

        if (ValidarCantidadBarcosDestructores(_tripulacionJugadorDos))
            throw new ArgumentException(MensajeCantidadBarcosDestructores);

        if (ValidarCantidadBarcosPortaaviones(_tripulacionJugadorDos))
            throw new ArgumentException(MensajeCantidadBarcosPortaaviones);
    }

    private void ValidacionesTripulacionJugadorUno()
    {
        if (ValidarLimitesPlataforma(_tripulacionJugadorUno))
            throw new ArgumentException(MensajeBarcoFueraDelLimiteDeLaPlataforma);

        if (ValidarLongitudBarcoCanonero(_tripulacionJugadorUno))
            throw new ArgumentException(MensajeLogitudBarcoCanonero);

        if (ValidarLongitudBarcoDestructor(_tripulacionJugadorUno))
            throw new ArgumentException(MensajeLogitudBarcoDestructor);

        if (ValidarLongitudBarcoPortaAviones(_tripulacionJugadorUno))
            throw new ArgumentException(MensajeLogitudBarcoPortaaviones);

        if (ValidarCantidadBarcosCanoneros(_tripulacionJugadorUno))
            throw new ArgumentException(MensajeCantidadBarcosCanoneros1);

        if (ValidarCantidadBarcosDestructores(_tripulacionJugadorUno))
            throw new ArgumentException(MensajeCantidadBarcosDestructores);

        if (ValidarCantidadBarcosPortaaviones(_tripulacionJugadorUno))
            throw new ArgumentException(MensajeCantidadBarcosPortaaviones);
    }

    private bool
        ValidarCantidadBarcosPortaaviones(List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> barcos) =>
        barcos.Count(barco => barco.tipoBarco == TipoBarco.Portaaviones) != 1;

    private bool ValidarCantidadBarcosDestructores(
        List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> barcos) =>
        barcos.Count(barco => barco.tipoBarco == TipoBarco.Destructor) != 2;

    private bool ValidarCantidadBarcosCanoneros(List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> barcos) =>
        barcos.Count(barco => barco.tipoBarco == TipoBarco.Canonero) != 4;

    private bool
        ValidarLongitudBarcoPortaAviones(List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> barcos) =>
        barcos.Any(barco => barco.tipoBarco == TipoBarco.Portaaviones && barco.coordenadas.Count() != 4);

    private bool ValidarLongitudBarcoDestructor(List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> barcos) =>
        barcos.Any(barco => barco.tipoBarco == TipoBarco.Destructor && barco.coordenadas.Count() != 3);

    private bool ValidarLongitudBarcoCanonero(List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> barcos) =>
        barcos.Any(barco => barco.tipoBarco == TipoBarco.Canonero && barco.coordenadas.Count() != 1);

    private bool ValidarLimitesPlataforma(List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> barcos) =>
        barcos.Any(barco => barco.coordenadas.Any(coor =>
            coor.x < LimiteInferiorPlataforma || coor.x > LimiteSuperiorPlataforma ||
            coor.y < LimiteInferiorPlataforma || coor.y > LimiteSuperiorPlataforma));
}
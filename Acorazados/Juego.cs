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
    private char[,] _plataforma;
    private List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> _jugadorUno;
    private List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> _jugadorDos;

    public Juego()
    {
        _plataforma = new char[10, 10];
    }

    public void AgregarJugador(List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> listaBarcos)
    {
        _jugadorUno = listaBarcos;
    }

    public void Iniciar()
    {
        if (ValidarLimitesPlataforma(_jugadorUno))
            throw new ArgumentException(MensajeBarcoFueraDelLimiteDeLaPlataforma);

        if (ValidarLongitudBarcoCanonero(_jugadorUno))
            throw new ArgumentException(MensajeLogitudBarcoCanonero);

        if (ValidarLongitudBarcoDestructor(_jugadorUno))
            throw new ArgumentException(MensajeLogitudBarcoDestructor);

        if (ValidarLongitudBarcoPortaAviones(_jugadorUno))
            throw new ArgumentException(MensajeLogitudBarcoPortaaviones);

        if (ValidarCantidadBarcosCanoneros(_jugadorUno))
            throw new ArgumentException(MensajeCantidadBarcosCanoneros1);

        if (_jugadorUno.Count(barco => barco.tipoBarco == TipoBarco.Destructor) != 2)
            throw new ArgumentException("Deben existir 2 destructores en la plataforma");

        if (_jugadorUno.Count(barco => barco.tipoBarco == TipoBarco.Portaaviones) != 1)
            throw new ArgumentException("Deben existir 1 portaavion en la plataforma");


        if (ValidarLimitesPlataforma(_jugadorDos))
            throw new ArgumentException(MensajeBarcoFueraDelLimiteDeLaPlataforma);

        if (ValidarLongitudBarcoCanonero(_jugadorDos))
            throw new ArgumentException(MensajeLogitudBarcoCanonero);

        if (ValidarLongitudBarcoDestructor(_jugadorDos))
            throw new ArgumentException(MensajeLogitudBarcoDestructor);

        if (ValidarLongitudBarcoPortaAviones(_jugadorDos))
            throw new ArgumentException(MensajeLogitudBarcoPortaaviones);

        if (ValidarCantidadBarcosCanoneros(_jugadorDos))
            throw new ArgumentException(MensajeCantidadBarcosCanoneros1);

        if (_jugadorDos.Count(barco => barco.tipoBarco == TipoBarco.Destructor) != 2)
            throw new ArgumentException("Deben existir 2 destructores en la plataforma");
        throw new NotImplementedException();
    }

    private bool ValidarCantidadBarcosCanoneros(List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> barcos)
    {
        return barcos.Count(barco => barco.tipoBarco == TipoBarco.Canonero) != 4;
    }

    private bool ValidarLongitudBarcoPortaAviones(List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> barcos)
    {
        return barcos.Any(barco => barco.tipoBarco == TipoBarco.Portaaviones && barco.coordenadas.Count() != 4);
    }

    private bool ValidarLongitudBarcoDestructor(List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> barcos)
    {
        return barcos.Any(barco => barco.tipoBarco == TipoBarco.Destructor && barco.coordenadas.Count() != 3);
    }

    private bool ValidarLongitudBarcoCanonero(List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> barcos)
    {
        return barcos.Any(barco => barco.tipoBarco == TipoBarco.Canonero && barco.coordenadas.Count() != 1);
    }

    private bool ValidarLimitesPlataforma(List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> barcos)
    {
        return barcos.Any(barco => barco.coordenadas.Any(coor =>
            coor.x < LimiteInferiorPlataforma || coor.x > LimiteSuperiorPlataforma ||
            coor.y < LimiteInferiorPlataforma || coor.y > LimiteSuperiorPlataforma));
    }

    public void AgregarJugadorDos(List<(List<(int x, int y)> coordenadas, TipoBarco)> listaBarcosJugadorDos)
    {
        _jugadorDos = listaBarcosJugadorDos;
    }
}
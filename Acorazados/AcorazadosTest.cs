using FluentAssertions;

namespace Acorazados;

public class AcorazadosTest
{
    
    [Fact]
    public void Si_AgregoUnJugadorConUnCañoneroEnLaPosicion0_10_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        List<(List<(int x, int y)> coordenadas, TipoBarco)> listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        List<(int x, int y)> coordenadasPrimerCañonero = new List<(int x, int y)>();
        coordenadasPrimerCañonero.Add((0,10));
        listaBarcos.Add((coordenadasPrimerCañonero,TipoBarco.Canonero));
        
        Action result = () => juego.AgregarJugador(listaBarcos);

        result.Should().ThrowExactly<ArgumentException>("Barco fuera del limite de la plataforma");
    }
    [Fact]
    public void Si_AgregoUnJugadorConUnCañoneroEnLaPosicion10_0_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        List<(List<(int x, int y)> coordenadas, TipoBarco)> listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        List<(int x, int y)> coordenadasPrimerCañonero = new List<(int x, int y)>();
        coordenadasPrimerCañonero.Add((10,0));
        listaBarcos.Add((coordenadasPrimerCañonero,TipoBarco.Canonero));
        
        Action result = () => juego.AgregarJugador(listaBarcos);

        result.Should().ThrowExactly<ArgumentException>("Barco fuera del limite de la plataforma");
    }
}

public enum TipoBarco
{
    Canonero
}

public class Juego
{
    private const int LimiteSuperiorPlataforma = 9;
    private const int LimiteInferiorPlataforma = 0;
    private char[,] _plataforma;
    public Juego()
    {
        _plataforma = new char[10, 10];
    }

    public void AgregarJugador(List<(List<(int x, int y)> coordenadas, TipoBarco)> listaBarcos)
    {
        if (listaBarcos[0].coordenadas[0].x > LimiteSuperiorPlataforma || listaBarcos[0].coordenadas[0].y < LimiteInferiorPlataforma)
        {
            throw new ArgumentException("Barco fuera del limite de la plataforma");
        }
        
        if (listaBarcos[0].coordenadas[0].x < LimiteInferiorPlataforma || listaBarcos[0].coordenadas[0].y > LimiteSuperiorPlataforma)
        {
            throw new ArgumentException("Barco fuera del limite de la plataforma");
        }
    }
}
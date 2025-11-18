using FluentAssertions;

namespace Acorazados;

public class AcorazadosTest
{
    
    [Fact]
    public void Si_AgregoUnJugadorConUnCañoneroEnLaPosicion0_10_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var coordenadasPrimerCañonero = new List<(int x, int y)> { (0,10) };
        listaBarcos.Add((coordenadasPrimerCañonero,TipoBarco.Canonero));
        
        Action result = () => juego.AgregarJugador(listaBarcos);

        result.Should().ThrowExactly<ArgumentException>("Barco fuera del limite de la plataforma");
    }
    [Fact]
    public void Si_AgregoUnJugadorConUnCañoneroEnLaPosicion10_0_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var coordenadasPrimerCañonero = new List<(int x, int y)> { (10,0) };
        listaBarcos.Add((coordenadasPrimerCañonero,TipoBarco.Canonero));
        
        Action result = () => juego.AgregarJugador(listaBarcos);

        result.Should().ThrowExactly<ArgumentException>("Barco fuera del limite de la plataforma");
    }
    
    [Fact]
    public void Si_AgregoUnJugadorConUnDestructorEnLaPosicion0_8_0_9_0_10Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var destructor = new List<(int x, int y)>
        {
            (0, 8),
            (0, 9),
            (0,10)
        };
        listaBarcos.Add((destructor,TipoBarco.Destructor));
        
        Action result = () => juego.AgregarJugador(listaBarcos);

        result.Should().ThrowExactly<ArgumentException>("Barco fuera del limite de la plataforma");
    }  
    
    [Fact]
    public void Si_AgregoUnJugadorConDosDestructorEnLaPosicion_0_8_0_9_0_10_Y_8_0_9_0_10_0_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var destructor1 = new List<(int x, int y)>
        {
            (0, 7),
            (0, 8),
            (0,9)
        };
        listaBarcos.Add((destructor1,TipoBarco.Destructor));
        
        var destructor2 = new List<(int x, int y)>
        {
            (8, 0),
            (9, 0),
            (10,0)
        };
        listaBarcos.Add((destructor2,TipoBarco.Destructor));
        
        Action result = () => juego.AgregarJugador(listaBarcos);

        result.Should().ThrowExactly<ArgumentException>("Barco fuera del limite de la plataforma");
    } 
    
    [Fact]
    public void Si_AgregoUnJugadorConUnCanoneroEnDosCoordenadasDebe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canonero = new List<(int x, int y)>
        {
            (0, 8),
            (0, 9)
        };
        listaBarcos.Add((canonero,TipoBarco.Canonero));
        
        Action result = () => juego.AgregarJugador(listaBarcos);

        result.Should().ThrowExactly<ArgumentException>("El tipo de barco cañonero es de una coordenada");
    }  
    
    [Fact]
    public void Si_AgregoUnJugadorConUnCanoneroSinCoordenadasDebe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canonero = new List<(int x, int y)>();
        listaBarcos.Add((canonero,TipoBarco.Canonero));
        
        Action result = () => juego.AgregarJugador(listaBarcos);

        result.Should().ThrowExactly<ArgumentException>("El tipo de barco cañonero es de una coordenada");
    }  

    [Fact]
    public void Si_AgregoUnJugadorConUnDestructorConDosCoordenadasDebe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canonero = new List<(int x, int y)>
        {
            (0, 8),
            (0, 9)
        };
        listaBarcos.Add((canonero,TipoBarco.Destructor));
        
        Action result = () => juego.AgregarJugador(listaBarcos);

        result.Should().ThrowExactly<ArgumentException>("El tipo de barco destructor es de tres coordenadas");
    }  
}

public enum TipoBarco
{
    Canonero,
    Destructor
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

    public void AgregarJugador(List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> listaBarcos)
    {
        if(listaBarcos.Any(barco => barco.tipoBarco == TipoBarco.Destructor && barco.coordenadas.Count() < 3))
            throw new ArgumentException("El tipo de barco cañonero es de una coordenada");
        
        if(listaBarcos.Any(barco => barco.tipoBarco == TipoBarco.Canonero && barco.coordenadas.Count() != 1))
            throw new ArgumentException("El tipo de barco cañonero es de una coordenada");
        
        if (listaBarcos.Any(barco => barco.coordenadas.Any(coor => coor.x < LimiteInferiorPlataforma || coor.x > LimiteSuperiorPlataforma || coor.y < LimiteInferiorPlataforma || coor.y > LimiteSuperiorPlataforma)))
        {
            throw new ArgumentException("Barco fuera del limite de la plataforma");
        }
    }
}
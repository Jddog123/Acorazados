using Acorazados.ClassData;
using Acorazados.Enums;
using FluentAssertions;

namespace Acorazados;

public class AcorazadosTest
{
    [Theory]
    [InlineData(0,10,TipoBarco.Canonero)]
    [InlineData(10,0,TipoBarco.Canonero)]
    [InlineData(11,11,TipoBarco.Canonero)]
    [InlineData(-1,-1,TipoBarco.Canonero)]

    public void Si_AgregoJugadorUnoYUnBarcoCañoneroEstaFueraDelLimiteEInicioJuego_Debe_ArrojarExcepcion(int coordenadaX, int coordenadaY, TipoBarco tipoBarco)
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var coordenadasPrimerCañonero = new List<(int x, int y)> { (coordenadaX, coordenadaY) };
        listaBarcos.Add((coordenadasPrimerCañonero, tipoBarco));

        juego.AgregarJugadorUno(listaBarcos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>("Barco fuera del limite de la plataforma");
    }

    [Fact]
    public void Si_AgregoUnJugadorConUnDestructorEnLaPosicion0_8_0_9_0_10EinicioJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var destructor = new List<(int x, int y)>
        {
            (0, 8),
            (0, 9),
            (0, 10)
        };
        listaBarcos.Add((destructor, TipoBarco.Destructor));

        juego.AgregarJugadorUno(listaBarcos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>("Barco fuera del limite de la plataforma");
    }

    [Fact]
    public void
        Si_AgregoUnJugadorConDosDestructorEnLaPosicion_0_8_0_9_0_10_Y_8_0_9_0_10_0_EInicioJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var destructor1 = new List<(int x, int y)>
        {
            (0, 7),
            (0, 8),
            (0, 9)
        };
        listaBarcos.Add((destructor1, TipoBarco.Destructor));

        var destructor2 = new List<(int x, int y)>
        {
            (8, 0),
            (9, 0),
            (10, 0)
        };
        listaBarcos.Add((destructor2, TipoBarco.Destructor));

        juego.AgregarJugadorUno(listaBarcos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Barco fuera del limite de la plataforma");
    }

    [Fact]
    public void Si_AgregoUnJugadorConUnCanoneroEnDosCoordenadasEInicioJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canonero = new List<(int x, int y)>
        {
            (0, 8),
            (0, 9)
        };
        listaBarcos.Add((canonero, TipoBarco.Canonero));

        juego.AgregarJugadorUno(listaBarcos);
        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("El tipo de barco cañonero es de una coordenada");
    }

    [Fact]
    public void Si_AgregoUnJugadorConUnCanoneroSinCoordenadasEInicioJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canonero = new List<(int x, int y)>();
        listaBarcos.Add((canonero, TipoBarco.Canonero));

        juego.AgregarJugadorUno(listaBarcos);
        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("El tipo de barco cañonero es de una coordenada");
    }

    [Fact]
    public void Si_AgregoUnJugadorConUnCanoneroConCordenadaDiferenteA1EInicioJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canonero = new List<(int x, int y)>();
        listaBarcos.Add((canonero, TipoBarco.Canonero));

        juego.AgregarJugadorUno(listaBarcos);
        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("El tipo de barco cañonero es de una coordenada");
    }

    [Fact]
    public void Si_AgregoUnJugadorConUnDestructorConDosCoordenadasDebe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var destructor = new List<(int x, int y)>
        {
            (0, 8),
            (0, 9)
        };
        listaBarcos.Add((destructor, TipoBarco.Destructor));

        juego.AgregarJugadorUno(listaBarcos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("El tipo de barco destructor es de tres coordenadas");
    }

    [Fact]
    public void Si_AgregoUnJugadorConUnDestructorConCuatroCoordenadasDebe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var destructor = new List<(int x, int y)>
        {
            (0, 6),
            (0, 7),
            (0, 8),
            (0, 9)
        };
        listaBarcos.Add((destructor, TipoBarco.Destructor));

        juego.AgregarJugadorUno(listaBarcos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("El tipo de barco destructor es de tres coordenadas");
    }

    [Fact]
    public void Si_AgregoUnJugadorConUnDestructorConCoordenadasDiferenteATresEInicioJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var destructor = new List<(int x, int y)>
        {
            (0, 8),
            (0, 9)
        };
        listaBarcos.Add((destructor, TipoBarco.Destructor));

        juego.AgregarJugadorUno(listaBarcos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("El tipo de barco destructor es de tres coordenadas");
    }

    [Fact]
    public void Si_AgregoUnJugadorConUnPortaavionesConTresCoordenadasDebe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var portaaviones = new List<(int x, int y)>
        {
            (0, 6),
            (0, 7),
            (0, 8)
        };
        listaBarcos.Add((portaaviones, TipoBarco.Portaaviones));
        juego.AgregarJugadorUno(listaBarcos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("El tipo de barco portaaviones es de cuatro coordenadas");
    }

    [Fact]
    public void Si_AgregoUnJugadorConUnPortaavionesConCincoCoordenadasDebe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var portaaviones = new List<(int x, int y)>
        {
            (0, 0),
            (0, 1),
            (0, 2),
            (0, 3),
            (0, 4),
        };
        listaBarcos.Add((portaaviones, TipoBarco.Portaaviones));
        juego.AgregarJugadorUno(listaBarcos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("El tipo de barco portaaviones es de cuatro coordenadas");
    }

    [Fact]
    public void Si_AgregoUnJugadorConUnPortaavionesConCoordenadasDiferentesA5EInicioJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var portaaviones = new List<(int x, int y)>
        {
            (0, 6),
            (0, 7),
            (0, 8)
        };
        listaBarcos.Add((portaaviones, TipoBarco.Portaaviones));
        juego.AgregarJugadorUno(listaBarcos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("El tipo de barco portaaviones es de cuatro coordenadas");
    }

    [Fact]
    public void Si_AgregoUnJugadorConTresBarcosCañonerosEInicioElJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canoneroUno = new List<(int x, int y)>
        {
            (0, 0)
        };
        var canoneroDos = new List<(int x, int y)>
        {
            (0, 1)
        };
        var canoneroTres = new List<(int x, int y)>
        {
            (0, 2)
        };

        listaBarcos.Add((canoneroUno, TipoBarco.Canonero));
        listaBarcos.Add((canoneroDos, TipoBarco.Canonero));
        listaBarcos.Add((canoneroTres, TipoBarco.Canonero));

        juego.AgregarJugadorUno(listaBarcos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Deben existir 4 cañoreros en la plataforma");
    }

    [Fact]
    public void Si_AgregoUnJugadorConCincoBarcosCañonerosEInicioElJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canoneroUno = new List<(int x, int y)>
        {
            (0, 0)
        };
        var canoneroDos = new List<(int x, int y)>
        {
            (0, 1)
        };
        var canoneroTres = new List<(int x, int y)>
        {
            (0, 2)
        };
        var canoneroCuatro = new List<(int x, int y)>
        {
            (0, 3)
        };
        var canoneroCinco = new List<(int x, int y)>
        {
            (0, 4)
        };

        listaBarcos.Add((canoneroUno, TipoBarco.Canonero));
        listaBarcos.Add((canoneroDos, TipoBarco.Canonero));
        listaBarcos.Add((canoneroTres, TipoBarco.Canonero));
        listaBarcos.Add((canoneroCuatro, TipoBarco.Canonero));
        listaBarcos.Add((canoneroCinco, TipoBarco.Canonero));

        juego.AgregarJugadorUno(listaBarcos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Deben existir 4 cañoreros en la plataforma");
    }

    [Fact]
    public void Si_AgregoUnJugadorCon4CañoerosYUnDestructorEInicioElJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canoneroUno = new List<(int x, int y)>
        {
            (0, 0)
        };
        var canoneroDos = new List<(int x, int y)>
        {
            (0, 1)
        };
        var canoneroTres = new List<(int x, int y)>
        {
            (0, 2)
        };
        var canoneroCuatro = new List<(int x, int y)>
        {
            (0, 3)
        };
        listaBarcos.Add((canoneroUno, TipoBarco.Canonero));
        listaBarcos.Add((canoneroDos, TipoBarco.Canonero));
        listaBarcos.Add((canoneroTres, TipoBarco.Canonero));
        listaBarcos.Add((canoneroCuatro, TipoBarco.Canonero));

        var destructor = new List<(int x, int y)>
        {
            (0, 0), (0, 2), (0, 3)
        };

        listaBarcos.Add((destructor, TipoBarco.Destructor));

        juego.AgregarJugadorUno(listaBarcos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Deben existir 2 destructores en la plataforma");
    }

    [Fact]
    public void Si_AgregoUnJugadorCon4CañoerosYTresDestructorEInicioElJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canoneroUno = new List<(int x, int y)>
        {
            (0, 0)
        };
        var canoneroDos = new List<(int x, int y)>
        {
            (0, 1)
        };
        var canoneroTres = new List<(int x, int y)>
        {
            (0, 2)
        };
        var canoneroCuatro = new List<(int x, int y)>
        {
            (0, 3)
        };
        listaBarcos.Add((canoneroUno, TipoBarco.Canonero));
        listaBarcos.Add((canoneroDos, TipoBarco.Canonero));
        listaBarcos.Add((canoneroTres, TipoBarco.Canonero));
        listaBarcos.Add((canoneroCuatro, TipoBarco.Canonero));

        var destructor1 = new List<(int x, int y)>
        {
            (0, 0), (0, 2), (0, 3)
        };
        var destructor2 = new List<(int x, int y)>
        {
            (0, 0), (0, 2), (0, 3)
        };
        var destructor3 = new List<(int x, int y)>
        {
            (0, 0), (0, 2), (0, 3)
        };

        listaBarcos.Add((destructor1, TipoBarco.Destructor));
        listaBarcos.Add((destructor2, TipoBarco.Destructor));
        listaBarcos.Add((destructor3, TipoBarco.Destructor));

        juego.AgregarJugadorUno(listaBarcos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Deben existir 2 destructores en la plataforma");
    }

    [Fact]
    public void Si_AgregoUnJugadorCon4CañoerosDosDestructorYNingunPortaavionEInicioElJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canoneroUno = new List<(int x, int y)>
        {
            (0, 0)
        };
        var canoneroDos = new List<(int x, int y)>
        {
            (0, 1)
        };
        var canoneroTres = new List<(int x, int y)>
        {
            (0, 2)
        };
        var canoneroCuatro = new List<(int x, int y)>
        {
            (0, 3)
        };
        listaBarcos.Add((canoneroUno, TipoBarco.Canonero));
        listaBarcos.Add((canoneroDos, TipoBarco.Canonero));
        listaBarcos.Add((canoneroTres, TipoBarco.Canonero));
        listaBarcos.Add((canoneroCuatro, TipoBarco.Canonero));

        var destructor1 = new List<(int x, int y)>
        {
            (0, 0), (0, 2), (0, 3)
        };
        var destructor2 = new List<(int x, int y)>
        {
            (0, 0), (0, 2), (0, 3)
        };

        listaBarcos.Add((destructor1, TipoBarco.Destructor));
        listaBarcos.Add((destructor2, TipoBarco.Destructor));

        juego.AgregarJugadorUno(listaBarcos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Deben existir 1 portaavion en la plataforma");
    }

    [Fact]
    public void Si_AgregoUnJugadorCon4CañoerosDosDestructorYDosPortaavionEInicioElJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canoneroUno = new List<(int x, int y)>
        {
            (2, 0)
        };
        var canoneroDos = new List<(int x, int y)>
        {
            (2, 1)
        };
        var canoneroTres = new List<(int x, int y)>
        {
            (2, 2)
        };
        var canoneroCuatro = new List<(int x, int y)>
        {
            (2, 3)
        };
        listaBarcos.Add((canoneroUno, TipoBarco.Canonero));
        listaBarcos.Add((canoneroDos, TipoBarco.Canonero));
        listaBarcos.Add((canoneroTres, TipoBarco.Canonero));
        listaBarcos.Add((canoneroCuatro, TipoBarco.Canonero));

        var destructor1 = new List<(int x, int y)>
        {
            (3, 0), (3, 2), (3, 3)
        };
        var destructor2 = new List<(int x, int y)>
        {
            (4, 0), (4, 2), (4, 3)
        };

        listaBarcos.Add((destructor1, TipoBarco.Destructor));
        listaBarcos.Add((destructor2, TipoBarco.Destructor));

        var portaAvion1 = new List<(int x, int y)>
        {
            (0, 0), (0, 2), (0, 3), (0, 4)
        };
        var portaAvion2 = new List<(int x, int y)>
        {
            (1, 0), (1, 2), (1, 3), (1, 3)
        };

        listaBarcos.Add((portaAvion1, TipoBarco.Portaaviones));
        listaBarcos.Add((portaAvion2, TipoBarco.Portaaviones));
        juego.AgregarJugadorUno(listaBarcos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Deben existir 1 portaavion en la plataforma");
    }

    [Fact]
    public void Si_AgregoUnSegundoJugadorConUnBarcoFueraDeLaPlataformaEInicioJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcosJugadorUno = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canoneroUno = new List<(int x, int y)>
        {
            (2, 0)
        };
        var canoneroDos = new List<(int x, int y)>
        {
            (2, 1)
        };
        var canoneroTres = new List<(int x, int y)>
        {
            (2, 2)
        };
        var canoneroCuatro = new List<(int x, int y)>
        {
            (2, 3)
        };
        listaBarcosJugadorUno.Add((canoneroUno, TipoBarco.Canonero));
        listaBarcosJugadorUno.Add((canoneroDos, TipoBarco.Canonero));
        listaBarcosJugadorUno.Add((canoneroTres, TipoBarco.Canonero));
        listaBarcosJugadorUno.Add((canoneroCuatro, TipoBarco.Canonero));

        var destructor1 = new List<(int x, int y)>
        {
            (3, 0), (3, 2), (3, 3)
        };
        var destructor2 = new List<(int x, int y)>
        {
            (4, 0), (4, 2), (4, 3)
        };

        listaBarcosJugadorUno.Add((destructor1, TipoBarco.Destructor));
        listaBarcosJugadorUno.Add((destructor2, TipoBarco.Destructor));

        var portaAvion1 = new List<(int x, int y)>
        {
            (0, 0), (0, 2), (0, 3), (0, 4)
        };

        listaBarcosJugadorUno.Add((portaAvion1, TipoBarco.Portaaviones));
        juego.AgregarJugadorUno(listaBarcosJugadorUno);

        var listaBarcosJugadorDos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canoneroJugadorDos = new List<(int x, int y)>
        {
            (10, 10)
        };

        listaBarcosJugadorDos.Add((canoneroJugadorDos, TipoBarco.Canonero));
        juego.AgregarJugadorDos(listaBarcosJugadorDos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Barco fuera del limite de la plataforma");
    }

    [Fact]
    public void
        Si_AgregoUnSegundoJugadorConUnCanoneroConSuLongitudDiferenteAUnaCordenadaEInicioJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcosJugadorUno = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canoneroUno = new List<(int x, int y)>
        {
            (2, 0)
        };
        var canoneroDos = new List<(int x, int y)>
        {
            (2, 1)
        };
        var canoneroTres = new List<(int x, int y)>
        {
            (2, 2)
        };
        var canoneroCuatro = new List<(int x, int y)>
        {
            (2, 3)
        };
        listaBarcosJugadorUno.Add((canoneroUno, TipoBarco.Canonero));
        listaBarcosJugadorUno.Add((canoneroDos, TipoBarco.Canonero));
        listaBarcosJugadorUno.Add((canoneroTres, TipoBarco.Canonero));
        listaBarcosJugadorUno.Add((canoneroCuatro, TipoBarco.Canonero));

        var destructor1 = new List<(int x, int y)>
        {
            (3, 0), (3, 2), (3, 3)
        };
        var destructor2 = new List<(int x, int y)>
        {
            (4, 0), (4, 2), (4, 3)
        };

        listaBarcosJugadorUno.Add((destructor1, TipoBarco.Destructor));
        listaBarcosJugadorUno.Add((destructor2, TipoBarco.Destructor));

        var portaAvion1 = new List<(int x, int y)>
        {
            (0, 0), (0, 2), (0, 3), (0, 4)
        };

        listaBarcosJugadorUno.Add((portaAvion1, TipoBarco.Portaaviones));
        juego.AgregarJugadorUno(listaBarcosJugadorUno);

        var listaBarcosJugadorDos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canonero1JugadorDos = new List<(int x, int y)>
        {
            (0, 2),
            (0, 1)
        };

        listaBarcosJugadorDos.Add((canonero1JugadorDos, TipoBarco.Canonero));
        juego.AgregarJugadorDos(listaBarcosJugadorDos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("El tipo de barco cañonero es de una coordenada");
    }

    [Fact]
    public void
        Si_AgregoUnSegundoJugadorConUnDestructorConSuLongitudDiferenteATresCoordenadasEInicioJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcosJugadorUno = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canoneroUno = new List<(int x, int y)>
        {
            (2, 0)
        };
        var canoneroDos = new List<(int x, int y)>
        {
            (2, 1)
        };
        var canoneroTres = new List<(int x, int y)>
        {
            (2, 2)
        };
        var canoneroCuatro = new List<(int x, int y)>
        {
            (2, 3)
        };
        listaBarcosJugadorUno.Add((canoneroUno, TipoBarco.Canonero));
        listaBarcosJugadorUno.Add((canoneroDos, TipoBarco.Canonero));
        listaBarcosJugadorUno.Add((canoneroTres, TipoBarco.Canonero));
        listaBarcosJugadorUno.Add((canoneroCuatro, TipoBarco.Canonero));

        var destructor1 = new List<(int x, int y)>
        {
            (3, 0), (3, 2), (3, 3)
        };
        var destructor2 = new List<(int x, int y)>
        {
            (4, 0), (4, 2), (4, 3)
        };

        listaBarcosJugadorUno.Add((destructor1, TipoBarco.Destructor));
        listaBarcosJugadorUno.Add((destructor2, TipoBarco.Destructor));

        var portaAvion1 = new List<(int x, int y)>
        {
            (0, 0), (0, 2), (0, 3), (0, 4)
        };

        listaBarcosJugadorUno.Add((portaAvion1, TipoBarco.Portaaviones));
        juego.AgregarJugadorUno(listaBarcosJugadorUno);

        var listaBarcosJugadorDos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var destructor1JugadorDos = new List<(int x, int y)>
        {
            (4, 0),
            (5, 0),
            (6, 0),
            (7, 0)
        };
        listaBarcosJugadorDos.Add((destructor1JugadorDos, TipoBarco.Destructor));

        juego.AgregarJugadorDos(listaBarcosJugadorDos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("El tipo de barco destructor es de tres coordenadas");
    }

    [Fact]
    public void
        Si_AgregoUnSegundoJugadorConUnPortaAvionConSuLongitudDiferenteACuatroCoordenadasEInicioJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcosJugadorUno = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canoneroUno = new List<(int x, int y)>
        {
            (2, 0)
        };
        var canoneroDos = new List<(int x, int y)>
        {
            (2, 1)
        };
        var canoneroTres = new List<(int x, int y)>
        {
            (2, 2)
        };
        var canoneroCuatro = new List<(int x, int y)>
        {
            (2, 3)
        };
        listaBarcosJugadorUno.Add((canoneroUno, TipoBarco.Canonero));
        listaBarcosJugadorUno.Add((canoneroDos, TipoBarco.Canonero));
        listaBarcosJugadorUno.Add((canoneroTres, TipoBarco.Canonero));
        listaBarcosJugadorUno.Add((canoneroCuatro, TipoBarco.Canonero));

        var destructor1 = new List<(int x, int y)>
        {
            (3, 0), (3, 2), (3, 3)
        };
        var destructor2 = new List<(int x, int y)>
        {
            (4, 0), (4, 2), (4, 3)
        };

        listaBarcosJugadorUno.Add((destructor1, TipoBarco.Destructor));
        listaBarcosJugadorUno.Add((destructor2, TipoBarco.Destructor));

        var portaAvion1 = new List<(int x, int y)>
        {
            (0, 0), (0, 2), (0, 3), (0, 4)
        };

        listaBarcosJugadorUno.Add((portaAvion1, TipoBarco.Portaaviones));
        juego.AgregarJugadorUno(listaBarcosJugadorUno);

        var listaBarcosJugadorDos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var portaAvion1JugadorDos = new List<(int x, int y)>
        {
            (4, 0),
            (5, 0),
            (6, 0),
            (7, 0),
            (8, 0)
        };
        listaBarcosJugadorDos.Add((portaAvion1JugadorDos, TipoBarco.Portaaviones));

        juego.AgregarJugadorDos(listaBarcosJugadorDos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("El tipo de barco portaaviones es de cuatro coordenadas");
    }

    [Fact]
    public void
        Si_AgregoUnSegundoJugadorConCantidadDeBarcosCañonerosDiferentesACuatroEInicioJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcosJugadorUno = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canoneroUno = new List<(int x, int y)>
        {
            (2, 0)
        };
        var canoneroDos = new List<(int x, int y)>
        {
            (2, 1)
        };
        var canoneroTres = new List<(int x, int y)>
        {
            (2, 2)
        };
        var canoneroCuatro = new List<(int x, int y)>
        {
            (2, 3)
        };
        listaBarcosJugadorUno.Add((canoneroUno, TipoBarco.Canonero));
        listaBarcosJugadorUno.Add((canoneroDos, TipoBarco.Canonero));
        listaBarcosJugadorUno.Add((canoneroTres, TipoBarco.Canonero));
        listaBarcosJugadorUno.Add((canoneroCuatro, TipoBarco.Canonero));

        var destructor1 = new List<(int x, int y)>
        {
            (3, 0), (3, 2), (3, 3)
        };
        var destructor2 = new List<(int x, int y)>
        {
            (4, 0), (4, 2), (4, 3)
        };

        listaBarcosJugadorUno.Add((destructor1, TipoBarco.Destructor));
        listaBarcosJugadorUno.Add((destructor2, TipoBarco.Destructor));

        var portaAvion1 = new List<(int x, int y)>
        {
            (0, 0), (0, 2), (0, 3), (0, 4)
        };

        listaBarcosJugadorUno.Add((portaAvion1, TipoBarco.Portaaviones));
        juego.AgregarJugadorUno(listaBarcosJugadorUno);

        var listaBarcosJugadorDos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canoneroUnoJugadorDos = new List<(int x, int y)>
        {
            (6, 0)
        };
        var canoneroDosJugadorDos = new List<(int x, int y)>
        {
            (7, 0)
        };
        var canoneroTresJugadorDos = new List<(int x, int y)>
        {
            (8, 0)
        };
        listaBarcosJugadorDos.Add((canoneroUnoJugadorDos, TipoBarco.Canonero));
        listaBarcosJugadorDos.Add((canoneroDosJugadorDos, TipoBarco.Canonero));
        listaBarcosJugadorDos.Add((canoneroTresJugadorDos, TipoBarco.Canonero));

        juego.AgregarJugadorDos(listaBarcosJugadorDos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Deben existir 4 cañoreros en la plataforma");
    }

    [Fact]
    public void Si_AgregoUnSegundoJugadorConCantidadDeBarcosDestructoresDiferentesA2EInicioJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcosJugadorUno = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canoneroUno = new List<(int x, int y)>
        {
            (2, 0)
        };
        var canoneroDos = new List<(int x, int y)>
        {
            (2, 1)
        };
        var canoneroTres = new List<(int x, int y)>
        {
            (2, 2)
        };
        var canoneroCuatro = new List<(int x, int y)>
        {
            (2, 3)
        };
        listaBarcosJugadorUno.Add((canoneroUno, TipoBarco.Canonero));
        listaBarcosJugadorUno.Add((canoneroDos, TipoBarco.Canonero));
        listaBarcosJugadorUno.Add((canoneroTres, TipoBarco.Canonero));
        listaBarcosJugadorUno.Add((canoneroCuatro, TipoBarco.Canonero));

        var destructor1 = new List<(int x, int y)>
        {
            (3, 0), (3, 2), (3, 3)
        };
        var destructor2 = new List<(int x, int y)>
        {
            (4, 0), (4, 2), (4, 3)
        };

        listaBarcosJugadorUno.Add((destructor1, TipoBarco.Destructor));
        listaBarcosJugadorUno.Add((destructor2, TipoBarco.Destructor));

        var portaAvion1 = new List<(int x, int y)>
        {
            (0, 0), (0, 2), (0, 3), (0, 4)
        };

        listaBarcosJugadorUno.Add((portaAvion1, TipoBarco.Portaaviones));
        juego.AgregarJugadorUno(listaBarcosJugadorUno);

        var listaBarcosJugadorDos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canoneroUnoJugadorDos = new List<(int x, int y)>
        {
            (6, 0)
        };
        var canoneroDosJugadorDos = new List<(int x, int y)>
        {
            (7, 0)
        };
        var canoneroTresJugadorDos = new List<(int x, int y)>
        {
            (8, 0)
        };
        var canoneroCuatroJugadorDos = new List<(int x, int y)>
        {
            (9, 0)
        };
        listaBarcosJugadorDos.Add((canoneroUnoJugadorDos, TipoBarco.Canonero));
        listaBarcosJugadorDos.Add((canoneroDosJugadorDos, TipoBarco.Canonero));
        listaBarcosJugadorDos.Add((canoneroTresJugadorDos, TipoBarco.Canonero));
        listaBarcosJugadorDos.Add((canoneroCuatroJugadorDos, TipoBarco.Canonero));

        var destructorUnoJugadorDos = new List<(int x, int y)>
        {
            (8, 0),
            (8, 1),
            (8, 2),
        };
        listaBarcosJugadorDos.Add((destructorUnoJugadorDos, TipoBarco.Destructor));

        juego.AgregarJugadorDos(listaBarcosJugadorDos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Deben existir 2 destructores en la plataforma");
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_AgregoUnSegundoJugadorConCantidadDeBarcosPortaavionesDiferentesA1EInicioJuego_Debe_ArrojarExcepcion(DatosTripulacionJugadores datosTripulacionJugadores)
    {
        var juego = new Juego();
        
        juego.AgregarJugadorUno(datosTripulacionJugadores.tripulacionJugadorUno);    

        var listaBarcosJugadorDos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canoneroUnoJugadorDos = new List<(int x, int y)>
        {
            (6, 0)
        };
        var canoneroDosJugadorDos = new List<(int x, int y)>
        {
            (7, 0)
        };
        var canoneroTresJugadorDos = new List<(int x, int y)>
        {
            (8, 0)
        };
        var canoneroCuatroJugadorDos = new List<(int x, int y)>
        {
            (9, 0)
        };
        listaBarcosJugadorDos.Add((canoneroUnoJugadorDos, TipoBarco.Canonero));
        listaBarcosJugadorDos.Add((canoneroDosJugadorDos, TipoBarco.Canonero));
        listaBarcosJugadorDos.Add((canoneroTresJugadorDos, TipoBarco.Canonero));
        listaBarcosJugadorDos.Add((canoneroCuatroJugadorDos, TipoBarco.Canonero));

        var destructorUnoJugadorDos = new List<(int x, int y)>
        {
            (8, 0),
            (8, 1),
            (8, 2),
        };
        var destructorDosJugadorDos = new List<(int x, int y)>
        {
            (7, 0),
            (7, 1),
            (7, 2),
        };
        listaBarcosJugadorDos.Add((destructorUnoJugadorDos, TipoBarco.Destructor));
        listaBarcosJugadorDos.Add((destructorDosJugadorDos, TipoBarco.Destructor));


        juego.AgregarJugadorDos(listaBarcosJugadorDos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Deben existir 1 portaavion en la plataforma");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_AgregoDosJugadoresYElJugadorDosTieneUnCanoneroEnLaPosicion0_0YElJugadorUnoDisparaEnLaPosicion0_0_Mensaje_Debe_SerBarcoHundido(DatosTripulacionJugadores datosTripulacionJugadores)
    {
        var juego = new Juego();
        
        juego.AgregarJugadorUno(datosTripulacionJugadores.tripulacionJugadorUno);
        juego.AgregarJugadorDos(datosTripulacionJugadores.tripulacionJugadorDos);
        juego.Iniciar();
       
        string mensaje = juego.Disparar(0,0);

        mensaje.Should().Be("Barco hundido");
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_AgregoDosJugadoresYElJugadorDosTieneUnCanoneroEnLaPosicion0_1YElJugadorUnoDisparaEnLaPosicion0_1_Mensaje_Debe_SerBarcoHundido(DatosTripulacionJugadores datosTripulacionJugadores)
    {
        var juego = new Juego();
        
        juego.AgregarJugadorUno(datosTripulacionJugadores.tripulacionJugadorUno);
        juego.AgregarJugadorDos(datosTripulacionJugadores.tripulacionJugadorDos);
        juego.Iniciar();
       
        string mensaje = juego.Disparar(0,1);

        mensaje.Should().Be("Barco hundido");
    }
    
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_AgregoDosJugadoresYElJugadorDosTieneUnCanoneroEnLaPosicion5_1YElJugadorUnoDisparaEnLaPosicion5_1_Mensaje_Debe_SerBarcoHundido(DatosTripulacionJugadores datosTripulacionJugadores)
    {
        var juego = new Juego();
        
        juego.AgregarJugadorUno(datosTripulacionJugadores.tripulacionJugadorUno);
        juego.AgregarJugadorDos(datosTripulacionJugadores.tripulacionJugadorDos);
        juego.Iniciar();
       
        string mensaje = juego.Disparar(5,1);

        mensaje.Should().Be("Barco hundido");
    }
    
}
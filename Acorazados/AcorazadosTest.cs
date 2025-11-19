using Acorazados.Enums;
using FluentAssertions;

namespace Acorazados;

public class AcorazadosTest
{
    [Fact]
    public void Si_AgregoUnJugadorConUnCañoneroEnLaPosicion0_10EInicioJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var coordenadasPrimerCañonero = new List<(int x, int y)> { (0, 10) };
        listaBarcos.Add((coordenadasPrimerCañonero, TipoBarco.Canonero));

        juego.AgregarJugador(listaBarcos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>("Barco fuera del limite de la plataforma");
    }

    [Fact]
    public void Si_AgregoUnJugadorConUnCañoneroEnLaPosicion10_0EInicioJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var coordenadasPrimerCañonero = new List<(int x, int y)> { (10, 0) };
        listaBarcos.Add((coordenadasPrimerCañonero, TipoBarco.Canonero));

        juego.AgregarJugador(listaBarcos);

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

        juego.AgregarJugador(listaBarcos);

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

        juego.AgregarJugador(listaBarcos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Barco fuera del limite de la plataforma");
    }

    [Fact]
    public void Si_AgregoUnJugadorConUnCañoneroFueraDeLaPlataformaEInicioJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var coordenadasPrimerCañonero = new List<(int x, int y)> { (0, 10) };
        listaBarcos.Add((coordenadasPrimerCañonero, TipoBarco.Canonero));

        juego.AgregarJugador(listaBarcos);

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

        juego.AgregarJugador(listaBarcos);
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

        juego.AgregarJugador(listaBarcos);
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

        juego.AgregarJugador(listaBarcos);
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

        juego.AgregarJugador(listaBarcos);

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

        juego.AgregarJugador(listaBarcos);

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

        juego.AgregarJugador(listaBarcos);

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
        juego.AgregarJugador(listaBarcos);

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
        juego.AgregarJugador(listaBarcos);

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
        juego.AgregarJugador(listaBarcos);

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

        juego.AgregarJugador(listaBarcos);

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

        juego.AgregarJugador(listaBarcos);

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

        juego.AgregarJugador(listaBarcos);

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

        juego.AgregarJugador(listaBarcos);

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

        juego.AgregarJugador(listaBarcos);

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
        juego.AgregarJugador(listaBarcos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Deben existir 1 portaavion en la plataforma");
    }
}
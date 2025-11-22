using System.Runtime.InteropServices;
using Acorazados.ClassData;
using Acorazados.Dominio;
using Acorazados.Dominio.Barcos;
using Acorazados.Dominio.Enums;
using FluentAssertions;

namespace Acorazados;

public class AcorazadosTest
{
    private Juego _juego;
    public AcorazadosTest()
    {
        _juego = new Juego();
    }
    
    [Theory]
    [InlineData(0, 10)]
    [InlineData(10, 0)]
    [InlineData(11, 11)]
    [InlineData(-1, -1)]
    public void Si_AgregoJugadorUnoYUnBarcoCañoneroEstaFueraDelLimiteEInicioJuego_Debe_ArrojarExcepcion(int coordenadaX,
        int coordenadaY)
    {
        var listaBarcos = new List<Barco> { new Canonero(coordenadaX, coordenadaY) };

        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");

        Action result = () => _juego.Iniciar(listaBarcos, new List<Barco>());

        result.Should().ThrowExactly<ArgumentException>("Barco fuera del limite de la plataforma");
    }

    [Fact]
    public void Si_AgregoUnJugadorConUnDestructorEnLaPosicion0_8_0_9_0_10EinicioJuego_Debe_ArrojarExcepcion()
    {
        var listaBarcos = new List<Barco> { new Destructor([(0, 8), (0, 9), (0, 10)]) };

        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");

        Action result = () => _juego.Iniciar(listaBarcos, new List<Barco>());

        result.Should().ThrowExactly<ArgumentException>("Barco fuera del limite de la plataforma");
    }

    [Fact]
    public void
        Si_AgregoUnJugadorConDosDestructorEnLaPosicion_0_8_0_9_0_10_Y_8_0_9_0_10_0_EInicioJuego_Debe_ArrojarExcepcion()
    {
        var listaBarcos = new List<Barco>
        {
            new Destructor([(0, 7), (0, 8), (0, 9)]),
            new Destructor([(8, 0), (9, 0), (10, 0)])
        };

        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        Action result = () => _juego.Iniciar(listaBarcos, new List<Barco>());

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Barco fuera del limite de la plataforma");
    }


    [Fact]
    public void Si_CreoUnCanoneroSinCoordenadas_Debe_ArrojarExcepcion()
    {
        Action result = () => new List<Barco> { new Canonero() };

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("El tipo de barco cañonero necesita las coordenadas");
    }

    [Fact]
    public void Si_CreoUnDestructorConDosCoordenadasDebe_ArrojarExcepcion()
    {
        Action result = () => new List<Barco> { new Destructor([(0, 8), (0, 9)]) };

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("El tipo de barco destructor es de tres coordenadas");
    }

    [Fact]
    public void Si_CreoUnDestructorConCuatroCoordenadasDebe_ArrojarExcepcion()
    {
        Action result = () => new List<Barco> { new Destructor([(0, 6), (0, 7), (0, 8), (0, 9)]) };

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("El tipo de barco destructor es de tres coordenadas");
    }

    [Fact]
    public void Si_AgregoUnJugadorConUnPortaavionesConTresCoordenadasDebe_ArrojarExcepcion()
    {
        Action result = () => new List<Barco> { new Portaaviones([(0, 6), (0, 7), (0, 8)]) };

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("El tipo de barco portaaviones es de cuatro coordenadas");
    }

    [Fact]
    public void Si_CreoUnPortaavionesConCincoCoordenadasDebe_ArrojarExcepcion()
    {
        Action result = () => new List<Barco> { new Portaaviones([(0, 0), (0, 1), (0, 2), (0, 3), (0, 4)]) };

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("El tipo de barco portaaviones es de cuatro coordenadas");
    }

    [Fact]
    public void Si_AgregoUnJugadorConTresBarcosCañonerosEInicioElJuego_Debe_ArrojarExcepcion()
    {
        var listaBarcos = new List<Barco>
        {
            new Canonero(0, 0),
            new Canonero(0, 1),
            new Canonero(0, 2)
        };

        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        Action result = () => _juego.Iniciar(listaBarcos, new List<Barco>());

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Deben existir 4 cañoreros en la plataforma");
    }

    [Fact]
    public void Si_AgregoUnJugadorConCincoBarcosCañonerosEInicioElJuego_Debe_ArrojarExcepcion()
    {
        var listaBarcos = new List<Barco>
        {
            new Canonero(0, 0),
            new Canonero(0, 1),
            new Canonero(0, 2),
            new Canonero(0, 3),
            new Canonero(0, 4)
        };

        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        Action result = () => _juego.Iniciar(listaBarcos, new List<Barco>());

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Deben existir 4 cañoreros en la plataforma");
    }

    [Fact]
    public void Si_AgregoUnJugadorCon4CañoerosYUnDestructorEInicioElJuego_Debe_ArrojarExcepcion()
    {
        var listaBarcos = new List<Barco>
        {
            new Canonero(0, 0),
            new Canonero(0, 1),
            new Canonero(0, 2),
            new Canonero(0, 3),
            new Destructor([(1, 0), (1, 2), (1, 3)])
        };

        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        Action result = () => _juego.Iniciar(listaBarcos, new List<Barco>());

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Deben existir 2 destructores en la plataforma");
    }

    [Fact]
    public void Si_AgregoUnJugadorCon4CañoerosYTresDestructorEInicioElJuego_Debe_ArrojarExcepcion()
    {
        var listaBarcos = new List<Barco>
        {
            new Canonero(0, 0),
            new Canonero(0, 1),
            new Canonero(0, 2),
            new Canonero(0, 3),
            new Destructor([(1, 0), (1, 1), (1, 2)]),
            new Destructor([(2, 0), (2, 1), (2, 2)]),
            new Destructor([(3, 0), (3, 1), (3, 2)])
        };

        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        Action result = () => _juego.Iniciar(listaBarcos, new List<Barco>());

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Deben existir 2 destructores en la plataforma");
    }

    [Fact]
    public void Si_AgregoUnJugadorCon4CañoerosDosDestructorYNingunPortaavionEInicioElJuego_Debe_ArrojarExcepcion()
    {
        var listaBarcos = new List<Barco>
        {
            new Canonero(0, 0),
            new Canonero(0, 1),
            new Canonero(0, 2),
            new Canonero(0, 3),
            new Destructor([(1, 0), (1, 1), (1, 2)]),
            new Destructor([(2, 0), (2, 1), (2, 2)])
        };

        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        Action result = () => _juego.Iniciar(listaBarcos, new List<Barco>());

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Deben existir 1 portaavion en la plataforma");
    }

    [Fact]
    public void Si_AgregoUnJugadorCon4CañoerosDosDestructorYDosPortaavionEInicioElJuego_Debe_ArrojarExcepcion()
    {
        var listaBarcos = new List<Barco>
        {
            new Canonero(0, 0),
            new Canonero(0, 1),
            new Canonero(0, 2),
            new Canonero(0, 3),
            new Destructor([(1, 0), (1, 1), (1, 2)]),
            new Destructor([(2, 0), (2, 1), (2, 2)]),
            new Portaaviones([(3, 0), (3, 1), (3, 2), (3, 3)]),
            new Portaaviones([(4, 0), (4, 1), (4, 2), (4, 3)]),
        };

        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        Action result = () => _juego.Iniciar(listaBarcos, new List<Barco>());

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Deben existir 1 portaavion en la plataforma");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_AgregoUnSegundoJugadorConUnBarcoFueraDeLaPlataformaEInicioJuego_Debe_ArrojarExcepcion(
        DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        var listaBarcosJugadorDos = new List<Barco> { new Canonero(10, 10) };

        Action result = () => _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, listaBarcosJugadorDos);

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Barco fuera del limite de la plataforma");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoUnSegundoJugadorConCantidadDeBarcosCañonerosDiferentesACuatroEInicioJuego_Debe_ArrojarExcepcion(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        var listaBarcosJugadorDos = new List<Barco>
        {
            new Canonero(6, 0),
            new Canonero(7, 0),
            new Canonero(8, 0)
        };

        Action result = () => _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, listaBarcosJugadorDos);

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Deben existir 4 cañoreros en la plataforma");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_AgregoUnSegundoJugadorConCantidadDeBarcosDestructoresDiferentesA2EInicioJuego_Debe_ArrojarExcepcion(
        DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        var listaBarcosJugadorDos = new List<Barco>
        {
            new Canonero(6, 0),
            new Canonero(7, 0),
            new Canonero(8, 0),
            new Canonero(9, 0),
            new Destructor([(8, 1), (8, 2), (8, 3)])
        };

        Action result = () => _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, listaBarcosJugadorDos);

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Deben existir 2 destructores en la plataforma");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_AgregoUnSegundoJugadorConCantidadDeBarcosPortaavionesDiferentesA1EInicioJuego_Debe_ArrojarExcepcion(
        DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        var listaBarcosJugadorDos = new List<Barco>
        {
            new Canonero(6, 0),
            new Canonero(7, 0),
            new Canonero(8, 0),
            new Canonero(9, 0),
            new Destructor([(8, 1), (8, 2), (8, 3)]),
            new Destructor([(9, 1), (9, 2), (9, 3)])
        };

        Action result = () => _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, listaBarcosJugadorDos);

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Deben existir 1 portaavion en la plataforma");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresYElJugadorDosTieneUnCanoneroEnLaPosicion0_0YElJugadorUnoDisparaEnLaPosicion0_0_Mensaje_Debe_SerBarcoHundido(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);

        var mensaje = _juego.Disparar(0, 0);

        mensaje.Should().Be("Barco hundido");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresYElJugadorDosTieneUnCanoneroEnLaPosicion0_1YElJugadorUnoDisparaEnLaPosicion0_1_Mensaje_Debe_SerBarcoHundido(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);

        var mensaje = _juego.Disparar(0, 1);

        mensaje.Should().Be("Barco hundido");
    }


    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresYElJugadorDosTieneUnCanoneroEnLaPosicion5_1YElJugadorUnoDisparaEnLaPosicion5_1_Mensaje_Debe_SerBarcoHundido(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);

        var mensaje = _juego.Disparar(5, 1);

        mensaje.Should().Be("Barco hundido");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresYElJugadorDosTieneUnCanoneroEnLaPosicion5_2YElJugadorUnoDisparaEnLaPosicion5_2_Mensaje_Debe_SerBarcoHundido(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);

        var mensaje = _juego.Disparar(5, 2);

        mensaje.Should().Be("Barco hundido");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresYElJugadorDosTieneUnDestructorEn9_0_8_0Y7_0YElJugadorUnoDisparaTodasLasCoordenadasElMensaje_Debe_SerBarcoHundido(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.Disparar(9, 0);
        _juego.Disparar(8, 0);

        var mensaje = _juego.Disparar(7, 0);

        mensaje.Should().Be("Barco hundido");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresYElJugadorDosTieneUnDestructorEn5_7_4_7_Y_3_7_0YElJugadorUnoDisparaTodasLasCoordenadasElMensaje_Debe_SerBarcoHundido(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.Disparar(3, 7);
        _juego.Disparar(4, 7);

        var mensaje = _juego.Disparar(5, 7);

        mensaje.Should().Be("Barco hundido");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresYElJugadorDosTieneUnPortaavionEn1_3_1_4_1_5Y_1_6YElJugadorUnoDisparaTodasLasCoordenadasElMensaje_Debe_SerBarcoHundido(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.Disparar(1, 3);
        _juego.Disparar(1, 4);
        _juego.Disparar(1, 5);

        var mensaje = _juego.Disparar(1, 6);

        mensaje.Should().Be("Barco hundido");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresYElJugadorDosTieneUnPortaavionEn9_9_8_9_7_9Y_6_9YElJugadorUnoDisparaTodasLasCoordenadasElMensaje_Debe_SerBarcoHundido(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        var tripulacionJugadorDos = new List<Barco>
        {
            new Canonero(0, 0),
            new Canonero(0, 1),
            new Canonero(5, 1),
            new Canonero(5, 2),
            new Destructor([(9, 0), (8, 0), (7, 0)]),
            new Destructor([(5, 7), (4, 7), (3, 7)]),
            new Portaaviones([(9, 9), (8, 9), (7, 9), (6, 9)])
        };
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, tripulacionJugadorDos);
        _juego.Disparar(9, 9);
        _juego.Disparar(8, 9);
        _juego.Disparar(7, 9);

        var mensaje = _juego.Disparar(6, 9);

        mensaje.Should().Be("Barco hundido");
    }

    [Fact]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_AgregoUnPrimerJugadorConNombrePepitoEInicioJuego_Debe_ArrojarExcepcion()
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepito");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        Action result = () => _juego.Iniciar(new List<Barco> { new Canonero(-11, -11) }, new List<Barco>());

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Barco fuera del limite de la plataforma");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_AgregoUnPrimerJugadorConNombrePepitoYSegundoJugadorConNombreMariaEInicioJuego_Debe_ArrojarExcepcion(
        DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepito");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        var tripulacionJugadorDos = new List<Barco> { new Canonero(-11, -11) };

        Action result = () => _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, tripulacionJugadorDos);

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Barco fuera del limite de la plataforma");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresIniciaJuegoYFinalizoTurnoYElJugadorUnoTieneUnCanoneroEnLaPosicion2_0YElJugadorDosDisparaEnLaPosicion2_0_Mensaje_Debe_SerBarcoHundido(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.FinalizarTurno();

        var mensaje = _juego.Disparar(2, 0);

        mensaje.Should().Be("Barco hundido");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresIniciaJuegoYFinalizoTurnoYElJugadorUnoTieneUnCanoneroEnLaPossicion2_1YElJugadorDosDisparaEnLaPosicion2_1_Mensaje_Debe_SerBarcoHundido(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.FinalizarTurno();

        var mensaje = _juego.Disparar(2, 1);

        mensaje.Should().Be("Barco hundido");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresIniciaJuegoYFinalizoTurnoYElJugadorUnoTieneUnDestructorEnLaPosicion_3_1_3_2_3_3_YElJugadorDosDisparaEnLaPosicion_3_1_3_2_3_3_Mensaje_Debe_SerBarcoHundido(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.FinalizarTurno();
        _juego.Disparar(3, 1);
        _juego.Disparar(3, 2);

        var mensaje = _juego.Disparar(3, 3);

        mensaje.Should().Be("Barco hundido");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresIniciaJuegoYFinalizoTurnoYElJugadorUnoTieneUnDestructorEnLaPosicion_4_1_4_2_4_3YElJugadorDosDisparaTodasLasPosicionesMensaje_Debe_SerBarcoHundido(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.FinalizarTurno();
        _juego.Disparar(4, 1);
        _juego.Disparar(4, 2);

        var mensaje = _juego.Disparar(4, 3);

        mensaje.Should().Be("Barco hundido");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresIniciaJuegoYFinalizoTurnoYElJugadorUnoTieneUnPortaAvionesEnLaPosicion_0_0_0_1_0_2_0_3_YElJugadorDosDisparaTodasLasPosicionesMensaje_Debe_SerBarcoHundido(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.FinalizarTurno();
        _juego.Disparar(0, 0);
        _juego.Disparar(0, 1);
        _juego.Disparar(0, 2);

        var mensaje = _juego.Disparar(0, 3);

        mensaje.Should().Be("Barco hundido");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_AgregoDosJugadoresIniciaJuegoYSeImprime_Debe_MostrarTableroEsperadoJugadorDos(
        DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);

        var tableroEsperado = @"
 |0|1|2|3|4|5|6|7|8|9|
0|g| | | | | | |d|d|d|
1|g| | | | |g| | | | |
2| | | | | |g| | | | |
3| |c| | | | | | | | |
4| |c| | | | | | | | |
5| |c| | | | | | | | |
6| |c| | | | | | | | |
7| | | |d|d|d| | | | |
8| | | | | | | | | | |
9| | | | | | | | | | |
";

        string tablero = _juego.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresIniciaJuegoYDisparaEn0_0YSeImprime_Debe_MostrarTableroConUnaXEnLaCoordenadaDisparada(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.Disparar(0, 0);

        var tableroEsperado = @"
 |0|1|2|3|4|5|6|7|8|9|
0|X| | | | | | |d|d|d|
1|g| | | | |g| | | | |
2| | | | | |g| | | | |
3| |c| | | | | | | | |
4| |c| | | | | | | | |
5| |c| | | | | | | | |
6| |c| | | | | | | | |
7| | | |d|d|d| | | | |
8| | | | | | | | | | |
9| | | | | | | | | | |
";

        string tablero = _juego.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresIniciaJuegoYDisparaEn_0_0_Y_5_1YSeImprime_Debe_MostrarTableroConUnaXEnLaCoordenadaDisparada(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.Disparar(0, 0);
        _juego.Disparar(5, 1);

        var tableroEsperado = @"
 |0|1|2|3|4|5|6|7|8|9|
0|X| | | | | | |d|d|d|
1|g| | | | |X| | | | |
2| | | | | |g| | | | |
3| |c| | | | | | | | |
4| |c| | | | | | | | |
5| |c| | | | | | | | |
6| |c| | | | | | | | |
7| | | |d|d|d| | | | |
8| | | | | | | | | | |
9| | | | | | | | | | |
";

        string tablero = _juego.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresIniciaJuegoYDisparoATodosLosCanonerosYSeImprime_Debe_MostrarTableroConUnaXEnLaCoordenadaDisparadas(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.Disparar(0, 0);
        _juego.Disparar(0, 1);
        _juego.Disparar(5, 1);
        _juego.Disparar(5, 2);

        var tableroEsperado = @"
 |0|1|2|3|4|5|6|7|8|9|
0|X| | | | | | |d|d|d|
1|X| | | | |X| | | | |
2| | | | | |X| | | | |
3| |c| | | | | | | | |
4| |c| | | | | | | | |
5| |c| | | | | | | | |
6| |c| | | | | | | | |
7| | | |d|d|d| | | | |
8| | | | | | | | | | |
9| | | | | | | | | | |
";

        string tablero = _juego.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresIniciaJuegoYDisparoEn9_0YSeImprime_Debe_MostrarTableroConUnaXEnLaCoordenadaDisparadas(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.Disparar(9, 0);

        var tableroEsperado = @"
 |0|1|2|3|4|5|6|7|8|9|
0|g| | | | | | |d|d|x|
1|g| | | | |g| | | | |
2| | | | | |g| | | | |
3| |c| | | | | | | | |
4| |c| | | | | | | | |
5| |c| | | | | | | | |
6| |c| | | | | | | | |
7| | | |d|d|d| | | | |
8| | | | | | | | | | |
9| | | | | | | | | | |
";

        string tablero = _juego.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresIniciaJuegoYDisparoEn9_0_8_0YSeImprime_Debe_MostrarTableroConUnaXEnLaCoordenadaDisparadas(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.Disparar(9, 0);
        _juego.Disparar(8, 0);

        var tableroEsperado = @"
 |0|1|2|3|4|5|6|7|8|9|
0|g| | | | | | |d|x|x|
1|g| | | | |g| | | | |
2| | | | | |g| | | | |
3| |c| | | | | | | | |
4| |c| | | | | | | | |
5| |c| | | | | | | | |
6| |c| | | | | | | | |
7| | | |d|d|d| | | | |
8| | | | | | | | | | |
9| | | | | | | | | | |
";

        string tablero = _juego.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresIniciaJuegoYDisparoEn9_0_8_0_7_0YSeImprime_Debe_MostrarTableroConUnaXEnLaCoordenadaDisparadas(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.Disparar(9, 0);
        _juego.Disparar(8, 0);
        _juego.Disparar(7, 0);

        var tableroEsperado = @"
 |0|1|2|3|4|5|6|7|8|9|
0|g| | | | | | |X|X|X|
1|g| | | | |g| | | | |
2| | | | | |g| | | | |
3| |c| | | | | | | | |
4| |c| | | | | | | | |
5| |c| | | | | | | | |
6| |c| | | | | | | | |
7| | | |d|d|d| | | | |
8| | | | | | | | | | |
9| | | | | | | | | | |
";

        string tablero = _juego.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresIniciaJuegoYDisparoEn1_3_YSeImprime_Debe_MostrarTableroConUnaxEnLaCoordenadaDisparadas(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.Disparar(1, 3);

        var tableroEsperado = @"
 |0|1|2|3|4|5|6|7|8|9|
0|g| | | | | | |d|d|d|
1|g| | | | |g| | | | |
2| | | | | |g| | | | |
3| |x| | | | | | | | |
4| |c| | | | | | | | |
5| |c| | | | | | | | |
6| |c| | | | | | | | |
7| | | |d|d|d| | | | |
8| | | | | | | | | | |
9| | | | | | | | | | |
";

        string tablero = _juego.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresIniciaJuegoYDisparoEn1_3_1_4_YSeImprime_Debe_MostrarTableroConUnaxEnLaCoordenadaDisparadas(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.Disparar(1, 3);
        _juego.Disparar(1, 4);

        var tableroEsperado = @"
 |0|1|2|3|4|5|6|7|8|9|
0|g| | | | | | |d|d|d|
1|g| | | | |g| | | | |
2| | | | | |g| | | | |
3| |x| | | | | | | | |
4| |x| | | | | | | | |
5| |c| | | | | | | | |
6| |c| | | | | | | | |
7| | | |d|d|d| | | | |
8| | | | | | | | | | |
9| | | | | | | | | | |
";

        string tablero = _juego.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresIniciaJuegoYDisparoEn1_3_1_4_1_5_YSeImprime_Debe_MostrarTableroConUnaxEnLaCoordenadaDisparadas(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.Disparar(1, 3);
        _juego.Disparar(1, 4);
        _juego.Disparar(1, 5);
        
        var tableroEsperado = @"
 |0|1|2|3|4|5|6|7|8|9|
0|g| | | | | | |d|d|d|
1|g| | | | |g| | | | |
2| | | | | |g| | | | |
3| |x| | | | | | | | |
4| |x| | | | | | | | |
5| |x| | | | | | | | |
6| |c| | | | | | | | |
7| | | |d|d|d| | | | |
8| | | | | | | | | | |
9| | | | | | | | | | |
";

        string tablero = _juego.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }


    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresIniciaJuegoYHundoElPortaAvion_SeImprime_Debe_MostrarTableroConUnaXEnLaCoordenadaDisparadas(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.Disparar(1, 3);
        _juego.Disparar(1, 4);
        _juego.Disparar(1, 5);
        _juego.Disparar(1, 6);


        var tableroEsperado = @"
 |0|1|2|3|4|5|6|7|8|9|
0|g| | | | | | |d|d|d|
1|g| | | | |g| | | | |
2| | | | | |g| | | | |
3| |X| | | | | | | | |
4| |X| | | | | | | | |
5| |X| | | | | | | | |
6| |X| | | | | | | | |
7| | | |d|d|d| | | | |
8| | | | | | | | | | |
9| | | | | | | | | | |
";

        string tablero = _juego.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresIniciaJuegoYCambioTurnoYJugadorDosDisparaEn2_0_YSeImprime_Debe_MostrarTableroConUnaXEnLaCoordenadaDisparada(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.FinalizarTurno();

        _juego.Disparar(2, 0);


        var tableroEsperado = @"
 |0|1|2|3|4|5|6|7|8|9|
0|c| |X| | | | | | | |
1|c| |g|d|d| | | | | |
2|c| |g|d|d| | | | | |
3|c| |g|d|d| | | | | |
4| | | | | | | | | | |
5| | | | | | | | | | |
6| | | | | | | | | | |
7| | | | | | | | | | |
8| | | | | | | | | | |
9| | | | | | | | | | |
";

        string tablero = _juego.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_InicioJuegoYJugadorUnoDisparaEnDosTurnosSeparadosAlPortaavion_Debe_MostrarTableroConLosDisparosAcertados(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.Disparar(1, 3);
        _juego.FinalizarTurno();
        _juego.Disparar(0, 0);
        _juego.FinalizarTurno();

        _juego.Disparar(1, 5);

        var tableroEsperado = @"
 |0|1|2|3|4|5|6|7|8|9|
0|g| | | | | | |d|d|d|
1|g| | | | |g| | | | |
2| | | | | |g| | | | |
3| |x| | | | | | | | |
4| |c| | | | | | | | |
5| |x| | | | | | | | |
6| |c| | | | | | | | |
7| | | |d|d|d| | | | |
8| | | | | | | | | | |
9| | | | | | | | | | |
";

        string tablero = _juego.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_InicioJuegoYJugadorUnoDisparaEnLaCordenada8_8__Debe_MostrarTableroConODisparaAlMar(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.Disparar(8, 8);

        var tableroEsperado = @"
 |0|1|2|3|4|5|6|7|8|9|
0|g| | | | | | |d|d|d|
1|g| | | | |g| | | | |
2| | | | | |g| | | | |
3| |c| | | | | | | | |
4| |c| | | | | | | | |
5| |c| | | | | | | | |
6| |c| | | | | | | | |
7| | | |d|d|d| | | | |
8| | | | | | | | |o| |
9| | | | | | | | | | |
";

        string tablero = _juego.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_JugadorUnoHundeTodosLosBarcosDelJugadorDos_Debe_ImprimirElInforme(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);

        _juego.Disparar(0, 0);
        _juego.Disparar(0, 1);
        _juego.Disparar(5, 1);
        _juego.Disparar(5, 2);

        _juego.Disparar(7, 0);
        _juego.Disparar(8, 0);
        _juego.Disparar(9, 0);

        _juego.Disparar(5, 7);
        _juego.Disparar(4, 7);
        _juego.Disparar(3, 7);

        _juego.Disparar(1, 3);
        _juego.Disparar(1, 4);
        _juego.Disparar(1, 5);
        _juego.Disparar(1, 6);

        var tableroEsperado = @"JUGADOR GANADOR
Jugador Pepe
Total disparos: 14
Fallos: 0
Acertados: 14
Barcos Hundidos:
Cañonero: (0,0)
Cañonero: (0,1)
Cañonero: (5,1)
Cañonero: (5,2)
Destructor: (7,0)
Destructor: (3,7)
Portaavion: (1,3)

 |0|1|2|3|4|5|6|7|8|9|
0|X| | | | | | |X|X|X|
1|X| | | | |X| | | | |
2| | | | | |X| | | | |
3| |X| | | | | | | | |
4| |X| | | | | | | | |
5| |X| | | | | | | | |
6| |X| | | | | | | | |
7| | | |X|X|X| | | | |
8| | | | | | | | | | |
9| | | | | | | | | | |

JUGADOR PERDEDOR
Jugador Maria
Total disparos: 0
Fallos: 0
Acertados: 0
Barcos Hundidos:

 |0|1|2|3|4|5|6|7|8|9|
0|c| |g| | | | | | | |
1|c| |g|d|d| | | | | |
2|c| |g|d|d| | | | | |
3|c| |g|d|d| | | | | |
4| | | | | | | | | | |
5| | | | | | | | | | |
6| | | | | | | | | | |
7| | | | | | | | | | |
8| | | | | | | | | | |
9| | | | | | | | | | |
";

        string tablero = _juego.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_JugadorUnoHundeTodosLosBarcosDelJugadorDosYDosTirosAlMar_Debe_ImprimirElInforme(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);

        _juego.Disparar(0, 0);
        _juego.Disparar(0, 1);
        _juego.Disparar(5, 1);
        _juego.Disparar(5, 2);

        _juego.Disparar(7, 0);
        _juego.Disparar(8, 0);
        _juego.Disparar(9, 0);

        _juego.Disparar(5, 7);
        _juego.Disparar(4, 7);
        _juego.Disparar(3, 7);

        _juego.Disparar(6, 6);
        _juego.Disparar(7, 4);

        _juego.Disparar(1, 3);
        _juego.Disparar(1, 4);
        _juego.Disparar(1, 5);
        _juego.Disparar(1, 6);

        var tableroEsperado = @"JUGADOR GANADOR
Jugador Pepe
Total disparos: 16
Fallos: 2
Acertados: 14
Barcos Hundidos:
Cañonero: (0,0)
Cañonero: (0,1)
Cañonero: (5,1)
Cañonero: (5,2)
Destructor: (7,0)
Destructor: (3,7)
Portaavion: (1,3)

 |0|1|2|3|4|5|6|7|8|9|
0|X| | | | | | |X|X|X|
1|X| | | | |X| | | | |
2| | | | | |X| | | | |
3| |X| | | | | | | | |
4| |X| | | | | |o| | |
5| |X| | | | | | | | |
6| |X| | | | |o| | | |
7| | | |X|X|X| | | | |
8| | | | | | | | | | |
9| | | | | | | | | | |

JUGADOR PERDEDOR
Jugador Maria
Total disparos: 0
Fallos: 0
Acertados: 0
Barcos Hundidos:

 |0|1|2|3|4|5|6|7|8|9|
0|c| |g| | | | | | | |
1|c| |g|d|d| | | | | |
2|c| |g|d|d| | | | | |
3|c| |g|d|d| | | | | |
4| | | | | | | | | | |
5| | | | | | | | | | |
6| | | | | | | | | | |
7| | | | | | | | | | |
8| | | | | | | | | | |
9| | | | | | | | | | |
";

        string tablero = _juego.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_JugadorDosHundeTodosLosBarcosDelJugadorUnoYTresTirosAlMar_Debe_ImprimirElInforme(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.Disparar(5, 9);

        _juego.FinalizarTurno();
        _juego.Disparar(2, 0);
        _juego.Disparar(2, 1);
        _juego.Disparar(2, 2);
        _juego.Disparar(2, 3);

        _juego.Disparar(3, 1);
        _juego.Disparar(3, 2);
        _juego.Disparar(3, 3);

        _juego.Disparar(4, 1);
        _juego.Disparar(4, 2);
        _juego.Disparar(4, 3);

        _juego.Disparar(6, 6);
        _juego.Disparar(7, 4);
        _juego.Disparar(8, 8);

        _juego.Disparar(0, 0);
        _juego.Disparar(0, 1);
        _juego.Disparar(0, 2);
        _juego.Disparar(0, 3);


        var tableroEsperado = @"JUGADOR GANADOR
Jugador Maria
Total disparos: 17
Fallos: 3
Acertados: 14
Barcos Hundidos:
Cañonero: (2,0)
Cañonero: (2,1)
Cañonero: (2,2)
Cañonero: (2,3)
Destructor: (3,1)
Destructor: (4,1)
Portaavion: (0,0)

 |0|1|2|3|4|5|6|7|8|9|
0|X| |X| | | | | | | |
1|X| |X|X|X| | | | | |
2|X| |X|X|X| | | | | |
3|X| |X|X|X| | | | | |
4| | | | | | | |o| | |
5| | | | | | | | | | |
6| | | | | | |o| | | |
7| | | | | | | | | | |
8| | | | | | | | |o| |
9| | | | | | | | | | |

JUGADOR PERDEDOR
Jugador Pepe
Total disparos: 1
Fallos: 1
Acertados: 0
Barcos Hundidos:

 |0|1|2|3|4|5|6|7|8|9|
0|g| | | | | | |d|d|d|
1|g| | | | |g| | | | |
2| | | | | |g| | | | |
3| |c| | | | | | | | |
4| |c| | | | | | | | |
5| |c| | | | | | | | |
6| |c| | | | | | | | |
7| | | |d|d|d| | | | |
8| | | | | | | | | | |
9| | | | | |o| | | | |
";

        string tablero = _juego.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_JugadorUnoHundioTodosLosBarcosYTerminaElJuegoYSeDispara_Debe_ArrojarExcepcion(
        DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.Disparar(0, 0);
        _juego.Disparar(0, 1);
        _juego.Disparar(5, 1);
        _juego.Disparar(5, 2);

        _juego.Disparar(7, 0);
        _juego.Disparar(8, 0);
        _juego.Disparar(9, 0);

        _juego.Disparar(5, 7);
        _juego.Disparar(4, 7);
        _juego.Disparar(3, 7);

        _juego.Disparar(1, 3);
        _juego.Disparar(1, 4);
        _juego.Disparar(1, 5);
        _juego.Disparar(1, 6);

        Action result = () => _juego.Disparar(9, 2);

        result.Should().ThrowExactly<Exception>().WithMessage("El juego ya termino");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_JugadorUnoHundioTodosLosBarcosYTerminaElJuegoYSeTerminaElTurnoYJugadorDosDispara_Debe_ArrojarExcepcion(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        _juego.Disparar(0, 0);
        _juego.Disparar(0, 1);
        _juego.Disparar(5, 1);
        _juego.Disparar(5, 2);

        _juego.Disparar(7, 0);
        _juego.Disparar(8, 0);
        _juego.Disparar(9, 0);

        _juego.Disparar(5, 7);
        _juego.Disparar(4, 7);
        _juego.Disparar(3, 7);

        _juego.Disparar(1, 3);
        _juego.Disparar(1, 4);
        _juego.Disparar(1, 5);
        _juego.Disparar(1, 6);

        _juego.FinalizarTurno();

        Action result = () => _juego.Disparar(9, 2);

        result.Should().ThrowExactly<Exception>().WithMessage("El juego ya termino");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresYElJugadorDosTieneDosBarcosnLaMismaCordenadaEInicioJuego_Debe_ArrojarExcepcion(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        var listaBarcosJugadorDos = new List<Barco>
        {
            new Canonero(6, 0),
            new Canonero(6, 0),
            new Canonero(8, 0),
            new Canonero(9, 0),
            new Destructor([(9, 0), (8, 0), (7, 0)]),
            new Destructor([(9, 0), (8, 0), (7, 0)]),
            new Portaaviones([(1, 3), (1, 4), (1, 5), (1, 6)])
        };

        Action result = () => _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, listaBarcosJugadorDos);

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Ya Existe un Barco en la misma posición");
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresYElJugadorUnoTieneDosBarcosEnLaMismaCordenadaEInicioJuego_Debe_ArrojarExcepcion(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        var listaBarcosJugadorUno = new List<Barco>
        {
            new Canonero(2, 0),
            new Canonero(2, 1),
            new Canonero(2, 2),
            new Canonero(2, 2),
            new Destructor([(3, 1), (3, 2), (3, 3)]),
            new Destructor([(4, 1), (4, 2), (4, 3)]),
            new Portaaviones([(0, 0), (0, 1), (0, 2), (0, 3)])
        };

        Action result = () => _juego.Iniciar(listaBarcosJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Ya Existe un Barco en la misma posición");
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_JugadorUnoRealizaUnDisparoYAciertaUnBarco_Debe_MensajeSerDisparoAcertado(DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);
        string resultado = _juego.Disparar(9, 0);

        resultado.Should().Be("Tiro acertado");
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresYElJugadorUnoColocaUnBarcoDestructorEnUnaPosicionDiagonalEInicioJuego_Debe_ArrojarExcepcion(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        var listaBarcosJugadorUno = new List<Barco>
        {
            new Canonero(2, 0),
            new Canonero(2, 1),
            new Canonero(2, 2),
            new Canonero(2, 3),
            new Destructor([(0, 9), (1, 8), (2, 7)]),
            new Destructor([(4, 1), (4, 2), (4, 3)]),
            new Portaaviones([(0, 0), (0, 1), (0, 2), (0, 3)])
        };

        Action result = () => _juego.Iniciar(listaBarcosJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Solo se pueden colocar barcos en posiciones horizontales o verticales");
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresYElJugadorUnoColocaUnBarcoPortaAvionesEnUnaPosicionDiagonalEInicioJuego_Debe_ArrojarExcepcion(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        var listaBarcosJugadorUno = new List<Barco>
        {
            new Canonero(2, 0),
            new Canonero(2, 1),
            new Canonero(2, 2),
            new Canonero(2, 3),
            new Destructor([(3, 1), (3, 2), (3, 3)]),
            new Destructor([(4, 1), (4, 2), (4, 3)]),
            new Portaaviones([(0, 9), (1, 8), (2, 7), (3, 6)])
        };

        Action result = () => _juego.Iniciar(listaBarcosJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Solo se pueden colocar barcos en posiciones horizontales o verticales");
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresYElJugadorDosColocaUnBarcoDestructorEnUnaPosicionDiagonalEIniciaJuego_Debe_ArrojarExcepcion(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        var tripulacionJugadorDos = new List<Barco>
        {
            new Canonero(0, 0),
            new Canonero(0, 1),
            new Canonero(5, 1),
            new Canonero(5, 2),
            new Destructor([(9, 0), (8, 0), (7, 0)]),
            new Destructor([(0, 9), (1, 8), (2, 7)]),
            new Portaaviones([(1, 3), (1, 4), (1, 5), (1, 6)])
        };

        Action result = () => _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, tripulacionJugadorDos );

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Solo se pueden colocar barcos en posiciones horizontales o verticales");
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresYElJugadorDosColocaUnBarcoPortaAvionesEnUnaPosicionDiagonalEIniciaJuego_Debe_ArrojarExcepcion(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        var tripulacionJugadorDos = new List<Barco>
        {
            new Canonero(0, 0),
            new Canonero(0, 1),
            new Canonero(5, 1),
            new Canonero(5, 2),
            new Destructor([(9, 0), (8, 0), (7, 0)]),
            new Destructor([(5, 7), (4, 7), (3, 7)]),
            new Portaaviones([(1, 3), (2, 4), (3, 5), (4, 6)])
        };

        Action result = () => _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, tripulacionJugadorDos );

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Solo se pueden colocar barcos en posiciones horizontales o verticales");
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresYJugadorDosTieneDosBarcosConUnaCoordenadaRepetidaEInicioJuego_Debe_ArrojarExcepcion(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        var listaBarcosJugadorDos = new List<Barco>
        {
            new Canonero(1, 0),
            new Canonero(2, 0),
            new Canonero(3, 0),
            new Canonero(4, 0),
            new Destructor([(9, 1), (8, 1), (7, 1)]),
            new Destructor([(9, 1), (8, 1), (7, 0)]),
            new Portaaviones([(1, 3), (1, 4), (1, 5), (1, 6)])
        };

        Action result = () => _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, listaBarcosJugadorDos);

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Ya Existe un Barco en la misma posición");
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_SeIniciaUnJuegoSinElJugadorDos_Debe_ArrojarExcepcion(DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        
        Action result = () => _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Debe agregar al jugador dos");
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_SeIniciaUnJuegoSinElJugadorUno_Debe_ArrojarExcepcion(DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        
        Action result = () => _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Debe agregar al jugador uno");
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_SeIniciaPartidaYSeDisparaFueraDelTablero_Debe_ArrojarExcepcion(DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);

        Action result = () => _juego.Disparar(20, 20);

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Disparo fuera del tablero");
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_JugadorUnoHundeTodosLosBarcosDelJugadorDos_Debe_ImprimirElInformeConEstadisticasDeAmbosJugadores(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");
        _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);

        _juego.Disparar(0, 0);
        _juego.Disparar(0, 1);
        _juego.Disparar(5, 1);
        _juego.Disparar(5, 2);
        
        _juego.FinalizarTurno();
        _juego.Disparar(7, 9);
        
        _juego.FinalizarTurno();
        _juego.Disparar(7, 0);
        _juego.Disparar(8, 0);
        _juego.Disparar(9, 0);

        _juego.Disparar(5, 7);
        _juego.Disparar(4, 7);
        _juego.Disparar(3, 7);

        _juego.Disparar(3, 3);
        
        _juego.Disparar(1, 3);
        _juego.Disparar(1, 4);
        _juego.Disparar(1, 5);
        _juego.Disparar(1, 6);

        var tableroEsperado = @"JUGADOR GANADOR
Jugador Pepe
Total disparos: 15
Fallos: 1
Acertados: 14
Barcos Hundidos:
Cañonero: (0,0)
Cañonero: (0,1)
Cañonero: (5,1)
Cañonero: (5,2)
Destructor: (7,0)
Destructor: (3,7)
Portaavion: (1,3)

 |0|1|2|3|4|5|6|7|8|9|
0|X| | | | | | |X|X|X|
1|X| | | | |X| | | | |
2| | | | | |X| | | | |
3| |X| |o| | | | | | |
4| |X| | | | | | | | |
5| |X| | | | | | | | |
6| |X| | | | | | | | |
7| | | |X|X|X| | | | |
8| | | | | | | | | | |
9| | | | | | | | | | |

JUGADOR PERDEDOR
Jugador Maria
Total disparos: 1
Fallos: 1
Acertados: 0
Barcos Hundidos:

 |0|1|2|3|4|5|6|7|8|9|
0|c| |g| | | | | | | |
1|c| |g|d|d| | | | | |
2|c| |g|d|d| | | | | |
3|c| |g|d|d| | | | | |
4| | | | | | | | | | |
5| | | | | | | | | | |
6| | | | | | | | | | |
7| | | | | | | | | | |
8| | | | | | | | | | |
9| | | | | | | |o| | |
";

        string tablero = _juego.Imprimir();

        tablero.Should().Be(tableroEsperado);
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresYJugadorDosTieneUnBarcoConUnaCoordenadaConSaltosEInicioJuego_Debe_ArrojarExcepcion(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        var listaBarcosJugadorDos = new List<Barco>
        {
            new Canonero(8, 0),
            new Canonero(9, 0),
            new Canonero(7, 0),
            new Canonero(6, 0),
            new Destructor([(9, 1), (7, 1), (6, 1)]),
            new Destructor([(9, 2), (8, 2), (7, 2)]),
            new Portaaviones([(1, 3), (1, 5), (1, 6), (1, 7)])
        };

        Action result = () => _juego.Iniciar(datosTripulacionJugadores.tripulacionJugadorUno, listaBarcosJugadorDos);

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Las coordenadas de un barco deben ser consecutivas");
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoDosJugadoresYJugadorUnoTieneUnBarcoConUnaCoordenadaConSaltosEInicioJuego_Debe_ArrojarExcepcion(
            DatosTripulacionJugadores datosTripulacionJugadores)
    {
        _juego.AgregarJugador(TipoJugador.Uno, "Pepe");
        _juego.AgregarJugador(TipoJugador.Dos, "Maria");

        var listaBarcosJugadorUno = new List<Barco>
        {
            new Canonero(8, 0),
            new Canonero(9, 0),
            new Canonero(7, 0),
            new Canonero(6, 0),
            new Destructor([(9, 1), (8, 1), (7, 1)]),
            new Destructor([(9, 2), (6, 2), (5, 2)]),
            new Portaaviones([(1, 3), (1, 5), (1, 6), (1, 7)])
        };

        Action result = () => _juego.Iniciar(listaBarcosJugadorUno, datosTripulacionJugadores.tripulacionJugadorDos);

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Las coordenadas de un barco deben ser consecutivas");
    }
}
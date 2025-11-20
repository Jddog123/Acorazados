using System.Runtime.InteropServices;
using Acorazados.ClassData;
using Acorazados.Dominio;
using Acorazados.Dominio.Barcos;
using Acorazados.Dominio.Enums;
using FluentAssertions;

namespace Acorazados;

public class AcorazadosTest
{
    [Theory]
    [InlineData(0,10)]
    [InlineData(10,0)]
    [InlineData(11,11)]
    [InlineData(-1,-1)]

    public void Si_AgregoJugadorUnoYUnBarcoCañoneroEstaFueraDelLimiteEInicioJuego_Debe_ArrojarExcepcion(int coordenadaX, int coordenadaY)
    {
        var juego = new Juego();
        var listaBarcos = new List<Barco> { new Canonero(coordenadaX,coordenadaY) };

        juego.AgregarJugador(listaBarcos , TipoJugador.Uno);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>("Barco fuera del limite de la plataforma");
    }

    [Fact]
    public void Si_AgregoUnJugadorConUnDestructorEnLaPosicion0_8_0_9_0_10EinicioJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<Barco> { new Destructor([ (0,8) , (0,9), (0,10)]) };

        juego.AgregarJugador(listaBarcos , TipoJugador.Uno);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>("Barco fuera del limite de la plataforma");
    }

    [Fact]
    public void
        Si_AgregoUnJugadorConDosDestructorEnLaPosicion_0_8_0_9_0_10_Y_8_0_9_0_10_0_EInicioJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<Barco>
        {
            new Destructor([ (0,7) , (0,8), (0,9)]),
            new Destructor([ (8,0) , (9,0), (10,0)])
        };
        
        juego.AgregarJugador(listaBarcos , TipoJugador.Uno);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Barco fuera del limite de la plataforma");
    }

    
    [Fact]
    public void Si_CreoUnCanoneroSinCoordenadas_Debe_ArrojarExcepcion()
    {
        Action result = () => new List<Barco> { new Canonero() };

        result.Should().ThrowExactly<ArgumentException>().WithMessage("El tipo de barco cañonero necesita las coordenadas");
    }

    [Fact]
    public void Si_CreoUnDestructorConDosCoordenadasDebe_ArrojarExcepcion()
    {
        Action result = () => new List<Barco> { new Destructor([ (0,8) , (0,9)]) };

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("El tipo de barco destructor es de tres coordenadas");
    }

    [Fact]
    public void Si_CreoUnDestructorConCuatroCoordenadasDebe_ArrojarExcepcion()
    {
        Action result = () => new List<Barco> { new Destructor([(0, 6), (0, 7), (0,8) , (0,9)]) };

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("El tipo de barco destructor es de tres coordenadas");
    }

    [Fact]
    public void Si_AgregoUnJugadorConUnPortaavionesConTresCoordenadasDebe_ArrojarExcepcion()
    {
        var juego = new Juego();
        
        Action result = () => new List<Barco> { new Portaaviones([(0, 6), (0, 7), (0, 8)]) };

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("El tipo de barco portaaviones es de cuatro coordenadas");
    }

    [Fact]
    public void Si_CreoUnPortaavionesConCincoCoordenadasDebe_ArrojarExcepcion()
    {
        var juego = new Juego();
        
        Action result = () => new List<Barco> { new Portaaviones([(0, 0), (0, 1), (0, 2) , (0,3) , (0,4)]) };

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("El tipo de barco portaaviones es de cuatro coordenadas");
    }

    [Fact]
    public void Si_AgregoUnJugadorConTresBarcosCañonerosEInicioElJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<Barco>
        {
            new Canonero(0, 0),
            new Canonero(0, 1),
            new Canonero(0,2)
        };

        juego.AgregarJugador(listaBarcos , TipoJugador.Uno);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Deben existir 4 cañoreros en la plataforma");
    }

    [Fact]
    public void Si_AgregoUnJugadorConCincoBarcosCañonerosEInicioElJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<Barco>
        {
            new Canonero(0, 0),
            new Canonero(0, 1),
            new Canonero(0,2),
            new Canonero(0,3),
            new Canonero(0,4)
        };

        juego.AgregarJugador(listaBarcos , TipoJugador.Uno);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Deben existir 4 cañoreros en la plataforma");
    }

    [Fact]
    public void Si_AgregoUnJugadorCon4CañoerosYUnDestructorEInicioElJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<Barco>
        {
            new Canonero(0, 0),
            new Canonero(0, 1),
            new Canonero(0,2),
            new Canonero(0,3),
            new Destructor([(1, 0), (1, 2), (1, 3)])
        };

        juego.AgregarJugador(listaBarcos , TipoJugador.Uno);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Deben existir 2 destructores en la plataforma");
    }

    [Fact]
    public void Si_AgregoUnJugadorCon4CañoerosYTresDestructorEInicioElJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<Barco>
        {
            new Canonero(0, 0),
            new Canonero(0, 1),
            new Canonero(0,2),
            new Canonero(0,3),
            new Destructor([(1, 0), (1, 1), (1, 2)]),
            new Destructor([(2, 0), (2, 1), (2, 2)]),
            new Destructor([(3, 0), (3, 1), (3, 2)])
        };

        juego.AgregarJugador(listaBarcos , TipoJugador.Uno);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Deben existir 2 destructores en la plataforma");
    }

    [Fact]
    public void Si_AgregoUnJugadorCon4CañoerosDosDestructorYNingunPortaavionEInicioElJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<Barco>
        {
            new Canonero(0, 0),
            new Canonero(0, 1),
            new Canonero(0,2),
            new Canonero(0,3),
            new Destructor([(1, 0), (1, 1), (1, 2)]),
            new Destructor([(2, 0), (2, 1), (2, 2)])
        };

        juego.AgregarJugador(listaBarcos , TipoJugador.Uno);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Deben existir 1 portaavion en la plataforma");
    }

    [Fact]
    public void Si_AgregoUnJugadorCon4CañoerosDosDestructorYDosPortaavionEInicioElJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        var listaBarcos = new List<Barco>
        {
            new Canonero(0, 0),
            new Canonero(0, 1),
            new Canonero(0,2),
            new Canonero(0,3),
            new Destructor([(1, 0), (1, 1), (1, 2)]),
            new Destructor([(2, 0), (2, 1), (2, 2)]),
            new Portaaviones([(3, 0), (3, 1), (3, 2),(3, 3)]),
            new Portaaviones([(4, 0), (4, 1), (4, 2),(4, 3)]),
        };
        
        juego.AgregarJugador(listaBarcos , TipoJugador.Uno);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Deben existir 1 portaavion en la plataforma");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_AgregoUnSegundoJugadorConUnBarcoFueraDeLaPlataformaEInicioJuego_Debe_ArrojarExcepcion(DatosTripulacionJugadores datosTripulacionJugadores)
    {
        var juego = new Juego();
        juego.AgregarJugador(datosTripulacionJugadores.tripulacionJugadorUno , TipoJugador.Uno);
        
        var listaBarcosJugadorDos = new List<Barco> { new Canonero(10,10) };
        juego.AgregarJugador(listaBarcosJugadorDos , TipoJugador.Dos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Barco fuera del limite de la plataforma");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void
        Si_AgregoUnSegundoJugadorConCantidadDeBarcosCañonerosDiferentesACuatroEInicioJuego_Debe_ArrojarExcepcion(DatosTripulacionJugadores datosTripulacionJugadores)
    {
        var juego = new Juego();
        juego.AgregarJugador(datosTripulacionJugadores.tripulacionJugadorUno , TipoJugador.Uno);

        var listaBarcosJugadorDos = new List<Barco>
        {
            new Canonero(6,0),
            new Canonero(7,0),
            new Canonero(8,0)
        };
        juego.AgregarJugador(listaBarcosJugadorDos , TipoJugador.Dos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Deben existir 4 cañoreros en la plataforma");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_AgregoUnSegundoJugadorConCantidadDeBarcosDestructoresDiferentesA2EInicioJuego_Debe_ArrojarExcepcion(DatosTripulacionJugadores datosTripulacionJugadores)
    {
        var juego = new Juego();
        juego.AgregarJugador(datosTripulacionJugadores.tripulacionJugadorUno , TipoJugador.Uno);

        var listaBarcosJugadorDos = new List<Barco>
        {
            new Canonero(6,0),
            new Canonero(7,0),
            new Canonero(8,0),
            new Canonero(9,0),
            new Destructor([(8,1),(8,2),(8,3)])
        };
        juego.AgregarJugador(listaBarcosJugadorDos , TipoJugador.Dos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Deben existir 2 destructores en la plataforma");
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_AgregoUnSegundoJugadorConCantidadDeBarcosPortaavionesDiferentesA1EInicioJuego_Debe_ArrojarExcepcion(DatosTripulacionJugadores datosTripulacionJugadores)
    {
        var juego = new Juego();
        juego.AgregarJugador(datosTripulacionJugadores.tripulacionJugadorUno , TipoJugador.Uno);

        var listaBarcosJugadorDos = new List<Barco>
        {
            new Canonero(6,0),
            new Canonero(7,0),
            new Canonero(8,0),
            new Canonero(9,0),
            new Destructor([(8,1),(8,2),(8,3)]),
            new Destructor([(9,1),(9,2),(9,3)])
        };
        juego.AgregarJugador(listaBarcosJugadorDos , TipoJugador.Dos);

        Action result = () => juego.Iniciar();

        result.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Deben existir 1 portaavion en la plataforma");
    }

    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_AgregoDosJugadoresYElJugadorDosTieneUnCanoneroEnLaPosicion0_0YElJugadorUnoDisparaEnLaPosicion0_0_Mensaje_Debe_SerBarcoHundido(DatosTripulacionJugadores datosTripulacionJugadores)
    {
        var juego = new Juego();
        juego.AgregarJugador(datosTripulacionJugadores.tripulacionJugadorUno , TipoJugador.Uno);
        juego.AgregarJugador(datosTripulacionJugadores.tripulacionJugadorDos , TipoJugador.Dos);
        juego.Iniciar();
       
        var mensaje = juego.Disparar(0,0);

        mensaje.Should().Be("Barco hundido");
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_AgregoDosJugadoresYElJugadorDosTieneUnCanoneroEnLaPosicion0_1YElJugadorUnoDisparaEnLaPosicion0_1_Mensaje_Debe_SerBarcoHundido(DatosTripulacionJugadores datosTripulacionJugadores)
    {
        var juego = new Juego();
        
        juego.AgregarJugador(datosTripulacionJugadores.tripulacionJugadorUno , TipoJugador.Uno);
        juego.AgregarJugador(datosTripulacionJugadores.tripulacionJugadorDos , TipoJugador.Dos);
        juego.Iniciar();
       
        var mensaje = juego.Disparar(0,1);

        mensaje.Should().Be("Barco hundido");
    }
    
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_AgregoDosJugadoresYElJugadorDosTieneUnCanoneroEnLaPosicion5_1YElJugadorUnoDisparaEnLaPosicion5_1_Mensaje_Debe_SerBarcoHundido(DatosTripulacionJugadores datosTripulacionJugadores)
    {
        var juego = new Juego();
        
        juego.AgregarJugador(datosTripulacionJugadores.tripulacionJugadorUno , TipoJugador.Uno);
        juego.AgregarJugador(datosTripulacionJugadores.tripulacionJugadorDos , TipoJugador.Dos);
        juego.Iniciar();
       
        var mensaje = juego.Disparar(5,1);

        mensaje.Should().Be("Barco hundido");
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_AgregoDosJugadoresYElJugadorDosTieneUnCanoneroEnLaPosicion5_2YElJugadorUnoDisparaEnLaPosicion5_2_Mensaje_Debe_SerBarcoHundido(DatosTripulacionJugadores datosTripulacionJugadores)
    {
        var juego = new Juego();
        
        juego.AgregarJugador(datosTripulacionJugadores.tripulacionJugadorUno , TipoJugador.Uno);
        juego.AgregarJugador(datosTripulacionJugadores.tripulacionJugadorDos , TipoJugador.Dos);
        juego.Iniciar();
       
        var mensaje = juego.Disparar(5,2);

        mensaje.Should().Be("Barco hundido");
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_AgregoDosJugadoresYElJugadorDosTieneUnDestructorEn9_0_8_0Y7_0YElJugadorUnoDisparaTodasLasCoordenadasElMensaje_Debe_SerBarcoHundido(DatosTripulacionJugadores datosTripulacionJugadores)
    {
        var juego = new Juego();
        
        juego.AgregarJugador(datosTripulacionJugadores.tripulacionJugadorUno , TipoJugador.Uno);
        juego.AgregarJugador(datosTripulacionJugadores.tripulacionJugadorDos , TipoJugador.Dos);
        juego.Iniciar();
        juego.Disparar(9,0);
        juego.Disparar(8,0);
        
        var mensaje = juego.Disparar(7,0);

        mensaje.Should().Be("Barco hundido");
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_AgregoDosJugadoresYElJugadorDosTieneUnDestructorEn5_7_4_7_Y_3_7_0YElJugadorUnoDisparaTodasLasCoordenadasElMensaje_Debe_SerBarcoHundido(DatosTripulacionJugadores datosTripulacionJugadores)
    {
        var juego = new Juego();
        
        juego.AgregarJugador(datosTripulacionJugadores.tripulacionJugadorUno , TipoJugador.Uno);
        juego.AgregarJugador(datosTripulacionJugadores.tripulacionJugadorDos , TipoJugador.Dos);
        juego.Iniciar();
        juego.Disparar(3,7);
        juego.Disparar(4,7);
        
        var mensaje = juego.Disparar(5,7);

        mensaje.Should().Be("Barco hundido");
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_AgregoDosJugadoresYElJugadorDosTieneUnPortaavionEn1_3_1_4_1_5Y_1_6YElJugadorUnoDisparaTodasLasCoordenadasElMensaje_Debe_SerBarcoHundido(DatosTripulacionJugadores datosTripulacionJugadores)
    {
        var juego = new Juego();
        
        juego.AgregarJugador(datosTripulacionJugadores.tripulacionJugadorUno , TipoJugador.Uno);
        juego.AgregarJugador(datosTripulacionJugadores.tripulacionJugadorDos , TipoJugador.Dos);
        juego.Iniciar();
        juego.Disparar(1,3);
        juego.Disparar(1,4);
        juego.Disparar(1,5);
        
        var mensaje = juego.Disparar(1,6);

        mensaje.Should().Be("Barco hundido");
    }
    
    [Theory]
    [ClassData(typeof(DatosTripulacionJugadoresClassData))]
    public void Si_AgregoDosJugadoresYElJugadorDosTieneUnPortaavionEn9_9_8_9_7_9Y_6_9YElJugadorUnoDisparaTodasLasCoordenadasElMensaje_Debe_SerBarcoHundido(DatosTripulacionJugadores datosTripulacionJugadores)
    {
        var juego = new Juego();
        juego.AgregarJugador(datosTripulacionJugadores.tripulacionJugadorUno , TipoJugador.Uno);
        
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
        juego.AgregarJugador(tripulacionJugadorDos , TipoJugador.Dos);
        juego.Iniciar();
        juego.Disparar(9,9);
        juego.Disparar(8,9);
        juego.Disparar(7,9);
        
        var mensaje = juego.Disparar(6,9);

        mensaje.Should().Be("Barco hundido");
    }

    [Fact]
    public void Si_AgregoUnPrimerJugadorConNombrePepitoEInicioJuego_Debe_ArrojarExcepcion()
    {
        var juego = new Juego();
        juego.AgregarJugador(TipoJugador.Uno,"Pepito");

        Action result = () => juego.Iniciar(new List<Barco> { new Canonero(-11,-11) });

        result.Should().ThrowExactly<ArgumentException>().WithMessage("Barco fuera del limite de la plataforma");
    }
}
using Acorazados.Dominio;
using Acorazados.Dominio.Barcos;
using Acorazados.Dominio.Enums;

Console.WriteLine("¡BIENVENIDO!");
string tablero;

var juego = new Juego();

var ListaBarcosJugadorUno = new List<Barco>
{
    new Canonero(2, 0),
    new Canonero(2, 1),
    new Canonero(2, 2),
    new Canonero(2, 3),
    new Destructor([(3, 1), (3, 2), (3, 3)]),
    new Destructor([(4, 1), (4, 2), (4, 3)]),
    new Portaaviones([(0, 0), (0, 1), (0, 2), (0, 3)])
};

var ListaBarcosJugadorDos = new List<Barco>
{
    new Canonero(0, 0),
    new Canonero(0, 1),
    new Canonero(5, 1),
    new Canonero(5, 2),
    new Destructor([(9, 0), (8, 0), (7, 0)]),
    new Destructor([(5, 7), (4, 7), (3, 7)]),
    new Portaaviones([(1, 3), (1, 4), (1, 5), (1, 6)])
};

Console.WriteLine("Por favor, ingresa el nombre del primer jugador:");
string nombrePrimerUsuario = Console.ReadLine();

Console.WriteLine("Por favor, ingresa el nombre del segundo jugador:");
string nombreSegundoUsuario = Console.ReadLine();
    
juego.AgregarJugador(TipoJugador.Uno, nombrePrimerUsuario);
juego.AgregarJugador(TipoJugador.Dos, nombreSegundoUsuario);

Console.WriteLine("Ingresa un comando (INICIAR) y presiona Enter:");
string comando = Console.ReadLine();

while (comando != "INICIAR")
{
    Console.WriteLine("Ingresa un comando (INICIAR) y presiona Enter:");
    comando = Console.ReadLine();
}

juego.Iniciar(ListaBarcosJugadorUno, ListaBarcosJugadorDos);
comando = string.Empty;

bool juegoTerminado = false;

while (!juegoTerminado)
{

    while (comando != "DISPARAR")
    {
        Console.WriteLine("Ingresa un comando (DISPARAR) y presiona Enter:");
        comando = Console.ReadLine();
    }
    comando = string.Empty;
    
    Console.WriteLine("Por favor, ingresa coordenada en X a disparar:");
    int coordenadaXDisparo = Convert.ToInt32(Console.ReadLine());
    
    Console.WriteLine("Por favor, ingresa coordenada en Y a disparar:");
    int coordenadaYDisparo = Convert.ToInt32(Console.ReadLine());
            
    juego.Disparar(coordenadaXDisparo, coordenadaYDisparo);
    
    tablero = juego.Imprimir();
    Console.WriteLine("--- TABLERO DE JUEGO ---");
    Console.WriteLine(tablero);
    Console.WriteLine("------------------------");
    
    while (comando != "SIGUIENTE TURNO")
    {
        Console.WriteLine("Ingresa un comando (SIGUIENTE TURNO) y presiona Enter:");
        comando = Console.ReadLine();
    }

    try
    {
        juego.FinalizarTurno();
    }
    catch (Exception e)
    {
        juegoTerminado = true;
    }
}

Console.WriteLine("JUEGO TERMINADO MUCHAS GRACIAS");

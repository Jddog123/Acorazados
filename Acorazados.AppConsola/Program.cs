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

Console.WriteLine("Por favor, especifica el modo de juego :");
Console.WriteLine("DISPARO X TURNO = Se cambiara el turno acierte o falle");
Console.WriteLine("RACHAS DE ACIERTOS = Si el jugador acierta puede seguir disparando hasta que falle");
string modoJuego = Console.ReadLine();
while (modoJuego != "DISPARO X TURNO" && modoJuego != "RACHAS DE ACIERTOS")
{
    Console.WriteLine("Por favor, especifica el modo de juego :");
    Console.WriteLine("DISPARO X TURNO = Se cambiara el turno acierte o falle");
    Console.WriteLine("RACHAS DE ACIERTOS = Si el jugador acierta puede seguir disparando hasta que falle");
    modoJuego = Console.ReadLine();
}

Console.WriteLine("Ingresa un comando (INICIAR) y presiona Enter:");
string comando = Console.ReadLine();

while (comando != "INICIAR")
{
    Console.WriteLine("Ingresa un comando (INICIAR) y presiona Enter:");
    comando = Console.ReadLine();
}

juego.Iniciar(ListaBarcosJugadorUno, ListaBarcosJugadorDos);

tablero = juego.Imprimir();
Console.WriteLine($"--- TABLERO DE JUEGO DEL JUGADOR {nombreSegundoUsuario}---");
Console.WriteLine(tablero);
Console.WriteLine("------------------------");
juego.FinalizarTurno();

tablero = juego.Imprimir();
Console.WriteLine($"--- TABLERO DE JUEGO DEL JUGADOR {nombrePrimerUsuario}---");
Console.WriteLine(tablero);
Console.WriteLine("------------------------");
juego.FinalizarTurno();

comando = string.Empty;

bool juegoTerminado = false;
string jugadorTurnoActual = nombrePrimerUsuario;
string resultadoDisparo = string.Empty;

while (!juegoTerminado)
{
    Console.WriteLine($"TURNO JUGADOR: {jugadorTurnoActual}");
    
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
            
    try
    {
        resultadoDisparo = juego.Disparar(coordenadaXDisparo, coordenadaYDisparo);

        Console.WriteLine(string.IsNullOrEmpty(resultadoDisparo)? "Fallaste": resultadoDisparo);
    }
    catch (Exception ex)
    {
        juegoTerminado = true;
    }
    
    tablero = juego.Imprimir();
    
    if(juegoTerminado == false) 
        Console.WriteLine("--- TABLERO DE JUEGO DEL ENEMIGO---");
    
    Console.WriteLine(tablero);
    Console.WriteLine("------------------------");

    if (juegoTerminado == false && (modoJuego.Equals("DISPARO X TURNO") || (modoJuego.Equals("RACHAS DE ACIERTOS") && string.IsNullOrEmpty(resultadoDisparo))))
    {
        while (comando != "SIGUIENTE TURNO")
        {
            Console.WriteLine("Ingresa un comando (SIGUIENTE TURNO) y presiona Enter:");
            comando = Console.ReadLine();
        }
        
        try
        {
            juego.FinalizarTurno();
            jugadorTurnoActual = jugadorTurnoActual.Equals(nombrePrimerUsuario)? nombreSegundoUsuario : nombrePrimerUsuario;
        }
        catch (Exception ex)
        {
            juegoTerminado = true;
        }
    } 
}

Console.WriteLine("------------------------");
Console.WriteLine("JUEGO TERMINADO MUCHAS GRACIAS");
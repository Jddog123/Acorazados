using Acorazados.Dominio;
using Acorazados.Dominio.Barcos;
using Acorazados.Dominio.Enums;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<Acorazados.Dominio.Juego>(); // inyectamos la clase Juego
var app = builder.Build();
var juego = new Juego();

app.UseStaticFiles();

// Inicializar juego
app.MapPost("/api/iniciar", (Acorazados.Dominio.Juego juego, JugadoresRequest req) =>
{
    juego.AgregarJugador(TipoJugador.Uno, req.Jugador1);
    juego.AgregarJugador(TipoJugador.Dos, req.Jugador2);

    // colocar barcos
    var listaJ1 = new List<Barco> { new Canonero(2,0), new Canonero(2,1), new Canonero(2,2), new Canonero(2,3), new Destructor(new List<(int,int)>{(3,1),(3,2),(3,3)}), new Destructor(new List<(int,int)>{(4,1),(4,2),(4,3)}), new Portaaviones(new List<(int,int)>{(0,0),(0,1),(0,2),(0,3)}) };
    var listaJ2 = new List<Barco> { new Canonero(0,0), new Canonero(0,1), new Canonero(5,1), new Canonero(5,2), new Destructor(new List<(int,int)>{(9,0),(8,0),(7,0)}), new Destructor(new List<(int,int)>{(5,7),(4,7),(3,7)}), new Portaaviones(new List<(int,int)>{(1,3),(1,4),(1,5),(1,6)}) };
    juego.Iniciar(listaJ1, listaJ2);

    return Results.Ok();
});

// Disparar
app.MapPost("/api/disparar", (Acorazados.Dominio.Juego juego, DisparoRequest req) =>
{
    string resultado = string.Empty;
    bool terminado = false;
    try { resultado = juego.Disparar(req.X, req.Y); } catch { terminado = true; }

    return Results.Ok(new { resultado = resultado, terminado = terminado });
});

// Imprimir
app.MapPost("/api/imprimir", (Acorazados.Dominio.Juego juego, DisparoRequest req) =>
{
    string resultado = string.Empty;
    try { resultado = juego.Imprimir(); } catch { }

    return Results.Ok(new { tablero = resultado});
});

// Cambiar turno
app.MapPost("/api/cambiarturno", (Acorazados.Dominio.Juego juego) =>
{
    juego.FinalizarTurno();
    return Results.Ok();
});

app.Run();

record JugadoresRequest(string Jugador1, string Jugador2);
record DisparoRequest(int X, int Y);
namespace Acorazados.Dominio.Constantes;

public static class ConstantesJuego
{
    public const int LongitudPlataforma = 10;
    public const int LimiteSuperiorPlataforma = 9;
    public const int LimiteInferiorPlataforma = 0;
    public const int CantidadMaximaPorJugadorCanonero = 4;
    public const int CantidadMaximaPorJugadorDestructor = 2;
    public const int CantidadMaximaPorJugadorPortaaviones = 1;
    public const int CantidadBarcosHundidosParaGanar = 7;
    public const string MensajeBarcoFueraDelLimiteDeLaPlataforma = "Barco fuera del limite de la plataforma";
    public const string MensajeCantidadBarcosCanoneros1 = "Deben existir 4 cañoreros en la plataforma";
    public const string MensajeCantidadBarcosDestructores = "Deben existir 2 destructores en la plataforma";
    public const string MensajeCantidadBarcosPortaaviones = "Deben existir 1 portaavion en la plataforma";
    public const string MensajeBarcoHundido = "Barco hundido";
    public const string MensajeTiroAcertado = "Tiro acertado";
    public const string MensajeBarcosEnMismaPosicion = "Ya Existe un Barco en la misma posición";
    public const string MensajeJugadorUnoRequerido = "Debe agregar al jugador uno";
    public const string MensajeJugadorDosRequerido = "Debe agregar al jugador dos";
    public const string MensajeBarcosEnDiagonal = "Solo se pueden colocar barcos en posiciones horizontales o verticales";
    public const string MensajeJuegoTerminado = "El juego ya termino";
    public const string MensajeDisparoFueraDelTablero = "Disparo fuera del tablero";
}
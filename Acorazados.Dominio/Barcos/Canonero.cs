using Acorazados.Dominio.Records;

namespace Acorazados.Dominio.Barcos;

public class Canonero : Barco
{
    private const string MensajeCoordenadaObligatoria = "El tipo de barco cañonero necesita las coordenadas";
    public int CoordenadaX { private set; get; }
    public int CoordenadaY { private set; get; }
    
    public Canonero(int? coordenadaX = null, int? coordenadaY = null)
    {
        if (coordenadaX is null || coordenadaY is null)
            throw new ArgumentException(MensajeCoordenadaObligatoria);
            
        CoordenadaX = coordenadaX.Value;
        CoordenadaY = coordenadaY.Value;
    }
    
    public override bool EstaFueraDeLimites(int limiteInferior, int limiteSuperior) =>
        CoordenadaX < limiteInferior || CoordenadaX > limiteSuperior ||
        CoordenadaY < limiteInferior || CoordenadaY > limiteSuperior;

    public override bool SeEncuentraEnCoordenada(int x, int y) => CoordenadaX == x && CoordenadaY == y;

    public override bool EstaHundido() => Hundido;

    public override void RegistrarDañoRecibido() => Hundido = true;

    public override string ObtenerDescripcion() => "Cañonero";

    public override Coordenada ObtenerCoordenadaMinima() => new(CoordenadaX, CoordenadaY);
}
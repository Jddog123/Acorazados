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
    
    public override bool EstaFueraDeLimites(int limiteInferior, int limiteSuperior)
    {
        return CoordenadaX < limiteInferior || CoordenadaX > limiteSuperior ||
               CoordenadaY < limiteInferior || CoordenadaY > limiteSuperior;
    }
    
    public override bool SeEncuentraEnCoordenada(int x, int y)
    {
        return CoordenadaX == x && CoordenadaY == y;
    }
    
    public override bool EstaHundido()
    {
        return Hundido;
    }

    public override void RegistrarDisparo()
    {
        Hundido = true;
    }

    public override string ObtenerDescripcion()
    {
        return "Cañonero";
    }

    public override Coordenada ObtenerCoordenadaMinima()
    {
        return new Coordenada(CoordenadaX, CoordenadaY);
    }
}
using Acorazados.Dominio.Records;

namespace Acorazados.Dominio.Barcos;

public class Destructor : Barco
{
    private const string MensajeLongitudBarcoDestructor = "El tipo de barco destructor es de tres coordenadas";
    public List<(int x, int y)> Coordenadas { private set; get; }
    
    public Destructor(List<(int x, int y)> coordenadas)
    {
        Coordenadas = coordenadas;
        
        if(coordenadas.Count != 3)
            throw new ArgumentException(MensajeLongitudBarcoDestructor);
    }
    
    public override bool EstaFueraDeLimites(int limiteInferior, int limiteSuperior)
    {
        return Coordenadas.Any(coor =>
            coor.x < limiteInferior || coor.x > limiteSuperior ||
            coor.y < limiteInferior || coor.y > limiteSuperior);
    }
    
    public override bool SeEncuentraEnCoordenada(int x, int y)
    {
        return Coordenadas.Any(coor => coor.x == x && coor.y == y);
    }
    
    public override bool EstaHundido()
    {
        return DisparosAcertados == Coordenadas.Count;
    }

    public override void RegistrarDisparo()
    {
        DisparosAcertados += 1;
    }
    
    public override string ObtenerDescripcion()
    {
        return "Destructor";
    }
    
    public override Coordenada ObtenerCoordenadaMinima()
    {
        return new Coordenada(Coordenadas.Min().x, Coordenadas.Min().y);
    }
}
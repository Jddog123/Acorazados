using Acorazados.Dominio.Records;

namespace Acorazados.Dominio.Barcos;

public abstract class Barco
{
    protected bool Hundido; 
    protected int DisparosAcertados; 
    public abstract bool EstaFueraDeLimites(int limiteInferior, int limiteSuperior);
    public abstract bool SeEncuentraEnCoordenada(int x, int y);
    public abstract bool EstaHundido();
    public abstract void RegistrarDisparo();
    public abstract string ObtenerDescripcion();
    public abstract Coordenada ObtenerCoordenadaMinima();
}



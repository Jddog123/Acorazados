using Acorazados.Enums;

namespace Acorazados;

public class Juego
{
    private const int LimiteSuperiorPlataforma = 9;
    private const int LimiteInferiorPlataforma = 0;
    private char[,] _plataforma;

    public Juego()
    {
        _plataforma = new char[10, 10];
    }

    public void AgregarJugador(List<(List<(int x, int y)> coordenadas, TipoBarco tipoBarco)> listaBarcos)
    {
        if (listaBarcos.Any(barco => barco.tipoBarco == TipoBarco.Portaaviones && barco.coordenadas.Count() != 4))
            throw new ArgumentException("El tipo de barco portaaviones es de una coordenada");
        
        if (listaBarcos.Any(barco => barco.tipoBarco == TipoBarco.Destructor && barco.coordenadas.Count() != 3))
            throw new ArgumentException("El tipo de barco destructor es de una coordenada");

        if (listaBarcos.Any(barco => barco.tipoBarco == TipoBarco.Canonero && barco.coordenadas.Count() != 1))
            throw new ArgumentException("El tipo de barco cañonero es de una coordenada");

        if (listaBarcos.Any(barco => barco.coordenadas.Any(coor =>
                coor.x < LimiteInferiorPlataforma || coor.x > LimiteSuperiorPlataforma ||
                coor.y < LimiteInferiorPlataforma || coor.y > LimiteSuperiorPlataforma)))
        {
            throw new ArgumentException("Barco fuera del limite de la plataforma");
        }
    }
}
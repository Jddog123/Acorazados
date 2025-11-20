using System.Collections;
using Acorazados.Dominio.Barcos;

namespace Acorazados.ClassData;

public class DatosTripulacionJugadoresClassData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var listaBarcosJugadorUno = ListaBarcosJugadorUno();
        var listaBarcosJugadorDos = ListaBarcosJugadorDos();

        yield return new object[]
        {
            new DatosTripulacionJugadores
            {
                tripulacionJugadorUno = listaBarcosJugadorUno,
                tripulacionJugadorDos = listaBarcosJugadorDos
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    private static List<Barco> ListaBarcosJugadorUno()
    {
        return
        [
            new Canonero(2, 0),
            new Canonero(2, 1),
            new Canonero(2, 2),
            new Canonero(2, 3),
            new Destructor([(3, 0), (3, 2), (3, 3)]),
            new Destructor([(4, 0), (4, 2), (4, 3)]),
            new Portaaviones([(0, 0), (0, 2), (0, 3), (0, 4)])
        ];
    }
    
    private static List<Barco> ListaBarcosJugadorDos()
    {
        return
        [
            new Canonero(0, 0),
            new Canonero(0, 1),
            new Canonero(5, 1),
            new Canonero(5, 2),
            new Destructor([(9, 0), (8, 0), (7, 0)]),
            new Destructor([(5, 7), (4, 7), (3, 7)]),
            new Portaaviones([(1, 3), (1, 4), (1, 5), (1, 6)])
        ];
    }
}
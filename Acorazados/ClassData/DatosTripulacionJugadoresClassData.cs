using System.Collections;
using Acorazados.Enums;

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
    
    private static List<(List<(int x, int y)> coordenadas, TipoBarco)> ListaBarcosJugadorUno()
    {
        var listaBarcosJugadorUno = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canoneroUno = new List<(int x, int y)>
        {
            (2, 0)
        };
        var canoneroDos = new List<(int x, int y)>
        {
            (2, 1)
        };
        var canoneroTres = new List<(int x, int y)>
        {
            (2, 2)
        };
        var canoneroCuatro = new List<(int x, int y)>
        {
            (2, 3)
        };
        listaBarcosJugadorUno.Add((canoneroUno, TipoBarco.Canonero));
        listaBarcosJugadorUno.Add((canoneroDos, TipoBarco.Canonero));
        listaBarcosJugadorUno.Add((canoneroTres, TipoBarco.Canonero));
        listaBarcosJugadorUno.Add((canoneroCuatro, TipoBarco.Canonero));

        var destructor1 = new List<(int x, int y)>
        {
            (3, 0), (3, 2), (3, 3)
        };
        var destructor2 = new List<(int x, int y)>
        {
            (4, 0), (4, 2), (4, 3)
        };

        listaBarcosJugadorUno.Add((destructor1, TipoBarco.Destructor));
        listaBarcosJugadorUno.Add((destructor2, TipoBarco.Destructor));

        var portaAvion1 = new List<(int x, int y)>
        {
            (0, 0), (0, 2), (0, 3), (0, 4)
        };

        listaBarcosJugadorUno.Add((portaAvion1, TipoBarco.Portaaviones));
        return listaBarcosJugadorUno;
    }
    
    private static List<(List<(int x, int y)> coordenadas, TipoBarco)> ListaBarcosJugadorDos()
    {
        var listaBarcosJugadorDos = new List<(List<(int x, int y)> coordenadas, TipoBarco)>();
        var canoneroUno = new List<(int x, int y)>
        {
            (0, 0)
        };
        var canoneroDos = new List<(int x, int y)>
        {
            (0, 1)
        };
        var canoneroTres = new List<(int x, int y)>
        {
            (5, 1)
        };
        var canoneroCuatro = new List<(int x, int y)>
        {
            (5, 2)
        };
        listaBarcosJugadorDos.Add((canoneroUno, TipoBarco.Canonero));
        listaBarcosJugadorDos.Add((canoneroDos, TipoBarco.Canonero));
        listaBarcosJugadorDos.Add((canoneroTres, TipoBarco.Canonero));
        listaBarcosJugadorDos.Add((canoneroCuatro, TipoBarco.Canonero));

        var destructor1 = new List<(int x, int y)>
        {
            (9, 0), (8, 0), (7, 0)
        };
        var destructor2 = new List<(int x, int y)>
        {
            (5, 7), (4, 7), (3, 7)
        };

        listaBarcosJugadorDos.Add((destructor1, TipoBarco.Destructor));
        listaBarcosJugadorDos.Add((destructor2, TipoBarco.Destructor));

        var portaAvion1 = new List<(int x, int y)>
        {
            (1, 3), (1, 4), (1, 5), (1, 6)
        };

        listaBarcosJugadorDos.Add((portaAvion1, TipoBarco.Portaaviones));
        return listaBarcosJugadorDos;
    }
}
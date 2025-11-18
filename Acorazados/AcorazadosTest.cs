using FluentAssertions;

namespace Acorazados;

public class AcorazadosTest
{
    [Fact]
    public void Si_SeCreaElJuegoLasCoordenadas0_0y9_9_Debe_Existir()
    {
        var juego = new Juego();

        juego.ExisteCoordenada(ejeX: 0, ejeY: 0).Should().BeTrue();
        juego.ExisteCoordenada(ejeX: 9, ejeY: 9).Should().BeTrue();
    }
}

public class Juego
{
    public bool ExisteCoordenada(int ejeX, int ejeY)
    {
        throw new NotImplementedException();
    }
}
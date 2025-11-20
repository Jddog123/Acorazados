using Acorazados.Dominio.Barcos;

namespace Acorazados.Dominio.Records;

public record EstadisticasJugador(
        int totalDisparos,
        int totalFallos,
        int totalAciertos,
        List<Barco> barcosHundidos
    );
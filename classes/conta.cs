public class Conta {
    public int Id { get; set; }
    public float LeituraMesAtual { get; set; }
    public float LeituraMesAnterior { get; set; }
    public float ValorUltimoMes { get; set; }
    public float TotalSemImposto { get; set; }
    public float ValorMedio { get; set; }
    public Consumidor? Consumidor { get; set; }
    public Conta(int id, float leituraMesAtual, float leituraMesAnterior, float valorUltimoMes, float totalSemImposto, float valorMedio, Consumidor consumidor) {
        Id = id;
        LeituraMesAtual = leituraMesAtual;
        LeituraMesAnterior = leituraMesAnterior;
        ValorUltimoMes = valorUltimoMes;
        TotalSemImposto = totalSemImposto;
        ValorMedio = valorMedio;
        Consumidor = consumidor;
    }

    public float GetValorTotal() {
        return ValorUltimoMes;
    }

    public float SetValorTotal(float total) {
        ValorUltimoMes = total;
        return ValorUltimoMes;
    }

    public float GetUltimoMes() {
        return ValorUltimoMes;
    }

    public float SetUltimoMes(float valorUltimoMes) {
        ValorUltimoMes = valorUltimoMes;
        return valorUltimoMes;
    }

    public float GetTotalSemImposto() {
        return TotalSemImposto;
    }

    public float SetTotalSemImposto(float totalSemImposto) {
        TotalSemImposto = totalSemImposto;
        return totalSemImposto;
    }

    public float GetValorMedioConta() {
        return ValorMedio;
    }

    public float SetValorMedioConta(float valorMedio) {
        ValorMedio = valorMedio;
        return valorMedio;
    }
}


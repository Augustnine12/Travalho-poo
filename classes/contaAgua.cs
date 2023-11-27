using System;
using System.Collections.Generic;

public class ContaAgua : Conta
{
    public float Confis { get; set; }
    public Dictionary<string, Tuple<float, float>> TarifasPorFaixa { get; set; }

    public ContaAgua(int id, float leituraMesAtual, float leituraMesAnterior, float valorUltimoMes, float totalSemImposto, float valorMedio, Consumidor consumidor, float confis, Dictionary<string, Tuple<float, float>> tarifasPorFaixa, float total) : base(id, leituraMesAtual, leituraMesAnterior, valorUltimoMes, totalSemImposto, valorMedio, consumidor, total) {
        try {
            Confis = confis;
            TarifasPorFaixa = tarifasPorFaixa;
        }
        catch (Exception ex) {
            Console.WriteLine($"Erro ao criar ContaAgua: {ex.Message}");
            throw;
        }
    }

    public float CalcularConsumoUltimoMes(float leituraMesAnterior, float leituraMesAtual) {
        return leituraMesAtual - leituraMesAnterior;
    }

    public float CalcularValorConta(float consumo, int tipoConsumidor) {
        try {
            var faixa = TarifasPorFaixa[$"{(tipoConsumidor == 1 ? "Residencial" : "Comercial")}"];
            float valorConta = 0;

            if (consumo > faixa.Item1)
            {
                float faixaConsumo = Math.Min(consumo - faixa.Item1, faixa.Item2 - faixa.Item1);
                valorConta += faixaConsumo * faixa.Item2;
            }

            return valorConta + Confis;
        }
        catch (Exception ex) {
            Console.WriteLine($"Erro ao calcular valor da conta de água: {ex.Message}");
            throw;
        }
    }

    public Tuple<float, float> CalcularVariacaoConta(float leituraMes1, float leituraMes2, int tipoConsumidor) {
        float consumoMes1 = CalcularConsumoUltimoMes(leituraMes1, leituraMes2);
        float valorContaMes1 = CalcularValorConta(consumoMes1, tipoConsumidor);

        float consumoMes2 = CalcularConsumoUltimoMes(leituraMes2, leituraMes1);
        float valorContaMes2 = CalcularValorConta(consumoMes2, tipoConsumidor);

        return new Tuple<float, float>(valorContaMes1, valorContaMes2);
    }

    public float CalcularValorMedioConta(float leituraMesAnterior, float leituraMesAtual, int tipoConsumidor) {
        float consumo = CalcularConsumoUltimoMes(leituraMesAnterior, leituraMesAtual);
        return CalcularValorConta(consumo, tipoConsumidor);
    }

    public string CalcularMesValorMaior(float leituraMesAnterior, float leituraMesAtual, int tipoConsumidor) {
        float consumo = CalcularConsumoUltimoMes(leituraMesAnterior, leituraMesAtual);
        float valorConta = CalcularValorConta(consumo, tipoConsumidor);

        return (valorConta > Total) ? "Atual" : "Anterior";
    }
}

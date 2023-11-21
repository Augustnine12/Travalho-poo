using System;
using System.Collections.Generic;

public class ContaEnergia : Conta {
    public float ContribuicaoIluminacaoPublica { get; set; }
    public float TarifaResidencial { get; set; }
    public float TarifaComercial { get; set; }
    public float ImpostoResidencial { get; set; }
    public float ImpostoComercial { get; set; }
    public float Total { get; set; }

     public ContaEnergia(int id, float leituraMesAtual, float leituraMesAnterior, float valorUltimoMes, float totalSemImposto, float valorMedio, Consumidor consumidor, float contribuicaoIluminacaoPublica, float tarifaResidencial, float tarifaComercial, float impostoResidencial, float impostoComercial, float total) : base(id, leituraMesAtual, leituraMesAnterior, valorUltimoMes, totalSemImposto, valorMedio, consumidor) {
        try {
            ContribuicaoIluminacaoPublica = contribuicaoIluminacaoPublica;
            TarifaResidencial = tarifaResidencial;
            TarifaComercial = tarifaComercial;
            ImpostoResidencial = impostoResidencial;
            ImpostoComercial = impostoComercial;
            Total = total;
        }
        catch (Exception ex) {
            Console.WriteLine($"Erro ao criar ContaEnergia: {ex.Message}");
            throw;
        }
    }

    public float CalcularConsumoUltimoMes(float leituraMesAnterior, float leituraMesAtual) {
        return leituraMesAtual - leituraMesAnterior;
    }

    public float CalcularValorConta(float consumo, int tipoConsumidor) {
        try {
            float tarifa = (tipoConsumidor == 1) ? TarifaResidencial : TarifaComercial;
            float valorConsumo = consumo * tarifa;
            float valorContribuicao = ContribuicaoIluminacaoPublica;
            float valorImposto = (tipoConsumidor == 1) ? ImpostoResidencial : ImpostoComercial;

            return valorConsumo + valorContribuicao + (valorConsumo + valorContribuicao) * (valorImposto / 100);
        }
        catch (Exception ex) {
            Console.WriteLine($"Erro ao calcular valor da conta de energia: {ex.Message}");
            throw;
        }
    }

    public float CalcularValorContaSemImpostos(float consumo, int tipoConsumidor) {
        float tarifa = (tipoConsumidor == 1) ? TarifaResidencial : TarifaComercial;
        return consumo * tarifa + ContribuicaoIluminacaoPublica;
    }

    public Tuple<float, float> CalcularVariacaoConta(float leituraMes1, float leituraMes2, int tipoConsumidor) {
        float consumoMes1 = CalcularConsumoUltimoMes(leituraMes1, leituraMes2);
        float valorContaMes1 = CalcularValorConta(consumoMes1, tipoConsumidor);

        float consumoMes2 = CalcularConsumoUltimoMes(leituraMes2, leituraMes1);
        float valorContaMes2 = CalcularValorConta(consumoMes2, tipoConsumidor);

        return new Tuple<float, float>(valorContaMes1, valorContaMes2);
    }

    public float CalcularValorMedioConta(float leituraMesAnterior, float leituraMesAtual, int tipoConsumidor){
        float consumo = CalcularConsumoUltimoMes(leituraMesAnterior, leituraMesAtual);
        return CalcularValorConta(consumo, tipoConsumidor);
    }

    public string CalcularMesValorMaior(float leituraMesAnterior, float leituraMesAtual, int tipoConsumidor) {
        float consumo = CalcularConsumoUltimoMes(leituraMesAnterior, leituraMesAtual);
        float valorConta = CalcularValorConta(consumo, tipoConsumidor);

        return (valorConta > Total) ? "Atual" : "Anterior";
    }
}
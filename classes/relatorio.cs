public class Relatorio {
    public static float ConsumoUltimoMes(Conta conta) {
        try {
            return conta.ValorUltimoMes;
        }
        catch (Exception ex) {
            Console.WriteLine($"Erro ao obter consumo do último mês: {ex.Message}");
            throw;
        }
    }

    public static float ValorTotalConta(Conta conta) {
        try {
            return conta.TotalSemImposto;
        }
        catch (Exception ex) {
            Console.WriteLine($"Erro ao obter valor total da conta: {ex.Message}");
            throw;
        }
    }

    public static float ValorContaSemImpostos(Conta conta) {
        try {
            return conta.TotalSemImposto;
        }
        catch (Exception ex) {
            Console.WriteLine($"Erro ao obter valor da conta sem impostos: {ex.Message}");
            throw;
        }
    }

    public static Tuple<float, float>? VariacaoContaEntreMeses(Conta conta, float leituraMesAnterior, float leituraMesAtual) {
        try {
            if (conta is ContaEnergia energia) {
                return energia.CalcularVariacaoConta(leituraMesAnterior, leituraMesAtual, conta.Consumidor.Tipo);
            }
            if (conta is ContaAgua agua) {
                return agua.CalcularVariacaoConta(leituraMesAnterior, leituraMesAtual, conta.Consumidor.Tipo);
            }

            return null;
        }
        catch (Exception ex) {
            Console.WriteLine($"Erro ao calcular variação da conta entre dois meses: {ex.Message}");
            throw;
        }
    }

    public static float ValorMedioConta(Conta conta, float leituraMesAnterior, float leituraMesAtual) {
        try {
            if (conta is ContaEnergia energia) {
                return energia.CalcularValorMedioConta(leituraMesAnterior, leituraMesAtual, conta.Consumidor.Tipo);
            }
            else if (conta is ContaAgua agua) {
                return agua.CalcularValorMedioConta(leituraMesAnterior, leituraMesAtual, conta.Consumidor.Tipo);
            }

            return 0;
        }
        catch (Exception ex) {
            Console.WriteLine($"Erro ao calcular valor médio da conta: {ex.Message}");
            throw;
        }
    }

    public static string? MesMaiorValor(Conta conta, float leituraMesAnterior, float leituraMesAtual) {
        try {
            if (conta is ContaEnergia energia) {
                return energia.CalcularMesValorMaior(leituraMesAnterior, leituraMesAtual, conta.Consumidor.Tipo);
            }
            else if (conta is ContaAgua agua) {
                return agua.CalcularMesValorMaior(leituraMesAnterior, leituraMesAtual, conta.Consumidor.Tipo);
            }

            return null;
        }
        catch (Exception ex) {
            Console.WriteLine($"Erro ao calcular mês de maior valor: {ex.Message}");
            throw;
        }
    }
}
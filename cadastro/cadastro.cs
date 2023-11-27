class CadastroService {
    public static Consumidor CadastrarConsumidorViaUsuario(int id){
        try {
            Console.WriteLine("\nCadastro de Consumidor:");
            Console.Write("Informe o nome: ");
            string nome = Console.ReadLine();

            Console.Write("Informe a idade: ");
            int idade = int.Parse(Console.ReadLine());

            Console.Write("Informe o CPF: ");
            int cpf = int.Parse(Console.ReadLine());

            Console.Write("Informe o tipo (1 para residencial, 2 para comercial): ");
            int tipo = int.Parse(Console.ReadLine());

            Consumidor consumidor = new Consumidor(id, nome, idade, cpf, tipo);
            Console.WriteLine("Consumidor cadastrado com sucesso!");
            return consumidor;
        }
        catch (Exception ex) {
            Console.WriteLine($"Erro ao cadastrar consumidor: {ex.Message}");
            throw;
        }
    }

    public static ContaAgua CadastrarContaAguaViaUsuario(int id, Consumidor consumidor) {
        try {
            int tipoConsumidor = consumidor.Tipo;

            if (tipoConsumidor != 1) {
                Console.WriteLine("Aviso: Este consumidor não é residencial. A tarifa de água pode ser diferente.");
            }

            Console.WriteLine("\nCadastro de Conta de Água:");
            Console.Write("Informe a leitura do mês atual: ");
            float leituraMesAtual = float.Parse(Console.ReadLine());

            Console.Write("Informe a leitura do mês anterior: ");
            float leituraMesAnterior = float.Parse(Console.ReadLine());

            Console.Write("Informe o valor do último mês: ");
            float valorUltimoMes = float.Parse(Console.ReadLine());

            Dictionary<string, Tuple<float, float>> tarifasPorFaixa = new Dictionary<string, Tuple<float, float>>();
            Console.Write("Informe a tarifa para consumo residencial (faixa 1): ");
            float tarifaResidencial1 = float.Parse(Console.ReadLine());
            Console.Write("Informe a tarifa para consumo residencial (faixa 2): ");
            float tarifaResidencial2 = float.Parse(Console.ReadLine());
            tarifasPorFaixa.Add("Residencial", new Tuple<float, float>(tarifaResidencial1, tarifaResidencial2));

            Console.Write("Informe a tarifa para consumo comercial (faixa 1): ");
            float tarifaComercial1 = float.Parse(Console.ReadLine());
            Console.Write("Informe a tarifa para consumo comercial (faixa 2): ");
            float tarifaComercial2 = float.Parse(Console.ReadLine());
            tarifasPorFaixa.Add("Comercial", new Tuple<float, float>(tarifaComercial1, tarifaComercial2));

            Console.Write("Informe o valor do Confis: ");
            float confis = float.Parse(Console.ReadLine());
            
            float totalSemImposto = 0;
            float valorMedio = 0;
            float total = 0;

            ContaAgua contaAgua = new ContaAgua(id, leituraMesAtual, leituraMesAnterior, valorUltimoMes, totalSemImposto, valorMedio, consumidor, confis, tarifasPorFaixa, total);

            Console.WriteLine("Conta de Água cadastrada com sucesso!");
            return contaAgua;
        }
        catch (Exception ex) {
            Console.WriteLine($"Erro ao cadastrar conta de água: {ex.Message}");
            throw;
        }
    }

    public static ContaEnergia CadastrarContaEnergiaViaUsuario(int id, Consumidor consumidor) {
        try {
            int tipoConsumidor = consumidor.Tipo;

            if (tipoConsumidor != 1) {
                Console.WriteLine("Aviso: Este consumidor não é residencial. A tarifa de água pode ser diferente.");
            }

            Console.WriteLine("\nCadastro de Conta de Energia:");
            Console.Write("Informe a leitura do mês atual: ");
            float leituraMesAtual = float.Parse(Console.ReadLine());

            Console.Write("Informe a leitura do mês anterior: ");
            float leituraMesAnterior = float.Parse(Console.ReadLine());

            Console.Write("Informe o valor do último mês: ");
            float valorUltimoMes = float.Parse(Console.ReadLine());

            Console.Write("Informe a contribuição para iluminação pública: ");
            float contribuicaoIluminacaoPublica = float.Parse(Console.ReadLine());

            Console.Write("Informe a tarifa residencial: ");
            float tarifaResidencial = float.Parse(Console.ReadLine());

            Console.Write("Informe a tarifa comercial: ");
            float tarifaComercial = float.Parse(Console.ReadLine());

            Console.Write("Informe o imposto residencial: ");
            float impostoResidencial = float.Parse(Console.ReadLine());

            Console.Write("Informe o imposto comercial: ");
            float impostoComercial = float.Parse(Console.ReadLine());

            ContaEnergia contaEnergia = new ContaEnergia(id, leituraMesAtual, leituraMesAnterior, valorUltimoMes, 0, 0, consumidor, contribuicaoIluminacaoPublica, tarifaResidencial, tarifaComercial, impostoResidencial, impostoComercial, 0);

            float totalSemImposto = contaEnergia.CalcularValorContaSemImpostos(leituraMesAtual, tipoConsumidor);
            float total = contaEnergia.CalcularValorConta(leituraMesAtual, tipoConsumidor);
            float valorMedio = contaEnergia.CalcularValorMedioConta(leituraMesAnterior, leituraMesAtual, tipoConsumidor);

            contaEnergia.TotalSemImposto = totalSemImposto;
            contaEnergia.Total = total;
            contaEnergia.ValorMedio = valorMedio;

            Console.WriteLine("Conta de Energia cadastrada com sucesso!");
            return contaEnergia;
        }
        catch (Exception ex) {
            Console.WriteLine($"Erro ao cadastrar conta de energia: {ex.Message}");
            throw;
        }
    }
}
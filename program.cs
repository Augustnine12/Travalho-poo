using System.IO;

class program {
    static int consumidorIdCounter = 1;
    static int contaIdCounter = 1;
    static List<Consumidor> ListaConsumidores = new List<Consumidor>();
    static List<Conta> ListaContas = new List<Conta>();
    static void Main(String[] args) {
        Console.WriteLine("Bem-vindo ao sistema da ALBERT");
        CarregarDadosDoArquivo();
        try {
            int opcao;
            do {
                Console.WriteLine("\nEscolha um opção: ");
                Console.WriteLine("1) Cadastro de consummidor e contas.");
                Console.WriteLine("2) Consultar informações");
                Console.WriteLine("3) Sair");

                if (int.TryParse(Console.ReadLine(), out opcao)) {
                    switch (opcao) {
                        case 1: 
                            Consumidor consumidor = CadastroService.CadastrarConsumidorViaUsuario(consumidorIdCounter++);
                            ListaConsumidores.Add(consumidor);
                            Console.WriteLine("Escolha o tipo de conta que deseja cadastra:");
                            Console.WriteLine("1) Conta de Água, 2) Conta de Energia");
                            
                            Console.WriteLine();
                            
                            int tipoConta = int.Parse(Console.ReadLine()); 

                            switch (tipoConta) {
                                case 1: 
                                    ContaAgua agua = CadastroService.CadastrarContaAguaViaUsuario(contaIdCounter++, consumidor);
                                    ListaContas.Add(agua);
                                    break;
                                case 2:
                                    ContaEnergia energia = CadastroService.CadastrarContaEnergiaViaUsuario(contaIdCounter++, consumidor);
                                    ListaContas.Add(energia);
                                    break;
                                default:
                                    Console.WriteLine("Tipo de conta inválido.");
                                    break;
                            }
                            break;
                        case 2:
                            if (ListaContas.Count == 0) {
                                Console.WriteLine("Nenhuma conta ainda foi cadastrada. Cadastre antes de consultar.");
                            } else {
                                Console.WriteLine("Escolha o tipo de conta que deseja consultar:");
                                Console.WriteLine("1) Conta de Água, 2) Conta de Energia");

                                int tipoConsultaConta;
                                do {
                                    if (int.TryParse(Console.ReadLine(), out tipoConsultaConta)) {
                                        switch (tipoConsultaConta) {
                                            case 1:
                                                List<ContaAgua> contasAgua = ListaContas.OfType<ContaAgua>().ToList();
                                                if (contasAgua.Count > 0) {
                                                    Console.WriteLine("Escolha o ID da conta de Água que deseja consultar:");
                                                    foreach (ContaAgua contaAgua in contasAgua) {
                                                        Console.WriteLine($"ID: {contaAgua.Id}");
                                                    }
                                                    if (int.TryParse(Console.ReadLine(), out int idContaAgua)) {
                                                        ContaAgua contaAguaSelecionada = contasAgua.Find(c => c.Id == idContaAgua);
                                                        if (contaAguaSelecionada != null) {
                                                            ConsultarInformacoesConta(contaAguaSelecionada, "Água");
                                                        } else {
                                                            Console.WriteLine("Conta de Água não encontrada.");
                                                        }
                                                    } else {
                                                        Console.WriteLine("ID inválido. Tente novamente.");
                                                    }
                                                } else {
                                                    Console.WriteLine("Nenhuma conta de Água cadastrada ainda. Cadastre uma conta de Água primeiro.");
                                                }
                                                break;

                                            case 2:
                                                List<ContaEnergia> contasEnergia = ListaContas.OfType<ContaEnergia>().ToList();
                                                if (contasEnergia.Count > 0) {
                                                    Console.WriteLine("Escolha o ID da conta de Energia que deseja consultar:");
                                                    foreach (ContaEnergia contaEnergia in contasEnergia) {
                                                        Console.WriteLine($"ID: {contaEnergia.Id}");
                                                    }
                                                    if (int.TryParse(Console.ReadLine(), out int idContaEnergia)) {
                                                        ContaEnergia contaEnergiaSelecionada = contasEnergia.Find(c => c.Id == idContaEnergia);
                                                        if (contaEnergiaSelecionada != null) {
                                                            ConsultarInformacoesConta(contaEnergiaSelecionada, "Energia");
                                                        } else {
                                                            Console.WriteLine("Conta de Energia não encontrada.");
                                                        }
                                                    } else {
                                                        Console.WriteLine("ID inválido. Tente novamente.");
                                                    }
                                                } else {
                                                    Console.WriteLine("Nenhuma conta de Energia cadastrada ainda. Cadastre uma conta de Energia primeiro.");
                                                }
                                                break;

                                            default:
                                                Console.WriteLine("Tipo de conta inválido!");
                                                break;
                                        }
                                    }
                                } while (tipoConsultaConta != 2);
                            }
                            break;
                        case 3:
                            Console.WriteLine("Saindo do sistema. Até logo!");
                            break;
                        default:
                            Console.WriteLine("Opção inválida. Tente novamente.");
                            break;
                    }
                }
                else {
                    Console.WriteLine("Opção inválida. Tente novamente.");
                }
            } while (opcao != 3);
        }
        catch (Exception ex) {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }

     private static void CarregarDadosDoArquivo() {
        try {
            if (File.Exists("dados.txt")) {
                string[] linhas = File.ReadAllLines("dados.txt");

                foreach (string linha in linhas) {
                    string[] dados = linha.Split(';');

                    if (dados[0] == "Consumidor") {
                        Consumidor consumidor = new Consumidor(
                            int.Parse(dados[1]),
                            dados[2],
                            int.Parse(dados[3]),
                            int.Parse(dados[4]),
                            int.Parse(dados[5])
                        );
                        ListaConsumidores.Add(consumidor);
                    } else if (dados[0] == "ContaAgua") {
                        ContaAgua agua = new ContaAgua(
                            int.Parse(dados[1]),
                            float.Parse(dados[2]),
                            float.Parse(dados[3]),
                            float.Parse(dados[4]),
                            float.Parse(dados[5]),
                            float.Parse(dados[6]),
                            ListaConsumidores.Find(c => c.Id == int.Parse(dados[7])),
                            float.Parse(dados[8]),
                            new Dictionary<string, Tuple<float, float>>(),
                            float.Parse(dados[9])
                        );
                        ListaContas.Add(agua);
                    } else if (dados[0] == "ContaEnergia") {
                        ContaEnergia energia = new ContaEnergia(
                            int.Parse(dados[1]),
                            float.Parse(dados[2]),
                            float.Parse(dados[3]),
                            float.Parse(dados[4]),
                            float.Parse(dados[5]),
                            float.Parse(dados[6]),
                            ListaConsumidores.Find(c => c.Id == int.Parse(dados[7])),
                            float.Parse(dados[8]),
                            float.Parse(dados[9]),
                            float.Parse(dados[10]),
                            float.Parse(dados[11]),
                            float.Parse(dados[12]),
                            float.Parse(dados[13])
                        );
                        ListaContas.Add(energia);
                    }
                }

                Console.WriteLine("Dados carregados com sucesso!");
            } else {
                Console.WriteLine("Nenhum arquivo de dados encontrado. Iniciando com dados vazios.");
            }
        } catch (Exception ex) {
            Console.WriteLine($"Erro ao carregar dados do arquivo: {ex.Message}");
        }
    }

    private static void SalvarDadosNoArquivo() {
        try {
            List<string> linhas = new List<string>();

            foreach (Consumidor consumidor in ListaConsumidores) {
                string linha = $"Consumidor;{consumidor.Id};{consumidor.Nome};{consumidor.Idade};{consumidor.CPF};{consumidor.Tipo}";
                linhas.Add(linha);
            }

            foreach (Conta conta in ListaContas) {
                if (conta is ContaAgua) {
                    ContaAgua agua = (ContaAgua)conta;
                    string linha = $"ContaAgua;{agua.Id};{agua.LeituraMesAtual};{agua.LeituraMesAnterior};{agua.ValorUltimoMes};" +
                        $"{agua.TotalSemImposto};{agua.ValorMedio};{agua.Consumidor.Id};{agua.Confis};{agua.Total}";
                    linhas.Add(linha);
                } else if (conta is ContaEnergia) {
                    ContaEnergia energia = (ContaEnergia)conta;
                    string linha = $"ContaEnergia;{energia.Id};{energia.LeituraMesAtual};{energia.LeituraMesAnterior};{energia.ValorUltimoMes};" +
                        $"{energia.TotalSemImposto};{energia.ValorMedio};{energia.Consumidor.Id};{energia.ContribuicaoIluminacaoPublica};" +
                        $"{energia.TarifaResidencial};{energia.TarifaComercial};{energia.ImpostoResidencial};{energia.ImpostoComercial};{energia.Total}";
                    linhas.Add(linha);
                }
            }

            File.WriteAllLines("dados.txt", linhas);

            Console.WriteLine("Dados salvos com sucesso!");
        } catch (Exception ex) {
            Console.WriteLine($"Erro ao salvar dados no arquivo: {ex.Message}");
        }
    }

    private static void ConsultarInformacoesConta(Conta conta, string tipoConta) {
        Console.WriteLine($"\nEscolha uma opção de consulta para conta de {tipoConta}:");
        Console.WriteLine("1) Consumo do último mês");
        Console.WriteLine("2) Valor total da conta");
        Console.WriteLine("3) Valor da conta sem impostos");
        Console.WriteLine("4) Variação da conta entre dois meses");
        Console.WriteLine("5) Valor médio da conta");
        Console.WriteLine("6) Mês de maior valor");
        Console.WriteLine("7) Sair da consulta");

        int opcao;
        do {
            if (int.TryParse(Console.ReadLine(), out opcao)) {
                switch (opcao) {
                    case 1:
                        float consumoUltimoMes = Relatorio.ConsumoUltimoMes(conta);
                        Console.WriteLine($"Consumo do ultimo mês: {consumoUltimoMes}");
                        break;
                    case 2:
                        float valorTotal = Relatorio.ValorTotalConta(conta);
                        Console.WriteLine($"Valor total da conta: {valorTotal}");
                        break;
                    case 3:
                        float valorSemImposto = Relatorio.ValorContaSemImpostos(conta);
                        Console.WriteLine($"Valor da conta sem impostos: {valorSemImposto}");
                        break;
                    case 4:
                        Tuple<float, float>? variacaoConta = Relatorio.VariacaoContaEntreMeses(conta, conta.LeituraMesAnterior, conta.LeituraMesAtual);
                        
                        if (variacaoConta != null) {
                            Console.WriteLine($"Variação da conta: {variacaoConta.Item1} para o mês anterior e {variacaoConta.Item2} para o mês atual");
                        } else {
                            Console.WriteLine("Variação da conta não disponível para este tipo de conta.");
                        }
                        break;
                    case 5:
                        float valorMedioConta = Relatorio.ValorMedioConta(conta, conta.LeituraMesAnterior, conta.LeituraMesAtual);
                        Console.WriteLine($"Valor médio da conta: {valorMedioConta}");
                        break;
                    case 6:
                        string mesMaiorValor = Relatorio.MesMaiorValor(conta, conta.LeituraMesAnterior, conta.LeituraMesAtual);
                        Console.WriteLine($"Mês de maior valor: {mesMaiorValor}");
                        break;
                    case 7:
                    Console.WriteLine("Saindo da consulta. Voltando para o menu principal.");
                    return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        } while (opcao != 7);
    }
}
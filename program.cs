class program {
    static int consumidorIdCounter = 1;
    static int contaIdCounter = 1;
    static ContaAgua contaAgua;
    static ContaEnergia contaEnergia;
    static void Main(String[] args) {
        Console.WriteLine("Bem-vindo ao sistema da ALBERT");
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
                            Console.WriteLine("Escolha o tipo de conta que deseja cadastra:");
                            Console.WriteLine("1) Conta de Água, 2) Conta de Energia");
                            
                            Console.WriteLine();
                            
                            int tipoConta = int.Parse(Console.ReadLine()); 

                            switch (tipoConta) {
                                case 1: 
                                    ContaAgua Agua = CadastroService.CadastrarContaAguaViaUsuario(contaIdCounter++, consumidor);
                                    break;
                                case 2:
                                    ContaEnergia contaEnergia = CadastroService.CadastrarContaEnergiaViaUsuario(contaIdCounter++, consumidor);
                                    break;
                                default:
                                    Console.WriteLine("Tipo de conta inválido.");
                                    break;
                            }
                            break;
                        case 2:
                            if (contaAgua == null && contaEnergia == null) {
                                Console.WriteLine("Nenhuma conta ainda foi cadastra. Cadastre antes de consultar.");
                            }

                            Console.WriteLine("Escolha o tipo de conta que deseja consultar:");
                            Console.WriteLine("1) Conta de Água, 2) Conta de Energia");

                            Console.WriteLine();

                            int tipoConsultaConta;

                            do {
                                if (int.TryParse(Console.ReadLine(), out tipoConsultaConta)) {
                                    switch (tipoConsultaConta) {
                                        case 1: 
                                            if (contaAgua != null) {
                                                ConsultarInformacoesConta(contaAgua, "Água");
                                            } else {
                                                Console.WriteLine("Nenhuma conta de água cadastrada ainda. Cadastre uma conta de água primeiro.");
                                            }
                                            break;
                                        case 2: 
                                            if (contaEnergia != null) {
                                                ConsultarInformacoesConta(contaEnergia, "Energia");
                                            } else {
                                                Console.WriteLine("Nenhuma conta de Energia cadastrada ainda. Cadastre uma conta de Energia primeiro.");
                                            }
                                            break;
                                        default:
                                            Console.WriteLine("Tipo de conta invalida!");
                                            break;
                                    }
                                }
                            } while (tipoConsultaConta != 2);
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

    private static void ConsultarInformacoesConta(Conta conta, string tipoConta) {
        Console.WriteLine($"\nEscolha uma opção de consulta para conta de {tipoConta}:");
        Console.WriteLine("1) Consumo do último mês");
        Console.WriteLine("2) Valor total da conta");
        Console.WriteLine("3) Valor da conta sem impostos");
        Console.WriteLine("4) Variação da conta entre dois meses");
        Console.WriteLine("5) Valor médio da conta");
        Console.WriteLine("6) Mês de maior valor");
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
                        Console.WriteLine($"Consumo do ultimo mês: {valorTotal}");
                        break;
                    case 3:
                        float valorSemImposto = Relatorio.ValorContaSemImpostos(conta);
                        Console.WriteLine($"Consumo do ultimo mês: {valorSemImposto}");
                        break;
                    case 4:
                        Console.Write("Informe a leitura do mês anterior: ");
                        float leituraMesAnterior = float.Parse(Console.ReadLine());
                        Console.Write("Informe a leitura do mês atual: ");
                        float leituraMesAtual = float.Parse(Console.ReadLine());
                        Tuple<float, float>? variacaoConta = Relatorio.VariacaoContaEntreMeses(conta, leituraMesAnterior, leituraMesAtual);
                        
                        if (variacaoConta != null) {
                            Console.WriteLine($"Variação da conta: {variacaoConta.Item1} para o mês anterior e {variacaoConta.Item2} para o mês atual");
                        } else {
                            Console.WriteLine("Variação da conta não disponível para este tipo de conta.");
                        }
                        break;
                    case 5:
                        Console.Write("Informe a leitura do mês anterior: ");
                        float leituraAnteriorMedia = float.Parse(Console.ReadLine());
                        Console.Write("Informe a leitura do mês atual: ");
                        float leituraAtualMedia = float.Parse(Console.ReadLine());
                        float valorMedioConta = Relatorio.ValorMedioConta(conta, leituraAnteriorMedia, leituraAtualMedia);
                        Console.WriteLine($"Valor médio da conta: {valorMedioConta}");
                        break;
                    case 6:
                        Console.Write("Informe a leitura do mês anterior: ");
                        float leituraMesAnteriorMaior = float.Parse(Console.ReadLine());
                        Console.Write("Informe a leitura do mês atual: ");
                        float leituraMesAtualMaior = float.Parse(Console.ReadLine());
                        string mesMaiorValor = Relatorio.MesMaiorValor(conta, leituraMesAnteriorMaior, leituraMesAtualMaior);
                        Console.WriteLine($"Mês de maior valor: {mesMaiorValor}");
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        } while (opcao != 6);
    }
}


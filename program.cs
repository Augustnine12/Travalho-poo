class program {
    static int consumidorIdCounter = 1;
    static int contaIdCounter = 1;

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
                            Console.WriteLine("Escolha o tipo de conta que deseja cadastra");
                            int tipoConta = int.Parse(Console.ReadLine()); 

                            switch (tipoConta) {
                                case 1: 
                                    ContaAgua Agua = CadastroService.CadastrarContaAguaViaUsuario(contaIdCounter++, consumidor);
                                    break;
                                case 2:
                                    // ContaEnergia contaEnergia = CadastroService.CadastrarContaEnergiaViaUsuario(contaIdCounter++, consumidor);
                                    break;
                                default:
                                    Console.WriteLine("Tipo de conta inválido.");
                                    break;
                            }
                            break;
                        case 2:
                            // ConsultarInformacoes();
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
}


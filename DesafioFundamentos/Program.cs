using DesafioFundamentos.Models;

// Coloca o encoding para UTF8 para exibir acentuação
Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("Seja bem vindo ao sistema de estacionamento!");

decimal precoInicial = Estacionamento.AlterarPrecos(true);
decimal precoPorHora = Estacionamento.AlterarPrecos(false);


// Instancia a classe Estacionamento, já com os valores obtidos anteriormente
Estacionamento es = new Estacionamento(precoInicial, precoPorHora);

string opcao = string.Empty;
bool exibirMenu = true;

// Realiza o loop do menu
while (exibirMenu)
{
    Console.Clear();
    Console.WriteLine($"Escolha uma opção (Taxa Inicial de R${precoInicial} e R${precoPorHora}/h):");
    Console.WriteLine("1 - Cadastrar veículo");
    Console.WriteLine("2 - Remover veículo");
    Console.WriteLine("3 - Listar veículos");
    Console.WriteLine("4 - Alterar Preço Inicial e Preço por hora");
    Console.WriteLine("5 - Encerrar");
    opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            es.AdicionarVeiculo();
            break;

        case "2":
            es.RemoverVeiculo();
            break;

        case "3":
            es.ListarVeiculos();
            break;
        
        case "4":
            precoInicial = Estacionamento.AlterarPrecos(true);
            precoPorHora = Estacionamento.AlterarPrecos(false);
            es.PrecosEstacionamento(precoInicial, precoPorHora);
            break;

        case "5":
            exibirMenu = false;
            break;

        default:
            Console.WriteLine("Opção inválida");
            break;
    }

    Console.WriteLine("Pressione uma tecla para continuar");
    Console.ReadLine();
}

Console.WriteLine("O programa se encerrou");

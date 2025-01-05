using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void PrecosEstacionamento(decimal precoInicial, decimal precoPorHora){
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            string placa = Console.ReadLine().ToUpper(); //Pedir para o usuário digitar uma placa e passa as letras p/ UpStream

            string placaAntigaPadrao = @"^[a-zA-Z]{3}\d{4}$"; // padrão da placa antiga AAA0000
            string placaNovaPadrao = @"^[a-zA-Z]{3}\d[a-zA-Z]\d{2}$"; //padrão da nova placa AAA0A00

            bool formatoAntigo = Regex.IsMatch(placa,placaAntigaPadrao, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(2)); //compara placa antiga com o digitado
            bool formatoNovo = Regex.IsMatch(placa,placaNovaPadrao, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(2)); //compara placa nova com o digitado
            
            if (!(formatoAntigo||formatoNovo)){ //verifica se a placa digitada não se encaixa no formato antigo nem no novo
                Console.WriteLine("A placa fornecida não se encontra no formato correto! \n" +
                    "AAA0000 , AAA0A00");
                return;
            }

            bool placaExistente = veiculos.Any(x => x == placa); //true se a placa já existe na lista

            if (placaExistente){ //verifica se a placa digitada já existe na lista
                Console.WriteLine("Veículo já está cadastrado e permanece no estacionamento!");
                return;
            }

            veiculos.Add(placa); //adiciona placa na lista
            Console.WriteLine($"Veículo '{placa}' adicionado com sucesso!");
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");
            string placa = Console.ReadLine().ToUpper(); //Pedir para o usuário digitar uma placa e passa as letras p/ UpStream
            
            if (veiculos.Any(x => x == placa)) // Verifica se o veículo existe
            {
                bool verificaHoras;
                int horas;
                do{
                    Console.WriteLine(" -- Digite a quantidade de horas que o veículo permaneceu estacionado: -- "); 
                    verificaHoras = int.TryParse(Console.ReadLine(), out horas);//le o input das horas e tenta converter para inteiro
                } while( (!verificaHoras) || (horas < 0) );

                bool verificaMinutos;
                int minutos;
                do{
                    Console.WriteLine(" -- Digite a quantidade de minutos que o veículo permaneceu estacionado: -- "); 
                    verificaMinutos = int.TryParse(Console.ReadLine(), out minutos); //le o input dos minutos e tenta converter para inteiro
                } while( (!verificaMinutos) || (minutos >= 60) || (minutos < 0) );

                decimal valorTotal = precoInicial + precoPorHora * (horas + (decimal)minutos/60); //calculo do valor a ser pago

                veiculos.Remove(placa); //Remove a placa digitada da lista de veículos

                Console.WriteLine($"O veículo '{placa}' foi removido e o preço total foi de: 'R$ {valorTotal}'");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                foreach (string veiculo in veiculos){ //cria o laço de repetição que mostra todos os veiculos cadastrasdos
                    Console.WriteLine($"{veiculo}");
                }
            }
            else
            {
                Console.WriteLine("Nenhum veículo estacionado!");
            }
        }

        public static decimal AlterarPrecos(bool chave){

            decimal novoPreco;
            bool verificaConversaonovoPreco;
            bool verificanovoPreco;

            do{  // repete o loop até que o preco seja conversível em decimal e positivo ou zero
                Console.WriteLine($" -- Digite o preço {(chave ? "inicial" : "por hora")}: -- ");
                verificaConversaonovoPreco = Decimal.TryParse(Console.ReadLine(), out novoPreco); //verfica se a conversao ocorreu

                if (chave)
                    verificanovoPreco = verificaConversaonovoPreco && novoPreco >= 0; //chave == true => preco incial => verifica se houve conversao e o valor é positivo ou zero
                else
                    verificanovoPreco = verificaConversaonovoPreco && novoPreco > 0; //chave == false => preco p/ hora => verifica se houve conversao e o valor é positivo  

                if ( !verificanovoPreco ){ //retorna ao usuario um erro caso nao seja possivel  a conversao
                    Console.WriteLine("Valor Inválido! Tente novamente.");
                }

            } while( !verificanovoPreco );

            return novoPreco;
        }
    }
}

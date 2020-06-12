using Biblioteca.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace thiago_gonçalves_AT_C
{
    class Menu
    {
        private static void EscreverNaTela(string texto)
        {
            Console.WriteLine(texto);
        }

        public static void MenuPrincipal()
        {
            EscreverNaTela("====== Aniversariantes do dia ======");
            AniversariantesDoDia();
            EscreverNaTela("====== Selecione uma operação ======");
            EscreverNaTela("1 - Adicionar nova pessoa");
            EscreverNaTela("2 - Consultar pessoas");
            EscreverNaTela("3 - Editar pessoa");
            EscreverNaTela("4 - Excluir pessoa");
            EscreverNaTela("5 - Sair");
            EscreverNaTela("====================================");

            char operacao = Console.ReadLine().ToCharArray()[0];

            switch (operacao)
            {
                case '1':
                    AdicionaPessoa(); 
                    break;
                case '2':
                    ConsultarPessoas();
                    break;
                case '3':
                    EditarPessoa();
                    break;
                case '4':
                    ExcluirPessoa();
                    break;
                case '5': EscreverNaTela("Saindo do programa..."); break;
                default: EscreverNaTela("Opção inexistente"); break;
            }
        }

        private static void EditarPessoa()
        {
            LimparTela();
            EscreverNaTela("=====================================");
            foreach (var pessoa in repositorio.BuscarTodasAsPessoas())
            {
                EscreverNaTela($"{pessoa.Id}- Nome: {pessoa.Nome} || Aniversário: {pessoa.DataDeAniversario} || Dias restantes: {pessoa.ProximoAniversario()}");
            }
            EscreverNaTela("======= Insira o Id da pessoa =======");
            int idEscolhido = int.Parse(Console.ReadLine());

            foreach(var pessoa in repositorio.BuscarTodasAsPessoas())
            {
                if (pessoa.Id == idEscolhido)
                {
                    EscreverNaTela("Digite o novo nome:");
                    string novoNome = Console.ReadLine();

                    EscreverNaTela("Digite a nova data no modelo dd/mm/aaaa:");
                    DateTime novaData = DateTime.Parse(Console.ReadLine());

                    Pessoa pessoaEditada = repositorio.BuscarPessoaPelo(idEscolhido);

                    Pessoa editado = new Pessoa(pessoaEditada.Id, novoNome, novaData);

                    repositorio.Editar(editado);

                    EscreverNaTela("Pessoa editada...");
                }
            }
            EscreverNaTela("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            LimparTela();
            MenuPrincipal();
        }

        private static void ExcluirPessoa()
        {
            LimparTela();
            EscreverNaTela("=====================================");
            foreach (var pessoa in repositorio.BuscarTodasAsPessoas())
            {
                EscreverNaTela($"{pessoa.Id}- Nome: {pessoa.Nome} || Aniversário: {pessoa.DataDeAniversario} || Dias restantes: {pessoa.ProximoAniversario()}");
            }           
            EscreverNaTela("======= Insira o Id da pessoa =======");
            int idEscolhido = int.Parse(Console.ReadLine());
            repositorio.Deletar(idEscolhido);
            EscreverNaTela("Pessoa deletada...");
            EscreverNaTela("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            LimparTela();
            MenuPrincipal();

        }

        private static void AniversariantesDoDia()
        {
            DateTime hoje = DateTime.Today;
            var listaDeAniversariantes = repositorio.BuscarTodasAsPessoas(hoje);

            if (listaDeAniversariantes.Count() == 0)
            {
                EscreverNaTela("Não há aniversariantes adicionados hoje");
            }
            else
            {
                foreach (var pessoa in listaDeAniversariantes)
                {
                    EscreverNaTela($"Nome: {pessoa.Nome}");
                }
            }
        }

        private static void ConsultarPessoas()
        {
            LimparTela();

            MenuDeConsulta();
        }

        private static void MenuDeConsulta()
        {
            EscreverNaTela("====== Escolha uma opção de filtro ======");
            EscreverNaTela("1 - Consultar pelo nome");
            EscreverNaTela("2 - Exibir todas as pessoas cadastradas");
            EscreverNaTela("=========================================");
            string tipoDeConsulta = Console.ReadLine();

            switch (tipoDeConsulta)
            {
                case "1":
                    ConsultarPeloNome();
                    break;

                case "2":
                    ExibirTodasAsPessoas();
                    break;

                default:
                    EscreverNaTela("Consulta incorreta");
                    MenuDeConsulta();
                    break;
            }
        }

        private static void ConsultarPeloNome()
        {
            EscreverNaTela("Entre com o nome da pessoa:");
            string nome = Console.ReadLine();

            var pessoasEncontradas = repositorio.BuscarTodasAsPessoas(nome);

            int quantidadeDePessoasEncontradas = pessoasEncontradas.Count();

            if (quantidadeDePessoasEncontradas > 0)
            {
                EscreverNaTela("======== Pessoas encontradas ========");

                foreach (var pessoa in pessoasEncontradas)
                {
                    EscreverNaTela($"{pessoa.Id}- Nome: {pessoa.Nome} || Aniversário: {pessoa.DataDeAniversario} || Dias restantes: {pessoa.ProximoAniversario()}");
                }
                EscreverNaTela("=====================================");
            }
            else
            {
                EscreverNaTela("Nenhum pessoa foi encontrada para o nome: " + nome);
            }
            EscreverNaTela("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            LimparTela();
            MenuPrincipal();
        }

        private static void ExibirTodasAsPessoas()
        {
            EscreverNaTela("=====================================");
            foreach (var pessoa in repositorio.BuscarTodasAsPessoas())
            {
                EscreverNaTela($"{pessoa.Id}- Nome: {pessoa.Nome} || Aniversário: {pessoa.DataDeAniversario} || Dias restantes: {pessoa.ProximoAniversario()}");
            }
            EscreverNaTela("=====================================");

            EscreverNaTela("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            LimparTela();
            MenuPrincipal();
        }

        private static void AdicionaPessoa()
        {
            LimparTela();

            EscreverNaTela("Entre com o Nome:");
            string nome = Console.ReadLine();

            EscreverNaTela("Entre com a data de aniversário no modelo dd/mm/aaaa:");
            DateTime dataDeAniversario = DateTime.Parse(Console.ReadLine());

            var pessoa = new Pessoa(nome, dataDeAniversario);

            var pessoas = repositorio.BuscarTodasAsPessoas();

            foreach (var item in pessoas)
            {
                Pessoa ultimoNaLista = pessoas.Last(x => x.Id == item.Id);
                pessoa.Id = ultimoNaLista.Id + 1;
            }

            try
            {
                repositorio.Salvar(pessoa);
            }
            catch (Exception e)
            {
                EscreverNaTela(e.ToString());
            }

            EscreverNaTela("Cadastrado com sucesso!");
            EscreverNaTela("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            LimparTela();

            MenuPrincipal();
        }
        private static void LimparTela()
        {
            Console.Clear();
        }

        private static IRepositorio repositorio = new Repositorio();
    }
}

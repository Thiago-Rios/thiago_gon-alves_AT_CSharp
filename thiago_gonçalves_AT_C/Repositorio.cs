using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace thiago_gonçalves_AT_C
{
    class Repositorio
    {

        public static void Salvar(Pessoa pessoa)
        {
            if (!PessoaJaEstaCadastrada(pessoa))
            {
                //AlterarExistente(pessoa);
                Console.WriteLine("Pessoa já esta cadastrada");
            }
            else
            {
                CriarNovo(pessoa);
            }
        }

        private static bool PessoaJaEstaCadastrada(Pessoa pessoa)
        {
            var id = pessoa.Id;

            var pessoaEncontrada = BuscarPessoaPelo(id);

            if (pessoaEncontrada != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static IEnumerable<Pessoa> BuscarTodasAsPessoas()
        {
            string nomeDoArquivo = ObterNomeArquivo();

            FileStream arquivo;
            if (!File.Exists(nomeDoArquivo))
            {
                arquivo = File.Create(nomeDoArquivo);
                arquivo.Close();
            }

            string resultado = File.ReadAllText(nomeDoArquivo);

            string[] pessoas = resultado.Split(';');

            List<Pessoa> listaPessoas = new List<Pessoa>();

            for (int i = 0; i < pessoas.Length - 1; i++)
            {
                string[] dadosDaPessoa = pessoas[i].Split(',');

                string nome = dadosDaPessoa[0];
                DateTime dataDeAniversario = Convert.ToDateTime(dadosDaPessoa[1]);

                Pessoa funcionario = new Pessoa(nome, dataDeAniversario);

                listaPessoas.Add(funcionario);
            }

            return listaPessoas;
        }

        private static string ObterNomeArquivo()
        {
            var pastaDesktop = Environment.SpecialFolder.Desktop;

            string localDaPastaDesktop = Environment.GetFolderPath(pastaDesktop);
            string nomeDoArquivo = @"\dadosDasPessoas.txt";

            return localDaPastaDesktop + nomeDoArquivo;
        }

        public static IEnumerable<Pessoa> BuscarTodasAsPessoas(string nome)
        {
            return (from x in BuscarTodasAsPessoas()
                    where x.Nome.Contains(nome, StringComparison.InvariantCultureIgnoreCase)
                    orderby x.Nome
                    select x);
        }

        public static Pessoa BuscarPessoaPelo(int id)
        {
            return (from x in BuscarTodasAsPessoas()
                    where x.Id == id
                    select x).FirstOrDefault();
        }

        protected static void CriarNovo(Pessoa pessoa)
        {
            string nomeDoArquivo = ObterNomeArquivo();

            string formato = $"{pessoa.Nome},{pessoa.DataDeAniversario};";

            File.AppendAllText(nomeDoArquivo, formato);
        }
    }
}

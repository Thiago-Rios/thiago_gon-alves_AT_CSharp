using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Biblioteca.Pessoa
{
    public class Repositorio : IRepositorio
    {

        public void Deletar(int id)
        {
            var pessoas = BuscarTodasAsPessoas();
            List<Pessoa> listaFiltrada = new List<Pessoa>();
            foreach(var pessoa in pessoas) 
            {
                if(pessoa.Id != id)
                {
                    listaFiltrada.Add(pessoa);
                }
            }
            RecriaArquivo(listaFiltrada);
        }

        public void Editar(Pessoa pessoaEscolhida)
        {
            var pessoas = BuscarTodasAsPessoas();
            List<Pessoa> listaFiltrada = new List<Pessoa>();
            foreach (var pessoa in pessoas)
            {
                if (pessoa.Id == pessoaEscolhida.Id)
                {
                    listaFiltrada.Add(pessoaEscolhida);
                }
                else
                {
                    listaFiltrada.Add(pessoa);
                }
            }
            RecriaArquivo(listaFiltrada);
        }

        public void Salvar(Pessoa pessoa)
        {
            if (PessoaJaEstaCadastrada(pessoa))
            {
                Console.WriteLine("Pessoa já esta cadastrada");
            }
            else
            {
                CriarNovo(pessoa);
            }
        }

        public bool PessoaJaEstaCadastrada(Pessoa pessoa)
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

        public IEnumerable<Pessoa> BuscarTodasAsPessoas()
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

                int id = int.Parse(dadosDaPessoa[0]);
                string nome = dadosDaPessoa[1];
                DateTime dataDeAniversario = Convert.ToDateTime(dadosDaPessoa[2]);

                Pessoa funcionario = new Pessoa(id, nome, dataDeAniversario);

                listaPessoas.Add(funcionario);
            }

            return listaPessoas;
        }

        public string ObterNomeArquivo()
        {
            var pastaDesktop = Environment.SpecialFolder.Desktop;

            string localDaPastaDesktop = Environment.GetFolderPath(pastaDesktop);
            string nomeDoArquivo = @"\dadosDasPessoas.txt";

            return localDaPastaDesktop + nomeDoArquivo;
        }

        public IEnumerable<Pessoa> BuscarTodasAsPessoas(string nome)
        {
            return (from x in BuscarTodasAsPessoas()
                    where x.Nome.Contains(nome, StringComparison.InvariantCultureIgnoreCase)
                    orderby x.Nome
                    select x);
        }

        public IEnumerable<Pessoa> BuscarTodasAsPessoas(DateTime dataDeHoje)
        {
            return (from x in BuscarTodasAsPessoas()
                    where x.DataDeAniversario.Day.Equals(dataDeHoje.Day) && x.DataDeAniversario.Month.Equals(dataDeHoje.Month)
                    orderby x.DataDeAniversario
                    select x);
        }

        public Pessoa BuscarPessoaPelo(int id)
        {
            return (from x in BuscarTodasAsPessoas()
                    where x.Id == id
                    select x).FirstOrDefault();
        }

        public void RecriaArquivo(List<Pessoa> pessoas)
        {
            string nomeDoArquivo = ObterNomeArquivo();

            File.Delete(nomeDoArquivo);

            FileStream arquivo;
            if (!File.Exists(nomeDoArquivo))
            {
                arquivo = File.Create(nomeDoArquivo);
                arquivo.Close();
            }

            foreach (var pessoa in pessoas)
            {
                CriarNovo(pessoa);
            }
        }

        public void CriarNovo(Pessoa pessoa)
        {
            string nomeDoArquivo = ObterNomeArquivo();

            string formato = $"{pessoa.Id},{pessoa.Nome},{pessoa.DataDeAniversario};";

            File.AppendAllText(nomeDoArquivo, formato);
        }
    }
}

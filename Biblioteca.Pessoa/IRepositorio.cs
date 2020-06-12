using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Pessoa
{
    public interface IRepositorio
    {
        void Deletar(int id);

        void Editar(Pessoa pessoaEscolhida);

        void Salvar(Pessoa pessoa);

        bool PessoaJaEstaCadastrada(Pessoa pessoa);

        IEnumerable<Pessoa> BuscarTodasAsPessoas();

        IEnumerable<Pessoa> BuscarTodasAsPessoas(string nome);

        IEnumerable<Pessoa> BuscarTodasAsPessoas(DateTime dataDeHoje);

        Pessoa BuscarPessoaPelo(int id);

        void RecriaArquivo(List<Pessoa> pessoas);

        void CriarNovo(Pessoa pessoa);
    }
}

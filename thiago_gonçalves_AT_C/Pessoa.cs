using System;
using System.Collections.Generic;
using System.Text;

namespace thiago_gonçalves_AT_C
{
    class Pessoa
    {
        public int Id { get; private set; }
        public string Nome { get; set; }
        public DateTime DataDeAniversario { get; set; }

        public Pessoa(string nome, DateTime data)
        {
            Nome = nome;
            DataDeAniversario = data;
        }
    }
}

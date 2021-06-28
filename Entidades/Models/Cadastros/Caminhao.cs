using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades.Models.Cadastros
{
    public class Caminhao
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public string Ano_Fabricacao { get; set; }
        public string Ano_Modelo { get; set; }
        public int Qtde_Estoque { get; set; }
    }
}

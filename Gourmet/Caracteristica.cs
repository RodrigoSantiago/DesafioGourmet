using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoGourmet.Gourmet
{
    [Serializable]
    public class Caracteristica
    {
        public string Nome { get; set; }

        public Caracteristica()
        {

        }

        public Caracteristica(string nome)
        {
            Nome = nome;
        }   
    }
}

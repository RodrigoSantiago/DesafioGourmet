using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoGourmet.Gourmet
{
    [Serializable]
    public class Comida
    {
        public string Nome { get; set; }
        public List<Caracteristica> Caracteristicas { get; set; }

        public Comida()
        {

        }

        public Comida(string nome) {
            Nome = nome;
            Caracteristicas = new List<Caracteristica>();
        }

        public Comida(string nome, params Caracteristica[] caracteristicas)
        {
            Nome = nome;
            Caracteristicas = caracteristicas.ToList();
        }

        public bool ContemCaracteristica(Caracteristica caracteristica)
        {
            return Caracteristicas.Any(a => a.Nome == caracteristica.Nome);
        }
    }
}

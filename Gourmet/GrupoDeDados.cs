using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoGourmet.Gourmet
{
    [Serializable]
    public class GrupoDeDados
    {
        public List<Comida> comidas = new List<Comida>();
        public List<Caracteristica> caracteristicas = new List<Caracteristica>();

        public GrupoDeDados()
        {

        }

    }
}

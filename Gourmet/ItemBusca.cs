using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoGourmet.Gourmet
{
    public class ItemBusca
    {
        public Comida comida;
        public int valor;

        public ItemBusca(Comida comida)
        {
            this.comida = comida;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace JogoGourmet.Gourmet
{
    public class Dados
    {
        public static GrupoDeDados obj;

        private static string DiretorioAtual()
        {
            string diretorioAtual = Directory.GetCurrentDirectory();
            return Path.Combine(diretorioAtual, "dados.json");
        }

        private static void Inicializa()
        {
            Caracteristica massa = new Caracteristica("massa");
            Caracteristica doce = new Caracteristica("doce");
            Caracteristica bebida = new Caracteristica("bebida");

            obj = new GrupoDeDados();

            obj.caracteristicas = new List<Caracteristica> {
                massa, doce, bebida
            };

            obj.comidas = new List<Comida> {
                new Comida("bolo", massa, doce),
                new Comida("macarrão", massa),
                new Comida("suco", bebida, doce),
            };
        }

        public static void Salvar()
        {
            string path = DiretorioAtual();
            string json = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public static void Carregar()
        {
            Debug.WriteLine(DiretorioAtual());
            try
            {
                string path = DiretorioAtual();
                string json = File.ReadAllText(path);
                obj = JsonConvert.DeserializeObject<GrupoDeDados>(json);
            } catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            if (obj == null)
            {
                Inicializa();
            }
        }

        public static void Atualiza(Comida comida, List<Caracteristica> caracteristicasPositivas)
        {
            comida.Caracteristicas.Clear();
            comida.Caracteristicas.AddRange(caracteristicasPositivas);
            Salvar();
        }

        public static void Adiciona(Comida comida, Caracteristica caracteristica) 
        {
            if (!obj.caracteristicas.Any(c => c.Nome == caracteristica.Nome))
                obj.caracteristicas.Add(caracteristica);

            obj.comidas.Add(comida);
            Salvar();
        }

        public static List<Caracteristica> ListarCaracteristicas()
        {
            return obj.caracteristicas.ToList();
        }

        public static Comida BuscaMelhorEntrada(List<Caracteristica> positivos, List<Caracteristica> negativos)
        {
            List<ItemBusca> busca = new List<ItemBusca>();
            foreach(var comida in obj.comidas)
            {
                ItemBusca itemBusca = new ItemBusca(comida);
                foreach(var positivo in positivos)
                {
                    if (comida.ContemCaracteristica(positivo))
                    {
                        itemBusca.valor++;
                    } else
                    {
                        itemBusca.valor--;
                    }
                }
                foreach (var negativo in negativos)
                {
                    if (comida.ContemCaracteristica(negativo))
                    {
                        itemBusca.valor--;
                    }
                    else
                    {
                        itemBusca.valor++;
                    }
                }
                busca.Add(itemBusca);
            }
            busca.Sort((c1, c2) => c2.valor.CompareTo(c1.valor));
            if (busca[0].valor <= -2)
            {
                return null;
            }
            return busca[0].comida;
        }
    }
}

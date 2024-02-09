using JogoGourmet.Gourmet;

namespace JogoGourmet
{
    public partial class Form1 : Form
    {

        private bool finalizado;
        private int indiceAtual;
        private Comida melhorBusca;
        private List<Caracteristica> caracteristicas = new List<Caracteristica>();
        private List<Caracteristica> caracteristicasPositivas = new List<Caracteristica>();
        private List<Caracteristica> caracteristicasNegativas = new List<Caracteristica>();

        public Form1()
        {
            InitializeComponent();
        }

        private void InicializaBusca()
        {
            finalizado = false;
            indiceAtual = 0;
            caracteristicas = Dados.ListarCaracteristicas();
            caracteristicasPositivas = new List<Caracteristica>();
            caracteristicasNegativas = new List<Caracteristica>();
            label1.Text = "Sua comida favorita é :";
            labelNome.Text = CapitalizaPrimeira(caracteristicas[indiceAtual].Nome);
        }

        private void ProximaCaracteristica(bool positivo)
        {
            if (positivo)
            {
                caracteristicasPositivas.Add(caracteristicas[indiceAtual]);
            }
            else
            {
                caracteristicasNegativas.Add(caracteristicas[indiceAtual]);
            }
            indiceAtual += 1;
            if (indiceAtual >= caracteristicas.Count)
            {
                Finaliza();
            }
            else
            {
                labelNome.Text = CapitalizaPrimeira(caracteristicas[indiceAtual].Nome);
            }
        }

        private string CapitalizaPrimeira(string str)
        {
            return str.Substring(0, 1).ToUpper() + str.Substring(1).ToLower();
        }

        private void Finaliza()
        {
            melhorBusca = Dados.BuscaMelhorEntrada(caracteristicasPositivas, caracteristicasNegativas);
            if (melhorBusca == null)
            {
                MessageBox.Show($"Não sei qual é sua comida favorita", "Finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                NovaComida();
            }
            else
            {
                label1.Text = "Eu acho que sua comida favorita é :";
                labelNome.Text = CapitalizaPrimeira(melhorBusca.Nome);
                finalizado = true;
            }
        }

        private void NovaComida()
        {
            string nome = Microsoft.VisualBasic.Interaction.InputBox(
                    "Por favor, diga qual sua comida:", "Digite sua comida favorita", "");

            string caracteristica = Microsoft.VisualBasic.Interaction.InputBox(
                "Digite uma caracteristica unica dela:", "Caracteristica", "");

            Caracteristica novaCaracteristica = new Caracteristica(caracteristica);
            Comida novaComida = new Comida(nome);
            novaComida.Caracteristicas.AddRange(caracteristicasPositivas);
            novaComida.Caracteristicas.Add(novaCaracteristica);
            Dados.Adiciona(novaComida, novaCaracteristica);

            MessageBox.Show($"Vamos tentar novamente agora!", "Finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            InicializaBusca();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InicializaBusca();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (finalizado)
            {
                MessageBox.Show($"Que bom, essa comida é mesmo deliciosa", "Finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Dados.Atualiza(melhorBusca, caracteristicasPositivas);
                InicializaBusca();
            }
            else
            {
                ProximaCaracteristica(true);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (finalizado)
            {
                NovaComida();
            }
            else
            {
                ProximaCaracteristica(false);
            }
        }
    }
}
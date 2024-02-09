using JogoGourmet.Gourmet;
using System.Diagnostics;

namespace JogoGourmet
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Dados.Carregar();

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
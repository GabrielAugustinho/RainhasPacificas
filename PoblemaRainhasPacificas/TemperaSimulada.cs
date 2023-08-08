using System;

namespace PoblemaRainhasPacificas
{
    public class TemperaSimulada
    {
        private const double TemperaturaInicial = 100.0;
        private const double TemperaturaFinal = 0.00001;
        private const double Frequencia = 0.99;

        private readonly int TamanhoTabuleiro;
        private readonly Random Aleatorio;
        private int[] SolucaoAtual;

        public TemperaSimulada(int tamanho, int[] solucaoAtual)
        {
            TamanhoTabuleiro = tamanho;
            Aleatorio = new Random();
            SolucaoAtual = solucaoAtual;
        }

        public int[] Resolver()
        {        
            int totalIteracoes = 0;
            double temperatura = TemperaturaInicial;
            int conflitosAtuais = ContarConflitos(SolucaoAtual);

            Console.WriteLine($"Numero de conflitos iniciais: {conflitosAtuais}");

            while (temperatura > TemperaturaFinal)
            {
                int[] novaSolucao = GerarVizinho();
                conflitosAtuais = ContarConflitos(SolucaoAtual);
                int novosConflitos = ContarConflitos(novaSolucao);

                if (novosConflitos == 0)
                {
                    // Encontrou uma solução válida
                    Console.WriteLine($"Numero de conflitos finais: {novosConflitos}");
                    Console.WriteLine("Total de iterações: " + totalIteracoes);
                    return novaSolucao;
                }

                double deltaE = novosConflitos - conflitosAtuais;

                if (deltaE < 0 || AceitarSolucao(deltaE, temperatura))
                {
                    SolucaoAtual = novaSolucao;
                }

                temperatura *= Frequencia;
                totalIteracoes++;
            }

            Console.WriteLine("Não foi possível encontrar uma solução válida.");
            Console.WriteLine("Total de iterações: " + totalIteracoes);
            return new int[8];           
        }

        private int[] GerarVizinho()
        {
            int[] vizinho = new int[TamanhoTabuleiro];
            Array.Copy(SolucaoAtual, vizinho, TamanhoTabuleiro);

            int linha = Aleatorio.Next(TamanhoTabuleiro);
            int coluna = Aleatorio.Next(TamanhoTabuleiro);

            vizinho[linha] = coluna;
            return vizinho;
        }

        private int ContarConflitos(int[] solucao)
        {
            int conflitos = 0;

            for (int i = 0; i < TamanhoTabuleiro - 1; i++)
            {
                for (int j = i + 1; j < TamanhoTabuleiro; j++)
                {
                    if (solucao[i] == solucao[j] || Math.Abs(i - j) == Math.Abs(solucao[i] - solucao[j]))
                    {
                        conflitos++;
                    }
                }
            }

            return conflitos;
        }

        private bool AceitarSolucao(double deltaE, double temperatura)
        {
            double probabilidade = Math.Exp(-deltaE / temperatura);
            return Aleatorio.NextDouble() < probabilidade;
        }
    }
}

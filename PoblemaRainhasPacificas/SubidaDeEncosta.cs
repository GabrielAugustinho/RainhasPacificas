using System;

namespace PoblemaRainhasPacificas
{
    public class SubidaEncosta
    {
        private readonly int TamanhoTabuleiro;
        private int[] SolucaoAtual;

        public SubidaEncosta(int tamanho, int[] solucaoAtual)
        {
            TamanhoTabuleiro = tamanho;
            SolucaoAtual = solucaoAtual;
        }

        public int[] Resolver()
        {
            int totalIteracoes = 0;
            var melhor = true;
            int conflitosAtuais = ContarConflitos(SolucaoAtual);

            Console.WriteLine($"Numero de conflitos iniciais: {conflitosAtuais}");

            while (melhor)
            {
                int[] novaSolucao = Vizinho(SolucaoAtual);
                conflitosAtuais = ContarConflitos(SolucaoAtual);
                int novosConflitos = ContarConflitos(novaSolucao);

                // Encontrou uma solução melhor
                if (novosConflitos < conflitosAtuais)
                    SolucaoAtual = novaSolucao;
                else
                    melhor = false;

                totalIteracoes++;
            }

            conflitosAtuais = ContarConflitos(SolucaoAtual);
            Console.WriteLine($"Numero de conflitos finais: {conflitosAtuais}");
            Console.WriteLine("Total de iterações: " + totalIteracoes);
            return SolucaoAtual;
        }

        private int[] Vizinho(int[] solucao)
        {
            int[] vizinho = new int[TamanhoTabuleiro];
            Array.Copy(solucao, vizinho, TamanhoTabuleiro);
            var random = new Random();

            for (int linha = 0; linha < TamanhoTabuleiro; linha++)
            {
                int colunaAleatoria = random.Next(0, TamanhoTabuleiro);
                vizinho[linha] = colunaAleatoria;
            }

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
    }
}


using System;

namespace PoblemaRainhasPacificas
{
    public class SubidaEncostaAlterada
    {
        private readonly int TamanhoTabuleiro;
        private int MaxTentativas;
        private int[] SolucaoAtual;

        public SubidaEncostaAlterada(int tamanho, int[] solucaoAtual, int maxTentativas)
        {
            TamanhoTabuleiro = tamanho;
            SolucaoAtual = solucaoAtual;
            MaxTentativas = maxTentativas;
        }

        public int[] Resolver()
        {
            bool melhor = true;
            int tentativa = 1;
            int totalIteracoes = 0;
            int conflitosAtuais = ContarConflitos(SolucaoAtual);

            Console.WriteLine($"Numero de conflitos iniciais: {conflitosAtuais}");

            while (melhor)
            {
                int[] novaSolucao = Vizinho(SolucaoAtual);
                conflitosAtuais = ContarConflitos(SolucaoAtual);
                int novosConflitos = ContarConflitos(novaSolucao);

                // Encontrou uma solução melhor
                if (novosConflitos < conflitosAtuais)
                {
                    SolucaoAtual = novaSolucao;
                    tentativa = 1;
                }
                else
                {
                    if (tentativa < MaxTentativas)
                        tentativa++;
                    else
                        melhor = false;
                }

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
            int[] melhor = new int[TamanhoTabuleiro];
            int vm, vv;

            Array.Copy(solucao, melhor, TamanhoTabuleiro);
            vm = ContarConflitos(melhor);

            var random = new Random();
            int colunaAleatoria = random.Next(0, TamanhoTabuleiro);

            for (int coluna = 0; coluna < TamanhoTabuleiro; coluna++)
            {
                Array.Copy(solucao, vizinho, TamanhoTabuleiro);
                int aux = vizinho[colunaAleatoria];
                vizinho[colunaAleatoria] = vizinho[coluna];
                vizinho[coluna] = aux;

                vv = ContarConflitos(vizinho);

                if (vv < vm)
                {
                    Array.Copy(vizinho, melhor, TamanhoTabuleiro);
                    vm = vv;
                }
            }

            return melhor;
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

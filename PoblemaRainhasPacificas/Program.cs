using System;

namespace PoblemaRainhasPacificas
{
    class Program
    {
        static void Main(string[] args)
        {
            var tamanhoTabuleiro = 8;
            var maxTentativasSEAlt = 8;
            var solucaoAtual = ColocarRainhaTabuleiro(tamanhoTabuleiro);

            Console.WriteLine("\n***** TABULEIRO INICIAL *****\n");
            ImprimirTabuleiro(solucaoAtual);

            Console.WriteLine("\n***** TABULEIRO FINAL SUBIDA DE ENCOSTA *****\n");
            ImprimirTabuleiro(new SubidaEncosta(tamanhoTabuleiro, solucaoAtual).Resolver());

            Console.WriteLine("\n***** TABULEIRO FINAL SUBIDA DE ENCOSTA ALTERADA *****\n");
            ImprimirTabuleiro(new SubidaEncostaAlterada(tamanhoTabuleiro, solucaoAtual, maxTentativasSEAlt).Resolver());

            Console.WriteLine("\n***** TABULEIRO FINAL SUBIDA DE ENCOSTA ALTERADA / 2 *****\n");
            ImprimirTabuleiro(new SubidaEncostaAlterada(tamanhoTabuleiro, solucaoAtual, maxTentativasSEAlt / 2).Resolver());

            Console.WriteLine("\n***** TABULEIRO FINAL SUBIDA DE ENCOSTA ALTERADA / 4 *****\n");
            ImprimirTabuleiro(new SubidaEncostaAlterada(tamanhoTabuleiro, solucaoAtual, maxTentativasSEAlt / 4).Resolver());

            Console.WriteLine("\n***** TABULEIRO FINAL TEMPERA SIMULADA *****\n");
            ImprimirTabuleiro(new TemperaSimulada(tamanhoTabuleiro, solucaoAtual).Resolver());            
        }

        private static int[] ColocarRainhaTabuleiro(int tamanhoTabuleiro)
        {
            var tabuleiro = new int[tamanhoTabuleiro];
            var random = new Random();

            for (int i = 0; i < tamanhoTabuleiro; i++)
            {
                int posicaoAleatoria = random.Next(tamanhoTabuleiro);
                tabuleiro[i] = posicaoAleatoria;
            }

            return tabuleiro;
        }

        private static void ImprimirTabuleiro(int[] solucao)
        {
            for (int linha = 0; linha < solucao.Length; linha++)
            {
                for (int coluna = 0; coluna < solucao.Length; coluna++)
                {
                    if (solucao[linha] == coluna)
                        Console.Write(" Q ");
                    else
                        Console.Write(" - ");
                }
                Console.WriteLine();
            }
        }
    }
}

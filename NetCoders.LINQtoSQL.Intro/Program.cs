using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoders.LINQtoSQL.Intro
{
    class Program
    {
        static void Main(string[] args)
        {
            //testar LINQ to SQL em uma aplicação Console
            ProducoesArtisticasDataContext context = new ProducoesArtisticasDataContext();

            //selecionar os filmes e agrupá-los por gênero
            var queryFilmes = from filme in context.tbFilmes
                              group filme by new { filme.Nome, filme.Genero } into filmesPorGenero
                              orderby filmesPorGenero.Key.Genero
                              select new 
                              {
                                  Nome_Filme = filmesPorGenero.Key.Nome,
                                  Genero_Filme = filmesPorGenero.Key.Genero,
                              };

            foreach (var filme in queryFilmes)
            {
                Console.WriteLine("Filme: " + filme.Nome_Filme + "\t" + "Gênero: " + filme. Genero_Filme);
            }

            var queryAtores = from ator in context.tbAtores
                              join filmes in context.tbFilmes
                              on ator.FilmeID equals filmes.FilmeID
                              select new
                              {
                                  Nome_Ator = ator.Nome,
                                  Nome_Filme = filmes.Nome
                              };

            foreach (var ator in queryAtores)
            {
                Console.WriteLine("Nome do Ator: " + ator.Nome_Ator);
                Console.WriteLine("Nome do Filme: " + ator.Nome_Filme);

                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}

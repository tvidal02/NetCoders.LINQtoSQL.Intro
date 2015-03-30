using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetCoders.LINQtoSQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //criação das colunas do grid
            dgFilmes.Columns.Add("colNomeAtor", "Ator");
            dgFilmes.Columns.Add("colNomeFilme", "Filme");

            //instanciar o contexto
            ProducoesArtisticasDataContext context = new ProducoesArtisticasDataContext();

            //aplicar a query

            //var filmes = from f in context.tbFilmes
            //             select f;

            //seleciona atores que tenham participado de algum filme
            var queryAtores = from ator in context.tbAtores
                              join filmes in context.tbFilmes
                              on ator.FilmeID equals filmes.FilmeID
                              select new 
                              {
                                  Nome_Ator = ator.Nome,
                                  Nome_Filme = filmes.Nome
                              };

            int idLinha = 0;

            foreach (var ator in queryAtores)
            {
                DataGridViewRow linha = new DataGridViewRow();
                dgFilmes.Rows.Add(linha);
                dgFilmes.Rows[idLinha].Cells[0].Value = ator.Nome_Filme;
                dgFilmes.Rows[idLinha].Cells[1].Value = ator.Nome_Ator;

                idLinha++;
            }

            dgFilmes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }  
    }
}

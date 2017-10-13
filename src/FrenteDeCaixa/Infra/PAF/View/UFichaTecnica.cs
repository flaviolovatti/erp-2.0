/********************************************************************************
Title: T2TiPDV
Description: Janela para cadastros de produtos produzidos pelo estabelecimento.

The MIT License

Copyright: Copyright (C) 2012 T2Ti.COM

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.

       The author may be contacted at:
           alberteije@gmail.com

@author T2Ti.COM
@version 1.0
********************************************************************************/


using System.Collections.Generic;
using System.Windows.Forms;
using PafEcf.VO;
using PafEcf.Controller;
using System;
using PafEcf.Util;

namespace PafEcf.View
{

    public partial class FFichaTecnica : Form
    {

        private static List<ProdutoVO> ListaProduto;
        private static List<ProdutoVO> ListaProducao;
        private static List<FichaTecnicaVO> ListaComposicao;

        public FFichaTecnica()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
            ListaProduto = new List<ProdutoVO>();
            ListaProducao = new List<ProdutoVO>();
            ListaComposicao = new List<FichaTecnicaVO>();
            GridPrincipal.AutoGenerateColumns = false;
            GridProducao.AutoGenerateColumns = false;
            GridComposicao.AutoGenerateColumns = false;
            EditLocaliza.Focus();
        }

        private void SpeedButton1_Click(object sender, System.EventArgs e)
        {
            string ProcurePor = "%" + EditLocaliza.Text + "%";
            ListaProduto = new ProdutoController().TabelaProduto(ProcurePor);
            GridPrincipal.DataSource = ListaProduto;
            if (ListaProduto.Count > 0)
                GridPrincipal.Focus();
        }

        private void EditLocaliza_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                SpeedButton1.PerformClick();
        }

        private void SpeedButton2_Click(object sender, System.EventArgs e)
        {
            string ProcurePor = "%" + editLocalizaProducao.Text + "%";
            ListaProducao = new ProdutoController().TabelaProduto(ProcurePor);
            GridProducao.DataSource = ListaProducao;
        }

        private void editLocalizaProducao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                SpeedButton2.PerformClick();
        }


        private void CarregarFichaTecnica()
        {
            try
            {
                ListaComposicao = new FichaTecnicaController().TabelaFichaTecnica(ListaProduto[GridPrincipal.CurrentRow.Index].Id.ToString());
                GridComposicao.DataSource = ListaComposicao;
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        private void btnAdicionar_Click(object sender, System.EventArgs e)
        {
            if ((ListaProduto.Count > 0) && (ListaProducao.Count > 0))
            {
                FichaTecnicaVO Ficha = new FichaTecnicaVO();
                Ficha.IdProduto = ListaProduto[GridPrincipal.CurrentRow.Index].Id;
                Ficha.Descricao = ListaProducao[GridProducao.CurrentRow.Index].Nome;
                Ficha.IdProdutoFilho = ListaProducao[GridProducao.CurrentRow.Index].Id;
                Ficha.Quantidade = Convert.ToDecimal(editQuantidade.Text);
                if (new FichaTecnicaController().GravaFichaTecnica(Ficha))
                {
                    CarregarFichaTecnica();
                }
            }
            else
                MessageBox.Show("Selecione um Produto para ser Produzido e um Outro para Compor a Produção.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (ListaComposicao.Count > 0)
            {
                new FichaTecnicaController().ExcluiFichaTecnica(ListaComposicao[GridComposicao.CurrentRow.Index].Id);
            }
            CarregarFichaTecnica();
        }

        private void botaoConfirma_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void botaoCancela_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FFichaTecnica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                botaoConfirma.PerformClick();
            if (e.KeyCode == Keys.Escape)
                botaoCancela.PerformClick();
        }

        private void GridPrincipal_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            CarregarFichaTecnica();
        }
    }

}

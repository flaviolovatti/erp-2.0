/********************************************************************************
Title: T2TiPDV
Description: Pesquisa por produto e importacao para a venda.

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


using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using PafEcf.VO;
using System.Collections.Generic;
using PafEcf.Controller;

namespace PafEcf.View
{

    public partial class FImportaProduto : Form
    {

        private static List<ProdutoVO> ListaProduto;

        public FImportaProduto()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
            EditLocaliza.Focus();
            ListaProduto = new List<ProdutoVO>();
            GridPrincipal.AutoGenerateColumns = false;
        }

        private void botaoConfirma_Click(object sender, EventArgs e)
        {
            Confirma();
        }

        private void botaoCancela_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SpeedButton1_Click(object sender, EventArgs e)
        {
            Localiza();
        }

        private void FImportaProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                Localiza();
            if (e.KeyCode == Keys.F12)
                botaoConfirma.PerformClick();
            if (e.KeyCode == Keys.Escape)
                botaoCancela.PerformClick();
        }

        public void Localiza()
        {
            string ProcurePor = "%" + EditLocaliza.Text + "%";
            ListaProduto = new ProdutoController().TabelaProduto(ProcurePor);
            Label2.Text = ListaProduto.Count.ToString() + " produtos localizados.";
            GridPrincipal.DataSource = ListaProduto;
            if (ListaProduto.Count > 0)
                GridPrincipal.Focus();
        }

        public void Confirma()
        {
            if (ListaProduto.Count > 0)
            {
                if (ListaProduto[GridPrincipal.CurrentRow.Index].ValorVenda > 0)
                {
                    if ((FCaixa.StatusCaixa == 0) || (FCaixa.StatusCaixa == 1) || (FCaixa.StatusCaixa == 2))
                    {
                        FCaixa.EditCodigo.Text = ListaProduto[GridPrincipal.CurrentRow.Index].GTIN;
                    }
                    else if (FCaixa.StatusCaixa == 5)
                    {
                        /*
                        FNotaFiscal.editCodigo.Text = Convert.ToString(((DataRowView)CDSProduto.Current).Row["ID"]);
                        FNotaFiscal.editUnitario.Value = Convert.ToDouble(((DataRowView)CDSProduto.Current).Row["VALOR_VENDA"]);
                         */
                    }
                }
                else
                    MessageBox.Show("Produto n�o pode ser vendido com valor zerado ou negativo.", "Informa��o do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }

        private void GridPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                EditLocaliza.Focus();
        }

    }

}

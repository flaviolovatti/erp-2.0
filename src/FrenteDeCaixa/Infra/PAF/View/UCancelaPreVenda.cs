/* *******************************************************************************
  Title: T2TiPDV
  Description: Cancela Pre-Venda.

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
  ******************************************************************************* */


using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using PafEcf.VO;
using System.Collections.Generic;
using PafEcf.Controller;
using PafEcf.Util;

namespace PafEcf.View
{

    //TODO:  Analise com calma o procedimento abaixo. Debugue. Corrija o que for necessário.

    public partial class FCancelaPreVenda : Form
    {

        private static List<PreVendaCabecalhoVO> ListaPreVendaCabecalho;
        private static List<PreVendaDetalheVO> ListaPreVendaDetalhe;
        private static PreVendaController PreVendaController;

        public FCancelaPreVenda()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
            ListaPreVendaCabecalho = new List<PreVendaCabecalhoVO>();
            ListaPreVendaDetalhe = new List<PreVendaDetalheVO>();
            PreVendaController = new PreVendaController();
            GridMestre.AutoGenerateColumns = false;
            GridDetalhe.AutoGenerateColumns = false;
            CarregarCabecalho();
            GridMestre.Focus();
        }

        private void botaoConfirma_Click(object sender, EventArgs e)
        {
            Confirma();
        }

        private void botaoCancela_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FCancelaPreVenda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                botaoConfirma.PerformClick();
            if (e.KeyCode == Keys.Escape)
                botaoCancela.PerformClick();
        }

        private void Confirma()
        {
            if (ListaPreVendaCabecalho.Count > 0)
            {
                int PVSelecionada = ListaPreVendaCabecalho[GridMestre.CurrentRow.Index].Id;
                if (MessageBox.Show("Tem certeza que deseja cancelar a Pre-Venda selecionada?", "Cancelar Pre-Venda", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    PreVendaController.CancelaPreVendasPendentes(PVSelecionada);
                    this.Close();
                }
            }
            else
                MessageBox.Show("Não existem Pre-Vendas disponiveis para cancelamento.", "Informação do Sistema", MessageBoxButtons.OK);
        }


        private void CarregarCabecalho()
        {
            try
            {
                ListaPreVendaCabecalho = PreVendaController.TabelaPreVendaCabecalho();
                GridMestre.DataSource = ListaPreVendaCabecalho;
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }

        private void CarregarDetalhe()
        {
            try
            {
                ListaPreVendaDetalhe = PreVendaController.TabelaPreVendaDetalhe(ListaPreVendaCabecalho[GridMestre.CurrentRow.Index].Id.ToString());
                GridDetalhe.DataSource = ListaPreVendaDetalhe;
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }

        private void GridMestre_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            CarregarDetalhe();
        }

    }


}

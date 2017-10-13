/* *******************************************************************************
  Title: T2TiPDV
  Description: Mescla duas ou mais Pre-Vendas.

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


using System.Drawing;
using System.Windows.Forms;
using PafEcf.VO;
using System.Collections.Generic;
using PafEcf.Controller;
using PafEcf.Util;
using System;

namespace PafEcf.View
{

    //TODO:  debugue com calma todo o procedimento dessa janela e acompanhe o Log. Corrija o que for necessário.
    //TODO:  avalie a necessidade/vantagem de usar Dataset/Datatable nessa janela.
    
    public partial class FMesclaPreVenda : Form
	{

        private static List<PreVendaCabecalhoVO> ListaPreVendaCabecalho;
        private static List<PreVendaDetalheVO> ListaPreVendaDetalhe;
        private static PreVendaController PreVendaController;
        
        public FMesclaPreVenda()
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

        private void FMesclaPreVenda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                botaoConfirma.PerformClick();
            if (e.KeyCode == Keys.Escape)
                botaoCancela.PerformClick();
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


        private void Confirma()
        {
            if (MessageBox.Show("Tem certeza que deseja mesclar as Pré-Vendas selecionadas?", "Mesclar Pre-Venda", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                FCaixa.LabelMensagens.Text = "Aguarde. Mesclando Pré-Venda!";
                List<PreVendaCabecalhoVO> ListaPreVendaCabecalhoMescla = new List<PreVendaCabecalhoVO>();
                List<PreVendaDetalheVO> ListaPreVendaDetalheMescla = new List<PreVendaDetalheVO>();
                PreVendaCabecalhoVO PreVendaCabecalho;
                PreVendaDetalheVO PreVendaDetalhe;

                for (int i = 0; i <= GridMestre.Rows.Count - 1; i++)
                {
                    if (ListaPreVendaCabecalho[i].Situacao == "X")
                    {
                        PreVendaCabecalho = new PreVendaCabecalhoVO();
                        PreVendaCabecalho.Id = ListaPreVendaCabecalho[i].Id;
                        PreVendaCabecalho.Valor = ListaPreVendaCabecalho[i].Valor;
                        ListaPreVendaCabecalhoMescla.Add(PreVendaCabecalho);

                        for (int j = 0; j <= GridDetalhe.Rows.Count - 1; j++)
                        {
                            //TODO:  Observe que não temos todos esses itens na lista. Implemente isso no Controller ao carregar a tabela de detalhe.
                            PreVendaDetalhe = new PreVendaDetalheVO();
                            PreVendaDetalhe.IdPreVenda = PreVendaCabecalho.Id;
                            PreVendaDetalhe.Id = ListaPreVendaDetalhe[j].Id;
                            PreVendaDetalhe.IdProduto = ListaPreVendaDetalhe[j].IdProduto;
                            PreVendaDetalhe.GtinProduto = ListaPreVendaDetalhe[j].GtinProduto;
                            PreVendaDetalhe.NomeProduto = ListaPreVendaDetalhe[j].NomeProduto;
                            PreVendaDetalhe.UnidadeProduto = ListaPreVendaDetalhe[j].UnidadeProduto;
                            PreVendaDetalhe.Cancelado = ListaPreVendaDetalhe[j].Cancelado;
                            PreVendaDetalhe.Quantidade = ListaPreVendaDetalhe[j].Quantidade;
                            PreVendaDetalhe.ValorUnitario = ListaPreVendaDetalhe[j].ValorUnitario;
                            PreVendaDetalhe.ValorTotal = ListaPreVendaDetalhe[j].ValorTotal;
                            ListaPreVendaDetalheMescla.Add(PreVendaDetalhe);
                        }
                    }
                }

                if (ListaPreVendaDetalheMescla.Count < 1)
                    MessageBox.Show("Nenhum item selecionado!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    PreVendaController.MesclaPreVenda(ListaPreVendaCabecalhoMescla, ListaPreVendaDetalheMescla);
                    FCaixa.LabelMensagens.Text = "Venda em andamento.";
                    this.Close();
                }

            }
        }

        private void GridMestre_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Space)
                {
                    if (ListaPreVendaCabecalho[GridMestre.CurrentRow.Index].Situacao == null)
                    {
                        ListaPreVendaCabecalho[GridMestre.CurrentRow.Index].Situacao = "X";
                    }
                    else
                    {
                        ListaPreVendaCabecalho[GridMestre.CurrentRow.Index].Situacao = null;
                    }
                }
                GridMestre.DataSource = ListaPreVendaCabecalho;
                GridMestre.Refresh();
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }

	}
	
}

/* *******************************************************************************
  Title: T2TiPDV
  Description: Mescla dois ou mais DAVs.

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
    //TODO:  para cumprir os requisitos é necessário permitir a seleção dos itens. Implemente essa funcionalidade.
    //TODO:  avalie a necessidade/vantagem de usar Dataset/Datatable nessa janela.

    public partial class FMesclaDAV : Form
    {

        private static List<DavCabecalhoVO> ListaDavCabecalho;
        private static List<DavDetalheVO> ListaDavDetalhe;
        private static DAVController DAVController;

        public FMesclaDAV()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
            ListaDavCabecalho = new List<DavCabecalhoVO>();
            ListaDavDetalhe = new List<DavDetalheVO>();
            DAVController = new DAVController();
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

        private void FMesclaDAV_KeyDown(object sender, KeyEventArgs e)
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
                ListaDavCabecalho = DAVController.TabelaDavCabecalho();
                GridMestre.DataSource = ListaDavCabecalho;
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
                ListaDavDetalhe = DAVController.TabelaDavDetalhe(ListaDavCabecalho[GridMestre.CurrentRow.Index].Id.ToString());
                GridDetalhe.DataSource = ListaDavDetalhe;
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
            decimal ValorNovoDav = 0;
            if (MessageBox.Show("Tem certeza que deseja mesclar os DAV selecionados?", "Mesclar DAV", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (EditDestinatario.Text.Trim() == "")
                {
                    MessageBox.Show("Preencha o Nome do Destinatario!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    EditDestinatario.Focus();
                    return;
                }
                if ((!Biblioteca.ValidaCPF(editCpfCnpj.Text)) && (!Biblioteca.ValidaCNPJ(editCpfCnpj.Text)))
                {
                    MessageBox.Show("Documento Invalido! Favor Corrigir", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    editCpfCnpj.Focus();
                    return;
                }

                FCaixa.LabelMensagens.Text = "Aguarde. Mesclando DAV!";
                List<DavCabecalhoVO> ListaDAVCabecalhoMescla = new List<DavCabecalhoVO>();
                List<DavDetalheVO> ListaDAVDetalheMescla = new List<DavDetalheVO>();
                DavCabecalhoVO DAVCabecalho;
                DavDetalheVO DAVDetalhe;

                for (int i = 0; i <= GridMestre.Rows.Count - 1; i++)
                {
                    if (ListaDavCabecalho[i].Situacao == "X")
                    {
                        DAVCabecalho = new DavCabecalhoVO();
                        DAVCabecalho.Id = ListaDavCabecalho[i].Id;
                        DAVCabecalho.NomeDestinatario = EditDestinatario.Text;
                        DAVCabecalho.CpfCnpjDestinatario = editCpfCnpj.Text;
                        DAVCabecalho.Valor = ListaDavCabecalho[i].Valor;
                        ListaDAVCabecalhoMescla.Add(DAVCabecalho);

                        for (int j = 0; j <= GridDetalhe.Rows.Count - 1; j++)
                        {
                            //TODO:  Observe que não temos todos esses itens na lista. Implemente isso no Controller ao carregar a tabela de detalhe.
                            DAVDetalhe = new DavDetalheVO();
                            DAVDetalhe.IdDavCabecalho = DAVCabecalho.Id;
                            DAVDetalhe.Id = ListaDavDetalhe[j].Id;
                            DAVDetalhe.IdProduto = ListaDavDetalhe[j].IdProduto;
                            DAVDetalhe.GtinProduto = ListaDavDetalhe[j].GtinProduto;
                            DAVDetalhe.NomeProduto = ListaDavDetalhe[j].NomeProduto;
                            DAVDetalhe.TotalizadorParcial = ListaDavDetalhe[j].TotalizadorParcial;
                            DAVDetalhe.UnidadeProduto = ListaDavDetalhe[j].UnidadeProduto;
                            DAVDetalhe.Cancelado = ListaDavDetalhe[j].Cancelado;
                            DAVDetalhe.Quantidade = ListaDavDetalhe[j].Quantidade;
                            DAVDetalhe.ValorUnitario = ListaDavDetalhe[j].ValorUnitario;
                            DAVDetalhe.ValorTotal = ListaDavDetalhe[j].ValorTotal;
                            if (DAVDetalhe.Cancelado == "N")
                                ValorNovoDav = Biblioteca.TruncaValor(ValorNovoDav + DAVDetalhe.ValorTotal, Constantes.DECIMAIS_VALOR);
                            ListaDAVDetalheMescla.Add(DAVDetalhe);
                        }
                    }

                }


                if (ListaDAVDetalheMescla.Count < 1)
                    MessageBox.Show("Nenhum item selecionado!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    DAVController.MesclaDAV(ListaDAVCabecalhoMescla, ListaDAVDetalheMescla, ValorNovoDav);
                    FCaixa.LabelMensagens.Text = "Venda em andamento.";
                    this.Close();
                }

            }
        }


        public void GridMestreKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Space)
                {
                    if (ListaDavCabecalho[GridMestre.CurrentRow.Index].Situacao == null)
                    {
                        ListaDavCabecalho[GridMestre.CurrentRow.Index].Situacao = "X";
                    }
                    else
                    {
                        ListaDavCabecalho[GridMestre.CurrentRow.Index].Situacao = null;
                    }
                }
                GridMestre.DataSource = ListaDavCabecalho;
                GridMestre.Refresh();
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }

    }


}

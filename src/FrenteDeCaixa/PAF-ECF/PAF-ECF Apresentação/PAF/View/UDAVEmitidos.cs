/********************************************************************************
Title: T2TiPDV
Description: Janela que gera relatorio de DAVs emitidos.

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
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using PafEcf.VO;
using PafEcf.Controller;
using PafEcf.Infra;
using PafEcf.Util;
using ACBrFramework.PAF;

namespace PafEcf.View
{


    public partial class FDavEmitidos : Form
    {

        public FDavEmitidos()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
            mkeDataIni.Text = DateTime.Now.ToString("dd/MM/yyyy");
            mkeDataFim.Text = DateTime.Now.ToString("dd/MM/yyyy");
            mkeDataIni.Focus();
            cbRelatorioGerencial.Checked = true;
        }

        private void FDavEmitidos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                Confirma();
            if (e.KeyCode == Keys.Escape)
                botaoCancela.PerformClick();
        }

        private void botaoConfirma_Click(object sender, EventArgs e)
        {
            Confirma();
        }

        private void botaoCancela_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void Confirma()
        {
            DAVController DAVController = new DAVController();
            List<DavCabecalhoVO> ListaDAV;
            List<DavDetalheVO> ListaDavDetalhe;

            string Numero, DataEmissao, Titulo, Valor, CCF, Mensagem, Tripa;

            // relatorio gerencial
            if (cbRelatorioGerencial.Checked)
            {
                if (FCaixa.StatusCaixa != 3)
                {
                    if (MessageBox.Show("Deseja imprimir o relatorio DAV EMITIDOS?", "Pergunta do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        ListaDAV = DAVController.ListaDAVPeriodo(Convert.ToDateTime(mkeDataIni.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(mkeDataFim.Text).ToString("yyyy-MM-dd"));
                        if (ListaDAV != null)
                        {
                            FDataModule.ACBrECF.AbreRelatorioGerencial(FCaixa.Configuracao.DavEmitidos);
                            FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));
                            FDataModule.ACBrECF.LinhaRelatorioGerencial("DAV EMITIDOS");
                            FDataModule.ACBrECF.LinhaRelatorioGerencial("PERIODO: " + mkeDataIni.Text + " A " + mkeDataFim.Text);
                            FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));
                            FDataModule.ACBrECF.LinhaRelatorioGerencial("NUMERO     EMISSAO    TITULO               VALOR");
                            FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));
                            for (int i = 0; i <= ListaDAV.Count - 1; i++)
                            {
                                Numero = ListaDAV[i].NumeroDav + " ";
                                DataEmissao = ListaDAV[i].DataEmissao + " ";
                                Titulo = "ORCAMENTO ";
                                Valor = ListaDAV[i].Valor.ToString("###,##0.00");
                                CCF = Convert.ToString(ListaDAV[i].Ccf);
                                CCF = new string('0', 3 - CCF.Length) + CCF;
                                Valor = new string(' ', 13 - Valor.Length) + Valor;
                                FDataModule.ACBrECF.LinhaRelatorioGerencial(Numero + DataEmissao + Titulo + "   " + Valor);
                            }
                            FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));
                            FDataModule.ACBrECF.FechaRelatorio();
                            UPAF.GravaR06("RG");
                        }
                        else
                            MessageBox.Show("Não existem DAV para o periodo informado.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }// if FCaixa.StatusCaixa <> 3 then
                else
                    MessageBox.Show("Terminal em Estado Somente Consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }// if radiogroup1.ItemIndex = 0 then

            // geracao de arquivo
            if (cbGeracaodeArquivo.Checked)
            {
                if (MessageBox.Show("Deseja gerar o arquivo de DAV EMITIDOS?", "Pergunta do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ListaDAV = DAVController.ListaDAVPeriodo(Convert.ToDateTime(mkeDataIni.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(mkeDataFim.Text).ToString("yyyy-MM-dd"));
                    if (ListaDAV != null)
                    {
                        EmpresaVO Empresa = new EmpresaController().PegaEmpresa(FCaixa.Configuracao.IdEmpresa);

                        //  registro D1
                        FDataModule.ACBrPAF.PAF_D.RegistroD1.CNPJ = Empresa.Cnpj;
                        FDataModule.ACBrPAF.PAF_D.RegistroD1.IE = Empresa.InscricaoEstadual;
                        FDataModule.ACBrPAF.PAF_D.RegistroD1.IM = Empresa.Cnpj;
                        FDataModule.ACBrPAF.PAF_D.RegistroD1.UF = Empresa.Uf;
                        FDataModule.ACBrPAF.PAF_D.RegistroD1.RazaoSocial = Empresa.RazaoSocial;

                        //  registro D2
                        FDataModule.ACBrPAF.PAF_D.RegistroD2.Clear();
                        // dados da impressora
                        ImpressoraVO Impressora = new ImpressoraController().PegaImpressora(FCaixa.Configuracao.IdImpressora);
                        for (int i = 0; i <= ListaDAV.Count - 1; i++)
                        {
                            Tripa = Convert.ToString(ListaDAV[i].Id) +
                                      Convert.ToString(ListaDAV[i].IdPessoa) +
                                      Convert.ToString(ListaDAV[i].Ccf) +
                                      Convert.ToString(ListaDAV[i].Coo) +
                                      ListaDAV[i].NomeDestinatario +
                                      ListaDAV[i].CpfCnpjDestinatario +
                                      ListaDAV[i].DataEmissao +
                                      ListaDAV[i].HoraEmissao +
                                      ListaDAV[i].Situacao +
                                      Biblioteca.FormataFloat("V", ListaDAV[i].TaxaAcrescimo) +
                                      Biblioteca.FormataFloat("V", ListaDAV[i].Acrescimo) +
                                      Biblioteca.FormataFloat("V", ListaDAV[i].TaxaDesconto) +
                                      Biblioteca.FormataFloat("V", ListaDAV[i].Desconto) +
                                      Biblioteca.FormataFloat("V", ListaDAV[i].Subtotal) +
                                      Biblioteca.FormataFloat("V", ListaDAV[i].Valor) +
                                      ListaDAV[i].NumeroDav +
                                      ListaDAV[i].NumeroEcf +
                                      Convert.ToString(ListaDAV[i].HashIncremento);

                            ACBrPAFRegistroD2 D2 = new ACBrPAFRegistroD2();

                            if (Biblioteca.MD5String(Tripa) != ListaDAV[i].HashTripa)
                                D2.RegistroValido = false;
                            D2.NUM_FAB = Impressora.Serie;
                            D2.MF_ADICIONAL = Impressora.MFD;
                            D2.TIPO_ECF = Impressora.Tipo;
                            D2.MARCA_ECF = Impressora.Marca;
                            D2.MODELO_ECF = Impressora.Modelo;
                            D2.COO = Convert.ToString(ListaDAV[i].Coo);
                            D2.NUMERO_ECF = ListaDAV[i].NumeroEcf;
                            D2.NOME_CLIENTE = ListaDAV[i].NomeDestinatario;
                            D2.CPF_CNPJ = ListaDAV[i].CpfCnpjDestinatario;
                            D2.NUM_DAV = ListaDAV[i].NumeroDav;
                            D2.DT_DAV = Convert.ToDateTime(ListaDAV[i].DataEmissao);
                            D2.TIT_DAV = "ORCAMENTO";
                            D2.VLT_DAV = ListaDAV[i].Valor;
                            FDataModule.ACBrPAF.PAF_D.RegistroD2.Add(D2);
                            D2 = null;

                            // registro D3
                            ListaDavDetalhe = DAVController.ListaDavDetalhe(ListaDAV[i].Id);
                            if (ListaDavDetalhe != null)
                            {
                                for (int j = 0; j <= ListaDavDetalhe.Count - 1; j++)
                                {
                                    Tripa = Convert.ToString(ListaDavDetalhe[j].Id) +
                                              Convert.ToString(ListaDavDetalhe[j].IdDavCabecalho) +
                                              Convert.ToString(ListaDavDetalhe[j].IdProduto) +
                                              ListaDavDetalhe[j].NumeroDav +
                                              ListaDavDetalhe[j].DataEmissao +
                                              Convert.ToString(ListaDavDetalhe[j].Item) +
                                              Biblioteca.FormataFloat("V", ListaDavDetalhe[j].Quantidade) +
                                              Biblioteca.FormataFloat("V", ListaDavDetalhe[j].ValorUnitario) +
                                              Biblioteca.FormataFloat("V", ListaDavDetalhe[j].ValorTotal) +
                                              ListaDavDetalhe[j].Cancelado +
                                              ListaDavDetalhe[j].MesclaProduto +
                                              ListaDavDetalhe[j].GtinProduto +
                                              ListaDavDetalhe[j].NomeProduto +
                                              ListaDavDetalhe[j].TotalizadorParcial +
                                              ListaDavDetalhe[j].UnidadeProduto +
                                              Convert.ToString(ListaDavDetalhe[j].HashIncremento);

                                    ACBrPAFRegistroD3 D3 = new ACBrPAFRegistroD3();
                                    if (Biblioteca.MD5String(Tripa) != ListaDavDetalhe[j].HashTripa)
                                        D3.RegistroValido = false;
                                    D3.DT_INCLUSAO = Convert.ToDateTime(ListaDAV[i].DataEmissao);
                                    D3.NUM_ITEM = ListaDavDetalhe[j].Item;
                                    D3.COD_ITEM = ListaDavDetalhe[j].GtinProduto;
                                    D3.DESC_ITEM = (ListaDavDetalhe[j].NomeProduto);

                                    D3.QTDE_ITEM = ListaDavDetalhe[j].Quantidade;
                                    D3.UNI_ITEM = ListaDavDetalhe[j].UnidadeProduto;
                                    D3.VL_UNIT = ListaDavDetalhe[j].ValorUnitario;
                                    D3.VL_DESCTO = 0;
                                    D3.VL_ACRES = 0;
                                    //D3.COD_TCTP = ListaDavDetalhe[j].TotalizadorParcial;
                                    D3.IND_CANC = ListaDavDetalhe[j].Cancelado;
                                    D3.VL_TOTAL = ListaDavDetalhe[j].ValorTotal;
                                    FDataModule.ACBrPAF.PAF_D.RegistroD2[i].RegistroD3.Add(D3);
                                    D3 = null;
                                }
                            }
                        }

                        FDataModule.ACBrPAF.Path = Application.StartupPath + "\\";
                        FDataModule.ACBrPAF.SaveFileTXT_D("PAF_D.txt");

                        Mensagem = "Arquivo armazenado em: " + Application.StartupPath + "\\PAF_D.txt";
                        MessageBox.Show(Mensagem, "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Não existem DAV para o periodo informado.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

    }

}

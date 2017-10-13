using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using PafEcf.Util;

namespace PafEcf.View
{
    public partial class FMenuOperacoes : Form
    {
        public FMenuOperacoes()
        {
            InitializeComponent();
            listaMenuOperacoes.Focus();
            listaMenuOperacoes.SelectedIndex = 0;
        }

        private void FMenuOperacoes_FormClosed(object sender, FormClosedEventArgs e)
        {
            FCaixa.MenuAberto = false;
        }

        private void FMenuOperacoes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void listaMenuOperacoes_KeyDown(object sender, KeyEventArgs e)
        {
            string RegistraPreVenda = "";
            try
            {
                XmlDocument ArquivoXML = new XmlDocument();
                ArquivoXML.Load(Application.StartupPath + "\\ArquivoAuxiliar.xml");
                RegistraPreVenda = Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("registraPreVenda").Item(0).InnerText.Trim());
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (RegistraPreVenda == "REGISTRA")
                {
                    //  carrega pre-venda
                    if (listaMenuOperacoes.SelectedIndex == 0)
                    {
                        if (FCaixa.StatusCaixa == 0)
                        {
                            FImportaNumero FImportaNumero = new FImportaNumero();
                            FImportaNumero.Text = "Carrega Pre-Venda";
                            FImportaNumero.LabelEntrada.Text = "Informe o numero da Pre-Venda";
                            try
                            {
                                if (FImportaNumero.ShowDialog() == DialogResult.OK)
                                {
                                    this.Close();
                                    FCaixa.CarregaPreVenda(FImportaNumero.EditEntrada.Text);
                                }
                            }
                            catch (Exception eError)
                            {
                                Log.write(eError.ToString());
                            }
                        } //  if StatusCaixa = 0 then
                        else
                            MessageBox.Show("Já existe uma venda em andamento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                    //  mesclar pre-venda
                    if (listaMenuOperacoes.SelectedIndex == 1)
                    {
                        FMesclaPreVenda FMesclaPreVenda = new FMesclaPreVenda();
                        FMesclaPreVenda.ShowDialog();
                        this.Close();
                    }


                    //  cancela pre-venda
                    if (listaMenuOperacoes.SelectedIndex == 2)
                    {
                        FCancelaPreVenda FCancelaPreVenda = new FCancelaPreVenda();
                        FCancelaPreVenda.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Sistema não está configurado para operações com Pré-Vendas.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                //  carrega dav
                if (listaMenuOperacoes.SelectedIndex == 3)
                {
                    if (FCaixa.StatusCaixa == 0)
                    {
                        FCarregaDAV FCarregaDAV = new FCarregaDAV();
                        try
                        {
                            if (FCarregaDAV.ShowDialog() == DialogResult.OK)
                            {
                                this.Close();
                                FCaixa.CarregaDAV(FCarregaDAV.NumeroSelecionado);
                            }
                        }
                        catch (Exception eError)
                        {
                            Log.write(eError.ToString());
                        }
                    }
                    else
                        MessageBox.Show("Ja existe uma venda em andamento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                //  mesclar DAVs
                if (listaMenuOperacoes.SelectedIndex == 4)
                {
                    FMesclaDAV FMesclaDAV = new FMesclaDAV();
                    FMesclaDAV.ShowDialog();
                    this.Close();
                }

            }
        }
    }
}

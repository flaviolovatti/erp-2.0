

using System;
using System.Windows.Forms;
using PafEcf.VO;
using PafEcf.Controller;
using PafEcf.Util;

namespace PafEcf.View
{

    //TODO:  Caso vá trabalhar com teclado reduzido, compreenda e complete este formulário

    public partial class FLocaliza : Form
    {

        public FLocaliza()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
        }

        private void FLocaliza_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                this.Close();
            if ((e.KeyValue == 97) || (e.KeyValue == 49))
                btnProdutos.PerformClick();
            if ((e.KeyValue == 98) || (e.KeyValue == 50))
                btnClientes.PerformClick();
            if ((e.KeyValue == 99) || (e.KeyValue == 51))
                btnVendedor.PerformClick();
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            /*
            FImportaProduto FImportaProduto = new FImportaProduto();
            FImportaProduto.ShowDialog();
            if ((FCaixa.StatusCaixa == 1) && (FCaixa.EditCodigo.Text.Trim() != ""))
            {
                this.Close();
                FCaixa.EditCodigo.Focus();
                FCaixa.IniciaVendaDeItens();
            }
             */
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            /*
            if (FCaixa.StatusCaixa != 3)
            {
                ClienteVO Cliente = new ClienteVO();
                if (FCaixa.Movimento != null)
                {
                    if (FCaixa.StatusCaixa == 0)
                    {
                        FIdentificaCliente FIdentificaCliente = new FIdentificaCliente();
                        FIdentificaCliente.ShowDialog();
                        if (Cliente.CpfOuCnpj != "")
                            FCaixa.IniciaVenda();
                        else
                            Cliente = null;
                    }
                    else
                        MessageBox.Show("Ja existe venda em andamento. Cancele o cupom e inicie nova venda.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Não existe um movimento aberto.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Terminal em Estado Somente Consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
             */
        }

        private void btnVendedor_Click(object sender, EventArgs e)
        {
            if (FCaixa.StatusCaixa != 3)
            {
                if (FCaixa.StatusCaixa == 1)
                {
                    FImportaNumero FImportaNumero = new FImportaNumero();
                    FImportaNumero.Text = "Identifica Vendedor";
                    FImportaNumero.LabelEntrada.Text = "Informe o codigo do vendedor";
                    try
                    {
                        if (FImportaNumero.ShowDialog() == DialogResult.OK)
                        {
                            FuncionarioVO Vendedor = new VendedorController().ConsultaVendedor(Convert.ToInt32(FImportaNumero.EditEntrada.Text));
                            if (Vendedor.Id != 0)
                                FCaixa.VendaCabecalho.IdVendedor = Vendedor.Id;
                            else
                                MessageBox.Show("Vendedor: codigo invalido ou inexistente.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception eError)
                    {
                        Log.write(eError.ToString());
                    }
                }
                else
                    MessageBox.Show("Não existe venda em andamento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Terminal em Estado Somente Consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }


}

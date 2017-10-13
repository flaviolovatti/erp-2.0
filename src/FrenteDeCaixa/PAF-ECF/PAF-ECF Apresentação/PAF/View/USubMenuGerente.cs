using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PafEcf.Infra;
using PafEcf.Util;

namespace PafEcf.View
{
    public partial class FSubMenuGerente : Form
    {
        public FSubMenuGerente()
        {
            InitializeComponent();
            listaGerente.Focus();
            listaGerente.SelectedIndex = 0;
        }

        private void listaGerente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }


            if (e.KeyCode == Keys.Enter)
            {
                //  inicia movimento
                if (listaGerente.SelectedIndex == 0)
                    FCaixa.IniciaMovimento();
                //  encerra movimento
                if (listaGerente.SelectedIndex == 1)
                    FCaixa.EncerraMovimento();
                //  suprimento
                if (listaGerente.SelectedIndex == 3)
                    FCaixa.Suprimento();
                //  sangria
                if (listaGerente.SelectedIndex == 4)
                    FCaixa.Sangria();
                //  Reducao Z
                if (listaGerente.SelectedIndex == 6)
                {
                    if (MessageBox.Show("Tem Certeza Que Deseja Executar a Reducao Z?" + "\r" + "\r" + "O Movimento da Impressora será Suspenso no dia de Hoje.", "Reducao Z", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            UECF.ReducaoZ();
                        }
                        finally
                        {
                            FCaixa.TelaPadrao(2);
                        }
                    }
                }

                //  consultar cliente
                if (listaGerente.SelectedIndex == 8)
                {
                    FImportaCliente FImportaCliente = new FImportaCliente();
                    FImportaCliente.QuemChamou = "SUBMENU";
                    FImportaCliente.ShowDialog();
                }
                //  configuracao do sistema
                if (listaGerente.SelectedIndex == 10)
                {
                    /*
                     * Utilize o Sistema Configurador
                    FConfiguracao FConfiguracao = new FConfiguracao();
                    FConfiguracao.ShowDialog();
                    */
                }
                //  funções administrativas do TEF Discado
                if (listaGerente.SelectedIndex == 12)
                {
                    
                    FEfetuaPagamento FEfetuaPagamento = new FEfetuaPagamento();
                    try
                    {
                        FEfetuaPagamento.ACBrTEFD.Initializar(ACBrFramework.TEFD.TefTipo.TefDial);
                        FEfetuaPagamento.ACBrTEFD.ADM(ACBrFramework.TEFD.TefTipo.TefDial);
                    }
                    catch (Exception eError)
                    {
                        Log.write(eError.ToString());
                        MessageBox.Show("Problemas no GP TEFDIAL.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    FEfetuaPagamento.Dispose();
                    
                }
                //  funções administrativas do TECBAN
                if (listaGerente.SelectedIndex == 13)
                {
                    FEfetuaPagamento FEfetuaPagamento = new FEfetuaPagamento();
                    try
                    {
                        FEfetuaPagamento.ACBrTEFD.Initializar(ACBrFramework.TEFD.TefTipo.TefDisc);
                        FEfetuaPagamento.ACBrTEFD.ADM(ACBrFramework.TEFD.TefTipo.TefDisc);
                    }
                    catch (Exception eError)
                    {
                        Log.write(eError.ToString());
                        MessageBox.Show("Problemas no GP TECBAN.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    FEfetuaPagamento.Dispose();
                }
                //  funções administrativas do HIPER
                if (listaGerente.SelectedIndex == 14)
                {
                    FEfetuaPagamento FEfetuaPagamento = new FEfetuaPagamento();
                    try
                    {
                        FEfetuaPagamento.ACBrTEFD.Initializar(ACBrFramework.TEFD.TefTipo.HiperTef);
                        FEfetuaPagamento.ACBrTEFD.ADM(ACBrFramework.TEFD.TefTipo.HiperTef);
                    }
                    catch (Exception eError)
                    {
                        Log.write(eError.ToString());
                        MessageBox.Show("Problemas no GP HIPER.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    FEfetuaPagamento.Dispose();
                }

                //  Importar Tabelas com Dispositivo (pen-drive)
                if (listaGerente.SelectedIndex == 16)
                {
                    FPenDrive FPenDrive = new FPenDrive();
                    FPenDrive.Rotina = "IMPORTA";
                    FPenDrive.ShowDialog();
                }

                //  Exportar Tabelas com Dispositivo (pen-drive)
                if (listaGerente.SelectedIndex == 17)
                {
                    FPenDrive FPenDrive = new FPenDrive();
                    FPenDrive.Rotina = "EXPORTA";
                    FPenDrive.ShowDialog();
                }

            }

        }
    }
}

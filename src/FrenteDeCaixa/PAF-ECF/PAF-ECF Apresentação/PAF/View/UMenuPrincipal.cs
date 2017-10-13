using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PafEcf.Controller;

namespace PafEcf.View
{
    public partial class FMenuPrincipal : Form
    {
        public FMenuPrincipal()
        {
            InitializeComponent();
            listaMenuPrincipal.Focus();
            listaMenuPrincipal.SelectedIndex = 0;
        }

        private void listaMenuPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                //  chama submenu do supervisor
                if (listaMenuPrincipal.SelectedIndex == 0)
                {
                    FLoginGerenteSupervisor FLoginGerenteSupervisor = new FLoginGerenteSupervisor();
                    try
                    {
                        FLoginGerenteSupervisor.ComboCargo.SelectedIndex = 1;
                        if (FLoginGerenteSupervisor.ShowDialog() == DialogResult.OK)
                        {
                            if (FLoginGerenteSupervisor.LoginOK)
                            {
                                FSubMenuSupervisor FSubMenuSupervisor = new FSubMenuSupervisor();
                                FSubMenuSupervisor.SetBounds(this.Left, this.Top + 198, 467, 212);
                                FSubMenuSupervisor.ShowDialog();
                            }
                            else
                                MessageBox.Show("Supervisor - dados incorretos.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    finally
                    {
                    }
                }

                //  chama submenu do gerente
                if (listaMenuPrincipal.SelectedIndex == 1)
                {
                    FLoginGerenteSupervisor FLoginGerenteSupervisor = new FLoginGerenteSupervisor();
                    try
                    {
                        FLoginGerenteSupervisor.ComboCargo.SelectedIndex = 0;
                        if (FLoginGerenteSupervisor.ShowDialog() == DialogResult.OK)
                        {
                            if (FLoginGerenteSupervisor.LoginOK)
                            {
                                FSubMenuGerente FSubMenuGerente = new FSubMenuGerente();
                                FSubMenuGerente.SetBounds(this.Left, this.Top + 198, 467, 212);
                                FSubMenuGerente.ShowDialog();
                            }
                            else
                                MessageBox.Show("Gerente - dados incorretos.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    finally
                    {
                    }
                }

                //  saida temporaria
                if (listaMenuPrincipal.SelectedIndex == 2)
                {
                    if (FCaixa.StatusCaixa == 0)
                    {
                        if (MessageBox.Show("Deseja fechar o caixa temporariamente?", "Fecha o caixa temporariamente", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            new MovimentoController().SaidaTemporaria(FCaixa.Movimento);
                            FMovimentoAberto FMovimentoAberto = new FMovimentoAberto();
                            FMovimentoAberto.ShowDialog();
                        }
                    }
                    else
                        MessageBox.Show("Status do caixa não permite saida temporaria.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


                //  receber carga da retaguarda
                //TODO:  Melhore esse procedimento fazendo com que a carga seja automática - Threads
                if (listaMenuPrincipal.SelectedIndex == 3)
                {
                    FCargaPDV FCargaPDV = new FCargaPDV();
                    FCargaPDV.Tipo = "importa";
                    FCargaPDV.ShowDialog();
                }
            }

        }

        private void FMenuPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void FMenuPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            FCaixa.MenuAberto = false;
        }
    }
}

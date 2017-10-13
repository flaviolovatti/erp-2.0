using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PafEcf.Infra;

namespace PafEcf.View
{
    public partial class FSubMenuSupervisor : Form
    {
        public FSubMenuSupervisor()
        {
            InitializeComponent();
            listaSupervisor.Focus();
            listaSupervisor.SelectedIndex = 0;
        }

        private void listaSupervisor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }


            if (e.KeyCode == Keys.Enter)
            {
                //  inicia movimento
                if (listaSupervisor.SelectedIndex == 0)
                    FCaixa.IniciaMovimento();
                //  encerra movimento
                if (listaSupervisor.SelectedIndex == 1)
                    FCaixa.EncerraMovimento();
                //  suprimento
                if (listaSupervisor.SelectedIndex == 3)
                    FCaixa.Suprimento();
                //  sangria
                if (listaSupervisor.SelectedIndex == 4)
                    FCaixa.Sangria();
                //  Reducao Z
                if (listaSupervisor.SelectedIndex == 6)
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
            }

        }
    }
}

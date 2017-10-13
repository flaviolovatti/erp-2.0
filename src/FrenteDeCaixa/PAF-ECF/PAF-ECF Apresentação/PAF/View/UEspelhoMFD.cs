/* *******************************************************************************
  Title: T2TiPDV
  Description: Janela para geracao do Espelho MDF

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
using System.Drawing;
using System.Windows.Forms;
using PafEcf.Util;

namespace PafEcf.View
{
	
	public partial class FEspelhoMfd : Form
	{
		
		public FEspelhoMfd()
		{
			// Required for Windows Form Designer support
			InitializeComponent();
            this.cbPeriododeData.Checked = true;
        }

        private void FEspelhoMfd_Activated(object sender, EventArgs e)
        {
            mkeDataIni.Text = FDataModule.ACBrECF.DataHora.ToString("dd/MM/yyyy");
            mkeDataFim.Text = FDataModule.ACBrECF.DataHora.ToString("dd/MM/yyyy");
            editFim.Text = FDataModule.ACBrECF.NumCOO;
            mkeDataIni.Focus();
        }

        private void FEspelhoMfd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                Confirma();
            if (e.KeyCode == Keys.Escape)
                botaoCancela.PerformClick();
        }


        private void mkeDataFim_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(mkeDataFim.Text) < Convert.ToDateTime(mkeDataIni.Text))
            {
                MessageBox.Show("Data Final deve ser Maior que Data Inicial", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mkeDataFim.Focus();
            }
        }

        private void editFim_Leave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(editFim.Text) < Convert.ToInt32(editInicio.Text))
            {
                MessageBox.Show("COO Final deve ser Maior que COO Inicial", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mkeDataFim.Focus();
            }
        }

        private void cbPeriododeData_Click(object sender, EventArgs e)
        {
            panCOO.Enabled = false;
            panCOO.BorderStyle = BorderStyle.None;
            panPeriodo.Enabled = true;
            panPeriodo.BorderStyle = BorderStyle.Fixed3D;
        }

        private void cbIntervalodeCOO_Click(object sender, EventArgs e)
        {
            panCOO.Enabled = true;
            panCOO.BorderStyle = BorderStyle.Fixed3D;
            panPeriodo.Enabled = false;
            panPeriodo.BorderStyle = BorderStyle.None;
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
            string Mensagem;
            System.DateTime aData;
            if (MessageBox.Show("Deseja gerar o espelho da MFD - Memória Fita Detalhe?", "Pergunta do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //  por data
                string NomeArquivo = Application.StartupPath + "\\EspelhoMFD.txt";


                if (cbPeriododeData.Checked)
                {
                    aData = Convert.ToDateTime(mkeDataFim.Text);
                    if (aData >= FDataModule.ACBrECF.DataHora)
                    {
                        Mensagem = "Data Final Precisa Ser Menor Que " + FDataModule.ACBrECF.DataHora.ToString();
                        MessageBox.Show(Mensagem, "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    try
                    {
                        FDataModule.ACBrECF.PafMF_MFD_Espelho(Convert.ToDateTime(mkeDataIni.Text), Convert.ToDateTime(mkeDataFim.Text), NomeArquivo);
                    }
                    catch (Exception eError)
                    {
                        Log.write(eError.ToString());
                    }
                }

                //  por reducao
                else if (cbIntervalodeCOO.Checked)
                {
                    if ((Convert.ToInt32(editFim.Text)) > (Convert.ToInt32(FDataModule.ACBrECF.NumCOO)))
                    {
                        Mensagem = "Número do COO não Pode Ser Maior Que " + FDataModule.ACBrECF.NumCOO;
                        MessageBox.Show(Mensagem, "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    try
                    {
                        FDataModule.ACBrECF.PafMF_MFD_Espelho(Convert.ToInt32(editInicio.Text), Convert.ToInt32(editFim.Text), NomeArquivo);
                    }
                    catch (Exception eError)
                    {
                        Log.write(eError.ToString());
                    }
                }
                //FDataModule.ACBrPAF.AssinarArquivoComEAD(NomeArquivo);
                Biblioteca.AssinarComOpenSsl(NomeArquivo);

                Mensagem = "Arquivo armazenado em: " + Application.StartupPath + "\\" + NomeArquivo;
                MessageBox.Show(Mensagem, "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
	}
	
}

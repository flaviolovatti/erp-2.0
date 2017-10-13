/********************************************************************************
Title: T2TiPDV
Description: Leitura da Memória fiscal simplificada.

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
using PafEcf.Util;

namespace PafEcf.View
{

    public partial class FLmfs : Form
    {
        string NomeArquivo = "";

        public FLmfs()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
            this.cbAImpressaododocumentonoECF.Checked = true;
            this.cbPeriododeData.Checked = true;
        }

        private void FLmfs_Activated(object sender, EventArgs e)
        {
            mkeDataIni.Text = DateTime.Now.ToString("dd/MM/yyyy");
            mkeDataFim.Text = DateTime.Now.ToString("dd/MM/yyyy");
            mkeDataIni.Focus();
        }

        private void FLmfs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                Confirma();
            if (e.KeyCode == Keys.Escape)
                botaoCancela.PerformClick();
        }

        private void cbIntervaloCRZ_Click(object sender, EventArgs e)
        {
            panCRZ.Enabled = true;
            panCRZ.BorderStyle = BorderStyle.Fixed3D;
            panPeriodo.Enabled = false;
            panPeriodo.BorderStyle = BorderStyle.None;
        }

        private void cbPeriododeData_Click(object sender, EventArgs e)
        {
            panCRZ.Enabled = false;
            panCRZ.BorderStyle = BorderStyle.None;
            panPeriodo.Enabled = true;
            panPeriodo.BorderStyle = BorderStyle.Fixed3D;
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
            // impressao do documento no ECF
            if (cbAImpressaododocumentonoECF.Checked)
            {
                if (MessageBox.Show("Deseja imprimir a LMFS - Leitura Memória Fiscal Simplificada?", "Pergunta do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // por data
                    if (cbPeriododeData.Checked)
                        FDataModule.ACBrECF.LeituraMemoriaFiscal(Convert.ToDateTime(mkeDataIni.Text), Convert.ToDateTime(mkeDataFim.Text), true);
                    // por reducao;
                    else if (cbIntervaloCRZ.Checked)
                        FDataModule.ACBrECF.LeituraMemoriaFiscal(Convert.ToInt32(editInicio.Text), Convert.ToInt32(editFim.Text), true);
                }
            }

            // Gravacao de arquivo eletronico no formato de espelho
            if (cbBGravacaodearquivoeletroniconoformatodeespelho.Checked)
            {
                if (MessageBox.Show("Deseja gerar o arquivo da LMFS - Formato Espelho?", "Pergunta do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    NomeArquivo = Application.StartupPath + "\\LMFS_Completa.txt";
                    // por data
                    if (cbPeriododeData.Checked)
                    {
                        FDataModule.ACBrECF.LeituraMemoriaFiscalSerial(Convert.ToDateTime(mkeDataIni.Text), Convert.ToDateTime(mkeDataFim.Text), NomeArquivo, true);
                    }
                    // por reducao
                    else if (cbIntervaloCRZ.Checked)
                    {
                        FDataModule.ACBrECF.LeituraMemoriaFiscalSerial(Convert.ToInt32(editInicio.Text), Convert.ToInt32(editFim.Text), NomeArquivo, true);
                    }
                }
                //FDataModule.ACBrPAF.AssinarArquivoComEAD(NomeArquivo);
                Biblioteca.AssinarComOpenSsl(NomeArquivo);

                Mensagem = "Arquivo armazenado em: " + NomeArquivo;
                MessageBox.Show(Mensagem, "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

    }


}

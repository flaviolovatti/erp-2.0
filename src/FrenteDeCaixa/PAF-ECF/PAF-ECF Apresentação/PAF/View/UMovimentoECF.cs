/********************************************************************************
Title: T2TiPDV
Description: Tela para emiss?o do Movimento por ECF

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
using PafEcf.VO;
using System.Collections.Generic;
using PafEcf.Controller;
using PafEcf.Infra;

namespace PafEcf.View
{

    public partial class FMovimentoECF : Form
    {
        List<ImpressoraVO> ListaImpressora;

        public FMovimentoECF()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
            mkeDataIni.Text = DateTime.Now.ToString("dd/MM/yyyy");
            mkeDataFim.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ComboImpressora.Focus();
            //
            ListaImpressora = ListaImpressora = new ImpressoraController().TabelaImpressora();
            for (int i = 0; i <= ListaImpressora.Count - 1; i++)
                ComboImpressora.Items.Add(ListaImpressora[i].Identificacao);
            ComboImpressora.SelectedIndex = 0;
        }

        private void botaoCancela_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void botaoConfirma_Click(object sender, EventArgs e)
        {
            Confirma();
        }

        private void Confirma()
        {
            if (MessageBox.Show("Deseja gerar o arquivo eletronico de movimento?", "Pergunta do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ImpressoraVO Impressora = ListaImpressora[ComboImpressora.SelectedIndex];
                UPAF.GeraMovimentoECF(Convert.ToDateTime(mkeDataIni.Text).ToString("yyyy-MM-dd"), Convert.ToDateTime(mkeDataFim.Text).ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"), Impressora);
            }
        }

        private void FMovimentoECF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                Confirma();
            if (e.KeyCode == Keys.Escape)
                botaoCancela.PerformClick();
        }

    }

}

/* *******************************************************************************
  Title: T2TiPDV
  Description: Janela Menu Fiscal que deve ser chamada de qualquer lugar da
  aplicação

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
using System.Windows.Forms;
using PafEcf.Infra;

namespace PafEcf.View
{

    public partial class FMenuFiscal : Form
    {

        public FMenuFiscal()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
        }


        public void FormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                botaoCancela.PerformClick();
            if (e.KeyCode == Keys.F12)
                botaoConfirma.PerformClick();
        }

        private void botaoConfirma_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void botaoCancela_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLX_Click(object sender, EventArgs e)
        {
            if (FCaixa.StatusCaixa != 3)
            {
                if (MessageBox.Show("Confirma a emissão da Leitura X?", "Emissão de Leitura X", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    UECF.LeituraX();
            }
            else
                MessageBox.Show("Terminal em Estado Somente Consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLMFC_Click(object sender, EventArgs e)
        {
            FLmfc FLmfc = new FLmfc();
            FLmfc.ShowDialog();
        }

        private void btnLMFS_Click(object sender, EventArgs e)
        {
            FLmfs FLmfs = new FLmfs();
            FLmfs.ShowDialog();
        }

        private void btnEspelhoMFD_Click(object sender, EventArgs e)
        {
            if (FDataModule.ACBrECF.MFD)
            {
                FEspelhoMfd FEspelhoMfd = new FEspelhoMfd();
                FEspelhoMfd.ShowDialog();
            }
            else
                MessageBox.Show("Funcao não suportada pelo modelo de ECF utilizado", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnArqMFD_Click(object sender, EventArgs e)
        {
            if (FDataModule.ACBrECF.MFD)
            {
                FArquivoMfd FArquivoMfd = new FArquivoMfd();
                FArquivoMfd.ShowDialog();
            }
            else
                MessageBox.Show("Funcao não suportada pelo modelo de ECF utilizado", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnTabProd_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja gerar o arquivo da Tabela de Produtos?", "Pergunta do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                UPAF.GeraTabelaProdutos();
        }

        private void btnEstoque_Click(object sender, EventArgs e)
        {
            FEstoque FEstoque = new FEstoque();
            FEstoque.ShowDialog();
        }

        private void btnMovimentoEcf_Click(object sender, EventArgs e)
        {
            FMovimentoECF FMovimentoECF = new FMovimentoECF();
            FMovimentoECF.ShowDialog();
        }

        private void btnMeiosPagto_Click(object sender, EventArgs e)
        {
            if (FCaixa.StatusCaixa != 3)
            {
                FMeiosPagamento FMeiosPagamento = new FMeiosPagamento();
                FMeiosPagamento.ShowDialog();
            }
            else
                MessageBox.Show("Terminal em Estado Somente Consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDavEmitidos_Click(object sender, EventArgs e)
        {
            FDavEmitidos FDavEmitidos = new FDavEmitidos();
            FDavEmitidos.ShowDialog();
        }

        private void btnIdentificacaoPafEcf_Click(object sender, EventArgs e)
        {
            if (FCaixa.StatusCaixa != 3)
            {
                if (MessageBox.Show("Deseja imprimir o relatorio IDENTIFICAÇÃO DO PAF-ECF?", "Pergunta do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    UPAF.IdentificacaoPafEcf();
            }
            else
                MessageBox.Show("Terminal em Estado Somente Consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnVendasPeriodo_Click(object sender, EventArgs e)
        {
            FVendasPeriodo FVendasPeriodo = new FVendasPeriodo();
            FVendasPeriodo.ShowDialog();
        }

        private void btnIndiceTecnico_Click(object sender, EventArgs e)
        {
            FFichaTecnica FFichaTecnica = new FFichaTecnica();
            FFichaTecnica.ShowDialog();
        }

        private void btnParametrosConfig_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja imprimir o relatorio PARÂMETROS DE CONFIGURAÇÃO?", "Pergunta do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                UPAF.ParametrodeConfiguracao();
        }

        private void FMenuFiscal_FormClosed(object sender, FormClosedEventArgs e)
        {
            FCaixa.MenuAberto = false;
        }

    }

}


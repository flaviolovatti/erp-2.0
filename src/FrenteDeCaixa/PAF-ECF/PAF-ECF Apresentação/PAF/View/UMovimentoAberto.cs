/********************************************************************************
Title: T2TiPDV
Description: Detecta um movimento aberto e solicita autenticacao.

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
using PafEcf.Controller;
using PafEcf.Util;

namespace PafEcf.View
{

    public partial class FMovimentoAberto : Form
    {

        public bool PodeFechar;
        MovimentoVO Movimento;

        public FMovimentoAberto()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
            this.editSenhaOperador.Focus();
            this.PodeFechar = false;
            Movimento = new MovimentoController().VerificaMovimento();
            LabelTurno.Text = Movimento.DescricaoTurno;
            LabelTerminal.Text = Movimento.NomeCaixa;
            LabelOperador.Text = Movimento.LoginOperador;
            LabelImpressora.Text = Movimento.IdentificacaoImpressora;
            LabelData.Text = Movimento.DataAbertura.ToString();
            LabelHora.Text = Movimento.HoraAbertura;
        }

        private void botaoCancela_Click(object sender, EventArgs e)
        {
            if (FCaixa.MenuAberto)
                Application.Exit();
            FCaixa.StatusCaixa = 4;
            this.Close();
        }

        private void FMovimentoAberto_FormClosed(object sender, FormClosedEventArgs e)
        {
            //  A Variavel chamada AcionaMenu foi criada para que o acionamento
            //  dos menus (com excecao do Menu Fiscal) so possa ser feito depois de logar no sistema.
            // TODO:  analise o código e verifique se existe a necessidade desse controle
            FCaixa.AcionaMenu = true;
        }

        public void botaoConfirmaClick(object Sender)
        {
            Confirma();
        }

        private void Confirma()
        {
            try
            {
                //  verifica se senha do operador esta correta
                OperadorVO Operador = new OperadorController().ConsultaUsuario(Movimento.LoginOperador, editSenhaOperador.Text);
                if (Operador != null)
                {
                    PodeFechar = true;
                    if (Movimento.Status == "T")
                    {
                        new MovimentoController().RetornoOperador(Movimento);
                    }
                    this.Close();
                    FCaixa.LabelCaixa.Text = "Terminal: " + Movimento.NomeCaixa;
                    FCaixa.LabelOperador.Text = "Operador: " + Movimento.LoginOperador;
                }
                else
                {
                    MessageBox.Show("Operador: dados incorretos.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    editSenhaOperador.Focus();
                }
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        public void FormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Confirma();
            if (e.KeyCode == Keys.F12)
                Confirma();
            if (e.KeyCode == Keys.Escape)
                botaoCancela.PerformClick();
        }

        private void botaoConfirma_Click(object sender, EventArgs e)
        {
            Confirma();
        }

    }

}

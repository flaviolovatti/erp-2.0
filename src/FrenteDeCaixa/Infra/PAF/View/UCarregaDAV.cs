/********************************************************************************
Title: T2TiPDV
Description: Carrega DAVs.

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


using System.Drawing;
using System.Windows.Forms;
using PafEcf.VO;
using System.Collections.Generic;
using PafEcf.Controller;
using PafEcf.Util;
using System;

namespace PafEcf.View
{


    public partial class FCarregaDAV : Form
    {

        private static List<DavCabecalhoVO> ListaDavCabecalho;
        private static List<DavDetalheVO> ListaDavDetalhe;
        private static DAVController DAVController;
        public static string NumeroSelecionado;

        public FCarregaDAV()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
            ListaDavCabecalho = new List<DavCabecalhoVO>();
            ListaDavDetalhe = new List<DavDetalheVO>();
            DAVController = new DAVController();
            GridMestre.AutoGenerateColumns = false;
            GridDetalhe.AutoGenerateColumns = false;
            CarregarCabecalho();
            GridMestre.Focus();
        }

        private void botaoConfirma_Click(object sender, System.EventArgs e)
        {
            Confirma();
        }

        private void botaoCancela_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void TFCarregaDAV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                botaoConfirma.PerformClick();
            if (e.KeyCode == Keys.Escape)
                botaoCancela.PerformClick();
        }

        private void Confirma()
        {
            if (ListaDavCabecalho.Count > 0)
            {
                NumeroSelecionado = ListaDavCabecalho[GridMestre.CurrentRow.Index].NumeroDav;
                this.Close();
            }
            else
                MessageBox.Show("Não existem DAVs disponiveis para cancelamento.", "Informação do Sistema", MessageBoxButtons.OK);
        }


        private void CarregarCabecalho()
        {
            try
            {
                ListaDavCabecalho = DAVController.TabelaDavCabecalho();
                GridMestre.DataSource = ListaDavCabecalho;
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }

        private void CarregarDetalhe()
        {
            try
            {
                ListaDavDetalhe = DAVController.TabelaDavDetalhe(ListaDavCabecalho[GridMestre.CurrentRow.Index].Id.ToString());
                GridDetalhe.DataSource = ListaDavDetalhe;
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }

        private void GridMestre_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            CarregarDetalhe();
        }
    }

}

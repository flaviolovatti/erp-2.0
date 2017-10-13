/********************************************************************************
Title: T2TiPDV
Description: Encerra um movimento aberto.

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
using System.Data;
using PafEcf.VO;
using System.Collections.Generic;
using PafEcf.Controller;
using PafEcf.Util;
using PafEcf.Infra;

namespace PafEcf.View
{

    public partial class FEncerraMovimento : Form
    {

        private static DataTable DTFechamento;
        private static DataTable DTResumo;
        public static bool FechouMovimento, AbreMovimento, PodeFechar;
        public static MovimentoVO Movimento;
        private static TipoPagamentoController TipoPagamentoController;
        private static TotalTipoPagamentoController TotalTipoPagamentoController;
        private static MovimentoController MovimentoController;
        private static FechamentoController FechamentoController;
        private static OperadorController OperadorController;

        public FEncerraMovimento()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
            ConfiguraDataSet();
            TipoPagamentoController = new TipoPagamentoController();
            TotalTipoPagamentoController = new TotalTipoPagamentoController();
            MovimentoController = new MovimentoController();
            FechamentoController = new FechamentoController();
            OperadorController = new OperadorController();
            FormCreate();
            ComboTipoPagamento.Focus();

            //TODO:  Carregue os valores de fechamento já gravados no banco
        }

        private void ConfiguraDataSet()
        {
            // Tabela Fechamento
            DTFechamento = new DataTable("FECHAMENTO");
            DTFechamento.Columns.Add("TIPO_PAGAMENTO", typeof(string));
            DTFechamento.Columns.Add("VALOR", typeof(decimal));
            DTFechamento.Columns.Add("ID", typeof(int));
            dataSet.Tables.Add(DTFechamento);

            GridValores.DataSource = dataSet;
            GridValores.DataMember = dataSet.Tables["FECHAMENTO"].TableName;

            //definimos os cabeçalhos da Grid
            GridValores.Columns[0].HeaderText = "Tipo Pagamento";
            GridValores.Columns[0].ReadOnly = true;
            GridValores.Columns[1].HeaderText = "Valor";
            GridValores.Columns[1].ReadOnly = true;
            GridValores.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            GridValores.Columns[2].Visible = false;


            //Tabela Resumo
            DTResumo = new DataTable("RESUMO");
            DTResumo.Columns.Add("TIPO_PAGAMENTO", typeof(string));
            DTResumo.Columns.Add("CALCULADO", typeof(decimal));
            DTResumo.Columns.Add("DECLARADO", typeof(decimal));
            DTResumo.Columns.Add("DIFERENCA", typeof(decimal));
            dataSet.Tables.Add(DTResumo);
        }


        public void FormCreate()
        {
            List<TipoPagamentoVO> ListaTipoPagamento;
            PodeFechar = false;
            AbreMovimento = true;
            FechouMovimento = false;
            Movimento = MovimentoController.VerificaMovimento();
            LabelTurno.Text = Movimento.DescricaoTurno;
            LabelTerminal.Text = Movimento.NomeCaixa;
            LabelOperador.Text = Movimento.LoginOperador;
            LabelImpressora.Text = Movimento.IdentificacaoImpressora;

            TotalizaFechamento();
            try
            {
                ListaTipoPagamento = TipoPagamentoController.TabelaTipoPagamento();
                for (int i = 0; i <= ListaTipoPagamento.Count - 1; i++)
                    ComboTipoPagamento.Items.Add(ListaTipoPagamento[i].Descricao);
                ComboTipoPagamento.SelectedIndex = 0;
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (ComboTipoPagamento.Text.Trim() == "")
            {
                MessageBox.Show("Informe uma forma de Pagamento valida!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ComboTipoPagamento.Focus();
                return;
            }

            if (Convert.ToDecimal(edtValor.Text) <= 0)
            {
                MessageBox.Show("Informe um Valor valido!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                edtValor.Focus();
                return;
            }

            try
            {
                FechamentoVO Fechamento = new FechamentoVO();
                Fechamento.IdMovimento = Movimento.Id;
                Fechamento.TipoPagamento = ComboTipoPagamento.Text;
                Fechamento.Valor = Convert.ToDecimal(edtValor.Text);

                DataRow Registro = DTFechamento.NewRow();
                Registro["TIPO_PAGAMENTO"] = Fechamento.TipoPagamento;
                Registro["VALOR"] = Convert.ToDecimal(Fechamento.Valor.ToString("0.00"));

                Fechamento = FechamentoController.GravaFechamento(Fechamento);
                if (Fechamento != null)
                {
                    Registro["ID"] = Fechamento.Id;
                    DTFechamento.Rows.Add(Registro);
                    TotalizaFechamento();
                }
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
            edtValor.Clear();
            ComboTipoPagamento.Focus();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {

            if (DTFechamento.Rows.Count > 0)
            {
                DataRow Registro = DTFechamento.Rows[GridValores.CurrentRow.Index];
                if (FechamentoController.ExcluiFechamento(Convert.ToInt32(Registro["ID"])))
                {
                    DTFechamento.Rows.RemoveAt(GridValores.CurrentRow.Index);
                    TotalizaFechamento();
                }
            }
            ComboTipoPagamento.Focus();
        }


        private void Confirma()
        {
            try
            {
                OperadorVO Operador = new OperadorVO();
                OperadorVO Gerente = new OperadorVO();

                //  verifica se senha do operador esta correta
                Operador = OperadorController.ConsultaUsuario(Movimento.LoginOperador, editSenhaOperador.Text);
                if (Operador != null)
                {
                    //  verifica se senha do gerente esta correta
                    Gerente = OperadorController.ConsultaUsuario(editLoginGerente.Text, editSenhaGerente.Text);
                    if (Gerente != null)
                    {
                        if ((Gerente.Nivel == "G") || (Gerente.Nivel == "S"))
                        {
                            // encerra movimento
                            Movimento.DataFechamento = FDataModule.ACBrECF.DataHora;
                            Movimento.HoraFechamento = FDataModule.ACBrECF.DataHora.ToString("hh:mm:ss");
                            Movimento.Status = "F";
                            MovimentoController.EncerraMovimento(Movimento);
                            Movimento = MovimentoController.VerificaMovimento(Movimento.Id);
                            ImprimeFechamento();
                            FCaixa.Movimento = Movimento;
                            MessageBox.Show("Movimento encerrado com sucesso.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FechouMovimento = true;
                            PodeFechar = true;
                            botaoConfirma.DialogResult = DialogResult.OK;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Gerente ou Supervisor: nivel de acesso incorreto.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            editLoginGerente.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Gerente ou Supervisor: dados incorretos.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        editLoginGerente.Focus();
                    }
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

        private void editSenhaGerente_Leave(object sender, EventArgs e)
        {
            botaoConfirma.Focus();
        }

        private void edtValor_Leave(object sender, EventArgs e)
        {
            if (edtValor.Text.Trim() == "")
                editSenhaOperador.Focus();
        }

        private void FEncerraMovimento_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !PodeFechar;
            if (AbreMovimento)
            {
                //TODO:  Abra a janela Inicia Movimento depois de encerrar o movimento
                //FIniciaMovimento FIniciaMovimento = new FIniciaMovimento();
                //FIniciaMovimento.ShowDialog();
            }
        }

        private void FEncerraMovimento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                botaoConfirma.PerformClick();
            if (e.KeyCode == Keys.Escape)
                botaoCancela.PerformClick();
        }

        private void botaoConfirma_Click(object sender, EventArgs e)
        {
            Confirma();
            FCaixa.StatusCaixa = 3;
            FCaixa.LabelMensagens.Text = "Terminal em Estado Somente Consulta";

        }

        private void botaoCancela_Click(object sender, EventArgs e)
        {
            PodeFechar = true;
            this.Close();
        }


        public void TotalizaFechamento()
        {
            decimal Total = 0;
            foreach (DataRow Registro in DTFechamento.Rows)
            {
                Total = Total + Convert.ToDecimal(Registro["VALOR"]);
            }
            edtTotal.Text = Total.ToString("0.00");
        }

        public void ImprimeFechamento()
        {
            List<MeiosPagamentoVO> TotalGerado;
            List<MeiosPagamentoVO> TotalDeclarado;
            string Calculado, Declarado, Diferenca;
            decimal TotCalculado, TotDeclarado, TotDiferenca;
            string Suprimento, Sangria, NaoFiscal, TotalVenda, Desconto,
           Acrescimo, Recebido, Troco, Cancelado, TotalFinal;

            try
            {
                FDataModule.ACBrECF.AbreRelatorioGerencial(FCaixa.Configuracao.GerencialX);
                FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));
                FDataModule.ACBrECF.LinhaRelatorioGerencial("             FECHAMENTO DE CAIXA                ");
                FDataModule.ACBrECF.PulaLinhas(1);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("DATA DE ABERTURA  : " + Movimento.DataAbertura);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("HORA DE ABERTURA  : " + Movimento.HoraAbertura);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("DATA DE FECHAMENTO: " + Movimento.DataFechamento);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("HORA DE FECHAMENTO: " + Movimento.HoraFechamento);
                FDataModule.ACBrECF.LinhaRelatorioGerencial(Movimento.NomeCaixa + "  OPERADOR: " + Movimento.LoginOperador);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("MOVIMENTO: " + Convert.ToString(Movimento.Id));
                FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));
                FDataModule.ACBrECF.PulaLinhas(1);

                //TODO:  Trate os valores nulos
                Suprimento = Movimento.TotalSuprimento.Value.ToString("0.00");
                Sangria = Movimento.TotalSangria.Value.ToString("0.00");
                NaoFiscal = "0.00";//Movimento.TotalNaoFiscal.Value.ToString("0.00");
                TotalVenda = Movimento.TotalVenda.Value.ToString("0.00");
                Desconto = Movimento.TotalDesconto.Value.ToString("0.00");
                Acrescimo = Movimento.TotalAcrescimo.Value.ToString("0.00");
                Recebido = Movimento.TotalRecebido.Value.ToString("0.00");
                Troco = Movimento.TotalTroco.Value.ToString("0.00");
                Cancelado = Movimento.TotalCancelado.Value.ToString("0.00");
                TotalFinal = Movimento.TotalFinal.Value.ToString("0.00");

                FDataModule.ACBrECF.LinhaRelatorioGerencial("SUPRIMENTO...: " + Suprimento);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("SANGRIA......: " + Sangria);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("NAO FISCAL...: " + NaoFiscal);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("TOTAL VENDA..: " + TotalVenda);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("DESCONTO.....: " + Desconto);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("ACRESCIMO....: " + Acrescimo);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("RECEBIDO.....: " + Recebido);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("TROCO........: " + Troco);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("CANCELADO....: " + Cancelado);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("TOTAL FINAL..: " + TotalFinal);
                FDataModule.ACBrECF.PulaLinhas(3);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("                 CALCULADO  DECLARADO  DIFERENCA");

                //TotalGerado = TotalTipoPagamentoController.EncerramentoTotal(Movimento.Id, 1);
                //TotalDeclarado = TotalTipoPagamentoController.EncerramentoTotal(Movimento.Id, 2);

                //TODO:  Use o DTResumo para calcular os valores CALCULADO DECLARADO e DIFERENCA

                TotCalculado = 0;
                TotDeclarado = 0;
                TotDiferenca = 0;

                FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('-', 48));

                Calculado = TotCalculado.ToString("0.00");
                Declarado = TotDeclarado.ToString("0.00");
                Diferenca = TotDiferenca.ToString("0.00");

                FDataModule.ACBrECF.LinhaRelatorioGerencial("TOTAL.........:" + Calculado + Declarado + Diferenca);
                FDataModule.ACBrECF.PulaLinhas(4);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("    ________________________________________    ");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("                 VISTO DO CAIXA                 ");
                FDataModule.ACBrECF.PulaLinhas(3);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("    ________________________________________    ");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("               VISTO DO SUPERVISOR              ");

                FDataModule.ACBrECF.FechaRelatorio();
                UPAF.GravaR06("RG");
                Application.DoEvents();

            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }

        private void editSenhaOperador_Leave(object sender, EventArgs e)
        {
            editLoginGerente.Focus();
        }
    }

}

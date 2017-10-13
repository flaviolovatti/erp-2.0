/********************************************************************************
Title: T2TiPDV
Description: Janela utilizada para iniciar um novo movimento.

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
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using PafEcf.VO;
using PafEcf.Controller;
using PafEcf.Infra;
using PafEcf.Util;
using System.Collections.Generic;

namespace PafEcf.View
{

    public partial class FIniciaMovimento : Form
    {

        private static DataTable DTTurno;
        private static MovimentoVO Movimento;
        private static MovimentoController MovimentoController;
        private static OperadorController OperadorController;
        private static TurnoController TurnoController;

        public FIniciaMovimento()
        {
            // Required for Windows Form Designer support
            InitializeComponent();
            //
            MovimentoController = new MovimentoController();
            OperadorController = new OperadorController();
            TurnoController = new TurnoController();
            ConfiguraDataSet();
            GridTurno.Focus();
        }

        private void ConfiguraDataSet()
        {
            // Tabela Fechamento
            DTTurno = new DataTable("TURNO");
            DTTurno.Columns.Add("DESCRICAO", typeof(string));
            DTTurno.Columns.Add("HORA_INICIO", typeof(string));
            DTTurno.Columns.Add("HORA_FIM", typeof(string));
            DTTurno.Columns.Add("ID", typeof(int));
            dataSet.Tables.Add(DTTurno);

            GridTurno.DataSource = dataSet;
            GridTurno.DataMember = dataSet.Tables["TURNO"].TableName;
            CarregarTurnos();
        }

        private void CarregarTurnos()
        {
            DataRow Registro;
            List<TurnoVO> ListaTurnos = TurnoController.TabelaTurno();
            for (int i = 0; i <= ListaTurnos.Count - 1; i++)
            {
                Registro = DTTurno.NewRow();
                Registro["DESCRICAO"] = ListaTurnos[i].Descricao;
                Registro["HORA_INICIO"] = ListaTurnos[i].HoraInicio;
                Registro["HORA_FIM"] = ListaTurnos[i].HoraFim;
                Registro["ID"] = Convert.ToInt32(ListaTurnos[i].Id);
                DTTurno.Rows.Add(Registro);
            }
        }

        private void botaoConfirma_Click(object sender, EventArgs e)
        {
            Confirma();
        }

        private void botaoCancela_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FIniciaMovimento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
                botaoConfirma.PerformClick();
            if (e.KeyCode == Keys.Escape)
                botaoCancela.PerformClick();
        }

        private void Confirma()
        {
            try
            {
                // verifica se senha e o nivel do operador estáo corretos
                OperadorVO Operador = OperadorController.ConsultaUsuario(editLoginOperador.Text, editSenhaOperador.Text);
                if (Operador != null)
                {
                    // verifica se senha do gerente esta correta
                    OperadorVO Gerente = OperadorController.ConsultaUsuario(editLoginGerente.Text, editSenhaGerente.Text);
                    if (Gerente != null)
                    {
                        // verifica nivel de acesso do gerente/supervisor
                        if ((Gerente.Nivel == "G") || (Gerente.Nivel == "S"))
                        {
                            if (UECF.ImpressoraOK(2))
                            {
                                DataRow Registro = DTTurno.Rows[GridTurno.CurrentRow.Index];

                                // insere movimento
                                Movimento = new MovimentoVO();
                                Movimento.IdTurno = Convert.ToInt32(Registro["ID"]);
                                Movimento.IdImpressora = FCaixa.Configuracao.IdImpressora;
                                Movimento.IdEmpresa = FCaixa.Configuracao.IdEmpresa;
                                Movimento.IdOperador = Operador.Id;
                                Movimento.IdCaixa = FCaixa.Configuracao.IdCaixa;
                                Movimento.IdGerenteSupervisor = Gerente.Id;
                                Movimento.DataAbertura = FDataModule.ACBrECF.DataHora;
                                Movimento.HoraAbertura = FDataModule.ACBrECF.DataHora.ToString("hh:mm:ss");
                                Movimento.TotalSuprimento = Convert.ToDecimal(editValorSuprimento.Text);
                                Movimento.Status = "A";
                                Movimento.Sincronizado = "N";
                                Movimento = MovimentoController.IniciaMovimento(Movimento);
                            }
                            else
                            {
                                MessageBox.Show("Não foi possivel abrir o movimento!.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                FCaixa.StatusCaixa = 3;
                                this.Close();
                            }

                            // insere suprimento
                            if (Convert.ToDecimal(editValorSuprimento.Text) != 0)
                            {
                                try
                                {
                                    SuprimentoVO Suprimento = new SuprimentoVO();
                                    Suprimento.IdMovimento = Movimento.Id;
                                    Suprimento.DataSuprimento = FDataModule.ACBrECF.DataHora;
                                    Suprimento.Valor = Convert.ToDecimal(editValorSuprimento.Text);
                                    MovimentoController.Suprimento(Suprimento);
                                }
                                catch (Exception eError)
                                {
                                    Log.write(eError.ToString());
                                }
                            }

                            FCaixa.LabelMensagens.Text = "CAIXA ABERTO";
                            if (Movimento != null)
                            {
                                FCaixa.LabelCaixa.Text = "Terminal: " + Movimento.NomeCaixa;
                                FCaixa.LabelOperador.Text = "Operador: " + Movimento.LoginOperador;
                                MessageBox.Show("Movimento aberto com sucesso.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                FCaixa.Movimento = Movimento;
                                ImprimeAbertura();
                            }
                            Application.DoEvents();
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

        private void GridTurno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                editValorSuprimento.Focus();
        }

        public void ImprimeAbertura()
        {
            Movimento = MovimentoController.VerificaMovimento(Movimento.Id, "A");

            FDataModule.ACBrECF.AbreRelatorioGerencial(FCaixa.Configuracao.GerencialX);
            FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));
            FDataModule.ACBrECF.LinhaRelatorioGerencial(" ABERTURA DE CAIXA ");
            FDataModule.ACBrECF.PulaLinhas(1);
            FDataModule.ACBrECF.LinhaRelatorioGerencial("DATA DE ABERTURA  : " + Movimento.DataAbertura);
            FDataModule.ACBrECF.LinhaRelatorioGerencial("HORA DE ABERTURA  : " + Movimento.HoraAbertura);
            FDataModule.ACBrECF.LinhaRelatorioGerencial(Movimento.NomeCaixa + "  OPERADOR: " + Movimento.LoginOperador);
            FDataModule.ACBrECF.LinhaRelatorioGerencial("MOVIMENTO: " + Convert.ToString(Movimento.Id));
            FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));
            FDataModule.ACBrECF.PulaLinhas(1);
            FDataModule.ACBrECF.LinhaRelatorioGerencial("SUPRIMENTO...: " + Convert.ToDecimal(editValorSuprimento.Text).ToString("0.00"));
            FDataModule.ACBrECF.PulaLinhas(3);
            FDataModule.ACBrECF.LinhaRelatorioGerencial(" ________________________________________ ");
            FDataModule.ACBrECF.LinhaRelatorioGerencial(" VISTO DO CAIXA ");
            FDataModule.ACBrECF.PulaLinhas(3);
            FDataModule.ACBrECF.LinhaRelatorioGerencial(" ________________________________________ ");
            FDataModule.ACBrECF.LinhaRelatorioGerencial(" VISTO DO SUPERVISOR ");

            FDataModule.ACBrECF.FechaRelatorio();
            UPAF.GravaR06("RG");
        }
    }

}

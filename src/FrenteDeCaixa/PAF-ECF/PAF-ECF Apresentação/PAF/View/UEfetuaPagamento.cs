/********************************************************************************
Title: T2TiPDV
Description: Janela para selecionar as formas de pagamento e finalizar a venda

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

/********************************************************************************
  Operacoes TEF Discado:

	ATV		Verifica se o Gerenciador Padr?o está ativo
	ADM		Permite o acionamento da Solu??o TEF Discado para execu??o das fun??es administrativas
	CHQ		Pedido de autorizacao para transacao por meio de cheque
	CRT		Pedido de autorizacao para transacao por meio de cartao
	CNC		Cancelamento de venda efetuada por qualquer meio de pagamento
	CNF		Confirmacao da venda e impressão de cupom
	NCN		não confirmacao da venda e/ou da impressão.
********************************************************************************/



using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PafEcf.Controller;
using PafEcf.Util;
using PafEcf.VO;
using System.Data;
using PafEcf.Infra;
using System.Threading;
using ACBrFramework.ECF;
using ACBrFramework.TEFD;

namespace PafEcf.View
{

    //TODO:  realize os testes solicitados pelos roteiros do TEF e corrija o que for necessário nos procedimentos abaixo

    public partial class FEfetuaPagamento : Form
    {

        public static List<TipoPagamentoVO> ListaTipoPagamento;
        public static List<TotalTipoPagamentoVO> ListaTotalTipoPagamento;
        private static decimal SaldoRestante, TotalVenda, Desconto, Acrescimo, TotalReceber, TotalRecebido, Troco;
        private static bool TransacaoComTef, ImpressaoOK, CupomCancelado, PodeFechar, StatusTransacao, SegundoCartaoCancelado;
        private static int IndiceTransacaoTef, QuantidadeCartao;
        public static int IdVenda;
        public static TipoPagamentoController TipoPagamentoController;
        public static TotalTipoPagamentoController TotalTipoPagamentoController;
        public static DataTable DTValores;
        public static ACBrFramework.TEFD.ACBrTEFD ACBrTEFD;


        public FEfetuaPagamento()
        {
            InitializeComponent();
            //
            ACBrTEFD = this.acBrTEFD;
        }

        private void FEfetuaPagamento_Shown(object sender, EventArgs e)
        {
            TipoPagamentoController = new TipoPagamentoController();
            TotalTipoPagamentoController = new TotalTipoPagamentoController();
            //
            TotalVenda = 0;
            Desconto = 0;
            Acrescimo = 0;
            TotalReceber = 0;
            TotalRecebido = 0;
            Troco = 0;
            QuantidadeCartao = 0;

            if (FCaixa.VendaCabecalho.TaxaAcrescimo > 0)
                FCaixa.VendaCabecalho.Acrescimo = Biblioteca.TruncaValor(FCaixa.VendaCabecalho.TaxaAcrescimo / 100 * FCaixa.VendaCabecalho.ValorVenda, Constantes.DECIMAIS_VALOR);
            if (FCaixa.VendaCabecalho.TaxaDesconto > 0)
                FCaixa.VendaCabecalho.Desconto = Biblioteca.TruncaValor(FCaixa.VendaCabecalho.TaxaDesconto / 100 * FCaixa.VendaCabecalho.ValorVenda, Constantes.DECIMAIS_VALOR);

            // preenche valores nas variaveis
            TotalVenda = FCaixa.VendaCabecalho.ValorVenda.Value;
            Acrescimo = FCaixa.VendaCabecalho.Acrescimo.Value;
            Desconto = FCaixa.VendaCabecalho.Desconto.Value;
            TotalReceber = Biblioteca.TruncaValor(TotalVenda + Acrescimo - Desconto, Constantes.DECIMAIS_VALOR);
            SaldoRestante = TotalReceber;

            SegundoCartaoCancelado = false;
            TransacaoComTef = false;
            CupomCancelado = false;
            PodeFechar = true;
            IndiceTransacaoTef = -1;

            AtualizaLabelsValores();

            if (SaldoRestante > 0)
                editValorPago.Text = SaldoRestante.ToString("0.00");
            else
                editValorPago.Text = "0.00";

            IdVenda = FCaixa.VendaCabecalho.Id;

            // lista que vai acumular os meios de pagamento
            ListaTotalTipoPagamento = new List<TotalTipoPagamentoVO>();

            // tela padrão
            TelaPadrao();
            ComboTipoPagamento.Focus();
        }


        public void AtualizaLabelsValores()
        {
            labelTotalVenda.Text = TotalVenda.ToString("###,###,##0.00");
            labelAcrescimo.Text = Acrescimo.ToString("###,###,##0.00");
            labelDesconto.Text = Desconto.ToString("###,###,##0.00");
            labelTotalReceber.Text = TotalReceber.ToString("###,###,##0.00");
            labelTotalRecebido.Text = TotalRecebido.ToString("###,###,##0.00");
            if (SaldoRestante > 0)
                labelAindaFalta.Text = SaldoRestante.ToString("###,###,##0.00");
            else
                labelAindaFalta.Text = "0.00";
            labelTroco.Text = Troco.ToString("###,###,##0.00");
        }


        private void FEfetuaPagamento_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((PodeFechar) && (FCaixa.StatusCaixa == 1))
            {
                e.Cancel = false;
                return;
            }
            if ((PodeFechar) && (FCaixa.StatusCaixa == 0))
            {
                FFechaEfetuaPagamento FFechaEfetuaPagamento = new FFechaEfetuaPagamento(this);
                FFechaEfetuaPagamento.SetBounds(this.Left + 3, this.Top + this.Height - 50, 663, 47);
                FFechaEfetuaPagamento.ShowDialog();
                return;
            }
            e.Cancel = PodeFechar;
        }


        private void TelaPadrao()
        {
            ListaTipoPagamento = TipoPagamentoController.TabelaTipoPagamento();
            for (int i = 0; i <= ListaTipoPagamento.Count - 1; i++)
                ComboTipoPagamento.Items.Add(ListaTipoPagamento[i].Descricao);
            ComboTipoPagamento.SelectedIndex = 0;

            // Configura Grid
            DTValores = new DataTable("VALORES");
            DTValores.Columns.Add("DESCRICAO", typeof(string)); //0
            DTValores.Columns.Add("VALOR", typeof(decimal)); //1
            DTValores.Columns.Add("ID", typeof(int)); //2
            //os campos abaixo serão utilizados caso seja necessario cancelar uma transacao TEF
            DTValores.Columns.Add("TEF", typeof(string)); //3
            DTValores.Columns.Add("NSU", typeof(string)); //4
            DTValores.Columns.Add("REDE", typeof(string)); //5
            DTValores.Columns.Add("DATA_HORA_TRANSACAO", typeof(string)); //6
            DTValores.Columns.Add("INDICE_TRANSACAO", typeof(int)); //7
            DTValores.Columns.Add("INDICE_LISTA", typeof(int)); //8
            DTValores.Columns.Add("FINALIZACAO", typeof(string)); //9

            dataSet.Tables.Add(DTValores);

            GridValores.DataSource = dataSet;
            GridValores.DataMember = dataSet.Tables[0].ToString();

            //definimos os cabeçalhos da Grid
            GridValores.Columns[0].HeaderText = "Descrição";
            GridValores.Columns[0].ReadOnly = true;
            GridValores.Columns[1].HeaderText = "Valor";
            GridValores.Columns[1].ReadOnly = true;
            GridValores.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //nao exibe as colunas abaixo
            GridValores.Columns[2].Visible = false;
            GridValores.Columns[3].Visible = false;
            GridValores.Columns[4].Visible = false;
            GridValores.Columns[5].Visible = false;
            GridValores.Columns[6].Visible = false;
            GridValores.Columns[7].Visible = false;
            GridValores.Columns[8].Visible = false;
            GridValores.Columns[9].Visible = false;
        }

        private void FEfetuaPagamento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (GridValores.RowCount == 0)
                {
                    if (MessageBox.Show("Confirma valores e encerra venda por fechamento rapido?", "Finalizar venda", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        FechamentoRapido();
                    }
                }
                else
                {
                    MessageBox.Show("Ja existem valores informados. Impossivel utilizar Fechamento Rapido.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ComboTipoPagamento.Focus();
                }
            }

            if (e.KeyCode == Keys.F12)
                botaoConfirma.PerformClick();

            if (e.KeyCode == Keys.Escape)
                botaoCancela.PerformClick();

            if (e.KeyCode == Keys.F5)
            {
                if (GridValores.RowCount > 0)
                    GridValores.Focus();
                else
                {
                    MessageBox.Show("Não existem valores informados para serem removidos.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ComboTipoPagamento.Focus();
                }
            }

        }

        private void botaoConfirma_Click(object sender, EventArgs e)
        {
            VerificaSaldoRestante();
            // se não houver mais saldo no ECF é porque Ja devemos finalizar a venda
            if (SaldoRestante <= 0)
            {
                if (MessageBox.Show("Deseja finalizar a venda?", "Finalizar venda", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    FinalizaVenda();
                }
                else
                {
                    if (TransacaoComTef)
                    {
                        ACBrTEFD.CancelarTransacoesPendentes();
                        FCaixa.ProblemaNoPagamento = true;
                        FCaixa.VendaCabecalho.CupomFoiCancelado = "S";
                        FCaixa.StatusCaixa = 0;
                        FechaVendaComProblemas();
                        PodeFechar = true;
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Valores informados não são suficientes para finalizar a venda.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ComboTipoPagamento.Focus();
            }
        }

        private void BotaoCancela_Click(object sender, EventArgs e)
        {
            CancelaOperacao();
        }


        private void botaoNao_Click(object sender, EventArgs e)
        {
            PanelConfirmaValores.Visible = false;
            ComboTipoPagamento.Focus();
        }


        private void botaoSim_Click(object sender, EventArgs e)
        {
            decimal ValorInformado;
            string Mensagem;
            TipoPagamentoVO TipoPagamento = ListaTipoPagamento[ComboTipoPagamento.SelectedIndex];
            ValorInformado = Biblioteca.TruncaValor(Convert.ToDecimal(editValorPago.Text), Constantes.DECIMAIS_VALOR);

            if (((TipoPagamento.Descricao == "CONSULTA CHEQUE") || (TipoPagamento.Descricao == "CONSULTA CHQ TECBAN")) && (TransacaoComTef))
            {
                MessageBox.Show("Compra com Cartao e Cheque não permitida.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ComboTipoPagamento.Focus();
                PanelConfirmaValores.Visible = false;
                PanelConfirmaValores.SendToBack();
            }
            else
            {
                TotalTipoPagamentoVO TotalTipoPagamento = new TotalTipoPagamentoVO();

                if (((TransacaoComTef) || (TipoPagamento.TEF == "S")) && (ValorInformado > SaldoRestante))
                {
                    MessageBox.Show("Compra não permite troco.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ComboTipoPagamento.Focus();
                    PanelConfirmaValores.Visible = false;
                    PanelConfirmaValores.SendToBack();
                }
                else if ((TipoPagamento.TEF == "S") && (QuantidadeCartao >= FCaixa.Configuracao.QuantidadeMaximaCartoes))
                {
                    MessageBox.Show("Ja foi utilizada a quantidade maxima de cartoes para efetuar pagamento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ComboTipoPagamento.Focus();
                    PanelConfirmaValores.Visible = false;
                    PanelConfirmaValores.SendToBack();
                }
                else if ((TipoPagamento.TEF == "S") && (QuantidadeCartao >= FCaixa.Configuracao.QuantidadeMaximaCartoes - 1) && (ValorInformado != SaldoRestante))
                {
                    Mensagem = "Multiplos Cartoes. Transacao suporta ate " + Convert.ToString(FCaixa.Configuracao.QuantidadeMaximaCartoes) + " cartoes. Informe o valor exato para fechar a venda.";
                    MessageBox.Show(Mensagem, "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ComboTipoPagamento.Focus();
                    PanelConfirmaValores.Visible = false;
                    PanelConfirmaValores.SendToBack();
                }
                else
                {
                    GroupBox3.Enabled = false;
                    StatusTransacao = true;
                    if (TipoPagamento.TEF == "S")
                    {
                        try
                        {
                            try
                            {
                                //TODO:  inicialize o TEF a partir de TipoPagamento.TipoGP
                                ACBrTEFD.Initializar(ACBrFramework.TEFD.TefTipo.TefDial);
                            }
                            catch (Exception eError)
                            {
                                Log.write(eError.ToString());
                                MessageBox.Show("GP para tipo de pagamento solicitado não instalado.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                StatusTransacao = false;
                            }
                            if ((TipoPagamento.Descricao == "CONSULTA CHEQUE") || (TipoPagamento.Descricao == "CONSULTA CHQ TECBAN"))
                                StatusTransacao = ACBrTEFD.CHQ(ValorInformado, TipoPagamento.Codigo);
                            else
                                StatusTransacao = ACBrTEFD.CRT(ValorInformado, TipoPagamento.Codigo, FDataModule.ACBrECF.NumCOO);

                            if (StatusTransacao)
                            {
                                IndiceTransacaoTef++;

                                //TODO:  Verifique se esta é a forma correta de pegar essas informações - Analise o "RespostasPendentes"
                                TotalTipoPagamento.NSU = ACBrTEFD.Req.NSU;
                                TotalTipoPagamento.Rede = ACBrTEFD.Req.Rede;
                                TotalTipoPagamento.DataHoraTransacao = ACBrTEFD.Req.DataHoraTransacaoComprovante;
                                TotalTipoPagamento.Finalizacao = ACBrTEFD.Req.Finalizacao;

                                //TODO:  Verifique se tem como saber se o cartão usado foi de credito ou debito e armazene essa informação do banco de dados 
                                TotalTipoPagamento.CartaoDebitoOuCredito = "C";

                                QuantidadeCartao++;
                                TransacaoComTef = true;
                                PodeFechar = false;
                            }
                        }
                        catch (Exception eError)
                        {
                            Log.write(eError.ToString());
                        }
                    }

                    if (StatusTransacao)
                    {
                        DataRow Registro = DTValores.NewRow();
                        Registro["DESCRICAO"] = ComboTipoPagamento.Text;
                        decimal valor = Convert.ToDecimal(editValorPago.Text);
                        Registro["VALOR"] = Convert.ToDecimal(valor.ToString("0.00"));
                        if (TipoPagamento.TEF == "S")
                        {
                            Registro["TEF"] = "S";
                            Registro["NSU"] = TotalTipoPagamento.NSU;
                            Registro["REDE"] = TotalTipoPagamento.Rede;
                            Registro["DATA_HORA_TRANSACAO"] = TotalTipoPagamento.DataHoraTransacao.ToString();
                            Registro["INDICE_TRANSACAO"] = IndiceTransacaoTef;
                            Registro["FINALIZACAO"] = TotalTipoPagamento.Finalizacao;
                        }

                        TotalRecebido = Biblioteca.TruncaValor(TotalRecebido + Convert.ToDecimal(editValorPago.Text), Constantes.DECIMAIS_VALOR);
                        Troco = Biblioteca.TruncaValor(TotalRecebido - TotalReceber, Constantes.DECIMAIS_VALOR);
                        if (Troco < 0)
                            Troco = 0;

                        VerificaSaldoRestante();

                        TotalTipoPagamento.IdVenda = IdVenda;
                        TotalTipoPagamento.IdTipoPagamento = TipoPagamento.Id;
                        TotalTipoPagamento.Valor = Biblioteca.TruncaValor(Convert.ToDecimal(editValorPago.Text), Constantes.DECIMAIS_VALOR);
                        TotalTipoPagamento.CodigoPagamento = TipoPagamento.Codigo.Trim();
                        TotalTipoPagamento.Estorno = "N";
                        TotalTipoPagamento.TemTEF = TipoPagamento.TEF;

                        ListaTotalTipoPagamento.Add(TotalTipoPagamento);

                        // guarda o índice da lista
                        Registro["INDICE_LISTA"] = ListaTotalTipoPagamento.Count - 1;
                        DTValores.Rows.Add(Registro);
                    }
                    PanelConfirmaValores.Visible = false;
                    PanelConfirmaValores.SendToBack();
                    if (SaldoRestante > 0)
                        editValorPago.Text = SaldoRestante.ToString("0.00");
                    else
                        editValorPago.Text = "0.00";

                    GroupBox3.Enabled = true;
                    ComboTipoPagamento.Focus();
                }

                VerificaSaldoRestante();
                if (SaldoRestante <= 0)
                    FinalizaVenda();

                if (SegundoCartaoCancelado)
                {
                    MessageBox.Show("Cupom fiscal cancelado. será aberto novo cupom e deve-se informar novamente os pagamentos.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FCaixa.ProblemaNoPagamento = true;
                    FCaixa.VendaCabecalho.CupomFoiCancelado = "S";
                    FCaixa.StatusCaixa = 0;
                    FechaVendaComProblemas();
                    PodeFechar = true;
                    this.Close();
                }
            }
        }


        public void FechamentoRapido()
        {
            StatusTransacao = true;
            botaoSim.PerformClick();
        }


        // controle das teclas digitadas na Grid
        public void GridValoresKeyDown(object sender, KeyEventArgs e)
        {
            DataRow Registro = DTValores.Rows[GridValores.CurrentRow.Index];
            if (e.KeyCode == Keys.Delete)
            {
                if (Registro["TEF"].ToString() == "S")
                    MessageBox.Show("Operacao não permitida.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    if (MessageBox.Show("Deseja remover o valor selecionado?", "Remover ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        TotalRecebido = Biblioteca.TruncaValor(TotalRecebido - Convert.ToDecimal(DTValores.Columns["VALOR"].ToString()), Constantes.DECIMAIS_VALOR);
                        Troco = Biblioteca.TruncaValor(TotalRecebido - TotalReceber, Constantes.DECIMAIS_VALOR);
                        if (Troco < 0)
                            Troco = 0;

                        ListaTotalTipoPagamento.RemoveAt(Convert.ToInt32(Registro["INDICE_LISTA"]));

                        DTValores.Rows.RemoveAt(GridValores.CurrentRow.Index);
                        VerificaSaldoRestante();
                        if (SaldoRestante > 0)
                            editValorPago.Text = SaldoRestante.ToString("###,###,##0.00");
                        else
                            editValorPago.Text = "0.00";
                    }
                    ComboTipoPagamento.Focus();
                }
            }
            if (e.KeyCode == Keys.Enter)
                ComboTipoPagamento.Focus();
        }


        private void editValorPago_Leave(object sender, EventArgs e)
        {
            decimal ValorInformado;
            if (decimal.TryParse(editValorPago.Text, out ValorInformado))
            {

                if (ValorInformado > 0)
                {
                    VerificaSaldoRestante();
                    // se ainda tem saldo no ECF para pagamento
                    if (SaldoRestante > 0)
                    {
                        PanelConfirmaValores.Visible = true;
                        PanelConfirmaValores.BringToFront();
                        LabelConfirmaValores.Text = "Confirma forma de pagamento e valor?";
                        botaoSim.Focus();
                    }
                    else
                        MessageBox.Show("Todos os valores Ja foram recebidos. Finalize a venda.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Valor não pode ser menor ou igual a zero.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    editValorPago.Clear();
                    ComboTipoPagamento.Focus();
                }
            }
            else
            {
                MessageBox.Show("Valor inválido.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        public void VerificaSaldoRestante()
        {
            decimal RecebidoAteAgora = 0;

            foreach (DataRow Registro in DTValores.Rows)
            {
                RecebidoAteAgora = Biblioteca.TruncaValor(RecebidoAteAgora + Convert.ToDecimal(Registro["VALOR"]), Constantes.DECIMAIS_VALOR);
            }
            SaldoRestante = Biblioteca.TruncaValor(TotalReceber - RecebidoAteAgora, Constantes.DECIMAIS_VALOR);
            AtualizaLabelsValores();
        }


        public void FinalizaVenda()
        {
            ImpressaoOK = true;

            // subtotaliza o cupom
            SubTotalizaCupom();

            // manda os pagamentos para o ECF
            if (TransacaoComTef)
                OrdenaLista();

            TotalTipoPagamentoVO TotalTipoPagamento = new TotalTipoPagamentoVO();
            for (int i = 0; i <= ListaTotalTipoPagamento.Count - 1; i++)
            {
                TotalTipoPagamento = ListaTotalTipoPagamento[i];
                if (TotalTipoPagamento.TemTEF != "S")
                    FDataModule.ACBrECF.EfetuaPagamento(TotalTipoPagamento.CodigoPagamento, TotalTipoPagamento.Valor);
            }

            //TODO:  Descomente para bloquear teclado e mouse
            //BlockInput.Bloquear(true);

            // finaliza o cupom
            ACBrTEFD.FinalizarCupom();

            // imprime as transacoes pendentes - comprovantes nao fiscais vinculados
            ACBrTEFD.ImprimirTransacoesPendentes();

            //TODO:  Descomente para bloquear teclado e mouse
            //BlockInput.Bloquear(false);

            if (ImpressaoOK)
            {
                // grava os pagamentos no banco de dados
                TotalTipoPagamentoController.GravaTotaisVenda(ListaTotalTipoPagamento);

                // conclui o encerramento da venda - grava dados de cabecalho no banco
                FCaixa.VendaCabecalho.ValorFinal = TotalReceber;
                FCaixa.VendaCabecalho.ValorRecebido = TotalRecebido;
                FCaixa.VendaCabecalho.Troco = Troco;
                FCaixa.VendaCabecalho.StatusVenda = "F";
                FCaixa.StatusCaixa = 0;
                FCaixa.ConcluiEncerramentoVenda();

                //  usado quando a gaveta tem sinal invertido
                if (FCaixa.Configuracao.SinalInvertido == 1)
                    FDataModule.ACBrECF.GavetaSinalInvertido = true;

                if (FCaixa.Configuracao.GavetaDinheiro > 0)
                    FDataModule.ACBrECF.AbreGaveta();

                PodeFechar = true;
                this.Close();
            }
            else
            {
                if (CupomCancelado)
                //ocorreu problema na impressao do comprovante do TEF. ECF Desligado.
                //Sistema pergunta ao usuário se o mesmo quer tentar novamente. Usuário responde não.
                //ECF agora está ligado e o sistema consegue cancelar o cupom.
                {
                    MessageBox.Show("Problemas no ECF. Cupom Fiscal Cancelado.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FCaixa.ProblemaNoPagamento = true;
                    FCaixa.VendaCabecalho.CupomFoiCancelado = "S";
                    FCaixa.StatusCaixa = 0;
                    FechaVendaComProblemas();
                    PodeFechar = true;
                    this.Close();
                }
                else
                //ocorreu problema na impressao do comprovante do TEF. ECF Desligado.
                //Sistema pergunta ao usuário se o mesmo quer tentar novamente. Usuário responde não.
                //ECF continua desligado e o sistema não consegue cancelar o cupom.
                {
                    MessageBox.Show("Problemas no ECF. Aplicação funcionará apenas para consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FCaixa.StatusCaixa = 3;
                    FechaVendaComProblemas();
                    PodeFechar = true;
                    this.Close();
                }
            }

        }


        public void FechaVendaComProblemas()
        {
            // altera o status da venda para C
            FCaixa.VendaCabecalho.StatusVenda = "C";
            FCaixa.ConcluiEncerramentoVenda();

            // grava os pagamentos no banco de dados com o indicador de estorno
            for (int i = 0; i <= ListaTotalTipoPagamento.Count - 1; i++)
                ListaTotalTipoPagamento[i].Estorno = "S";
            TotalTipoPagamentoController.GravaTotaisVenda(ListaTotalTipoPagamento);
        }


        public void CancelaOperacao()
        {
            if (TransacaoComTef)
            {
                if (MessageBox.Show("Existem pagamentos no cartao. O cupom fiscal será cancelado. Deseja continuar?", "Pergunta do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ACBrTEFD.CancelarTransacoesPendentes();
                    if (CupomCancelado)
                    {
                        FCaixa.ProblemaNoPagamento = true;
                        FCaixa.VendaCabecalho.CupomFoiCancelado = "S";
                        FCaixa.StatusCaixa = 0;
                        FechaVendaComProblemas();
                        MessageBox.Show("Transacao com cartao cancelada. Cupom Fiscal Cancelado.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        FCaixa.StatusCaixa = 3;
                        FechaVendaComProblemas();
                        MessageBox.Show("Problemas no ECF. Aplicação funcionará apenas para consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    PodeFechar = true;
                    this.Close();
                }
            }
            else
                this.Close();
        }


        private void ComboTipoPagamento_Leave(object sender, EventArgs e)
        {
            TipoPagamentoVO TipoPagamento = TipoPagamentoController.ConsultaPelaDescricao(ComboTipoPagamento.Text);

            if (TipoPagamento != null)
            {
                // TODO:  Conclua a janela de parcelamentos
                if (TipoPagamento.GeraParcelas == "S")
                {/*
                    VerificaSaldoRestante();
                    if (SaldoRestante > 0)
                    {
                        try
                        {
                            FParcelamento FParcelamento = new FParcelamento();
                            FParcelamento.EditNome.Text = FCaixa.VendaCabecalho.NomeCliente;
                            FParcelamento.editCPF.Text = FCaixa.VendaCabecalho.CPFouCNPJCliente;
                            FParcelamento.editValorVenda.Text = labelTotalVenda.Text;
                            FParcelamento.editValorRecebido.Text = labelTotalRecebido.Text;
                            FParcelamento.editValorParcelar.Value = SaldoRestante;
                            FParcelamento.editVencimento.Text = (DateTime.Now).ToString("dd/MM/yyyy");

                            if (FCaixa.VendaCabecalho.Desconto > 0)
                            {
                                FParcelamento.lblDesconto.Text = "Desconto";
                                FParcelamento.editDesconto.Value = FCaixa.VendaCabecalho.Desconto;
                            }

                            if (FCaixa.VendaCabecalho.Acrescimo > 0)
                            {
                                FParcelamento.lblDesconto.Text = "Acrescimo";
                                FParcelamento.editDesconto.Value = FCaixa.VendaCabecalho.Acrescimo;
                            }

                            if ((FParcelamento.ShowDialog() == DialogResult.OK))
                            {
                                //  Depois de chamar a tela de parcelamento, se tudo deu certo finaliza a Venda.
                                editValorPago.Text = SaldoRestante.ToString();
                                botaoSim.PerformClick();
                            }
                            else
                                ComboTipoPagamento.Focus();
                        }
                        catch (Exception eError)
                        {
                            Log.write(eError.ToString());
                        }
                    }
                */
                }
                else
                    // TODO:  Conclua a janela de cheques
                    if (TipoPagamento.GeraParcelas == "C")
                    {/*
                        VerificaSaldoRestante();
                        if (SaldoRestante > 0)
                        {
                            try
                            {
                                FCheques FCheques = new FCheques();
                                FCheques.EditNome.Text = FCaixa.VendaCabecalho.NomeCliente;
                                FCheques.editCPF.Text = FCaixa.VendaCabecalho.CPFouCNPJCliente;
                                FCheques.edValor.Text = labelTotalVenda.Text;

                                if ((FCheques.ShowDialog() == DialogResult.OK))
                                {
                                    //  Depois d chamar a tela de parcelamente, se tudo deu certo finaliza a Venda.
                                    editValorPago.Text = SaldoRestante.ToString();
                                    botaoSim.PerformClick();
                                }
                                else
                                    ComboTipoPagamento.Focus();
                            }
                            catch (Exception eError)
                            {
                                Log.write(eError.ToString());
                            }
                        }
                    */
                    }
            }
        }


        public void SubTotalizaCupom()
        {
            if ((FDataModule.ACBrECF.Estado.ToString() == "Venda"))
            {
                if (FCaixa.VendaCabecalho.Desconto > 0)
                    UECF.SubTotalizaCupom(FCaixa.VendaCabecalho.Desconto * -1);
                else if (FCaixa.VendaCabecalho.Acrescimo > 0)
                    UECF.SubTotalizaCupom(FCaixa.VendaCabecalho.Acrescimo);
                else
                    UECF.SubTotalizaCupom(0);
            }
        }



        // TODO:  Verifique a necessidade deste método e se o mesmo pode ser melhorado
        public void OrdenaLista()
        {
            List<TotalTipoPagamentoVO> ListaTotalTipoPagamentoLocal = ListaTotalTipoPagamento;
            ListaTotalTipoPagamentoLocal = ListaTotalTipoPagamento;
            ListaTotalTipoPagamento = new List<TotalTipoPagamentoVO>();

            // no primeiro laço insere na lista só quem nao tem TEF
            for (int i = 0; i <= ListaTotalTipoPagamentoLocal.Count - 1; i++)
            {
                if (ListaTotalTipoPagamentoLocal[i].TemTEF == "N")
                    ListaTotalTipoPagamento.Add(ListaTotalTipoPagamentoLocal[i]);
            }

            // no segundo laço insere os pagamentos que tem tef
            for (int i = 0; i <= ListaTotalTipoPagamentoLocal.Count - 1; i++)
                if (ListaTotalTipoPagamentoLocal[i].TemTEF == "S")
                    ListaTotalTipoPagamento.Add(ListaTotalTipoPagamentoLocal[i]);
        }

        #region Metodos do Componente ACBrTEFD

        private void acBrTEFD_OnAguardaResp(object sender, ACBrFramework.TEFD.AguardaRespEventArgs e)
        {
            String Msg = "";

            if ((ACBrTEFD.GPAtual == ACBrFramework.TEFD.TefTipo.CliSiTef) || (ACBrTEFD.GPAtual == ACBrFramework.TEFD.TefTipo.CliSiTef)) // É TEF dedicado?
            {
                if (e.Arquivo == "23")  // está aguardando Pin-Pad ?
                {
                    Msg = "Tecle ESC para cancelar.";
                }
            }
            else
                Msg = "Aguardando: " + e.Arquivo + " " + Convert.ToString(e.SegundosTimeout);

            if (Msg != "")
                FCaixa.LabelMensagens.Text = Msg;
            Application.DoEvents();
        }

        private void acBrTEFD_OnAntesCancelarTransacao(object sender, ACBrFramework.TEFD.AntesCancelarTransacaoEventArgs e)
        {
            if ((FDataModule.ACBrECF.Estado.ToString() == "Venda") || (FDataModule.ACBrECF.Estado.ToString() == "Pagamento"))
            {
                UECF.CancelaCupom();
                CupomCancelado = true;
            }
            else if ((FDataModule.ACBrECF.Estado.ToString() == "Relatorio"))
            {
                FDataModule.ACBrECF.FechaRelatorio();
                UPAF.GravaR06("CC");
            }
            else
                FDataModule.ACBrECF.CorrigeEstadoErro(false);
        }

        private void acBrTEFD_OnAntesFinalizarRequisicao(object sender, ACBrFramework.TEFD.AntesFinalizarRequisicaoEventArgs e)
        {
        }

        private void acBrTEFD_OnBloqueiaMouseTeclado(object sender, ACBrFramework.TEFD.BloqueiaMouseTecladoEventArgs e)
        {
            e.Tratado = true;  //  Se "False" --> Deixa executar o codigo de Bloqueio do ACBrTEFD 
        }

        private void acBrTEFD_OnComandaECF(object sender, ACBrFramework.TEFD.ComandaECFEventArgs e)
        {
            string Mensagem = "";

            try
            {

                switch (e.Operacao)
                {
                    case ACBrFramework.TEFD.OperacaoECF.AbreGerencial:
                        {
                            FDataModule.ACBrECF.AbreRelatorioGerencial();
                        }
                        break;

                    case ACBrFramework.TEFD.OperacaoECF.CancelaCupom:
                        {
                            ImpressaoOK = false;
                            try
                            {
                                UECF.CancelaCupom();
                                CupomCancelado = true;
                            }
                            catch (Exception eError)
                            {
                                Log.write(eError.ToString());
                                CupomCancelado = false;
                            }
                        } break;

                    case ACBrFramework.TEFD.OperacaoECF.FechaCupom:
                        {
                            if (FCaixa.VendaCabecalho.IdPreVenda > 0)
                                Mensagem = "PV" + new string('0', 10 - Convert.ToString(FCaixa.VendaCabecalho.IdPreVenda).Length) + Convert.ToString(FCaixa.VendaCabecalho.IdPreVenda);
                            if (FCaixa.VendaCabecalho.IdDAV > 0)
                            {
                                DavCabecalhoVO DavCabecalho = new DAVController().ConsultaDAVId(FCaixa.VendaCabecalho.IdDAV);
                                Mensagem = Mensagem + "DAV" + DavCabecalho.NumeroDav;
                            }
                            Mensagem = FCaixa.MD5 + Mensagem + "\r" + "\n";
                            try
                            {
                                EmpresaVO Empresa = new EmpresaController().PegaEmpresa(FCaixa.Configuracao.IdEmpresa);
                                if (Empresa.Uf == "MG")
                                {
                                    Mensagem = Mensagem + "MINAS LEGAL:" +
                                             Empresa.Cnpj.Substring(8, 1) + FDataModule.ACBrECF.DataHora.ToString("ddMMyyyy");
                                    if (FCaixa.VendaCabecalho.ValorFinal >= 1)
                                    {
                                        Mensagem = Mensagem + FCaixa.VendaCabecalho.ValorFinal.Value.ToString("0.00");
                                    }
                                    Mensagem = Mensagem + "\r" + "\n";
                                }
                                UECF.FechaCupom(Mensagem + FCaixa.Configuracao.MensagemCupom);
                            }
                            catch (Exception eError)
                            {
                                Log.write(eError.ToString());
                            }
                        } break;

                    case ACBrFramework.TEFD.OperacaoECF.SubTotalizaCupom:
                        SubTotalizaCupom(); //FDataModule.ACBrECF.SubtotalizaCupom(0); 
                        break;

                    case ACBrFramework.TEFD.OperacaoECF.FechaVinculado:
                        {
                            FDataModule.ACBrECF.FechaRelatorio();
                            UPAF.GravaR06("CC");
                        } break;

                    case ACBrFramework.TEFD.OperacaoECF.PulaLinhas:
                        {
                            FDataModule.ACBrECF.PulaLinhas(FDataModule.ACBrECF.LinhasEntreCupons);
                            FDataModule.ACBrECF.CortaPapel(true);
                            Thread.Sleep(200);
                        } break;
                }

                e.RetornoECF = true;
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
                e.RetornoECF = false;
            }

        }

        private void acBrTEFD_OnComandaECFAbreVinculado(object sender, ACBrFramework.TEFD.ComandaECFAbreVinculadoEventArgs e)
        {
            try
            {
                FDataModule.ACBrECF.AbreCupomVinculado(e.COO, e.IndiceECF, e.Valor);
                e.RetornoECF = true;
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
                e.RetornoECF = false;
            }
        }

        private void acBrTEFD_OnComandaECFImprimeVia(object sender, ACBrFramework.TEFD.ComandaECFImprimeViaEventArgs e)
        {
            //* *** Se estiver usando ACBrECF... Lembre-se de configurar ***
            //ACBrECF1.MaxLinhasBuffer   := 3; // Os homologadores permitem no m?ximo
            // Impressao de 3 em 3 linhas
            //ACBrECF1.LinhasEntreCupons := 7; // (ajuste conforme o seu ECF)
            //NOTA: ACBrECF nao possui comando para imprimir a 2a via do CCD 
            try
            {
                switch (e.TipoRelatorio)
                {
                    case ACBrFramework.TEFD.TipoRelatorio.Gerencial:
                        FDataModule.ACBrECF.LinhaRelatorioGerencial(e.ImagemComprovante);
                        break;
                    case ACBrFramework.TEFD.TipoRelatorio.Vinculado:
                        FDataModule.ACBrECF.LinhaCupomVinculado(e.ImagemComprovante);
                        break;
                }
                e.RetornoECF = true;
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
                e.RetornoECF = false;
            }
        }

        private void acBrTEFD_OnComandaECFPagamento(object sender, ACBrFramework.TEFD.ComandaECFPagamentoEventArgs e)
        {
            try
            {
                FDataModule.ACBrECF.EfetuaPagamento(e.IndiceECF, e.Valor);
                e.RetornoECF = true;
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
                e.RetornoECF = false;
            }
        }

        private void acBrTEFD_OnExibeMensagem(object sender, ACBrFramework.TEFD.ExibeMensagemEventArgs e)
        {
            string OldMensagem;

            switch (e.Operacao)
            {
                case ACBrFramework.TEFD.OperacaoMensagem.OK:
                    MessageBox.Show(e.Mensagem, "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.ModalResult = ACBrFramework.TEFD.ModalResult.OK;
                    break;

                case ACBrFramework.TEFD.OperacaoMensagem.YesNo:
                    {
                        if (!FDataModule.ACBrECF.Ativo)
                        {
                            FDataModule.ACBrECF.Modelo = (ModeloECF)Convert.ToInt32(FCaixa.Configuracao.ModeloImpressora);
                            FDataModule.ACBrECF.Device.Porta = FCaixa.Configuracao.PortaECF;
                            FDataModule.ACBrECF.Device.TimeOut = FCaixa.Configuracao.TimeOutECF;
                            FDataModule.ACBrECF.IntervaloAposComando = FCaixa.Configuracao.IntervaloECF;
                            FDataModule.ACBrECF.Device.Baud = FCaixa.Configuracao.BitsPorSegundo;
                            try
                            {
                                FDataModule.ACBrECF.Ativar();
                            }
                            catch (Exception eError)
                            {
                                Log.write(eError.ToString());
                            }
                            FDataModule.ACBrECF.CarregaAliquotas();
                            FDataModule.ACBrECF.CarregaFormasPagamento();
                        }
                        e.ModalResult = MessageBox.Show(e.Mensagem, "Pergunta do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes ? ACBrFramework.TEFD.ModalResult.Yes : ACBrFramework.TEFD.ModalResult.No;
                    }
                    break;

                case ACBrFramework.TEFD.OperacaoMensagem.ExibirMsgOperador:
                    break;

                case ACBrFramework.TEFD.OperacaoMensagem.RemoverMsgOperador:
                    FCaixa.LabelMensagens.Text = e.Mensagem;
                    break;

                case ACBrFramework.TEFD.OperacaoMensagem.ExibirMsgCliente:
                    break;

                case ACBrFramework.TEFD.OperacaoMensagem.RemoverMsgCliente:
                    FCaixa.LabelMensagens.Text = e.Mensagem;
                    break;

                case ACBrFramework.TEFD.OperacaoMensagem.DestaqueVia:
                    {
                        OldMensagem = FCaixa.LabelMensagens.Text;
                        try
                        {
                            FCaixa.LabelMensagens.Text = e.Mensagem;

                            //  Aguardando 3 segundos 
                            Thread.Sleep(3000);

                        }
                        finally
                        {
                            FCaixa.LabelMensagens.Text = OldMensagem;
                        }
                    }
                    break;
            }

            if ((e.ModalResult == ACBrFramework.TEFD.ModalResult.No) && (e.Mensagem == "Gostaria de continuar a transacao com outra(s) forma(s) de pagamento ?"))
            {
                SegundoCartaoCancelado = true;
            }
            Application.DoEvents();
        }

        private void acBrTEFD_OnInfoECF(object sender, ACBrFramework.TEFD.InfoECFEventArgs e)
        {
            switch (e.Operacao)
            {
                case ACBrFramework.TEFD.InfoECF.SubTotal:
                    e.Value = FDataModule.ACBrECF.SubTotal - FDataModule.ACBrECF.TotalPago;
                    break;

                case ACBrFramework.TEFD.InfoECF.EstadoECF:
                    {
                        switch (FDataModule.ACBrECF.Estado)
                        {
                            case EstadoECF.Livre:
                                e.RetornoECF = RetornoECF.Livre;
                                break;

                            case EstadoECF.Venda:
                                e.RetornoECF = RetornoECF.VendaDeItens;
                                break;

                            case EstadoECF.Pagamento:
                                e.RetornoECF = RetornoECF.PagamentoOuSubTotal;
                                break;

                            case EstadoECF.Relatorio:
                                e.RetornoECF = RetornoECF.RelatorioGerencial;
                                break;

                            case EstadoECF.NaoFiscal:
                                e.RetornoECF = RetornoECF.RecebimentoNaoFiscal;
                                break;

                            default:
                                e.RetornoECF = RetornoECF.Outro;
                                break;
                        }
                    } break;
            }

        }

        private void acBrTEFD_OnRestauraFocoAplicacao(object sender, ExecutaAcaoEventArgs e)
        {
            this.Focus();
            e.Tratado = true;
        }

        #endregion Metodos do Componente ACBrTEFD

        #region Tab por Enter
        private void ComboTipoPagamento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                SendKeys.Send("{tab}");
            }
        }

        private void editValorPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                SendKeys.Send("{tab}");
            }
        }
        #endregion Tab por Enter

    }

    public class BlockInput
    {
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "BlockInput")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool Bloquear([System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)] bool fBlockIt);
    }

}

/* *******************************************************************************
  Title: T2TiPDV
  Description: Tela principal do PAF-ECF - Caixa.

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
using System.Collections.Generic;
using System.Windows.Forms;
using PafEcf.VO;
using PafEcf.Controller;
using ACBrFramework.ECF;
using PafEcf.Util;
using System.IO;
using PafEcf.Infra;
using System.Drawing;
using System.Xml;

namespace PafEcf.View
{
    /*
    //TODO:  Analise o tratamento dos dados em todos os Controllers para não ocorrer problemas com dados nulos. 
    // Observe o tratamento feito na classe ProdutoController e fique analisando o arquivo de LOG: "DebugLogPAF.txt"
    // Observe o tratamento feito no MovimentoVO e escolha a melhor forma de trabalhar
    */
    public partial class FCaixa : Form
    {

        #region Variáveis

        public static bool MenuAberto;
        public static int StatusCaixa; //  0-aberto | 1-venda em andamento | 2-venda em recuperacao ou importacao de PV/DAV | 3-So Consulta | 4-Usuario cancelou a tela Movimento Aberto | 5-Informando dados de NF
        public static int ItemCupom;
        public static decimal SubTotal, TotalGeral, Desconto, Acrescimo;
        public static string MD5;
        public static bool ProblemaNoPagamento;
        public static bool CargaOK, AcionaMenu;
        public static int AtualizarEstoque;
        public static bool BalancaLePeso;
        public static string Pathlocal, PathCargaRemoto, PathSemaforo;

        public static MovimentoVO Movimento;
        public static ConfiguracaoVO Configuracao;
        public static ClienteVO Cliente;
        public static VendaCabecalhoVO VendaCabecalho;

        public static VendaController VendaController = new VendaController();
        public static MovimentoController MovimentoController = new MovimentoController();
        public static ProdutoController ProdutoController = new ProdutoController();

        public static ProdutoVO Produto;
        public static List<VendaDetalheVO> ListaVendaDetalhe;
        public static VendaDetalheVO VendaDetalhe;

        public static Label LabelMensagens { get; set; }
        public static Label LabelCaixa { get; set; }
        public static Label LabelOperador { get; set; }
        public static TextBox EditQuantidade { get; set; }
        public static TextBox EditCodigo { get; set; }
        public static TextBox EditUnitario { get; set; }
        public static TextBox EditTotalItem { get; set; }
        public static TextBox EditSubTotal { get; set; }
        public static Label LabelTotalGeral { get; set; }
        public static Label LabelDescricaoProduto { get; set; }
        public static Label LabelDescontoAcrescimo { get; set; }
        public static Label EdtNVenda { get; set; }
        public static Label EdtCOO { get; set; }
        public static PictureBox ImageProduto { get; set; }
        public static ListBox Bobina { get; set; }

        #endregion Variáveis

        public FCaixa()
        {
            InitializeComponent();
            //
            LabelCaixa = this.labelCaixa;
            LabelOperador = this.labelOperador;
            LabelMensagens = this.labelMensagens;
            EditQuantidade = this.editQuantidade;
            EditCodigo = this.editCodigo;
            EditUnitario = this.editUnitario;
            EditTotalItem = this.editTotalItem;
            EditSubTotal = this.editSubTotal;
            LabelTotalGeral = this.labelTotalGeral;
            LabelDescricaoProduto = this.labelDescricaoProduto;
            LabelDescontoAcrescimo = this.labelDescontoAcrescimo;
            EdtNVenda = this.edtNVenda;
            EdtCOO = this.edtCOO;
            ImageProduto = this.imageProduto;
            Bobina = this.bobina;
            //
            FormCreate();
        }

        #region Infra

        public void FormCloseQuery()
        {

            if ((StatusCaixa == 0) || (StatusCaixa == 3) || (StatusCaixa == 4))
            {
                if (MessageBox.Show("Tem Certeza Que Deseja Sair do Sistema?", "Sair do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    FDataModule.ACBrECF.Desativar();
                    Application.Exit();
                }
            }
            else
            {
                MessageBox.Show("Existe uma venda em andamento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }


        public void FormCreate()
        {
            try
            {
                XmlDocument ArquivoXML = new XmlDocument();
                ArquivoXML.Load(Application.StartupPath + "\\Conexao.xml");
                PathCargaRemoto = ArquivoXML.GetElementsByTagName("remoteApp").Item(0).InnerText.Trim();
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }

            AcionaMenu = false;

            FDataModule FDataModule = new FDataModule();
            FSplash Splash = new FSplash();
            Splash.Show();
            Splash.Refresh();
            System.Threading.Thread.Sleep(2000);

            DesabilitaControlesVenda();

            Movimento = MovimentoController.VerificaMovimento();
            MenuAberto = false;
            StatusCaixa = 0;

            PegaConfiguracao();
            ConfiguraConstantes();

            panelTitulo.Text = Configuracao.TituloTelaCaixa;

            // TODO:  Carregue a imagem de fundo do formulário de acordo com o que está configurado no banco de dados.
            if (File.Exists(Configuracao.CaminhoImagensLayout + Configuracao.ResolucaoVO.ImagemTela))
            {
                //this.BackgroundImage = Configuracao.CaminhoImagensLayout + Configuracao.ResolucaoVO.ImagemTela;
            }

            SetResolucao();

            try
            {
                ConfiguraACBr();
                VerificaEstadoImpressora();

                /*
                FEfetuaPagamento FEfetuaPagamento = new FEfetuaPagamento();
                FEfetuaPagamento.ACBrTEFD.Initializar(ACBrFramework.TEFD.TefTipo.TefDial);
                FEfetuaPagamento.Dispose();
                Application.DoEvents();
                */

                MD5 = "MD-5:" + UPAF.GeraMD5();
                TelaPadrao(1);

                if (Movimento != null)
                {
                    FMovimentoAberto FMovimentoAberto = new FMovimentoAberto();
                    FMovimentoAberto.ShowDialog();
                }
                else
                {
                    StatusCaixa = 3;
                }

                //  só continua o procedimento caso o usuário não cancele a tela FMovimentoAberto
                if (StatusCaixa != 4)
                {
                    HabilitaControlesVenda();
                    editCodigo.Focus();
                    try
                    {
                        if (!UPAF.ECFAutorizado())
                        {
                            MessageBox.Show("ECF não autorizado - aplicação aberta apenas para consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            StatusCaixa = 3;
                            labelMensagens.Text = "Terminal em Estado Somente Consulta";
                        }

                        if (!VerificaVendaAberta())
                        {
                            if (!UPAF.ConfereGT())
                            {
                                MessageBox.Show("Grande total invalido - entre em contato com a Software House.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                StatusCaixa = 3;
                                labelMensagens.Text = "Terminal em Estado Somente Consulta";
                            }
                        }
                    }
                    catch (Exception eError)
                    {
                        Log.write(eError.ToString());
                        labelMensagens.Text = "Terminal em Estado Somente Consulta";
                    }

                    ProblemaNoPagamento = false;
                }

                if (Configuracao.BalancaModelo > 0)
                {
                    try
                    {
                        ConectaComBalanca();
                    }
                    catch (Exception eError)
                    {
                        Log.write(eError.ToString());
                        MessageBox.Show("Balança não conectada ou desligada!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                try
                {
                    if (DateTime.Now.ToString("dd/MM/yyyy") != FDataModule.ACBrECF.DataHora.ToString("dd/MM/yyyy"))
                    {
                        MessageBox.Show("Data do ECF diferente da data do computador - aplicação aberta apenas para consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        StatusCaixa = 3;
                        labelMensagens.Text = "Data do ECF diferente da data do computador. Terminal em Estado Somente Consulta";
                    }
                }
                catch (Exception eError)
                {
                    Log.write(eError.ToString());
                }

                if (Configuracao.NumSerieECF != FDataModule.ACBrECF.NumSerie)
                {
                    MessageBox.Show("Numero de Serie do ECF diferente do cadastrado no computador - aplicação aberta apenas para consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    StatusCaixa = 3;
                    labelMensagens.Text = "Numero de Serie do ECF diferente do cadastrado na base. Terminal em Estado Somente Consulta";
                }

            }
            finally
            {
                Application.DoEvents();
                Splash.Close();
            }
        }


        public void VerificaEstadoImpressora()
        {
            string Estado = FDataModule.ACBrECF.Estado.ToString();

            if (Estado == "Não Inicializada")
            {
                MessageBox.Show("Estado da impressora fiscal: não Inicializada. Aplicação será aberta para somente consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StatusCaixa = 3;
            }
            if (Estado == "Desconhecido")
            {
                MessageBox.Show("Estado da impressora fiscal: Desconhecido. Aplicação será aberta para somente consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StatusCaixa = 3;
            }
            if ((Estado == "Venda") || (Estado == "Pagamento"))
            {
                if (!new VendaController().VendaAberta(""))
                {
                    //  se por um acaso ocorrer de existir um cupom aberto no ecf e nenhuma venda com status 'A' no BD
                    MessageBox.Show("Existe um cupom aberto inconsistente. Cupom fiscal será cancelado.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UECF.CancelaCupom();
                }
            }
            if (Estado == "RequerX")
            {
                if (MessageBox.Show("É necessario emitir uma Leitura X. Deseja fazer isso agora?", "Leitura X", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    UECF.LeituraX();
            }
            if (Estado == "RequerZ")
            {
                if (MessageBox.Show("É necessario emitir uma Reducao Z. Deseja fazer isso agora?", "Leitura Z", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    UECF.ReducaoZ();
            }
        }


        public void ConfiguraACBr()
        {
            FDataModule.ACBrECF.Modelo = (ModeloECF)Convert.ToInt32(Configuracao.ModeloImpressora);
            FDataModule.ACBrECF.Device.Porta = Configuracao.PortaECF;
            FDataModule.ACBrECF.Device.TimeOut = Configuracao.TimeOutECF;
            FDataModule.ACBrECF.IntervaloAposComando = Configuracao.IntervaloECF;
            FDataModule.ACBrECF.Device.Baud = Configuracao.BitsPorSegundo;
            try
            {
                FDataModule.ACBrECF.Ativar();
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
                MessageBox.Show("ECF com problemas ou desligado. Aplicação será aberta para somente consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DesabilitaControlesVenda();

                FEfetuaPagamento FEfetuaPagamento = new FEfetuaPagamento();
                try
                {
                    FEfetuaPagamento.ACBrTEFD.Initializar(ACBrFramework.TEFD.TefTipo.TefDial);
                    //FEfetuaPagamento.ACBrTEFD.Initializar(ACBrFramework.TEFD.TefTipo.HiperTef);
                    //FEfetuaPagamento.ACBrTEFD.Initializar(ACBrFramework.TEFD.TefTipo.TefDisc);
                }
                catch (Exception eeError)
                {
                    Log.write(eeError.ToString());
                }
                FEfetuaPagamento.Dispose();

                StatusCaixa = 3;
                TelaPadrao(1);
                return;
            }

            FDataModule.ACBrECF.CarregaAliquotas();
            if (FDataModule.ACBrECF.Aliquotas.Length <= 0)
            {
                MessageBox.Show("ECF sem aliquotas cadastradas. Aplicação será aberta para somente consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StatusCaixa = 3;
            }

            FDataModule.ACBrECF.CarregaFormasPagamento();
            if (FDataModule.ACBrECF.FormasPagamento.Length <= 0)
            {
                MessageBox.Show("ECF sem formas de pagamento cadastradas. Aplicação será aberta para somente consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StatusCaixa = 3;
            }
        }


        public void PegaConfiguracao()
        {
            Configuracao = new ConfiguracaoController().PegaConfiguracao();
        }


        public void ConfiguraConstantes()
        {
            Constantes.DECIMAIS_QUANTIDADE = Configuracao.DecimaisQuantidade;
            Constantes.DECIMAIS_VALOR = Configuracao.DecimaisValor;
        }


        public void SetResolucao()
        {
            //TODO:  Conclua o para posicionar os componentes na tela de acordo com o que está definido no banco de dados e configurar texto e fonte (tabela ECF_POSICAO_COMPONENTES)
            List<PosicaoComponentesVO> ListaPosicoes;

            string NomeComponente;
            ListaPosicoes = new ConfiguracaoController().VerificaPosicaoTamanho();
            PosicaoComponentesVO PosicaoComponente;

            foreach (Control c in this.Controls)
            {
                NomeComponente = c.Name;
                for (int i = 0; i <= ListaPosicoes.Count - 1; i++)
                {
                    PosicaoComponente = ListaPosicoes[i];
                    if (PosicaoComponente.NomeComponente == NomeComponente)
                    {
                        if (c is TextBox)
                            (c as TextBox).SetBounds(PosicaoComponente.Esquerda, PosicaoComponente.Topo, PosicaoComponente.Largura, PosicaoComponente.Altura);
                        else if (c is Label)
                            (c as Label).SetBounds(PosicaoComponente.Esquerda, PosicaoComponente.Topo, PosicaoComponente.Largura, PosicaoComponente.Altura);
                    }
                }
            }

        }

        public bool VerificaVendaAberta()
        {
            bool NovoCupom = false;
            ListaVendaDetalhe = VendaController.VendaAberta();

            if (ListaVendaDetalhe != null)
            {

                if (FDataModule.ACBrECF.Estado.ToString() == "Livre")
                {
                    Cliente = new ClienteVO();
                    Cliente.CpfOuCnpj = ListaVendaDetalhe[0].IdentificacaoCliente;
                    UECF.AbreCupom(Cliente.CpfOuCnpj, "", "");
                    NovoCupom = true;
                }

                ImprimeCabecalhoBobina();
                ParametrosIniciaisVenda();

                StatusCaixa = 2;
                VendaCabecalho = new VendaCabecalhoVO();
                VendaCabecalho = VendaController.RetornaCabecalhoDaUltimaVenda();

                labelMensagens.Text = "Venda recuperada em andamento..";

                for (int i = 0; i <= ListaVendaDetalhe.Count - 1; i++)
                {
                    VendaDetalhe = ListaVendaDetalhe[i];
                    ConsultaProduto(VendaDetalhe.GTIN, 2);
                    CompoeItemParaVenda();
                    ImprimeItemBobina();
                    SubTotal = SubTotal + VendaDetalhe.ValorTotal;
                    TotalGeral = TotalGeral + VendaDetalhe.ValorTotal;
                    AtualizaTotais();
                    if (NovoCupom)
                        UECF.VendeItem(VendaDetalhe);
                }

                bobina.SelectedIndex = bobina.Items.Count - 1;
                editCodigo.Focus();
                StatusCaixa = 1;
                return true;
            }
            else
            {
                return false;
            }
        }


        public static void TelaPadrao(int Tipo)
        {
            if (Movimento == null)
            {
                LabelMensagens.Text = "CAIXA FECHADO";
                if (Tipo == 1)
                    IniciaMovimento(); //  se o caixa estiver fechado abre o iniciaMovimento
            }
            else if (Movimento.Status == "T")
                LabelMensagens.Text = "SAIDA Temporaria";
            else
                LabelMensagens.Text = "CAIXA ABERTO";

            if (StatusCaixa == 1)
                LabelMensagens.Text = "Venda em andamento...";

            if (StatusCaixa == 3 || StatusCaixa == 4)
                LabelMensagens.Text = "Terminal em Estado Somente Consulta";

            EditQuantidade.Text = "1";
            EditCodigo.Text = "";
            EditUnitario.Text = "0,00";
            EditTotalItem.Text = "0,00";
            EditSubTotal.Text = "0,00";
            LabelTotalGeral.Text = "0,00";
            LabelDescricaoProduto.Text = "";
            LabelDescontoAcrescimo.Text = "";
            EdtNVenda.Text = "";
            EdtCOO.Text = "";

            SubTotal = 0;
            TotalGeral = 0;
            Desconto = 0;
            Acrescimo = 0;

            Bobina.Items.Clear();

            if (Configuracao.MarketingAtivo == "S")
            {
                //TODO:  Implemente o Timer do Marketing
                //TimerMarketing.Enabled = true;
            }
            else
            {
                if (File.Exists(Configuracao.CaminhoImagensProdutos + "padrao.png"))
                    ImageProduto.ImageLocation = Configuracao.CaminhoImagensProdutos + "padrao.png";
                else
                    ImageProduto.ImageLocation = Application.StartupPath + "\\imgProdutos\\padrao.png";
            }
            Cliente = null;
        }


        private void FCaixa_KeyDown(object sender, KeyEventArgs e)
        {
            if (AcionaMenu)
            {
                //  F2 - Menu Principal
                if (e.KeyCode == Keys.F2)
                    AcionaMenuPrincipal();

                //  F3 - Menu Operacoes
                if (e.KeyCode == Keys.F3)
                    AcionaMenuOperacoes();

                //TODO:  Verifique se o menu fiscal está sendo chamado de todas as janelas da aplicação. Implemente esse requisito onde for necessário.
                //  F4 - Menu Fiscal
                if (e.KeyCode == Keys.F4)
                    AcionaMenuFiscal();

                //  F7 - Encerra Venda
                if (e.KeyCode == Keys.F7)
                    IniciaEncerramentoVenda();

                //  F8 - Cancela Item
                if (e.KeyCode == Keys.F8)
                    CancelaItem();

                //  F9 - Cancela Cupom
                if (e.KeyCode == Keys.F9)
                    CancelaCupom();

                //  F10 - Concede Desconto
                if (e.KeyCode == Keys.F10)
                    DescontoOuAcrescimo();

                //  F11 - Identifica Vendedor
                if (e.KeyCode == Keys.F11)
                    IdentificaVendedor();

                if (e.KeyCode == Keys.B && e.Modifiers == Keys.Shift)
                {
                    if (Configuracao.BalancaModelo > 0)
                    {
                        try
                        {
                            BalancaLePeso = true;
                            FDataModule.ACBrBAL.LePeso(Configuracao.BalancaTimeOut);
                            editCodigo.Text = "";
                            editCodigo.Focus();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Balança não conectada ou desligada!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            } //  if AcionaMenu then

            //  F1 - Identifica Cliente
            if (e.KeyCode == Keys.F1)
                IdentificaCliente();

            //  F5 - Entrada de Dados de NF
            if (e.KeyCode == Keys.F5)
                EntradaDadosNF();

            //  F6 - Localiza Produto
            if (e.KeyCode == Keys.F6)
                LocalizaProduto();

            //  F12 - Sai do Caixa
            if (e.KeyCode == Keys.F12)
            {
                FormCloseQuery();
            }
        }


        public void AcionaMenuPrincipal()
        {
            if (StatusCaixa != 3)
            {
                if (StatusCaixa != 1)
                {
                    MenuAberto = true;
                    FMenuPrincipal FMenuPrincipal = new FMenuPrincipal();
                    //TODO:  Pegue o "Bounds" do Menu na tabela ECF_POSICAO_COMPONENTES
                    FMenuPrincipal.SetBounds(this.Left + 10, this.Top + 82, 213, 200);
                    FMenuPrincipal.ShowDialog();
                }
                else
                    MessageBox.Show("Existe uma venda em andamento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Terminal em Estado Somente Consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public void AcionaMenuOperacoes()
        {
            if (StatusCaixa != 3)
            {
                if (StatusCaixa != 1)
                {
                    MenuAberto = true;
                    FMenuOperacoes FMenuOperacoes = new FMenuOperacoes();
                    //TODO:  Pegue o "Bounds" do Menu na tabela ECF_POSICAO_COMPONENTES
                    FMenuOperacoes.SetBounds(this.Left + 10, this.Top + 82, 213, 200);
                    FMenuOperacoes.ShowDialog();
                }
                else
                    MessageBox.Show("Existe uma venda em andamento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Terminal em Estado Somente Consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        // TODO:  Adapte o código abaixo para utilizar a balança
        public void ACBrLCB1LeCodigo(object Sender)
        {
            /*
              if( editCodigo.Focused ) //  Para evitar que ponha o codigo no campo quantidade por exemplo
              {
                editCodigo.Text = ACBrLCB1.UltimoCodigo; //  Preenche o edit com o codigo lido
                keybd_event(VK_RETURN, 0, 0, 0); //  Simula o acionamento da tecla ENTER
              }
            */
        }


        public void AcionaMenuFiscal()
        {
            if (StatusCaixa != 1)
            {
                MenuAberto = true;
                FMenuFiscal FMenuFiscal = new FMenuFiscal();
                FMenuFiscal.ShowDialog();
            }
            else
                MessageBox.Show("Existe uma venda em andamento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion Infra


        #region Procedimentos referentes ao Menu Principal e seus SubMenus
        public static void IniciaMovimento()
        {
            try
            {
                Movimento = MovimentoController.VerificaMovimento();
                if (Movimento == null)
                {
                    FIniciaMovimento FIniciaMovimento = new FIniciaMovimento();
                    FIniciaMovimento.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Ja existe um movimento aberto.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        public static void EncerraMovimento()
        {
            try
            {
                Movimento = MovimentoController.VerificaMovimento();
                if (Movimento == null)
                    MessageBox.Show("Não existe um movimento aberto.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    FEncerraMovimento FEncerraMovimento = new FEncerraMovimento();
                    FEncerraMovimento.ShowDialog();
                }
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        public static void Suprimento()
        {
            decimal ValorSuprimento;
            Movimento = MovimentoController.VerificaMovimento();
            if (Movimento == null)
                MessageBox.Show("Não existe um movimento aberto.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                FValorReal FValorReal = new FValorReal();
                FValorReal.Text = "Suprimento";
                FValorReal.LabelEntrada.Text = "Informe o valor do suprimento:";
                try
                {
                    if (FValorReal.ShowDialog() == DialogResult.OK)
                    {
                        if (decimal.TryParse(FValorReal.EditEntrada.Text, out ValorSuprimento))
                        {
                            UECF.Suprimento(ValorSuprimento, Configuracao.DescricaoSuprimento);

                            SuprimentoVO Suprimento = new SuprimentoVO();
                            Suprimento.IdMovimento = Movimento.Id;
                            Suprimento.DataSuprimento = FDataModule.ACBrECF.DataHora;
                            Suprimento.Valor = ValorSuprimento;
                            MovimentoController.Suprimento(Suprimento);
                            Movimento.TotalSuprimento = Movimento.TotalSuprimento + ValorSuprimento;

                            UPAF.GravaR06("CN");

                            FCargaPDV FCargaPDV = new FCargaPDV();
                            FCargaPDV.Tipo = "suprimento";
                            FCargaPDV.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Valor inválido.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception eError)
                {
                    Log.write(eError.ToString());
                }
            }
        }


        public static void Sangria()
        {
            decimal ValorSangria;
            Movimento = MovimentoController.VerificaMovimento();
            if (Movimento == null)
                MessageBox.Show("Não existe um movimento aberto.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                FValorReal FValorReal = new FValorReal();
                FValorReal.Text = "Sangria";
                FValorReal.LabelEntrada.Text = "Informe o valor da sangria:";
                try
                {
                    if (FValorReal.ShowDialog() == DialogResult.OK)
                    {
                        if (decimal.TryParse(FValorReal.EditEntrada.Text, out ValorSangria))
                        {
                            UECF.Sangria(ValorSangria, Configuracao.DescricaoSangria);

                            SangriaVO Sangria = new SangriaVO();
                            Sangria.IdMovimento = Movimento.Id;
                            Sangria.DataSangria = FDataModule.ACBrECF.DataHora;
                            Sangria.Valor = ValorSangria;
                            MovimentoController.Sangria(Sangria);
                            Movimento.TotalSangria = Movimento.TotalSangria + ValorSangria;

                            UPAF.GravaR06("CN");

                            FCargaPDV FCargaPDV = new FCargaPDV();
                            FCargaPDV.Tipo = "sangria";
                            FCargaPDV.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Valor inválido.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception eError)
                {
                    Log.write(eError.ToString());
                }
            }

        }


        private void DescontoOuAcrescimo()
        {
            try
            {

                //  0-Desconto em Dinheiro
                //  1-Desconto Percentual
                //  2-Acrescimo em Dinheiro
                //  3-Acrescimo Percentual
                //  5-Cancela o Desconto ou Acrescimo

                int Operacao;
                decimal Valor;
                if (StatusCaixa != 3)
                {
                    if (StatusCaixa == 1)
                    {
                        FLoginGerenteSupervisor FLoginGerenteSupervisor = new FLoginGerenteSupervisor();

                        if (FLoginGerenteSupervisor.ShowDialog() == DialogResult.OK)
                        {
                            if (FLoginGerenteSupervisor.LoginOK)
                            {
                                FDescontoAcrescimo FDescontoAcrescimo = new FDescontoAcrescimo();
                                FDescontoAcrescimo.Text = "Desconto ou Acrescimo";

                                if (FDescontoAcrescimo.ShowDialog() == DialogResult.OK)
                                {
                                    Operacao = FDescontoAcrescimo.ComboOperacao.SelectedIndex;
                                    //  cancela desconto ou acrescimo
                                    if (Operacao == 5)
                                    {
                                        VendaCabecalho.TaxaAcrescimo = 0;
                                        VendaCabecalho.TaxaDesconto = 0;
                                        Acrescimo = 0;
                                        Desconto = 0;
                                        AtualizaTotais();
                                        return;
                                    } //  if Operacao = 5 then

                                    if (decimal.TryParse(FDescontoAcrescimo.EditEntrada.Text, out Valor))
                                    {
                                        //  desconto em valor
                                        if (Operacao == 0)
                                        {
                                            if (Valor >= VendaCabecalho.ValorVenda)
                                                MessageBox.Show("Desconto não pode ser superior ou igual ao valor da venda.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            else
                                            {
                                                if (Valor <= 0)
                                                    MessageBox.Show("Valor zerado ou negativo. Operacao não realizada.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                else
                                                {
                                                    Desconto = Desconto + Valor;
                                                    AtualizaTotais();
                                                }
                                            }
                                        } //  if Operacao = 0 then

                                        //  desconto em taxa
                                        if (Operacao == 1)
                                        {
                                            if (Valor > Convert.ToDecimal(99.99))
                                                MessageBox.Show("Desconto não pode ser superior a 100%.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            else
                                            {
                                                if (Valor <= 0)
                                                    MessageBox.Show("Valor zerado ou negativo. Operacao não realizada.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                else
                                                {
                                                    VendaCabecalho.TaxaDesconto = 100 - (((100 - VendaCabecalho.TaxaDesconto) / 100) * ((100 - Valor) / 100)) * 100;

                                                    Desconto = Desconto + Biblioteca.TruncaValor(SubTotal * (Valor / 100), Constantes.DECIMAIS_VALOR);
                                                    AtualizaTotais();
                                                }
                                            }
                                        } //  if Operacao = 1 then

                                        //  acrescimo em valor
                                        if (Operacao == 2)
                                        {
                                            if (Valor <= 0)
                                                MessageBox.Show("Valor zerado ou negativo. Operacao não realizada.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            else if (Valor >= VendaCabecalho.ValorVenda)
                                                MessageBox.Show("Valor do acrescimo não pode ser igual ou superior ao valor da venda!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            else
                                            {
                                                Acrescimo = Acrescimo + Valor;
                                                AtualizaTotais();
                                            }
                                        } //  if Operacao = 2 then

                                        //  acrescimo em taxa
                                        if (Operacao == 3)
                                        {
                                            if (Valor <= 0)
                                                MessageBox.Show("Valor zerado ou negativo. Operacao não realizada.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            else if (Valor > Convert.ToDecimal(99.99))
                                                MessageBox.Show("Acrescimo não pode ser superior a 100%!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            else
                                            {
                                                VendaCabecalho.TaxaAcrescimo = (((100 + Valor) / 100) * ((100 + VendaCabecalho.TaxaAcrescimo) / 100)) / 100;
                                                Acrescimo = Acrescimo + Biblioteca.TruncaValor(SubTotal * (Valor / 100), Constantes.DECIMAIS_VALOR);
                                                AtualizaTotais();
                                            }
                                        } //  if Operacao = 3 then

                                    }
                                    else
                                    {
                                        MessageBox.Show("Valor inválido.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                            else
                                MessageBox.Show("Login - dados incorretos.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                        MessageBox.Show("Não existe venda em andamento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Terminal em Estado Somente Consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }

        #endregion Procedimentos referentes ao Menu Principal e seus SubMenus


        #region Procedimentos referentes ao Menu Operacoes e seus SubMenus

        public static void CarregaPreVenda(string Numero)
        {
            PreVendaDetalheVO PreVendaDetalhe;
            List<PreVendaDetalheVO> ListaPreVenda = new PreVendaController().CarregaPreVenda(Convert.ToInt32(Numero));
            if (ListaPreVenda != null)
            {
                IniciaVenda();
                StatusCaixa = 2;
                VendaCabecalho.IdPreVenda = ListaPreVenda[0].IdPreVenda;
                for (int i = 0; i <= ListaPreVenda.Count - 1; i++)
                {
                    PreVendaDetalhe = ListaPreVenda[i];
                    Produto = ProdutoController.ConsultaId(PreVendaDetalhe.IdProduto);
                    VendaDetalhe = new VendaDetalheVO();
                    VendaDetalhe.Quantidade = PreVendaDetalhe.Quantidade;
                    VendaDetalhe.ValorUnitario = PreVendaDetalhe.ValorUnitario;
                    VendaDetalhe.ValorTotal = PreVendaDetalhe.ValorTotal;
                    VendeItem();
                    SubTotal = SubTotal + VendaDetalhe.ValorTotal;
                    TotalGeral = TotalGeral + VendaDetalhe.ValorTotal;
                    AtualizaTotais();
                    if (PreVendaDetalhe.Cancelado == "S")
                    {
                        UECF.CancelaItem(ItemCupom);
                        VendaController.CancelaItem(VendaDetalhe);

                        Bobina.Items.Add(new string('*', 48));
                        string DescricaoProduto = VendaDetalhe.DescricaoPDV.Length < 27 ? VendaDetalhe.DescricaoPDV : VendaDetalhe.DescricaoPDV.Substring(0, 27);
                        Bobina.Items.Add(new string('0', 3 - Convert.ToString(ItemCupom).Length) + Convert.ToString(ItemCupom) + "  " + VendaDetalhe.GTIN + new string(' ', 14 - VendaDetalhe.GTIN.Length) + " " + DescricaoProduto);

                        Bobina.Items.Add("ITEM CANCELADO");
                        Bobina.Items.Add(new string('*', 48));

                        SubTotal = SubTotal - VendaDetalhe.ValorTotal;
                        TotalGeral = TotalGeral - VendaDetalhe.ValorTotal;

                        //  cancela possíveis descontos ou acrescimos
                        Bobina.SelectedIndex = Bobina.Items.Count - 1;
                        AtualizaTotais();
                    }
                }
                Bobina.SelectedIndex = Bobina.Items.Count - 1;
                EditCodigo.Focus();
                StatusCaixa = 1;
            }
            else
                MessageBox.Show("Pre-Venda inexistente ou Ja efetivada/mesclada.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        //TODO:  Debugue esse procedimento e acompanhe os Logs. Corrija os problema encontrados.
        public static void CarregaDAV(string Numero)
        {
            List<DavDetalheVO> ListaDAV = new DAVController().CarregaDAV(Convert.ToInt32(Numero));
            if (ListaDAV != null)
            {
                DavDetalheVO DAVDetalhe;
                IniciaVenda();
                StatusCaixa = 2;
                VendaCabecalho.IdDAV = ListaDAV[0].IdDavCabecalho;
                for (int i = 0; i <= ListaDAV.Count - 1; i++)
                {
                    DAVDetalhe = ListaDAV[i];
                    Produto = ProdutoController.ConsultaId(DAVDetalhe.IdProduto);
                    VendaDetalhe = new VendaDetalheVO();
                    VendaDetalhe.Quantidade = DAVDetalhe.Quantidade;
                    VendaDetalhe.ValorUnitario = DAVDetalhe.ValorUnitario;
                    VendaDetalhe.ValorTotal = DAVDetalhe.ValorTotal;
                    VendeItem();
                    SubTotal = SubTotal + VendaDetalhe.ValorTotal;
                    TotalGeral = TotalGeral + VendaDetalhe.ValorTotal;
                    AtualizaTotais();

                    if (DAVDetalhe.Cancelado == "S")
                    {
                        UECF.CancelaItem(ItemCupom);
                        VendaController.CancelaItem(VendaDetalhe);

                        Bobina.Items.Add(new string('*', 48));
                        string DescricaoProduto = VendaDetalhe.DescricaoPDV.Length < 27 ? VendaDetalhe.DescricaoPDV : VendaDetalhe.DescricaoPDV.Substring(0, 27);
                        Bobina.Items.Add(new string('0', 3 - Convert.ToString(ItemCupom).Length) + Convert.ToString(ItemCupom) + "  " + VendaDetalhe.GTIN + new string(' ', 14 - VendaDetalhe.GTIN.Length) + " " + DescricaoProduto);
                        Bobina.Items.Add("ITEM CANCELADO");
                        Bobina.Items.Add(new string('*', 48));

                        SubTotal = SubTotal - VendaDetalhe.ValorTotal;
                        TotalGeral = TotalGeral - VendaDetalhe.ValorTotal;

                        //  cancela possíveis descontos ou acrescimos
                        Bobina.SelectedIndex = Bobina.Items.Count - 1;
                        AtualizaTotais();
                    }

                }
                Bobina.SelectedIndex = Bobina.Items.Count - 1;
                EditCodigo.Focus();
                StatusCaixa = 1;
            }
            else
                MessageBox.Show("DAV inexistente ou Ja efetivado/mesclado.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion Procedimentos referentes ao Menu Operacoes e seus SubMenus


        #region Procedimentos para controle da venda

        private void LocalizaProduto()
        {
            if (Configuracao.TecladoReduzido == 1)
            {
                FLocaliza FLocaliza = new FLocaliza();
                FLocaliza.ShowDialog();
            }
            else
            {
                FImportaProduto FImportaProduto = new FImportaProduto();
                FImportaProduto.ShowDialog();
            }
        }


        private void IdentificaCliente()
        {
            if (StatusCaixa != 3)
            {
                Cliente = new ClienteVO();
                if (Movimento != null)
                {
                    if (StatusCaixa == 0)
                    {
                        FIdentificaCliente FIdentificaCliente = new FIdentificaCliente();
                        FIdentificaCliente.ShowDialog();
                        if (Cliente != null)
                            IniciaVenda();
                    }
                    else
                        MessageBox.Show("Ja existe venda em andamento. Cancele o cupom e inicie nova venda.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Não existe um movimento aberto.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }



        public void IdentificaVendedor()
        {
            if (StatusCaixa != 3)
            {
                if (StatusCaixa == 1)
                {
                    FImportaNumero FImportaNumero = new FImportaNumero();
                    FImportaNumero.Text = "Identifica Vendedor";
                    FImportaNumero.LabelEntrada.Text = "Informe o codigo do vendedor";
                    try
                    {
                        if (FImportaNumero.ShowDialog() == DialogResult.OK)
                        {
                            FuncionarioVO Vendedor = new VendedorController().ConsultaVendedor(Convert.ToInt32(FImportaNumero.EditEntrada.Text));
                            if (Vendedor.Id != 0)
                                VendaCabecalho.IdVendedor = Vendedor.Id;
                            else
                                MessageBox.Show("Vendedor: codigo invalido ou inexistente.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception eError)
                    {
                        Log.write(eError.ToString());
                    }
                }
                else
                    MessageBox.Show("Não existe venda em andamento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Terminal em Estado Somente Consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        public static void IniciaVenda()
        {
            Bobina.Items.Clear();

            if (Movimento == null)
                MessageBox.Show("Não existe um movimento aberto.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (!UPAF.ECFAutorizado())
                {
                    MessageBox.Show("ECF não autorizado - aplicação aberta apenas para consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    StatusCaixa = 3;
                    LabelMensagens.Text = "Terminal em Estado Somente Consulta";
                    return;
                }
                else if (!UPAF.ConfereGT())
                {
                    MessageBox.Show("Grande total invalido - entre em contato com a Software House.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    StatusCaixa = 3;
                    LabelMensagens.Text = "Terminal em Estado Somente Consulta";
                    return;
                }
                else
                {
                    try
                    {
                        FDataModule.ACBrECF.TestaPodeAbrirCupom();
                    }
                    catch (Exception eError)
                    {
                        Log.write(eError.ToString());
                        TelaPadrao(1);
                        EditCodigo.Focus();
                    }

                    //  instancia venda e detalhe
                    VendaCabecalho = new VendaCabecalhoVO();
                    ListaVendaDetalhe = new List<VendaDetalheVO>();

                    //  parametro para identificar o cliente na abertura do cupom (nota paulista)
                    if ((Configuracao.PedeCPFCupom == 1) && (Cliente == null))
                    {
                        FIdentificaCliente FIdentificaCliente = new FIdentificaCliente();
                        FIdentificaCliente.ShowDialog();
                    }

                    //  atribui dados do cliente abre o cupom
                    if (Cliente != null)
                    {
                        VendaCabecalho.IdCliente = Cliente.Id;
                        VendaCabecalho.NomeCliente = Cliente.Nome;
                        VendaCabecalho.CPFouCNPJCliente = Cliente.CpfOuCnpj;
                        UECF.AbreCupom(Cliente.CpfOuCnpj, Cliente.Nome, Cliente.Logradouro);
                    }
                    else
                        UECF.AbreCupom("", "", "");

                    ImprimeCabecalhoBobina();
                    ParametrosIniciaisVenda();
                    StatusCaixa = 1;
                    LabelMensagens.Text = "Venda em andamento...";
                    VendaCabecalho.IdMovimento = Movimento.Id;
                    VendaCabecalho.DataVenda = FDataModule.ACBrECF.DataHora;
                    VendaCabecalho.HoraVenda = FDataModule.ACBrECF.DataHora.ToString("HH:mm:ss");
                    VendaCabecalho.StatusVenda = "A";
                    VendaCabecalho.CFOP = Configuracao.CFOPECF;
                    VendaCabecalho.COO = Convert.ToInt32(FDataModule.ACBrECF.NumCOO);
                    VendaCabecalho.CCF = Convert.ToInt32(FDataModule.ACBrECF.NumCCF);

                    VendaCabecalho = VendaController.IniciaVenda(VendaCabecalho);
                    EditCodigo.Focus();
                    EditCodigo.SelectAll();

                    EdtNVenda.Text = "VENDA nº " + Convert.ToString(VendaCabecalho.Id);
                    EdtCOO.Text = "CUPOM nº " + Convert.ToString(VendaCabecalho.COO);
                }
            }
        }


        public static void ParametrosIniciaisVenda()
        {
            if (File.Exists(Configuracao.CaminhoImagensProdutos + "padrao.png"))
                ImageProduto.ImageLocation = Configuracao.CaminhoImagensProdutos + "padrao.png";
            else
                ImageProduto.ImageLocation = Application.StartupPath + "\\imgProdutos\\padrao.png";

            ItemCupom = 0;
            SubTotal = 0;
            TotalGeral = 0;
        }


        public static void ImprimeCabecalhoBobina()
        {
            try
            {
                Bobina.Items.Add(new string('-', 48));
                Bobina.Items.Add("               ** CUPOM FISCAL **               ");
                Bobina.Items.Add(new string('-', 48));
                Bobina.Items.Add("ITEM Codigo         Descricao                   ");
                Bobina.Items.Add("QTD.     UN      VL.UNIT.(R$) ST    VL.ITEM(R$)");
                Bobina.Items.Add(new string('-', 48));
                if (Cliente != null)
                {
                    Bobina.Items.Add("CNPJ/CPF do Consumidor: " + Cliente.CpfOuCnpj);
                    Bobina.Items.Add(new string('-', 48));
                }
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }



        public static void IniciaVendaDeItens()
        {
            decimal Unitario, Quantidade, Total;
            string Estado;

            if (StatusCaixa != 3)
            {

                try
                {
                    Estado = FDataModule.ACBrECF.Estado.ToString();
                    if ((Estado == "Requer Z") || (Estado == "Bloqueada"))
                    {
                        StatusCaixa = 3;
                        LabelMensagens.Text = "Terminal em Estado Somente Consulta";
                        if (Estado == "Requer Z")
                            MessageBox.Show("Impressora Requer Reducao Z!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Impressora Bloqueada Até o Final do Dia!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TelaPadrao(2);
                        EditCodigo.Focus();
                        return;
                    }
                }
                catch (Exception eError)
                {
                    Log.write(eError.ToString());
                    StatusCaixa = 3;
                    LabelMensagens.Text = "Terminal em Estado Somente Consulta";
                    MessageBox.Show("Impressora Bloqueada ou Desligada  ou  Sem Papel  ou Fora de Linha!" + "\r" + "Caso a Impressora esteja ligada, com Papel e Em Linha" + "\r" + "Verifique se os cabos  estáo  devidamente  conectados.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TelaPadrao(2);
                    EditCodigo.Focus();
                    return;
                }

                try
                {
                    if (UECF.ImpressoraOK(1))
                    {
                        if (Movimento.Id == 0)
                        {
                            LabelMensagens.Text = "CAIXA FECHADO";
                            IniciaMovimento(); //  se o caixa estiver fechado abre o iniciaMovimento
                            return;
                        }

                        if (!MenuAberto)
                        {
                            if (StatusCaixa == 0)
                            {
                                IniciaVenda();
                            }
                            if (EditCodigo.Text.Trim() != "")
                            {
                                DesmembraCodigoDigitado(EditCodigo.Text.Trim());
                                Application.DoEvents();

                                if (Produto.Id != 0)
                                {
                                    if (Produto.ValorVenda <= 0)
                                    {
                                        MessageBox.Show("Produto não pode ser vendido com valor Zerado ou negativo!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        EditCodigo.Focus();
                                        EditCodigo.SelectAll();
                                        return;
                                    }

                                    LabelMensagens.Text = "Venda em andamento...";

                                    decimal Qtde = Convert.ToDecimal(EditQuantidade.Text);
                                    if ((Produto.PodeFracionarUnidade == "N") && (Qtde - Decimal.Truncate(Qtde) > 0))
                                    {
                                        MessageBox.Show("Produto não pode ser vendido com quantidade fracionada.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        EditUnitario.Text = "0";
                                        EditTotalItem.Text = "0";
                                        EditQuantidade.Text = "1";
                                        LabelDescricaoProduto.Text = "";
                                        EditCodigo.Text = "";
                                        EditCodigo.Focus();
                                    }
                                    else
                                    {
                                        EditUnitario.Text = Biblioteca.FormataFloat("V", Produto.ValorVenda);
                                        LabelDescricaoProduto.Text = Produto.DescricaoPDV;
                                        //  carrega imagem do produto
                                        if (File.Exists(Configuracao.CaminhoImagensProdutos + "padrao.png"))
                                            ImageProduto.ImageLocation = Configuracao.CaminhoImagensProdutos + "padrao.png";
                                        else
                                            ImageProduto.ImageLocation = Application.StartupPath + "\\imgProdutos\\padrao.png";
                                        if (File.Exists(Configuracao.CaminhoImagensProdutos + Produto.GTIN + ".jpg"))
                                            ImageProduto.ImageLocation = Configuracao.CaminhoImagensProdutos + Produto.GTIN + ".jpg";

                                        Unitario = Convert.ToDecimal(EditUnitario.Text);
                                        Quantidade = Convert.ToDecimal(EditQuantidade.Text);

                                        Total = Biblioteca.TruncaValor(Unitario * Quantidade, Constantes.DECIMAIS_VALOR);
                                        EditTotalItem.Text = Biblioteca.FormataFloat("V", Total);

                                        VendaDetalhe = new VendaDetalheVO();
                                        VendeItem();
                                        SubTotal = SubTotal + VendaDetalhe.ValorTotal;
                                        TotalGeral = TotalGeral + VendaDetalhe.ValorTotal;
                                        AtualizaTotais();
                                        EditCodigo.Clear();
                                        EditCodigo.Focus();
                                        EditQuantidade.Text = "1";
                                        Application.DoEvents();
                                    }
                                }
                                else
                                {
                                    MensagemDeProdutoNaoEncontrado();
                                }
                            }
                        }
                    }
                }
                catch (Exception eError)
                {
                    Log.write(eError.ToString());
                }
            }

        }


        public static void ConsultaProduto(string Codigo, int Tipo)
        {
            Produto = ProdutoController.Consulta(Codigo, Tipo);
        }


        public static void MensagemDeProdutoNaoEncontrado()
        {
            MessageBox.Show("Codigo não encontrado.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            EditUnitario.Text = "0";
            EditTotalItem.Text = "0";
            EditQuantidade.Text = "1";
            LabelDescricaoProduto.Text = "";
            EditCodigo.Focus();
            EditCodigo.SelectAll();
        }


        public static void DesmembraCodigoDigitado(string CodigoDeBarraOuDescricaoOuIdProduto)
        {
            string IdentificadorBalanca, vCodDescrId;
            int LengthCodigo;
            Int64 ValorInformado;
            IdentificadorBalanca = Configuracao.BalancaIdentificadorBalanca;
            vCodDescrId = CodigoDeBarraOuDescricaoOuIdProduto;
            LengthCodigo = CodigoDeBarraOuDescricaoOuIdProduto.Length;

            if (Int64.TryParse(CodigoDeBarraOuDescricaoOuIdProduto, out ValorInformado))
            {
                try
                {
                    if ((LengthCodigo == 13) || (LengthCodigo == 14))
                    {
                        ConsultaProduto(vCodDescrId, 2);
                        if (Produto.Id != 0)
                            return;
                    }
                    if ((LengthCodigo <= 4) && (BalancaLePeso == true))
                    {
                        ConsultaProduto(vCodDescrId, 1);
                        if (Produto.Id != 0)
                            return;
                    }
                    else
                    {
                        ConsultaProduto(vCodDescrId, 4);
                        if (Produto.Id != 0)
                            return;
                    }
                }
                finally
                {
                    BalancaLePeso = false;
                }

            }
            else
            {
                FImportaProduto FImportaProduto = new FImportaProduto();
                FImportaProduto.EditLocaliza.Text = vCodDescrId;
                FImportaProduto.ShowDialog();
            }
        }


        public void editCodigoKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (editCodigo.Text.Trim() != "")
                    IniciaVendaDeItens();
                else
                    editQuantidade.Focus();
            }
        }


        public void editCodigoKeyPress(object sender, KeyPressEventArgs e)
        {
            decimal Quantidade;

            if (e.KeyChar == '*')
            {
                e.Handled = true;
                try
                {
                    Quantidade = Convert.ToDecimal(EditCodigo.Text);
                    if ((Quantidade <= 0) || (Quantidade > 999))
                    {
                        MessageBox.Show("Quantidade invalida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        editCodigo.Text = "";
                        editQuantidade.Text = "1";
                    }
                    else
                    {
                        editQuantidade.Text = Quantidade.ToString();
                        editCodigo.Text = "";
                    }
                }
                catch (Exception eError)
                {
                    Log.write(eError.ToString());
                    MessageBox.Show("Quantidade invalida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    editCodigo.Text = "";
                    editQuantidade.Text = "1";
                }
            }

        }

        private void editQuantidade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                editCodigo.Focus();
            }
        }

        private void editQuantidade_Leave(object sender, EventArgs e)
        {
            decimal Quantidade;
            Quantidade = Convert.ToDecimal(editCodigo.Text);
            if ((Quantidade <= 0) || (Quantidade > 999))
            {
                MessageBox.Show("Quantidade invalida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                editQuantidade.Text = "1";
            }
        }


        public static void VendeItem()
        {
            CompoeItemParaVenda();

            if (!new AliquotasController().LocalizaAliquota(VendaDetalhe.ECFICMS))
            {
                MessageBox.Show("Produto com ICMS não Definido!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EditUnitario.Text = "0";
                EditTotalItem.Text = "0";
                EditSubTotal.Text = "0";
                EditCodigo.Focus();
                EditCodigo.SelectAll();
                ItemCupom--;
                return;
            }

            if (VendaDetalhe.ECFICMS == "")
            {
                MessageBox.Show("Produto com ICMS não Cadastrado na Impressora!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EditUnitario.Text = "0";
                EditTotalItem.Text = "0";
                EditSubTotal.Text = "0";
                EditCodigo.Focus();
                EditCodigo.SelectAll();
                ItemCupom--;
                return;
            }

            if (VendaDetalhe.GTIN == "")
            {
                MessageBox.Show("Produto com Codigo ou GTIN não Definido!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EditUnitario.Text = "0";
                EditTotalItem.Text = "0";
                EditSubTotal.Text = "0";
                EditCodigo.Focus();
                EditCodigo.SelectAll();
                ItemCupom--;
                return;
            }

            if (VendaDetalhe.DescricaoPDV == "")
            {
                MessageBox.Show("Produto com Descricao não Definida!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EditUnitario.Text = "0";
                EditTotalItem.Text = "0";
                EditSubTotal.Text = "0";
                EditCodigo.Focus();
                EditCodigo.SelectAll();
                ItemCupom--;
                return;
            }

            if (VendaDetalhe.UnidadeProduto == "")
            {
                MessageBox.Show("Produto com Unidade não Definida!", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EditUnitario.Text = "0";
                EditTotalItem.Text = "0";
                EditSubTotal.Text = "0";
                EditCodigo.Focus();
                EditCodigo.SelectAll();
                ItemCupom--;
                return;
            }

            //  vende item
            UECF.VendeItem(VendaDetalhe);
            VendaDetalhe = VendaController.InserirItem(VendaDetalhe);
            ListaVendaDetalhe.Add(VendaDetalhe);
            ImprimeItemBobina();
            Bobina.SelectedIndex = Bobina.Items.Count - 1;
        }



        public static void CompoeItemParaVenda()
        {
            try
            {
                ItemCupom++;
                VendaDetalhe.IdProduto = Produto.Id;
                VendaDetalhe.CFOP = Configuracao.CFOPECF;
                VendaDetalhe.IdVendaCabecalho = VendaCabecalho.Id;
                VendaDetalhe.DescricaoPDV = Produto.DescricaoPDV;
                VendaDetalhe.UnidadeProduto = Produto.UnidadeProduto;
                VendaDetalhe.CST = Produto.Cst;
                VendaDetalhe.ECFICMS = Produto.ECFICMS;
                VendaDetalhe.TaxaICMS = Produto.AliquotaICMS;
                VendaDetalhe.TotalizadorParcial = Produto.TotalizadorParcial;

                if (Produto.GTIN.Trim() == "")
                    VendaDetalhe.GTIN = Convert.ToString(Produto.Id);
                else
                    VendaDetalhe.GTIN = Produto.GTIN;

                VendaDetalhe.Item = ItemCupom;
                if (Produto.IPPT == "T")
                    VendaDetalhe.MovimentaEstoque = "S";
                else
                    VendaDetalhe.MovimentaEstoque = "N";
                if (StatusCaixa == 1)
                {
                    VendaDetalhe.Quantidade = Convert.ToDecimal(EditQuantidade.Text);
                    VendaDetalhe.ValorUnitario = Convert.ToDecimal(EditUnitario.Text);
                    VendaDetalhe.ValorTotal = Convert.ToDecimal(EditTotalItem.Text);
                    VendaDetalhe.TotalItem = Convert.ToDecimal(EditTotalItem.Text);
                }
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }

        }


        public static void ImprimeItemBobina()
        {
            string Quantidade, Unitario, Total, Unidade;
            Quantidade = VendaDetalhe.Quantidade.ToString("###,##0.00");
            Unitario = VendaDetalhe.ValorUnitario.ToString("###,##0.00");
            Total = VendaDetalhe.ValorTotal.ToString("###,##0.00");
            string DescricaoProduto = VendaDetalhe.DescricaoPDV.Length < 27 ? VendaDetalhe.DescricaoPDV : VendaDetalhe.DescricaoPDV.Substring(0, 27);
            Bobina.Items.Add(new string('0', 3 - Convert.ToString(ItemCupom).Length) + Convert.ToString(ItemCupom) + "  " + VendaDetalhe.GTIN + new string(' ', 14 - VendaDetalhe.GTIN.Length) + " " + DescricaoProduto);

            Unidade = VendaDetalhe.UnidadeProduto.Trim();

            Bobina.Items.Add(new string(' ', 8 - Quantidade.Length) + Quantidade + " " + new string(' ', 3 - Unidade.Length) + Unidade + " x " + new string(' ', 13 - Unitario.Length) + Unitario + " " + new string(' ', 5 - VendaDetalhe.ECFICMS.Length) + VendaDetalhe.ECFICMS + new string(' ', 14 - Total.Length) + Total);
        }


        public static void AtualizaTotais()
        {
            decimal DescontoAcrescimo;
            VendaCabecalho.ValorVenda = SubTotal;
            VendaCabecalho.Desconto = Desconto;
            VendaCabecalho.Acrescimo = Acrescimo;

            VendaCabecalho.ValorFinal = TotalGeral - Desconto + Acrescimo;
            DescontoAcrescimo = Acrescimo - Desconto;

            if (DescontoAcrescimo < 0)
            {
                LabelDescontoAcrescimo.Text = "Desconto: R$ " + (-DescontoAcrescimo).ToString("###,###,##0.00");
                LabelDescontoAcrescimo.ForeColor = Color.Red;
                VendaCabecalho.Desconto = -DescontoAcrescimo;
                VendaCabecalho.Acrescimo = 0;
            }
            else if (DescontoAcrescimo > 0)
            {
                LabelDescontoAcrescimo.Text = "Acrescimo: R$ " + DescontoAcrescimo.ToString("###,###,##0.00");
                LabelDescontoAcrescimo.ForeColor = Color.Blue;
                VendaCabecalho.Desconto = 0;
                VendaCabecalho.Acrescimo = DescontoAcrescimo;
            }
            else
            {
                LabelDescontoAcrescimo.Text = "";
                VendaCabecalho.TaxaAcrescimo = 0;
                VendaCabecalho.TaxaDesconto = 0;
                Acrescimo = 0;
                Desconto = 0;
            }

            if (((VendaCabecalho.ValorFinal < VendaCabecalho.ValorVenda) && (VendaCabecalho.TaxaDesconto != 0) && (Desconto != ((1 - (VendaCabecalho.ValorFinal / VendaCabecalho.ValorVenda)) * 100))))
            {
                VendaCabecalho.TaxaDesconto = (1 - (VendaCabecalho.ValorFinal / VendaCabecalho.ValorVenda)) * 100;
                VendaCabecalho.TaxaAcrescimo = 0;
            }

            if (((VendaCabecalho.ValorFinal > VendaCabecalho.ValorVenda) && (VendaCabecalho.TaxaAcrescimo != 0) && (Acrescimo != ((VendaCabecalho.ValorFinal / VendaCabecalho.ValorVenda) * 100) - 100)))
            {
                VendaCabecalho.TaxaAcrescimo = ((VendaCabecalho.ValorFinal / VendaCabecalho.ValorVenda) * 100) - 100;
                VendaCabecalho.TaxaDesconto = 0;
            }

            EditSubTotal.Text = VendaCabecalho.ValorVenda.Value.ToString("###,###,##0.00");
            LabelTotalGeral.Text = VendaCabecalho.ValorFinal.Value.ToString("###,###,##0.00");
        }


        public static void IniciaEncerramentoVenda()
        {
            if (StatusCaixa != 3)
            {
                if (StatusCaixa == 1)
                {
                    if (ListaVendaDetalhe.Count > 0)
                    {
                        VendaCabecalho.CupomFoiCancelado = "N";
                        if (VendaCabecalho.ValorFinal <= 0)
                        {
                            if (MessageBox.Show("Todos os itens foram cancelados." + "\r" + "\r" + "Deseja cancelar o cupom?", "Cancelar o cupom", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                CancelaCupom();
                            return;
                        }

                        ProblemaNoPagamento = false;
                        FEfetuaPagamento FEfetuaPagamento = new FEfetuaPagamento();
                        FEfetuaPagamento.ShowDialog();
                        //EdtNVenda.Text = "";
                        //EdtCOO.Text = "";
                    }
                    else
                        MessageBox.Show("A venda não contem itens.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Não existe venda em andamento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Terminal em Estado Somente Consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public static void ConcluiEncerramentoVenda()
        {
            try
            {
                VendaController.EncerraVenda(VendaCabecalho);
            }
            finally
            {
                VendaCabecalho = null;
                ListaVendaDetalhe = null;
                Produto = null;
                TelaPadrao(1);
            }
        }


        public static void CancelaCupom()
        {

            if (StatusCaixa != 3)
            {
                if (Movimento.Id == 0)
                    MessageBox.Show("Não existe um movimento aberto.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    if ((StatusCaixa == 0) || (StatusCaixa == 1))
                    {
                        FLoginGerenteSupervisor FLoginGerenteSupervisor = new FLoginGerenteSupervisor();
                        try
                        {
                            if (FLoginGerenteSupervisor.ShowDialog() == DialogResult.OK)
                            {
                                if (FLoginGerenteSupervisor.LoginOK)
                                {
                                    if (StatusCaixa == 1)
                                    {
                                        if (MessageBox.Show("Deseja cancelar o cupom atual?", "Pergunta do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            UECF.CancelaCupom();
                                            VendaController.CancelaVenda(VendaCabecalho, ListaVendaDetalhe);
                                            StatusCaixa = 0;
                                            TelaPadrao(1);
                                        }
                                    }
                                    else if (StatusCaixa == 0)
                                    {
                                        if (MessageBox.Show("Deseja cancelar o cupom anterior?", "Pergunta do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            //  verifica se a ultima venda Ja teve o seu cupom cancelado
                                            if (VendaController.CupomJaFoiCancelado())
                                            {
                                                MessageBox.Show("O cupom referente à última venda Ja foi cancelado.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            else
                                            {
                                                if (VendaController.CancelaVendaAnterior())
                                                {
                                                    MessageBox.Show("Cupom cancelado com sucesso.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                                else
                                                    MessageBox.Show("Problemas ao cancelar cupom anterior.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }
                                    }
                                }
                                else
                                    MessageBox.Show("Login - dados incorretos.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception eError)
                        {
                            Log.write(eError.ToString());
                        }
                    }
                    else
                        MessageBox.Show("Não existe venda em andamento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
                MessageBox.Show("Terminal em Estado Somente Consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        public static void CancelaItem()
        {
            int cancela;
            if (StatusCaixa != 3)
            {
                if (StatusCaixa == 1)
                {
                    FLoginGerenteSupervisor FLoginGerenteSupervisor = new FLoginGerenteSupervisor();
                    try
                    {
                        if (FLoginGerenteSupervisor.ShowDialog() == DialogResult.OK)
                        {
                            if (FLoginGerenteSupervisor.LoginOK)
                            {

                                FImportaNumero FImportaNumero = new FImportaNumero();
                                FImportaNumero.Text = "Cancela Item";
                                FImportaNumero.LabelEntrada.Text = "Informe o código do item:";

                                if (FImportaNumero.ShowDialog() == DialogResult.OK)
                                {
                                    cancela = Convert.ToInt32(FImportaNumero.EditEntrada.Text);
                                    if (cancela > 0)
                                    {
                                        if (cancela <= ListaVendaDetalhe.Count)
                                        {
                                            VendaDetalhe = ListaVendaDetalhe[cancela - 1];

                                            if (VendaDetalhe.Cancelado != "S")
                                            {
                                                UECF.CancelaItem(cancela);

                                                VendaDetalhe.Cancelado = "S";
                                                VendaController.CancelaItem(VendaDetalhe);

                                                Bobina.Items.Add(new string('*', 48));
                                                string DescricaoProduto = VendaDetalhe.DescricaoPDV.Length < 27 ? VendaDetalhe.DescricaoPDV : VendaDetalhe.DescricaoPDV.Substring(0, 27);
                                                Bobina.Items.Add(new string('0', 3 - Convert.ToString(cancela).Length) + Convert.ToString(cancela) + "  " + VendaDetalhe.GTIN + new string(' ', 14 - VendaDetalhe.GTIN.Length) + " " + DescricaoProduto);

                                                Bobina.Items.Add("ITEM CANCELADO");
                                                Bobina.Items.Add(new string('*', 48));

                                                SubTotal = SubTotal - VendaDetalhe.ValorTotal;
                                                TotalGeral = TotalGeral - VendaDetalhe.ValorTotal;

                                                //  cancela possíveis descontos ou acrescimos
                                                Desconto = 0;
                                                Acrescimo = 0;
                                                VendaCabecalho.TaxaAcrescimo = 0;
                                                VendaCabecalho.TaxaDesconto = 0;
                                                Bobina.SelectedIndex = Bobina.Items.Count - 1;
                                                AtualizaTotais();
                                            }
                                            else
                                                MessageBox.Show("O item solicitado Ja foi cancelado.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                            MessageBox.Show("O item solicitado não existe na venda atual.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                        MessageBox.Show("Informe um numero de item valido.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                                MessageBox.Show("Login - dados incorretos.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception eError)
                    {
                        Log.write(eError.ToString());
                    }
                }
                else
                    MessageBox.Show("Não existe venda em andamento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Terminal em Estado Somente Consulta.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public static void CancelaItem(int cancela)
        {
            try
            {
                if (cancela > 0)
                {
                    if (cancela <= ListaVendaDetalhe.Count)
                    {
                        VendaDetalhe = ListaVendaDetalhe[cancela - 1];

                        if (VendaDetalhe.Cancelado != "S")
                        {

                            UECF.CancelaItem(cancela);

                            VendaDetalhe.Cancelado = "S";
                            VendaController.CancelaItem(VendaDetalhe);

                            Bobina.Items.Add(new string('*', 48));
                            string DescricaoProduto = VendaDetalhe.DescricaoPDV.Length < 27 ? VendaDetalhe.DescricaoPDV : VendaDetalhe.DescricaoPDV.Substring(0, 27);
                            Bobina.Items.Add(new string('0', 3 - Convert.ToString(cancela).Length) + Convert.ToString(cancela) + "  " + VendaDetalhe.GTIN + new string(' ', 14 - VendaDetalhe.GTIN.Length) + " " + DescricaoProduto);

                            Bobina.Items.Add("ITEM CANCELADO");
                            Bobina.Items.Add(new string('*', 48));

                            SubTotal = SubTotal - VendaDetalhe.ValorTotal;
                            TotalGeral = TotalGeral - VendaDetalhe.ValorTotal;

                            //  cancela possíveis descontos ou acrescimos
                            Desconto = 0;
                            Acrescimo = 0;
                            VendaCabecalho.TaxaAcrescimo = 0;
                            VendaCabecalho.TaxaDesconto = 0;

                            AtualizaTotais();
                        }
                        else
                            MessageBox.Show("O item solicitado Ja foi cancelado.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("O item solicitado não existe na venda atual.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        public void DesabilitaControlesVenda()
        {
            editCodigo.Enabled = false;
            editQuantidade.Enabled = false;
            editUnitario.Enabled = false;
            editTotalItem.Enabled = false;
            editSubTotal.Enabled = false;
            bobina.Enabled = false;
            PanelBotoes.Enabled = false;
        }


        public void HabilitaControlesVenda()
        {
            editCodigo.Enabled = true;
            editQuantidade.Enabled = true;
            editUnitario.Enabled = true;
            editTotalItem.Enabled = true;
            editSubTotal.Enabled = true;
            bobina.Enabled = true;
            PanelBotoes.Enabled = true;
        }


        public void EntradaDadosNF()
        {
            int GuardaStatus;
            if (Configuracao.LancamentoNotasManuais != 1)
            {
                MessageBox.Show("Lancamento de notas manuais não disponivel.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (StatusCaixa != 1)
            {
                try
                {
                    GuardaStatus = StatusCaixa;
                    StatusCaixa = 5;
                    FNotaFiscal FNotaFiscal = new FNotaFiscal();
                    FNotaFiscal.ShowDialog();
                    StatusCaixa = 0;
                }
                catch (Exception eError)
                {
                    Log.write(eError.ToString());
                }
            }
            else
                MessageBox.Show("Existe uma venda em andamento.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion Procedimentos para controle da venda


        //TODO: Leitor: Conclua a implementação da integração com balanças
        #region Procedimentos para ler peso direto das balanças componente ACBrBal
        public void ConectaComBalanca()
        {
            try
            {
                //  se houver conexão aberta, Fecha a conexão
                if (FDataModule.ACBrBAL.Ativo)
                    FDataModule.ACBrBAL.Desativar();

                //  configura porta de comunicacao
                FDataModule.ACBrBAL.Modelo = ACBrFramework.BAL.ModeloBal.Filizola; // Configuracao.BalancaModelo;
                FDataModule.ACBrBAL.Device.HandShake = ACBrFramework.SerialHandShake.XON_XOFF; // Configuracao.BalancaHandShaking;
                FDataModule.ACBrBAL.Device.Parity = ACBrFramework.SerialParity.None; // Configuracao.BalancaParity;
                FDataModule.ACBrBAL.Device.StopBits = ACBrFramework.SerialStopBits.One; // Configuracao.BalancaStopBits;
                FDataModule.ACBrBAL.Device.DataBits = Configuracao.BalancaDataBits;
                FDataModule.ACBrBAL.Device.Baud = Configuracao.BalancaBaudRate;
                FDataModule.ACBrBAL.Device.Porta = Configuracao.BalancaPortaSerial;
                //  Conecta com a balança
                FDataModule.ACBrBAL.Ativar();
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }

        //TODO:  Onde deve ficar esse procedimento? Ou o componente ACBrBal deve ser movido para esse formulário? Implemente da forma que achar melhor.
        public void ACBrBAL1LePeso(decimal Peso, string Resposta)
        {
            int valid;
            editCodigo.Text = Peso.ToString("0.000") + "*";
            if (Peso > 0)
            {
                labelMensagens.Text = "Leitura da Balança OK !";
                editQuantidade.Text = Peso.ToString("0.000");
                editCodigo.Focus();
            }
            else
            {
                valid = Convert.ToInt32(FDataModule.ACBrBAL.UltimoPesoLido); switch (valid)
                {
                    case 0:
                        labelMensagens.Text = "Coloque o produto sobre a Balança!"; break;
                    case -1:
                        labelMensagens.Text = "Tente Nova Leitura"; break;
                    case -2:
                        labelMensagens.Text = "Peso Negativo !"; break;
                    case -10:
                        labelMensagens.Text = "Sobrepeso !"; break;
                }
            }
        }

        #endregion Procedimentos para ler peso direto das balanças componente ACBrBal



        #region Controle dos cliques nos botões de função
        private void botaoF1_Click(object sender, EventArgs e)
        {
            IdentificaCliente();
        }

        private void botaoF2_Click(object sender, EventArgs e)
        {
            AcionaMenuPrincipal();
        }

        private void botaoF3_Click(object sender, EventArgs e)
        {
            AcionaMenuOperacoes();
        }

        private void botaoF4_Click(object sender, EventArgs e)
        {
            AcionaMenuFiscal();
        }

        private void botaoF5_Click(object sender, EventArgs e)
        {
            EntradaDadosNF();
        }

        private void botaoF6_Click(object sender, EventArgs e)
        {
            LocalizaProduto();
        }

        private void botaoF7_Click(object sender, EventArgs e)
        {
            IniciaEncerramentoVenda();
        }

        private void botaoF8_Click(object sender, EventArgs e)
        {
            CancelaItem();
        }

        private void botaoF9_Click(object sender, EventArgs e)
        {
            CancelaCupom();
        }

        private void botaoF10_Click(object sender, EventArgs e)
        {
            DescontoOuAcrescimo();
        }

        private void botaoF11_Click(object sender, EventArgs e)
        {
            IdentificaVendedor();
        }

        private void botaoF12_Click(object sender, EventArgs e)
        {
            FormCloseQuery();
        }
        #endregion Controle dos cliques nos botões de função

    }

}

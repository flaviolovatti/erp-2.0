﻿/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emissão de Nota Fiscal Eletrônica - NFe e Nota Fiscal de  */
/* Consumidor Eletrônica - NFC-e (http://www.nfe.fazenda.gov.br)                */
/*                                                                              */
/* Direitos Autorais Reservados (c) 2014 Adenilton Batista da Silva             */
/*                                       Zeusdev Tecnologia LTDA ME             */
/*                                                                              */
/*  Você pode obter a última versão desse arquivo no GitHub                     */
/* localizado em https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe               */
/*                                                                              */
/*                                                                              */
/*  Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la */
/* sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela  */
/* Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério) */
/* qualquer versão posterior.                                                   */
/*                                                                              */
/*  Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM   */
/* NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU      */
/* ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor*/
/* do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)              */
/*                                                                              */
/*  Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto*/
/* com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,  */
/* no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.          */
/* Você também pode obter uma copia da licença em:                              */
/* http://www.opensource.org/licenses/lgpl-license.php                          */
/*                                                                              */
/* Zeusdev Tecnologia LTDA ME - adenilton@zeusautomacao.com.br                  */
/* http://www.zeusautomacao.com.br/                                             */
/* Rua Comendador Francisco josé da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;
using NFe.Classes;
using NFe.Classes.Informacoes;
using NFe.Classes.Informacoes.Cobranca;
using NFe.Classes.Informacoes.Destinatario;
using NFe.Classes.Informacoes.Detalhe;
using NFe.Classes.Informacoes.Detalhe.Tributacao;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using NFe.Classes.Informacoes.Emitente;
using NFe.Classes.Informacoes.Identificacao;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Informacoes.Pagamento;
using NFe.Classes.Informacoes.Total;
using NFe.Classes.Informacoes.Transporte;
using NFe.Classes.Servicos.Tipos;
using NFe.Servicos;
using NFe.Servicos.Retorno;
using NFe.Utils;
using NFe.Utils.Assinatura;
using NFe.Utils.NFe;
using Xceed.Wpf.Toolkit.Core.Input;
using RichTextBox = System.Windows.Controls.RichTextBox;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using WebBrowser = System.Windows.Controls.WebBrowser;

namespace NFe.AppTeste
{
    /// <summary>
    ///     Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow
    {
        private const string ArquivoConfiguracao = @"\configuracao.xml";
        private const string TituloErro = "Erro";
        private ConfiguracaoApp _configuracoes;
        private Classes.NFe _nfe;
        private readonly string _path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public MainWindow()
        {
            InitializeComponent();
            CarregarConfiguracao();
            DataContext = _configuracoes;
        }

        private void CarregaDadosCertificado()
        {
            try
            {
                var cert = new CertificadoDigital();
                TxtCertificado.Text = cert.Serial;
                //lblCertificadoValidade.Content = cert.Validade.ToString();
            }
            catch (Exception ex)
            {
                Funcoes.Mensagem(ex.Message, TituloErro, MessageBoxButton.OK);
            }
        }

        private void btnCertificado_Click(object sender, RoutedEventArgs e)
        {
            CarregaDadosCertificado();
        }

        private void txt_InputValidationError(object sender, InputValidationErrorEventArgs e)
        {
            Funcoes.Mensagem("Valor Inválido", TituloErro, MessageBoxButton.OK);
        }

        private void BtnStatusServico_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Status do serviço

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoStatus = servicoNFe.NfeStatusServico();

                TrataRetorno(retornoStatus);

                #endregion
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            SalvarConfiguracao();
        }

        private void SalvarConfiguracao()
        {
            try
            {
                _configuracoes.Salvar(_path + ArquivoConfiguracao);
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(String.Format("{0} \nDetalhes: {1}", ex.Message, ex.InnerException.Message), "Erro", MessageBoxButton.OK);                
            }
        }

        private void CarregarConfiguracao()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            try
            {
                _configuracoes = !File.Exists(path + ArquivoConfiguracao) ? new ConfiguracaoApp() : FuncoesXml.ArquivoXmlParaClasse<ConfiguracaoApp>(path + ArquivoConfiguracao);
                if (_configuracoes.CfgServico.TimeOut == 0)
                    _configuracoes.CfgServico.TimeOut = 100;//mínimo
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnConsultaChave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Consulta Situação NFe

                var chave = Funcoes.InpuBox(this, "Consultar NFe pela Chave", "Chave da NFe:");
                if (String.IsNullOrEmpty(chave)) throw new Exception("A Chave deve ser informada!");
                if (chave.Length != 44) throw new Exception("Chave deve conter 44 caracteres!");

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoConsulta = servicoNFe.NfeConsultaProtocolo(chave);
                TrataRetorno(retornoConsulta);

                #endregion
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnConsultaXml_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Consulta Situação NFe pelo XML

                var arquivoXml = Funcoes.BuscarArquivoXml();
                var nfe = new Classes.NFe().CarregarDeArquivoXml(arquivoXml);
                var chave = nfe.infNFe.Id.Substring(3);

                if (String.IsNullOrEmpty(chave)) throw new Exception("A Chave da NFe não foi encontrada no arquivo!");
                if (chave.Length != 44) throw new Exception("Chave deve conter 44 caracteres!");

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoConsulta = servicoNFe.NfeConsultaProtocolo(chave);
                TrataRetorno(retornoConsulta);

                #endregion
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnCriareEnviar2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Cria e Envia NFe

                var numero = Funcoes.InpuBox(this, "Criar e Enviar NFe", "Número da Nota:");
                if (String.IsNullOrEmpty(numero)) throw new Exception("O Número deve ser informado!");

                var lote = Funcoes.InpuBox(this, "Criar e Enviar NFe", "Id do Lote:");
                if (String.IsNullOrEmpty(lote)) throw new Exception("A Id do lote deve ser informada!");

                _nfe = GetNf(Convert.ToInt32(numero), ModeloDocumento.NFe, _configuracoes.CfgServico.VersaoNfeRecepcao);
                _nfe.Assina(); //não precisa validar aqui, pois o lote será validado em ServicosNFe.NFeAutorizacao
                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoEnvio = servicoNFe.NfeRecepcao(Convert.ToInt32(lote), new List<Classes.NFe> {_nfe});

                TrataRetorno(retornoEnvio);

                #endregion
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnGerarNfe2_Click(object sender, RoutedEventArgs e)
        {
            GeranNfe(_configuracoes.CfgServico.VersaoNfeRecepcao, ModeloDocumento.NFe);
        }

        private void GeranNfe(VersaoServico versaoServico, ModeloDocumento modelo)
        {
            try
            {
                #region Gerar NFe

                var numero = Funcoes.InpuBox(this, "Criar e Enviar NFe", "Número da Nota:");
                if (String.IsNullOrEmpty(numero)) throw new Exception("O Número deve ser informado!");

                _nfe = GetNf(Convert.ToInt32(numero), modelo, versaoServico);
                _nfe.Assina().Valida();

                #endregion

                ExibeNfe();

                var dlg = new SaveFileDialog
                {
                    FileName = _nfe.infNFe.Id.Substring(3),
                    DefaultExt = ".xml",
                    Filter = "Arquivo XML (.xml)|*.xml"
                };
                var result = dlg.ShowDialog();
                if (result != true) return;
                var arquivoXml = dlg.FileName;
                _nfe.SalvarArquivoXml(arquivoXml);
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnConsultarReciboLote2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Consulta Recibo de lote

                var recibo = Funcoes.InpuBox(this, "Consultar processamento de lote de NF-e", "Número do recibo:");
                if (String.IsNullOrEmpty(recibo)) throw new Exception("O número do recibo deve ser informado!");
                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoRecibo = servicoNFe.NfeRetRecepcao(recibo);

                TrataRetorno(retornoRecibo);

                #endregion
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnCriareEnviar3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Cria e Envia NFe

                var numero = Funcoes.InpuBox(this, "Criar e Enviar NFe", "Número da Nota:");
                if (String.IsNullOrEmpty(numero)) throw new Exception("O Número deve ser informado!");

                var lote = Funcoes.InpuBox(this, "Criar e Enviar NFe", "Id do Lote:");
                if (String.IsNullOrEmpty(lote)) throw new Exception("A Id do lote deve ser informada!");

                _nfe = GetNf(Convert.ToInt32(numero), ModeloDocumento.NFe, _configuracoes.CfgServico.VersaoNFeAutorizacao);
                _nfe.Assina(); //não precisa validar aqui, pois o lote será validado em ServicosNFe.NFeAutorizacao
                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoEnvio = servicoNFe.NFeAutorizacao(Convert.ToInt32(lote), IndicadorSincronizacao.Assincrono, new List<Classes.NFe> {_nfe});

                TrataRetorno(retornoEnvio);

                #endregion
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnConsultarReciboLote3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Consulta Recibo de lote

                var recibo = Funcoes.InpuBox(this, "Consultar processamento de lote de NF-e", "Número do recibo:");
                if (String.IsNullOrEmpty(recibo)) throw new Exception("O número do recibo deve ser informado!");
                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoRecibo = servicoNFe.NFeRetAutorizacao(recibo);

                TrataRetorno(retornoRecibo);

                #endregion
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void TrataRetorno(RetornoBasico retornoBasico)
        {
            EnvioStr(RtbEnvioStr, retornoBasico.EnvioStr);
            RetornoStr(RtbRetornoStr, retornoBasico.RetornoStr);
            RetornoXml(WebXmlRetorno, retornoBasico.RetornoStr);
            RetornoCompletoStr(RtbRetornoCompletoStr, retornoBasico.RetornoCompletoStr);
            RetornoDados(retornoBasico.Retorno, RtbDadosRetorno);
        }

        private void BtnGerarNfe3_Click(object sender, RoutedEventArgs e)
        {
            GeranNfe(_configuracoes.CfgServico.VersaoNFeAutorizacao, ModeloDocumento.NFe);
        }

        private void BtnInutiliza_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Inutiliza Numeração

                var ano = Funcoes.InpuBox(this, "Inutilizar Numeração", "Ano");
                if (String.IsNullOrEmpty(ano)) throw new Exception("O Ano deve ser informado!");
                if (ano.Length > 2) throw new Exception("O Ano deve ter dois números apenas!");

                var modelostr = Funcoes.InpuBox(this, "Inutilizar Numeração", "Modelo");
                if (String.IsNullOrEmpty(modelostr)) throw new Exception("O Modelo deve ser informado!");

                var modelo = (ModeloDocumento) Convert.ToInt16(modelostr);

                var serie = Funcoes.InpuBox(this, "Inutilizar Numeração", "Série");
                if (String.IsNullOrEmpty(serie)) throw new Exception("A série deve ser informada!");

                var numeroInicial = Funcoes.InpuBox(this, "Inutilizar Numeração", "Número Inicial");
                if (String.IsNullOrEmpty(numeroInicial)) throw new Exception("O Número Inicial deve ser informado!");

                var numeroFinal = Funcoes.InpuBox(this, "Inutilizar Numeração", "Número Final");
                if (String.IsNullOrEmpty(numeroFinal)) throw new Exception("O Número Final deve ser informado!");

                var justificativa = Funcoes.InpuBox(this, "Inutilizar Numeração", "Justificativa");
                if (String.IsNullOrEmpty(justificativa)) throw new Exception("A Justificativa deve ser informada!");

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoConsulta = servicoNFe.NfeInutilizacao(_configuracoes.Emitente.CNPJ, Convert.ToInt16(ano), modelo, Convert.ToInt16(serie), Convert.ToInt16(numeroInicial),
                    Convert.ToInt16(numeroFinal), justificativa);

                TrataRetorno(retornoConsulta);

                #endregion
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnCartaCorrecao_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Carta de correção

                var idlote = Funcoes.InpuBox(this, "Carta de correção", "Identificador de controle do Lote de envio:");
                if (String.IsNullOrEmpty(idlote)) throw new Exception("A Id do Lote deve ser informada!");

                var sequenciaEvento = Funcoes.InpuBox(this, "Carta de correção", "Número sequencial do evento:");
                if (String.IsNullOrEmpty(sequenciaEvento)) throw new Exception("O número sequencial deve ser informado!");

                var chave = Funcoes.InpuBox(this, "Carta de correção", "Chave da NFe:");
                if (String.IsNullOrEmpty(chave)) throw new Exception("A Chave deve ser informada!");
                if (chave.Length != 44) throw new Exception("Chave deve conter 44 caracteres!");

                var correcao = Funcoes.InpuBox(this, "Carta de correção", "Correção");
                if (String.IsNullOrEmpty(correcao)) throw new Exception("A Correção deve ser informada!");

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var cpfcnpj = String.IsNullOrEmpty(_configuracoes.Emitente.CNPJ) ? _configuracoes.Emitente.CPF : _configuracoes.Emitente.CNPJ;
                var retornoCartaCorrecao = servicoNFe.RecepcaoEventoCartaCorrecao(Convert.ToInt16(idlote), Convert.ToInt16(sequenciaEvento), chave, correcao, cpfcnpj);
                TrataRetorno(retornoCartaCorrecao);

                #endregion
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnCancelarNFe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Cancelar NFe

                var idlote = Funcoes.InpuBox(this, "Cancelar NFe", "Identificador de controle do Lote de envio:");
                if (String.IsNullOrEmpty(idlote)) throw new Exception("A Id do Lote deve ser informada!");

                var sequenciaEvento = Funcoes.InpuBox(this, "Cancelar NFe", "Número sequencial do evento:");
                if (String.IsNullOrEmpty(sequenciaEvento)) throw new Exception("O número sequencial deve ser informado!");

                var protocolo = Funcoes.InpuBox(this, "Cancelar NFe", "Protocolo de Autorização da NFe:");
                if (String.IsNullOrEmpty(protocolo)) throw new Exception("O protocolo deve ser informado!");

                var chave = Funcoes.InpuBox(this, "Cancelar NFe", "Chave da NFe:");
                if (String.IsNullOrEmpty(chave)) throw new Exception("A Chave deve ser informada!");
                if (chave.Length != 44) throw new Exception("Chave deve conter 44 caracteres!");

                var justificativa = Funcoes.InpuBox(this, "Cancelar NFe", "Justificativa");
                if (String.IsNullOrEmpty(justificativa)) throw new Exception("A justificativa deve ser informada!");

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var cpfcnpj = String.IsNullOrEmpty(_configuracoes.Emitente.CNPJ) ? _configuracoes.Emitente.CPF : _configuracoes.Emitente.CNPJ;
                var retornoCancelamento = servicoNFe.RecepcaoEventoCancelamento(Convert.ToInt16(idlote), Convert.ToInt16(sequenciaEvento), protocolo, chave, justificativa, cpfcnpj);
                TrataRetorno(retornoCancelamento);

                #endregion
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnEnviaEpec_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /*
                 * Atenção:
                 * O campo dhEmi da nfe a ser vinculada ao EPEC deve ser exatamente igual ao informado m detevento do EPEC, assim como os demais dados, como emitente, destinatário, etc. 
                 * Vide a rejeição código 467 no manual do EPEC
                */

                #region Enviar EPEC

                var idlote = Funcoes.InpuBox(this, "Enviar EPEC", "Identificador de controle do Lote de envio:");
                if (String.IsNullOrEmpty(idlote)) throw new Exception("A Id do Lote deve ser informada!");

                var sequenciaEvento = Funcoes.InpuBox(this, "Enviar EPEC", "Número sequencial do evento:");
                if (String.IsNullOrEmpty(sequenciaEvento)) throw new Exception("O número sequencial deve ser informado!");

                var numeronota = Funcoes.InpuBox(this, "Enviar EPEC", "Número da Nota:");
                if (String.IsNullOrEmpty(numeronota)) throw new Exception("O Número da Nota deve ser informado!");

                _nfe = GetNf(Convert.ToInt32(numeronota), ModeloDocumento.NFe, _configuracoes.CfgServico.VersaoNFeAutorizacao);

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoEpec = servicoNFe.RecepcaoEventoEpec(Convert.ToInt16(idlote), Convert.ToInt16(sequenciaEvento), _nfe, "3.10");
                TrataRetorno(retornoEpec);

                #endregion
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnConsultaEpec_Click(object sender, RoutedEventArgs e)
        {
            /*
             * Atenção:
             * Depois de enviar a EPEC, leva um tempo para a consulta retornar os dados, pois o ambiente AN precisa replicar os dados para o serviço de consulta dos demais estados
             * Se uma NFe já foi conciliada para a EPEC, então o serviço retornará os dados de autorização da nfe e a EPEC virá no grupo procEventoNFe(eventos)
            */
            BtnConsultaChave_Click(sender, e); //Nada mais que uma consulta ao serviço NfeConsultaProtocolo, passando a chave gerada pelo EPEC
        }

        private void BtnDiretorioSchema_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dlg = new FolderBrowserDialog();
                dlg.ShowDialog();
                TxtDiretorioSchema.Text = dlg.SelectedPath;
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnImportarXml_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CarregaArquivoNfe();
                ExibeNfe();
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void ExibeNfe()
        {
            _nfe.SalvarArquivoXml(_path + @"\tmp.xml");
            WebXmlNfe.Navigate(_path + @"\tmp.xml");
            TabItemNfe.IsSelected = true;
        }

        private void CarregaArquivoNfe()
        {
            var arquivoXml = Funcoes.BuscarArquivoXml();
            _nfe = new Classes.NFe().CarregarDeArquivoXml(arquivoXml);
        }

        private void BtnValida_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_nfe == null)
                {
                    CarregaArquivoNfe();
                }

                if (_nfe == null) return;
                _nfe.Valida();
                Funcoes.Mensagem(String.Format("NFe número {0} validada com sucesso!", _nfe.infNFe.ide.nNF), "Atenção", MessageBoxButton.OK);
                ExibeNfe();
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnAssina_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_nfe == null)
                {
                    CarregaArquivoNfe();
                }

                if (_nfe == null) return;
                _nfe.Assina();
                Funcoes.Mensagem(String.Format("NFe número {0} assinada com sucesso!", _nfe.infNFe.ide.nNF), "Atenção", MessageBoxButton.OK);
                ExibeNfe();
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnCriareEnviarNfce_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Cria e Envia NFe

                var numero = Funcoes.InpuBox(this, "Criar e Enviar NFCe", "Número da NFCe:");
                if (String.IsNullOrEmpty(numero)) throw new Exception("O Número deve ser informado!");

                var lote = Funcoes.InpuBox(this, "Criar e Enviar NFCe", "Id do Lote:");
                if (String.IsNullOrEmpty(lote)) throw new Exception("A Id do lote deve ser informada!");

                _nfe = GetNf(Convert.ToInt32(numero), ModeloDocumento.NFCe, _configuracoes.CfgServico.VersaoNFeAutorizacao);
                _nfe.Assina(); //não precisa validar aqui, pois o lote será validado em ServicosNFe.NFeAutorizacao
                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoEnvio = servicoNFe.NFeAutorizacao(Convert.ToInt32(lote), IndicadorSincronizacao.Assincrono, new List<Classes.NFe> {_nfe});

                TrataRetorno(retornoEnvio);

                #endregion
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnDiretorioXml_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dlg = new FolderBrowserDialog();
                dlg.ShowDialog();
                TxtDiretorioXml.Text = dlg.SelectedPath;
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void CbxSalvarXml_Click(object sender, RoutedEventArgs e)
        {
            if (CbxSalvarXml.IsChecked == true)
                BtnDiretorioXml_Click(sender, e);
        }

        private void BtnAdicionaNfeproc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var arquivoXml = Funcoes.BuscarArquivoXml();
                var nfe = new Classes.NFe().CarregarDeArquivoXml(arquivoXml);
                var chave = nfe.infNFe.Id.Substring(3);

                if (String.IsNullOrEmpty(chave)) throw new Exception("A Chave da NFe não foi encontrada no arquivo!");
                if (chave.Length != 44) throw new Exception("Chave deve conter 44 caracteres!");

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoConsulta = servicoNFe.NfeConsultaProtocolo(chave);
                TrataRetorno(retornoConsulta);

                var nfeproc = new nfeProc {NFe = nfe, protNFe = retornoConsulta.Retorno.protNFe, versao = retornoConsulta.Retorno.versao};
                var novoArquivo = Path.GetDirectoryName(arquivoXml) + @"\" + nfeproc.protNFe.infProt.chNFe + "-procNfe.xml";
                FuncoesXml.ClasseParaArquivoXml(nfeproc, novoArquivo);
                Funcoes.Mensagem("Arquivo salvo em " + novoArquivo, "Atenção", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnCarregaXmlEnvia_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var lote = Funcoes.InpuBox(this, "Criar e Enviar NFe", "Id do Lote:");
                if (String.IsNullOrEmpty(lote)) throw new Exception("A Id do lote deve ser informada!");

                BtnImportarXml_Click(sender, e);
                _nfe.Assina(); //não precisa validar aqui, pois o lote será validado em ServicosNFe.NFeAutorizacao
                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoEnvio = servicoNFe.NFeAutorizacao(Convert.ToInt32(lote), IndicadorSincronizacao.Assincrono, new List<Classes.NFe> {_nfe});

                TrataRetorno(retornoEnvio);
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        private void BtnConsultaCadastro_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Consulta Cadastro

                var uf = Funcoes.InpuBox(this, "Consultar Cadastro", "UF do Documento a ser Consultado:");
                if (String.IsNullOrEmpty(uf)) throw new Exception("A UF deve ser informada!");
                if (uf.Length != 2) throw new Exception("UF deve conter 2 caracteres!");

                var documento = Funcoes.InpuBox(this, "Consultar Cadastro", "Documento(CPF/CNPJ):");
                if (String.IsNullOrEmpty(documento)) throw new Exception("O Documento(CPF/CNPJ) deve ser informado!");
                if (documento.Length != 11 & documento.Length != 14) throw new Exception("O Documento(CPF/CNPJ) deve conter 11 ou 14 caracteres!");

                var servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoConsulta = servicoNFe.NfeConsultaCadastro(uf, documento);
                TrataRetorno(retornoConsulta);

                #endregion
            }
            catch (Exception ex)
            {
                if (!String.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        #region Criar NFe

        protected virtual Classes.NFe GetNf(int numero, ModeloDocumento modelo, VersaoServico versao)
        {
            var nf = new Classes.NFe {infNFe = GetInf(numero, modelo, versao)};
            return nf;
        }

        protected virtual infNFe GetInf(int numero, ModeloDocumento modelo, VersaoServico versao)
        {
            var infNFe = new infNFe
            {
                versao = Auxiliar.VersaoServicoParaString(versao),
                ide = GetIdentificacao(numero, modelo, versao),
                emit = GetEmitente(),
                dest = GetDestinatario(versao),
                transp = GetTransporte()
            };
            if (infNFe.ide.mod == ModeloDocumento.NFe & versao == VersaoServico.ve310)
            {
                infNFe.cobr = GetCobranca(); //V3.00 Somente
            }
            if (infNFe.ide.mod == ModeloDocumento.NFCe)
                infNFe.pag = GetPagamento(); //NFCe Somente               

            for (var i = 0; i < 1; i++)
            {
                infNFe.det.Add(GetDetalhe(i, infNFe.emit.CRT));
            }
            infNFe.total = GetTotal(versao, infNFe.det);

            return infNFe;
        }

        protected virtual ide GetIdentificacao(int numero, ModeloDocumento modelo, VersaoServico versao)
        {
            var ide = new ide
            {
                cUF = Estado.DF,
                natOp = "VENDA",
                indPag = IndicadorPagamento.ipVista,
                mod = modelo,
                serie = 1,
                nNF = numero,
                tpNF = TipoNFe.tnSaida,
                cMunFG = 5300108,
                tpEmis = _configuracoes.CfgServico.tpEmis,
                //tpImp = TipoImpressao.tiRetrato,
                cNF = numero.ToString(),
                tpAmb = _configuracoes.CfgServico.tpAmb,
                finNFe = FinalidadeNFe.fnNormal,
                verProc = "3.000"
            };

            if (ide.tpEmis != TipoEmissao.teNormal)
            {
                ide.dhCont = DateTime.Now.ToString(versao == VersaoServico.ve310 ? "yyyy-MM-ddTHH:mm:sszzz" : "yyyy-MM-ddTHH:mm:ss");
                ide.xJust = "TESTE DE CONTIGÊNCIA PARA NFe/NFCe";
            }

            #region V2.00

            if (versao == VersaoServico.ve200)
            {
                ide.dEmi = DateTime.Today.ToString("yyyy-MM-dd"); //Mude aqui para enviar a nfe vinculada ao EPEC, V2.00
                ide.dSaiEnt = DateTime.Today.ToString("yyyy-MM-dd");
            }

            #endregion

            #region V3.00

            if (versao != VersaoServico.ve310) return ide;
            ide.idDest = DestinoOperacao.doInterna;
            ide.dhEmi = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"); //Mude aqui para enviar a nfe vinculada ao EPEC, V3.10
            if (ide.mod == ModeloDocumento.NFe)
                ide.dhSaiEnt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz");
            else
                ide.tpImp = TipoImpressao.tiNFCe;
            ide.procEmi = ProcessoEmissao.peAplicativoContribuinte;
            ide.indFinal = ConsumidorFinal.cfConsumidorFinal; //NFCe: Tem que ser consumidor Final
            ide.indPres = PresencaComprador.pcPresencial; //NFCe: deve ser 1 ou 4

            #endregion

            return ide;
        }

        protected virtual emit GetEmitente()
        {
            var emit = _configuracoes.Emitente; // new emit
            //{
            //    //CPF = "80365027553",
            //    CNPJ = "32876302000114",
            //    xNome = "FIOLUX COMERCIAL LTDA",
            //    xFant = "FIOLUX COMERCIAL LTDA",
            //    IE = "270844821",
            //};
            emit.enderEmit = GetEnderecoEmitente();            
            return emit;
        }

        protected virtual enderEmit GetEnderecoEmitente()
        {
            var enderEmit = _configuracoes.EnderecoEmitente; // new enderEmit
            //{
            //    xLgr = "RUA COMENDADOR FRANCISCO JOSE DA CUNHA",
            //    nro = "171",
            //    xCpl = "1 ANDAR",
            //    xBairro = "CENTRO",
            //    cMun = 2802908,
            //    xMun = "ITABAIANA",
            //    UF = "SE",
            //    CEP = 49500000,
            //    fone = 7934313234
            //};
            enderEmit.cPais = 1058;
            enderEmit.xPais = "BRASIL";
            return enderEmit;
        }

        protected virtual dest GetDestinatario(VersaoServico versao)
        {
            var dest = new dest(versao)
            {
                //nfe CNPJ = "06185129000106",
                //nfe IE = "0745379400183",
                CPF = "61363944304", //nfce
                xNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL",
                enderDest = GetEnderecoDestinatario()
            };

            //if (versao == VersaoServico.ve200)
            //    dest.IE = "ISENTO";
            if (versao != VersaoServico.ve310) return dest;
            dest.indIEDest = indIEDest.NaoContribuinte; //NFCe: Tem que ser não contribuinte V3.00 Somente
            dest.email = "teste@gmail.com"; //V3.00 Somente
            return dest;
        }

        protected virtual enderDest GetEnderecoDestinatario()
        {
            var enderDest = new enderDest
            {
                xLgr = "RUA ...",
                nro = "S/N",
                xBairro = "CENTRO",
                cMun = 5300108,
                xMun = "brasilia",
                UF = "DF",
                CEP = "71925000",
                cPais = 1058,
                xPais = "BRASIL"
            };
            return enderDest;
        }

        protected virtual det GetDetalhe(int i, CRT crt)
        {
            var det = new det
            {
                nItem = i + 1,
                prod = GetProduto(i + 1),
                imposto = new imposto
                {
                    vTotTrib = decimal.Parse("0,17"),
                    ICMS = new ICMS
                    {
                        TipoICMS = crt == CRT.SimplesNacional ? InformarCSOSN(Csosnicms.Csosn102) : InformarICMS(Csticms.Cst00, VersaoServico.ve310)
                    }
                    //nfe ,
                    //nfe COFINS = new COFINS {TipoCOFINS = new COFINSOutr {CST = CSTCOFINS.cofins99, pCOFINS = 0, vBC = 0, vCOFINS = 0}},
                    //nfePIS = new PIS {TipoPIS = new PISOutr {CST = CSTPIS.pis99, pPIS = 0, vBC = 0, vPIS = 0}},
                    //nfeIPI = new IPI() { cEnq = "999", TipoIPI = new IPITrib() { CST = CSTIPI.ipi00, pIPI = 5, vBC = 1, vIPI = Decimal.Parse("0,05")} }
                }
            };

            //det.impostoDevol = new impostoDevol() { IPI = new IPIDevolvido() { vIPIDevol = decimal.Parse("10") }, pDevol = decimal.Parse("100") };

            return det;
        }

        protected virtual prod GetProduto(int i)
        {
            var p = new prod
            {
                cProd = i.ToString().PadLeft(5, '0'),
                cEAN = "7770000000012",
                xProd = "ABRACADEIRA NYLON 6.6 BRANCA 91X92 " + i,
                NCM = "73269000",
                CFOP = 5102,
                uCom = "UNID",
                qCom = decimal.Parse("1,000"),
                vUnCom = decimal.Parse("1,00"),
                vProd = decimal.Parse("1,00"),
                vDesc = decimal.Parse("0,10"),
                cEANTrib = "7770000000012",
                uTrib = "UNID",
                qTrib = decimal.Parse("1,000"),
                vUnTrib = decimal.Parse("1,00"),
                indTot = IndicadorTotal.ValorDoItemCompoeTotalNF,
                
                //ProdutoEspecifico = new arma
                //{
                //    tpArma = TipoArma.UsoPermitido,
                //    nSerie = "123456",
                //    nCano = "123456",
                //    descr = "TESTE DE ARMA"
                //}
            };
            return p;
        }

        protected virtual ICMSBasico InformarICMS(Csticms CST, VersaoServico versao)
        {
            var icms20 = new ICMS20
            {
                orig = OrigemMercadoria.OmNacional,
                CST = Csticms.Cst20,
                modBC = DeterminacaoBaseIcms.DbiValorOperacao,
                vBC = 1,
                pICMS = 17,
                vICMS = decimal.Parse("0,17"),
                motDesICMS = MotivoDesoneracaoIcms.MdiTaxi
            };
            if (versao == VersaoServico.ve310)
                icms20.vICMSDeson = decimal.Parse("0,10"); //V3.00 ou maior Somente

            switch (CST)
            {
                case Csticms.Cst00:
                    return new ICMS00
                    {
                        CST = Csticms.Cst00,
                        modBC = DeterminacaoBaseIcms.DbiValorOperacao,
                        orig = OrigemMercadoria.OmNacional,
                        pICMS = 17,
                        vBC = 1,
                        vICMS = decimal.Parse("0,17")
                    };
                case Csticms.Cst20:
                    return icms20;
                //Outros casos aqui
            }

            return new ICMS10();
        }

        protected virtual ICMSBasico InformarCSOSN(Csosnicms CST)
        {
            switch (CST)
            {
                case Csosnicms.Csosn101:
                    return new ICMSSN101
                    {
                        CSOSN = Csosnicms.Csosn101,
                        orig = OrigemMercadoria.OmNacional
                    };
                case Csosnicms.Csosn102:
                    return new ICMSSN102
                    {
                        CSOSN = Csosnicms.Csosn102,
                        orig = OrigemMercadoria.OmNacional
                    };
                    //Outros casos aqui
                default:
                    return new ICMSSN201();
            }
        }

        protected virtual total GetTotal(VersaoServico versao, List<det> produtos)
        {
            var icmsTot = new ICMSTot
            {
                vProd = produtos.Sum(p => p.prod.vProd),
                vNF = produtos.Sum(p => p.prod.vProd) - produtos.Sum(p => p.prod.vDesc ?? 0),
                vDesc = produtos.Sum(p => p.prod.vDesc ?? 0),
                vTotTrib = produtos.Sum(p => p.imposto.vTotTrib ?? 0),
            };
            if (versao == VersaoServico.ve310)
                icmsTot.vICMSDeson = 0;

            foreach (var produto in produtos)
            {
                //nfe if (produto.imposto.IPI.TipoIPI.GetType() == typeof(IPITrib))
                //nfe     icmsTot.vIPI = icmsTot.vIPI + ((IPITrib)produto.imposto.IPI.TipoIPI).vIPI ?? 0;
                if (produto.imposto.ICMS.TipoICMS.GetType() == typeof (ICMS00))
                {
                    icmsTot.vBC = icmsTot.vBC + ((ICMS00)produto.imposto.ICMS.TipoICMS).vBC;
                    icmsTot.vICMS = icmsTot.vICMS + ((ICMS00)produto.imposto.ICMS.TipoICMS).vICMS;  
                }
                if (produto.imposto.ICMS.TipoICMS.GetType() == typeof(ICMS20))
                {
                    icmsTot.vBC = icmsTot.vBC + ((ICMS20)produto.imposto.ICMS.TipoICMS).vBC;
                    icmsTot.vICMS = icmsTot.vICMS + ((ICMS20)produto.imposto.ICMS.TipoICMS).vICMS;
                }
                //Outros Ifs aqui, caso vá usar as classes ICMS00, ICMS10 para totalizar
            }

            var t = new total {ICMSTot = icmsTot};
            return t;
        }



        protected virtual transp GetTransporte()
        {
            //var volumes = new List<vol> {GetVolume(), GetVolume()};

            var t = new transp
            {
                modFrete = ModalidadeFrete.mfSemFrete //NFCe: Não pode ter frete
                //vol = volumes 
            };

            return t;
        }

        protected virtual vol GetVolume()
        {
            var v = new vol
            {
                esp = "teste de especia",
                lacres = new List<lacres> {new lacres {nLacre = "123456"}}
            };

            return v;
        }

        protected virtual cobr GetCobranca()
        {
            var c = new cobr
            {
                fat = new fat {nFat = "12345678910", vLiq = 90},
                dup = new List<dup>
                {
                    new dup {nDup = "12345678", vDup = decimal.Parse("0,45")},
                    new dup {nDup = "987654321", vDup = decimal.Parse("0,45")}
                }
            };

            return c;
        }

        protected virtual List<pag> GetPagamento()
        {
            var p = new List<pag>
            {
                new pag {tPag = FormaPagamento.fpDinheiro, vPag = decimal.Parse("0,45")},
                new pag {tPag = FormaPagamento.fpCheque, vPag = decimal.Parse("0,45")}
            };
            return p;
        }

        #endregion

        #region Tratamento de retornos dos Serviços

        internal void RetornoDados<T>(T objeto, RichTextBox richTextBox) where T : class
        {
            richTextBox.Document.Blocks.Clear();

            foreach (var atributos in Funcoes.LerPropriedades(objeto))
            {
                richTextBox.AppendText(atributos.Key + " = " + atributos.Value + "\r");
            }
        }

        internal void RetornoCompletoStr(RichTextBox richTextBox, string retornoCompletoStr)
        {
            richTextBox.Document.Blocks.Clear();
            richTextBox.AppendText(retornoCompletoStr);
        }

        internal void EnvioStr(RichTextBox richTextBox, string envioStr)
        {
            richTextBox.Document.Blocks.Clear();
            richTextBox.AppendText(envioStr);
        }

        internal void RetornoXml(WebBrowser webBrowser, string retornoXmlString)
        {
            var stw = new StreamWriter(_path + @"\tmp.xml");
            stw.WriteLine(retornoXmlString);
            stw.Close();
            webBrowser.Navigate(_path + @"\tmp.xml");
        }

        internal void RetornoStr(RichTextBox richTextBox, string retornoXmlString)
        {
            richTextBox.Document.Blocks.Clear();
            richTextBox.AppendText(retornoXmlString);
        }

        #endregion
    }
}
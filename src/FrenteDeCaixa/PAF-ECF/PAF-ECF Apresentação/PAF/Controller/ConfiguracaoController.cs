/********************************************************************************
Title: T2TiPDV
Description: Classe de controle da configuração

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
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using PafEcf.VO;
using PafEcf.Util;
using PafEcf.Infra;

namespace PafEcf.Controller
{

    public class ConfiguracaoController
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public ConfiguracaoController()
        {
            conexao = dbConnection.conectar();
        }

        public List<PosicaoComponentesVO> VerificaPosicaoTamanho()
        {
            List<PosicaoComponentesVO> listaPosicoes = new List<PosicaoComponentesVO>();
            ConsultaSQL =
                    " select "
                    + "P.ID,"
                    + "P.NOME, "
                    + "P.ALTURA, "
                    + "P.LARGURA, "
                    + "P.TOPO, "
                    + "P.ESQUERDA, "
                    + "P.TAMANHO_FONTE, "
                    + "P.TEXTO, "
                    + "C.ID_ECF_RESOLUCAO "
                    + " from "
                    + "ECF_POSICAO_COMPONENTES P, ECF_CONFIGURACAO C "
                    + " where "
                    + "P.ID_ECF_RESOLUCAO=C.ID_ECF_RESOLUCAO";
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    PosicaoComponentesVO PosicaoComponentes = new PosicaoComponentesVO();

                    PosicaoComponentes.Id = Convert.ToInt32(leitor["ID"]);
                    PosicaoComponentes.NomeComponente = leitor["NOME"].ToString();
                    PosicaoComponentes.Altura = Convert.ToInt32(leitor["ALTURA"]);
                    PosicaoComponentes.Largura = Convert.ToInt32(leitor["LARGURA"]);
                    PosicaoComponentes.Topo = Convert.ToInt32(leitor["TOPO"]);
                    PosicaoComponentes.Esquerda = Convert.ToInt32(leitor["ESQUERDA"]);
                    PosicaoComponentes.TamanhoFonte = Convert.ToInt32(leitor["TAMANHO_FONTE"]);
                    PosicaoComponentes.TextoComponente = leitor["TEXTO"].ToString();

                    listaPosicoes.Add(PosicaoComponentes);
                }
                return listaPosicoes;
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
                return null;
            }
            finally
            {
                if (leitor != null)
                    leitor.Close();
            }
        }


        public ConfiguracaoVO PegaConfiguracao()
        {
            ConfiguracaoVO Configuracao = new ConfiguracaoVO();
            ImpressoraVO Impressora = new ImpressoraVO();
            Configuracao.ImpressoraVO = Impressora;
            ResolucaoVO Resolucao = new ResolucaoVO();
            Configuracao.ResolucaoVO = Resolucao;
            string[] Linha;

            ConsultaSQL = "select " +
                        " C.ID_ECF_IMPRESSORA, " +
                        " C.ID_ECF_RESOLUCAO, " +
                        " C.ID_ECF_CAIXA, " +
                        " C.ID_ECF_EMPRESA, " +
                        " C.MENSAGEM_CUPOM, " +
                        " C.PORTA_ECF, " +
                        " C.IP_SERVIDOR, " +
                        " C.IP_SITEF, " +
                        " C.TIPO_TEF, " +
                        " C.TITULO_TELA_CAIXA, " +
                        " C.CAMINHO_IMAGENS_PRODUTOS, " +
                        " C.CAMINHO_IMAGENS_MARKETING, " +
                        " C.CAMINHO_IMAGENS_LAYOUT, " +
                        " C.COR_JANELAS_INTERNAS, " +
                        " C.MARKETING_ATIVO, " +
                        " C.CFOP_ECF, " +
                        " C.CFOP_NF2, " +
                        " C.TIMEOUT_ECF, " +
                        " C.INTERVALO_ECF, " +
                        " C.DESCRICAO_SUPRIMENTO, " +
                        " C.DESCRICAO_SANGRIA, " +
                        " C.TEF_TIPO_GP, " +
                        " C.TEF_TEMPO_ESPERA, " +
                        " C.TEF_ESPERA_STS, " +
                        " C.TEF_NUMERO_VIAS, " +
                        " C.DECIMAIS_QUANTIDADE, " +
                        " C.DECIMAIS_VALOR, " +
                        " C.BITS_POR_SEGUNDO, " +
                        " C.QTDE_MAXIMA_CARTOES, " +
                        " C.PESQUISA_PARTE, " +
                        " C.CONFIGURACAO_BALANCA, " +
                        " C.PARAMETROS_DIVERSOS, " +
                        " C.ULTIMA_EXCLUSAO, " +
                        " C.LAUDO, " +
                        " C.INDICE_GERENCIAL, " +
                        " C.DATA_ATUALIZACAO_ESTOQUE, " +
                        " C.SINCRONIZADO, " +
                        " R.RESOLUCAO_TELA, " +
                        " R.LARGURA, " +
                        " R.ALTURA, " +
                        " R.IMAGEM_TELA, " +
                        " R.IMAGEM_MENU, " +
                        " R.IMAGEM_SUBMENU, " +
                        " R.HOTTRACK_COLOR, " +
                        " R.ITEM_STYLE_FONT_NAME, " +
                        " R.ITEM_STYLE_FONT_COLOR, " +
                        " R.ITEM_SEL_STYLE_COLOR, " +
                        " R.LABEL_TOTAL_GERAL_FONT_COLOR, " +
                        " R.ITEM_STYLE_FONT_STYLE," +
                        " R.EDITS_COLOR, " +
                        " R.EDITS_FONT_COLOR, " +
                        " R.EDITS_DISABLED_COLOR, " +
                        " R.EDITS_FONT_NAME, " +
                        " R.EDITS_FONT_STYLE, " +
                        " (select nome from ECF_CAIXA where ECF_CAIXA.id=c.ID_ECF_CAIXA) AS NOME_CAIXA," +
                        " I.MODELO_ACBR, " +
                        " I.SERIE " +
                        "from " +
                        " ECF_RESOLUCAO R, ECF_CONFIGURACAO C, ECF_IMPRESSORA I " +
                        "where " +
                        " C.ID_ECF_RESOLUCAO=R.ID and C.ID_ECF_IMPRESSORA=I.ID";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                if (leitor.Read())
                {

                    Configuracao.IdImpressora = Convert.ToInt32(leitor["ID_ECF_IMPRESSORA"]);
                    Configuracao.IdCaixa = Convert.ToInt32(leitor["ID_ECF_CAIXA"]);
                    Configuracao.IdEmpresa = Convert.ToInt32(leitor["ID_ECF_EMPRESA"]);
                    Configuracao.MensagemCupom = Convert.ToString(leitor["MENSAGEM_CUPOM"]);
                    Configuracao.PortaECF = Convert.ToString(leitor["PORTA_ECF"]);
                    Configuracao.IpServidor = Convert.ToString(leitor["IP_SERVIDOR"]);
                    Configuracao.IpSitef = Convert.ToString(leitor["IP_SITEF"]);
                    Configuracao.TipoTEF = Convert.ToString(leitor["TIPO_TEF"]);
                    Configuracao.TituloTelaCaixa = Convert.ToString(leitor["TITULO_TELA_CAIXA"]);
                    Configuracao.CaminhoImagensProdutos = Convert.ToString(leitor["CAMINHO_IMAGENS_PRODUTOS"]);
                    Configuracao.CaminhoImagensMarketing = Convert.ToString(leitor["CAMINHO_IMAGENS_MARKETING"]);
                    Configuracao.CaminhoImagensLayout = Convert.ToString(leitor["CAMINHO_IMAGENS_LAYOUT"]);
                    Configuracao.CorJanelasInternas = Convert.ToString(leitor["COR_JANELAS_INTERNAS"]);
                    Configuracao.MarketingAtivo = Convert.ToString(leitor["MARKETING_ATIVO"]);
                    Configuracao.CFOPECF = Convert.ToInt32(leitor["CFOP_ECF"]);
                    Configuracao.CFOPNF2 = Convert.ToInt32(leitor["CFOP_NF2"]);
                    Configuracao.TimeOutECF = Convert.ToInt32(leitor["TIMEOUT_ECF"]);
                    Configuracao.IntervaloECF = Convert.ToInt32(leitor["INTERVALO_ECF"]);
                    Configuracao.DescricaoSuprimento = Convert.ToString(leitor["DESCRICAO_SUPRIMENTO"]);
                    Configuracao.DescricaoSangria = Convert.ToString(leitor["DESCRICAO_SANGRIA"]);
                    Configuracao.TEFTipoGP = Convert.ToInt32(leitor["TEF_TIPO_GP"]);
                    Configuracao.TEFTempoEspera = Convert.ToInt32(leitor["TEF_TEMPO_ESPERA"]);
                    Configuracao.TEFEsperaSTS = Convert.ToInt32(leitor["TEF_ESPERA_STS"]);
                    Configuracao.TEFNumeroVias = Convert.ToInt32(leitor["TEF_NUMERO_VIAS"]);
                    Configuracao.DecimaisQuantidade = Convert.ToInt32(leitor["DECIMAIS_QUANTIDADE"]);
                    Configuracao.DecimaisValor = Convert.ToInt32(leitor["DECIMAIS_VALOR"]);
                    Configuracao.BitsPorSegundo = Convert.ToInt32(leitor["BITS_POR_SEGUNDO"]);
                    Configuracao.QuantidadeMaximaCartoes = Convert.ToInt32(leitor["QTDE_MAXIMA_CARTOES"]);
                    Configuracao.PesquisaParte = Convert.ToString(leitor["PESQUISA_PARTE"]);
                    Configuracao.ConfiguracaoBalanca = Convert.ToString(leitor["CONFIGURACAO_BALANCA"]);
                    Configuracao.ParametrosDiversos = Convert.ToString(leitor["PARAMETROS_DIVERSOS"]);
                    Configuracao.UltimaExclusao = Convert.ToInt32(leitor["ULTIMA_EXCLUSAO"]);
                    Configuracao.Laudo = Convert.ToString(leitor["LAUDO"]);
                    Configuracao.IndiceGerencial = Convert.ToString(leitor["INDICE_GERENCIAL"]);
                    Configuracao.DataAtualizacaoEstoque = (DateTime)(leitor["DATA_ATUALIZACAO_ESTOQUE"]);

                    Configuracao.ResolucaoVO.ResolucaoTela = Convert.ToString(leitor["RESOLUCAO_TELA"]);
                    Configuracao.ResolucaoVO.Largura = Convert.ToInt32(leitor["LARGURA"]);
                    Configuracao.ResolucaoVO.Altura = Convert.ToInt32(leitor["ALTURA"]);
                    Configuracao.ResolucaoVO.ImagemTela = Convert.ToString(leitor["IMAGEM_TELA"]);
                    Configuracao.ResolucaoVO.ImagemMenu = Convert.ToString(leitor["IMAGEM_MENU"]);
                    Configuracao.ResolucaoVO.ImagemSubMenu = Convert.ToString(leitor["IMAGEM_SUBMENU"]);
                    Configuracao.ResolucaoVO.HotTrackColor = Convert.ToString(leitor["HOTTRACK_COLOR"]);
                    Configuracao.ResolucaoVO.ItemStyleFontName = Convert.ToString(leitor["ITEM_STYLE_FONT_NAME"]);
                    Configuracao.ResolucaoVO.ItemStyleFontColor = Convert.ToString(leitor["ITEM_STYLE_FONT_COLOR"]);
                    Configuracao.ResolucaoVO.ItemSelStyleColor = Convert.ToString(leitor["ITEM_SEL_STYLE_COLOR"]);
                    Configuracao.ResolucaoVO.LabelTotalGeralFontColor = Convert.ToString(leitor["LABEL_TOTAL_GERAL_FONT_COLOR"]);
                    Configuracao.ResolucaoVO.ItemStyleFontStyle = Convert.ToString(leitor["ITEM_STYLE_FONT_STYLE"]);
                    Configuracao.ResolucaoVO.EditColor = Convert.ToString(leitor["EDITS_COLOR"]);
                    Configuracao.ResolucaoVO.EditFontColor = Convert.ToString(leitor["EDITS_FONT_COLOR"]);
                    Configuracao.ResolucaoVO.EditDisabledColor = Convert.ToString(leitor["EDITS_DISABLED_COLOR"]);
                    Configuracao.ResolucaoVO.EditFontName = Convert.ToString(leitor["EDITS_FONT_NAME"]);
                    Configuracao.ResolucaoVO.EditFontStyle = Convert.ToString(leitor["EDITS_FONT_STYLE"]);

                    Configuracao.NomeCaixa = Convert.ToString(leitor["NOME_CAIXA"]);
                    Configuracao.ModeloImpressora = Convert.ToString(leitor["MODELO_ACBR"]);
                    Configuracao.NumSerieECF = Convert.ToString(leitor["SERIE"]);

                    // ******* ConfiguracaoBalanca ********************************************
                    Linha = Configuracao.ConfiguracaoBalanca.Split('|');

                    Configuracao.BalancaModelo = Convert.ToInt32(Linha[0]);
                    Configuracao.BalancaIdentificadorBalanca = Linha[1];
                    Configuracao.BalancaHandShaking = Convert.ToInt32(Linha[2]);
                    Configuracao.BalancaParity = Convert.ToInt32(Linha[3]);
                    Configuracao.BalancaStopBits = Convert.ToInt32(Linha[4]);
                    Configuracao.BalancaDataBits = Convert.ToInt32(Linha[5]);
                    Configuracao.BalancaBaudRate = Convert.ToInt32(Linha[6]);
                    Configuracao.BalancaPortaSerial = Linha[7];
                    Configuracao.BalancaTimeOut = Convert.ToInt32(Linha[8]);
                    Configuracao.BalancaTipoConfiguracaoBalanca = Linha[9];
                    // *******  Fim ConfiguracaoBalanca ***************************************

                    // ******* IndiceGerencial ***********************************************
                    Linha = Configuracao.IndiceGerencial.Split('|');

                    Configuracao.GerencialX = Convert.ToInt32(Linha[0]);
                    Configuracao.MeiosDePagamento = Convert.ToInt32(Linha[1]);
                    Configuracao.DavEmitidos = Convert.ToInt32(Linha[2]);
                    Configuracao.IdentificacaoPaf = Convert.ToInt32(Linha[3]);
                    Configuracao.ParametrosDeConfiguracao = Convert.ToInt32(Linha[4]);
                    Configuracao.Relatorio = Convert.ToInt32(Linha[5]);
                    // ******* Fim IndiceGerencial ********************************************

                    // ******* ParametrosDiversos *********************************************
                    Linha = Configuracao.ParametrosDiversos.Split('|');

                    Configuracao.PedeCPFCupom = Convert.ToInt32(Linha[0]);
                    Configuracao.UsaIntegracao = Convert.ToInt32(Linha[1]);
                    Configuracao.TimerIntegracao = Convert.ToInt32(Linha[2]);
                    Configuracao.GavetaDinheiro = Convert.ToInt32(Linha[3]);
                    Configuracao.SinalInvertido = Convert.ToInt32(Linha[4]);
                    Configuracao.QtdeMaximaParcelas = Convert.ToInt32(Linha[5]);
                    Configuracao.ImprimeParcelas = Convert.ToInt32(Linha[6]);
                    Configuracao.TecladoReduzido = Convert.ToInt32(Linha[7]);
                    // Parametros do Leitor Serial
                    Configuracao.UsaLeitorSerial = Convert.ToInt32(Linha[8]);
                    Configuracao.PortaLeitorSerial = Linha[9];
                    Configuracao.BaudLeitorSerial = Linha[10];
                    Configuracao.SufixoLeitorSerial = Linha[11];
                    Configuracao.IntervaloLeitorSerial = Linha[12];
                    Configuracao.ParidadeLeitorSerial = Convert.ToInt32(Linha[13]);
                    Configuracao.HardFlowLeitorSerial = Convert.ToInt32(Linha[14]);
                    Configuracao.SoftFlowLeitorSerial = Convert.ToInt32(Linha[15]);
                    Configuracao.HandShakeLeitorSerial = Convert.ToInt32(Linha[16]);
                    Configuracao.StopLeitorSerial = Convert.ToInt32(Linha[17]);
                    Configuracao.FilaLeitorSerial = Convert.ToInt32(Linha[18]);
                    Configuracao.ExcluiSufixoLeitorSerial = Convert.ToInt32(Linha[19]);
                    // LancamentoNotasManuais
                    Configuracao.LancamentoNotasManuais = Convert.ToInt32(Linha[22]);
                    // ******* Fim ParametrosDiversos *****************************************

                    return Configuracao;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
                return null;
            }
            finally
            {
                if (leitor != null)
                    leitor.Close();
            }
        }


        // TODO:  Pegue como base o BancoController e implemente os métodos para receber os dados da retaguarda - integração
    }

}
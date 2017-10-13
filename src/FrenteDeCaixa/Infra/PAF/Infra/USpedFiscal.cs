/* *******************************************************************************
  Title: T2TiPDV
  Description: Funções e procedimentos do Sped Fiscal;

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
using PafEcf.VO;
using System.Collections.Generic;
using PafEcf.View;
using PafEcf.Controller;
using ACBrFramework.Sped;
using PafEcf.Util;


namespace PafEcf.Infra
{

    //TODO:  Analise com cuidado toda a classe abaixo e implemente o que está faltando para a geração do Sped Fiscal

    public static class USpedFiscal
    {

        public static int VersaoLeiaute, FinalidadeArquivo, PerfilApresentacao;
        public static string DataInicial, DataFinal;

        //  Bloco 0
        public static void GerarBloco0()
        {
            EmpresaVO Empresa = new EmpresaController().PegaEmpresa(FCaixa.Configuracao.IdEmpresa);
            ContadorVO Contador = new ContadorController().PegaContador();
            List<UnidadeProdutoVO> ListaUnidade = new UnidadeController().UnidadeSPED(DataInicial, DataFinal);
            List<ProdutoVO> ListaProduto = new ProdutoController().ConsultaProdutoSPED(DataInicial, DataFinal, PerfilApresentacao);
            List<ClienteVO> ListaCliente = new ClienteController().ConsultaClienteSPED(DataInicial, DataFinal);

            if ((ListaUnidade == null) || (ListaProduto == null))
            {
                MessageBox.Show("Não há venda no período selecionado" + "\r" + "e ou a data informada é inválida", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var Bloco0 = FDataModule.ACBrSpedFiscal.Bloco_0;

            //  Registro0000 - Dados da Empresa
            var Registro0000 = Bloco0.Registro0000;

            switch (VersaoLeiaute)
            {
                case 0:
                    Registro0000.COD_VER = ACBrFramework.Sped.VersaoLeiaute.Versao100; break;
                case 1:
                    Registro0000.COD_VER = ACBrFramework.Sped.VersaoLeiaute.Versao101; break;
                case 2:
                    Registro0000.COD_VER = ACBrFramework.Sped.VersaoLeiaute.Versao102; break;
                case 3:
                    Registro0000.COD_VER = ACBrFramework.Sped.VersaoLeiaute.Versao103; break;
            }

            switch (FinalidadeArquivo)
            {
                case 0:
                    Registro0000.COD_FIN = ACBrFramework.Sped.CodFinalidade.Original; break;
                case 1:
                    Registro0000.COD_FIN = ACBrFramework.Sped.CodFinalidade.Substituto; break;
            }

            switch (PerfilApresentacao)
            {
                case 0:
                    Registro0000.IND_PERFIL = ACBrFramework.Sped.Perfil.PerfilA; break;
                case 1:
                    Registro0000.IND_PERFIL = ACBrFramework.Sped.Perfil.PerfilB; break;
                case 2:
                    Registro0000.IND_PERFIL = ACBrFramework.Sped.Perfil.PerfilC; break;
            }

            Registro0000.NOME = Empresa.RazaoSocial;
            Registro0000.CNPJ = Empresa.Cnpj;
            Registro0000.CPF = "";
            Registro0000.UF = Empresa.Uf;
            Registro0000.IE = Empresa.InscricaoEstadual;
            Registro0000.COD_MUN = Empresa.CodigoIbgeCidade;
            Registro0000.IM = Empresa.InscricaoMunicipal;
            Registro0000.SUFRAMA = Empresa.Suframa;

            //  0 - Industrial ou equiparado a industrial;
            //  1 - Outros.
            Registro0000.IND_ATIV = ACBrFramework.Sped.Atividade.Outros;


            //  Registro0001 
            var Registro0001 = Bloco0.Registro0001;

            //  Indicador de movimento:
            Registro0001.IND_MOV = ACBrFramework.Sped.IndicadorMovimento.ComDados;

            //Registro0005 - FILHO - Dados complementares da Empresa
            var Registro0005 = Registro0001.Registro0005;
            Registro0005.FANTASIA = Empresa.NomeFantasia;
            Registro0005.CEP = Empresa.Cep;
            Registro0005.ENDERECO = Empresa.Logradouro;
            Registro0005.NUM = Empresa.Numero;
            Registro0005.COMPL = Empresa.Complemento;
            Registro0005.BAIRRO = Empresa.Bairro;
            Registro0005.FONE = Empresa.Fone;
            Registro0005.FAX = Empresa.Fax;
            Registro0005.EMAIL = Empresa.Email;

            //Registro0100 -  FILHO - Dados do contador.
            var Registro0100 = Registro0001.Registro0100;
            Registro0100.NOME = Contador.Nome;
            Registro0100.CPF = Contador.CPF;
            Registro0100.CRC = Contador.CRC;
            Registro0100.CNPJ = Contador.CNPJ;
            Registro0100.CEP = Contador.CEP;
            Registro0100.ENDERECO = Contador.Logradouro;
            Registro0100.NUM = Convert.ToString(Contador.Numero);
            Registro0100.COMPL = Contador.Complemento;
            Registro0100.BAIRRO = Contador.Bairro;
            Registro0100.FONE = Contador.Fone;
            Registro0100.FAX = Contador.Fax;
            Registro0100.EMAIL = Contador.Email;
            Registro0100.COD_MUN = Contador.CodigoMunicipio;

            //  Lista de cliente da nfe
            //Registro0150
            if (ListaCliente != null)
            {
                for (int i = 0; i <= ListaCliente.Count - 1; i++)
                {
                    var Registro0150 = new Registro0150();
                    Registro0150.COD_PART = Convert.ToString(ListaCliente[i].Id);
                    Registro0150.NOME = ListaCliente[i].Nome;
                    Registro0150.COD_PAIS = "01058";
                    if (ListaCliente[i].TipoPessoa == "F")
                        Registro0150.CPF = ListaCliente[i].CpfOuCnpj;
                    else
                        Registro0150.CNPJ = ListaCliente[i].CpfOuCnpj;
                    Registro0150.IE = ListaCliente[i].InscricaoEstadual;
                    Registro0150.COD_MUN = ListaCliente[i].CodigoIbgeCidade.Value;
                    Registro0150.SUFRAMA = "";
                    Registro0150.ENDERECO = ListaCliente[i].Logradouro;
                    Registro0150.NUM = ListaCliente[i].Numero;
                    Registro0150.COMPL = ListaCliente[i].Complemento;
                    Registro0150.BAIRRO = ListaCliente[i].Bairro;

                    Registro0001.Registro0150.Add(Registro0150);

                }
            }

            //  FILHO - Identificação das unidades de medida
            //Registro0190
            for (int i = 0; i <= ListaUnidade.Count - 1; i++)
            {
                var Registro0190 = new Registro0190();
                Registro0190.UNID = Convert.ToString(ListaUnidade[i].Id);
                Registro0190.DESCR = ListaUnidade[i].Nome;
                Registro0001.Registro0190.Add(Registro0190);
            }

            //  FILHO - Tabela de Identificação do Item (Produtos e Serviços)
            //Registro0200
            for (int i = 0; i <= ListaProduto.Count - 1; i++)
            {
                var Registro0200 = new Registro0200();
                Registro0200.COD_ITEM = Convert.ToString(ListaProduto[i].Id);
                Registro0200.DESCR_ITEM = ListaProduto[i].Nome;
                Registro0200.COD_BARRA = ListaProduto[i].GTIN;
                Registro0200.COD_ANT_ITEM = "";
                Registro0200.UNID_INV = Convert.ToString(ListaProduto[i].IdUnidade);

                switch (Convert.ToInt32(ListaProduto[i].TipoItemSped))
                {
                    case 0:
                        Registro0200.TIPO_ITEM = TipoItem.MercadoriaRevenda; break;
                    case 1:
                        Registro0200.TIPO_ITEM = TipoItem.MateriaPrima; break;
                    case 2:
                        Registro0200.TIPO_ITEM = TipoItem.Embalagem; break;
                    case 3:
                        Registro0200.TIPO_ITEM = TipoItem.ProdutoProcesso; break;
                    case 4:
                        Registro0200.TIPO_ITEM = TipoItem.ProdutoAcabado; break;
                    case 5:
                        Registro0200.TIPO_ITEM = TipoItem.Subproduto; break;
                    case 6:
                        Registro0200.TIPO_ITEM = TipoItem.ProdutoIntermediario; break;
                    case 7:
                        Registro0200.TIPO_ITEM = TipoItem.MaterialConsumo; break;
                    case 8:
                        Registro0200.TIPO_ITEM = TipoItem.AtivoImobilizado; break;
                    case 9:
                        Registro0200.TIPO_ITEM = TipoItem.Servicos; break;
                    case 10:
                        Registro0200.TIPO_ITEM = TipoItem.OutrosInsumos; break;
                    case 99:
                        Registro0200.TIPO_ITEM = TipoItem.Outras; break;
                }

                Registro0200.COD_NCM = ListaProduto[i].NCM;
                Registro0200.EX_IPI = "";
                Registro0200.COD_GEN = ListaProduto[i].NCM.Substring(2, 1);
                Registro0200.COD_LST = "";
                Registro0200.ALIQ_ICMS = ListaProduto[i].AliquotaICMS;

                Registro0001.Registro0200.Add(Registro0200);
            }
        }


        public static void GerarBlocoC()
        {
            NotaFiscalController NotaFiscalController = new NotaFiscalController();
            SpedFiscalController SpedFiscalController = new SpedFiscalController();
            RegistroRController RegistroRController = new RegistroRController();

            var BlocoC = FDataModule.ACBrSpedFiscal.Bloco_C;

            var RegistroC001 = BlocoC.RegistroC001;
            RegistroC001.IND_MOV = IndicadorMovimento.ComDados;

            List<NotaFiscalCabecalhoVO> ListaNF2Cabecalho = NotaFiscalController.ConsultaNFCabecalhoSPED(DataInicial, DataFinal);
            List<NotaFiscalCabecalhoVO> ListaNF2CabecalhoCanceladas = NotaFiscalController.ConsultaNFCabecalhoCanceladasSPED(DataInicial, DataFinal);

            List<SpedFiscalC370VO> ListaC370;
            List<SpedFiscalC390VO> ListaC390;
            List<SpedFiscalC321VO> ListaC321;
            List<SpedFiscalC425VO> ListaC425;
            List<SpedFiscalC490VO> ListaC490;

            List<R02VO> ListaR02;
            List<R03VO> ListaR03;
            List<R04VO> ListaR04;
            List<R05VO> ListaR05;

            //TODO:  Implemente a busca por NF-e na sua retaguarda
            List<Object> ListaNFeCabecalho = new List<Object>();
            List<Object> ListaCupomNFe;
            List<Object> ListaNFeAnalitico;

            // / ///////////
            //  Perfil A  //
            // / ///////////
            if (PerfilApresentacao == 0)
            {
                if (ListaNFeCabecalho != null)
                {
                    for (int i = 0; i <= ListaNFeCabecalho.Count - 1; i++)
                    {
                        //  c100
                        //TODO:  Implemente a busca por NF-e na sua retaguarda
                        var RegistroC100 = new RegistroC100();
                        RegistroC100.IND_OPER = TipoOperacao.SaidaPrestacao;
                        RegistroC100.IND_EMIT = Emitente.EmissaoPropria;
                        RegistroC100.COD_PART = "1";
                        RegistroC100.COD_MOD = "1";
                        RegistroC100.COD_SIT = SituacaoDocto.Regular;
                        RegistroC100.SER = "1";
                        RegistroC100.NUM_DOC = "1";
                        RegistroC100.CHV_NFE = "1";
                        RegistroC100.DT_DOC = DateTime.Now;
                        RegistroC100.DT_E_S = DateTime.Now;
                        RegistroC100.VL_DOC = 1;
                        RegistroC100.IND_PGTO = TipoPagamento.SemPagamento;
                        RegistroC100.VL_DESC = 0;
                        RegistroC100.VL_ABAT_NT = 0;
                        RegistroC100.VL_MERC = 1;
                        RegistroC100.IND_FRT = TipoFrete.SemCobrancaFrete;
                        RegistroC100.VL_FRT = 0;
                        RegistroC100.VL_SEG = 0;
                        RegistroC100.VL_OUT_DA = 0;
                        RegistroC100.VL_BC_ICMS = 1;
                        RegistroC100.VL_ICMS = 0;
                        RegistroC100.VL_BC_ICMS_ST = 0;
                        RegistroC100.VL_ICMS_ST = 0;
                        RegistroC100.VL_IPI = 0;
                        RegistroC100.VL_PIS = 0;
                        RegistroC100.VL_COFINS = 0;
                        RegistroC100.VL_PIS_ST = 0;
                        RegistroC100.VL_COFINS_ST = 0;
                        RegistroC001.RegistroC100.Add(RegistroC100);

                        //  C114
                        // TODO  Implemente a busca por NF-e na sua retaguarda
                        ListaCupomNFe = new List<Object>();

                        if (ListaCupomNFe != null)
                        {
                            for (int j = 0; j <= ListaCupomNFe.Count - 1; j++)
                            {
                                var RegistroC114 = new RegistroC114();
                                RegistroC114.COD_MOD = "1";
                                RegistroC114.ECF_FAB = "1";
                                RegistroC114.ECF_CX = "1";
                                RegistroC114.NUM_DOC = "1";
                                RegistroC114.DT_DOC = DateTime.Now;
                                //RegistroC001.RegistroC100[i].RegistroC110[i].RegistroC114.Add(RegistroC114);
                            }
                        }

                        //  C190
                        // TODO  Implemente a busca por NF-e na sua retaguarda
                        ListaCupomNFe = new List<Object>();
                        ListaNFeAnalitico = new List<Object>();

                        if (ListaNFeAnalitico != null)
                        {
                            for (int j = 0; j <= ListaNFeAnalitico.Count - 1; j++)
                            {
                                var RegistroC190 = new RegistroC190();
                                RegistroC190.CST_ICMS = "1";
                                RegistroC190.CFOP = "1";
                                RegistroC190.ALIQ_ICMS = 0;
                                RegistroC190.VL_OPR = 1;
                                RegistroC190.VL_BC_ICMS = 0;
                                RegistroC190.VL_ICMS = 0;
                                RegistroC190.VL_BC_ICMS_ST = 0;
                                RegistroC190.VL_ICMS_ST = 0;
                                RegistroC190.VL_RED_BC = 0;
                                RegistroC190.VL_IPI = 0;
                                RegistroC190.COD_OBS = "";
                                RegistroC100.RegistroC190.Add(RegistroC190);
                            }
                        }

                    }
                }

                if (ListaNF2Cabecalho != null)
                {
                    for (int i = 0; i <= ListaNF2Cabecalho.Count - 1; i++)
                    {
                        var RegistroC350 = new RegistroC350();
                        RegistroC350.SER = ListaNF2Cabecalho[i].Serie;
                        RegistroC350.SUB_SER = ListaNF2Cabecalho[i].Subserie;
                        RegistroC350.NUM_DOC = ListaNF2Cabecalho[i].Numero;
                        RegistroC350.DT_DOC = Convert.ToDateTime(ListaNF2Cabecalho[i].DataEmissao);
                        RegistroC350.CNPJ_CPF = ListaNF2Cabecalho[i].CpfCnpjCliente;
                        RegistroC350.VL_MERC = ListaNF2Cabecalho[i].TotalProdutos;
                        RegistroC350.VL_DOC = ListaNF2Cabecalho[i].TotalNf;
                        RegistroC350.VL_DESC = ListaNF2Cabecalho[i].Desconto;
                        RegistroC350.VL_PIS = ListaNF2Cabecalho[i].Pis;
                        RegistroC350.VL_COFINS = ListaNF2Cabecalho[i].Cofins;
                        RegistroC350.COD_CTA = "";
                        RegistroC001.RegistroC350.Add(RegistroC350);

                        //  C370
                        ListaC370 = SpedFiscalController.TabelaC370(ListaNF2Cabecalho[i].Id);
                        if (ListaC370 != null)
                        {
                            for (int j = 0; j <= ListaC370.Count - 1; j++)
                            {
                                var RegistroC370 = new RegistroC370();
                                RegistroC370.NUM_ITEM = Convert.ToString(ListaC370[j].Item);
                                RegistroC370.COD_ITEM = Convert.ToString(ListaC370[j].IdProduto);
                                RegistroC370.QTD = ListaC370[j].Quantidade;
                                RegistroC370.UNID = Convert.ToString(ListaC370[j].IdUnidade);
                                RegistroC370.VL_ITEM = ListaC370[j].Valor;
                                RegistroC370.VL_DESC = ListaC370[j].Desconto;
                                RegistroC001.RegistroC350[i].RegistroC370.Add(RegistroC370);
                            }
                        }


                        //  C390
                        ListaC390 = SpedFiscalController.TabelaC390(ListaNF2Cabecalho[i].Id);
                        if (ListaC390 != null)
                        {
                            for (int l = 0; l <= ListaC390.Count - 1; l++)
                            {
                                var RegistroC390 = new RegistroC390();
                                RegistroC390.CST_ICMS = ListaC390[l].CST;
                                RegistroC390.CFOP = Convert.ToString(ListaC390[l].CFOP);
                                RegistroC390.ALIQ_ICMS = ListaC390[l].TaxaICMS;
                                RegistroC390.VL_OPR = ListaC390[l].SomaValor;
                                RegistroC390.VL_BC_ICMS = ListaC390[l].SomaBaseICMS;
                                RegistroC390.VL_ICMS = ListaC390[l].SomaICMS;
                                RegistroC390.VL_RED_BC = ListaC390[l].SomaICMSOutras;
                                RegistroC001.RegistroC350[i].RegistroC390.Add(RegistroC390);
                            }
                        }

                    }
                }
            }

            // / ///////////
            //  Perfil B  //
            // / ///////////
            if (PerfilApresentacao == 1)
            {
                if (ListaNF2Cabecalho != null)
                {
                    for (int i = 0; i <= ListaNF2Cabecalho.Count - 1; i++)
                    {
                        //  C300
                        var RegistroC300 = new RegistroC300();
                        RegistroC300.COD_MOD = "02";
                        RegistroC300.SER = ListaNF2Cabecalho[i].Serie;
                        RegistroC300.SUB = ListaNF2Cabecalho[i].Subserie;
                        RegistroC300.DT_DOC = Convert.ToDateTime(ListaNF2Cabecalho[i].DataEmissao);
                        RegistroC300.VL_DOC = ListaNF2Cabecalho[i].TotalNf;
                        RegistroC300.VL_PIS = ListaNF2Cabecalho[i].Pis;
                        RegistroC300.VL_COFINS = ListaNF2Cabecalho[i].Cofins;
                        RegistroC300.COD_CTA = "";
                        RegistroC001.RegistroC300.Add(RegistroC300);
                    }
                }

                if (ListaNF2CabecalhoCanceladas != null)
                {
                    for (int i = 0; i <= ListaNF2CabecalhoCanceladas.Count - 1; i++)
                    {
                        //  C310
                        var RegistroC310 = new RegistroC310();
                        RegistroC310.NUM_DOC_CANC = ListaNF2CabecalhoCanceladas[i].Numero;
                        RegistroC001.RegistroC300[i].RegistroC310.Add(RegistroC310);
                    }
                }

                //  C320 ---> igual ao C390
                ListaC390 = SpedFiscalController.TabelaC390(ListaNF2Cabecalho[0].Id);
                if (ListaC390 != null)
                {
                    for (int l = 0; l <= ListaC390.Count - 1; l++)
                    {
                        var RegistroC320 = new RegistroC320();
                        RegistroC320.CST_ICMS = ListaC390[l].CST;
                        RegistroC320.CFOP = Convert.ToString(ListaC390[l].CFOP);
                        RegistroC320.ALIQ_ICMS = ListaC390[l].TaxaICMS;
                        RegistroC320.VL_OPR = ListaC390[l].SomaValor;
                        RegistroC320.VL_BC_ICMS = ListaC390[l].SomaBaseICMS;
                        RegistroC320.VL_ICMS = ListaC390[l].SomaICMS;
                        RegistroC320.VL_RED_BC = ListaC390[l].SomaICMSOutras;
                        RegistroC001.RegistroC300[0].RegistroC320.Add(RegistroC320);
                    }
                }

                //  C321
                ListaC321 = SpedFiscalController.TabelaC321(DataInicial, DataFinal);
                if (ListaC321 != null)
                {
                    for (int i = 0; i <= ListaC321.Count - 1; i++)
                    {
                        var RegistroC321 = new RegistroC321();

                        RegistroC321.COD_ITEM = Convert.ToString(ListaC321[i].IdProduto);
                        RegistroC321.QTD = ListaC321[i].SomaQuantidade;
                        RegistroC321.UNID = ListaC321[i].DescricaoUnidade;
                        RegistroC321.VL_ITEM = ListaC321[i].SomaValor;
                        RegistroC321.VL_DESC = ListaC321[i].SomaDesconto;
                        RegistroC321.VL_BC_ICMS = ListaC321[i].SomaBaseICMS;
                        RegistroC321.VL_ICMS = ListaC321[i].SomaICMS;
                        RegistroC321.VL_PIS = ListaC321[i].SomaPIS;
                        RegistroC321.VL_COFINS = ListaC321[i].SomaCOFINS;
                        RegistroC001.RegistroC300[0].RegistroC320[0].RegistroC321.Add(RegistroC321);
                    }
                }

            } //  if PerfilApresentacao = 1 then

            // / //////////////////
            //  Ambos os Perfis  //
            // / //////////////////
            List<ImpressoraVO> ListaImpressora = new ImpressoraController().TabelaImpressora();
            if (ListaImpressora != null)
            {
                for (int i = 0; i <= ListaImpressora.Count - 1; i++)
                {
                    //  verifica se existe movimento no periodo para aquele ECF
                    ListaR02 = RegistroRController.TabelaR02(DataInicial, DataFinal, ListaImpressora[i].Id);
                    if (ListaR02 != null)
                    {
                        var RegistroC400 = new RegistroC400();
                        RegistroC400.COD_MOD = ListaImpressora[i].ModeloDocumentoFiscal;
                        RegistroC400.ECF_MOD = ListaImpressora[i].Modelo;
                        RegistroC400.ECF_FAB = ListaImpressora[i].Serie;
                        RegistroC400.ECF_CX = Convert.ToString(ListaImpressora[i].Numero);
                        RegistroC001.RegistroC400.Add(RegistroC400);

                        //  C405
                        for (int j = 0; j <= ListaR02.Count - 1; j++)
                        {
                            var RegistroC405 = new RegistroC405();
                            RegistroC405.DT_DOC = Convert.ToDateTime(ListaR02[j].DataMovimento);
                            RegistroC405.CRO = ListaR02[j].CRO;
                            RegistroC405.CRZ = ListaR02[j].CRZ;
                            RegistroC405.NUM_COO_FIN = ListaR02[j].COO;
                            RegistroC405.GT_FIN = ListaR02[j].GrandeTotal;
                            RegistroC405.VL_BRT = ListaR02[j].VendaBruta;
                            RegistroC400.RegistroC405.Add(RegistroC405);

                            //  C420
                            ListaR03 = RegistroRController.TabelaR03(ListaR02[j].Id);
                            if (ListaR03 != null)
                            {
                                for (int k = 0; k <= ListaR03.Count - 1; k++)
                                {
                                    var RegistroC420 = new RegistroC420();
                                    if (ListaR03[k].TotalizadorParcial.Length == 8)
                                        RegistroC420.COD_TOT_PAR = ListaR03[k].TotalizadorParcial.Substring(ListaR03[k].TotalizadorParcial.Length, 2);
                                    else
                                        RegistroC420.COD_TOT_PAR = ListaR03[k].TotalizadorParcial;
                                    RegistroC420.VLR_ACUM_TOT = ListaR03[k].ValorAcumulado;
                                    if (RegistroC420.COD_TOT_PAR.Trim().Length == 7)
                                        RegistroC420.NR_TOT = Convert.ToInt32(RegistroC420.COD_TOT_PAR.Substring(2, 1));
                                    else
                                        RegistroC420.NR_TOT = 0;
                                    RegistroC405.RegistroC420.Add(RegistroC420);


                                    if (PerfilApresentacao == 1)
                                    {
                                        //  C425
                                        ListaC425 = SpedFiscalController.TabelaC425(ListaR02[j].DataMovimento.ToString("yyyy-mm-dd"), ListaR02[j].DataMovimento.ToString("yyyy-mm-dd"), ListaR03[k].TotalizadorParcial);
                                        if (ListaC425 != null)
                                        {
                                            for (int l = 0; l <= ListaC425.Count - 1; l++)
                                            {
                                                var RegistroC425 = new RegistroC425();

                                                RegistroC425.COD_ITEM = Convert.ToString(ListaC425[l].IdProduto);
                                                RegistroC425.UNID = Convert.ToString(ListaC425[l].IdUnidade);
                                                RegistroC425.QTD = ListaC425[l].SomaQuantidade;
                                                RegistroC425.VL_ITEM = ListaC425[l].SomaValor;
                                                RegistroC425.VL_PIS = ListaC425[l].SomaPIS;
                                                RegistroC425.VL_COFINS = ListaC425[l].SomaCOFINS;
                                                RegistroC420.RegistroC425.Add(RegistroC425);

                                            }
                                        }
                                    }
                                }
                            }

                            //  se tiver o perfil A, gera o C460 com C470
                            if (PerfilApresentacao == 0)
                            {
                                //  C460
                                ListaR04 = RegistroRController.TabelaR04(ListaR02[j].DataMovimento.ToString("yyyy-mm-dd"), ListaR02[j].DataMovimento.ToString("yyyy-mm-dd"), ListaImpressora[i].Id);
                                if (ListaR04 != null)
                                {
                                    for (int l = 0; l <= ListaR04.Count - 1; l++)
                                    {
                                        var RegistroC460 = new RegistroC460();
                                        RegistroC460.COD_MOD = "2D";
                                        if (ListaR04[l].Cancelado == "S")
                                            RegistroC460.COD_SIT = SituacaoDocto.Cancelado;
                                        else
                                            RegistroC460.COD_SIT = SituacaoDocto.Regular;

                                        if (RegistroC460.COD_SIT == SituacaoDocto.Regular)
                                        {
                                            RegistroC460.DT_DOC = ListaR04[l].DataEmissao;
                                            RegistroC460.VL_DOC = ListaR04[l].ValorLiquido;
                                            RegistroC460.VL_PIS = ListaR04[l].PIS;
                                            RegistroC460.VL_PIS = ListaR04[l].COFINS;
                                            RegistroC460.CPF_CNPJ = ListaR04[l].CPFCNPJ;
                                            RegistroC460.NOM_ADQ = ListaR04[l].Cliente;
                                        }
                                        RegistroC460.NUM_DOC = Convert.ToString(ListaR04[l].COO);
                                        RegistroC405.RegistroC460.Add(RegistroC460);

                                        if (RegistroC460.COD_SIT == SituacaoDocto.Regular)
                                        {
                                            //  C470
                                            ListaR05 = RegistroRController.TabelaR05(ListaR04[l].Id, "Sped");
                                            if (ListaR05 != null)
                                            {
                                                for (int m = 0; m <= ListaR05.Count - 1; m++)
                                                {
                                                    var RegistroC470 = new RegistroC470();
                                                    RegistroC470.COD_ITEM = Convert.ToString(ListaR05[m].IdProduto);
                                                    RegistroC470.QTD = ListaR05[m].Quantidade;
                                                    RegistroC470.QTD_CANC = ListaR05[m].QuantidadeCancelada;
                                                    RegistroC470.UNID = Convert.ToString(ListaR05[m].IdUnidade);
                                                    RegistroC470.VL_ITEM = ListaR05[m].TotalItem;
                                                    RegistroC470.CST_ICMS = ListaR05[m].CST;
                                                    RegistroC470.CFOP = Convert.ToString(ListaR05[m].CFOP);
                                                    RegistroC470.ALIQ_ICMS = ListaR05[m].AliquotaICMS;
                                                    RegistroC470.VL_PIS = ListaR05[m].PIS;
                                                    RegistroC470.VL_COFINS = ListaR05[m].COFINS;
                                                    RegistroC460.RegistroC470.Add(RegistroC470);

                                                }
                                            }
                                        }

                                    }
                                }
                            }

                            //  C490
                            ListaC490 = SpedFiscalController.TabelaC490(RegistroC405.DT_DOC.ToString("yyyy-mm-dd"), DataFinal);
                            if (ListaC490 != null)
                            {
                                for (int g = 0; g <= ListaC490.Count - 1; g++)
                                {
                                    var RegistroC490 = new RegistroC490();

                                    RegistroC490.CST_ICMS = ListaC490[g].CST;
                                    RegistroC490.CFOP = Convert.ToString(ListaC490[g].CFOP);
                                    RegistroC490.ALIQ_ICMS = ListaC490[g].TaxaICMS;
                                    RegistroC490.VL_OPR = ListaC490[g].SomaValor;
                                    RegistroC490.VL_BC_ICMS = ListaC490[g].SomaBaseICMS;
                                    RegistroC490.VL_ICMS = ListaC490[g].SomaICMS;
                                    RegistroC405.RegistroC490.Add(RegistroC490);

                                }
                            }

                        }

                    }
                }
            }
        }


        //  Bloco E
        public static void GerarBlocoE()
        {
            var BlocoE = FDataModule.ACBrSpedFiscal.Bloco_E;

            var RegistroE001 = BlocoE.RegistroE001;
            RegistroE001.IND_MOV = IndicadorMovimento.ComDados;

            var RegistroE100 = new RegistroE100();
            RegistroE100.DT_INI = Convert.ToDateTime(DataInicial);
            RegistroE100.DT_FIN = Convert.ToDateTime(DataFinal);

            List<MeiosPagamentoVO> ListaE110 = new SpedFiscalController().TabelaE110(DataInicial, DataFinal);
            if (ListaE110 != null)
            {
                for (int i = 0; i <= ListaE110.Count - 1; i++)
                {
                    var RegistroE110 = RegistroE100.RegistroE110;
                    RegistroE110.VL_TOT_DEBITOS = ListaE110[i].Total;
                    RegistroE110.VL_AJ_DEBITOS = 0;
                    RegistroE110.VL_TOT_AJ_DEBITOS = 0;
                    RegistroE110.VL_ESTORNOS_CRED = 0;
                    RegistroE110.VL_TOT_CREDITOS = 0;
                    RegistroE110.VL_AJ_CREDITOS = 0;
                    RegistroE110.VL_TOT_AJ_CREDITOS = 0;
                    RegistroE110.VL_ESTORNOS_DEB = 0;
                    RegistroE110.VL_SLD_CREDOR_ANT = 0;
                    RegistroE110.VL_SLD_APURADO = ListaE110[i].Total;
                    RegistroE110.VL_TOT_DED = 0;
                    RegistroE110.VL_ICMS_RECOLHER = ListaE110[i].Total;
                    RegistroE110.VL_SLD_CREDOR_TRANSPORTAR = 0;
                    RegistroE110.DEB_ESP = 0;

                    var RegistroE116 = new RegistroE116();

                    RegistroE116.COD_OR = "000";
                    RegistroE116.VL_OR = ListaE110[i].Total;
                    RegistroE116.DT_VCTO = Convert.ToDateTime(DataFinal);
                    RegistroE116.COD_REC = "1";
                    RegistroE116.NUM_PROC = "";
                    RegistroE116.IND_PROC = OrigemProcesso.Nenhum;
                    RegistroE116.PROC = "";
                    RegistroE116.TXT_COMPL = "";
                    RegistroE116.MES_REF = ListaE110[i].DataHora.ToString("dd/MM/yyyy").Substring(2, 6) + ListaE110[i].DataHora.ToString("dd/MM/yyyy").Substring(4, 1); //  '092011';
                }
            }
        }


        //  Bloco H
        public static void GerarBlocoH()
        {
            var BlocoH = FDataModule.ACBrSpedFiscal.Bloco_H;

            var RegistroH001 = BlocoH.RegistroH001;
            RegistroH001.IND_MOV = IndicadorMovimento.SemDados;
        }


        public static void GerarArquivoSpedFiscal(string pDataIni, string pDataFim, int pVersao, int pFinalidade, int pPerfil)
        {
            try
            {
                string Mensagem, Arquivo;
                VersaoLeiaute = pVersao;
                FinalidadeArquivo = pFinalidade;
                PerfilApresentacao = pPerfil;
                DataInicial = pDataIni;
                DataFinal = pDataFim;

                FDataModule.ACBrSpedFiscal.DT_INI = Convert.ToDateTime(pDataIni);
                FDataModule.ACBrSpedFiscal.DT_FIN = Convert.ToDateTime(pDataFim);

                GerarBloco0();
                GerarBlocoC();
                GerarBlocoE();
                GerarBlocoH();

                Arquivo = FCaixa.Configuracao.Laudo + DateTime.Now.ToString("DDMMYYYYhhmmss") + ".txt";

                FDataModule.ACBrSpedFiscal.Path = Application.StartupPath;
                //FDataModule.ACBrSpedFiscal.SaveFileTXT(@Arquivo);

                Mensagem = "Arquivo armazenado em: " + Application.StartupPath + "\\" + Arquivo;
                MessageBox.Show(Mensagem, "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }

    }

}

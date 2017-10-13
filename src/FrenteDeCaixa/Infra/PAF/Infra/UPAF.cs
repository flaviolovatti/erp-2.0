/********************************************************************************
Title: T2TiPDV
Description: Funções e procedimentos do PAF;

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
using PafEcf.VO;
using PafEcf.Controller;
using System.Collections.Generic;
using PafEcf.View;
using PafEcf.Util;
using ACBrFramework.PAF;
using System.Xml;

namespace PafEcf.Infra
{

    public static class UPAF
    {

        public static void GeraTabelaProdutos()
        {
            string Mensagem, Tripa;
            try
            {
                List<ProdutoVO> ListaProduto = new ProdutoController().TabelaProduto();
                if (ListaProduto != null)
                {
                    EmpresaVO Empresa = new EmpresaController().PegaEmpresa(FCaixa.Configuracao.IdEmpresa);
                    ImpressoraVO Impressora = new ImpressoraController().PegaImpressora(FCaixa.Configuracao.IdImpressora);

                    //  registro P1
                    FDataModule.ACBrPAF.PAF_P.RegistroP1.CNPJ = Empresa.Cnpj;
                    FDataModule.ACBrPAF.PAF_P.RegistroP1.IE = Empresa.InscricaoEstadual;
                    FDataModule.ACBrPAF.PAF_P.RegistroP1.IM = Empresa.Cnpj;
                    FDataModule.ACBrPAF.PAF_P.RegistroP1.UF = Empresa.Uf;
                    FDataModule.ACBrPAF.PAF_P.RegistroP1.RazaoSocial = Empresa.RazaoSocial;

                    //  registro P2
                    FDataModule.ACBrPAF.PAF_P.RegistroP2.Clear();
                    for (int i = 0; i <= ListaProduto.Count - 1; i++)
                    {
                        Tripa = ListaProduto[i].GTIN +
                                  ListaProduto[i].Descricao +
                                  ListaProduto[i].DescricaoPDV +
                                  Biblioteca.FormataFloat("Q", ListaProduto[i].QtdeEstoqueAnterior) +
                                  ListaProduto[i].DataEstoque +
                                  ListaProduto[i].Cst +
                                  Biblioteca.FormataFloat("V", ListaProduto[i].AliquotaICMS) +
                                  Biblioteca.FormataFloat("V", ListaProduto[i].ValorVenda) +
                                  Convert.ToString(ListaProduto[i].HashIncremento);

                        ACBrPAFRegistroP2 P2 = new ACBrPAFRegistroP2();

                        P2.COD_MERC_SERV = ListaProduto[i].GTIN;
                        P2.DESC_MERC_SERV = ListaProduto[i].DescricaoPDV;
                        P2.UN_MED = ListaProduto[i].UnidadeProduto;

                        if (Biblioteca.MD5String(Tripa) != ListaProduto[i].HashTripa)
                            P2.RegistroValido = false;
                        else
                            P2.RegistroValido = true;
                        P2.IAT = ListaProduto[i].IAT;
                        P2.IPPT = ListaProduto[i].IPPT;
                        P2.ST = ListaProduto[i].PafProdutoST;
                        P2.ALIQ = ListaProduto[i].AliquotaICMS;
                        P2.VL_UNIT = ListaProduto[i].ValorVenda;
                        FDataModule.ACBrPAF.PAF_P.RegistroP2.Add(P2);
                        P2 = null;
                    }

                    FDataModule.ACBrPAF.Path = Application.StartupPath;
                    FDataModule.ACBrPAF.SaveFileTXT_P(@"\PAF_P.txt");

                    Mensagem = "Arquivo armazenado em: " + Application.StartupPath + "\\PAF_P.txt";
                    MessageBox.Show(Mensagem, "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Não existem produtos na tabela.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        public static void GeraArquivoEstoque()
        {
            try
            {
                List<ProdutoVO> ListaProduto = new ProdutoController().TabelaProduto();
                if (ListaProduto != null)
                    GeraEstoque(ListaProduto);
                else
                    MessageBox.Show("Não existem produtos na tabela.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        public static void GeraArquivoEstoque(int pCodigoInicio, int pCodigoFim)
        {
            try
            {
                List<ProdutoVO> ListaProduto = new ProdutoController().TabelaProduto(pCodigoInicio, pCodigoFim);
                if (ListaProduto != null)
                    GeraEstoque(ListaProduto);
                else
                    MessageBox.Show("Não existem produtos na tabela.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        public static void GeraArquivoEstoque(string pNomeInicio, string pNomeFim)
        {
            try
            {
                List<ProdutoVO> ListaProduto = new ProdutoController().TabelaProduto(pNomeInicio, pNomeFim);
                if (ListaProduto != null)
                    GeraEstoque(ListaProduto);
                else
                    MessageBox.Show("Não existem produtos na tabela.", "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        public static void GeraEstoque(List<ProdutoVO> ListaProduto)
        {
            string Mensagem, Tripa;
            try
            {
                EmpresaVO Empresa = new EmpresaController().PegaEmpresa(FCaixa.Configuracao.IdEmpresa);

                //  registro E1
                FDataModule.ACBrPAF.PAF_E.RegistroE1.CNPJ = Empresa.Cnpj;
                FDataModule.ACBrPAF.PAF_E.RegistroE1.IE = Empresa.InscricaoEstadual;
                FDataModule.ACBrPAF.PAF_E.RegistroE1.IM = Empresa.Cnpj;
                FDataModule.ACBrPAF.PAF_E.RegistroE1.UF = Empresa.Uf;
                FDataModule.ACBrPAF.PAF_E.RegistroE1.RazaoSocial = Empresa.RazaoSocial;

                //  registro E2
                FDataModule.ACBrPAF.PAF_E.RegistroE2.Clear();
                for (int i = 0; i <= ListaProduto.Count - 1; i++)
                {
                    Tripa = ListaProduto[i].GTIN +
                              ListaProduto[i].Descricao +
                              ListaProduto[i].DescricaoPDV +
                              Biblioteca.FormataFloat("Q", ListaProduto[i].QtdeEstoqueAnterior) +
                              ListaProduto[i].DataEstoque +
                              ListaProduto[i].Cst +
                              Biblioteca.FormataFloat("V", ListaProduto[i].AliquotaICMS) +
                              Biblioteca.FormataFloat("V", ListaProduto[i].ValorVenda) +
                              Convert.ToString(ListaProduto[i].HashIncremento);

                    ACBrPAFRegistroE2 E2 = new ACBrPAFRegistroE2();

                    E2.COD_MERC = ListaProduto[i].GTIN;
                    E2.DESC_MERC = ListaProduto[i].DescricaoPDV;
                    E2.UN_MED = ListaProduto[i].UnidadeProduto;

                    if (Biblioteca.MD5String(Tripa) != ListaProduto[i].HashTripa)
                        E2.RegistroValido = false;
                    else
                        E2.RegistroValido = true;

                    E2.QTDE_EST = ListaProduto[i].QtdeEstoqueAnterior;
                    FDataModule.ACBrPAF.PAF_E.RegistroE2.Add(E2);
                    E2 = null;
                }

                FDataModule.ACBrPAF.Path = Application.StartupPath;
                FDataModule.ACBrPAF.SaveFileTXT_E(@"\PAF_E.txt");

                Mensagem = "Arquivo armazenado em: " + Application.StartupPath + "\\PAF_E.txt";
                MessageBox.Show(Mensagem, "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        //TODO:  Verifique se o procedimento abaixo está correto. Corrija o que for necessário.
        public static void GravaR02R03()
        {
            string Indice, Aliquota;
            try
            {
                RegistroRController RegistroRController = new RegistroRController();

                List<R03VO> ListaR03 = new List<R03VO>();
                R03VO R03;

                // Dados para o registro R02
                R02VO R02 = new R02VO();
                R02.IdCaixa = FCaixa.Movimento.IdCaixa;
                R02.IdOperador = FCaixa.Movimento.IdOperador;
                R02.IdImpressora = FCaixa.Movimento.IdImpressora;

                FDataModule.ACBrECF.DadosReducaoZ();

                R02.CRZ = FDataModule.ACBrECF.DadosReducaoZClass.CRZ + 1;
                R02.COO = Convert.ToInt32(FDataModule.ACBrECF.NumCOO) + 1;
                R02.CRO = FDataModule.ACBrECF.DadosReducaoZClass.CRO;
                R02.SerieEcf = FCaixa.Configuracao.NumSerieECF;
                R02.DataMovimento = FDataModule.ACBrECF.DadosReducaoZClass.DataDoMovimento;
                R02.DataEmissao = FDataModule.ACBrECF.DadosReducaoZClass.DataDaImpressora;
                R02.HoraEmissao = FDataModule.ACBrECF.DadosReducaoZClass.DataDaImpressora.ToString("hh:mm:ss");
                R02.VendaBruta = FDataModule.ACBrECF.DadosReducaoZClass.ValorVendaBruta;
                R02.GrandeTotal = FDataModule.ACBrECF.DadosReducaoZClass.ValorGrandeTotal;

                R02 = RegistroRController.GravaR02(R02);

                // Dados para o registro R03

                // Dados ICMS
                for (int i = 0; i <= FDataModule.ACBrECF.DadosReducaoZClass.ICMS.Length - 1; i++)
                {
                    R03 = new R03VO();
                    R03.IdR02 = R02.Id;
                    R03.CRZ = FDataModule.ACBrECF.DadosReducaoZClass.CRZ + 1;
                    // Completa com zeros a esquerda
                    Indice = new string('0', 2 - FDataModule.ACBrECF.DadosReducaoZClass.ICMS[i].Indice.Length) + FDataModule.ACBrECF.DadosReducaoZClass.ICMS[i].Indice;
                    // Tira as virgulas
                    Aliquota = (FDataModule.ACBrECF.DadosReducaoZClass.ICMS[i].ValorAliquota * 100).ToString().Replace(",", "");
                    // Completa com zeros a esquerda e a direita
                    Aliquota = new string('0', 4 - Aliquota.Length) + Aliquota;
                    R03.TotalizadorParcial = Indice + "T" + Aliquota;
                    R03.ValorAcumulado = FDataModule.ACBrECF.DadosReducaoZClass.ICMS[i].Total;
                    R03.SerieEcf = FCaixa.Configuracao.NumSerieECF;
                    ListaR03.Add(R03);
                }

                // Dados ISSQN
                for (int i = 0; i <= FDataModule.ACBrECF.DadosReducaoZClass.ISSQN.Length - 1; i++)
                {
                    R03 = new R03VO();
                    R03.IdR02 = R02.Id;
                    // Completa com zeros a esquerda
                    Indice = new string('0', 2 - FDataModule.ACBrECF.DadosReducaoZClass.ISSQN[i].Indice.Length) + FDataModule.ACBrECF.DadosReducaoZClass.ISSQN[i].Indice;
                    // Tira as virgulas
                    Aliquota = (FDataModule.ACBrECF.DadosReducaoZClass.ISSQN[i].ValorAliquota * 100).ToString().Replace(",", "");
                    // Completa com zeros a esquerda
                    Aliquota = new string('0', 4 - Aliquota.Length) + Aliquota;
                    R03.TotalizadorParcial = Indice + "S" + Aliquota;
                    R03.ValorAcumulado = FDataModule.ACBrECF.DadosReducaoZClass.ISSQN[i].Total;
                    ListaR03.Add(R03);
                }

                // Substituição Tributária - ICMS
                R03 = new R03VO();
                R03.IdR02 = R02.Id;
                R03.TotalizadorParcial = "F1";
                R03.ValorAcumulado = FDataModule.ACBrECF.DadosReducaoZClass.SubstituicaoTributariaICMS;
                ListaR03.Add(R03);

                // Isento - ICMS
                R03 = new R03VO();
                R03.IdR02 = R02.Id;
                R03.TotalizadorParcial = "I1";
                R03.ValorAcumulado = FDataModule.ACBrECF.DadosReducaoZClass.IsentoICMS;
                ListaR03.Add(R03);

                // Não-incidência - ICMS
                R03 = new R03VO();
                R03.IdR02 = R02.Id;
                R03.TotalizadorParcial = "N1";
                R03.ValorAcumulado = FDataModule.ACBrECF.DadosReducaoZClass.NaoTributadoICMS;
                ListaR03.Add(R03);

                // Substituição Tributária - ISSQN
                R03 = new R03VO();
                R03.IdR02 = R02.Id;
                R03.TotalizadorParcial = "FS1";
                R03.ValorAcumulado = FDataModule.ACBrECF.DadosReducaoZClass.SubstituicaoTributariaISSQN;
                ListaR03.Add(R03);

                // Isento - ISSQN
                R03 = new R03VO();
                R03.IdR02 = R02.Id;
                R03.TotalizadorParcial = "IS1";
                R03.ValorAcumulado = FDataModule.ACBrECF.DadosReducaoZClass.IsentoISSQN;
                ListaR03.Add(R03);

                // Não-incidência - ISSQN
                R03 = new R03VO();
                R03.IdR02 = R02.Id;
                R03.TotalizadorParcial = "NS1";
                R03.ValorAcumulado = FDataModule.ACBrECF.DadosReducaoZClass.NaoTributadoISSQN;
                ListaR03.Add(R03);

                // Operações Não Fiscais
                R03 = new R03VO();
                R03.IdR02 = R02.Id;
                R03.TotalizadorParcial = "OPNF";
                R03.ValorAcumulado = FDataModule.ACBrECF.DadosReducaoZClass.TotalOperacaoNaoFiscal;
                ListaR03.Add(R03);

                // Desconto - ICMS
                R03 = new R03VO();
                R03.IdR02 = R02.Id;
                R03.TotalizadorParcial = "DT";
                R03.ValorAcumulado = FDataModule.ACBrECF.DadosReducaoZClass.DescontoICMS;
                ListaR03.Add(R03);

                // Desconto - ISSQN
                R03 = new R03VO();
                R03.IdR02 = R02.Id;
                R03.TotalizadorParcial = "DS";
                R03.ValorAcumulado = FDataModule.ACBrECF.DadosReducaoZClass.DescontoISSQN;
                ListaR03.Add(R03);

                // Acréscimo - ICMS
                R03 = new R03VO();
                R03.IdR02 = R02.Id;
                R03.TotalizadorParcial = "AT";
                R03.ValorAcumulado = FDataModule.ACBrECF.DadosReducaoZClass.AcrescimoICMS;
                ListaR03.Add(R03);

                // Acréscimo - ISSQN
                R03 = new R03VO();
                R03.IdR02 = R02.Id;
                R03.TotalizadorParcial = "AS";
                R03.ValorAcumulado = FDataModule.ACBrECF.DadosReducaoZClass.AcrescimoISSQN;
                ListaR03.Add(R03);

                // Cancelamento - ICMS
                R03 = new R03VO();
                R03.IdR02 = R02.Id;
                R03.TotalizadorParcial = "Can-T";
                R03.ValorAcumulado = FDataModule.ACBrECF.DadosReducaoZClass.CancelamentoICMS;
                ListaR03.Add(R03);

                // Cancelamento - ISSQN
                R03 = new R03VO();
                R03.IdR02 = R02.Id;
                R03.TotalizadorParcial = "Can-S";
                R03.ValorAcumulado = FDataModule.ACBrECF.DadosReducaoZClass.CancelamentoISSQN;
                ListaR03.Add(R03);

                RegistroRController.GravaR03(ListaR03);

                Grava60M60A(R02.IdImpressora);

            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        //TODO:  Verifique se o procedimento abaixo está correto. Corrija o que for necessário.
        public static void Grava60M60A(int IdImpressora)
        {
            try
            {
                ImpressoraVO Impressora = new ImpressoraController().PegaImpressora(IdImpressora);

                SintegraController SintegraController = new SintegraController();

                Sintegra60MVO Sintegra60M = new Sintegra60MVO();
                Sintegra60AVO Sintegra60A;
                List<Sintegra60AVO> Lista60A = new List<Sintegra60AVO>();

                Sintegra60M.DataEmissao = FDataModule.ACBrECF.DataHora;
                Sintegra60M.SerieImpressora = FDataModule.ACBrECF.NumSerie;
                Sintegra60M.NumeroEquipamento = Convert.ToInt32(FDataModule.ACBrECF.NumECF);
                Sintegra60M.ModeloDocumentoFiscal = Impressora.ModeloDocumentoFiscal;
                Sintegra60M.COOInicial = Convert.ToInt32(FDataModule.ACBrECF.NumCOOInicial);
                Sintegra60M.COOFinal = Convert.ToInt32(FDataModule.ACBrECF.NumCOO) + 1;
                Sintegra60M.CRZ = Convert.ToInt32(FDataModule.ACBrECF.NumCRZ) + 1;
                Sintegra60M.CRO = Convert.ToInt32(FDataModule.ACBrECF.NumCRO);
                Sintegra60M.VendaBruta = FDataModule.ACBrECF.VendaBruta;
                Sintegra60M.GrandeTotal = FDataModule.ACBrECF.GrandeTotal;

                SintegraController.Grava60M(Sintegra60M);

                FDataModule.ACBrECF.DadosReducaoZ();

                // Dados para o registro R03

                // Dados ICMS
                for (int i = 0; i <= FDataModule.ACBrECF.DadosReducaoZClass.ICMS.Length - 1; i++)
                {
                    Sintegra60A = new Sintegra60AVO();
                    Sintegra60A.Id60M = Sintegra60M.Id;
                    Sintegra60A.SituacaoTributaria = FDataModule.ACBrECF.DadosReducaoZClass.ICMS[i].ValorAliquota.ToString().Replace(",", "");
                    Sintegra60A.Valor = FDataModule.ACBrECF.DadosReducaoZClass.ICMS[i].Total;
                    Lista60A.Add(Sintegra60A);
                }

                // Dados ISSQN
                for (int i = 0; i <= FDataModule.ACBrECF.DadosReducaoZClass.ISSQN.Length - 1; i++)
                {
                    Sintegra60A = new Sintegra60AVO();
                    Sintegra60A.Id60M = Sintegra60M.Id;
                    Sintegra60A.SituacaoTributaria = FDataModule.ACBrECF.DadosReducaoZClass.ISSQN[i].ValorAliquota.ToString().Replace(",", "");
                    Sintegra60A.Valor = FDataModule.ACBrECF.DadosReducaoZClass.ISSQN[i].Total;
                    Lista60A.Add(Sintegra60A);
                }

                // Substituição Tributária - ICMS
                Sintegra60A = new Sintegra60AVO();
                Sintegra60A.Id60M = Sintegra60M.Id;
                Sintegra60A.SituacaoTributaria = "F";
                Sintegra60A.Valor = FDataModule.ACBrECF.DadosReducaoZClass.SubstituicaoTributariaICMS;
                Lista60A.Add(Sintegra60A);

                // Isento - ICMS
                Sintegra60A = new Sintegra60AVO();
                Sintegra60A.Id60M = Sintegra60M.Id;
                Sintegra60A.SituacaoTributaria = "I";
                Sintegra60A.Valor = FDataModule.ACBrECF.DadosReducaoZClass.IsentoICMS;
                Lista60A.Add(Sintegra60A);

                // Não-incidência - ICMS
                Sintegra60A = new Sintegra60AVO();
                Sintegra60A.Id60M = Sintegra60M.Id;
                Sintegra60A.SituacaoTributaria = "N";
                Sintegra60A.Valor = FDataModule.ACBrECF.DadosReducaoZClass.NaoTributadoICMS;
                Lista60A.Add(Sintegra60A);

                // Desconto - ICMS
                Sintegra60A = new Sintegra60AVO();
                Sintegra60A.Id60M = Sintegra60M.Id;
                Sintegra60A.SituacaoTributaria = "DESC";
                Sintegra60A.Valor = FDataModule.ACBrECF.DadosReducaoZClass.DescontoICMS;
                Lista60A.Add(Sintegra60A);

                // Cancelamento - ICMS
                Sintegra60A = new Sintegra60AVO();
                Sintegra60A.Id60M = Sintegra60M.Id;
                Sintegra60A.SituacaoTributaria = "CANC";
                Sintegra60A.Valor = FDataModule.ACBrECF.DadosReducaoZClass.CancelamentoICMS;
                Lista60A.Add(Sintegra60A);

                SintegraController.Grava60A(Lista60A);
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        //TODO:  Verifique se o procedimento abaixo está correto. Corrija o que for necessário.
        public static void GeraMovimentoECF(string DataInicio, string DataFim, string DataMovimento, ImpressoraVO Impressora)
        {

            RegistroRController RegistroRController = new RegistroRController();
            List<R02VO> ListaR02;
            List<R03VO> ListaR03;
            List<R04VO> ListaR04;
            List<R05VO> ListaR05;
            List<R06VO> ListaR06;
            List<R07VO> ListaR07;

            string Mensagem, NomeArquivo, TripaR01, TripaR02, TripaR03, TripaR04, TripaR05, TripaR06, TripaR07;

            try
            {
                // dados da empresa
                EmpresaVO Empresa = new EmpresaController().PegaEmpresa(FCaixa.Configuracao.IdEmpresa);
                // dados software house e demais do R01
                R01VO R01 = new RegistroRController().RegistroR01();

                //  Registro R01 - Identificação do ECF, do Usuário, do PAF-ECF e da Empresa Desenvolvedora e Dados do Arquivo

                TripaR01 = R01.SerieEcf +
                            R01.CnpjEmpresa +
                            Convert.ToString(R01.HashIncremento);

                if (Biblioteca.MD5String(TripaR01) != R01.HashTripa)
                    FDataModule.ACBrPAF.PAF_R.RegistroR1.RegistroValido = false;

                FDataModule.ACBrPAF.PAF_R.RegistroR1.NUM_FAB = R01.SerieEcf;
                FDataModule.ACBrPAF.PAF_R.RegistroR1.MF_ADICIONAL = Impressora.MFD;
                FDataModule.ACBrPAF.PAF_R.RegistroR1.TIPO_ECF = Impressora.Tipo;
                FDataModule.ACBrPAF.PAF_R.RegistroR1.MARCA_ECF = Impressora.Marca;
                FDataModule.ACBrPAF.PAF_R.RegistroR1.MODELO_ECF = Impressora.Modelo;
                FDataModule.ACBrPAF.PAF_R.RegistroR1.VERSAO_SB = Impressora.Versao;
                FDataModule.ACBrPAF.PAF_R.RegistroR1.DT_INST_SB = Impressora.DataInstalacaoSb;
                FDataModule.ACBrPAF.PAF_R.RegistroR1.HR_INST_SB = Convert.ToDateTime(Impressora.HoraInstalacaoSb);
                FDataModule.ACBrPAF.PAF_R.RegistroR1.NUM_SEQ_ECF = Convert.ToInt32(Impressora.NumeroEcf);
                FDataModule.ACBrPAF.PAF_R.RegistroR1.CNPJ = R01.CnpjEmpresa;
                FDataModule.ACBrPAF.PAF_R.RegistroR1.IE = Empresa.InscricaoEstadual;
                FDataModule.ACBrPAF.PAF_R.RegistroR1.CNPJ_SH = R01.CnpjSh;
                FDataModule.ACBrPAF.PAF_R.RegistroR1.IE_SH = R01.InscricaoEstadualSh;
                FDataModule.ACBrPAF.PAF_R.RegistroR1.IM_SH = R01.InscricaoMunicipalSh;
                FDataModule.ACBrPAF.PAF_R.RegistroR1.NOME_SH = R01.DenominacaoSh;
                FDataModule.ACBrPAF.PAF_R.RegistroR1.NOME_PAF = R01.NomePafEcf;
                FDataModule.ACBrPAF.PAF_R.RegistroR1.VER_PAF = R01.VersaoPafEcf;
                FDataModule.ACBrPAF.PAF_R.RegistroR1.COD_MD5 = R01.Md5PafEcf;
                FDataModule.ACBrPAF.PAF_R.RegistroR1.DT_INI = Convert.ToDateTime(DataInicio);
                FDataModule.ACBrPAF.PAF_R.RegistroR1.DT_FIN = Convert.ToDateTime(DataFim);
                FDataModule.ACBrPAF.PAF_R.RegistroR1.ER_PAF_ECF = R01.VersaoEr;


                //  Registro R02 e R03
                ListaR02 = RegistroRController.TabelaR02(DataInicio, DataFim, Impressora.Id);
                if (ListaR02 != null)
                {
                    for (int i = 0; i <= ListaR02.Count - 1; i++)
                    {
                        ACBrPAFRegistroR2 RegistroR02 = new ACBrPAFRegistroR2();
                        TripaR02 = Convert.ToString(ListaR02[i].Id) +
                                  Convert.ToString(ListaR02[i].IdOperador) +
                                  Convert.ToString(ListaR02[i].IdImpressora) +
                                  Convert.ToString(ListaR02[i].IdCaixa) +
                                  Convert.ToString(ListaR02[i].CRZ) +
                                  Convert.ToString(ListaR02[i].COO) +
                                  Convert.ToString(ListaR02[i].CRO) +
                                  Convert.ToDateTime(ListaR02[i].DataMovimento).ToString("yyyy-mm-dd") +
                                  Convert.ToDateTime(ListaR02[i].DataEmissao).ToString("yyyy-mm-dd") +
                                  ListaR02[i].HoraEmissao +
                                  Biblioteca.FormataFloat("V", ListaR02[i].VendaBruta) +
                                  Biblioteca.FormataFloat("V", ListaR02[i].GrandeTotal) +
                                  ListaR02[i].SerieEcf +
                                  Convert.ToString(ListaR02[i].HashIncremento);

                        if (Biblioteca.MD5String(TripaR02) != ListaR02[i].HashTripa)
                            RegistroR02.RegistroValido = false;

                        RegistroR02.NUM_USU = ListaR02[i].IdOperador;
                        RegistroR02.CRZ = ListaR02[i].CRZ;
                        RegistroR02.COO = ListaR02[i].COO;
                        RegistroR02.CRO = ListaR02[i].CRO;
                        RegistroR02.DT_MOV = Convert.ToDateTime(ListaR02[i].DataMovimento);
                        RegistroR02.DT_EMI = Convert.ToDateTime(ListaR02[i].DataEmissao);
                        RegistroR02.HR_EMI = Convert.ToDateTime(ListaR02[i].HoraEmissao);
                        RegistroR02.VL_VBD = ListaR02[i].VendaBruta;
                        RegistroR02.PAR_ECF = "";
                        FDataModule.ACBrPAF.PAF_R.RegistroR2.Add(RegistroR02);
                        RegistroR02 = null;

                        //  Registro R03 - FILHO
                        ListaR03 = RegistroRController.TabelaR03(ListaR02[i].Id);
                        if (ListaR03 != null)
                        {
                            for (int j = 0; j <= ListaR03.Count - 1; j++)
                            {
                                ACBrPAFRegistroR3 RegistroR03 = new ACBrPAFRegistroR3();
                                TripaR03 = ListaR03[j].TotalizadorParcial +
                                          Biblioteca.FormataFloat("V", ListaR03[j].ValorAcumulado) +
                                          Convert.ToString(ListaR03[j].CRZ) +
                                          ListaR03[j].SerieEcf +
                                          Convert.ToString(ListaR03[j].HashIncremento);


                                if (Biblioteca.MD5String(TripaR03) != ListaR03[j].HashTripa)
                                    RegistroR03.RegistroValido = false;

                                RegistroR03.TOT_PARCIAL = ListaR03[j].TotalizadorParcial;
                                RegistroR03.VL_ACUM = ListaR03[j].ValorAcumulado;
                                FDataModule.ACBrPAF.PAF_R.RegistroR2[i].RegistroR3.Add(RegistroR03);
                                RegistroR03 = null;

                            }// for j := 0 to ListaR03.Count - 1 do
                        }// if Assigned(ListaR03) then

                    }// for i := 0 to ListaR02.Count - 1 do
                }// if Assigned(ListaR02) then


                //  Registro R04 e R05
                ListaR04 = RegistroRController.TabelaR04(DataInicio, DataFim, Impressora.Id);
                if (ListaR04 != null)
                {
                    for (int i = 0; i <= ListaR04.Count - 1; i++)
                    {

                        ACBrPAFRegistroR4 RegistroR04 = new ACBrPAFRegistroR4();
                        TripaR04 = Convert.ToString(ListaR04[i].Id) +
                                 Convert.ToString(ListaR04[i].CCF) +
                                 Convert.ToString(ListaR04[i].COO) +
                                 Biblioteca.FormataFloat("V", ListaR04[i].ValorLiquido) +
                                 ListaR04[i].SerieEcf +
                                 ListaR04[i].StatusVenda +
                                 ListaR04[i].Cancelado +
                                 Convert.ToString(ListaR04[i].HashIncremento);


                        if (Biblioteca.MD5String(TripaR04) != ListaR04[i].HashTripa)
                            RegistroR04.RegistroValido = false;

                        RegistroR04.NUM_USU = ListaR04[i].IdOperador;
                        RegistroR04.NUM_CONT = ListaR04[i].CCF;
                        RegistroR04.COO = ListaR04[i].COO;
                        RegistroR04.DT_INI = Convert.ToDateTime(ListaR04[i].DataEmissao);
                        RegistroR04.SUB_DOCTO = ListaR04[i].SubTotal;
                        RegistroR04.SUB_DESCTO = ListaR04[i].Desconto;
                        RegistroR04.TP_DESCTO = ListaR04[i].IndicadorDesconto;
                        RegistroR04.SUB_ACRES = ListaR04[i].Acrescimo;
                        RegistroR04.TP_ACRES = ListaR04[i].IndicadorAcrescimo;
                        RegistroR04.VL_TOT = ListaR04[i].ValorLiquido;
                        RegistroR04.CANC = ListaR04[i].Cancelado;
                        RegistroR04.VL_CA = ListaR04[i].CancelamentoAcrescimo;
                        RegistroR04.ORDEM_DA = ListaR04[i].OrdemDescontoAcrescimo;
                        RegistroR04.NOME_CLI = ListaR04[i].Cliente;
                        RegistroR04.CNPJ_CPF = ListaR04[i].CPFCNPJ;

                        FDataModule.ACBrPAF.PAF_R.RegistroR4.Add(RegistroR04);
                        RegistroR04 = null;

                        // Registro R05 - FILHO
                        ListaR05 = RegistroRController.TabelaR05(ListaR04[i].Id, "Paf");
                        if (ListaR05 != null)
                        {
                            for (int j = 0; j <= ListaR05.Count - 1; j++)
                            {

                                ACBrPAFRegistroR5 RegistroR05 = new ACBrPAFRegistroR5();
                                TripaR05 =
                                            ListaR05[j].SerieEcf +
                                            Convert.ToString(ListaR05[j].COO) +
                                            Convert.ToString(ListaR05[j].CCF) +
                                            ListaR05[j].GTIN +
                                            Biblioteca.FormataFloat("Q", ListaR05[j].Quantidade) +
                                            Biblioteca.FormataFloat("V", ListaR05[j].ValorUnitario) +
                                            Biblioteca.FormataFloat("V", ListaR05[j].TotalItem) +
                                            ListaR05[j].TotalizadorParcial +
                                            ListaR05[j].IndicadorCancelamento +
                                            Convert.ToString(ListaR05[j].HashIncremento);


                                if (Biblioteca.MD5String(TripaR05) != ListaR05[j].HashTripa)
                                    RegistroR05.RegistroValido = false;

                                RegistroR05.NUM_ITEM = ListaR05[j].Item;
                                RegistroR05.COD_ITEM = ListaR05[j].GTIN;
                                RegistroR05.DESC_ITEM = ListaR05[j].DescricaoPDV;
                                RegistroR05.QTDE_ITEM = ListaR05[j].Quantidade;
                                RegistroR05.UN_MED = ListaR05[j].SiglaUnidade;
                                RegistroR05.VL_UNIT = ListaR05[j].ValorUnitario;
                                RegistroR05.DESCTO_ITEM = ListaR05[j].Desconto;
                                RegistroR05.ACRES_ITEM = ListaR05[j].Acrescimo;
                                RegistroR05.VL_TOT_ITEM = ListaR05[j].TotalItem;
                                RegistroR05.COD_TOT_PARC = ListaR05[j].TotalizadorParcial;
                                RegistroR05.IND_CANC = ListaR05[j].IndicadorCancelamento;
                                RegistroR05.QTDE_CANC = ListaR05[j].QuantidadeCancelada;
                                RegistroR05.VL_CANC = ListaR05[j].ValorCancelado;
                                RegistroR05.VL_CANC_ACRES = ListaR05[j].CancelamentoAcrescimo;
                                RegistroR05.IAT = ListaR05[j].IAT;
                                RegistroR05.IPPT = ListaR05[j].IPPT;
                                RegistroR05.QTDE_DECIMAL = ListaR05[j].CasasDecimaisQuantidade;
                                RegistroR05.VL_DECIMAL = ListaR05[j].CasasDecimaisValor;

                                FDataModule.ACBrPAF.PAF_R.RegistroR4[i].RegistroR5.Add(RegistroR05);
                                RegistroR05 = null;

                            }// for j := 0 to ListaR05.Count - 1 do
                        }// if Assigned(ListaR05) then


                        //  Registro R07 - MEIOS DE PAGAMENTO
                        ListaR07 = RegistroRController.TabelaR07IdR04(ListaR04[i].Id);
                        if (ListaR07 != null)
                        {
                            for (int j = 0; j <= ListaR07.Count - 1; j++)
                            {

                                ACBrPAFRegistroR7 RegistroR07 = new ACBrPAFRegistroR7();
                                TripaR07 =
                                            ListaR07[j].SerieEcf +
                                            Convert.ToString(ListaR07[j].Coo) +
                                            Convert.ToString(ListaR07[j].Ccf) +
                                            Convert.ToString(ListaR07[j].Gnf) +
                                            Convert.ToString(ListaR07[j].HashIncremento);



                                if (Biblioteca.MD5String(TripaR07) != ListaR07[j].HashTripa)
                                    RegistroR07.RegistroValido = false;

                                RegistroR07.CCF = ListaR07[j].Ccf;
                                RegistroR07.GNF = ListaR07[j].Gnf;
                                RegistroR07.MP = ListaR07[j].MeioPagamento;
                                RegistroR07.VL_PAGTO = ListaR07[j].ValorPagamento;
                                RegistroR07.IND_EST = ListaR07[j].IndicadorEstorno;
                                RegistroR07.VL_EST = ListaR07[j].ValorEstorno;

                                FDataModule.ACBrPAF.PAF_R.RegistroR4[i].RegistroR7.Add(RegistroR07);
                                RegistroR07 = null;

                            }// for j := 0 to ListaR07.Count - 1 do
                        }// if Assigned(ListaR07) then

                    }// for i := 0 to ListaR04.Count - 1 do
                }// if Assigned(ListaR04) then


                //  Registro R06 e R07
                ListaR06 = RegistroRController.TabelaR06(DataInicio, DataFim, Impressora.Id);
                if (ListaR06 != null)
                {
                    for (int i = 0; i <= ListaR06.Count - 1; i++)
                    {

                        ACBrPAFRegistroR6 RegistroR06 = new ACBrPAFRegistroR6();
                        TripaR06 =
                                 Convert.ToString(ListaR06[i].COO) +
                                 Convert.ToString(ListaR06[i].GNF) +
                                 Convert.ToString(ListaR06[i].GRG) +
                                 Convert.ToString(ListaR06[i].CDC) +
                                 ListaR06[i].Denominacao +
                                 ListaR06[i].DataEmissao +
                                 ListaR06[i].HoraEmissao +
                                 ListaR06[i].SerieEcf +
                                 Convert.ToString(ListaR06[i].HashIncremento);


                        if (Biblioteca.MD5String(TripaR06) != ListaR06[i].HashTripa)
                            RegistroR06.RegistroValido = false;

                        RegistroR06.NUM_USU = ListaR06[i].IdOperador;
                        RegistroR06.COO = ListaR06[i].COO;
                        RegistroR06.GNF = ListaR06[i].GNF;
                        RegistroR06.GRG = ListaR06[i].GRG;
                        RegistroR06.CDC = ListaR06[i].CDC;
                        RegistroR06.DENOM = ListaR06[i].Denominacao;
                        RegistroR06.DT_FIN = ListaR06[i].DataEmissao;
                        RegistroR06.HR_FIN = Convert.ToDateTime(ListaR06[i].HoraEmissao);

                        FDataModule.ACBrPAF.PAF_R.RegistroR6.Add(RegistroR06);
                        RegistroR06 = null;

                        /*
                        Usado apenas num paf que tenha R07 vinculado a R06 - não é o caso desse protótipo
                                  
                        //  Registro R07 - MEIOS DE PAGAMENTO
                        ListaR07 = RegistroRController.TabelaR07IdR06(ListaR06[i].Id);
                        if( ListaR07 != null )
                        {
                          for(int j = 0;j <= ListaR07.Count - 1;j++)
                          {
                            ACBrPAFRegistroR7 RegistroR07 = new ACBrPAFRegistroR7();
                              if( Biblioteca.MD5String(TripaR07) != ListaR07[j].HashTripa)
                                  RegistroR07.RegistroValido = false;

                              RegistroR07.CCF = ListaR07[j].Ccf;
                              RegistroR07.GNF = ListaR07[j].Gnf;
                              RegistroR07.MP = ListaR07[j].MeioPagamento;
                              RegistroR07.VL_PAGTO = ListaR07[j].ValorPagamento;
                              RegistroR07.IND_EST = ListaR07[j].IndicadorEstorno;
                              RegistroR07.VL_EST = ListaR07[j].ValorEstorno;
                                      
                          }// for j := 0 to ListaR07.Count - 1 do
                        }// if Assigned(ListaR07) then
                        */

                    }// for i := 0 to ListaR06.Count - 1 do
                }// if Assigned(ListaR06) then


                //TODO:  Formate o nome do arqivo de acordo com a especificação de requisitos
                NomeArquivo = Impressora.Codigo;
                NomeArquivo = NomeArquivo + Convert.ToDateTime(DataMovimento).ToString("ddmmyyyy");
                NomeArquivo = NomeArquivo + ".txt";

                FDataModule.ACBrPAF.Path = Application.StartupPath + "\\";
                FDataModule.ACBrPAF.SaveFileTXT_R(NomeArquivo);

                Mensagem = "Arquivo armazenado em: " + Application.StartupPath + "\\" + NomeArquivo;
                MessageBox.Show(Mensagem, "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }

        }


        //TODO:  Verifique se o procedimento abaixo está correto. Corrija o que for necessário.
        public static void MeiosPagamento(string pDataIni, string pDataFim)
        {
            string Meio, TipoDoc, Valor, Data, DataAnterior = "";
            if (MessageBox.Show("Deseja imprimir o relatório MEIOS DE PAGAMENTOS?", "Pergunta do Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    List<MeiosPagamentoVO> ListaMeiosPagamento = new TotalTipoPagamentoController().MeiosPagamento(pDataIni, pDataFim, FCaixa.Movimento.IdImpressora);

                    FDataModule.ACBrECF.AbreRelatorioGerencial(FCaixa.Configuracao.MeiosDePagamento);
                    FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));
                    FDataModule.ACBrECF.LinhaRelatorioGerencial("MEIOS DE PAGAMENTO");
                    FDataModule.ACBrECF.LinhaRelatorioGerencial("PERIODO: " + pDataIni + " A " + pDataFim);
                    FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));
                    FDataModule.ACBrECF.LinhaRelatorioGerencial("DT.ACUMUL. MEIO DE PGTO.   TIPO DOC. VLR.ACUMUL.");
                    FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));
                    for (int i = 0; i <= ListaMeiosPagamento.Count - 1; i++)
                    {
                        Data = ListaMeiosPagamento[i].DataHora.ToString("dd/MM/yyyy");
                        if (Data != DataAnterior)
                            FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('-', 48));
                        Meio = ListaMeiosPagamento[i].Descricao;
                        Meio = " " + Meio + new string(' ', 15 - Meio.Length);
                        if (ListaMeiosPagamento[i].Descricao != "SUPRIMENTO")
                            TipoDoc = " FISCAL  ";
                        else
                            TipoDoc = " NAO FISC";
                        Valor = ListaMeiosPagamento[i].Total.ToString("###,###,##0.00");
                        Valor = new string(' ', 13 - Valor.Length) + Valor;
                        FDataModule.ACBrECF.LinhaRelatorioGerencial(Data + Meio + TipoDoc + Valor);
                        DataAnterior = Data;
                    }
                    FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));
                    FDataModule.ACBrECF.LinhaRelatorioGerencial("TOTAIS ACUMULADOS NO PERIODO:");
                    FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));

                    ListaMeiosPagamento = new TotalTipoPagamentoController().MeiosPagamentoTotal(pDataIni, pDataFim, FCaixa.Movimento.IdImpressora);

                    for (int i = 0; i <= ListaMeiosPagamento.Count - 1; i++)
                    {
                        Meio = ListaMeiosPagamento[i].Descricao;
                        Meio = Meio + new string(' ', 18 - Meio.Length);
                        Valor = ListaMeiosPagamento[i].Total.ToString("###,###,##0.00");
                        Valor = new string(' ', 30 - Valor.Length) + Valor;
                        FDataModule.ACBrECF.LinhaRelatorioGerencial(Meio + Valor);
                    }

                    FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));
                    FDataModule.ACBrECF.FechaRelatorio();
                    GravaR06("RG");
                }
                catch (Exception eError)
                {
                    Log.write(eError.ToString());
                }
            }
        }


        //TODO:  Verifique se o procedimento abaixo está correto. Corrija o que for necessário.
        public static void ParametrodeConfiguracao()
        {
            try
            {
                XmlDocument ArquivoXML = new XmlDocument();
                ArquivoXML.Load(Application.StartupPath + "\\ArquivoAuxiliar.xml");

                FDataModule.ACBrECF.AbreRelatorioGerencial(FCaixa.Configuracao.ParametrosDeConfiguracao);
                FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));
                FDataModule.ACBrECF.LinhaRelatorioGerencial("***********PARAMETROS DE CONFIGURACAO***********");
                FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));
                FDataModule.ACBrECF.LinhaRelatorioGerencial("<n>CONFIGURAÇÃO:</n>");
                FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));

                FDataModule.ACBrECF.LinhaRelatorioGerencial("<s><n>Funcionalidades:</n></s>");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("TIPO DE FUNCIONAMENTO: ......... " + (Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("fun1").Item(0).InnerText.Trim())));
                FDataModule.ACBrECF.LinhaRelatorioGerencial("TIPO DE DESENVOLVIMENTO: ....... " + (Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("fun2").Item(0).InnerText.Trim())));
                FDataModule.ACBrECF.LinhaRelatorioGerencial("INTEGRACAO DO PAF-ECF: ......... " + (Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("fun3").Item(0).InnerText.Trim())));
                FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));

                FDataModule.ACBrECF.LinhaRelatorioGerencial("<s><n>Parâmetros Para Não Concomitância:</n></s>");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("PRÉ-VENDA: ................................. " + (Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("par1").Item(0).InnerText.Trim())));
                FDataModule.ACBrECF.LinhaRelatorioGerencial("DAV POR ECF: ............................... " + (Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("par2").Item(0).InnerText.Trim())));
                FDataModule.ACBrECF.LinhaRelatorioGerencial("DAV IMPRESSORA NÃO FISCAL: ................. " + (Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("par3").Item(0).InnerText.Trim())));
                FDataModule.ACBrECF.LinhaRelatorioGerencial("DAV-OS: .................................... " + (Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("par4").Item(0).InnerText.Trim())));
                FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));

                FDataModule.ACBrECF.LinhaRelatorioGerencial("<s><n>Aplicações Especiais:</n></s>");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("TAB. ÍNDICE TÉCNICO DE PRODUÇÃO: ........... " + (Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("apl1").Item(0).InnerText.Trim())));
                FDataModule.ACBrECF.LinhaRelatorioGerencial("POSTO REVENDEDOR DE COMBUSTÍVEIS: .......... " + (Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("apl2").Item(0).InnerText.Trim())));
                FDataModule.ACBrECF.LinhaRelatorioGerencial("Bar, Restaurante e Similar - ECF-Restaurante:" + (Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("apl3").Item(0).InnerText.Trim())));
                FDataModule.ACBrECF.LinhaRelatorioGerencial("Bar, Restaurante e Similar - ECF-Comum: .... " + (Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("apl4").Item(0).InnerText.Trim())));
                FDataModule.ACBrECF.LinhaRelatorioGerencial("FARMÁCIA DE MANIPULAÇÃO: ................... " + (Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("apl5").Item(0).InnerText.Trim())));
                FDataModule.ACBrECF.LinhaRelatorioGerencial("OFICINA DE CONSERTO: ....................... " + (Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("apl6").Item(0).InnerText.Trim())));
                FDataModule.ACBrECF.LinhaRelatorioGerencial("TRANSPORTE DE PASSAGEIROS: ................. " + (Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("apl7").Item(0).InnerText.Trim())));
                FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));

                FDataModule.ACBrECF.LinhaRelatorioGerencial("<s><n>Critérios por Unidade Federada:</n></s>");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("<n>REQUISITO XVIII - Tela Consulta de Preço:</n>");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("TOTALIZAÇÃO DOS VALORES DA LISTA: .......... " + (Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("cri1").Item(0).InnerText.Trim())));
                FDataModule.ACBrECF.LinhaRelatorioGerencial("TRANSFORMAÇÃO DAS INFORMÇÕES EM PRÉ-VENDA: . " + (Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("cri2").Item(0).InnerText.Trim())));
                FDataModule.ACBrECF.LinhaRelatorioGerencial("TRANSFORMAÇÃO DAS INFORMÇÕES EM DAV: ....... " + (Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("cri3").Item(0).InnerText.Trim())));
                FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));

                FDataModule.ACBrECF.LinhaRelatorioGerencial("<s><n>REQUISITO XXII - PAF-ECF Integrado ao ECF:</n></s>");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("NÃO COINCIDÊNCIA GT(ECF) x ARQUIVO CRIPTOGRAFADO");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("RECOMPOE VALOR DO GT ARQUIVO CRIPTOGRAFADO:  " + (Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("XXII1").Item(0).InnerText.Trim())));
                FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));

                FDataModule.ACBrECF.LinhaRelatorioGerencial("<s><n>REQUISITO XXXVI - A - PAF-ECF Combustível:</n></s>");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("Impedir Registro de Venda com Valor Zero ou");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("Negativo: .................................. " + (Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("XXXVI1").Item(0).InnerText.Trim())));
                FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));

                FDataModule.ACBrECF.FechaRelatorio();
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }



        public static void IdentificacaoPafEcf()
        {
            string sMD5, sSerie = "";

            try
            {
                XmlDocument ArquivoXML = new XmlDocument();
                ArquivoXML.Load(Application.StartupPath + "\\ArquivoAuxiliar.xml");

                R01VO R01 = new RegistroRController().RegistroR01();

                FDataModule.ACBrECF.AbreRelatorioGerencial(FCaixa.Configuracao.IdentificacaoPaf);
                FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));
                FDataModule.ACBrECF.LinhaRelatorioGerencial("************IDENTIFICACAO DO PAF-ECF************");
                FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));
                FDataModule.ACBrECF.LinhaRelatorioGerencial("NUMERO DO LAUDO...: " + R01.NumeroLaudoPaf);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("*****IDENTIFICACAO DA EMPRESA DESENVOLVEDORA****");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("C.N.P.J. .........: " + R01.CnpjSh);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("RAZAO SOCIAL......: " + R01.RazaoSocialSh);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("ENDERECO..........: " + R01.EnderecoSh);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("NUMERO............: " + R01.NumeroSh);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("COMPLEMENTO.......: " + R01.ComplementoSh);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("BAIRRO............: " + R01.BairroSh);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("CIDADE............: " + R01.CidadeSh);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("CEP...............: " + R01.CepSh);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("UF................: " + R01.UfSh);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("TELEFONE..........: " + R01.TelefoneSh);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("CONTATO...........: " + R01.ContatoSh);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("************IDENTIFICACAO DO PAF-ECF************");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("NOME COMERCIAL....: " + R01.NomePafEcf);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("VERSAO DO PAF-ECF.: " + R01.VersaoPafEcf);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("**********PRINCIPAL ARQUIVO EXECUTAVEL**********");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("NOME..............: " + R01.PrincipalExecutavel);
                sMD5 = Biblioteca.MD5File(Application.StartupPath + "\\" + R01.PrincipalExecutavel);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("MD5.: " + sMD5);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("****************DEMAIS ARQUIVOS*****************");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("NOME..............: " + "Balcao.exe");
                sMD5 = Biblioteca.MD5File(Application.StartupPath + "\\" + "Balcao.exe");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("MD5.: " + sMD5);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("**************NOME DO ARQUIVO TEXTO*************");
                FDataModule.ACBrECF.LinhaRelatorioGerencial("NOME..............: " + "ArquivoMD5.txt");
                sMD5 = Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("arquivosMD5").Item(0).InnerText.Trim());
                FDataModule.ACBrECF.LinhaRelatorioGerencial("MD5.: " + sMD5);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("VERSAO ER PAF-ECF.: " + R01.VersaoEr);
                FDataModule.ACBrECF.LinhaRelatorioGerencial("**********RELACAO DOS ECF AUTORIZADOS***********");
                sSerie = Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("serie").Item(0).InnerText.Trim());
                FDataModule.ACBrECF.LinhaRelatorioGerencial(new string('=', 48));
                FDataModule.ACBrECF.FechaRelatorio();
                GravaR06("RG");
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        public static bool ECFAutorizado()
        {
            string MD5Serie, Serie = "";

            if (!File.Exists(Application.StartupPath + "\\ArquivoAuxiliar.xml"))
            {
                return false;
            }
            else
            {
                try
                {
                    XmlDocument ArquivoXML = new XmlDocument();
                    ArquivoXML.Load(Application.StartupPath + "\\ArquivoAuxiliar.xml");
                    MD5Serie = FDataModule.ACBrECF.NumSerie;
                    Serie = Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("serie").Item(0).InnerText.Trim());
                    if (Serie == MD5Serie)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                catch (Exception eError)
                {
                    Log.write(eError.ToString());
                    return false;
                }
            }
        }


        public static bool ConfereGT()
        {
            string sGT;
            if (!File.Exists(Application.StartupPath + "\\ArquivoAuxiliar.xml"))
            {
                return false;
            }
            else
            {
                try
                {
                    XmlDocument ArquivoXML = new XmlDocument();
                    ArquivoXML.Load(Application.StartupPath + "\\ArquivoAuxiliar.xml");

                    sGT = Biblioteca.Desencripta(ArquivoXML.GetElementsByTagName("gt").Item(0).InnerText.Trim()); ;
                    if (FDataModule.ACBrECF.GrandeTotal.ToString() == sGT)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception eError)
                {
                    Log.write(eError.ToString());
                    return false;
                }
            }
        }


        public static void AtualizaGT()
        {
            try
            {
                XmlDocument ArquivoXML = new XmlDocument();
                ArquivoXML.Load(Application.StartupPath + "\\ArquivoAuxiliar.xml");
                ArquivoXML.GetElementsByTagName("gt").Item(0).InnerText = Biblioteca.Encripta(FDataModule.ACBrECF.GrandeTotal.ToString());
                ArquivoXML.Save(Application.StartupPath + "\\ArquivoAuxiliar.xml");
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        public static string GeraMD5()
        {
            string NomeArquivo, Mensagem, MD5ArquivoMD5;

            try
            {
                EmpresaVO Empresa = new EmpresaController().PegaEmpresa(FCaixa.Configuracao.IdEmpresa);

                //  registro N1
                FDataModule.ACBrPAF.PAF_N.RegistroN1.CNPJ = Empresa.Cnpj;
                FDataModule.ACBrPAF.PAF_N.RegistroN1.IE = Empresa.InscricaoEstadual;
                FDataModule.ACBrPAF.PAF_N.RegistroN1.IM = Empresa.Cnpj;
                FDataModule.ACBrPAF.PAF_N.RegistroN1.UF = Empresa.Uf;
                FDataModule.ACBrPAF.PAF_N.RegistroN1.RazaoSocial = Empresa.RazaoSocial;

                //  registro N2
                R01VO R01 = new RegistroRController().RegistroR01();

                FDataModule.ACBrPAF.PAF_N.RegistroN2.LAUDO = R01.NumeroLaudoPaf;
                FDataModule.ACBrPAF.PAF_N.RegistroN2.NOME = R01.NomePafEcf;
                FDataModule.ACBrPAF.PAF_N.RegistroN2.VERSAO = R01.VersaoPafEcf;
                FDataModule.ACBrPAF.PAF_N.RegistroN3.Clear();

                //  registros N3
                ACBrPAFRegistroN3 N3;
                NomeArquivo = Application.StartupPath + "\\PafEcf.exe";
                N3 = new ACBrPAFRegistroN3();
                N3.NOME_ARQUIVO = R01.PrincipalExecutavel;
                N3.MD5 = Biblioteca.MD5File(NomeArquivo);
                FDataModule.ACBrPAF.PAF_N.RegistroN3.Add(N3);

                NomeArquivo = Application.StartupPath + "\\Balcao.exe";
                N3 = new ACBrPAFRegistroN3();
                N3.NOME_ARQUIVO = "Balcao.exe";
                N3.MD5 = Biblioteca.MD5File(NomeArquivo);
                FDataModule.ACBrPAF.PAF_N.RegistroN3.Add(N3);

                FDataModule.ACBrPAF.Path = Application.StartupPath;
                FDataModule.ACBrPAF.SaveFileTXT_N(@"\ArquivoMD5.txt");

                MD5ArquivoMD5 = Biblioteca.MD5File(Application.StartupPath + "\\ArquivoMD5.txt");

                XmlDocument ArquivoXML = new XmlDocument();
                ArquivoXML.Load(Application.StartupPath + "\\ArquivoAuxiliar.xml");
                ArquivoXML.GetElementsByTagName("arquivosMD5").Item(0).InnerText = Biblioteca.Encripta(MD5ArquivoMD5);
                ArquivoXML.Save(Application.StartupPath + "\\ArquivoAuxiliar.xml");

                Mensagem = "Arquivo armazenado em: " + Application.StartupPath + "\\ArquivoMD5.txt";
                MessageBox.Show(Mensagem, "Informação do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return MD5ArquivoMD5;
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
                return null;
            }
        }


        public static void GravaR06(string pSimbolo)
        {
            try
            {
                R06VO R06 = new R06VO();
                R06.IdCaixa = FCaixa.Movimento.IdCaixa;
                R06.IdOperador = FCaixa.Movimento.IdOperador;
                R06.IdImpressora = FCaixa.Movimento.IdImpressora;
                R06.SerieEcf = FCaixa.Configuracao.NumSerieECF;
                R06.COO = Convert.ToInt32(FDataModule.ACBrECF.NumCOO);
                R06.GNF = Convert.ToInt32(FDataModule.ACBrECF.NumGNF);
                R06.GRG = Convert.ToInt32(FDataModule.ACBrECF.NumGRG);

                if (FDataModule.ACBrECF.MFD)
                    R06.CDC = Convert.ToInt32(FDataModule.ACBrECF.NumCDC);
                else
                    R06.CDC = 0;

                R06.Denominacao = pSimbolo;
                /*       Relação dos Símbolos Possíveis
                  Documento                        Símbolo
                  ========================================
                  Conferência de Mesa                 - CM
                  Registro de Venda                   - RV
                  Comprovante de Crédito ou Débito    - CC
                  Comprovante Não-Fiscal              - CN
                  Comprovante Não-Fiscal Cancelamento - NC
                  Relatório Gerencial                 - RG
                 */
                R06.DataEmissao = FDataModule.ACBrECF.DataHora;
                R06.HoraEmissao = FDataModule.ACBrECF.DataHora.ToString("HH:mm:ss");
                new RegistroRController().GravaR06(R06);
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


    }

}

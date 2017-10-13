/********************************************************************************
Title: T2TiPDV
Description: Funções e procedimentos do Sintegra;

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
using System.Windows.Forms;
using PafEcf.View;
using PafEcf.VO;
using PafEcf.Controller;
using System.Collections.Generic;
using ACBrFramework.Sintegra;
using PafEcf.Util;

namespace PafEcf.Infra
{

    public static class USintegra
    {

        public static string SerieImpressora, DataInicial, DataFinal;
        public static int CodigoConvenio, NaturezaInformacao, FinalidadeArquivo;
        public static EmpresaVO Empresa;

        //Registro mestre do estabelecimento, destinado à identificação do estabelecimento informante
        public static void GerarRegistro10()
        {
            try
            {
                Empresa = new EmpresaController().PegaEmpresa(FCaixa.Configuracao.IdEmpresa);
                FDataModule.ACBrSintegra.Registro10.CNPJ = Empresa.Cnpj;
                FDataModule.ACBrSintegra.Registro10.Inscricao = Empresa.InscricaoEstadual;
                FDataModule.ACBrSintegra.Registro10.RazaoSocial = Empresa.RazaoSocial;
                FDataModule.ACBrSintegra.Registro10.Cidade = Empresa.Cidade;
                FDataModule.ACBrSintegra.Registro10.Estado = Empresa.Uf;
                FDataModule.ACBrSintegra.Registro10.Telefone = Empresa.Fone;
                FDataModule.ACBrSintegra.Registro10.DataInicial = Convert.ToDateTime(DataInicial);
                FDataModule.ACBrSintegra.Registro10.DataFinal = Convert.ToDateTime(Convert.ToDateTime(DataFinal));
                FDataModule.ACBrSintegra.Registro10.CodigoConvenio = CodigoConvenio;
                FDataModule.ACBrSintegra.Registro10.NaturezaInformacoes = NaturezaInformacao;
                FDataModule.ACBrSintegra.Registro10.FinalidadeArquivo = FinalidadeArquivo;
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        //Dados complementares do informante
        public static void GerarRegistro11()
        {
            try
            {
                FDataModule.ACBrSintegra.Registro11.Endereco = Empresa.Logradouro;
                FDataModule.ACBrSintegra.Registro11.Numero = Empresa.Numero;
                FDataModule.ACBrSintegra.Registro11.Bairro = Empresa.Bairro;
                FDataModule.ACBrSintegra.Registro11.Cep = Empresa.Cep;
                FDataModule.ACBrSintegra.Registro11.Responsavel = Empresa.Contato;
                FDataModule.ACBrSintegra.Registro11.Telefone = Empresa.Fone;
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        //Registro destinado a informar as operações e prestações realizadas com os documentos
        //fiscais emitidos por equipamento emissor de cupom fiscal os quais são: Cupom Fiscal,
        //Cupom Fiscal - PDV, Bilhete de Passagem Rodoviário, modelo 13, Bilhete de Passagem
        //Aquaviário, modelo 14, Bilhete de Passagem e Nota de Bagagem, modelo 15, Bilhete
        //de Passagem Ferroviário, modelo 16, e Nota Fiscal de Venda a Consumidor, modelo 2;

        //60M-MESTRE    <------
        //60A-ANALITICO   <------
        //60D-DIARIO
        //60I-ITEM
        public static void GerarRegistro50()
        {
            try
            {
                List<SintegraVO> Lista50 = new SintegraController().Tabela50(DataInicial, DataFinal);
                SintegraRegistro50 Registro50;

                if (Lista50 != null)
                {
                    for (int i = 0; i <= Lista50.Count - 1; i++)
                    {
                        Registro50 = new SintegraRegistro50();
                        Registro50.CPFCNPJ = Lista50[i].CPFCNPJ;
                        Registro50.Inscricao = Lista50[i].Inscricao;
                        Registro50.DataDocumento = Lista50[i].DataDocumento;
                        Registro50.UF = Lista50[i].UF;
                        Registro50.ValorContabil = Lista50[i].ValorContabil;
                        Registro50.Modelo = Lista50[i].Modelo;
                        Registro50.Serie = Lista50[i].Serie;
                        Registro50.Numero = Lista50[i].Numero;
                        Registro50.Cfop = Lista50[i].Cfop;
                        Registro50.EmissorDocumento = Lista50[i].EmissorDocumento;
                        Registro50.BasedeCalculo = Lista50[i].BasedeCalculo;
                        Registro50.Icms = Lista50[i].Icms;
                        Registro50.Isentas = Lista50[i].Isentas;
                        Registro50.Outras = Lista50[i].Outras;
                        Registro50.Aliquota = Lista50[i].AliquotaICMS;
                        Registro50.Situacao = Lista50[i].Situacao;
                        FDataModule.ACBrSintegra.Registro50.Add(Registro50);
                        Registro50 = null;
                    }
                }
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        public static void GerarRegistro51()
        {
            try
            {
                List<SintegraVO> Lista51 = new SintegraController().Tabela51(DataInicial, DataFinal);
                SintegraRegistro51 Registro51;

                if (Lista51 != null)
                {
                    for (int i = 0; i <= Lista51.Count - 1; i++)
                    {
                        Registro51 = new SintegraRegistro51();
                        Registro51.CPFCNPJ = Lista51[i].CPFCNPJ;
                        Registro51.Inscricao = Lista51[i].Inscricao;
                        Registro51.DataDocumento = Lista51[i].DataDocumento;
                        Registro51.Estado = Lista51[i].UF;
                        Registro51.Serie = Lista51[i].Serie;
                        Registro51.Numero = Lista51[i].Numero;
                        Registro51.Cfop = Lista51[i].Cfop;
                        Registro51.ValorContabil = Lista51[i].ValorContabil;
                        Registro51.ValorIpi = Lista51[i].ValorIpi;
                        Registro51.ValorOutras = Lista51[i].ValorOutras;
                        Registro51.ValorIsentas = Lista51[i].ValorIsentas;
                        Registro51.Situacao = Lista51[i].Situacao;
                        FDataModule.ACBrSintegra.Registro51.Add(Registro51);
                        Registro51 = null;
                    }
                }
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        public static void GerarRegistro54()
        {
            try
            {
                SintegraController SintegraController = new SintegraController();
                List<SintegraVO> Lista50 = SintegraController.Tabela50(DataInicial, DataFinal);
                SintegraRegistro54 Registro54;
                SintegraRegistro75 Registro75;

                if (Lista50 != null)
                {
                    for (int i = 0; i <= Lista50.Count - 1; i++)
                    {
                        List<SintegraVO> Lista54 = SintegraController.Registro54e75(Convert.ToString(Lista50[i].Id));
                        for (int j = 0; j <= Lista54.Count - 1; j++)
                        {
                            Registro54 = new SintegraRegistro54();
                            Registro54.CPFCNPJ = Lista50[i].CPFCNPJ;
                            Registro54.Modelo = Lista50[i].Modelo;
                            Registro54.Serie = Lista50[i].Serie;
                            Registro54.Numero = Lista50[i].Numero;
                            Registro54.NumeroItem = Convert.ToInt32(Lista54[j].NumeroItem);
                            Registro54.Descricao = Lista54[j].Descricao;
                            Registro54.CST = Lista54[j].CST;
                            Registro54.Codigo = Lista54[j].Codigo;
                            Registro54.CFOP = Lista50[i].Cfop;
                            Registro54.Quantidade = Lista54[j].Quantidade;
                            Registro54.Valor = Lista54[j].Valor;
                            Registro54.ValorDescontoDespesa = Lista54[j].Despesas;
                            Registro54.BasedeCalculo = Lista54[j].BasedeCalculo;
                            Registro54.BaseST = Lista54[j].BaseST;
                            Registro54.ValorIpi = Lista54[j].ValorIpi;
                            Registro54.Aliquota = Lista54[j].AliquotaICMS;
                            FDataModule.ACBrSintegra.Registro54.Add(Registro54);
                            Registro54 = null;

                            Registro75 = new SintegraRegistro75();
                            Registro75.DataInicial = Convert.ToDateTime(DataInicial);
                            Registro75.DataFinal = Convert.ToDateTime(DataFinal);
                            Registro75.Codigo = Lista54[j].Codigo;
                            Registro75.NCM = Lista54[j].NCM;
                            Registro75.Descricao = Lista54[j].Descricao;
                            Registro75.Unidade = Lista54[j].Unidade;
                            Registro75.AliquotaIpi = Lista54[j].AliquotaIpi;
                            Registro75.AliquotaICMS = Lista54[j].AliquotaICMS;
                            Registro75.Reducao = Lista54[j].Reducao;
                            Registro75.BaseST = Lista54[j].BaseST;
                            FDataModule.ACBrSintegra.Registro75.Add(Registro75);
                            Registro75 = null;
                        }
                    }
                }
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        public static void GerarRegistro60M()
        {
            try
            {
                SintegraController SintegraController = new SintegraController();
                List<Sintegra60MVO> Lista60M = SintegraController.Tabela60M(DataInicial, DataFinal);
                List<Sintegra60AVO> Lista60A;
                SintegraRegistro60M Registro60M;
                SintegraRegistro60A Registro60A;

                if (Lista60M.Count > 0)
                {
                    SerieImpressora = Lista60M[0].SerieImpressora;

                    for (int i = 0; i <= Lista60M.Count - 1; i++)
                    {
                        Registro60M = new SintegraRegistro60M();
                        Registro60M.Emissao = Convert.ToDateTime(Lista60M[i].DataEmissao);
                        Registro60M.NumSerie = Lista60M[i].SerieImpressora;
                        Registro60M.NumOrdem = Lista60M[i].NumeroEquipamento;
                        if ((Lista60M[i].ModeloDocumentoFiscal) == "")
                            Registro60M.ModeloDoc = "2D";
                        else
                            Registro60M.ModeloDoc = Lista60M[i].ModeloDocumentoFiscal;
                        Registro60M.CooInicial = Lista60M[i].COOInicial;
                        Registro60M.CooFinal = Lista60M[i].COOFinal;
                        Registro60M.CRZ = Lista60M[i].CRZ;
                        Registro60M.CRO = Lista60M[i].CRO;
                        Registro60M.VendaBruta = Lista60M[i].VendaBruta;
                        Registro60M.ValorGT = Lista60M[i].GrandeTotal;

                        FDataModule.ACBrSintegra.Registro60M.Add(Registro60M);

                        Lista60A = SintegraController.Tabela60A(Lista60M[i].Id);
                        if (Lista60A != null)
                        {
                            for (int j = 0; j <= Lista60A.Count - 1; j++)
                            {
                                Registro60A = new SintegraRegistro60A();
                                Registro60A.Emissao = Registro60M.Emissao;
                                Registro60A.NumSerie = Lista60M[i].SerieImpressora;

                                Registro60A.Aliquota = Lista60A[j].SituacaoTributaria;


                                Registro60A.Valor = Lista60A[j].Valor;
                                FDataModule.ACBrSintegra.Registro60A.Add(Registro60A);
                                Registro60A = null;
                            }
                        }
                        Registro60M = null;
                    }
                }
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        //Registro destinado a informar as operações e prestações realizadas com os documentos
        //fiscais emitidos por equipamento emissor de cupom fiscal os quais são: Cupom Fiscal,
        //Cupom Fiscal - PDV, Bilhete de Passagem Rodoviário, modelo 13, Bilhete de Passagem
        //Aquaviário, modelo 14, Bilhete de Passagem e Nota de Bagagem, modelo 15, Bilhete
        //de Passagem Ferroviário, modelo 16, e Nota Fiscal de Venda a Consumidor, modelo 2;

        //60M-MESTRE
        //60A-ANALITICO
        //60D-DIARIO    <------
        //60I-ITEM
        public static void GerarRegistro60D()
        {
            try
            {
                SintegraController SintegraController = new SintegraController();
                SintegraRegistro60D Registro60D;
                SintegraRegistro75 Registro75;
                List<Sintegra60DVO> Lista60D = SintegraController.Tabela60D(DataInicial, DataFinal);
                ProdutoVO Produto;

                if (Lista60D.Count > 0)
                {
                    for (int i = 0; i <= Lista60D.Count - 1; i++)
                    {
                        Registro60D = new SintegraRegistro60D();
                        Registro60D.Emissao = Convert.ToDateTime(Lista60D[i].DataEmissao);
                        Registro60D.NumSerie = Lista60D[i].SerieECF;
                        Registro60D.Codigo = Lista60D[i].GTIN;
                        Registro60D.Quantidade = Lista60D[i].SomaQuantidade;
                        Registro60D.Valor = Lista60D[i].SomaValor;
                        Registro60D.BaseDeCalculo = Lista60D[i].SomaBaseICMS;
                        Registro60D.StAliquota = Lista60D[i].SituacaoTributaria;
                        Registro60D.ValorIcms = Lista60D[i].SomaValorICMS;

                        FDataModule.ACBrSintegra.Registro60D.Add(Registro60D);

                        Produto = new ProdutoController().Consulta(Registro60D.Codigo, 2);

                        Registro75 = new SintegraRegistro75();
                        Registro75.DataInicial = FDataModule.ACBrSintegra.Registro10.DataInicial;
                        Registro75.DataFinal = FDataModule.ACBrSintegra.Registro10.DataFinal;
                        Registro75.Codigo = Registro60D.Codigo;
                        if (Produto == null)
                        {
                            Registro75.NCM = "nulo?";
                            Registro75.Descricao = "nulo?";
                            Registro75.Unidade = "nulo?";
                        }
                        else
                        {
                            Registro75.NCM = Produto.NCM;
                            Registro75.Descricao = Produto.Descricao;
                            Registro75.Unidade = Produto.UnidadeProduto;
                        }
                        Registro75.AliquotaIpi = 0;
                        Registro75.AliquotaICMS = 0;
                        Registro75.Reducao = 0;
                        Registro75.BaseST = 0;
                        FDataModule.ACBrSintegra.Registro75.Add(Registro75);
                        Registro60D = null;
                        Registro75 = null;
                    }
                }
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        //Registro destinado a informar as operações e prestações realizadas com os documentos
        //fiscais emitidos por equipamento emissor de cupom fiscal os quais são: Cupom Fiscal,
        //Cupom Fiscal - PDV, Bilhete de Passagem Rodoviário, modelo 13, Bilhete de Passagem
        //Aquaviário, modelo 14, Bilhete de Passagem e Nota de Bagagem, modelo 15, Bilhete
        //de Passagem Ferroviário, modelo 16, e Nota Fiscal de Venda a Consumidor, modelo 2;

        //60M-MESTRE
        //60A-ANALITICO
        //60D-DIARIO
        //60I-ITEM
        //60R-Mensal    <------
        public static void GerarRegistro60R()
        {
            try
            {
                SintegraController SintegraController = new SintegraController();
                SintegraRegistro60R Registro60R;
                SintegraRegistro75 Registro75;
                List<Sintegra60RVO> Lista60R = SintegraController.Tabela60R(DataInicial, DataFinal);
                ProdutoVO Produto;

                for (int i = 0; i <= Lista60R.Count - 1; i++)
                {
                    Registro60R = new SintegraRegistro60R();
                    Registro60R.MesAno = Lista60R[i].MesEmissao + Lista60R[i].AnoEmissao;
                    Registro60R.Codigo = Lista60R[i].GTIN;
                    Registro60R.Qtd = Lista60R[i].SomaQuantidade;
                    Registro60R.Valor = Lista60R[i].SomaValor;
                    Registro60R.BaseDeCalculo = Lista60R[i].SomaBaseICMS;
                    Registro60R.Aliquota = Lista60R[i].SituacaoTributaria;

                    FDataModule.ACBrSintegra.Registro60R.Add(Registro60R);

                    Produto = new ProdutoController().Consulta(Registro60R.Codigo, 2);

                    Registro75 = new SintegraRegistro75();
                    Registro75.DataInicial = FDataModule.ACBrSintegra.Registro10.DataInicial;
                    Registro75.DataFinal = FDataModule.ACBrSintegra.Registro10.DataFinal;
                    Registro75.Codigo = Registro60R.Codigo;
                    if (Produto == null)
                    {
                        Registro75.NCM = "nulo?";
                        Registro75.Descricao = "nulo?";
                        Registro75.Unidade = "nulo?";
                    }
                    else
                    {
                        Registro75.NCM = Produto.NCM;
                        Registro75.Descricao = Produto.Descricao;
                        Registro75.Unidade = Produto.UnidadeProduto;
                    }
                    Registro75.AliquotaIpi = 0;
                    Registro75.AliquotaICMS = 0;
                    Registro75.Reducao = 0;
                    Registro75.BaseST = 0;
                    FDataModule.ACBrSintegra.Registro75.Add(Registro75);
                    Registro60R = null;
                    Registro75 = null;
                }
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }



        //Para os documentos fiscais descritos a seguir, quando não emitidos por equipamento
        //emissor de cupom fiscal : Bilhete de Passagem Aquaviário (modelo 14), Bilhete de
        //Passagem e Nota de Bagagem (modelo 15), Bilhete de Passagem Ferroviário (modelo 16),
        //Bilhete de Passagem Rodoviário (modelo 13) e Nota Fiscal de Venda a Consumidor
        //(modelo 2), Nota Fiscal de Produtor (modelo 4), para as unidades da Federa??o
        //que não o exigirem na forma prevista no item 11.
        //
        public static void GerarRegistro61()
        {
            try
            {
                SintegraRegistro61 Registro61;
                List<NotaFiscalCabecalhoVO> ListaNF2Cabecalho = new NotaFiscalController().ConsultaNFCabecalhoSPED(DataInicial, DataFinal);
                if (ListaNF2Cabecalho != null)
                {
                    for (int i = 0; i <= ListaNF2Cabecalho.Count - 1; i++)
                    {
                        Registro61 = new SintegraRegistro61();
                        Registro61.Emissao = Convert.ToDateTime(ListaNF2Cabecalho[i].DataEmissao);
                        Registro61.Modelo = "02";

                        Registro61.NumOrdemInicial = ListaNF2Cabecalho[i].NumOrdemInicial;
                        Registro61.NumOrdemFinal = ListaNF2Cabecalho[i].NumOrdemFinal;

                        Registro61.Serie = ListaNF2Cabecalho[i].Serie;
                        Registro61.SubSerie = ListaNF2Cabecalho[i].Subserie;
                        Registro61.Valor = ListaNF2Cabecalho[i].TotalNf;
                        Registro61.BaseDeCalculo = ListaNF2Cabecalho[i].BaseIcms;
                        Registro61.ValorIcms = ListaNF2Cabecalho[i].Icms;
                        Registro61.Outras = ListaNF2Cabecalho[i].IcmsOutras;
                        FDataModule.ACBrSintegra.Registro61.Add(Registro61);
                        Registro61 = null;
                    }
                }
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        //Registro de mercadoria/produto ou serviço comercializados através de Nota Fiscal
        //de Produtor ou Nota Fiscal de Venda a Consumidor não emitida por ECF.
        public static void GerarRegistro61R()
        {
            try
            {
                SintegraController SintegraController = new SintegraController();
                SintegraRegistro61R Registro61R;
                SintegraRegistro75 Registro75;
                List<Sintegra61RVO> Lista61R = SintegraController.Tabela61R(DataInicial, DataFinal);
                ProdutoVO Produto;

                if (Lista61R != null)
                {
                    for (int i = 0; i <= Lista61R.Count - 1; i++)
                    {
                        Registro61R = new SintegraRegistro61R();
                        Registro61R.MesAno = Lista61R[i].MesEmissao + Lista61R[i].AnoEmissao;
                        Registro61R.Codigo = Lista61R[i].GTIN;
                        Registro61R.Qtd = Lista61R[i].SomaQuantidade;
                        Registro61R.Valor = Lista61R[i].SomaValor;
                        Registro61R.BaseDeCalculo = Lista61R[i].SomaBaseICMS;

                        try
                        {
                            Registro61R.Aliquota = Convert.ToDecimal(Lista61R[i].SituacaoTributaria) / 100;
                        }
                        catch (Exception)
                        {
                            Registro61R.Aliquota = 0;
                        }

                        FDataModule.ACBrSintegra.Registro61R.Add(Registro61R);

                        Produto = new ProdutoController().Consulta(Registro61R.Codigo, 2);

                        Registro75 = new SintegraRegistro75();
                        Registro75.DataInicial = FDataModule.ACBrSintegra.Registro10.DataInicial;
                        Registro75.DataFinal = FDataModule.ACBrSintegra.Registro10.DataFinal;
                        Registro75.Codigo = Registro61R.Codigo;
                        if (Produto == null)
                        {
                            Registro75.NCM = "nulo?";
                            Registro75.Descricao = "nulo?";
                            Registro75.Unidade = "nulo?";
                        }
                        else
                        {
                            Registro75.NCM = Produto.NCM;
                            Registro75.Descricao = Produto.Descricao;
                            Registro75.Unidade = Produto.UnidadeProduto;
                        }
                        Registro75.AliquotaIpi = 0;
                        Registro75.AliquotaICMS = 0;
                        Registro75.Reducao = 0;
                        Registro75.BaseST = 0;
                        FDataModule.ACBrSintegra.Registro75.Add(Registro75);
                        Registro61R = null;
                        Registro75 = null;
                    }
                }
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        public static void GerarArquivoSintegra(string pDataIni, string pDataFim, int pCodigoConvenio, int pNaturezaInformacao, int pFinalidadeArquivo)
        {
            try
            {
                string Mensagem, Arquivo;
                CodigoConvenio = pCodigoConvenio;
                NaturezaInformacao = pNaturezaInformacao;
                FinalidadeArquivo = pFinalidadeArquivo;
                DataInicial = pDataIni;
                DataFinal = pDataFim;

                Arquivo = Application.StartupPath + "\\" + FCaixa.Configuracao.Laudo + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".txt";

                FDataModule.ACBrSintegra.FileName = Arquivo;
                FDataModule.ACBrSintegra.VersaoValidador = ACBrFramework.Sintegra.VersaoValidador.V524;
                GerarRegistro10();
                GerarRegistro11();
                //TODO:  Implemente a NF-e na sua retaguarda para ativar a chamada aos métodos abaixo
                //GerarRegistro50();
                //GerarRegistro54();
                
                //GerarRegistro60M();
                //GerarRegistro60D();
                //GerarRegistro60R();
                
                GerarRegistro61();
                GerarRegistro61R();
                FDataModule.ACBrSintegra.GeraArquivo();

                //FDataModule.ACBrPAF.AssinarArquivoComEAD(Arquivo);
                Biblioteca.AssinarComOpenSsl(Arquivo);

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
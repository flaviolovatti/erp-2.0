/********************************************************************************
Title: T2TiPDV
Description: Classe de controle da venda

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
using MySql.Data.MySqlClient;
using PafEcf.Infra;
using PafEcf.Util;
using PafEcf.VO;
using System.Collections.Generic;
using PafEcf.View;

namespace PafEcf.Controller
{


    public class VendaController
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public VendaController()
        {
            conexao = dbConnection.conectar();
        }


        public VendaCabecalhoVO IniciaVenda(VendaCabecalhoVO pVendaCabecalho)
        {

            ConsultaSQL =
              "insert into ECF_VENDA_CABECALHO (" +
              " STATUS_VENDA," +
              " ID_ECF_MOVIMENTO," +
              " ID_ECF_PRE_VENDA_CABECALHO," +
              " SERIE_ECF," +
              " CFOP," +
              " COO," +
              " CCF," +
              " ID_CLIENTE," +
              " NOME_CLIENTE," +
              " CPF_CNPJ_CLIENTE," +
              " DATA_VENDA," +
              " HORA_VENDA) values (" +
              " ?pStatus," +
              " ?pMovimento," +
              " ?pIdPreVenda," +
              " ?pSerieEcf," +
              " ?pCFOP," +
              " ?pCOO," +
              " ?pCCF," +
              " ?pIdCliente," +
              " ?pNomeCliente," +
              " ?pCPFouCNPJCliente," +
              " ?pDataVenda," +
              " ?pHoraVenda)";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pStatus", pVendaCabecalho.StatusVenda);
                comando.Parameters.AddWithValue("?pMovimento", pVendaCabecalho.IdMovimento);
                comando.Parameters.AddWithValue("?pSerieEcf", FCaixa.Configuracao.NumSerieECF);
                comando.Parameters.AddWithValue("?pCFOP", pVendaCabecalho.CFOP);
                comando.Parameters.AddWithValue("?pCOO", pVendaCabecalho.COO);
                comando.Parameters.AddWithValue("?pCCF", pVendaCabecalho.CCF);
                comando.Parameters.AddWithValue("?pIdCliente", pVendaCabecalho.IdCliente);
                comando.Parameters.AddWithValue("?pIdPreVenda", pVendaCabecalho.IdPreVenda);
                comando.Parameters.AddWithValue("?pNomeCliente", pVendaCabecalho.NomeCliente);
                comando.Parameters.AddWithValue("?pCPFouCNPJCliente", pVendaCabecalho.CPFouCNPJCliente);
                comando.Parameters.AddWithValue("?pDataVenda", pVendaCabecalho.DataVenda);
                comando.Parameters.AddWithValue("?pHoraVenda", pVendaCabecalho.HoraVenda);
                comando.ExecuteNonQuery();

                ConsultaSQL = "select max(ID) as ID from ECF_VENDA_CABECALHO";
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();

                pVendaCabecalho.Id = Convert.ToInt32(leitor["ID"]);
                return pVendaCabecalho;
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


        public VendaDetalheVO InserirItem(VendaDetalheVO pVendaDetalhe)
        {
            string Tripa, Hash;
            int Ccf, Coo;

            ConsultaSQL =
              "insert into ECF_VENDA_DETALHE (" +
              " CFOP," +
              " ID_ECF_PRODUTO," +
              " ID_ECF_VENDA_CABECALHO," +
              " SERIE_ECF," +
              " GTIN," +
              " CCF," +
              " COO," +
              " ITEM," +
              " QUANTIDADE," +
              " VALOR_UNITARIO," +
              " VALOR_TOTAL," +
              " TOTAL_ITEM," +
              " TOTALIZADOR_PARCIAL," +
              " ECF_ICMS_ST," +
              " CST," +
              " HASH_TRIPA," +
              " CANCELADO," +
              " TAXA_ICMS," +
              " TAXA_ISSQN," +
              " MOVIMENTA_ESTOQUE) values (" +
              " ?pCFOP," +
              " ?pIdProduto," +
              " ?pIdVendaCabecalho," +
              " ?pSerieEcf," +
              " ?pGtin," +
              " ?pCcf," +
              " ?pCoo," +
              " ?pItem," +
              " ?pQuantidade," +
              " ?pValorUnitario," +
              " ?pValorTotal," +
              " ?pTotalItem," +
              " ?pTotalizadorParcial," +
              " ?pECFIcmsST," +
              " ?pCST," +
              " ?pHash," +
              " ?pCancelado," +
              " ?pTaxaICMS," +
              " ?pTaxaISSQN," +
              " ?pMovimentaEstoque)";

            try
            {
                Ccf = Convert.ToInt32(FDataModule.ACBrECF.NumCCF);
                Coo = Convert.ToInt32(FDataModule.ACBrECF.NumCOO);

                // calcula e grava o hash
                Tripa =
                         FCaixa.Configuracao.NumSerieECF +
                         Convert.ToString(Coo) +
                         Convert.ToString(Ccf) +
                         pVendaDetalhe.GTIN +
                         Biblioteca.FormataFloat("Q", pVendaDetalhe.Quantidade) +
                         Biblioteca.FormataFloat("V", pVendaDetalhe.ValorUnitario) +
                         Biblioteca.FormataFloat("V", pVendaDetalhe.TotalItem) +
                         pVendaDetalhe.TotalizadorParcial +
                         "N" +
                         "0";
                Hash = Biblioteca.MD5String(Tripa);

                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pHash", Hash);
                comando.Parameters.AddWithValue("?pCFOP", pVendaDetalhe.CFOP);
                comando.Parameters.AddWithValue("?pIdProduto", pVendaDetalhe.IdProduto);
                comando.Parameters.AddWithValue("?pIdVendaCabecalho", pVendaDetalhe.IdVendaCabecalho);
                comando.Parameters.AddWithValue("?pSerieEcf", FCaixa.Configuracao.NumSerieECF);
                comando.Parameters.AddWithValue("?pGtin", pVendaDetalhe.GTIN);
                comando.Parameters.AddWithValue("?pCcf", Ccf);
                comando.Parameters.AddWithValue("?pCoo", Coo);
                comando.Parameters.AddWithValue("?pItem", pVendaDetalhe.Item);
                comando.Parameters.AddWithValue("?pQuantidade", pVendaDetalhe.Quantidade);
                comando.Parameters.AddWithValue("?pValorUnitario", pVendaDetalhe.ValorUnitario);
                comando.Parameters.AddWithValue("?pValorTotal", pVendaDetalhe.ValorTotal);
                comando.Parameters.AddWithValue("?pTotalItem", pVendaDetalhe.ValorTotal);
                comando.Parameters.AddWithValue("?pTotalizadorParcial", pVendaDetalhe.TotalizadorParcial);
                if (pVendaDetalhe.ECFICMS == "NN")
                    comando.Parameters.AddWithValue("?pECFIcmsST", "N");
                else if (pVendaDetalhe.ECFICMS == "FF")
                    comando.Parameters.AddWithValue("?pECFIcmsST", "F");
                else if (pVendaDetalhe.ECFICMS == "II")
                    comando.Parameters.AddWithValue("?pECFIcmsST", "I");
                else
                {
                    if (pVendaDetalhe.TotalizadorParcial.Substring(1, 3) == "S")
                        comando.Parameters.AddWithValue("?pECFIcmsST", pVendaDetalhe.TotalizadorParcial.Substring(4, 4));
                    else
                        if (pVendaDetalhe.TotalizadorParcial.Substring(1, 3) == "T")
                            comando.Parameters.AddWithValue("?pECFIcmsST", pVendaDetalhe.TotalizadorParcial.Substring(4, 4));
                        else
                            if (pVendaDetalhe.TotalizadorParcial == "Can-T")
                                comando.Parameters.AddWithValue("?pECFIcmsST", "CANC");
                            else
                            {
                                comando.Parameters.AddWithValue("?pECFIcmsST", "1700");
                            }
                }
                comando.Parameters.AddWithValue("?pTaxaISSQN", pVendaDetalhe.TaxaISSQN);
                comando.Parameters.AddWithValue("?pCST", pVendaDetalhe.CST);
                comando.Parameters.AddWithValue("?pMovimentaEstoque", pVendaDetalhe.MovimentaEstoque);
                comando.Parameters.AddWithValue("?pCancelado", "N");

                if ((pVendaDetalhe.ECFICMS != "II") && (pVendaDetalhe.ECFICMS != "NN"))
                    comando.Parameters.AddWithValue("?pTaxaICMS", pVendaDetalhe.TaxaICMS);
                else
                    comando.Parameters.AddWithValue("?pTaxaICMS", 0);
                comando.ExecuteNonQuery();

                ConsultaSQL = "select max(ID) as ID from ECF_VENDA_DETALHE";
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();

                pVendaDetalhe.Id = Convert.ToInt32(leitor["ID"]);
                return pVendaDetalhe;
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


        public void EncerraVenda(VendaCabecalhoVO pVendaCabecalho)
        {
            string Tripa, Hash;

            pVendaCabecalho = CalculaImpostos(pVendaCabecalho);

            ConsultaSQL =
              "update ECF_VENDA_CABECALHO set " +
              "VALOR_VENDA=?pValorVenda, " +
              "TOTAL_PRODUTOS=?pValorVenda, " +
              "TOTAL_DOCUMENTO=?pValorVenda, " +
              "BASE_ICMS=?pBaseICMS, " +
              "ICMS_OUTRAS=?pOutrasICMS, " +
              "ICMS=?pICMS, " +
              "ISSQN=?pISSQN, " +
              "VALOR_FINAL=?pValorFinal, " +
              "VALOR_RECEBIDO=?pValorRecebido, " +
              "TAXA_DESCONTO=?pTaxaDesconto, " +
              "DESCONTO=?pDesconto, " +
              "TAXA_ACRESCIMO=?pTaxaAcrescimo, " +
              "ACRESCIMO=?pAcrescimo, " +
              "TROCO=?pTroco, " +
              "ID_ECF_DAV=?pDav, " +
              "ID_ECF_PRE_VENDA_CABECALHO=?pPreVenda, " +
              "STATUS_VENDA=?pStatus, " +
              "ID_ECF_FUNCIONARIO=?pVendedor, " +
              "CUPOM_CANCELADO=?pCupomFoiCancelado, " +
              "HASH_INCREMENTO=?pHashIncremento, " +
              "HASH_TRIPA=?pHash " +
              " where ID = ?pId";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pId", pVendaCabecalho.Id);
                comando.Parameters.AddWithValue("?pValorVenda", pVendaCabecalho.ValorVenda);
                comando.Parameters.AddWithValue("?pValorFinal", pVendaCabecalho.ValorFinal);
                comando.Parameters.AddWithValue("?pValorRecebido", pVendaCabecalho.ValorRecebido);
                comando.Parameters.AddWithValue("?pTaxaDesconto", pVendaCabecalho.TaxaDesconto);
                comando.Parameters.AddWithValue("?pDesconto", pVendaCabecalho.Desconto);
                comando.Parameters.AddWithValue("?pTaxaAcrescimo", pVendaCabecalho.TaxaAcrescimo);
                comando.Parameters.AddWithValue("?pAcrescimo", pVendaCabecalho.Acrescimo);
                comando.Parameters.AddWithValue("?pTroco", pVendaCabecalho.Troco);
                comando.Parameters.AddWithValue("?pStatus", pVendaCabecalho.StatusVenda);
                comando.Parameters.AddWithValue("?pCupomFoiCancelado", pVendaCabecalho.CupomFoiCancelado);
                comando.Parameters.AddWithValue("?pBaseICMS", pVendaCabecalho.BaseICMS);
                comando.Parameters.AddWithValue("?pOutrasICMS", pVendaCabecalho.ICMSOutras);
                comando.Parameters.AddWithValue("?pICMS", pVendaCabecalho.ICMS);
                comando.Parameters.AddWithValue("?pISSQN", pVendaCabecalho.ISSQN);

                // calcula e grava o hash
                Tripa = Convert.ToString(pVendaCabecalho.Id) +
                         Convert.ToString(pVendaCabecalho.CCF) +
                         Convert.ToString(pVendaCabecalho.COO) +
                         Biblioteca.FormataFloat("V", pVendaCabecalho.ValorFinal) +
                        FCaixa.Configuracao.NumSerieECF +
                         pVendaCabecalho.StatusVenda +
                         pVendaCabecalho.CupomFoiCancelado +
                         "0";
                Hash = Biblioteca.MD5String(Tripa);

                comando.Parameters.AddWithValue("?pHash", Hash);
                comando.Parameters.AddWithValue("?pHashIncremento", -1);
                comando.Parameters.AddWithValue("?pVendedor", pVendaCabecalho.IdVendedor);

                comando.Parameters.AddWithValue("?pPreVenda", pVendaCabecalho.IdPreVenda);
                if (pVendaCabecalho.IdPreVenda != null)
                {
                    new PreVendaController().FechaPreVenda(pVendaCabecalho.IdPreVenda, pVendaCabecalho.CCF);
                }

                comando.Parameters.AddWithValue("?pDav", pVendaCabecalho.IdDAV);
                if (pVendaCabecalho.IdDAV != null)
                {
                    new DAVController().FechaDAV(pVendaCabecalho.IdDAV, pVendaCabecalho.CCF, pVendaCabecalho.COO);
                }
                comando.ExecuteNonQuery();
            }

            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
            finally
            {
                if (leitor != null)
                    leitor.Close();
            }
        }


        public VendaCabecalhoVO CalculaImpostos(VendaCabecalhoVO pVendaCabecalho)
        {
            decimal BaseICMSCabecalho, OutrasICMSCabecalho, ICMSCabecalho, ISSCabecalho;
            decimal DescontoRateio, AcrescimoRateio, ResiduoDesconto, ResiduoAcrescimo;

            BaseICMSCabecalho = 0;
            OutrasICMSCabecalho = 0;
            ICMSCabecalho = 0;
            ISSCabecalho = 0;
            DescontoRateio = 0;
            AcrescimoRateio = 0;
            ResiduoDesconto = 0;
            ResiduoAcrescimo = 0;

            try
            {
                // seleciona os registros de detalhes da venda pertencentes a venda atual
                ConsultaSQL =
                  "select VD.*, P.GTIN " +
                  "from ECF_VENDA_DETALHE VD, PRODUTO P " +
                  "where VD.ID_ECF_PRODUTO = P.ID " +
                  " and CANCELADO = " + Biblioteca.QuotedStr("N") +
                  " and ID_ECF_VENDA_CABECALHO=" + Convert.ToString(pVendaCabecalho.Id);

                List<VendaDetalheVO> ListaVendaDetalhe = new List<VendaDetalheVO>();

                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                // popula a lista de detalhes da venda com os registros retornados
                while (leitor.Read())
                {
                    VendaDetalheVO VendaDetalhe = new VendaDetalheVO();
                    VendaDetalhe.Id = Convert.ToInt32(leitor["ID"]);
                    VendaDetalhe.CFOP = Convert.ToInt32(leitor["CFOP"]);
                    VendaDetalhe.GTIN = Convert.ToString(leitor["GTIN"]);
                    VendaDetalhe.IdProduto = Convert.ToInt32(leitor["ID_ECF_PRODUTO"]);
                    VendaDetalhe.Quantidade = Convert.ToInt32(leitor["QUANTIDADE"]);
                    VendaDetalhe.ValorUnitario = Convert.ToDecimal(leitor["VALOR_UNITARIO"]);
                    VendaDetalhe.ValorTotal = Convert.ToDecimal(leitor["VALOR_TOTAL"]);
                    VendaDetalhe.TotalizadorParcial = Convert.ToString(leitor["TOTALIZADOR_PARCIAL"]);
                    VendaDetalhe.TaxaICMS = Convert.ToDecimal(leitor["TAXA_ICMS"]);
                    VendaDetalhe.TaxaISSQN = Convert.ToDecimal(leitor["TAXA_ISSQN"]);
                    ListaVendaDetalhe.Add(VendaDetalhe);
                }

                // laco para fazer os devidos calculos de impostos e rateio de desconto/acrescimo
                for (int i = 0; i <= ListaVendaDetalhe.Count - 1; i++)
                {
                    ListaVendaDetalhe[i].DescontoRateio = 0;
                    ListaVendaDetalhe[i].AcrescimoRateio = 0;
                    if (pVendaCabecalho.Desconto > 0)
                    {
                        ListaVendaDetalhe[i].DescontoRateio = Biblioteca.TruncaValor(ListaVendaDetalhe[i].ValorTotal * pVendaCabecalho.Desconto / pVendaCabecalho.ValorVenda, Constantes.DECIMAIS_VALOR);
                        DescontoRateio = DescontoRateio + ListaVendaDetalhe[i].DescontoRateio;
                    }
                    else if (pVendaCabecalho.Acrescimo > 0)
                    {
                        ListaVendaDetalhe[i].AcrescimoRateio = Biblioteca.TruncaValor(ListaVendaDetalhe[i].ValorTotal * pVendaCabecalho.Acrescimo / pVendaCabecalho.ValorVenda, Constantes.DECIMAIS_VALOR);
                        AcrescimoRateio = AcrescimoRateio + ListaVendaDetalhe[i].AcrescimoRateio;
                    }

                    ListaVendaDetalhe[i].TotalItem = Biblioteca.TruncaValor(ListaVendaDetalhe[i].ValorTotal - ListaVendaDetalhe[i].DescontoRateio + ListaVendaDetalhe[i].AcrescimoRateio, Constantes.DECIMAIS_VALOR);
                    ListaVendaDetalhe[i].BaseICMS = Biblioteca.TruncaValor(ListaVendaDetalhe[i].ValorTotal - ListaVendaDetalhe[i].DescontoRateio + ListaVendaDetalhe[i].AcrescimoRateio, Constantes.DECIMAIS_VALOR);
                    ListaVendaDetalhe[i].ICMS = Biblioteca.TruncaValor(ListaVendaDetalhe[i].BaseICMS * ListaVendaDetalhe[i].TaxaICMS / 100, Constantes.DECIMAIS_VALOR);
                    ListaVendaDetalhe[i].ISSQN = Biblioteca.TruncaValor(ListaVendaDetalhe[i].BaseICMS * ListaVendaDetalhe[i].TaxaISSQN / 100, Constantes.DECIMAIS_VALOR);
                    if ((ListaVendaDetalhe[i].TotalizadorParcial == "N1") || (ListaVendaDetalhe[i].TotalizadorParcial == "F1"))
                        OutrasICMSCabecalho = OutrasICMSCabecalho + ListaVendaDetalhe[i].ValorTotal;
                    else
                        BaseICMSCabecalho = BaseICMSCabecalho + ListaVendaDetalhe[i].ValorTotal;
                    ICMSCabecalho = ICMSCabecalho + ListaVendaDetalhe[i].ICMS;
                    ISSCabecalho = ISSCabecalho + ListaVendaDetalhe[i].ISSQN;
                }

                // armazena as informacoes de impostos de cabecalho para retornar para o metodo que fecha a venda
                pVendaCabecalho.BaseICMS = BaseICMSCabecalho;
                pVendaCabecalho.ICMSOutras = OutrasICMSCabecalho;
                pVendaCabecalho.ICMS = ICMSCabecalho;
                pVendaCabecalho.ISSQN = ISSCabecalho;

                // armazena os possiveis residuos para gravar no item do grupo de aliquota que tem o maior valor na venda
                ResiduoDesconto = pVendaCabecalho.Desconto.Value - DescontoRateio;
                ResiduoAcrescimo = pVendaCabecalho.Acrescimo.Value - AcrescimoRateio;

                // se houver residuo no desconto/acrescimo, deve-se armazenar o mesmo
                if ((ResiduoDesconto > 0) || (ResiduoAcrescimo > 0))
                {
                    // essa consulta vai trazer apenas um registro contendo o totalizador parcial
                    // cujo valor é o maior nessa venda
                    ConsultaSQL = "select TOTALIZADOR_PARCIAL,sum(VALOR_TOTAL) as TOTAL " +
                    " from ECF_VENDA_DETALHE where ID_ECF_VENDA_CABECALHO=" + Convert.ToString(pVendaCabecalho.Id) +
                    " group by TOTALIZADOR_PARCIAL " +
                    " order by TOTAL desc " +
                    " limit 1";

                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    leitor = comando.ExecuteReader();
                    leitor.Read();

                    // neste laco vamos encontrar um item que pertenca ao grupo acima e lancar o residuo nele
                    for (int i = 0; i <= ListaVendaDetalhe.Count - 1; i++)
                    {
                        if (ListaVendaDetalhe[i].TotalizadorParcial == Convert.ToString(leitor["TOTALIZADOR_PARCIAL"]))
                        {
                            ListaVendaDetalhe[i].DescontoRateio = ListaVendaDetalhe[i].DescontoRateio + ResiduoDesconto;
                            ListaVendaDetalhe[i].AcrescimoRateio = ListaVendaDetalhe[i].AcrescimoRateio + ResiduoAcrescimo;
                            break;
                        }
                    }
                }
                leitor.Close();
                AtualizaItens(ListaVendaDetalhe);
                return pVendaCabecalho;
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


        public void AtualizaItens(List<VendaDetalheVO> pListaVendaDetalhe)
        {
            int Ccf, Coo;

            try
            {
                Ccf = Convert.ToInt32(FDataModule.ACBrECF.NumCCF);
                Coo = Convert.ToInt32(FDataModule.ACBrECF.NumCOO);

                for (int i = 0; i <= pListaVendaDetalhe.Count - 1; i++)
                {
                    ConsultaSQL =
                      "update ECF_VENDA_DETALHE set " +
                      "BASE_ICMS = ?pBaseIcms," +
                      "ICMS = ?pIcms," +
                      "ISSQN = ?pIss," +
                      "CCF=?pCCF, " +
                      "COO=?pCOO, " +
                      "DESCONTO_RATEIO = ?pDescontoRateio," +
                      "ACRESCIMO_RATEIO = ?pAcrescimoRateio," +
                      "HASH_INCREMENTO = ?pHashIncremento " +
                      " where ID = ?pId";

                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    comando.Parameters.AddWithValue("?pHashIncremento", -1);
                    comando.Parameters.AddWithValue("?pId", pListaVendaDetalhe[i].Id);
                    comando.Parameters.AddWithValue("?pBaseIcms", pListaVendaDetalhe[i].BaseICMS);
                    comando.Parameters.AddWithValue("?pIcms", pListaVendaDetalhe[i].ICMS);
                    comando.Parameters.AddWithValue("?pIss", pListaVendaDetalhe[i].ISSQN);
                    comando.Parameters.AddWithValue("?pDescontoRateio", pListaVendaDetalhe[i].DescontoRateio);
                    comando.Parameters.AddWithValue("?pAcrescimoRateio", pListaVendaDetalhe[i].AcrescimoRateio);
                    comando.Parameters.AddWithValue("?pCCF", Ccf);
                    comando.Parameters.AddWithValue("?pCOO", Coo);
                    comando.ExecuteNonQuery();
                }
            }

            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
            finally
            {
                if (leitor != null)
                    leitor.Close();
            }
        }


        public List<VendaDetalheVO> VendaAberta()
        {
            int TotalRegistros;

            // verifica se existe alguma venda aberta
            ConsultaSQL =
              "select count(*) as TOTAL from ECF_VENDA_CABECALHO where STATUS_VENDA = " + Biblioteca.QuotedStr("A");

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();
                TotalRegistros = Convert.ToInt32(leitor["TOTAL"]);
                leitor.Close();
                // se existe algum registro com venda aberta inicia a recuperacao da venda
                if (TotalRegistros > 0)
                {
                    // verifica se existem itens para a venda aberta
                    ConsultaSQL =
                      "select " +
                      " C.ID as CID, " +
                      " C.STATUS_VENDA, " +
                      " C.CPF_CNPJ_CLIENTE, " +
                      " D.ID as DID, " +
                      " D.ID_ECF_PRODUTO, " +
                      " D.QUANTIDADE, " +
                      " D.VALOR_UNITARIO, " +
                      " D.VALOR_TOTAL, " +
                      " D.CFOP, " +
                      " P.GTIN, " +
                      " P.ID " +
                      "from " +
                      " ECF_VENDA_CABECALHO C LEFT JOIN ECF_VENDA_DETALHE D ON C.ID=D.ID_ECF_VENDA_CABECALHO, PRODUTO P " +
                      "where " +
                      " C.STATUS_VENDA = " + Biblioteca.QuotedStr("A") + " and D.CANCELADO = " + Biblioteca.QuotedStr("N") + " and D.ID_ECF_PRODUTO=P.ID";

                    List<VendaDetalheVO> ListaVenda = new List<VendaDetalheVO>();
                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        VendaDetalheVO VendaDetalhe = new VendaDetalheVO();
                        VendaDetalhe.Id = Convert.ToInt32(leitor["DID"]);
                        VendaDetalhe.IdVendaCabecalho = Convert.ToInt32(leitor["CID"]);
                        VendaDetalhe.CFOP = Convert.ToInt32(leitor["CFOP"]);
                        VendaDetalhe.IdProduto = Convert.ToInt32(leitor["ID_ECF_PRODUTO"]);
                        VendaDetalhe.Quantidade = Convert.ToInt32(leitor["QUANTIDADE"]);
                        VendaDetalhe.ValorUnitario = Convert.ToDecimal(leitor["VALOR_UNITARIO"]);
                        VendaDetalhe.ValorTotal = Convert.ToDecimal(leitor["VALOR_TOTAL"]);
                        VendaDetalhe.GTIN = Convert.ToString(leitor["GTIN"]);
                        VendaDetalhe.IdentificacaoCliente = Convert.ToString(leitor["CPF_CNPJ_CLIENTE"]);
                        ListaVenda.Add(VendaDetalhe);
                    }
                    leitor.Close();
                    // caso existam itens, continua com a recuperacao da venda
                    if (ListaVenda.Count > 0)
                    {
                        return ListaVenda;
                    }
                    // caso tenha sido aberto um cupom, mas não tenha sido inserido nenhum item
                    // altera o status da venda para cancelado e chama o metodo para cancelamento do cupom
                    else
                    {
                        ConsultaSQL =
                          "update ECF_VENDA_CABECALHO set HASH_INCREMENTO=-1, STATUS_VENDA=" + Biblioteca.QuotedStr("C") +
                          " where STATUS_VENDA = " + Biblioteca.QuotedStr("A");

                        try
                        {
                            comando = new MySqlCommand(ConsultaSQL, conexao);
                            comando.ExecuteNonQuery();
                            UECF.CancelaCupom();
                            return null;
                        }
                        catch (Exception eError)
                        {
                            Log.write(eError.ToString());
                            return null;
                        }
                    }
                }
                // caso não existe uma venda aberta, retorna um ponteiro nulo
                else
                    return null;
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


        public bool VendaAberta(string p)
        {
            // verifica se existe alguma venda aberta
            ConsultaSQL =
              "select * from ECF_VENDA_CABECALHO where STATUS_VENDA = " + Biblioteca.QuotedStr("A");

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                if (leitor.HasRows)
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
            finally
            {
                if (leitor != null)
                    leitor.Close();
            }
        }


        public List<VendaDetalheVO> VendaComProblemas()
        {
            // carrega os itens da venda problematica numa lista
            ConsultaSQL =
              "select " +
              " C.ID as CID, " +
              " C.STATUS_VENDA, " +
              " C.CPF_CNPJ_CLIENTE, " +
              " D.ID as DID, " +
              " D.ID_ECF_PRODUTO, " +
              " D.QUANTIDADE, " +
              " D.VALOR_UNITARIO, " +
              " D.VALOR_TOTAL, " +
              " D.CFOP, " +
              " P.GTIN, " +
              " P.ID " +
              "from " +
              " ECF_VENDA_CABECALHO C LEFT JOIN ECF_VENDA_DETALHE D ON C.ID=D.ID_ECF_VENDA_CABECALHO, PRODUTO P " +
              "where " +
              " C.STATUS_VENDA = " + Biblioteca.QuotedStr("P") + " and D.ID_ECF_PRODUTO=P.ID";
            try
            {
                List<VendaDetalheVO> ListaVenda = new List<VendaDetalheVO>();

                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    VendaDetalheVO VendaDetalhe = new VendaDetalheVO();
                    VendaDetalhe.Id = Convert.ToInt32(leitor["DID"]);
                    VendaDetalhe.IdVendaCabecalho = Convert.ToInt32(leitor["CID"]);
                    VendaDetalhe.CFOP = Convert.ToInt32(leitor["CFOP"]);
                    VendaDetalhe.IdProduto = Convert.ToInt32(leitor["ID_ECF_PRODUTO"]);
                    VendaDetalhe.Quantidade = Convert.ToInt32(leitor["QUANTIDADE"]);
                    VendaDetalhe.ValorUnitario = Convert.ToDecimal(leitor["VALOR_UNITARIO"]);
                    VendaDetalhe.ValorTotal = Convert.ToDecimal(leitor["VALOR_TOTAL"]);
                    VendaDetalhe.GTIN = Convert.ToString(leitor["GTIN"]);
                    VendaDetalhe.IdentificacaoCliente = Convert.ToString(leitor["CPF_CNPJ_CLIENTE"]);
                    ListaVenda.Add(VendaDetalhe);
                }

                // cancela o cupom fiscal da venda anterior
                CancelaVendaAnterior();

                // retorna a lista populada para ser carregada como uma nova venda
                return ListaVenda;
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


        public void CancelaVenda(VendaCabecalhoVO pVendaCabecalho, List<VendaDetalheVO> pListaVendaDetalhe)
        {
            string Tripa, Hash;
            int Ccf, Coo;

            ConsultaSQL =
           "update ECF_VENDA_CABECALHO set " +
           "STATUS_VENDA=?pStatus, " +
           "VALOR_VENDA=?pValorVenda, " +
           "VALOR_FINAL=?pValorFinal, " +
           "HASH_TRIPA=?pHash, " +
           "HASH_INCREMENTO=?pHashIncremento, " +
           "CUPOM_CANCELADO=?pCupomFoiCancelado, " +
           "VALOR_CANCELADO=?pValorCancelado " +
           " where ID = ?pId";

            // zera o valor final
            pVendaCabecalho.ValorFinal = 0;

            try
            {
                Ccf = Convert.ToInt32(FDataModule.ACBrECF.NumCCF);
                Coo = Convert.ToInt32(FDataModule.ACBrECF.NumCOO);

                // calcula e grava o hash
                Tripa = Convert.ToString(pVendaCabecalho.Id) +
                         Convert.ToString(pVendaCabecalho.CCF) +
                         Convert.ToString(pVendaCabecalho.COO) +
                         Biblioteca.FormataFloat("V", pVendaCabecalho.ValorFinal) +
                         FCaixa.Configuracao.NumSerieECF +
                         "C" +
                         "S" +
                         "0";

                Hash = Biblioteca.MD5String(Tripa);

                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pHash", Hash);
                comando.Parameters.AddWithValue("?pHashIncremento", -1);
                comando.Parameters.AddWithValue("?pId", pVendaCabecalho.Id);
                comando.Parameters.AddWithValue("?pValorCancelado", pVendaCabecalho.ValorVenda);
                comando.Parameters.AddWithValue("?pValorVenda", pVendaCabecalho.ValorVenda);
                comando.Parameters.AddWithValue("?pStatus", "C");
                comando.Parameters.AddWithValue("?pCupomFoiCancelado", "S");
                comando.Parameters.AddWithValue("?pValorFinal", pVendaCabecalho.ValorFinal);
                comando.ExecuteNonQuery();

                // cancela os itens da venda
                for (int i = 0; i <= pListaVendaDetalhe.Count - 1; i++)
                {
                    pListaVendaDetalhe[i].TotalizadorParcial = "Can-T";
                    pListaVendaDetalhe[i].Cancelado = "S";

                    ConsultaSQL =
                      "update ECF_VENDA_DETALHE set " +
                      "CANCELADO=?pCancelado, " +
                      "TOTALIZADOR_PARCIAL=?pTotalizadorParcial, " +
                      "CCF=?pCCF, " +
                      "COO=?pCOO, " +
                      "HASH_TRIPA=?pHash, " +
                      "HASH_INCREMENTO=?pHashIncremento " +
                      " where ID = ?pId";

                    // calcula e grava o hash
                    Tripa =
                             FCaixa.Configuracao.NumSerieECF +
                             Convert.ToString(Coo) +
                             Convert.ToString(Ccf) +
                             pListaVendaDetalhe[i].GTIN +
                             Biblioteca.FormataFloat("Q", pListaVendaDetalhe[i].Quantidade) +
                             Biblioteca.FormataFloat("V", pListaVendaDetalhe[i].ValorUnitario) +
                             Biblioteca.FormataFloat("V", pListaVendaDetalhe[i].TotalItem) +
                             pListaVendaDetalhe[i].TotalizadorParcial +
                             pListaVendaDetalhe[i].Cancelado +
                             "0";
                    Hash = Biblioteca.MD5String(Tripa);

                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    comando.Parameters.AddWithValue("?pCancelado", pListaVendaDetalhe[i].Cancelado);
                    comando.Parameters.AddWithValue("?pTotalizadorParcial", pListaVendaDetalhe[i].TotalizadorParcial);
                    comando.Parameters.AddWithValue("?pCCF", Ccf);
                    comando.Parameters.AddWithValue("?pCOO", Coo);
                    comando.Parameters.AddWithValue("?pHash", Hash);
                    comando.Parameters.AddWithValue("?pHashIncremento", -1);
                    comando.Parameters.AddWithValue("?pId", pListaVendaDetalhe[i].Id);
                    comando.ExecuteNonQuery();
                }

                // estorna os pagamentos realizados
                ConsultaSQL =
                  "update ECF_TOTAL_TIPO_PGTO set " +
                  "ESTORNO=" + Biblioteca.QuotedStr("S") +
                  ", HASH_INCREMENTO=?pHashIncremento" +
                  " where ID_ECF_VENDA_CABECALHO = " + Convert.ToString(pVendaCabecalho.Id);

                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pHashIncremento", -1);
                comando.ExecuteNonQuery();
            }

            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
            finally
            {
                if (leitor != null)
                    leitor.Close();
            }
        }


        public void CancelaItem(VendaDetalheVO pVendaDetalhe)
        {
            string Tripa, Hash;
            int Ccf, Coo;

            pVendaDetalhe.TotalizadorParcial = "Can-T";
            pVendaDetalhe.Cancelado = "S";

            ConsultaSQL =
              "update ECF_VENDA_DETALHE set " +
              "CANCELADO=?pCancelado, " +
              "TOTALIZADOR_PARCIAL=?pTotalizadorParcial, " +
              "CCF=?pCCF, " +
              "COO=?pCOO, " +
              "HASH_TRIPA=?pHash, " +
              "HASH_INCREMENTO=?pHashIncremento " +
              " where ID = ?pId";

            try
            {
                Ccf = Convert.ToInt32(FDataModule.ACBrECF.NumCCF);
                Coo = Convert.ToInt32(FDataModule.ACBrECF.NumCOO);

                // calcula e grava o hash
                Tripa =
                         FCaixa.Configuracao.NumSerieECF +
                         Convert.ToString(Coo) +
                         Convert.ToString(Ccf) +
                         pVendaDetalhe.GTIN +
                         Biblioteca.FormataFloat("Q", pVendaDetalhe.Quantidade) +
                         Biblioteca.FormataFloat("V", pVendaDetalhe.ValorUnitario) +
                         Biblioteca.FormataFloat("V", pVendaDetalhe.TotalItem) +
                         pVendaDetalhe.TotalizadorParcial +
                         pVendaDetalhe.Cancelado +
                         "0";
                Hash = Biblioteca.MD5String(Tripa);

                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pCancelado", pVendaDetalhe.Cancelado);
                comando.Parameters.AddWithValue("?pTotalizadorParcial", pVendaDetalhe.TotalizadorParcial);
                comando.Parameters.AddWithValue("?pCCF", Ccf);
                comando.Parameters.AddWithValue("?pCOO", Coo);
                comando.Parameters.AddWithValue("?pHash", Hash);
                comando.Parameters.AddWithValue("?pHashIncremento", -1);
                comando.Parameters.AddWithValue("?pId", pVendaDetalhe.Id);
                comando.ExecuteNonQuery();
            }

            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
            finally
            {
                if (leitor != null)
                    leitor.Close();
            }
        }


        public bool CancelaVendaAnterior()
        {
            string Tripa, Hash;
            int Ccf, Coo;

            // primeiro pegamos o ultimo ID e verificamos se o status da venda é? "F" ou "P"
            ConsultaSQL = "select " +
                          "ID, CCF, COO, VALOR_FINAL, STATUS_VENDA " +
                          "from ECF_VENDA_CABECALHO " +
                          "where ID = (select MAX(ID) from ECF_VENDA_CABECALHO)";

            try
            {
                Ccf = Convert.ToInt32(FDataModule.ACBrECF.NumCCF);
                Coo = Convert.ToInt32(FDataModule.ACBrECF.NumCOO);

                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();

                if ((Convert.ToString(leitor["STATUS_VENDA"]) == "F") || (Convert.ToString(leitor["STATUS_VENDA"]) == "P"))
                {

                    if (!CupomJaFoiCancelado())
                        UECF.CancelaCupom();

                    VendaCabecalhoVO VendaCabecalho = new VendaCabecalhoVO();
                    VendaCabecalho.Id = Convert.ToInt32(leitor["ID"]);
                    VendaCabecalho.CCF = Convert.ToInt32(leitor["CCF"]);
                    VendaCabecalho.COO = Convert.ToInt32(leitor["COO"]);
                    VendaCabecalho.ValorFinal = Convert.ToDecimal(leitor["VALOR_FINAL"]);
                    VendaCabecalho.StatusVenda = "C";

                    leitor.Close();

                    ConsultaSQL =
                      "update ECF_VENDA_CABECALHO set " +
                      "STATUS_VENDA=?pStatus, " +
                      "CUPOM_CANCELADO=?pCupomFoiCancelado, " +
                      "HASH_TRIPA=?pHash, " +
                      "HASH_INCREMENTO=?pHashIncremento " +
                      " where ID = ?pId";

                    // calcula e grava o hash
                    Tripa = Convert.ToString(VendaCabecalho.Id) +
                             Convert.ToString(VendaCabecalho.CCF) +
                             Convert.ToString(VendaCabecalho.COO) +
                             Biblioteca.FormataFloat("V", VendaCabecalho.ValorFinal) +
                             FCaixa.Configuracao.NumSerieECF +
                             VendaCabecalho.StatusVenda +
                             "S" +
                             "0";
                    Hash = Biblioteca.MD5String(Tripa);

                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    comando.Parameters.AddWithValue("?pHash", Hash);
                    comando.Parameters.AddWithValue("?pHashIncremento", -1);
                    comando.Parameters.AddWithValue("?pId", VendaCabecalho.Id);
                    comando.Parameters.AddWithValue("?pStatus", VendaCabecalho.StatusVenda);
                    comando.Parameters.AddWithValue("?pCupomFoiCancelado", "S");
                    comando.ExecuteNonQuery();

                    // popula os itens da venda
                    ConsultaSQL = "select " +
                                   " P.GTIN, " +
                                   " VD.ID, " +
                                   " VD.CCF, " +
                                   " VD.COO, " +
                                   " VD.QUANTIDADE, " +
                                   " VD.VALOR_UNITARIO, " +
                                   " VD.TOTAL_ITEM, " +
                                   " VD.TOTALIZADOR_PARCIAL " +
                                  "from " +
                                  " ECF_VENDA_DETALHE VD, PRODUTO P " +
                                  "where " +
                                  " P.ID = VD.ID_ECF_PRODUTO " +
                                  " and VD.ID_ECF_VENDA_CABECALHO = " + Convert.ToString(VendaCabecalho.Id);

                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    leitor = comando.ExecuteReader();

                    // cancela os itens da venda
                    while (leitor.Read())
                    {
                        ConsultaSQL =
                          "update ECF_VENDA_DETALHE set " +
                          "CANCELADO=?pCancelado, " +
                          "TOTALIZADOR_PARCIAL=?pTotalizadorParcial, " +
                          "CCF=?pCCF, " +
                          "COO=?pCOO, " +
                          "HASH_TRIPA=?pHash, " +
                          "HASH_INCREMENTO=?pHashIncremento " +
                          " where ID = ?pId";

                        // calcula e grava o hash
                        Tripa =
                                 FCaixa.Configuracao.NumSerieECF +
                                 Convert.ToString(Coo) +
                                 Convert.ToString(Ccf) +
                                 Convert.ToString(leitor["GTIN"]) +
                                 Biblioteca.FormataFloat("Q", Convert.ToDecimal(leitor["QUANTIDADE"])) +
                                 Biblioteca.FormataFloat("V", Convert.ToDecimal(leitor["VALOR_UNITARIO"])) +
                                 Biblioteca.FormataFloat("V", Convert.ToDecimal(leitor["TOTAL_ITEM"])) +
                                 "Can-T" +
                                 "S" +
                                 "0";
                        Hash = Biblioteca.MD5String(Tripa);

                        comando = new MySqlCommand(ConsultaSQL, conexao);
                        comando.Parameters.AddWithValue("?pCancelado", "S");
                        comando.Parameters.AddWithValue("?pTotalizadorParcial", "Can-T");
                        comando.Parameters.AddWithValue("?pCCF", Ccf);
                        comando.Parameters.AddWithValue("?pCOO", Coo);
                        comando.Parameters.AddWithValue("?pHash", Hash);
                        comando.Parameters.AddWithValue("?pHashIncremento", -1);
                        comando.Parameters.AddWithValue("?pId", Convert.ToInt32(leitor["ID"]));
                        comando.ExecuteNonQuery();
                    }

                    // estorna os pagamentos realizados
                    ConsultaSQL =
                      "update ECF_TOTAL_TIPO_PGTO set " +
                      "ESTORNO=" + Biblioteca.QuotedStr("S") +
                      ", HASH_INCREMENTO=?pHashIncremento" +
                      " where ID_ECF_VENDA_CABECALHO = " + Convert.ToString(VendaCabecalho.Id);

                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    comando.Parameters.AddWithValue("?pHashIncremento", -1);
                    comando.ExecuteNonQuery();

                    return true;
                }
                else
                    return false;
            }

            catch (Exception eError)
            {
                Log.write(eError.ToString());
                return false;
            }
            finally
            {
                if (leitor != null)
                    leitor.Close();
            }
        }


        public bool CupomJaFoiCancelado()
        {

            // verifica se o cupom referente a ultima venda Ja foi cancelado
            ConsultaSQL = "select CUPOM_CANCELADO " +
                          "from ECF_VENDA_CABECALHO " +
                          "where ID = (select MAX(ID) from ECF_VENDA_CABECALHO)";
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();

                if (Convert.ToString(leitor["CUPOM_CANCELADO"]) == "S")
                    return true;
                else
                    return false;
            }

            catch (Exception eError)
            {
                Log.write(eError.ToString());
                return false;
            }
            finally
            {
                if (leitor != null)
                    leitor.Close();
            }
        }


        public VendaCabecalhoVO RetornaCabecalhoDaUltimaVenda()
        {

            ConsultaSQL = "SELECT * FROM ECF_VENDA_CABECALHO WHERE ID = (SELECT MAX(ID) FROM ECF_VENDA_CABECALHO)";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();

                VendaCabecalhoVO VendaCabecalho = new VendaCabecalhoVO();

                VendaCabecalho.Id = Convert.ToInt32(leitor["ID"]);
                VendaCabecalho.IdCliente = leitor["ID_CLIENTE"] as int?;
                VendaCabecalho.IdVendedor = leitor["ID_ECF_FUNCIONARIO"] as int?;
                VendaCabecalho.IdMovimento = leitor["ID_ECF_MOVIMENTO"] as int?;
                VendaCabecalho.IdDAV = leitor["ID_ECF_DAV"] as int?;
                VendaCabecalho.IdPreVenda = leitor["ID_ECF_PRE_VENDA_CABECALHO"] as int?;
                VendaCabecalho.CFOP = Convert.ToInt32(leitor["CFOP"]);
                VendaCabecalho.COO = Convert.ToInt32(leitor["COO"]);
                VendaCabecalho.CCF = Convert.ToInt32(leitor["CCF"]);
                VendaCabecalho.DataVenda = (Convert.ToDateTime(leitor["DATA_VENDA"]));
                VendaCabecalho.HoraVenda = Convert.ToString(leitor["HORA_VENDA"]);
                VendaCabecalho.ValorVenda = leitor["VALOR_VENDA"] as decimal?;
                VendaCabecalho.TaxaDesconto = leitor["TAXA_DESCONTO"] as decimal?;
                VendaCabecalho.Desconto = leitor["DESCONTO"] as decimal?;
                VendaCabecalho.TaxaAcrescimo = leitor["TAXA_ACRESCIMO"] as decimal?;
                VendaCabecalho.Acrescimo = leitor["ACRESCIMO"] as decimal?;
                VendaCabecalho.ValorFinal = leitor["VALOR_FINAL"] as decimal?;
                VendaCabecalho.ValorRecebido = leitor["VALOR_RECEBIDO"] as decimal?;
                VendaCabecalho.Troco = leitor["TROCO"] as decimal?;
                VendaCabecalho.ValorCancelado = leitor["VALOR_CANCELADO"] as decimal?;
                VendaCabecalho.Sincronizado = Convert.ToString(leitor["SINCRONIZADO"]);
                VendaCabecalho.TotalProdutos = leitor["TOTAL_PRODUTOS"] as decimal?;
                VendaCabecalho.TotalDocumentos = leitor["TOTAL_DOCUMENTO"] as decimal?;
                VendaCabecalho.BaseICMS = leitor["BASE_ICMS"] as decimal?;
                VendaCabecalho.ICMS = leitor["ICMS"] as decimal?;
                VendaCabecalho.ICMSOutras = leitor["ICMS_OUTRAS"] as decimal?;
                VendaCabecalho.ISSQN = leitor["ISSQN"] as decimal?;
                VendaCabecalho.PIS = leitor["PIS"] as decimal?;
                VendaCabecalho.COFINS = leitor["COFINS"] as decimal?;
                VendaCabecalho.AcrescimoItens = leitor["ACRESCIMO_ITENS"] as decimal?;
                VendaCabecalho.DescontoItens = leitor["DESCONTO_ITENS"] as decimal?;
                VendaCabecalho.StatusVenda = Convert.ToString(leitor["STATUS_VENDA"]);
                VendaCabecalho.NomeCliente = Convert.ToString(leitor["NOME_CLIENTE"]);
                VendaCabecalho.CPFouCNPJCliente = Convert.ToString(leitor["CPF_CNPJ_CLIENTE"]);
                VendaCabecalho.CupomFoiCancelado = Convert.ToString(leitor["CUPOM_CANCELADO"]);
                VendaCabecalho.HashTripa = Convert.ToString(leitor["HASH_TRIPA"]);

                return VendaCabecalho;
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


        public List<VendaDetalheVO> RetornaDetalheDaUltimaVenda(int pIdCabecalho)
        {

            ConsultaSQL =
              "select * from ECF_VENDA_DETALHE where ID_ECF_VENDA_CABECALHO = " + Convert.ToString(pIdCabecalho);

            try
            {

                List<VendaDetalheVO> ListaVenda = new List<VendaDetalheVO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    VendaDetalheVO VendaDetalhe = new VendaDetalheVO();
                    VendaDetalhe.Id = Convert.ToInt32(leitor["ID"]);
                    VendaDetalhe.IdProduto = Convert.ToInt32(leitor["ID_ECF_PRODUTO"]);
                    VendaDetalhe.IdVendaCabecalho = Convert.ToInt32(leitor["ID_ECF_VENDA_CABECALHO"]);
                    VendaDetalhe.GTIN = Convert.ToString(leitor["GTIN"]);
                    VendaDetalhe.CFOP = Convert.ToInt32(leitor["CFOP"]);
                    VendaDetalhe.Ccf = Convert.ToInt32(leitor["CCF"]);
                    VendaDetalhe.Coo = Convert.ToInt32(leitor["COO"]);
                    VendaDetalhe.Item = Convert.ToInt32(leitor["ITEM"]);
                    VendaDetalhe.Quantidade = Convert.ToDecimal(leitor["QUANTIDADE"]);
                    VendaDetalhe.ValorUnitario = Convert.ToDecimal(leitor["VALOR_UNITARIO"]);
                    VendaDetalhe.ValorTotal = Convert.ToDecimal(leitor["VALOR_TOTAL"]);
                    VendaDetalhe.TotalItem = Convert.ToDecimal(leitor["TOTAL_ITEM"]);
                    VendaDetalhe.BaseICMS = Convert.ToDecimal(leitor["BASE_ICMS"]);
                    VendaDetalhe.TaxaICMS = Convert.ToDecimal(leitor["TAXA_ICMS"]);
                    VendaDetalhe.ICMS = Convert.ToDecimal(leitor["ICMS"]);
                    VendaDetalhe.TaxaDesconto = Convert.ToDecimal(leitor["TAXA_DESCONTO"]);
                    VendaDetalhe.Desconto = Convert.ToDecimal(leitor["DESCONTO"]);
                    VendaDetalhe.TaxaICMS = Convert.ToDecimal(leitor["TAXA_ISSQN"]);
                    VendaDetalhe.ISSQN = Convert.ToDecimal(leitor["ISSQN"]);
                    VendaDetalhe.TaxaPIS = Convert.ToDecimal(leitor["TAXA_PIS"]);
                    VendaDetalhe.PIS = Convert.ToDecimal(leitor["PIS"]);
                    VendaDetalhe.TaxaCOFINS = Convert.ToDecimal(leitor["TAXA_COFINS"]);
                    VendaDetalhe.COFINS = Convert.ToDecimal(leitor["COFINS"]);
                    VendaDetalhe.TaxaAcrescimo = Convert.ToDecimal(leitor["TAXA_ACRESCIMO"]);
                    VendaDetalhe.Acrescimo = Convert.ToDecimal(leitor["ACRESCIMO"]);
                    VendaDetalhe.AcrescimoRateio = Convert.ToDecimal(leitor["ACRESCIMO_RATEIO"]);
                    VendaDetalhe.DescontoRateio = Convert.ToDecimal(leitor["DESCONTO_RATEIO"]);
                    VendaDetalhe.TotalizadorParcial = Convert.ToString(leitor["TOTALIZADOR_PARCIAL"]);
                    VendaDetalhe.CST = Convert.ToString(leitor["CST"]);
                    VendaDetalhe.Cancelado = Convert.ToString(leitor["CANCELADO"]);
                    VendaDetalhe.MovimentaEstoque = Convert.ToString(leitor["MOVIMENTA_ESTOQUE"]);
                    VendaDetalhe.HashTripa = Convert.ToString(leitor["HASH_TRIPA"]);

                    ListaVenda.Add(VendaDetalhe);
                }
                return ListaVenda;
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


        public void AlteraClienteNaVenda(int pIdVenda, int pIdCliente, string pCpfCnpj, string pNomeCliente)
        {
            ConsultaSQL =
              "update ECF_VENDA_CABECALHO set " +
              "ID_CLIENTE=?pIDCliente, " +
              "CPF_CNPJ_CLIENTE=?pcpf_cnpj, " +
              "NOME_CLIENTE=?pNomeCliente, " +
              "HASH_INCREMENTO=?pHashIncremento " +
              " where ID = ?pIDVenda";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pIDVenda", pIdVenda);
                comando.Parameters.AddWithValue("?pIDCliente", pIdCliente);
                comando.Parameters.AddWithValue("?pcpf_cnpj", pCpfCnpj);
                comando.Parameters.AddWithValue("?pNomeCliente", pNomeCliente);
                comando.Parameters.AddWithValue("?pHashIncremento", -1);
                comando.ExecuteNonQuery();
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
            finally
            {
                if (leitor != null)
                    leitor.Close();
            }
        }

    }

}

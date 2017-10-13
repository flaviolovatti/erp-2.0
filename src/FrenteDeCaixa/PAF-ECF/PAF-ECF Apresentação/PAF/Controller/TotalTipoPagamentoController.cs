/********************************************************************************
Title: T2TiPDV
Description: Classe de controle do total dos tipos de pagamento

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
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using PafEcf.Infra;
using PafEcf.Util;
using PafEcf.View;
using PafEcf.VO;

namespace PafEcf.Controller
{


    public class TotalTipoPagamentoController
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public TotalTipoPagamentoController()
        {
            conexao = dbConnection.conectar();
        }


        public void GravaTotalTipoPagamento(TotalTipoPagamentoVO TotalTipoPagamento)
        {
            string Tripa, Hash;

            int Coo, Ccf, Gnf;
            ConsultaSQL = "insert into " +
                           " ECF_TOTAL_TIPO_PGTO ( " +
                           " ID_ECF_VENDA_CABECALHO, " +
                           " ID_ECF_TIPO_PAGAMENTO, " +
                           " VALOR, " +
                           " NSU, " +
                           " ESTORNO, " +
                           " REDE, " +
                           " CARTAO_DC) " +
                           " values ( " +
                           " ?pIdVendaCabecalho, " +
                           " ?pIdTipoPagamento, " +
                           " ?pValor, " +
                           " ?pNSU, " +
                           " ?pEstorno, " +
                           " ?pRede, " +
                           " ?pDebitoCredito)";

            try
            {

                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pIdVendaCabecalho", TotalTipoPagamento.IdVenda);
                comando.Parameters.AddWithValue("?pIdTipoPagamento", TotalTipoPagamento.IdTipoPagamento);
                comando.Parameters.AddWithValue("?pValor", TotalTipoPagamento.Valor);
                comando.Parameters.AddWithValue("?pEstorno", TotalTipoPagamento.Estorno);
                comando.Parameters.AddWithValue("?pNSU", TotalTipoPagamento.NSU);
                comando.Parameters.AddWithValue("?pRede", TotalTipoPagamento.Rede);
                comando.Parameters.AddWithValue("?pDebitoCredito", TotalTipoPagamento.CartaoDebitoOuCredito);
                comando.ExecuteNonQuery();

                ConsultaSQL = "select max(ID) as ID from ECF_TOTAL_TIPO_PGTO";
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();
                TotalTipoPagamento.Id = Convert.ToInt32(leitor["ID"]);
                leitor.Close();

                // calcula e grava o hash
                ConsultaSQL =
                  "update ECF_TOTAL_TIPO_PGTO set " +
                  "SERIE_ECF = ?pSERIE_ECF, " +
                  "COO = ?pCOO, " +
                  "CCF = ?pCCF, " +
                  "GNF = ?pGNF, " +
                  "HASH_TRIPA = ?pHashTripa, " +
                  "HASH_INCREMENTO = ?pHashIncremento " +
                  " where ID = ?pId";

                Coo = Convert.ToInt32(FDataModule.ACBrECF.NumCOO);
                Ccf = Convert.ToInt32(FDataModule.ACBrECF.NumCCF);
                Gnf = Convert.ToInt32(FDataModule.ACBrECF.NumGNF);

                Tripa = FCaixa.Configuracao.NumSerieECF +
                          Convert.ToString(Coo) +
                          Convert.ToString(Ccf) +
                          Convert.ToString(Gnf) +
                          "0";
                Hash = Biblioteca.MD5String(Tripa);

                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pHashIncremento", -1);
                comando.Parameters.AddWithValue("?pHashTripa", Hash);
                comando.Parameters.AddWithValue("?pId", TotalTipoPagamento.Id);
                comando.Parameters.AddWithValue("?pSERIE_ECF", FCaixa.Configuracao.NumSerieECF);
                comando.Parameters.AddWithValue("?pCOO", Coo);
                comando.Parameters.AddWithValue("?pCCF", Ccf);
                comando.Parameters.AddWithValue("?pGNF", Gnf);
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


        public void GravaTotaisVenda(List<TotalTipoPagamentoVO> ListaTotalTipoPagamento)
        {
            string Tripa, Hash;
            int Coo, Ccf, Gnf;

            try
            {
                for (int i = 0; i <= ListaTotalTipoPagamento.Count - 1; i++)
                {
                    ConsultaSQL = "insert into " +
                                   " ECF_TOTAL_TIPO_PGTO ( " +
                                   " ID_ECF_VENDA_CABECALHO, " +
                                   " ID_ECF_TIPO_PAGAMENTO, " +
                                   " VALOR, " +
                                   " NSU, " +
                                   " ESTORNO, " +
                                   " REDE, " +
                                   " CARTAO_DC) " +
                                   " values ( " +
                                   " ?pIdVendaCabecalho, " +
                                   " ?pIdTipoPagamento, " +
                                   " ?pValor, " +
                                   " ?pNSU, " +
                                   " ?pEstorno, " +
                                   " ?pRede, " +
                                   " ?pDebitoCredito)";

                    TotalTipoPagamentoVO TotalTipoPagamento = ListaTotalTipoPagamento[i];

                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    comando.Parameters.AddWithValue("?pIdVendaCabecalho", TotalTipoPagamento.IdVenda);
                    comando.Parameters.AddWithValue("?pIdTipoPagamento", TotalTipoPagamento.IdTipoPagamento);
                    comando.Parameters.AddWithValue("?pValor", TotalTipoPagamento.Valor);
                    comando.Parameters.AddWithValue("?pEstorno", TotalTipoPagamento.Estorno);
                    comando.Parameters.AddWithValue("?pNSU", TotalTipoPagamento.NSU);
                    comando.Parameters.AddWithValue("?pRede", TotalTipoPagamento.Rede);
                    comando.Parameters.AddWithValue("?pDebitoCredito", TotalTipoPagamento.CartaoDebitoOuCredito);
                    comando.ExecuteNonQuery();

                    ConsultaSQL = "select max(ID) as ID from ECF_TOTAL_TIPO_PGTO";
                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    leitor = comando.ExecuteReader();
                    leitor.Read();
                    TotalTipoPagamento.Id = Convert.ToInt32(leitor["ID"]);
                    leitor.Close();

                    // calcula e grava o hash
                    ConsultaSQL =
                      "update ECF_TOTAL_TIPO_PGTO set " +
                      "SERIE_ECF = ?pSERIE_ECF, " +
                      "COO = ?pCOO, " +
                      "CCF = ?pCCF, " +
                      "GNF = ?pGNF, " +
                      "HASH_TRIPA = ?pHashTripa, " +
                      "HASH_INCREMENTO = ?pHashIncremento " +
                      " where ID = ?pId";

                    Coo = Convert.ToInt32(FDataModule.ACBrECF.NumCOO);
                    Ccf = Convert.ToInt32(FDataModule.ACBrECF.NumCCF);
                    Gnf = Convert.ToInt32(FDataModule.ACBrECF.NumGNF);

                    Tripa = FCaixa.Configuracao.NumSerieECF +
                              Convert.ToString(Coo) +
                              Convert.ToString(Ccf) +
                              Convert.ToString(Gnf) +
                              "0";
                    Hash = Biblioteca.MD5String(Tripa);

                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    comando.Parameters.AddWithValue("?pHashIncremento", -1);
                    comando.Parameters.AddWithValue("?pHashTripa", Hash);
                    comando.Parameters.AddWithValue("?pId", TotalTipoPagamento.Id);
                    comando.Parameters.AddWithValue("?pSERIE_ECF", FCaixa.Configuracao.NumSerieECF);
                    comando.Parameters.AddWithValue("?pCOO", Coo);
                    comando.Parameters.AddWithValue("?pCCF", Ccf);
                    comando.Parameters.AddWithValue("?pGNF", Gnf);
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


        public List<MeiosPagamentoVO> MeiosPagamento(string pDataInicio, string pDataFim, int pIdImpressora)
        {
            ConsultaSQL =
              "SELECT * from VIEW_MEIOS_PAGAMENTO " +
              "WHERE " +
              "ID_ECF_IMPRESSORA = " + Convert.ToString(pIdImpressora) + " AND " +
              "(DATA_ACUMULADO BETWEEN " + Biblioteca.QuotedStr(pDataInicio) + " and " + Biblioteca.QuotedStr(pDataFim) +
              ") order by DATA_ACUMULADO";
            try
            {

                List<MeiosPagamentoVO> ListaMeiosPagamento = new List<MeiosPagamentoVO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    MeiosPagamentoVO MeiosPagamento = new MeiosPagamentoVO();
                    MeiosPagamento.Descricao = Convert.ToString(leitor["DESCRICAO"]);
                    MeiosPagamento.DataHora = Convert.ToDateTime(leitor["DATA_ACUMULADO"]);
                    MeiosPagamento.Total = Convert.ToDecimal(leitor["TOTAL"]);
                    ListaMeiosPagamento.Add(MeiosPagamento);
                }
                return ListaMeiosPagamento;
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


        public List<MeiosPagamentoVO> MeiosPagamentoTotal(string pDataInicio, string pDataFim, int pIdImpressora)
        {
            ConsultaSQL =
            "select m.ID_ECF_IMPRESSORA,p.DESCRICAO, " +
            "sum(tp.VALOR) AS TOTAL " +
            "from ecf_venda_cabecalho v " +
               " INNER JOIN ecf_movimento m ON (v.ID_ECF_MOVIMENTO = m.ID) " +
               " INNER JOIN ecf_total_tipo_pgto tp ON (v.ID = tp.ID_ECF_VENDA_CABECALHO) " +
               " INNER JOIN ecf_tipo_pagamento p ON (tp.ID_ECF_TIPO_PAGAMENTO = p.ID) " +
              "WHERE " +
              "m.ID_ECF_IMPRESSORA = " + Convert.ToString(pIdImpressora) + " AND " +
              "(v.DATA_VENDA BETWEEN " + Biblioteca.QuotedStr(pDataInicio) + " and " + Biblioteca.QuotedStr(pDataFim) + ")  " +
              "GROUP BY m.ID_ECF_IMPRESSORA,p.DESCRICAO order by p.DESCRICAO ";

            try
            {
                List<MeiosPagamentoVO> ListaMeiosPagamento = new List<MeiosPagamentoVO>();

                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    MeiosPagamentoVO MeiosPagamento = new MeiosPagamentoVO();
                    MeiosPagamento.Descricao = Convert.ToString(leitor["DESCRICAO"]);
                    MeiosPagamento.Total = Convert.ToDecimal(leitor["TOTAL"]);
                    ListaMeiosPagamento.Add(MeiosPagamento);
                }
                return ListaMeiosPagamento;
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


        public List<MeiosPagamentoVO> EncerramentoTotal(int pIdMovimento, int pTipo)
        {

            if (pTipo == 1)
                ConsultaSQL =
                  "select v.DATA_VENDA AS DATA_ACUMULADO,m.ID_ECF_IMPRESSORA,p.DESCRICAO, " +
                  "COALESCE(sum(tp.VALOR),0) AS TOTAL " +
                  "from ecf_venda_cabecalho v " +
                        "INNER JOIN ecf_movimento m ON (v.ID_ECF_MOVIMENTO = m.ID) " +
                        "INNER JOIN ecf_total_tipo_pgto tp ON (v.ID = tp.ID_ECF_VENDA_CABECALHO) " +
                        "INNER JOIN ecf_tipo_pagamento p ON (tp.ID_ECF_TIPO_PAGAMENTO = p.ID) " +
                  " WHERE v.ID_ECF_MOVIMENTO = " + Convert.ToString(pIdMovimento) +
                  " GROUP BY p.DESCRICAO,m.ID_ECF_IMPRESSORA,v.DATA_VENDA";
            else
                ConsultaSQL =
                  "SELECT " + Biblioteca.QuotedStr("DATA") + " AS DATA_ACUMULADO, TIPO_PAGAMENTO AS DESCRICAO, " +
                  " COALESCE(sum(VALOR),0) AS TOTAL  FROM ECF_FECHAMENTO" +
                  " WHERE ID_ECF_MOVIMENTO = " + Convert.ToString(pIdMovimento) +
                  " GROUP BY TIPO_PAGAMENTO";

            try
            {
                List<MeiosPagamentoVO> ListaMeiosPagamento = new List<MeiosPagamentoVO>();

                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();

                while (leitor.Read())
                {

                    MeiosPagamentoVO MeiosPagamento = new MeiosPagamentoVO();
                    MeiosPagamento.Descricao = Convert.ToString(leitor["DESCRICAO"]);
                    MeiosPagamento.DataHora = Convert.ToDateTime(leitor["DATA_ACUMULADO"]);
                    MeiosPagamento.Total = Convert.ToDecimal(leitor["TOTAL"]);
                    ListaMeiosPagamento.Add(MeiosPagamento);

                }
                return ListaMeiosPagamento;
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


        public int QuantidadeRegistroTabela()
        {
            ConsultaSQL =
              "SELECT count(*) as TOTAL from ecf_total_tipo_pgto";
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();

                return Convert.ToInt32(leitor["TOTAL"]);
            }

            catch (Exception eError)
            {
                Log.write(eError.ToString());
                return 1;
            }
            finally
            {
                if (leitor != null)
                    leitor.Close();
            }
        }


        public List<TotalTipoPagamentoVO> RetornaMeiosPagamentoDaUltimaVenda(int pIdCabecalho)
        {
            ConsultaSQL = "SELECT " +
                           " T.ID, " +
                           " T.ID_ECF_VENDA_CABECALHO, " +
                           " T.ID_ECF_TIPO_PAGAMENTO, " +
                           " T.VALOR, " +
                           " T.NSU, " +
                           " T.ESTORNO, " +
                           " T.REDE, " +
                           " T.CARTAO_DC, " +
                           " P.DESCRICAO " +
                           "FROM " +
                           " ECF_TIPO_PAGAMENTO  P, ECF_TOTAL_TIPO_PGTO T " +
                           "WHERE " +
                           " (ID_ECF_VENDA_CABECALHO = " + Convert.ToString(pIdCabecalho) + ")  " +
                           " and (P.ID = T.ID_ECF_TIPO_PAGAMENTO) order by T.ID_ECF_TIPO_PAGAMENTO";
            try
            {
                List<TotalTipoPagamentoVO> ListaTotalTipoPagamento = new List<TotalTipoPagamentoVO>();

                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    TotalTipoPagamentoVO TotalTipoPagamento = new TotalTipoPagamentoVO();

                    TotalTipoPagamento.Id = Convert.ToInt32(leitor["ID"]);
                    TotalTipoPagamento.IdVenda = Convert.ToInt32(leitor["ID_ECF_VENDA_CABECALHO"]);
                    TotalTipoPagamento.IdTipoPagamento = Convert.ToInt32(leitor["ID_ECF_TIPO_PAGAMENTO"]);
                    TotalTipoPagamento.Valor = Convert.ToDecimal(leitor["VALOR"]);
                    TotalTipoPagamento.NSU = Convert.ToString(leitor["NSU"]);
                    TotalTipoPagamento.Estorno = Convert.ToString(leitor["ESTORNO"]);
                    TotalTipoPagamento.Rede = Convert.ToString(leitor["REDE"]);
                    TotalTipoPagamento.CartaoDebitoOuCredito = Convert.ToString(leitor["CARTAO_DC"]);
                    TotalTipoPagamento.Descricao = Convert.ToString(leitor["DESCRICAO"]);
                    ListaTotalTipoPagamento.Add(TotalTipoPagamento);
                }
                return ListaTotalTipoPagamento;
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

    }

}

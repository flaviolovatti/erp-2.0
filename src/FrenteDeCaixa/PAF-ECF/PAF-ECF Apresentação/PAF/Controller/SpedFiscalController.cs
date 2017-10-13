/********************************************************************************
Title: T2TiPDV
Description: Classe de controle do Sped Fiscal

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
using MySql.Data;
using PafEcf.Util;
using PafEcf.Infra;
using PafEcf.VO;
using System.Collections.Generic;

namespace PafEcf.Controller
{

    public class SpedFiscalController
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public SpedFiscalController()
        {
            conexao = dbConnection.conectar();
        }


        public List<SpedFiscalC370VO> TabelaC370(int pId)
        {
            ConsultaSQL =
              "select * from VIEW_C370 " +
              "where ID_NF_CABECALHO = " + Convert.ToString(pId);
            try
            {
                List<SpedFiscalC370VO> ListaC370 = new List<SpedFiscalC370VO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    SpedFiscalC370VO RegistroC370 = new SpedFiscalC370VO();
                    RegistroC370.IdProduto = Convert.ToInt32(leitor["ID_PRODUTO"]);
                    RegistroC370.Item = Convert.ToInt32(leitor["ITEM"]);
                    RegistroC370.IdUnidade = Convert.ToInt32(leitor["ID_UNIDADE_PRODUTO"]);
                    RegistroC370.Quantidade = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["QUANTIDADE"]), Constantes.DECIMAIS_VALOR);
                    RegistroC370.Valor = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["VALOR_TOTAL"]), Constantes.DECIMAIS_VALOR);
                    RegistroC370.Valor = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["VALOR_TOTAL"]), Constantes.DECIMAIS_VALOR);

                    ListaC370.Add(RegistroC370);
                }
                return ListaC370;
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


        public List<SpedFiscalC390VO> TabelaC390(int pId)
        {
            ConsultaSQL =
             "select * from VIEW_C390 " +
             "where ID = " + Convert.ToString(pId);

            try
            {
                List<SpedFiscalC390VO> ListaC390 = new List<SpedFiscalC390VO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    SpedFiscalC390VO RegistroC390 = new SpedFiscalC390VO();
                    RegistroC390.CST = Convert.ToString(leitor["CST"]);
                    RegistroC390.CFOP = Convert.ToInt32(leitor["CFOP"]);
                    RegistroC390.TaxaICMS = Convert.ToDecimal(leitor["TAXA_ICMS"]);
                    RegistroC390.SomaValor = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_ITEM"]), Constantes.DECIMAIS_VALOR);
                    RegistroC390.SomaBaseICMS = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_BASE_ICMS"]), Constantes.DECIMAIS_VALOR);
                    RegistroC390.SomaICMS = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_ICMS"]), Constantes.DECIMAIS_VALOR);
                    RegistroC390.SomaICMSOutras = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_ICMS_OUTRAS"]), Constantes.DECIMAIS_VALOR);

                    ListaC390.Add(RegistroC390);
                }
                return ListaC390;
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


        public List<SpedFiscalC321VO> TabelaC321(string pDataInicio, string pDataFim)
        {
            ConsultaSQL =
              "select * from VIEW_C321 " +
              "where " +
              "DATA_EMISSAO between " +
              Biblioteca.QuotedStr(pDataInicio) + " and " + Biblioteca.QuotedStr(pDataFim);

            try
            {
                List<SpedFiscalC321VO> ListaC321 = new List<SpedFiscalC321VO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    SpedFiscalC321VO RegistroC321 = new SpedFiscalC321VO();
                    RegistroC321.IdProduto = Convert.ToInt32(leitor["ID_PRODUTO"]);
                    RegistroC321.SomaQuantidade = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_QUANTIDADE"]), Constantes.DECIMAIS_QUANTIDADE);
                    RegistroC321.SomaValor = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_ITEM"]), Constantes.DECIMAIS_VALOR);
                    RegistroC321.SomaDesconto = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_DESCONTO"]), Constantes.DECIMAIS_VALOR);
                    RegistroC321.SomaBaseICMS = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_BASE_ICMS"]), Constantes.DECIMAIS_VALOR);
                    RegistroC321.SomaICMS = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_ICMS"]), Constantes.DECIMAIS_VALOR);
                    RegistroC321.SomaPIS = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_PIS"]), Constantes.DECIMAIS_VALOR);
                    RegistroC321.SomaCOFINS = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_COFINS"]), Constantes.DECIMAIS_VALOR);
                    RegistroC321.DescricaoUnidade = Convert.ToString(leitor["DESCRICAO_UNIDADE"]);

                    ListaC321.Add(RegistroC321);
                }
                return ListaC321;
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


        public List<SpedFiscalC425VO> TabelaC425(string pDataInicio, string pDataFim, string pTotalizadorParcial)
        {
            ConsultaSQL =
              "select * from VIEW_C425 " +
              "where " +
              "DATA_VENDA between " +
              Biblioteca.QuotedStr(pDataInicio) + " and " + Biblioteca.QuotedStr(pDataFim) +
              " and TOTALIZADOR_PARCIAL = " + Biblioteca.QuotedStr(pTotalizadorParcial);

            try
            {
                List<SpedFiscalC425VO> ListaC425 = new List<SpedFiscalC425VO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    SpedFiscalC425VO RegistroC425 = new SpedFiscalC425VO();
                    RegistroC425.IdProduto = Convert.ToInt32(leitor["ID_ECF_PRODUTO"]);
                    RegistroC425.IdUnidade = Convert.ToInt32(leitor["ID_UNIDADE_PRODUTO"]);
                    RegistroC425.SomaQuantidade = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_QUANTIDADE"]), Constantes.DECIMAIS_QUANTIDADE);
                    RegistroC425.SomaValor = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_ITEM"]), Constantes.DECIMAIS_VALOR);
                    RegistroC425.SomaPIS = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_PIS"]), Constantes.DECIMAIS_VALOR);
                    RegistroC425.SomaCOFINS = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_COFINS"]), Constantes.DECIMAIS_VALOR);
                    RegistroC425.DescricaoUnidade = Convert.ToString(leitor["DESCRICAO_UNIDADE"]);

                    ListaC425.Add(RegistroC425);
                }
                return ListaC425;
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


        public List<SpedFiscalC490VO> TabelaC490(string pDataInicio, string pDataFim)
        {
            ConsultaSQL = "select * from VIEW_C490 " +
              "where " +
              "DATA_VENDA = " + Biblioteca.QuotedStr(pDataInicio);

            try
            {
                List<SpedFiscalC490VO> ListaC490 = new List<SpedFiscalC490VO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    SpedFiscalC490VO RegistroC490 = new SpedFiscalC490VO();
                    RegistroC490.CST = Convert.ToString(leitor["CST"]);
                    RegistroC490.CFOP = Convert.ToInt32(leitor["CFOP"]);
                    RegistroC490.TaxaICMS = Convert.ToDecimal(leitor["TAXA_ICMS"]);
                    RegistroC490.SomaValor = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_ITEM"]), Constantes.DECIMAIS_VALOR);
                    RegistroC490.SomaBaseICMS = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_BASE_ICMS"]), Constantes.DECIMAIS_VALOR);
                    RegistroC490.SomaICMS = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_ICMS"]), Constantes.DECIMAIS_VALOR);

                    ListaC490.Add(RegistroC490);
                }
                return ListaC490;
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



        public List<MeiosPagamentoVO> TabelaE110(string pDataInicio, string pDataFim)
        {
            ConsultaSQL =
                          "select sum(soma_icms) as soma_icms from VIEW_E110 " +
                          "where " +
                          "DATA_EMISSAO between " +
                          Biblioteca.QuotedStr(pDataInicio) + " and " + Biblioteca.QuotedStr(pDataFim) +
                          " group by extract(year from data_emissao), extract(month from data_emissao)";

            try
            {
                List<MeiosPagamentoVO> ListaE110 = new List<MeiosPagamentoVO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    MeiosPagamentoVO MeiosPagamento = new MeiosPagamentoVO();
                    MeiosPagamento.DataHora = DateTime.Now;//TODO:  Passe a data corretamente
                    MeiosPagamento.Total = Convert.ToDecimal(leitor["SOMA_ICMS"]);
                    ListaE110.Add(MeiosPagamento);
                }
                return ListaE110;
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

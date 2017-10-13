/********************************************************************************
Title: T2TiPDV
Description: Classe de controle dos registros R

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
Observações importantes

Registro tipo R01 - Identificação do ECF, do Usu?rio, do PAF-ECF e da Empresa Desenvolvedora e Dados do Arquivo;
Registro tipo R02 - Relação de Reduções Z;
Registro tipo R03 - Detalhe da Reduçõeo Z;
Registro tipo R04 - Cupom Fiscal, Nota Fiscal de Venda a Consumidor ou Bilhete de Passagem;
Registro tipo R05 - Detalhe do Cupom Fiscal, da Nota Fiscal de Venda a Consumidor ou do Bilhete de Passagem;
Registro tipo R06 - Demais documentos emitidos pelo ECF;
Registro tipo R07 - Detalhe do Cupom Fiscal e do Documento Não Fiscal - Meio de Pagamento;
Registro EAD - Assinatura digital.

Numa venda com cartão teremos:
-Um R04 referente ao Cupom Fiscal (já gravamos no venda_cabecalho)
-Um R05 para cada item vendido  (já gravamos no venda_detalhe)
-Um R06 para o Comprovante de Crédito ou D?bito (o CCD se encaixa como "outros documentos emitidos");
-Um R07 referente à forma de pagamento utilizada no Cupom Fiscal, no caso, Cartão.
********************************************************************************/


using System;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data;
using PafEcf.Util;
using PafEcf.Infra;
using PafEcf.VO;
using PafEcf.View;
using System.Collections.Generic;

namespace PafEcf.Controller
{

    public class RegistroRController
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public RegistroRController()
        {
            conexao = dbConnection.conectar();
        }


        public R02VO GravaR02(R02VO R02)
        {
            string Tripa, Hash;

            ConsultaSQL =
              "insert into R02 (" +
              "ID_OPERADOR," +
              "ID_IMPRESSORA," +
              "ID_ECF_CAIXA," +
              "SERIE_ECF," +
              "CRZ," +
              "COO," +
              "CRO," +
              "DATA_MOVIMENTO," +
              "DATA_EMISSAO," +
              "HORA_EMISSAO," +
              "VENDA_BRUTA," +
              "GRANDE_TOTAL) values (" +
              "apidoperador," +
              "?pIdImpressora," +
              "?pIdCaixa," +
              "?pSerieEcf," +
              "?pCRZ," +
              "?pCOO," +
              "?pCRO," +
              "?pDataMovimento," +
              "?pDataEmissao," +
              "?pHoraEmissao," +
              "?pVendaBruta," +
              "?pGrandeTotal)";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pIdOperador", R02.IdOperador);
                comando.Parameters.AddWithValue("?pIdImpressora", R02.IdImpressora);
                comando.Parameters.AddWithValue("?pIdCaixa", R02.IdCaixa);
                comando.Parameters.AddWithValue("?pSerieEcf", R02.SerieEcf);
                comando.Parameters.AddWithValue("?pCRZ", R02.CRZ);
                comando.Parameters.AddWithValue("?pCOO", R02.COO);
                comando.Parameters.AddWithValue("?pCRO", R02.CRO);
                comando.Parameters.AddWithValue("?pDataMovimento", R02.DataMovimento);
                comando.Parameters.AddWithValue("?pDataEmissao", R02.DataEmissao);
                comando.Parameters.AddWithValue("?pHoraEmissao", R02.HoraEmissao);
                comando.Parameters.AddWithValue("?pVendaBruta", R02.VendaBruta);
                comando.Parameters.AddWithValue("?pGrandeTotal", R02.GrandeTotal);
                comando.ExecuteNonQuery();

                ConsultaSQL = "select max(ID) as ID from R02";
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();
                R02.Id = Convert.ToInt32(leitor["ID"]);
                leitor.Close();

                // calcula e grava o hash
                Tripa = Convert.ToString(R02.Id) +
                          Convert.ToString(R02.IdOperador) +
                          Convert.ToString(R02.IdImpressora) +
                          Convert.ToString(R02.IdCaixa) +
                          Convert.ToString(R02.CRZ) +
                          Convert.ToString(R02.COO) +
                          Convert.ToString(R02.CRO) +
                          R02.DataMovimento +
                          R02.DataEmissao +
                          R02.HoraEmissao +
                          Biblioteca.FormataFloat("V", R02.VendaBruta) +
                          Biblioteca.FormataFloat("V", R02.GrandeTotal) +
                          R02.SerieEcf +
                          "0";

                Hash = Biblioteca.MD5String(Tripa);

                ConsultaSQL =
                  "update R02 set " +
                  "HASH_TRIPA=?pHash, " +
                  "HASH_INCREMENTO = ?pHashIncremento " +
                  " where ID = ?pId";

                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pHash", Hash);
                comando.Parameters.AddWithValue("?pHashIncremento", -1);
                comando.Parameters.AddWithValue("?pId", R02.Id);
                comando.ExecuteNonQuery();

                return R02;
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


        public void GravaR03(List<R03VO> ListaR03)
        {
            string Tripa, Hash;

            try
            {
                for (int i = 0; i <= ListaR03.Count - 1; i++)
                {
                    ConsultaSQL =
                    "insert into R03 (" +
                    "ID_R02," +
                    "CRZ," +
                    "SERIE_ECF," +
                    "TOTALIZADOR_PARCIAL," +
                    "VALOR_ACUMULADO, HASH_TRIPA) values (" +
                    "?pIdR02," +
                    "?pCRZ," +
                    "?pSerieEcf," +
                    "?pTotalizadorParcial," +
                    "?pValorAcumulado," +
                    "?pHash)";

                    // calcula e grava o hash
                    Tripa = ListaR03[i].TotalizadorParcial +
                             Biblioteca.FormataFloat("V", ListaR03[i].ValorAcumulado) +
                             Convert.ToString(ListaR03[i].CRZ) +
                             ListaR03[i].SerieEcf +
                             "0";
                    Hash = Biblioteca.MD5String(Tripa);

                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    comando.Parameters.AddWithValue("?pIdR02", ListaR03[i].IdR02);
                    comando.Parameters.AddWithValue("?pCRZ", ListaR03[i].CRZ);
                    comando.Parameters.AddWithValue("?pSerieEcf", ListaR03[i].SerieEcf);
                    comando.Parameters.AddWithValue("?pTotalizadorParcial", ListaR03[i].TotalizadorParcial);
                    comando.Parameters.AddWithValue("?pValorAcumulado", ListaR03[i].ValorAcumulado);
                    comando.Parameters.AddWithValue("?pHash", Hash);
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


        public void GravaR06(R06VO R06)
        {
            string Tripa, Hash;

            ConsultaSQL =
            "insert into R06 (" +
            "ID_OPERADOR," +
            "ID_IMPRESSORA," +
            "ID_ECF_CAIXA," +
            "SERIE_ECF," +
            "COO," +
            "GNF," +
            "GRG," +
            "CDC," +
            "DENOMINACAO," +
            "DATA_EMISSAO," +
            "HORA_EMISSAO," +
            "HASH_TRIPA) values (" +
            "?pIdoperador," +
            "?pIdImpressora," +
            "?pIdCaixa," +
            "?pSerieEcf," +
            "?pCOO," +
            "?pGNF," +
            "?pGRG," +
            "?pCDC," +
            "?pDenominacao," +
            "?pDataEmissao," +
            "?pHoraEmissao," +
            "?pHash)";

            try
            {
                // calcula e grava o hash
                Tripa = Convert.ToString(R06.COO) +
                         Convert.ToString(R06.GNF) +
                         Convert.ToString(R06.GRG) +
                         Convert.ToString(R06.CDC) +
                         R06.Denominacao +
                         R06.DataEmissao +
                         R06.HoraEmissao +
                         R06.SerieEcf +
                         "0";

                Hash = Biblioteca.MD5String(Tripa);

                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pIdOperador", R06.IdOperador);
                comando.Parameters.AddWithValue("?pIdImpressora", R06.IdImpressora);
                comando.Parameters.AddWithValue("?pIdCaixa", R06.IdCaixa);
                comando.Parameters.AddWithValue("?pSerieEcf", R06.SerieEcf);
                comando.Parameters.AddWithValue("?pGNF", R06.GNF);
                comando.Parameters.AddWithValue("?pCOO", R06.COO);
                comando.Parameters.AddWithValue("?pGRG", R06.GRG);
                comando.Parameters.AddWithValue("?pCDC", R06.CDC);
                comando.Parameters.AddWithValue("?pDataEmissao", R06.DataEmissao);
                comando.Parameters.AddWithValue("?pHoraEmissao", R06.HoraEmissao);
                comando.Parameters.AddWithValue("?pDenominacao", R06.Denominacao);
                comando.Parameters.AddWithValue("?pHash", Hash);
                comando.ExecuteNonQuery();
            }

            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        public List<R02VO> TabelaR02(string pDataInicio, string pDataFim, int pIdImpressora)
        {

            ConsultaSQL = "select * from R02 where " +
            "ID_IMPRESSORA=" + Convert.ToString(pIdImpressora) +
            " and (DATA_MOVIMENTO between " +
            Biblioteca.QuotedStr(pDataInicio) + " and " + Biblioteca.QuotedStr(pDataFim) + ")";

            try
            {
                List<R02VO> ListaR02 = new List<R02VO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    R02VO R02 = new R02VO();
                    R02.Id = Convert.ToInt32(leitor["ID"]);
                    R02.IdOperador = Convert.ToInt32(leitor["ID_OPERADOR"]);
                    R02.IdImpressora = Convert.ToInt32(leitor["ID_IMPRESSORA"]);
                    R02.IdCaixa = Convert.ToInt32(leitor["ID_ECF_CAIXA"]);
                    R02.SerieEcf = Convert.ToString(leitor["SERIE_ECF"]);
                    R02.CRZ = Convert.ToInt32(leitor["CRZ"]);
                    R02.COO = Convert.ToInt32(leitor["COO"]);
                    R02.CRO = Convert.ToInt32(leitor["CRO"]);
                    R02.DataMovimento = (DateTime)(leitor["DATA_MOVIMENTO"]);
                    R02.DataEmissao = (DateTime)(leitor["DATA_EMISSAO"]);
                    R02.HoraEmissao = Convert.ToString(leitor["HORA_EMISSAO"]);
                    R02.VendaBruta = Convert.ToDecimal(leitor["VENDA_BRUTA"]);
                    R02.GrandeTotal = Convert.ToDecimal(leitor["GRANDE_TOTAL"]);
                    R02.HashTripa = Convert.ToString(leitor["HASH_TRIPA"]);
                    R02.HashIncremento = Convert.ToInt32(leitor["HASH_INCREMENTO"]);
                    ListaR02.Add(R02);
                }
                return ListaR02;
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


        public List<R02VO> TabelaR02(int pIdImpressora)
        {
            ConsultaSQL = "select * from R02 where ID_IMPRESSORA=" + Convert.ToString(pIdImpressora);
            try
            {
                List<R02VO> ListaR02 = new List<R02VO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    R02VO R02 = new R02VO();
                    R02.Id = Convert.ToInt32(leitor["ID"]);
                    R02.IdOperador = Convert.ToInt32(leitor["ID_OPERADOR"]);
                    R02.IdImpressora = Convert.ToInt32(leitor["ID_IMPRESSORA"]);
                    R02.IdCaixa = Convert.ToInt32(leitor["ID_ECF_CAIXA"]);
                    R02.SerieEcf = Convert.ToString(leitor["SERIE_ECF"]);
                    R02.CRZ = Convert.ToInt32(leitor["CRZ"]);
                    R02.COO = Convert.ToInt32(leitor["COO"]);
                    R02.CRO = Convert.ToInt32(leitor["CRO"]);
                    R02.DataMovimento = (DateTime)(leitor["DATA_MOVIMENTO"]);
                    R02.DataEmissao = (DateTime)(leitor["DATA_EMISSAO"]);
                    R02.HoraEmissao = Convert.ToString(leitor["HORA_EMISSAO"]);
                    R02.VendaBruta = Convert.ToDecimal(leitor["VENDA_BRUTA"]);
                    R02.GrandeTotal = Convert.ToDecimal(leitor["GRANDE_TOTAL"]);
                    R02.HashTripa = Convert.ToString(leitor["HASH_TRIPA"]);
                    R02.HashIncremento = Convert.ToInt32(leitor["HASH_INCREMENTO"]);
                    ListaR02.Add(R02);
                }
                return ListaR02;
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


        public List<R03VO> TabelaR03(int pIdR02)
        {
            ConsultaSQL = "select * from R03 where ID_R02=" + Convert.ToString(pIdR02);
            try
            {
                List<R03VO> ListaR03 = new List<R03VO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    R03VO R03 = new R03VO();
                    R03.Id = Convert.ToInt32(leitor["ID"]);
                    R03.IdR02 = Convert.ToInt32(leitor["ID_R02"]);
                    R03.SerieEcf = Convert.ToString(leitor["SERIE_ECF"]);
                    R03.TotalizadorParcial = Convert.ToString(leitor["TOTALIZADOR_PARCIAL"]);
                    R03.ValorAcumulado = Convert.ToDecimal(leitor["VALOR_ACUMULADO"]);
                    R03.CRZ = Convert.ToInt32(leitor["CRZ"]);
                    R03.HashTripa = Convert.ToString(leitor["HASH_TRIPA"]);
                    R03.HashIncremento = Convert.ToInt32(leitor["HASH_INCREMENTO"]);
                    ListaR03.Add(R03);
                }
                return ListaR03;

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


        public List<R04VO> TabelaR04(string pDataInicio, string pDataFim, int pIdImpressora)
        {
            ConsultaSQL =
              "select * from VIEW_R04 " +
              "where ID_ECF_IMPRESSORA=" + Convert.ToString(pIdImpressora) +
              " and (DATA_VENDA between " +
              Biblioteca.QuotedStr(pDataInicio) + " and " + Biblioteca.QuotedStr(pDataFim) + ")";

            try
            {

                List<R04VO> ListaR04 = new List<R04VO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    R04VO R04 = new R04VO();
                    R04.Id = Convert.ToInt32(leitor["VCID"]);
                    R04.IdOperador = Convert.ToInt32(leitor["ID_ECF_OPERADOR"]);
                    R04.SerieEcf = Convert.ToString(leitor["SERIE_ECF"]);
                    R04.CCF = Convert.ToInt32(leitor["CCF"]);
                    R04.COO = Convert.ToInt32(leitor["COO"]);
                    R04.DataEmissao = (DateTime)(leitor["DATA_VENDA"]);
                    R04.SubTotal = Convert.ToDecimal(leitor["VALOR_VENDA"]);
                    R04.Desconto = Convert.ToDecimal(leitor["DESCONTO"]);
                    R04.IndicadorDesconto = "V";
                    R04.Acrescimo = Convert.ToDecimal(leitor["ACRESCIMO"]);
                    R04.IndicadorAcrescimo = "V";
                    R04.ValorLiquido = Convert.ToDecimal(leitor["VALOR_FINAL"]);
                    R04.PIS = Convert.ToDecimal(leitor["PIS"]);
                    R04.COFINS = Convert.ToDecimal(leitor["COFINS"]);
                    R04.Cancelado = Convert.ToString(leitor["CUPOM_CANCELADO"]);
                    R04.CancelamentoAcrescimo = 0;
                    R04.OrdemDescontoAcrescimo = "D";
                    R04.Cliente = Convert.ToString(leitor["NOME_CLIENTE"]);
                    R04.CPFCNPJ = Convert.ToString(leitor["CPF_CNPJ_CLIENTE"]);
                    R04.HashTripa = Convert.ToString(leitor["HASH_TRIPA"]);
                    R04.HashIncremento = Convert.ToInt32(leitor["HASH_INCREMENTO"]);
                    R04.StatusVenda = Convert.ToString(leitor["STATUS_VENDA"]);
                    ListaR04.Add(R04);
                }
                return ListaR04;
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


        public List<R04VO> TabelaR04(string pDataInicio, string pDataFim)
        {
            ConsultaSQL =
              "select * from VIEW_R04 " +
              " where DATA_HORA_VENDA between " +
              Biblioteca.QuotedStr(pDataInicio) + " and " + Biblioteca.QuotedStr(pDataFim);
            try
            {

                List<R04VO> ListaR04 = new List<R04VO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    R04VO R04 = new R04VO();
                    R04.Id = Convert.ToInt32(leitor["VCID"]);
                    R04.IdOperador = Convert.ToInt32(leitor["ID_ECF_OPERADOR"]);
                    R04.SerieEcf = Convert.ToString(leitor["SERIE_ECF"]);
                    R04.CCF = Convert.ToInt32(leitor["CCF"]);
                    R04.COO = Convert.ToInt32(leitor["COO"]);
                    R04.DataEmissao = (DateTime)(leitor["DATA_VENDA"]);
                    R04.SubTotal = Convert.ToDecimal(leitor["VALOR_VENDA"]);
                    R04.Desconto = Convert.ToDecimal(leitor["DESCONTO"]);
                    R04.IndicadorDesconto = "V";
                    R04.Acrescimo = Convert.ToDecimal(leitor["ACRESCIMO"]);
                    R04.IndicadorAcrescimo = "V";
                    R04.ValorLiquido = Convert.ToDecimal(leitor["VALOR_FINAL"]);
                    R04.PIS = Convert.ToDecimal(leitor["PIS"]);
                    R04.COFINS = Convert.ToDecimal(leitor["COFINS"]);
                    R04.Cancelado = Convert.ToString(leitor["CUPOM_CANCELADO"]);
                    R04.CancelamentoAcrescimo = 0;
                    R04.OrdemDescontoAcrescimo = "D";
                    R04.Cliente = Convert.ToString(leitor["NOME_CLIENTE"]);
                    R04.CPFCNPJ = Convert.ToString(leitor["CPF_CNPJ_CLIENTE"]);
                    R04.HashTripa = Convert.ToString(leitor["HASH_TRIPA"]);
                    R04.HashIncremento = Convert.ToInt32(leitor["HASH_INCREMENTO"]);
                    R04.StatusVenda = Convert.ToString(leitor["STATUS_VENDA"]);
                    ListaR04.Add(R04);
                }
                return ListaR04;
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


        public List<R05VO> TabelaR05(int pId, string pQuemChamou)
        {
            if (pQuemChamou == "Sped")
            {
                ConsultaSQL =
                  "select * from VIEW_R05 " +
                  "where cancelado <> " + Biblioteca.QuotedStr("S") + " and VCID=" + Convert.ToString(pId);
            }
            else if (pQuemChamou == "Paf")
            {
                ConsultaSQL =
                  "select * from VIEW_R05 " +
                  "where VCID=" + Convert.ToString(pId);
            }
            try
            {
                List<R05VO> ListaR05 = new List<R05VO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    R05VO R05 = new R05VO();

                    R05.Id = Convert.ToInt32(leitor["VID"]);
                    R05.Item = Convert.ToInt32(leitor["ITEM"]);
                    R05.SerieEcf = Convert.ToString(leitor["SERIE_ECF"]);
                    R05.IdProduto = Convert.ToInt32(leitor["ID_ECF_PRODUTO"]);
                    R05.GTIN = Convert.ToString(leitor["GTIN"]);
                    R05.CCF = Convert.ToInt32(leitor["CCF"]);
                    R05.COO = Convert.ToInt32(leitor["COO"]);
                    R05.DescricaoPDV = Convert.ToString(leitor["DESCRICAO_PDV"]);
                    R05.Quantidade = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["QUANTIDADE"]), Constantes.DECIMAIS_QUANTIDADE);
                    R05.IdUnidade = Convert.ToInt32(leitor["ID_UNIDADE"]);
                    R05.SiglaUnidade = Convert.ToString(leitor["SIGLA_UNIDADE"]);
                    R05.ValorUnitario = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["VALOR_UNITARIO"]), Constantes.DECIMAIS_VALOR);
                    R05.Desconto = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["DESCONTO"]), Constantes.DECIMAIS_VALOR);
                    R05.Acrescimo = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["ACRESCIMO"]), Constantes.DECIMAIS_VALOR);
                    R05.TotalItem = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["TOTAL_ITEM"]), Constantes.DECIMAIS_VALOR);
                    R05.TotalizadorParcial = Convert.ToString(leitor["TOTALIZADOR_PARCIAL"]);
                    R05.IndicadorCancelamento = Convert.ToString(leitor["CANCELADO"]);
                    if (R05.IndicadorCancelamento == "S")
                        R05.QuantidadeCancelada = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["QUANTIDADE"]), Constantes.DECIMAIS_QUANTIDADE);
                    else
                        R05.QuantidadeCancelada = 0;
                    if (R05.IndicadorCancelamento == "S")
                        R05.ValorCancelado = Convert.ToDecimal(leitor["TOTAL_ITEM"]);
                    else
                        R05.ValorCancelado = 0;
                    R05.CancelamentoAcrescimo = 0;
                    R05.IAT = Convert.ToString(leitor["IAT"]);
                    R05.IPPT = Convert.ToString(leitor["IPPT"]);
                    R05.CasasDecimaisQuantidade = 3;
                    R05.CasasDecimaisValor = 2;
                    R05.CST = Convert.ToString(leitor["CST"]);
                    R05.CFOP = Convert.ToInt32(leitor["CFOP"]);
                    R05.AliquotaICMS = Convert.ToDecimal(leitor["TAXA_ICMS"]);
                    R05.PIS = Convert.ToDecimal(leitor["PIS"]);
                    R05.COFINS = Convert.ToDecimal(leitor["COFINS"]);
                    R05.HashTripa = Convert.ToString(leitor["HASH_TRIPA"]);
                    R05.HashIncremento = Convert.ToInt32(leitor["HASH_INCREMENTO"]);

                    ListaR05.Add(R05);
                }
                return ListaR05;

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


        public List<R06VO> TabelaR06(string pDataInicio, string pDataFim, int pIdImpressora)
        {
            ConsultaSQL = "select * from R06 where " +
            "ID_IMPRESSORA=" + Convert.ToString(pIdImpressora) +
            " and (DATA_EMISSAO between " +
              Biblioteca.QuotedStr(pDataInicio) + " and " + Biblioteca.QuotedStr(pDataFim) + ")";

            try
            {
                List<R06VO> ListaR06 = new List<R06VO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    R06VO R06 = new R06VO();
                    R06.Id = Convert.ToInt32(leitor["ID"]);
                    R06.IdOperador = Convert.ToInt32(leitor["ID_OPERADOR"]);
                    R06.IdImpressora = Convert.ToInt32(leitor["ID_IMPRESSORA"]);
                    R06.IdCaixa = Convert.ToInt32(leitor["ID_ECF_CAIXA"]);
                    R06.COO = Convert.ToInt32(leitor["COO"]);
                    R06.GNF = Convert.ToInt32(leitor["GNF"]);
                    R06.GRG = Convert.ToInt32(leitor["GRG"]);
                    R06.CDC = Convert.ToInt32(leitor["CDC"]);
                    R06.Denominacao = Convert.ToString(leitor["DENOMINACAO"]);
                    R06.DataEmissao = Convert.ToDateTime(Convert.ToString(leitor["DATA_EMISSAO"]));
                    R06.HoraEmissao = Convert.ToString(leitor["HORA_EMISSAO"]);
                    R06.SerieEcf = Convert.ToString(leitor["SERIE_ECF"]);
                    R06.HashTripa = Convert.ToString(leitor["HASH_TRIPA"]);
                    R06.HashIncremento = Convert.ToInt32(leitor["HASH_INCREMENTO"]);
                    ListaR06.Add(R06);
                }
                return ListaR06;
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


        public List<R07VO> TabelaR07IdR04(int pId)
        {
            ConsultaSQL =
              "select " +
              "VC.ID AS VCID, TTP.HASH_TRIPA, TTP.HASH_INCREMENTO, TTP.CCF, TTP.COO, TTP.GNF, " +
              "TTP.SERIE_ECF, TP.DESCRICAO, TTP.VALOR, TTP.ESTORNO " +
              "from " +
              "ECF_VENDA_CABECALHO VC, ECF_TIPO_PAGAMENTO TP, ECF_TOTAL_TIPO_PGTO TTP " +
              "where " +
              "TTP.ID_ECF_VENDA_CABECALHO = VC.ID " +
              "and TTP.ID_ECF_TIPO_PAGAMENTO = TP.ID " +
              "and VC.ID = " + Convert.ToString(pId);

            try
            {
                List<R07VO> ListaR07 = new List<R07VO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    R07VO R07 = new R07VO();
                    R07.Coo = Convert.ToInt32(leitor["COO"]);
                    R07.Ccf = Convert.ToInt32(leitor["CCF"]);
                    R07.Gnf = Convert.ToInt32(leitor["GNF"]);
                    R07.SerieEcf = Convert.ToString(leitor["SERIE_ECF"]);
                    R07.HashTripa = Convert.ToString(leitor["HASH_TRIPA"]);
                    R07.HashIncremento = Convert.ToInt32(leitor["HASH_INCREMENTO"]);
                    R07.MeioPagamento = Convert.ToString(leitor["DESCRICAO"]);
                    R07.ValorPagamento = Convert.ToDecimal(leitor["VALOR"]);
                    R07.IndicadorEstorno = Convert.ToString(leitor["ESTORNO"]);
                    R07.ValorEstorno = 0;
                    ListaR07.Add(R07);
                }
                return ListaR07;


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


        public R01VO RegistroR01()
        {

            ConsultaSQL = "select * from R01";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();

                R01VO R01 = new R01VO();
                R01.Id = Convert.ToInt32(leitor["ID"]);
                R01.SerieEcf = Convert.ToString(leitor["SERIE_ECF"]);
                R01.CnpjEmpresa = Convert.ToString(leitor["CNPJ_EMPRESA"]);
                R01.CnpjSh = Convert.ToString(leitor["CNPJ_SH"]);
                R01.InscricaoEstadualSh = Convert.ToString(leitor["INSCRICAO_ESTADUAL_SH"]);
                R01.InscricaoMunicipalSh = Convert.ToString(leitor["INSCRICAO_MUNICIPAL_SH"]);
                R01.DenominacaoSh = Convert.ToString(leitor["DENOMINACAO_SH"]);
                R01.NomePafEcf = Convert.ToString(leitor["NOME_PAF_ECF"]);
                R01.VersaoPafEcf = Convert.ToString(leitor["VERSAO_PAF_ECF"]);
                R01.Md5PafEcf = Convert.ToString(leitor["MD5_PAF_ECF"]);
                R01.DataInicial = Convert.ToDateTime(leitor["DATA_INICIAL"]);
                R01.DataFinal = Convert.ToDateTime(leitor["DATA_FINAL"]);
                R01.VersaoEr = Convert.ToString(leitor["VERSAO_ER"]);
                R01.NumeroLaudoPaf = Convert.ToString(leitor["NUMERO_LAUDO_PAF"]);
                R01.RazaoSocialSh = Convert.ToString(leitor["RAZAO_SOCIAL_SH"]);
                R01.EnderecoSh = Convert.ToString(leitor["ENDERECO_SH"]);
                R01.NumeroSh = Convert.ToString(leitor["NUMERO_SH"]);
                R01.ComplementoSh = Convert.ToString(leitor["COMPLEMENTO_SH"]);
                R01.BairroSh = Convert.ToString(leitor["BAIRRO_SH"]);
                R01.CidadeSh = Convert.ToString(leitor["CIDADE_SH"]);
                R01.CepSh = Convert.ToString(leitor["CEP_SH"]);
                R01.UfSh = Convert.ToString(leitor["UF_SH"]);
                R01.TelefoneSh = Convert.ToString(leitor["TELEFONE_SH"]);
                R01.ContatoSh = Convert.ToString(leitor["CONTATO_SH"]);
                R01.PrincipalExecutavel = Convert.ToString(leitor["PRINCIPAL_EXECUTAVEL"]);
                R01.HashTripa = Convert.ToString(leitor["HASH_TRIPA"]);
                R01.HashIncremento = Convert.ToInt32(leitor["HASH_INCREMENTO"]);

                return R01;
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

        public int TotalR02(string pDataInicio, string pDataFim)
        {
            int TotalRegistros;

            ConsultaSQL =
              "select count(*) as total from r02 " +
              "where data_movimento between " +
              Biblioteca.QuotedStr(pDataInicio) + " and " + Biblioteca.QuotedStr(pDataFim) + ")";
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                TotalRegistros = Convert.ToInt32(leitor["TOTAL"]);

                return TotalRegistros;
            }

            catch (Exception eError)
            {
                Log.write(eError.ToString());
                return 0;
            }
            finally
            {
                if (leitor != null)
                    leitor.Close();
            }
        }
    }

}

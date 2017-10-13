/********************************************************************************
Title: T2TiPDV
Description: Classe de controle da NF2

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

    public class NotaFiscalController
    {

        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public NotaFiscalController()
        {
            conexao = dbConnection.conectar();
        }


        public NotaFiscalCabecalhoVO TabelaNotaFiscalCabecalho(string pNumeroNota)
        {
            ConsultaSQL =
              "select * from NOTA_FISCAL_CABECALHO where " +
              "NUMERO =" + Biblioteca.QuotedStr(pNumeroNota);
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();

                if (leitor.HasRows)
                {
                    NotaFiscalCabecalhoVO NotaFiscalCabecalho = new NotaFiscalCabecalhoVO();
                    NotaFiscalCabecalho.Id = Convert.ToInt32(leitor["ID"]);
                    NotaFiscalCabecalho.IdEcfFuncionario = Convert.ToInt32(leitor["ID_ECF_FUNCIONARIO"]);
                    NotaFiscalCabecalho.IdCliente = Convert.ToInt32(leitor["ID_CLIENTE"]);
                    NotaFiscalCabecalho.Cfop = Convert.ToInt32(leitor["CFOP"]);
                    NotaFiscalCabecalho.Numero = Convert.ToString(leitor["NUMERO"]);
                    NotaFiscalCabecalho.DataEmissao = (DateTime)(Convert.ToDateTime(leitor["DATA_EMISSAO"]));
                    NotaFiscalCabecalho.HoraEmissao = Convert.ToString(leitor["HORA_EMISSAO"]);
                    NotaFiscalCabecalho.Serie = Convert.ToString(leitor["SERIE"]);
                    NotaFiscalCabecalho.Subserie = Convert.ToString(leitor["SUBSERIE"]);
                    NotaFiscalCabecalho.TotalProdutos = Convert.ToDecimal(leitor["TOTAL_PRODUTOS"]);
                    NotaFiscalCabecalho.TotalNf = Convert.ToDecimal(leitor["TOTAL_NF"]);
                    NotaFiscalCabecalho.BaseIcms = Convert.ToDecimal(leitor["BASE_ICMS"]);
                    NotaFiscalCabecalho.Icms = Convert.ToDecimal(leitor["ICMS"]);
                    NotaFiscalCabecalho.IcmsOutras = Convert.ToDecimal(leitor["ICMS_OUTRAS"]);
                    NotaFiscalCabecalho.Issqn = Convert.ToDecimal(leitor["ISSQN"]);
                    NotaFiscalCabecalho.Pis = Convert.ToDecimal(leitor["PIS"]);
                    NotaFiscalCabecalho.Cofins = Convert.ToDecimal(leitor["COFINS"]);
                    NotaFiscalCabecalho.Ipi = Convert.ToDecimal(leitor["IPI"]);
                    NotaFiscalCabecalho.TaxaAcrescimo = Convert.ToDecimal(leitor["TAXA_ACRESCIMO"]);
                    NotaFiscalCabecalho.Acrescimo = Convert.ToDecimal(leitor["ACRESCIMO"]);
                    NotaFiscalCabecalho.AcrescimoItens = Convert.ToDecimal(leitor["ACRESCIMO_ITENS"]);
                    NotaFiscalCabecalho.TaxaDesconto = Convert.ToDecimal(leitor["TAXA_DESCONTO"]);
                    NotaFiscalCabecalho.Desconto = Convert.ToDecimal(leitor["DESCONTO"]);
                    NotaFiscalCabecalho.DescontoItens = Convert.ToDecimal(leitor["DESCONTO_ITENS"]);
                    NotaFiscalCabecalho.Cancelada = Convert.ToString(leitor["CANCELADA"]);
                    NotaFiscalCabecalho.Sincronizado = Convert.ToString(leitor["SINCRONIZADO"]);
                    NotaFiscalCabecalho.TipoNota = Convert.ToString(leitor["TIPO_NOTA"]);
                    return NotaFiscalCabecalho;
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


        public List<NotaFiscalDetalheVO> TabelaNotaFiscalDetalhe(int pId)
        {
            List<NotaFiscalDetalheVO> ListaNotaFiscalDetalhe = new List<NotaFiscalDetalheVO>();

            ConsultaSQL = "select * from NOTA_FISCAL_DETALHE where ID_NF_CABECALHO=" + Convert.ToString(pId);
            try
            {

                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    NotaFiscalDetalheVO NotaFiscalDetalhe = new NotaFiscalDetalheVO();

                    NotaFiscalDetalhe.Id = Convert.ToInt32(leitor["ID"]);
                    NotaFiscalDetalhe.IdNfCabecalho = Convert.ToInt32(leitor["ID_NF_CABECALHO"]);
                    NotaFiscalDetalhe.IdProduto = Convert.ToInt32(leitor["ID_PRODUTO"]);
                    NotaFiscalDetalhe.Cfop = Convert.ToInt32(leitor["CFOP"]);
                    NotaFiscalDetalhe.Item = Convert.ToInt32(leitor["ITEM"]);
                    NotaFiscalDetalhe.Quantidade = Convert.ToDecimal(leitor["QUANTIDADE"]);
                    NotaFiscalDetalhe.ValorUnitario = Convert.ToDecimal(leitor["VALOR_UNITARIO"]);
                    NotaFiscalDetalhe.ValorTotal = Convert.ToDecimal(leitor["VALOR_TOTAL"]);
                    NotaFiscalDetalhe.BaseIcms = Convert.ToDecimal(leitor["BASE_ICMS"]);
                    NotaFiscalDetalhe.TaxaIcms = Convert.ToDecimal(leitor["TAXA_ICMS"]);
                    NotaFiscalDetalhe.Icms = Convert.ToDecimal(leitor["ICMS"]);
                    NotaFiscalDetalhe.IcmsOutras = Convert.ToDecimal(leitor["ICMS_OUTRAS"]);
                    NotaFiscalDetalhe.IcmsIsento = Convert.ToDecimal(leitor["ICMS_ISENTO"]);
                    NotaFiscalDetalhe.TaxaDesconto = Convert.ToDecimal(leitor["TAXA_DESCONTO"]);
                    NotaFiscalDetalhe.Desconto = Convert.ToDecimal(leitor["DESCONTO"]);
                    NotaFiscalDetalhe.TaxaIssqn = Convert.ToDecimal(leitor["TAXA_ISSQN"]);
                    NotaFiscalDetalhe.Issqn = Convert.ToDecimal(leitor["ISSQN"]);
                    NotaFiscalDetalhe.TaxaPis = Convert.ToDecimal(leitor["TAXA_PIS"]);
                    NotaFiscalDetalhe.Pis = Convert.ToDecimal(leitor["PIS"]);
                    NotaFiscalDetalhe.TaxaCofins = Convert.ToDecimal(leitor["TAXA_COFINS"]);
                    NotaFiscalDetalhe.Cofins = Convert.ToDecimal(leitor["COFINS"]);
                    NotaFiscalDetalhe.TaxaAcrescimo = Convert.ToDecimal(leitor["TAXA_ACRESCIMO"]);
                    NotaFiscalDetalhe.Acrescimo = Convert.ToDecimal(leitor["ACRESCIMO"]);
                    NotaFiscalDetalhe.TaxaIpi = Convert.ToDecimal(leitor["TAXA_IPI"]);
                    NotaFiscalDetalhe.Ipi = Convert.ToDecimal(leitor["IPI"]);
                    NotaFiscalDetalhe.Cancelado = Convert.ToString(leitor["CANCELADO"]);
                    NotaFiscalDetalhe.Cst = Convert.ToString(leitor["CST"]);
                    NotaFiscalDetalhe.MovimentaEstoque = Convert.ToString(leitor["MOVIMENTA_ESTOQUE"]);
                    NotaFiscalDetalhe.Sincronizado = Convert.ToString(leitor["SINCRONIZADO"]);

                    ListaNotaFiscalDetalhe.Add(NotaFiscalDetalhe);

                }
                return ListaNotaFiscalDetalhe;

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


        public int InsereNotaFiscal(NotaFiscalCabecalhoVO NotaFiscalCabecalho, List<NotaFiscalDetalheVO> ListaNotaFiscalDetalhe)
        {

            ExcluiNotaFiscal(NotaFiscalCabecalho.Numero);

            ConsultaSQL =
                "insert into NOTA_FISCAL_CABECALHO (" +
                  "ID_ECF_FUNCIONARIO, " +
                  "ID_CLIENTE, " +
                  "CFOP, " +
                  "NUMERO, " +
                  "DATA_EMISSAO, " +
                  "HORA_EMISSAO, " +
                  "SERIE, " +
                  "SUBSERIE, " +
                  "TOTAL_PRODUTOS, " +
                  "TOTAL_NF, " +
                  "BASE_ICMS, " +
                  "ICMS, " +
                  "ICMS_OUTRAS, " +
                  "ISSQN, " +
                  "PIS, " +
                  "COFINS, " +
                  "IPI, " +
                  "TAXA_ACRESCIMO, " +
                  "ACRESCIMO, " +
                  "ACRESCIMO_ITENS, " +
                  "TAXA_DESCONTO, " +
                  "DESCONTO, " +
                  "DESCONTO_ITENS, " +
                  "CANCELADA, " +
                  "TIPO_NOTA) " +
                "values (" +
                  "?pID_ECF_FUNCIONARIO, " +
                  "?pID_CLIENTE, " +
                  "?pCFOP, " +
                  "?pNUMERO, " +
                  "?pDATA_EMISSAO, " +
                  "?pHORA_EMISSAO, " +
                  "?pSERIE, " +
                  "?pSUBSERIE, " +
                  "?pTOTAL_PRODUTOS, " +
                  "?pTOTAL_NF, " +
                  "?pBASE_ICMS, " +
                  "?pICMS, " +
                  "?pICMS_OUTRAS, " +
                  "?pISSQN, " +
                  "?pPIS, " +
                  "?pCOFINS, " +
                  "?pIPI, " +
                  "?pTAXA_ACRESCIMO, " +
                  "?pACRESCIMO, " +
                  "?pACRESCIMO_ITENS, " +
                  "?pTAXA_DESCONTO, " +
                  "?pDESCONTO, " +
                  "?pDESCONTO_ITENS, " +
                  "?pCANCELADA, " +
                  "?pTIPO_NOTA) ";
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pID_ECF_FUNCIONARIO", NotaFiscalCabecalho.IdEcfFuncionario);
                comando.Parameters.AddWithValue("?pID_CLIENTE", NotaFiscalCabecalho.IdCliente);
                comando.Parameters.AddWithValue("?pCFOP", NotaFiscalCabecalho.Cfop);
                comando.Parameters.AddWithValue("?pNUMERO", NotaFiscalCabecalho.Numero);
                comando.Parameters.AddWithValue("?pDATA_EMISSAO", DateTime.Now);
                comando.Parameters.AddWithValue("?pHORA_EMISSAO", DateTime.Now.ToString("HH:mm:ss"));
                comando.Parameters.AddWithValue("?pSERIE", NotaFiscalCabecalho.Serie);
                comando.Parameters.AddWithValue("?pSUBSERIE", NotaFiscalCabecalho.Subserie);
                comando.Parameters.AddWithValue("?pTOTAL_PRODUTOS", NotaFiscalCabecalho.TotalProdutos);
                comando.Parameters.AddWithValue("?pTOTAL_NF", NotaFiscalCabecalho.TotalNf);
                comando.Parameters.AddWithValue("?pBASE_ICMS", NotaFiscalCabecalho.BaseIcms);
                comando.Parameters.AddWithValue("?pICMS", NotaFiscalCabecalho.Icms);
                comando.Parameters.AddWithValue("?pICMS_OUTRAS", NotaFiscalCabecalho.IcmsOutras);
                comando.Parameters.AddWithValue("?pISSQN", NotaFiscalCabecalho.Issqn);
                comando.Parameters.AddWithValue("?pPIS", NotaFiscalCabecalho.Pis);
                comando.Parameters.AddWithValue("?pCOFINS", NotaFiscalCabecalho.Cofins);
                comando.Parameters.AddWithValue("?pIPI", NotaFiscalCabecalho.Ipi);
                comando.Parameters.AddWithValue("?pTAXA_ACRESCIMO", NotaFiscalCabecalho.TaxaAcrescimo);
                comando.Parameters.AddWithValue("?pACRESCIMO", NotaFiscalCabecalho.Acrescimo);
                comando.Parameters.AddWithValue("?pACRESCIMO_ITENS", NotaFiscalCabecalho.AcrescimoItens);
                comando.Parameters.AddWithValue("?pTAXA_DESCONTO", NotaFiscalCabecalho.TaxaDesconto);
                comando.Parameters.AddWithValue("?pDESCONTO", NotaFiscalCabecalho.Desconto);
                comando.Parameters.AddWithValue("?pDESCONTO_ITENS", NotaFiscalCabecalho.DescontoItens);
                comando.Parameters.AddWithValue("?pCANCELADA", NotaFiscalCabecalho.Cancelada);
                comando.Parameters.AddWithValue("?pTIPO_NOTA", NotaFiscalCabecalho.TipoNota);
                comando.ExecuteNonQuery();

                ConsultaSQL = "select max(ID) as ID from NOTA_FISCAL_CABECALHO";
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();
                NotaFiscalCabecalho.Id = Convert.ToInt32(leitor["ID"]);
            }

            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }

            ConsultaSQL =
              "insert into NOTA_FISCAL_DETALHE (" +
                "ID_NF_CABECALHO, " +
                "ID_PRODUTO, " +
                "CFOP, " +
                "ITEM, " +
                "QUANTIDADE, " +
                "VALOR_UNITARIO, " +
                "VALOR_TOTAL, " +
                "BASE_ICMS, " +
                "TAXA_ICMS, " +
                "ICMS, " +
                "ICMS_OUTRAS, " +
                "ICMS_ISENTO, " +
                "TAXA_DESCONTO, " +
                "DESCONTO, " +
                "TAXA_ISSQN, " +
                "ISSQN, " +
                "TAXA_PIS, " +
                "PIS, " +
                "TAXA_COFINS, " +
                "COFINS, " +
                "TAXA_ACRESCIMO, " +
                "ACRESCIMO, " +
                "TAXA_IPI, " +
                "IPI, " +
                "CANCELADO, " +
                "CST, " +
                "ECF_ICMS_ST," +
                "MOVIMENTA_ESTOQUE) " +
              "values (" +
                "?pID_NF_CABECALHO, " +
                "?pID_PRODUTO, " +
                "?pCFOP, " +
                "?pITEM, " +
                "?pQUANTIDADE, " +
                "?pVALOR_UNITARIO, " +
                "?pVALOR_TOTAL, " +
                "?pBASE_ICMS, " +
                "?pTAXA_ICMS, " +
                "?pICMS, " +
                "?pICMS_OUTRAS, " +
                "?pICMS_ISENTO, " +
                "?pTAXA_DESCONTO, " +
                "?pDESCONTO, " +
                "?pTAXA_ISSQN, " +
                "?pISSQN, " +
                "?pTAXA_PIS, " +
                "?pPIS, " +
                "?pTAXA_COFINS, " +
                "?pCOFINS, " +
                "?pTAXA_ACRESCIMO, " +
                "?pACRESCIMO, " +
                "?pTAXA_IPI, " +
                "?pIPI, " +
                "?pCANCELADO, " +
                "?pCST, " +
                "?pECFIcmsST, " +
                "?pMOVIMENTA_ESTOQUE)";
            try
            {
                for (int i = 0; i <= ListaNotaFiscalDetalhe.Count - 1; i++)
                {
                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    comando.Parameters.AddWithValue("?pID_NF_CABECALHO", NotaFiscalCabecalho.Id);
                    comando.Parameters.AddWithValue("?pID_PRODUTO", ListaNotaFiscalDetalhe[i].IdProduto);
                    comando.Parameters.AddWithValue("?pCFOP", ListaNotaFiscalDetalhe[i].Cfop);
                    comando.Parameters.AddWithValue("?pITEM", ListaNotaFiscalDetalhe[i].Item);
                    comando.Parameters.AddWithValue("?pQUANTIDADE", ListaNotaFiscalDetalhe[i].Quantidade);
                    comando.Parameters.AddWithValue("?pVALOR_UNITARIO", ListaNotaFiscalDetalhe[i].ValorUnitario);
                    comando.Parameters.AddWithValue("?pVALOR_TOTAL", ListaNotaFiscalDetalhe[i].ValorTotal);
                    comando.Parameters.AddWithValue("?pBASE_ICMS", ListaNotaFiscalDetalhe[i].BaseIcms);
                    comando.Parameters.AddWithValue("?pTAXA_ICMS", ListaNotaFiscalDetalhe[i].TaxaIcms);
                    comando.Parameters.AddWithValue("?pICMS", ListaNotaFiscalDetalhe[i].Icms);
                    comando.Parameters.AddWithValue("?pICMS_OUTRAS", ListaNotaFiscalDetalhe[i].IcmsOutras);
                    comando.Parameters.AddWithValue("?pICMS_ISENTO", ListaNotaFiscalDetalhe[i].IcmsIsento);
                    comando.Parameters.AddWithValue("?pTAXA_DESCONTO", ListaNotaFiscalDetalhe[i].TaxaDesconto);
                    comando.Parameters.AddWithValue("?pDESCONTO", ListaNotaFiscalDetalhe[i].Desconto);
                    comando.Parameters.AddWithValue("?pTAXA_ISSQN", ListaNotaFiscalDetalhe[i].TaxaIssqn);
                    comando.Parameters.AddWithValue("?pISSQN", ListaNotaFiscalDetalhe[i].Issqn);
                    comando.Parameters.AddWithValue("?pTAXA_PIS", ListaNotaFiscalDetalhe[i].TaxaPis);
                    comando.Parameters.AddWithValue("?pPIS", ListaNotaFiscalDetalhe[i].Pis);
                    comando.Parameters.AddWithValue("?pTAXA_COFINS", ListaNotaFiscalDetalhe[i].TaxaCofins);
                    comando.Parameters.AddWithValue("?pCOFINS", ListaNotaFiscalDetalhe[i].Cofins);
                    comando.Parameters.AddWithValue("?pTAXA_ACRESCIMO", ListaNotaFiscalDetalhe[i].TaxaAcrescimo);
                    comando.Parameters.AddWithValue("?pACRESCIMO", ListaNotaFiscalDetalhe[i].Acrescimo);
                    comando.Parameters.AddWithValue("?pTAXA_IPI", ListaNotaFiscalDetalhe[i].TaxaIpi);
                    comando.Parameters.AddWithValue("?pIPI", ListaNotaFiscalDetalhe[i].Ipi);
                    comando.Parameters.AddWithValue("?pCANCELADO", ListaNotaFiscalDetalhe[i].Cancelado);
                    comando.Parameters.AddWithValue("?pCST", ListaNotaFiscalDetalhe[i].Cst);
                    comando.Parameters.AddWithValue("?pMOVIMENTA_ESTOQUE", ListaNotaFiscalDetalhe[i].MovimentaEstoque);
                    comando.ExecuteNonQuery();

                }


            }

            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }

            return NotaFiscalCabecalho.Id;
        }


        public List<NotaFiscalCabecalhoVO> ConsultaNFCabecalhoSPED(string pDataInicio, string pDataFim)
        {
            ConsultaSQL =
              " SELECT (SELECT g.numero FROM NOTA_FISCAL_CABECALHO g WHERE g.id IN (SELECT MIN(b.id) FROM NOTA_FISCAL_CABECALHO b WHERE " +
              " (b.DATA_EMISSAO BETWEEN " + Biblioteca.QuotedStr(pDataInicio) + " and " + Biblioteca.QuotedStr(pDataFim) + "))) AS minimo, " +
              " (SELECT F.NUMERO FROM NOTA_FISCAL_CABECALHO F WHERE F.ID IN  (SELECT MAX(s.ID)  FROM NOTA_FISCAL_CABECALHO s " +
              " WHERE (s.DATA_EMISSAO BETWEEN " + Biblioteca.QuotedStr(pDataInicio) + " and " + Biblioteca.QuotedStr(pDataFim) + "))) AS MAXIMO, N.*, C.* FROM NOTA_FISCAL_CABECALHO N, CLIENTE C WHERE " +
              " N.ID_CLIENTE = C.ID AND (n.DATA_EMISSAO BETWEEN " + Biblioteca.QuotedStr(pDataInicio) + " and " + Biblioteca.QuotedStr(pDataFim) + ")";
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();

                List<NotaFiscalCabecalhoVO> ListaNFCabecalho = new List<NotaFiscalCabecalhoVO>();

                while (leitor.Read())
                {
                    NotaFiscalCabecalhoVO NFCabecalho = new NotaFiscalCabecalhoVO();
                    NFCabecalho.Id = Convert.ToInt32(leitor["ID"]);
                    NFCabecalho.IdEcfFuncionario = Convert.ToInt32(leitor["ID_ECF_FUNCIONARIO"]);
                    NFCabecalho.IdCliente = Convert.ToInt32(leitor["ID_CLIENTE"]);
                    NFCabecalho.Cfop = Convert.ToInt32(leitor["CFOP"]);
                    NFCabecalho.Numero = Convert.ToString(leitor["NUMERO"]);
                    NFCabecalho.NumOrdemInicial = Convert.ToInt32(leitor["MINIMO"]);
                    NFCabecalho.NumOrdemFinal = Convert.ToInt32(leitor["MAXIMO"]);
                    NFCabecalho.DataEmissao = (DateTime)(leitor["DATA_EMISSAO"]);
                    NFCabecalho.HoraEmissao = Convert.ToString(leitor["HORA_EMISSAO"]);
                    NFCabecalho.Serie = Convert.ToString(leitor["SERIE"]);
                    NFCabecalho.Subserie = Convert.ToString(leitor["SUBSERIE"]);
                    NFCabecalho.TotalProdutos = Convert.ToDecimal(leitor["TOTAL_PRODUTOS"]);
                    NFCabecalho.TotalNf = Convert.ToDecimal(leitor["TOTAL_NF"]);
                    NFCabecalho.BaseIcms = Convert.ToDecimal(leitor["BASE_ICMS"]);
                    NFCabecalho.Icms = Convert.ToDecimal(leitor["ICMS"]);
                    NFCabecalho.IcmsOutras = Convert.ToDecimal(leitor["ICMS_OUTRAS"]);
                    NFCabecalho.Issqn = Convert.ToDecimal(leitor["ISSQN"]);
                    NFCabecalho.Pis = Convert.ToDecimal(leitor["PIS"]);
                    NFCabecalho.Cofins = Convert.ToDecimal(leitor["COFINS"]);
                    NFCabecalho.Ipi = Convert.ToDecimal(leitor["IPI"]);
                    NFCabecalho.TaxaAcrescimo = Convert.ToDecimal(leitor["TAXA_ACRESCIMO"]);
                    NFCabecalho.Acrescimo = Convert.ToDecimal(leitor["ACRESCIMO"]);
                    NFCabecalho.AcrescimoItens = Convert.ToDecimal(leitor["ACRESCIMO_ITENS"]);
                    NFCabecalho.TaxaDesconto = Convert.ToDecimal(leitor["TAXA_DESCONTO"]);
                    NFCabecalho.Desconto = Convert.ToDecimal(leitor["DESCONTO"]);
                    NFCabecalho.DescontoItens = Convert.ToDecimal(leitor["DESCONTO_ITENS"]);
                    NFCabecalho.Cancelada = Convert.ToString(leitor["CANCELADA"]);
                    NFCabecalho.CpfCnpjCliente = Convert.ToString(leitor["CPF_CNPJ"]);
                    ListaNFCabecalho.Add(NFCabecalho);
                }
                return ListaNFCabecalho;
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



        public List<NotaFiscalDetalheVO> ConsultaNFDetalheSPED(int pId)
        {
            ConsultaSQL = "select * from NOTA_FISCAL_DETALHE where ID=" + Convert.ToString(pId);
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();

                List<NotaFiscalDetalheVO> ListaNFDetalhe = new List<NotaFiscalDetalheVO>();

                while (leitor.Read())
                {
                    NotaFiscalDetalheVO NFDetalhe = new NotaFiscalDetalheVO();
                    NFDetalhe.Id = Convert.ToInt32(leitor["ID"]);
                    NFDetalhe.IdNfCabecalho = Convert.ToInt32(leitor["ID_NF_CABECALHO"]);
                    NFDetalhe.IdProduto = Convert.ToInt32(leitor["ID_PRODUTO"]);
                    NFDetalhe.Cfop = Convert.ToInt32(leitor["CFOP"]);
                    NFDetalhe.Item = Convert.ToInt32(leitor["ITEM"]);
                    NFDetalhe.Quantidade = Convert.ToDecimal(leitor["QUANTIDADE"]);
                    NFDetalhe.ValorUnitario = Convert.ToDecimal(leitor["VALOR_UNITARIO"]);
                    NFDetalhe.ValorTotal = Convert.ToDecimal(leitor["VALOR_TOTAL"]);
                    NFDetalhe.BaseIcms = Convert.ToDecimal(leitor["BASE_ICMS"]);
                    NFDetalhe.TaxaIcms = Convert.ToDecimal(leitor["TAXA_ICMS"]);
                    NFDetalhe.Icms = Convert.ToDecimal(leitor["ICMS"]);
                    NFDetalhe.IcmsOutras = Convert.ToDecimal(leitor["ICMS_OUTRAS"]);
                    NFDetalhe.IcmsIsento = Convert.ToDecimal(leitor["ICMS_ISENTO"]);
                    NFDetalhe.TaxaDesconto = Convert.ToDecimal(leitor["TAXA_DESCONTO"]);
                    NFDetalhe.Desconto = Convert.ToDecimal(leitor["DESCONTO"]);
                    NFDetalhe.TaxaIssqn = Convert.ToDecimal(leitor["TAXA_ISSQN"]);
                    NFDetalhe.Issqn = Convert.ToDecimal(leitor["ISSQN"]);
                    NFDetalhe.TaxaPis = Convert.ToDecimal(leitor["TAXA_PIS"]);
                    NFDetalhe.Pis = Convert.ToDecimal(leitor["PIS"]);
                    NFDetalhe.TaxaCofins = Convert.ToDecimal(leitor["TAXA_COFINS"]);
                    NFDetalhe.Cofins = Convert.ToDecimal(leitor["COFINS"]);
                    NFDetalhe.TaxaAcrescimo = Convert.ToDecimal(leitor["TAXA_ACRESCIMO"]);
                    NFDetalhe.Acrescimo = Convert.ToDecimal(leitor["ACRESCIMO"]);
                    NFDetalhe.TaxaIpi = Convert.ToDecimal(leitor["TAXA_IPI"]);
                    NFDetalhe.Ipi = Convert.ToDecimal(leitor["IPI"]);
                    NFDetalhe.Cancelado = Convert.ToString(leitor["CANCELADO"]);
                    NFDetalhe.Cst = Convert.ToString(leitor["CST"]);
                    NFDetalhe.MovimentaEstoque = Convert.ToString(leitor["MOVIMENTA_ESTOQUE"]);
                    NFDetalhe.DescricaoUnidade = "Descubra como pegar essa descricao";
                    ListaNFDetalhe.Add(NFDetalhe);
                }
                return ListaNFDetalhe;
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



        public List<NotaFiscalCabecalhoVO> ConsultaNFCabecalhoCanceladasSPED(string pDataInicio, string pDataFim)
        {
            ConsultaSQL =
              "select * from NOTA_FISCAL_CABECALHO, CLIENTE where " +
              "NOTA_FISCAL_CABECALHO.ID_CLIENTE = CLIENTE.ID and CANCELADA=" + Biblioteca.QuotedStr("S") + " and " +
              "(DATA_EMISSAO between " +
              Biblioteca.QuotedStr(pDataInicio) + " and " + Biblioteca.QuotedStr(pDataFim) + ")";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();

                List<NotaFiscalCabecalhoVO> ListaNFCabecalho = new List<NotaFiscalCabecalhoVO>();

                while (leitor.Read())
                {
                    NotaFiscalCabecalhoVO NFCabecalho = new NotaFiscalCabecalhoVO();
                    NFCabecalho.Id = Convert.ToInt32(leitor["ID"]);
                    NFCabecalho.IdEcfFuncionario = Convert.ToInt32(leitor["ID_ECF_FUNCIONARIO"]);
                    NFCabecalho.IdCliente = Convert.ToInt32(leitor["ID_CLIENTE"]);
                    NFCabecalho.Cfop = Convert.ToInt32(leitor["CFOP"]);
                    NFCabecalho.Numero = Convert.ToString(leitor["NUMERO"]);
                    NFCabecalho.DataEmissao = (DateTime)(leitor["DATA_EMISSAO"]);
                    NFCabecalho.HoraEmissao = Convert.ToString(leitor["HORA_EMISSAO"]);
                    NFCabecalho.Serie = Convert.ToString(leitor["SERIE"]);
                    NFCabecalho.Subserie = Convert.ToString(leitor["SUBSERIE"]);
                    NFCabecalho.TotalProdutos = Convert.ToDecimal(leitor["TOTAL_PRODUTOS"]);
                    NFCabecalho.TotalNf = Convert.ToDecimal(leitor["TOTAL_NF"]);
                    NFCabecalho.BaseIcms = Convert.ToDecimal(leitor["BASE_ICMS"]);
                    NFCabecalho.Icms = Convert.ToDecimal(leitor["ICMS"]);
                    NFCabecalho.IcmsOutras = Convert.ToDecimal(leitor["ICMS_OUTRAS"]);
                    NFCabecalho.Issqn = Convert.ToDecimal(leitor["ISSQN"]);
                    NFCabecalho.Pis = Convert.ToDecimal(leitor["PIS"]);
                    NFCabecalho.Cofins = Convert.ToDecimal(leitor["COFINS"]);
                    NFCabecalho.Ipi = Convert.ToDecimal(leitor["IPI"]);
                    NFCabecalho.TaxaAcrescimo = Convert.ToDecimal(leitor["TAXA_ACRESCIMO"]);
                    NFCabecalho.Acrescimo = Convert.ToDecimal(leitor["ACRESCIMO"]);
                    NFCabecalho.AcrescimoItens = Convert.ToDecimal(leitor["ACRESCIMO_ITENS"]);
                    NFCabecalho.TaxaDesconto = Convert.ToDecimal(leitor["TAXA_DESCONTO"]);
                    NFCabecalho.Desconto = Convert.ToDecimal(leitor["DESCONTO"]);
                    NFCabecalho.DescontoItens = Convert.ToDecimal(leitor["DESCONTO_ITENS"]);
                    NFCabecalho.Cancelada = Convert.ToString(leitor["CANCELADA"]);
                    NFCabecalho.CpfCnpjCliente = Convert.ToString(leitor["CPF_CNPJ"]);
                    ListaNFCabecalho.Add(NFCabecalho);
                }
                return ListaNFCabecalho;
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


        public void ExcluiNotaFiscal(string pNota)
        {
            NotaFiscalCabecalhoVO NotaFiscalCabecalho = TabelaNotaFiscalCabecalho(pNota);
            if (NotaFiscalCabecalho.Id > 0)
            {

                ConsultaSQL = "delete from NOTA_FISCAL_DETALHE " +
                               " where (ID_NF_CABECALHO = ?pID)";
                try
                {
                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    comando.Parameters.AddWithValue("?pID", NotaFiscalCabecalho.Id);
                    comando.ExecuteNonQuery();
                }
                catch (Exception eError)
                {
                    Log.write(eError.ToString());
                }


                ConsultaSQL = "delete from NOTA_FISCAL_CABECALHO " +
                               " where (ID = ?pID) ";
                try
                {
                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    comando.Parameters.AddWithValue("?pID", NotaFiscalCabecalho.Id);
                    comando.ExecuteNonQuery();
                }
                catch (Exception eError)
                {
                    Log.write(eError.ToString());
                }
            }
        }


    }

}

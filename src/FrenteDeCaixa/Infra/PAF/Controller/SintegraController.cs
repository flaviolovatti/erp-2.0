/********************************************************************************
Title: T2TiPDV
Description: Classe de controle do Sintegra

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
using PafEcf.View;

namespace PafEcf.Controller
{

    public class SintegraController
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public SintegraController()
        {
            conexao = dbConnection.conectar();
        }


        public Sintegra60MVO Grava60M(Sintegra60MVO Sintegra60M)
        {
            ConsultaSQL =
              "insert into SINTEGRA_60M (" +
              "DATA_EMISSAO," +
              "NUMERO_SERIE_ECF," +
              "NUMERO_EQUIPAMENTO," +
              "MODELO_DOCUMENTO_FISCAL," +
              "COO_INICIAL," +
              "COO_FINAL," +
              "CRZ," +
              "CRO," +
              "VALOR_VENDA_BRUTA," +
              "VALOR_GRANDE_TOTAL) values (" +
              "?pDataEmissao," +
              "?pSerieImpressora," +
              "?pNumeroEquipamento," +
              "?pModeloDocumentoFiscal," +
              "?pCOOInicial," +
              "?pCOOFinal," +
              "?pCRZ," +
              "?pCRO," +
              "?pVendaBruta," +
              "?pGrandeTotal)";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pDataEmissao", Sintegra60M.DataEmissao);
                comando.Parameters.AddWithValue("?pSerieImpressora", Sintegra60M.SerieImpressora);
                comando.Parameters.AddWithValue("?pNumeroEquipamento", Sintegra60M.NumeroEquipamento);
                comando.Parameters.AddWithValue("?pModeloDocumentoFiscal", Sintegra60M.ModeloDocumentoFiscal);
                comando.Parameters.AddWithValue("?pCOOInicial", Sintegra60M.COOInicial);
                comando.Parameters.AddWithValue("?pCOOFinal", Sintegra60M.COOFinal);
                comando.Parameters.AddWithValue("?pCRZ", Sintegra60M.CRZ);
                comando.Parameters.AddWithValue("?pCRO", Sintegra60M.CRO);
                comando.Parameters.AddWithValue("?pVendaBruta", Sintegra60M.VendaBruta);
                comando.Parameters.AddWithValue("?pGrandeTotal", Sintegra60M.GrandeTotal);
                comando.ExecuteNonQuery();

                ConsultaSQL = "select max(ID) as ID from SINTEGRA_60M";
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();
                Sintegra60M.Id = Convert.ToInt32(leitor["ID"]);
                return Sintegra60M;
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


        public List<Sintegra60MVO> Tabela60M(string pDataInicio, string pDataFim)
        {
            ConsultaSQL = "select * from SINTEGRA_60M where " +
            "(DATA_EMISSAO between " +
              Biblioteca.QuotedStr(pDataInicio) + " and " + Biblioteca.QuotedStr(pDataFim) + ")";

            try
            {
                List<Sintegra60MVO> Lista60M = new List<Sintegra60MVO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    Sintegra60MVO Sintegra60M = new Sintegra60MVO();
                    Sintegra60M.Id = Convert.ToInt32(leitor["ID"]);
                    Sintegra60M.DataEmissao = Convert.ToDateTime(leitor["DATA_EMISSAO"]);
                    Sintegra60M.SerieImpressora = Convert.ToString(leitor["NUMERO_SERIE_ECF"]);
                    Sintegra60M.NumeroEquipamento = Convert.ToInt32(leitor["NUMERO_EQUIPAMENTO"]);
                    Sintegra60M.ModeloDocumentoFiscal = Convert.ToString(leitor["MODELO_DOCUMENTO_FISCAL"]);
                    Sintegra60M.COOInicial = Convert.ToInt32(leitor["COO_INICIAL"]);
                    Sintegra60M.COOFinal = Convert.ToInt32(leitor["COO_FINAL"]);
                    Sintegra60M.CRZ = Convert.ToInt32(leitor["CRZ"]);
                    Sintegra60M.CRO = Convert.ToInt32(leitor["CRO"]);
                    Sintegra60M.VendaBruta = Convert.ToDecimal(leitor["VALOR_VENDA_BRUTA"]);
                    Sintegra60M.GrandeTotal = Convert.ToDecimal(leitor["VALOR_GRANDE_TOTAL"]);
                    Lista60M.Add(Sintegra60M);
                }
                return Lista60M;
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


        public void Grava60A(List<Sintegra60AVO> Lista60A)
        {
            for (int i = 0; i <= Lista60A.Count - 1; i++)
            {
                ConsultaSQL =
                "insert into SINTEGRA_60A (" +
                "ID_SINTEGRA_60M," +
                "SITUACAO_TRIBUTARIA," +
                "VALOR) values (" +
                "?pId60M," +
                "?pST," +
                "?pValor)";
                try
                {
                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    comando.Parameters.AddWithValue("?pId60M", Lista60A[i].Id60M);
                    comando.Parameters.AddWithValue("?pST", Lista60A[i].SituacaoTributaria);
                    comando.Parameters.AddWithValue("?pValor", Lista60A[i].Valor);
                    comando.ExecuteNonQuery();
                }

                catch (Exception eError)
                {
                    Log.write(eError.ToString());
                }
            }
        }


        public List<SintegraVO> Tabela50(string pDataInicio, string pDataFim)
        {
            ConsultaSQL = "select " +
                           " C.ID, " +
                           " P.CPF_CNPJ, " +
                           " P.INSCRICAO_ESTADUAL, " +
                           " C.DATA_EMISSAO, " +
                           " C.SERIE, " +
                           " C.NUMERO, " +
                           " E.UF, " +
                           " C.VALOR_TOTAL, " +
                           " C.BASE_CALCULO_ICMS, " +
                           " C.VALOR_ICMS, " +
                           " P.TIPO " +
                           " from " +
                           " NFE_CABECALHO C, PESSOA P, PESSOA_ENDERECO E " +
                           "where " +
                           " C.ID_CLIENTE = P.ID " +
                           " and E.ID_PESSOA = P.ID " +
                           " and C.DATA_EMISSAO between " + Biblioteca.QuotedStr(pDataInicio) + " and " + Biblioteca.QuotedStr(pDataFim);

            try
            {
                List<SintegraVO> ListaSintegra = new List<SintegraVO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    SintegraVO Sintegra = new SintegraVO();

                    Sintegra.Id = Convert.ToInt32(leitor["ID"]);
                    Sintegra.CPFCNPJ = Convert.ToString(leitor["CPF_CNPJ"]);
                    if (Convert.ToString(leitor["TIPO"]) == "J")
                    {
                        if (Convert.ToString(leitor["INSCRICAO_ESTADUAL"]).Trim() != "")
                            Sintegra.Inscricao = Convert.ToString(leitor["INSCRICAO_ESTADUAL"]);
                        else
                            Sintegra.Inscricao = "ISENTO";
                    }
                    else
                        Sintegra.Inscricao = "ISENTO";

                    Sintegra.DataDocumento = Convert.ToDateTime(leitor["DATA_EMISSAO"]);
                    Sintegra.Serie = "001";
                    Sintegra.Numero = Convert.ToString(leitor["NUMERO"]);
                    Sintegra.UF = Convert.ToString(leitor["UF"]);
                    Sintegra.ValorContabil = Convert.ToDecimal(leitor["VALOR_TOTAL"]);
                    Sintegra.Cfop = Convert.ToString(FCaixa.Configuracao.CFOPNF2);
                    Sintegra.BasedeCalculo = Convert.ToDecimal(leitor["BASE_CALCULO_ICMS"]);
                    Sintegra.Icms = Convert.ToDecimal(leitor["VALOR_ICMS"]);
                    Sintegra.ValorOutras = 0;
                    Sintegra.ValorIsentas = 0;
                    Sintegra.Isentas = 0;
                    Sintegra.Aliquota = 0;
                    Sintegra.AliquotaICMS = (Convert.ToDecimal(leitor["VALOR_ICMS"]) * 100) / Convert.ToDecimal(leitor["VALOR_TOTAL"]);

                    Sintegra.EmissorDocumento = "P"; // emissao propria
                    Sintegra.Situacao = "N";
                    Sintegra.Modelo = "55";
                    ListaSintegra.Add(Sintegra);
                }
                return ListaSintegra;
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


        public List<SintegraVO> Tabela54(string pDataInicio, string pDataFim)
        {
            ConsultaSQL = "select " +
                           " C.ID, " +
                           " P.CPF_CNPJ, " +
                           " P.INSCRICAO_ESTADUAL, " +
                           " C.DATA_EMISSAO, " +
                           " C.SERIE, " +
                           " C.NUMERO, " +
                           " E.UF, " +
                           " D.CFOP, " +
                           " D.VALOR_TOTAL, " +
                           " D.BASE_CALCULO_ICMS, " +
                           " D.VALOR_ICMS, " +
                           " D.VALOR_OUTRAS_DESPESAS, " +
                           " P.TIPO " +
                           " from " +
                           " NFE_CABECALHO C, NFE_DETALHE D, PESSOA P, PESSOA_ENDERECO E " +
                           "where " +
                           " C.ID=D.ID_NFE_CABECALHO " +
                           " and C.ID_CLIENTE = P.ID " +
                           " and E.ID_PESSOA = P.ID " +
                           " and C.DATA_EMISSAO between " + Biblioteca.QuotedStr(pDataInicio) + " and " + Biblioteca.QuotedStr(pDataFim);

            try
            {
                List<SintegraVO> ListaSintegra = new List<SintegraVO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    SintegraVO Sintegra = new SintegraVO();
                    Sintegra.Id = Convert.ToInt32(leitor["ID"]);
                    Sintegra.CPFCNPJ = Convert.ToString(leitor["CPF_CNPJ"]);
                    if (Convert.ToString(leitor["TIPO"]) == "J")
                    {
                        if (Convert.ToString(leitor["INSCRICAO_ESTADUAL"]).Trim() != "")
                            Sintegra.Inscricao = Convert.ToString(leitor["INSCRICAO_ESTADUAL"]);
                        else
                            Sintegra.Inscricao = "ISENTO";
                    }
                    else
                        Sintegra.Inscricao = "ISENTO";

                    Sintegra.DataDocumento = Convert.ToDateTime(leitor["DATA_EMISSAO"]);
                    Sintegra.Serie = "001";
                    Sintegra.Numero = Convert.ToString(leitor["NUMERO"]);
                    Sintegra.UF = Convert.ToString(leitor["UF"]);
                    Sintegra.Cfop = Convert.ToString(FCaixa.Configuracao.CFOPNF2);
                    Sintegra.ValorContabil = Convert.ToDecimal(leitor["VALOR_TOTAL"]);
                    Sintegra.BasedeCalculo = Convert.ToDecimal(leitor["BASE_CALCULO_ICMS"]);
                    Sintegra.Icms = Convert.ToDecimal(leitor["VALOR_ICMS"]);
                    Sintegra.ValorOutras = Convert.ToDecimal(leitor["VALOR_OUTRAS_DESPESAS"]);
                    Sintegra.ValorIsentas = 0;
                    Sintegra.Isentas = 0;
                    Sintegra.Aliquota = 0;
                    Sintegra.EmissorDocumento = "P"; // emissao propria
                    Sintegra.Situacao = "N";
                    Sintegra.Modelo = "55";
                    ListaSintegra.Add(Sintegra);
                }
                return ListaSintegra;
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


        public List<SintegraVO> Tabela51(string pDataInicio, string pDataFim)
        {
            ConsultaSQL = "select " +
                           " D.ID, " +
                           " D.CPF_CNPJ, " +
                           " D.IE, " +
                           " D.DATA_EMISSAO, " +
                           " D.SERIE, " +
                           " D.NUMERO, " +
                           " D.CFOP, " +
                           " D.VALOR_TOTAL, " +
                           " D.VALOR_IPI, " +
                           " D.VALOR_DESPESAS_ACESSORIAS, " +
                           " D.SITUACAO_NOTA " +
                           "from " +
                           " NFE_CABECALHO C, NFE_DETALHE D " +
                           "where " +
                           " C.ID=D.ID_NFE_CABECALHO " +
                           " and C.DATA_EMISSAO between " + Biblioteca.QuotedStr(pDataInicio) + " and " + Biblioteca.QuotedStr(pDataFim);

            try
            {
                List<SintegraVO> ListaSintegra = new List<SintegraVO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    SintegraVO Sintegra = new SintegraVO();
                    Sintegra.Id = Convert.ToInt32(leitor["ID"]);
                    Sintegra.CPFCNPJ = Convert.ToString(leitor["CPF_CNPJ"]);
                    Sintegra.Inscricao = Convert.ToString(leitor["IE"]);
                    Sintegra.DataDocumento = Convert.ToDateTime(leitor["DATA_EMISSAO"]);
                    Sintegra.Serie = Convert.ToString(leitor["SERIE"]);
                    Sintegra.Numero = Convert.ToString(leitor["NUMERO"]);
                    Sintegra.Cfop = Convert.ToString(leitor["CFOP"]);
                    Sintegra.ValorContabil = Convert.ToInt32(leitor["VALOR_TOTAL"]);
                    Sintegra.ValorIpi = Convert.ToInt32(leitor["VALOR_IPI"]);
                    Sintegra.ValorOutras = Convert.ToDecimal(leitor["VALOR_DESPESAS_ACESSORIAS"]);
                    Sintegra.ValorIsentas = 0;
                    Sintegra.Situacao = Convert.ToString(leitor["SITUACAO_NOTA"]);
                    ListaSintegra.Add(Sintegra);
                }
                return ListaSintegra;
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


        public List<Sintegra60AVO> Tabela60A(int pId)
        {
            ConsultaSQL = "select * from SINTEGRA_60A where ID_SINTEGRA_60M=" + Convert.ToString(pId);
            try
            {
                List<Sintegra60AVO> Lista60A = new List<Sintegra60AVO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    Sintegra60AVO Sintegra60A = new Sintegra60AVO();
                    Sintegra60A.Id = Convert.ToInt32(leitor["ID"]);
                    Sintegra60A.Id60M = Convert.ToInt32(leitor["ID_SINTEGRA_60M"]);
                    Sintegra60A.SituacaoTributaria = Convert.ToString(leitor["SITUACAO_TRIBUTARIA"]);
                    Sintegra60A.Valor = Convert.ToDecimal(leitor["VALOR"]);
                    Lista60A.Add(Sintegra60A);
                }
                return Lista60A;
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


        public List<Sintegra60DVO> Tabela60D(string pDataInicio, string pDataFim)
        {
            ConsultaSQL =
              "select * from VIEW_60D " +
              "where DATA_EMISSAO between " +
              Biblioteca.QuotedStr(pDataInicio) + " and " + Biblioteca.QuotedStr(pDataFim);

            try
            {
                List<Sintegra60DVO> Lista60D = new List<Sintegra60DVO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    Sintegra60DVO Sintegra60D = new Sintegra60DVO();
                    Sintegra60D.GTIN = Convert.ToString(leitor["GTIN"]);
                    Sintegra60D.DataEmissao = Convert.ToDateTime(leitor["DATA_EMISSAO"]);
                    Sintegra60D.SerieECF = Convert.ToString(leitor["SERIE"]);
                    Sintegra60D.SomaQuantidade = Convert.IsDBNull(leitor["SOMA_QUANTIDADE"]) ? 0 : Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_QUANTIDADE"]), Constantes.DECIMAIS_VALOR);
                    Sintegra60D.SomaValor = Convert.IsDBNull(leitor["SOMA_ITEM"]) ? 0 : Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_ITEM"]), Constantes.DECIMAIS_VALOR);
                    Sintegra60D.SomaBaseICMS = Convert.IsDBNull(leitor["SOMA_BASE_ICMS"]) ? 0 : Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_BASE_ICMS"]), Constantes.DECIMAIS_VALOR);
                    Sintegra60D.SomaValorICMS = Convert.IsDBNull(leitor["SOMA_ICMS"]) ? 0 : Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_ICMS"]), Constantes.DECIMAIS_VALOR);
                    Sintegra60D.SituacaoTributaria = Convert.ToString(leitor["ECF_ICMS_ST"]);
                    Lista60D.Add(Sintegra60D);
                }
                return Lista60D;
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


        public List<Sintegra60RVO> Tabela60R(string pDataInicio, string pDataFim)
        {
            ConsultaSQL =
              "select * from VIEW_60R " +
              "where DATA_EMISSAO between " +
              Biblioteca.QuotedStr(pDataInicio) + " and " + Biblioteca.QuotedStr(pDataFim);

            try
            {
                List<Sintegra60RVO> Lista60R = new List<Sintegra60RVO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    Sintegra60RVO Sintegra60R = new Sintegra60RVO();
                    Sintegra60R.GTIN = Convert.ToString(leitor["GTIN"]);
                    Sintegra60R.DataEmissao = Convert.ToDateTime(leitor["DATA_EMISSAO"]);
                    Sintegra60R.MesEmissao = Convert.ToString(leitor["MES_EMISSAO"]);
                    Sintegra60R.MesEmissao = new string('0', 2 - Sintegra60R.MesEmissao.Length) + Sintegra60R.MesEmissao;
                    Sintegra60R.AnoEmissao = Convert.ToString(leitor["ANO_EMISSAO"]);
                    Sintegra60R.SomaQuantidade = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_QUANTIDADE"]), Constantes.DECIMAIS_QUANTIDADE);
                    Sintegra60R.SomaValor = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_ITEM"]), Constantes.DECIMAIS_VALOR);
                    Sintegra60R.SomaBaseICMS = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_BASE_ICMS"]), Constantes.DECIMAIS_VALOR);
                    Sintegra60R.SituacaoTributaria = Convert.ToString(leitor["ECF_ICMS_ST"]);
                    Lista60R.Add(Sintegra60R);
                }
                return Lista60R;
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


        public List<Sintegra61RVO> Tabela61R(string pDataInicio, string pDataFim)
        {
            ConsultaSQL =
              "select * from VIEW_61R " +
              "where DATA_EMISSAO between " +
              Biblioteca.QuotedStr(pDataInicio) + " and " + Biblioteca.QuotedStr(pDataFim);

            try
            {
                List<Sintegra61RVO> Lista61R = new List<Sintegra61RVO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    Sintegra61RVO Sintegra61R = new Sintegra61RVO();
                    Sintegra61R.GTIN = Convert.ToString(leitor["GTIN"]);
                    Sintegra61R.DataEmissao = Convert.ToDateTime(leitor["DATA_EMISSAO"]);
                    Sintegra61R.MesEmissao = Convert.ToString(leitor["MES_EMISSAO"]);
                    Sintegra61R.MesEmissao = new string('0', 2 - Sintegra61R.MesEmissao.Length) + Sintegra61R.MesEmissao;
                    Sintegra61R.AnoEmissao = Convert.ToString(leitor["ANO_EMISSAO"]);
                    Sintegra61R.SomaQuantidade = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_QUANTIDADE"]), Constantes.DECIMAIS_QUANTIDADE);
                    Sintegra61R.SomaValor = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_ITEM"]), Constantes.DECIMAIS_VALOR);
                    Sintegra61R.SomaBaseICMS = Biblioteca.TruncaValor(Convert.ToDecimal(leitor["SOMA_BASE_ICMS"]), Constantes.DECIMAIS_VALOR);
                    Sintegra61R.SituacaoTributaria = Convert.ToString(leitor["ECF_ICMS_ST"]);
                    Lista61R.Add(Sintegra61R);
                }
                return Lista61R;
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


        public List<SintegraVO> Registro54e75(string pId)
        {
            ConsultaSQL = "select " +
                           " numero_item, " +
                           " nome_produto, " +
                           " cst_icms, " +
                           " codigo_produto, " +
                           " cfop, " +
                           " quantidade_comercial, " +
                           " valor_total, " +
                           " base_calculo_icms, " +
                           " base_calculo_icms_st, " +
                           " valor_ipi, " +
                           " aliquota_icms, " +
                           " ncm, " +
                           " unidade_comercial, " +
                           " aliquota_ipi, " +
                           " reducao_bc_icms_st " +
                           "from " +
                           " nfe_detalhe " +
                           "where " +
                           " id_nfe_cabecalho=" + pId;
            try
            {
                List<SintegraVO> ListaSintegra = new List<SintegraVO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    SintegraVO Sintegra = new SintegraVO();
                    Sintegra.NumeroItem = Convert.ToString(leitor["numero_item"]);
                    Sintegra.Descricao = Convert.ToString(leitor["nome_produto"]);
                    Sintegra.CST = Convert.ToString(leitor["cst_icms"]);
                    Sintegra.Codigo = Convert.ToString(leitor["codigo_produto"]);
                    Sintegra.Cfop = Convert.ToString(leitor["cfop"]);
                    Sintegra.Quantidade = Convert.ToDecimal(leitor["quantidade_comercial"]);
                    Sintegra.Valor = Convert.ToDecimal(leitor["valor_total"]);
                    Sintegra.BasedeCalculo = Convert.ToDecimal(leitor["valor_total"]);
                    Sintegra.BaseST = Convert.ToDecimal(leitor["base_calculo_icms_st"]);
                    Sintegra.Despesas = 0;
                    Sintegra.ValorIpi = Convert.ToDecimal(leitor["valor_ipi"]);
                    Sintegra.AliquotaICMS = Convert.ToDecimal(leitor["aliquota_icms"]);
                    Sintegra.NCM = Convert.ToString(leitor["ncm"]);
                    Sintegra.Unidade = Convert.ToString(leitor["unidade_comercial"]);
                    Sintegra.AliquotaIpi = Convert.ToDecimal(leitor["aliquota_ipi"]);
                    Sintegra.Reducao = Convert.ToDecimal(leitor["reducao_bc_icms_st"]);
                    ListaSintegra.Add(Sintegra);
                }
                return ListaSintegra;
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

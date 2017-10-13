/********************************************************************************
Title: T2TiPDV
Description: Classe de controle do movimento.

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
using PafEcf.VO;
using PafEcf.Infra;

namespace PafEcf.Controller
{

    public class MovimentoController
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public MovimentoController()
        {
            conexao = dbConnection.conectar();
        }

        public MovimentoVO IniciaMovimento(MovimentoVO pMovimento)
        {
            ConsultaSQL =
              "insert into ECF_MOVIMENTO (" +
              "ID_ECF_EMPRESA," +
              "ID_ECF_TURNO," +
              "ID_ECF_IMPRESSORA," +
              "ID_ECF_OPERADOR," +
              "ID_ECF_CAIXA," +
              "ID_GERENTE_SUPERVISOR," +
              "DATA_ABERTURA," +
              "HORA_ABERTURA," +
              "TOTAL_SUPRIMENTO," +
              "STATUS_MOVIMENTO," +
              "SINCRONIZADO) values (" +
              "?pEmpresa," +
              "?pTurno," +
              "?pImpressora," +
              "?pOperador," +
              "?pCaixa," +
              "?pGerenteSupervisor," +
              "?pDataAbertura," +
              "?pHoraAbertura," +
              "?pTotalSuprimento," +
              "?pStatus," +
              "?pSincronizado)";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pTurno", pMovimento.IdTurno);
                comando.Parameters.AddWithValue("?pImpressora", pMovimento.IdImpressora);
                comando.Parameters.AddWithValue("?pOperador", pMovimento.IdOperador);
                comando.Parameters.AddWithValue("?pCaixa", pMovimento.IdCaixa);
                comando.Parameters.AddWithValue("?pEmpresa", pMovimento.IdEmpresa);
                comando.Parameters.AddWithValue("?pGerenteSupervisor", pMovimento.IdGerenteSupervisor);
                comando.Parameters.AddWithValue("?pDataAbertura", pMovimento.DataAbertura);
                comando.Parameters.AddWithValue("?pHoraAbertura", pMovimento.HoraAbertura);
                comando.Parameters.AddWithValue("?pTotalSuprimento", pMovimento.TotalSuprimento);
                comando.Parameters.AddWithValue("?pStatus", pMovimento.Status);
                comando.Parameters.AddWithValue("?pSincronizado", pMovimento.Sincronizado);
                comando.ExecuteNonQuery();
                return VerificaMovimento();
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


        public void EncerraMovimento(MovimentoVO pMovimento)
        {
            ConsultaSQL =
              "update ECF_MOVIMENTO set " +
              "DATA_FECHAMENTO = ?pDataFechamento," +
              "HORA_FECHAMENTO = ?pHoraFechamento," +
              "TOTAL_SUPRIMENTO = ?pTotalSuprimento," +
              "TOTAL_SANGRIA = ?pTotalSangria," +
              "TOTAL_NAO_FISCAL = ?pTotalNaoFiscal," +
              "TOTAL_VENDA = ?pTotalVenda," +
              "TOTAL_DESCONTO = ?pTotalDesconto," +
              "TOTAL_ACRESCIMO = ?pTotalAcrescimo," +
              "TOTAL_FINAL = ?pTotalFinal," +
              "TOTAL_RECEBIDO = ?pTotalRecebido," +
              "TOTAL_TROCO = ?pTotalTroco," +
              "TOTAL_CANCELADO = ?pTotalCancelado," +
              "STATUS_MOVIMENTO = ?pStatus " +
              " where ID = ?pId";

            try
            {
                // total de suprimentos
                comando = new MySqlCommand("select sum(VALOR) as TOTAL from ECF_SUPRIMENTO where ID_ECF_MOVIMENTO=" + Convert.ToString(pMovimento.Id), conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();
                pMovimento.TotalSuprimento = leitor["TOTAL"] as decimal?;
                leitor.Close();

                // total de sangrias
                comando = new MySqlCommand("select sum(VALOR) as TOTAL from ECF_SANGRIA where ID_ECF_MOVIMENTO=" + Convert.ToString(pMovimento.Id), conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();
                pMovimento.TotalSangria = leitor["TOTAL"] as decimal?;
                leitor.Close();

                // total de recebimentos nao fiscais
                comando = new MySqlCommand("select sum(VALOR) as TOTAL from ECF_RECEBIMENTO_NAO_FISCAL where ID_ECF_MOVIMENTO=" + Convert.ToString(pMovimento.Id), conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();
                pMovimento.TotalNaoFiscal = leitor["TOTAL"] as decimal?;
                leitor.Close();

                // totais das vendas
                string consulta =
                                  "select sum(VALOR_VENDA) as VALOR_VENDA, sum(DESCONTO) as DESCONTO, " +
                                  " sum(ACRESCIMO) as ACRESCIMO, sum(VALOR_FINAL) as VALOR_FINAL, " +
                                  " sum(VALOR_RECEBIDO) as VALOR_RECEBIDO, sum(TROCO) as TROCO, " +
                                  " sum(VALOR_CANCELADO) as  VALOR_CANCELADO " +
                                  "from ECF_VENDA_CABECALHO " +
                                  "where ID_ECF_MOVIMENTO=" + Convert.ToString(pMovimento.Id);

                comando = new MySqlCommand(consulta, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();
                pMovimento.TotalVenda = leitor["VALOR_VENDA"] as decimal?;
                pMovimento.TotalDesconto = leitor["DESCONTO"] as decimal?;
                pMovimento.TotalAcrescimo = leitor["ACRESCIMO"] as decimal?;
                pMovimento.TotalFinal = (leitor["VALOR_FINAL"] as decimal?) + pMovimento.TotalSuprimento - pMovimento.TotalSangria;
                pMovimento.TotalRecebido = leitor["VALOR_RECEBIDO"] as decimal?;
                pMovimento.TotalTroco = leitor["TROCO"] as decimal?;
                pMovimento.TotalCancelado = leitor["VALOR_CANCELADO"] as decimal?;
                leitor.Close();

                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pId", pMovimento.Id);
                comando.Parameters.AddWithValue("?pDataFechamento", pMovimento.DataFechamento);
                comando.Parameters.AddWithValue("?pHoraFechamento", pMovimento.HoraFechamento);
                comando.Parameters.AddWithValue("?pTotalSuprimento", pMovimento.TotalSuprimento);
                comando.Parameters.AddWithValue("?pTotalSangria", pMovimento.TotalSangria);
                comando.Parameters.AddWithValue("?pTotalNaoFiscal", pMovimento.TotalNaoFiscal);
                comando.Parameters.AddWithValue("?pTotalVenda", pMovimento.TotalVenda);
                comando.Parameters.AddWithValue("?pTotalDesconto", pMovimento.TotalDesconto);
                comando.Parameters.AddWithValue("?pTotalAcrescimo", pMovimento.TotalAcrescimo);
                comando.Parameters.AddWithValue("?pTotalFinal", pMovimento.TotalFinal);
                comando.Parameters.AddWithValue("?pTotalRecebido", pMovimento.TotalRecebido);
                comando.Parameters.AddWithValue("?pTotalCancelado", pMovimento.TotalCancelado);
                comando.Parameters.AddWithValue("?pTotalTroco", pMovimento.TotalTroco);
                comando.Parameters.AddWithValue("?pStatus", pMovimento.Status);
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


        public void SaidaTemporaria(MovimentoVO pMovimento)
        {
            ConsultaSQL =
              "update ECF_MOVIMENTO set STATUS_MOVIMENTO=" + Biblioteca.QuotedStr("T") +
              " where ID = ?pId";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pId", pMovimento.Id);
                comando.ExecuteNonQuery();
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }


        public void RetornoOperador(MovimentoVO pMovimento)
        {
            ConsultaSQL =
              "update ECF_MOVIMENTO set STATUS_MOVIMENTO=" + Biblioteca.QuotedStr("A") +
              " where ID = ?pId";
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pId", pMovimento.Id);
                comando.ExecuteNonQuery();
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
        }



        public MovimentoVO VerificaMovimento()
        {
            ConsultaSQL = "select " +
                            " M.ID as MID, " +
                            " M.ID_GERENTE_SUPERVISOR, " +
                            " M.DATA_ABERTURA, " +
                            " M.HORA_ABERTURA, " +
                            " M.DATA_FECHAMENTO, " +
                            " M.HORA_FECHAMENTO, " +
                            " M.TOTAL_SUPRIMENTO, " +
                            " M.TOTAL_SANGRIA, " +
                            " M.TOTAL_NAO_FISCAL, " +
                            " M.TOTAL_VENDA, " +
                            " M.TOTAL_DESCONTO, " +
                            " M.TOTAL_ACRESCIMO, " +
                            " M.TOTAL_FINAL, " +
                            " M.TOTAL_RECEBIDO, " +
                            " M.TOTAL_TROCO, " +
                            " M.TOTAL_CANCELADO, " +
                            " M.STATUS_MOVIMENTO, " +
                            " T.ID as TID, " +
                            " T.DESCRICAO, " +
                            " C.ID as CID, " +
                            " C.NOME, " +
                            " O.ID as OID, " +
                            " O.LOGIN, " +
                            " I.ID as IID, " +
                            " I.IDENTIFICACAO " +
                            "from " +
                            " ECF_MOVIMENTO M, ECF_TURNO T, ECF_CAIXA C, ECF_OPERADOR O, ECF_IMPRESSORA I " +
                            "where " +
                            " M.ID_ECF_TURNO = T.ID AND " +
                            " M.ID_ECF_IMPRESSORA = I.ID AND " +
                            " M.ID_ECF_OPERADOR = O.ID AND " +
                            " M.ID_ECF_CAIXA = C.ID AND" +
                            " (STATUS_MOVIMENTO=" + Biblioteca.QuotedStr("A") + " or STATUS_MOVIMENTO=" + Biblioteca.QuotedStr("T") + ")";
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                if (leitor.Read())
                {

                    MovimentoVO Movimento = new MovimentoVO();
                    Movimento.Id = Convert.ToInt32(leitor["MID"]);
                    Movimento.IdGerenteSupervisor = Convert.ToInt32(leitor["ID_GERENTE_SUPERVISOR"]);
                    Movimento.DataAbertura = (DateTime)(leitor["DATA_ABERTURA"]);
                    Movimento.HoraAbertura = Convert.ToString(leitor["HORA_ABERTURA"]);
                    Movimento.DataFechamento = leitor["DATA_FECHAMENTO"] as DateTime?;
                    Movimento.HoraFechamento = Convert.ToString(leitor["HORA_FECHAMENTO"]);
                    Movimento.TotalSuprimento = leitor["TOTAL_SUPRIMENTO"] as decimal?;
                    Movimento.TotalSangria = leitor["TOTAL_SANGRIA"] as decimal?;
                    Movimento.TotalNaoFiscal = leitor["TOTAL_NAO_FISCAL"] as decimal?;
                    Movimento.TotalVenda = leitor["TOTAL_VENDA"] as decimal?;
                    Movimento.TotalDesconto = leitor["TOTAL_DESCONTO"] as decimal?;
                    Movimento.TotalAcrescimo = leitor["TOTAL_ACRESCIMO"] as decimal?;
                    Movimento.TotalFinal = leitor["TOTAL_FINAL"] as decimal?;
                    Movimento.TotalRecebido = leitor["TOTAL_RECEBIDO"] as decimal?;
                    Movimento.TotalTroco = leitor["TOTAL_TROCO"] as decimal?;
                    Movimento.TotalCancelado = leitor["TOTAL_CANCELADO"] as decimal?;
                    Movimento.Status = Convert.ToString(leitor["STATUS_MOVIMENTO"]);
                    Movimento.IdTurno = Convert.ToInt32(leitor["TID"]);
                    Movimento.DescricaoTurno = Convert.ToString(leitor["DESCRICAO"]);
                    Movimento.IdCaixa = Convert.ToInt32(leitor["CID"]);
                    Movimento.NomeCaixa = Convert.ToString(leitor["NOME"]);
                    Movimento.IdOperador = Convert.ToInt32(leitor["OID"]);
                    Movimento.LoginOperador = Convert.ToString(leitor["LOGIN"]);
                    Movimento.IdImpressora = Convert.ToInt32(leitor["IID"]);
                    Movimento.IdentificacaoImpressora = Convert.ToString(leitor["IDENTIFICACAO"]);
                    return Movimento;
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


        public MovimentoVO VerificaMovimento(int pId)
        {
            ConsultaSQL = "select " +
                            " M.ID as MID, " +
                            " M.ID_GERENTE_SUPERVISOR, " +
                            " M.DATA_ABERTURA, " +
                            " M.HORA_ABERTURA, " +
                            " M.DATA_FECHAMENTO, " +
                            " M.HORA_FECHAMENTO, " +
                            " M.TOTAL_SUPRIMENTO, " +
                            " M.TOTAL_SANGRIA, " +
                            " M.TOTAL_NAO_FISCAL, " +
                            " M.TOTAL_VENDA, " +
                            " M.TOTAL_DESCONTO, " +
                            " M.TOTAL_ACRESCIMO, " +
                            " M.TOTAL_FINAL, " +
                            " M.TOTAL_RECEBIDO, " +
                            " M.TOTAL_TROCO, " +
                            " M.TOTAL_CANCELADO, " +
                            " M.STATUS_MOVIMENTO, " +
                            " T.ID as TID, " +
                            " T.DESCRICAO, " +
                            " C.ID as CID, " +
                            " C.NOME, " +
                            " O.ID as OID, " +
                            " O.LOGIN, " +
                            " I.ID as IID, " +
                            " I.IDENTIFICACAO " +
                            "from " +
                            " ECF_MOVIMENTO M, ECF_TURNO T, ECF_CAIXA C, ECF_OPERADOR O, ECF_IMPRESSORA I " +
                            "where " +
                            " M.ID = " + Convert.ToString(pId) + " AND " +
                            " M.ID_ECF_TURNO = T.ID AND " +
                            " M.ID_ECF_IMPRESSORA = I.ID AND " +
                            " M.ID_ECF_OPERADOR = O.ID AND " +
                            " M.ID_ECF_CAIXA = C.ID AND" +
                            " (STATUS_MOVIMENTO=" + Biblioteca.QuotedStr("F") + ")";
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                if (leitor.Read())
                {

                    MovimentoVO Movimento = new MovimentoVO();
                    Movimento.Id = Convert.ToInt32(leitor["MID"]);
                    Movimento.IdGerenteSupervisor = Convert.ToInt32(leitor["ID_GERENTE_SUPERVISOR"]);
                    Movimento.DataAbertura = (DateTime)(leitor["DATA_ABERTURA"]);
                    Movimento.HoraAbertura = Convert.ToString(leitor["HORA_ABERTURA"]);
                    Movimento.DataFechamento = leitor["DATA_FECHAMENTO"] as DateTime?;
                    Movimento.HoraFechamento = Convert.ToString(leitor["HORA_FECHAMENTO"]);
                    Movimento.TotalSuprimento = leitor["TOTAL_SUPRIMENTO"] as decimal?;
                    Movimento.TotalSangria = leitor["TOTAL_SANGRIA"] as decimal?;
                    Movimento.TotalNaoFiscal = leitor["TOTAL_NAO_FISCAL"] as decimal?;
                    Movimento.TotalVenda = leitor["TOTAL_VENDA"] as decimal?;
                    Movimento.TotalDesconto = leitor["TOTAL_DESCONTO"] as decimal?;
                    Movimento.TotalAcrescimo = leitor["TOTAL_ACRESCIMO"] as decimal?;
                    Movimento.TotalFinal = leitor["TOTAL_FINAL"] as decimal?;
                    Movimento.TotalRecebido = leitor["TOTAL_RECEBIDO"] as decimal?;
                    Movimento.TotalTroco = leitor["TOTAL_TROCO"] as decimal?;
                    Movimento.TotalCancelado = leitor["TOTAL_CANCELADO"] as decimal?;
                    Movimento.Status = Convert.ToString(leitor["STATUS_MOVIMENTO"]);
                    Movimento.IdTurno = Convert.ToInt32(leitor["TID"]);
                    Movimento.DescricaoTurno = Convert.ToString(leitor["DESCRICAO"]);
                    Movimento.IdCaixa = Convert.ToInt32(leitor["CID"]);
                    Movimento.NomeCaixa = Convert.ToString(leitor["NOME"]);
                    Movimento.IdOperador = Convert.ToInt32(leitor["OID"]);
                    Movimento.LoginOperador = Convert.ToString(leitor["LOGIN"]);
                    Movimento.IdImpressora = Convert.ToInt32(leitor["IID"]);
                    Movimento.IdentificacaoImpressora = Convert.ToString(leitor["IDENTIFICACAO"]);
                    return Movimento;
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


        public MovimentoVO VerificaMovimento(int pId, string pStatusMovimento)
        {

            ConsultaSQL = "select " +
                            " M.ID as MID, " +
                            " M.ID_GERENTE_SUPERVISOR, " +
                            " M.DATA_ABERTURA, " +
                            " M.HORA_ABERTURA, " +
                            " M.DATA_FECHAMENTO, " +
                            " M.HORA_FECHAMENTO, " +
                            " M.TOTAL_SUPRIMENTO, " +
                            " M.TOTAL_SANGRIA, " +
                            " M.TOTAL_NAO_FISCAL, " +
                            " M.TOTAL_VENDA, " +
                            " M.TOTAL_DESCONTO, " +
                            " M.TOTAL_ACRESCIMO, " +
                            " M.TOTAL_FINAL, " +
                            " M.TOTAL_RECEBIDO, " +
                            " M.TOTAL_TROCO, " +
                            " M.TOTAL_CANCELADO, " +
                            " M.STATUS_MOVIMENTO, " +
                            " T.ID as TID, " +
                            " T.DESCRICAO, " +
                            " C.ID as CID, " +
                            " C.NOME, " +
                            " O.ID as OID, " +
                            " O.LOGIN, " +
                            " I.ID as IID, " +
                            " I.IDENTIFICACAO " +
                            "from " +
                            " ECF_MOVIMENTO M, ECF_TURNO T, ECF_CAIXA C, ECF_OPERADOR O, ECF_IMPRESSORA I " +
                            "where " +
                            " M.ID = " + Convert.ToString(pId) + " AND " +
                            " M.ID_ECF_TURNO = T.ID AND " +
                            " M.ID_ECF_IMPRESSORA = I.ID AND " +
                            " M.ID_ECF_OPERADOR = O.ID AND " +
                            " M.ID_ECF_CAIXA = C.ID AND" +
                            " (STATUS_MOVIMENTO=" + Biblioteca.QuotedStr(pStatusMovimento) + ")";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                if (leitor.Read())
                {

                    MovimentoVO Movimento = new MovimentoVO();
                    Movimento.Id = Convert.ToInt32(leitor["MID"]);
                    Movimento.IdGerenteSupervisor = Convert.ToInt32(leitor["ID_GERENTE_SUPERVISOR"]);
                    Movimento.DataAbertura = (DateTime)(leitor["DATA_ABERTURA"]);
                    Movimento.HoraAbertura = Convert.ToString(leitor["HORA_ABERTURA"]);
                    Movimento.DataFechamento = leitor["DATA_FECHAMENTO"] as DateTime?;
                    Movimento.HoraFechamento = Convert.ToString(leitor["HORA_FECHAMENTO"]);
                    Movimento.TotalSuprimento = leitor["TOTAL_SUPRIMENTO"] as decimal?;
                    Movimento.TotalSangria = leitor["TOTAL_SANGRIA"] as decimal?;
                    Movimento.TotalNaoFiscal = leitor["TOTAL_NAO_FISCAL"] as decimal?;
                    Movimento.TotalVenda = leitor["TOTAL_VENDA"] as decimal?;
                    Movimento.TotalDesconto = leitor["TOTAL_DESCONTO"] as decimal?;
                    Movimento.TotalAcrescimo = leitor["TOTAL_ACRESCIMO"] as decimal?;
                    Movimento.TotalFinal = leitor["TOTAL_FINAL"] as decimal?;
                    Movimento.TotalRecebido = leitor["TOTAL_RECEBIDO"] as decimal?;
                    Movimento.TotalTroco = leitor["TOTAL_TROCO"] as decimal?;
                    Movimento.TotalCancelado = leitor["TOTAL_CANCELADO"] as decimal?;
                    Movimento.Status = Convert.ToString(leitor["STATUS_MOVIMENTO"]);
                    Movimento.IdTurno = Convert.ToInt32(leitor["TID"]);
                    Movimento.DescricaoTurno = Convert.ToString(leitor["DESCRICAO"]);
                    Movimento.IdCaixa = Convert.ToInt32(leitor["CID"]);
                    Movimento.NomeCaixa = Convert.ToString(leitor["NOME"]);
                    Movimento.IdOperador = Convert.ToInt32(leitor["OID"]);
                    Movimento.LoginOperador = Convert.ToString(leitor["LOGIN"]);
                    Movimento.IdImpressora = Convert.ToInt32(leitor["IID"]);
                    Movimento.IdentificacaoImpressora = Convert.ToString(leitor["IDENTIFICACAO"]);
                    return Movimento;
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


        public bool Suprimento(SuprimentoVO pSuprimento)
        {
            ConsultaSQL =
            "insert into ECF_SUPRIMENTO (ID_ECF_MOVIMENTO,DATA_SUPRIMENTO,VALOR)" +
            " values (?pIdMovimento,?pDataSuprimento,?pValor)";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pIdMovimento", pSuprimento.IdMovimento);
                comando.Parameters.AddWithValue("?pDataSuprimento", pSuprimento.DataSuprimento);
                comando.Parameters.AddWithValue("?pValor", pSuprimento.Valor);
                comando.ExecuteNonQuery();
                return true;
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
                return false;
            }
        }


        public bool Sangria(SangriaVO pSangria)
        {
            ConsultaSQL =
            "insert into ECF_SANGRIA (ID_ECF_MOVIMENTO,DATA_SANGRIA,VALOR)" +
            " values (?pIdMovimento,?pDataSangria,?pValor)";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pIdMovimento", pSangria.IdMovimento);
                comando.Parameters.AddWithValue("?pDataSangria", pSangria.DataSangria);
                comando.Parameters.AddWithValue("?pValor", pSangria.Valor);
                comando.ExecuteNonQuery();
                return true;
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
                return false;
            }
        }


        public bool PrimeiroMovimento(System.DateTime pData)
        {
            ConsultaSQL =
            "select Count(ID) as Total " +
            " from ECF_MOVIMENTO where DATA_ABERTURA = ?pDataAbertura";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pDataAbertura", pData);
                leitor = comando.ExecuteReader();
                leitor.Read();
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

        public SuprimentoVO UltimoSuprimento()
        {
            ConsultaSQL = "select * from ECF_SUPRIMENTO "
                    + "where id = (select max(id) from ECF_SUPRIMENTO)";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();
                if (leitor.HasRows)
                {
                    SuprimentoVO Suprimento = new SuprimentoVO();
                    Suprimento.Id = Convert.ToInt32(leitor["ID"]);
                    Suprimento.IdMovimento = Convert.ToInt32(leitor["ID_ECF_MOVIMENTO"]);
                    Suprimento.DataSuprimento = (DateTime)(leitor["DATA_SUPRIMENTO"]);
                    Suprimento.Valor = Convert.ToDecimal(leitor["VALOR"]);

                    return Suprimento;
                }
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


        public SangriaVO UltimaSangria()
        {
            ConsultaSQL = "select * from ECF_SANGRIA "
                    + "where id = (select max(id) from ECF_SANGRIA)";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();
                if (leitor.HasRows)
                {
                    SangriaVO Sangria = new SangriaVO();
                    Sangria.Id = Convert.ToInt32(leitor["ID"]);
                    Sangria.IdMovimento = Convert.ToInt32(leitor["ID_ECF_MOVIMENTO"]);
                    Sangria.DataSangria = (DateTime)(leitor["DATA_SANGRIA"]);
                    Sangria.Valor = Convert.ToDecimal(leitor["VALOR"]);

                    return Sangria;
                }
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


    }

}

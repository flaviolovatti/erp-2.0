/********************************************************************************
Title: T2TiPDV
Description: Classe de controle do fechamento

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

namespace PafEcf.Controller
{

    public class FechamentoController
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public FechamentoController()
        {
            conexao = dbConnection.conectar();
        }


        public FechamentoVO GravaFechamento(FechamentoVO pFechamento)
        {
            ConsultaSQL = "insert into ecf_fechamento ( " +
                            "ID_ECF_MOVIMENTO," +
                            "TIPO_PAGAMENTO," +
                            "VALOR)" +
                            " values (" +
                            "?pID_ECF_MOVIMENTO," +
                            "?pTIPO_PAGAMENTO," +
                            "?pVALOR)";
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pID_ECF_MOVIMENTO", pFechamento.IdMovimento);
                comando.Parameters.AddWithValue("?pTIPO_PAGAMENTO", pFechamento.TipoPagamento);
                comando.Parameters.AddWithValue("?pVALOR", pFechamento.Valor);
                comando.ExecuteNonQuery();

                comando = new MySqlCommand("select max(ID) as ID from ECF_FECHAMENTO", conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();
                pFechamento.Id = Convert.ToInt32(leitor["ID"]);
                return pFechamento;
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
            //TODO:  Grave o arquivo de integração para a retaguarda
        }


        public bool ExcluiFechamento(int pId)
        {

            ConsultaSQL = "delete from ecf_fechamento where id = ?pID ";
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pID", pId);
                comando.ExecuteNonQuery();
                return true;

            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
                return false;
            }
            finally
            {
            }
            //TODO:  Grave o arquivo de integração para a retaguarda
        }

    }

}

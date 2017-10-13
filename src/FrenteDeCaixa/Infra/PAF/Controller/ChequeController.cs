/********************************************************************************
Title: T2TiPDV
Description: Classe de controle dos cheques dos clientes

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
using PafEcf.Util;
using PafEcf.VO;
using PafEcf.Infra;

namespace PafEcf.Controller
{

    public class ChequeController
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public ChequeController()
        {
            conexao = dbConnection.conectar();
        }

        public bool IncluirCheque(List<ChequeClienteVO> pListaCheque)
        {
            ConsultaSQL = "insert into ECF_CHEQUE_CLIENTE ( " +
                            "ID_BANCO," +
                            "ID_CLIENTE," +
                            "ID_ECF_MOVIMENTO," +
                            "NUMERO_CHEQUE," +
                            "DATA_CHEQUE," +
                            "AGENCIA," +
                            "CONTA," +
                            "OBSERVACOES," +
                            "TIPO_CHEQUE," +
                            "VALOR_CHEQUE) values (" +
                            "?pID_BANCO," +
                            "?pID_CLIENTE," +
                            "?pID_ECF_MOVIMENTO," +
                            "?pNUMERO_CHEQUE," +
                            "?pDATA_CHEQUE," +
                            "?pAGENCIA," +
                            "?pCONTA," +
                            "?pOBSERVACOES," +
                            "?pTIPO_CHEQUE," +
                            "?pVALOR_CHEQUE)";


            try
            {
                for (int i = 0; i <= pListaCheque.Count; i++)
                {
                    ChequeClienteVO Cheque = pListaCheque[i];
                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    comando.Parameters.AddWithValue("pID_BANCO", Cheque.IdBanco);
                    comando.Parameters.AddWithValue("pID_CLIENTE", Cheque.IdCliente);
                    comando.Parameters.AddWithValue("pID_ECF_MOVIMENTO", Cheque.IdEcfMovimento);
                    comando.Parameters.AddWithValue("pNUMERO_CHEQUE", Cheque.NumeroCheque);
                    comando.Parameters.AddWithValue("pDATA_CHEQUE", Cheque.DataCheque);
                    comando.Parameters.AddWithValue("pAGENCIA", Cheque.Agencia);
                    comando.Parameters.AddWithValue("pCONTA", Cheque.Conta);
                    comando.Parameters.AddWithValue("pOBSERVACOES", Cheque.Observacoes);
                    comando.Parameters.AddWithValue("pTIPO_CHEQUE", Cheque.TipoCheque);
                    comando.Parameters.AddWithValue("pVALOR_CHEQUE", Cheque.ValorCheque);
                    comando.ExecuteNonQuery();
                }
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

        }


        public bool ExcluirCheque(int pId)
        {
            ConsultaSQL = "delete from ECF_CHEQUE_CLIENTE  where id = ?pID ";
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pId", pId);
                if (comando.ExecuteNonQuery() > 0)
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
            }
        }
    }

}

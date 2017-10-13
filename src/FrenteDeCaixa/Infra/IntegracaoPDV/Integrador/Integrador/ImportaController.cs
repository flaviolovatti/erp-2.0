/********************************************************************************
Title: T2TiPDV
Description: Classe de controle de importação dos dados do Integrador

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
using MySql.Data.MySqlClient;
using PafEcf.Util;
using PafEcf.Infra;

namespace PafEcf.Controller
{


    public class ImportaController
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public ImportaController()
        {
            conexao = dbConnection.conectarRetaguarda();
        }


        public Boolean GravaCargaSangria(string pTupla)
        {
            string[] tupla = pTupla.Split('|');
            try
            {
                ConsultaSQL =
                        "insert into"
                        + " ECF_SANGRIA "
                        + "(NOME_CAIXA, "
                        + "ID_GERADO_CAIXA, "
                        + "ID_ECF_MOVIMENTO, "
                        + "DATA_SANGRIA, "
                        + "VALOR, "
                        + "DATA_SINCRONIZACAO, "
                        + "HORA_SINCRONIZACAO)"
                        + " values ("
                        + tupla[2] + " ," //  "NOME_CAIXA, "+
                        + tupla[3] + " ," //  "ID_GERADO_CAIXA, "+
                        + tupla[4] + " ," //  "ID_ECF_MOVIMENTO, "+
                        + tupla[5] + " ," //  "DATA_SUPRIMENTO, "+
                        + tupla[6].Replace(',','.') + " ," //  "VALOR, "+
                        + "'" + DateTime.Now.ToString("yyyy-MM-dd") + "' ," //  "DATA_SINCRONIZACAO, "+
                        + "'" + DateTime.Now.ToString("HH:mm:ss") + "')";               //  "HORA_SINCRONIZACAO[]"+

                comando = new MySqlCommand(ConsultaSQL, conexao);
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
        }

        public Boolean GravaCargaSuprimento(string pTupla)
        {
            string[] tupla = pTupla.Split('|');
            try
            {
                ConsultaSQL =
                        "insert into"
                        + " ECF_SUPRIMENTO "
                        + "(NOME_CAIXA, "
                        + "ID_GERADO_CAIXA, "
                        + "ID_ECF_MOVIMENTO, "
                        + "DATA_SUPRIMENTO, "
                        + "VALOR, "
                        + "DATA_SINCRONIZACAO, "
                        + "HORA_SINCRONIZACAO)"
                        + " values ("
                        + tupla[2] + " ," //  "NOME_CAIXA, "+
                        + tupla[3] + " ," //  "ID_GERADO_CAIXA, "+
                        + tupla[4] + " ," //  "ID_ECF_MOVIMENTO, "+
                        + tupla[5] + " ," //  "DATA_SUPRIMENTO, "+
                        + tupla[6].Replace(',', '.') + " ," //  "VALOR, "+
                        + "'" + DateTime.Now.ToString("yyyy-MM-dd") + "' ," //  "DATA_SINCRONIZACAO, "+
                        + "'" + DateTime.Now.ToString("HH:mm:ss") + "')";               //  "HORA_SINCRONIZACAO[]"+

                comando = new MySqlCommand(ConsultaSQL, conexao);
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
        }

    }

}
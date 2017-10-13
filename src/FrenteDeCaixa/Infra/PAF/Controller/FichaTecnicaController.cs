/********************************************************************************
Title: T2TiPDV
Description: Classe de controle da Ficha Técnica

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

    public class FichaTecnicaController
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public FichaTecnicaController()
        {
            conexao = dbConnection.conectarRetaguarda();
        }


        public bool GravaFichaTecnica(FichaTecnicaVO FichaTecnica)
        {

            ConsultaSQL = "insert into ficha_tecnica ( " +
                            "ID_PRODUTO," +
                            "DESCRICAO," +
                            "ID_PRODUTO_FILHO," +
                            "QUANTIDADE) " +
                            " values (" +
                            "?pID_PRODUTO," +
                            "?pDESCRICAO," +
                            "?pID_PRODUTO_FILHO," +
                            "?pQUANTIDADE)";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pID_PRODUTO", FichaTecnica.IdProduto);
                comando.Parameters.AddWithValue("?pDESCRICAO", FichaTecnica.Descricao);
                comando.Parameters.AddWithValue("?pID_PRODUTO_FILHO", FichaTecnica.IdProdutoFilho);
                comando.Parameters.AddWithValue("?pQUANTIDADE", FichaTecnica.Quantidade);
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


        public bool ExcluiFichaTecnica(int pId)
        {
            ConsultaSQL = "delete from FICHA_TECNICA where id = ?pID ";

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
        }


        public List<FichaTecnicaVO> TabelaFichaTecnica(string pIdProduto)
        {
            List<FichaTecnicaVO> ListaFichaTecnica = new List<FichaTecnicaVO>();

            ConsultaSQL = " select " +
                            " f.ID, " +
                            " f.ID_PRODUTO, " +
                            " f.DESCRICAO, " +
                            " f.ID_PRODUTO_FILHO, " +
                            " f.QUANTIDADE " +
                            " from " +
                            " FICHA_TECNICA f " +
                            " where " +
                            " f.ID_PRODUTO = " + Biblioteca.QuotedStr(pIdProduto);

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    FichaTecnicaVO FichaTecnica = new FichaTecnicaVO();

                    FichaTecnica.Id = Convert.ToInt32(leitor["ID"]);
                    FichaTecnica.IdProduto = Convert.ToInt32(leitor["ID_PRODUTO"]);
                    FichaTecnica.IdProdutoFilho = Convert.ToInt32(leitor["ID_PRODUTO_FILHO"]);
                    FichaTecnica.Descricao = leitor["DESCRICAO"].ToString();
                    FichaTecnica.Quantidade = Convert.ToDecimal(leitor["QUANTIDADE"]);

                    ListaFichaTecnica.Add(FichaTecnica);
                }
                return ListaFichaTecnica;
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

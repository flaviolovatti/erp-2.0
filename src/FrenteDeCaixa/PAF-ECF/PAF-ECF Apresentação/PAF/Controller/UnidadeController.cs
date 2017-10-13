/********************************************************************************
Title: T2TiPDV
Description: Classe de controle da unidade

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


    public class UnidadeController
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public UnidadeController()
        {
            conexao = dbConnection.conectar();
        }


        public List<UnidadeProdutoVO> TabelaUnidade()
        {
            ConsultaSQL = "select * from UNIDADE_PRODUTO";
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();

                List<UnidadeProdutoVO> ListaUnidade = new List<UnidadeProdutoVO>();

                while (leitor.Read())
                {
                    UnidadeProdutoVO Unidade = new UnidadeProdutoVO();
                    Unidade.Id = Convert.ToInt32(leitor["ID"]);
                    Unidade.Nome = Convert.ToString(leitor["NOME"]);
                    Unidade.Descricao = Convert.ToString(leitor["DESCRICAO"]);
                    Unidade.PodeFracionar = Convert.ToString(leitor["PODE_FRACIONAR"]);
                    ListaUnidade.Add(Unidade);
                }
                return ListaUnidade;
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


        public List<UnidadeProdutoVO> UnidadeSPED(string pDataInicial, string pDataFinal)
        {

            ConsultaSQL = "SELECT distinct U.* " +
                           " FROM UNIDADE_PRODUTO U, PRODUTO P, ECF_VENDA_CABECALHO V, ECF_VENDA_DETALHE D " +
                           " WHERE V.DATA_VENDA BETWEEN " + Biblioteca.QuotedStr(pDataInicial) + " and " + Biblioteca.QuotedStr(pDataFinal) +
                           " AND P.ID_UNIDADE_PRODUTO=U.ID " +
                           " AND V.ID=D.ID_ECF_VENDA_CABECALHO" +
                           " AND D.ID_ECF_PRODUTO=P.ID";
            try
            {


                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();

                List<UnidadeProdutoVO> ListaUnidade = new List<UnidadeProdutoVO>();

                while (leitor.Read())
                {
                    UnidadeProdutoVO Unidade = new UnidadeProdutoVO();
                    Unidade.Id = Convert.ToInt32(leitor["ID"]);
                    Unidade.Nome = Convert.ToString(leitor["NOME"]);
                    Unidade.Descricao = Convert.ToString(leitor["DESCRICAO"]);
                    Unidade.PodeFracionar = Convert.ToString(leitor["PODE_FRACIONAR"]);
                    ListaUnidade.Add(Unidade);
                }
                return ListaUnidade;
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


        public bool ConsultaIdUnidade(int pId)
        {
            ConsultaSQL = "select ID from UNIDADE_PRODUTO where id = " + Convert.ToString(pId);
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
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


        public bool GravaCargaUnidadeProduto(string pTupla)
        {
            String[] tupla = pTupla.Split('|');
            int id;
            try
            {
                id = Convert.ToInt32(tupla[1]);   //ID  INTEGER NOT NULL,
                if (!ConsultaIdUnidade(id))
                {
                    ConsultaSQL =
                            "insert into"
                            + " UNIDADE_PRODUTO "
                            + " (ID, "
                            + " NOME, "
                            + " DESCRICAO, "
                            + " PODE_FRACIONAR) "
                            + "values ("
                            + id + ", " //    ID      INTEGER NOT NULL,
                            + tupla[2] + ", " //    NOME            VARCHAR(10),
                            + tupla[3] + ", " //    DESCRICAO       VARCHAR(250),
                            + tupla[4] + ")";   //    PODE_FRACIONAR  CHAR(1)
                }
                else
                {
                    ConsultaSQL =
                            "update "
                            + " UNIDADE_PRODUTO "
                            + "set "
                            + " NOME = " + tupla[2] + ", " //    NOME            VARCHAR(10),
                            + " DESCRICAO =" + tupla[3] + ", " //    DESCRICAO       VARCHAR(250),
                            + " PODE_FRACIONAR =" + tupla[4] //    PODE_FRACIONAR  CHAR(1)
                            + "where "
                            + "ID =" + id;
                }
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
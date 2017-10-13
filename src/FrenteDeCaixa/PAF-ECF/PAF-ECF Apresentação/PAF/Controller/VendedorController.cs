/********************************************************************************
Title: T2TiPDV
Description: Classe de controle do vendedor

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

    public class VendedorController
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public VendedorController()
        {
            conexao = dbConnection.conectar();
        }

        public bool ConsultaIdVendedor(int pId)
        {
            ConsultaSQL = "select ID from ECF_FUNCIONARIO where id = " + Convert.ToString(pId);
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


        public FuncionarioVO ConsultaVendedor(int pId)
        {

            ConsultaSQL = "select * from ECF_FUNCIONARIO where " +
                           "ID=" + Convert.ToString(pId);
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();

                FuncionarioVO Vendedor = new FuncionarioVO();
                Vendedor.Id = Convert.ToInt32(leitor["ID"]);
                Vendedor.Nome = Convert.ToString(leitor["NOME"]);
                return Vendedor;
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


        public bool GravaCargaFuncionario(string pTupla)
        {
            String[] tupla = pTupla.Split('|');
            int id;
            try
            {
                id = Convert.ToInt32(tupla[1]);   //ID  INTEGER NOT NULL,

                if (!ConsultaIdVendedor(id))
                {
                    ConsultaSQL =
                            "insert into"
                            + " ECF_FUNCIONARIO "
                            + " (ID, "
                            + "NOME, "
                            + "TELEFONE, "
                            + "CELULAR, "
                            + "EMAIL, "
                            + "COMISSAO_VISTA, "
                            + "COMISSAO_PRAZO, "
                            + "NIVEL_AUTORIZACAO) "
                            + "values ("
                            + id + ", " //    ID      INTEGER NOT NULL,
                            + tupla[2] + ", " //    NOME               VARCHAR(100),
                            + tupla[3] + ", " //    TELEFONE           VARCHAR(10),
                            + tupla[4] + ", " //    CELULAR            VARCHAR(10),
                            + tupla[5] + ", " //    EMAIL              VARCHAR(250),
                            + tupla[6] + ", " //    COMISSAO_VISTA     DECIMAL(18,6),
                            + tupla[7] + ", " //    COMISSAO_PRAZO     DECIMAL(18,6),
                            + tupla[8] + ")";   //    NIVEL_AUTORIZACAO  CHAR(1)
                }
                else
                {
                    ConsultaSQL =
                            "update "
                            + " ECF_FUNCIONARIO "
                            + "set "
                            + " NOME = " + tupla[2] + ", " //    NOME               VARCHAR(100),
                            + " TELEFONE =" + tupla[3] + ", " //    TELEFONE           VARCHAR(10),
                            + " CELULAR =" + tupla[4] //    CELULAR            VARCHAR(10),
                            + " EMAIL =" + tupla[5] //    EMAIL              VARCHAR(250),
                            + " COMISSAO_VISTA =" + tupla[6] //    COMISSAO_VISTA     DECIMAL(18,6),
                            + " COMISSAO_PRAZO =" + tupla[7] //    COMISSAO_PRAZO     DECIMAL(18,6),
                            + " NIVEL_AUTORIZACAO =" + tupla[8] //    NIVEL_AUTORIZACAO  CHAR(1)
                            + " where "
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
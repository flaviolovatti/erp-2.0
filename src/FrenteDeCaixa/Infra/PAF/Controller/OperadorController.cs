/********************************************************************************
Title: T2TiPDV
Description: Classe de controle do operador.

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

    public class OperadorController
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public OperadorController()
        {
            conexao = dbConnection.conectar();
        }


        public string ConsultaFuncionario(int pId)
        {

            ConsultaSQL =
              "select ID, NOME  from ECF_FUNCIONARIO where ID =" + Convert.ToString(pId);

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();
                return Convert.ToString(leitor["NOME"]);
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
                return "";
            }
            finally
            {
                if (leitor != null)
                    leitor.Close();
            }
        }


        public bool ConsultaIdOperador(int pId)
        {

            ConsultaSQL = "select ID from ECF_FUNCIONARIO where ID =" + Convert.ToString(pId);
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


        public OperadorVO ConsultaUsuario(string pLogin, string pSenha)
        {
            ConsultaSQL = "select " +
                           " O.ID, " +
                           " O.ID_ECF_FUNCIONARIO, " +
                           " O.LOGIN, " +
                           " O.SENHA, " +
                           " F.NIVEL_AUTORIZACAO " +
                           "from " +
                           " ECF_OPERADOR O, ECF_FUNCIONARIO F " +
                           "where " +
                           " O.ID_ECF_FUNCIONARIO=F.ID " +
                           " and LOGIN=" + Biblioteca.QuotedStr(pLogin) +
                           " and SENHA=" + Biblioteca.QuotedStr(pSenha);
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                if (leitor.Read())
                {
                    OperadorVO Operador = new OperadorVO();
                    Operador.Id = Convert.ToInt32(leitor["ID"]);
                    Operador.IdFuncionario = Convert.ToInt32(leitor["ID_ECF_FUNCIONARIO"]);
                    Operador.Login = Convert.ToString(leitor["LOGIN"]);
                    Operador.Senha = Convert.ToString(leitor["SENHA"]);
                    Operador.Nivel = Convert.ToString(leitor["NIVEL_AUTORIZACAO"]);
                    return Operador;
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


        public List<FuncionarioVO> RetornaFuncionario()
        {

            ConsultaSQL = "select ID, NOME from ECF_FUNCIONARIO order by NOME ";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                if (!leitor.HasRows)
                {
                    return null;
                }
                else
                {
                    List<FuncionarioVO> ListaFuncionario = new List<FuncionarioVO>();
                    while (leitor.Read())
                    {
                        FuncionarioVO Funcionario = new FuncionarioVO();
                        Funcionario.Id = Convert.ToInt32(leitor["ID"]);
                        Funcionario.Nome = Convert.ToString(leitor["NOME"]);
                        ListaFuncionario.Add(Funcionario);
                    }
                    return ListaFuncionario;
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


        public bool GravaCargaOperador(string pTupla)
        {
            String[] tupla = pTupla.Split('|');
            int id;
            try
            {
                id = Convert.ToInt32(tupla[1]);   //ID  INTEGER NOT NULL,
                if (!ConsultaIdOperador(id))
                {
                    ConsultaSQL =
                            "insert into"
                            + " ECF_OPERADOR "
                            + " (ID, "
                            + " ID_ECF_FUNCIONARIO, "
                            + " LOGIN, "
                            + " SENHA) "
                            + "values ("
                            + id + ", " //    ID      INTEGER NOT NULL,
                            + tupla[2] + ", " //     ID_ECF_FUNCIONARIO  INTEGER NOT NULL,
                            + tupla[3] + ", " //     LOGIN               VARCHAR(20),
                            + tupla[4] + ")";   //     SENHA               VARCHAR(20)
                }
                else
                {
                    ConsultaSQL =
                            "update "
                            + " ECF_OPERADOR "
                            + "set "
                            + " ID_ECF_FUNCIONARIO = " + tupla[2] + ", " //     ID_ECF_FUNCIONARIO  INTEGER NOT NULL,
                            + " LOGIN =" + tupla[3] + ", " //     LOGIN               VARCHAR(20),
                            + " SENHA =" + tupla[4] //     SENHA               VARCHAR(20)
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
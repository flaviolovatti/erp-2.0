/********************************************************************************
Title: T2TiPDV
Description: Classe de controle do contador

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
using PafEcf.VO;
using PafEcf.Infra;

namespace PafEcf.Controller
{

    public class ContadorController
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public ContadorController()
        {
            conexao = dbConnection.conectar();
        }

        public ContadorVO PegaContador()
        {
            ContadorVO Contador = new ContadorVO();
            ConsultaSQL = "select * from ECF_CONTADOR";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                if (leitor.HasRows)
                {
                    Contador.Id = Convert.ToInt32(leitor["ID"]);
                    Contador.CPF = Convert.ToString(leitor["CPF"]);
                    Contador.CNPJ = Convert.ToString(leitor["CNPJ"]);
                    Contador.Nome = Convert.ToString(leitor["NOME"]);
                    Contador.CRC = Convert.ToString(leitor["INSCRICAO_CRC"]);
                    Contador.Fone = Convert.ToString(leitor["FONE"]);
                    Contador.Fax = Convert.ToString(leitor["FAX"]);
                    Contador.Logradouro = Convert.ToString(leitor["LOGRADOURO"]);
                    Contador.Numero = Convert.ToInt32(leitor["NUMERO"]);
                    Contador.Complemento = Convert.ToString(leitor["COMPLEMENTO"]);
                    Contador.Bairro = Convert.ToString(leitor["BAIRRO"]);
                    Contador.CEP = Convert.ToString(leitor["CEP"]);
                    Contador.CodigoMunicipio = Convert.ToInt32(leitor["CODIGO_MUNICIPIO"]);
                    Contador.UF = Convert.ToString(leitor["UF"]);
                    Contador.Email = Convert.ToString(leitor["EMAIL"]);
                    return Contador;
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


        public bool ConsultaIdContador(int pId)
        {

            ConsultaSQL = "select ID from ECF_CONTADOR where (ID = ?pID) ";
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pId", pId);
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


        public Boolean GravaCargaContador(String pTupla)
        {
            String[] tupla = pTupla.Split('|');
            int id;
            try
            {
                id = Convert.ToInt32(tupla[1]);   //ID  INTEGER NOT NULL,

                if (!ConsultaIdContador(id))
                {
                    ConsultaSQL =
                            "insert into"
                            + " ECF_CONTADOR "
                            + "(ID, "
                            + "ID_ECF_EMPRESA, "//2
                            + "CPF, "//3
                            + "CNPJ, "//4
                            + "NOME, "//5
                            + "INSCRICAO_CRC, "//6
                            + "FONE, "//7
                            + "FAX, "//8
                            + "LOGRADOURO, "//9
                            + "NUMERO, "//10
                            + "COMPLEMENTO, "//11
                            + "BAIRRO, "//12
                            + "CEP, "//13
                            + "CODIGO_MUNICIPIO, "//14
                            + "UF, "//15
                            + "EMAIL)"//16
                            + "values ("
                            + id + ", " //    ID      INTEGER NOT NULL,
                            + tupla[2] + ", " //    ID_ECF_EMPRESA    INTEGER NOT NULL,
                            + tupla[3] + ", " //    CPF               VARCHAR(11),
                            + tupla[4] + ", " //    CNPJ              VARCHAR(14),
                            + tupla[5] + ", " //    NOME              VARCHAR(100),
                            + tupla[6] + ", " //    INSCRICAO_CRC     VARCHAR(15),
                            + tupla[7] + ", " //    FONE              VARCHAR(15),
                            + tupla[8] + ", " //    FAX               VARCHAR(15),
                            + tupla[9] + ", " //    LOGRADOURO        VARCHAR(100),
                            + tupla[10] + ", " //    NUMERO            INTEGER,
                            + tupla[11] + ", " //    COMPLEMENTO       VARCHAR(100),
                            + tupla[12] + ", " //    BAIRRO            VARCHAR(30),
                            + tupla[13] + ", " //    CEP               VARCHAR(8),
                            + tupla[14] + ", " //    CODIGO_MUNICIPIO  INTEGER,
                            + tupla[15] + ", " //    UF                CHAR(2),
                            + tupla[16] + ')';     //    EMAIL             VARCHAR(250)
                }
                else
                {
                    ConsultaSQL =
                            "update "
                            + " ECF_CONTADOR "
                            + "set "
                            + "ID_ECF_EMPRESA =" + tupla[2] + "," //    ID_ECF_EMPRESA    INTEGER NOT NULL,
                            + "CPF =" + tupla[3] + "," //    CPF               VARCHAR(11),
                            + "CNPJ =" + tupla[4] + "," //    CNPJ              VARCHAR(14),
                            + "NOME =" + tupla[5] + "," //    NOME              VARCHAR(100),
                            + "INSCRICAO_CRC =" + tupla[6] + "," //    INSCRICAO_CRC     VARCHAR(15),
                            + "FONE =" + tupla[7] + "," //    FONE              VARCHAR(15),
                            + "FAX =" + tupla[8] + "," //    FAX               VARCHAR(15),
                            + "LOGRADOURO =" + tupla[9] + "," //    LOGRADOURO        VARCHAR(100),
                            + "NUMERO =" + tupla[10] + "," //    NUMERO            INTEGER,
                            + "COMPLEMENTO =" + tupla[11] + "," //    COMPLEMENTO       VARCHAR(100),
                            + "BAIRRO =" + tupla[12] + "," //    BAIRRO            VARCHAR(30),
                            + "CEP =" + tupla[13] + "," //    CEP               VARCHAR(8),
                            + "CODIGO_MUNICIPIO =" + tupla[14] + "," //    CODIGO_MUNICIPIO  INTEGER,
                            + "UF =" + tupla[15] + "," //    UF                CHAR(2),
                            + "EMAIL =" + tupla[16] + ")" //    EMAIL             VARCHAR(250)
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

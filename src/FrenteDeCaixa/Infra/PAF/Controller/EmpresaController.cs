/********************************************************************************
Title: T2TiPDV
Description: Classe de controle da empresa

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

    public class EmpresaController
    {

        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public EmpresaController()
        {
            conexao = dbConnection.conectar();
        }


        public EmpresaVO PegaEmpresa(int pId)
        {

            ConsultaSQL = "select * from ECF_EMPRESA where ID=" + Convert.ToString(pId);


            try
            {
                EmpresaVO Empresa = new EmpresaVO();

                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();

                Empresa.Id = Convert.ToInt32(leitor["ID"]);
                Empresa.IdEmpresa = Convert.IsDBNull(leitor["ID_EMPRESA"]) ? 0 : Convert.ToInt32(leitor["ID_EMPRESA"]);
                Empresa.RazaoSocial = Convert.ToString(leitor["RAZAO_SOCIAL"]);
                Empresa.NomeFantasia = Convert.ToString(leitor["NOME_FANTASIA"]);
                Empresa.Cnpj = Convert.ToString(leitor["CNPJ"]);
                Empresa.InscricaoEstadual = Convert.ToString(leitor["INSCRICAO_ESTADUAL"]);
                Empresa.InscricaoEstadualSt = Convert.ToString(leitor["INSCRICAO_ESTADUAL_ST"]);
                Empresa.InscricaoMunicipal = Convert.ToString(leitor["INSCRICAO_MUNICIPAL"]);
                Empresa.InscricaoJuntaComercial = Convert.ToString(leitor["INSCRICAO_JUNTA_COMERCIAL"]);
                Empresa.DataInscJuntaComercial = Convert.IsDBNull(leitor["DATA_INSC_JUNTA_COMERCIAL"]) ? new DateTime() : (DateTime)(leitor["DATA_INSC_JUNTA_COMERCIAL"]);
                Empresa.MatrizFilial = Convert.ToString(leitor["MATRIZ_FILIAL"]);
                Empresa.DataCadastro = Convert.IsDBNull(leitor["DATA_CADASTRO"]) ? new DateTime() : (DateTime)(leitor["DATA_CADASTRO"]);
                Empresa.DataInicioAtividades = Convert.IsDBNull(leitor["DATA_INICIO_ATIVIDADES"]) ? new DateTime() : (DateTime)(leitor["DATA_INICIO_ATIVIDADES"]);
                Empresa.Suframa = Convert.ToString(leitor["SUFRAMA"]);
                Empresa.Email = Convert.ToString(leitor["EMAIL"]);
                Empresa.ImagemLogotipo = Convert.ToString(leitor["IMAGEM_LOGOTIPO"]);
                Empresa.Crt = Convert.ToString(leitor["CRT"]);
                Empresa.TipoRegime = Convert.ToString(leitor["TIPO_REGIME"]);
                Empresa.AliquotaPis = Convert.IsDBNull(leitor["ALIQUOTA_PIS"]) ? 0 : Convert.ToDecimal(leitor["ALIQUOTA_PIS"]);
                Empresa.AliquotaCofins = Convert.IsDBNull(leitor["ALIQUOTA_COFINS"]) ? 0 : Convert.ToDecimal(leitor["ALIQUOTA_COFINS"]);
                Empresa.Logradouro = Convert.ToString(leitor["LOGRADOURO"]);
                Empresa.Numero = Convert.ToString(leitor["NUMERO"]);
                Empresa.Complemento = Convert.ToString(leitor["COMPLEMENTO"]);
                Empresa.Cep = Convert.ToString(leitor["CEP"]);
                Empresa.Bairro = Convert.ToString(leitor["BAIRRO"]);
                Empresa.Cidade = Convert.ToString(leitor["CIDADE"]);
                Empresa.Uf = Convert.ToString(leitor["UF"]);
                Empresa.Fone = Convert.ToString(leitor["FONE"]);
                Empresa.Fax = Convert.ToString(leitor["FAX"]);
                Empresa.Contato = Convert.ToString(leitor["CONTATO"]);
                Empresa.CodigoIbgeCidade = Convert.ToInt32(leitor["CODIGO_IBGE_CIDADE"]);
                Empresa.CodigoIbgeUf = Convert.ToInt32(leitor["CODIGO_IBGE_UF"]);

                return Empresa;
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



        public bool ConsultaIdEmpresa(int pId)
        {

            ConsultaSQL = "select ID from ECF_EMPRESA where (ID = ?pID) ";
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


        public bool GravaCargaEmpresa(string pTupla)
        {
            String[] tupla = pTupla.Split('|');
            int id;
            try
            {
                id = Convert.ToInt32(tupla[1]);   //ID  INTEGER NOT NULL,

                if (ConsultaIdEmpresa(id))
                {
                    ConsultaSQL =
                            "update "
                            + " ECF_EMPRESA "
                            + "set "
                            + "ID_EMPRESA =" + tupla[2] + "," //   ID_EMPRESA                 INTEGER
                            + "RAZAO_SOCIAL =" + tupla[3] + "," //   RAZAO_SOCIAL               VARCHAR(150),
                            + "NOME_FANTASIA =" + tupla[4] + "," //   NOME_FANTASIA              VARCHAR(150),
                            + "CNPJ =" + tupla[5] + "," //   CNPJ                       VARCHAR(14),
                            + "INSCRICAO_ESTADUAL =" + tupla[6] + "," //   INSCRICAO_ESTADUAL         VARCHAR(30),
                            + "INSCRICAO_ESTADUAL_ST =" + tupla[7] + "," //   INSCRICAO_ESTADUAL_ST      VARCHAR(30),
                            + "INSCRICAO_MUNICIPAL =" + tupla[8] + "," //   INSCRICAO_MUNICIPAL        VARCHAR(30),
                            + "INSCRICAO_JUNTA_COMERCIAL =" + tupla[9] + "," //   INSCRICAO_JUNTA_COMERCIAL  VARCHAR(30),
                            + "DATA_INSC_JUNTA_COMERCIAL =" + tupla[10] + "," //   DATA_INSC_JUNTA_COMERCIAL  DATE,
                            + "MATRIZ_FILIAL =" + tupla[11] + "," //   MATRIZ_FILIAL              CHAR(1),
                            + "DATA_CADASTRO =" + tupla[12] + "," //   DATA_CADASTRO              DATE,
                            + "DATA_INICIO_ATIVIDADES =" + tupla[13] + "," //   DATA_INICIO_ATIVIDADES     DATE,
                            + "SUFRAMA =" + tupla[14] + "," //   SUFRAMA                    VARCHAR(9),
                            + "EMAIL =" + tupla[15] + "," //   EMAIL                      VARCHAR(250),
                            + "IMAGEM_LOGOTIPO =" + tupla[16] + "," //   IMAGEM_LOGOTIPO            VARCHAR(250),
                            + "CRT =" + tupla[17] + "," //   CRT                        CHAR(1),
                            + "TIPO_REGIME =" + tupla[18] + "," //   TIPO_REGIME                CHAR(1),
                            + "ALIQUOTA_PIS =" + tupla[19] + "," //   ALIQUOTA_PIS               DECIMAL(18,6),
                            + "ALIQUOTA_COFINS =" + tupla[20] + "," //   ALIQUOTA_COFINS            DECIMAL(18,6),
                            + "LOGRADOURO =" + tupla[21] + "," //   LOGRADOURO                 VARCHAR(250),
                            + "NUMERO =" + tupla[22] + "," //   NUMERO                     VARCHAR(6),
                            + "COMPLEMENTO =" + tupla[23] + "," //   COMPLEMENTO                VARCHAR(100),
                            + "CEP =" + tupla[24] + "," //   CEP                        VARCHAR(8),
                            + "BAIRRO =" + tupla[25] + "," //   BAIRRO                     VARCHAR(100),
                            + "CIDADE =" + tupla[26] + "," //   CIDADE                     VARCHAR(100),
                            + "UF =" + tupla[27] + "," //   UF                         CHAR(2),
                            + "FONE =" + tupla[28] + "," //   FONE                       VARCHAR(10),
                            + "FAX =" + tupla[29] + "," //   FAX                        VARCHAR(10),
                            + "CONTATO =" + tupla[30] + "," //   CONTATO                    VARCHAR(30),
                            + "CODIGO_IBGE_CIDADE =" + tupla[31] + "," //   CODIGO_IBGE_CIDADE         INTEGER,
                            + "CODIGO_IBGE_UF =" + tupla[32] + ")" //   CODIGO_IBGE_UF             INTEGER,
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
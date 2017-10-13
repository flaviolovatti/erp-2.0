/********************************************************************************
Title: T2TiPDV
Description: Classe de controle da impressora

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
using System.Collections.Generic;

namespace PafEcf.Controller
{

    public class ImpressoraController
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public ImpressoraController()
        {
            conexao = dbConnection.conectar();
        }


        public ImpressoraVO PegaImpressora(int pId)
        {
            ConsultaSQL = "select * from ECF_IMPRESSORA where ID=" + Convert.ToString(pId);

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();

                if (leitor.Read())
                {
                    ImpressoraVO Impressora = new ImpressoraVO();

                    Impressora.Numero = Convert.ToInt32(leitor["NUMERO"]);
                    Impressora.Codigo = Convert.ToString(leitor["CODIGO"]);
                    Impressora.Serie = Convert.ToString(leitor["SERIE"]);
                    Impressora.Identificacao = Convert.ToString(leitor["IDENTIFICACAO"]);
                    Impressora.MC = Convert.ToString(leitor["MC"]);
                    Impressora.MD = Convert.ToString(leitor["MD"]);
                    Impressora.VR = Convert.ToString(leitor["VR"]);
                    Impressora.Tipo = Convert.ToString(leitor["TIPO"]);
                    Impressora.Marca = Convert.ToString(leitor["MARCA"]);
                    Impressora.Modelo = Convert.ToString(leitor["MODELO"]);
                    Impressora.ModeloACBr = Convert.ToString(leitor["MODELO_ACBR"]);
                    Impressora.ModeloDocumentoFiscal = Convert.ToString(leitor["MODELO_DOCUMENTO_FISCAL"]);
                    Impressora.Versao = Convert.ToString(leitor["VERSAO"]);
                    Impressora.LE = Convert.ToString(leitor["LE"]);
                    Impressora.LEF = Convert.ToString(leitor["LEF"]);
                    Impressora.MFD = Convert.ToString(leitor["MFD"]);
                    Impressora.LacreNaMFD = Convert.ToString(leitor["LACRE_NA_MFD"]);
                    Impressora.DOCTO = Convert.ToString(leitor["DOCTO"]);
                    Impressora.NumeroEcf = Convert.ToString(leitor["ECF_IMPRESSORA"]);
                    Impressora.DataInstalacaoSb = (DateTime)(leitor["DATA_INSTALACAO_SB"]);
                    Impressora.HoraInstalacaoSb = Convert.ToString(leitor["HORA_INSTALACAO_SB"]);

                    return Impressora;
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


        public List<ImpressoraVO> TabelaImpressora()
        {

            ConsultaSQL = "select * from ECF_IMPRESSORA";
            try
            {

                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();

                List<ImpressoraVO> ListaImpressora = new List<ImpressoraVO>();

                while (leitor.Read())
                {
                    ImpressoraVO Impressora = new ImpressoraVO();

                    Impressora.Id = Convert.ToInt32(leitor["ID"]);
                    Impressora.Numero = Convert.ToInt32(leitor["NUMERO"]);
                    Impressora.Codigo = Convert.ToString(leitor["CODIGO"]);
                    Impressora.Serie = Convert.ToString(leitor["SERIE"]);
                    Impressora.Identificacao = Convert.ToString(leitor["IDENTIFICACAO"]);
                    Impressora.MC = Convert.ToString(leitor["MC"]);
                    Impressora.MD = Convert.ToString(leitor["MD"]);
                    Impressora.VR = Convert.ToString(leitor["VR"]);
                    Impressora.Tipo = Convert.ToString(leitor["TIPO"]);
                    Impressora.Marca = Convert.ToString(leitor["MARCA"]);
                    Impressora.Modelo = Convert.ToString(leitor["MODELO"]);
                    Impressora.ModeloACBr = Convert.ToString(leitor["MODELO_ACBR"]);
                    Impressora.ModeloDocumentoFiscal = Convert.ToString(leitor["MODELO_DOCUMENTO_FISCAL"]);
                    Impressora.Versao = Convert.ToString(leitor["VERSAO"]);
                    Impressora.LE = Convert.ToString(leitor["LE"]);
                    Impressora.LEF = Convert.ToString(leitor["LEF"]);
                    Impressora.MFD = Convert.ToString(leitor["MFD"]);
                    Impressora.LacreNaMFD = Convert.ToString(leitor["LACRE_NA_MFD"]);
                    Impressora.DOCTO = Convert.ToString(leitor["DOCTO"]);
                    Impressora.NumeroEcf = Convert.ToString(leitor["ECF_IMPRESSORA"]);
                    Impressora.DataInstalacaoSb = (DateTime)(leitor["DATA_INSTALACAO_SB"]);
                    Impressora.HoraInstalacaoSb = Convert.ToString(leitor["HORA_INSTALACAO_SB"]);

                    ListaImpressora.Add(Impressora);

                }
                return ListaImpressora;
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

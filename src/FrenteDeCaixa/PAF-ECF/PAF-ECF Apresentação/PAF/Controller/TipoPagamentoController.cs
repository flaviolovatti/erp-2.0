/********************************************************************************
Title: T2TiPDV
Description: Classe de controle do tipo de pagamento

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

    public class TipoPagamentoController
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public TipoPagamentoController()
        {
            conexao = dbConnection.conectar();
        }


        public TipoPagamentoVO ConsultaPeloID(int pId)
        {
            ConsultaSQL =
              "select * from ECF_TIPO_PAGAMENTO where id=" + Convert.ToString(pId);

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();

                TipoPagamentoVO TipoPagamento = new TipoPagamentoVO();
                TipoPagamento.Id = Convert.ToInt32(leitor["ID"]);
                TipoPagamento.Codigo = Convert.ToString(leitor["CODIGO"]);
                TipoPagamento.Descricao = Convert.ToString(leitor["DESCRICAO"]);
                TipoPagamento.TEF = Convert.ToString(leitor["TEF"]);
                TipoPagamento.ImprimeVinculado = Convert.ToString(leitor["IMPRIME_VINCULADO"]);
                TipoPagamento.PermiteTroco = Convert.ToString(leitor["PERMITE_TROCO"]);
                TipoPagamento.TipoGP = Convert.ToString(leitor["TEF_TIPO_GP"]);
                TipoPagamento.GeraParcelas = Convert.ToString(leitor["GERA_PARCELAS"]);
                return TipoPagamento;
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


        public List<TipoPagamentoVO> TabelaTipoPagamento()
        {
            ConsultaSQL = "select * from ECF_TIPO_PAGAMENTO";

            try
            {
                List<TipoPagamentoVO> ListaTipoPagamento = new List<TipoPagamentoVO>();
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();

                while (leitor.Read())
                {
                    TipoPagamentoVO TipoPagamento = new TipoPagamentoVO();
                    TipoPagamento.Id = Convert.ToInt32(leitor["ID"]);
                    TipoPagamento.Codigo = Convert.ToString(leitor["CODIGO"]);
                    TipoPagamento.Descricao = Convert.ToString(leitor["DESCRICAO"]);
                    TipoPagamento.TEF = Convert.ToString(leitor["TEF"]);
                    TipoPagamento.ImprimeVinculado = Convert.ToString(leitor["IMPRIME_VINCULADO"]);
                    TipoPagamento.PermiteTroco = Convert.ToString(leitor["PERMITE_TROCO"]);
                    TipoPagamento.TipoGP = Convert.ToString(leitor["TEF_TIPO_GP"]);
                    TipoPagamento.GeraParcelas = Convert.ToString(leitor["GERA_PARCELAS"]);
                    ListaTipoPagamento.Add(TipoPagamento);
                }
                return ListaTipoPagamento;
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


        public TipoPagamentoVO ConsultaPelaDescricao(string pDescricao)
        {

            ConsultaSQL =
              "select * from ECF_TIPO_PAGAMENTO where descricao=" + Biblioteca.QuotedStr(pDescricao);
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();
                TipoPagamentoVO TipoPagamento = new TipoPagamentoVO();
                TipoPagamento.Id = Convert.ToInt32(leitor["ID"]);
                TipoPagamento.Codigo = Convert.ToString(leitor["CODIGO"]);
                TipoPagamento.Descricao = Convert.ToString(leitor["DESCRICAO"]);
                TipoPagamento.TEF = Convert.ToString(leitor["TEF"]);
                TipoPagamento.ImprimeVinculado = Convert.ToString(leitor["IMPRIME_VINCULADO"]);
                TipoPagamento.PermiteTroco = Convert.ToString(leitor["PERMITE_TROCO"]);
                TipoPagamento.TipoGP = Convert.ToString(leitor["TEF_TIPO_GP"]);
                TipoPagamento.GeraParcelas = Convert.ToString(leitor["GERA_PARCELAS"]);
                return TipoPagamento;
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
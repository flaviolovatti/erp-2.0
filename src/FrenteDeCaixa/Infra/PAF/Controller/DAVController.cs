/********************************************************************************
Title: T2TiPDV
Description: Classe de controle do DAV

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
using PafEcf.View;

namespace PafEcf.Controller
{

    public class DAVController
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public DAVController()
        {
            conexao = dbConnection.conectarRetaguarda();
        }

        public List<DavDetalheVO> CarregaDAV(int? pId)
        {
            // verifica se existe o DAV solicitado
            ConsultaSQL =
              "select count(*) as TOTAL from DAV_CABECALHO " +
              "where SITUACAO <> " + Biblioteca.QuotedStr("E") + " and SITUACAO <> " + Biblioteca.QuotedStr("M") + " and ID=" + Convert.ToString(pId);

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();

                //  caso exista o DAV, procede com a importacao do mesmo
                if (leitor.HasRows)
                {
                    leitor.Close();

                    // verifica se existem itens para o DAV
                    ConsultaSQL =
                      "select count(*) as TOTAL from DAV_DETALHE where ID_DAV_CABECALHO=" + Convert.ToString(pId);

                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    leitor = comando.ExecuteReader();
                    leitor.Read();

                    // caso existam itens no detalhe
                    if (leitor.HasRows)
                    {
                        leitor.Close();

                        List<DavDetalheVO> ListaDAV = new List<DavDetalheVO>();

                        ConsultaSQL =
                          "select * from DAV_DETALHE where ID_DAV_CABECALHO=" + Convert.ToString(pId);
                        comando = new MySqlCommand(ConsultaSQL, conexao);
                        leitor = comando.ExecuteReader();
                        while (leitor.Read())
                        {
                            DavDetalheVO DAVDetalhe = new DavDetalheVO();
                            DAVDetalhe.Id = Convert.ToInt32(leitor["ID"]);
                            DAVDetalhe.IdDavCabecalho = pId.Value;
                            DAVDetalhe.IdProduto = Convert.ToInt32(leitor["ID_PRODUTO"]);
                            DAVDetalhe.Item = Convert.ToInt32(leitor["ITEM"]);
                            DAVDetalhe.Quantidade = Convert.ToInt32(leitor["QUANTIDADE"]);
                            DAVDetalhe.ValorUnitario = Convert.ToDecimal(leitor["VALOR_UNITARIO"]);
                            DAVDetalhe.ValorTotal = Convert.ToDecimal(leitor["VALOR_TOTAL"]);
                            DAVDetalhe.Cancelado = Convert.ToString(leitor["CANCELADO"]);
                            DAVDetalhe.MesclaProduto = Convert.ToString(leitor["MESCLA_PRODUTO"]);
                            DAVDetalhe.GtinProduto = Convert.ToString(leitor["GTIN_PRODUTO"]);
                            DAVDetalhe.NomeProduto = Convert.ToString(leitor["NOME_PRODUTO"]);
                            DAVDetalhe.UnidadeProduto = Convert.ToString(leitor["UNIDADE_PRODUTO"]);
                            DAVDetalhe.TotalizadorParcial = Convert.ToString(leitor["TOTALIZADOR_PARCIAL"]);
                            ListaDAV.Add(DAVDetalhe);
                        }
                        return ListaDAV;
                    }
                    else
                        return null;
                }
                //  caso não exista o DAV, retorna um ponteiro nulo
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


        public void FechaDAV(int? pId, int pCCF, int pCOO)
        {
            string Hash = "";
            string Tripa = "";
            try
            {
                List<DavDetalheVO> ListaDAV = CarregaDAV(pId);
                for (int i = 0; i <= ListaDAV.Count - 1; i++)
                {
                    ConsultaSQL =
                      "update DAV_DETALHE set " +
                      "HASH_TRIPA = ?pHashTripa, " +
                      "HASH_INCREMENTO = ?pHashIncremento " +
                      " where ID = ?pId";

                    Tripa = Convert.ToString(ListaDAV[i].Id) +
                              Convert.ToString(ListaDAV[i].IdDavCabecalho) +
                              Convert.ToString(ListaDAV[i].IdProduto) +
                              ListaDAV[i].NumeroDav +
                              ListaDAV[i].DataEmissao +
                              Convert.ToString(ListaDAV[i].Item) +
                              Biblioteca.FormataFloat("V", ListaDAV[i].Quantidade) +
                              Biblioteca.FormataFloat("V", ListaDAV[i].ValorUnitario) +
                              Biblioteca.FormataFloat("V", ListaDAV[i].ValorTotal) +
                              ListaDAV[i].Cancelado +
                              ListaDAV[i].MesclaProduto +
                              ListaDAV[i].GtinProduto +
                              ListaDAV[i].NomeProduto +
                              ListaDAV[i].TotalizadorParcial +
                              ListaDAV[i].UnidadeProduto + "0";

                    Hash = Biblioteca.MD5String(Tripa);

                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    comando.Parameters.AddWithValue("?pId", ListaDAV[i].Id);
                    comando.Parameters.AddWithValue("?pHashTripa", Hash);
                    comando.Parameters.AddWithValue("?pHashIncremento", -1);
                    comando.ExecuteNonQuery();
                }

                ConsultaSQL =
                  "update DAV_CABECALHO set " +
                  "SITUACAO=?pSituacao, " +
                  "CCF=?pCCF, " +
                  "NUMERO_ECF=?pNUMERO_ECF, " +
                  "COO=?pCOO " +
                  " where ID = ?pId";

                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pId", pId);
                comando.Parameters.AddWithValue("?pCCF", pCCF);
                comando.Parameters.AddWithValue("?pCOO", pCOO);
                comando.Parameters.AddWithValue("?pSituacao", "E");
                //todo_albert comando.Parameters.AddWithValue("?pNUMERO_ECF", UCaixa.ACBrECF.NumECF);
                comando.ExecuteNonQuery();

                // calcula e grava o hash do cabeçalho
                DavCabecalhoVO DAVCabecalho = ConsultaDAVId(pId);
                Tripa = Convert.ToString(DAVCabecalho.Id) +
                          Convert.ToString(DAVCabecalho.IdPessoa) +
                          Convert.ToString(DAVCabecalho.Ccf) +
                          Convert.ToString(DAVCabecalho.Coo) +
                          DAVCabecalho.NomeDestinatario +
                          DAVCabecalho.CpfCnpjDestinatario +
                          DAVCabecalho.DataEmissao +
                          DAVCabecalho.HoraEmissao +
                          DAVCabecalho.Situacao +
                          Biblioteca.FormataFloat("V", DAVCabecalho.TaxaAcrescimo) +
                          Biblioteca.FormataFloat("V", DAVCabecalho.Acrescimo) +
                          Biblioteca.FormataFloat("V", DAVCabecalho.TaxaDesconto) +
                          Biblioteca.FormataFloat("V", DAVCabecalho.Desconto) +
                          Biblioteca.FormataFloat("V", DAVCabecalho.Subtotal) +
                          Biblioteca.FormataFloat("V", DAVCabecalho.Valor) +
                          DAVCabecalho.NumeroDav +
                    //UCaixa.v.NumECF +
                          "0";

                Hash = Biblioteca.MD5String(Tripa);

                ConsultaSQL =
                  "update DAV_CABECALHO set " +
                  "HASH_TRIPA = ?pHashTripa, " +
                  "HASH_INCREMENTO = ?pHashIncremento " +
                  " where ID = ?pId";

                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pId", pId);
                comando.Parameters.AddWithValue("?pHashTripa", Hash);
                comando.Parameters.AddWithValue("?pHashIncremento", -1);
                comando.ExecuteNonQuery();

            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
            finally
            {
            }
        }


        public void MesclaDAV(List<DavCabecalhoVO> ListaDAVCabecalho, List<DavDetalheVO> ListaDAVDetalhe, decimal ValorNovoDav)
        {
            string Tripa, Hash;
            string NumeroUltimoDav, NumeroNovoDav;

            // inicia e configura o novo DAV
            DavCabecalhoVO NovoDAVCabecalho = new DavCabecalhoVO();
            NovoDAVCabecalho.IdPessoa = ListaDAVCabecalho[0].IdPessoa;
            NovoDAVCabecalho.NomeDestinatario = ListaDAVCabecalho[0].NomeDestinatario;
            NovoDAVCabecalho.CpfCnpjDestinatario = ListaDAVCabecalho[0].CpfCnpjDestinatario;
            NovoDAVCabecalho.DataEmissao = new DateTime();
            NovoDAVCabecalho.HoraEmissao = DateTime.Now.ToString("HH:mm:ss");
            NovoDAVCabecalho.Situacao = "P";

            // atualiza a tabela de cabecalho
            for (int i = 0; i <= ListaDAVCabecalho.Count - 1; i++)
            {
                // altera a situacao do DAV selecionado para M de mesclado
                ConsultaSQL =
                  "update DAV_CABECALHO set " +
                  "SITUACAO=?pSituacao, " +
                  "HASH_TRIPA = ?pHashTripa, " +
                  "HASH_INCREMENTO = ?pHashIncremento " +
                  " where ID = ?pId";

                try
                {
                    // calcula e grava o hash
                    Tripa = Convert.ToString(ListaDAVCabecalho[i].Id) +
                              Convert.ToString(ListaDAVCabecalho[i].IdPessoa) +
                              Convert.ToString(ListaDAVCabecalho[i].Ccf) +
                              Convert.ToString(ListaDAVCabecalho[i].Coo) +
                              ListaDAVCabecalho[i].NomeDestinatario +
                              ListaDAVCabecalho[i].CpfCnpjDestinatario +
                              ListaDAVCabecalho[i].DataEmissao +
                              ListaDAVCabecalho[i].HoraEmissao +
                              "M" + // Situacao
                              Biblioteca.FormataFloat("V", ListaDAVCabecalho[i].TaxaAcrescimo) +
                              Biblioteca.FormataFloat("V", ListaDAVCabecalho[i].Acrescimo) +
                              Biblioteca.FormataFloat("V", ListaDAVCabecalho[i].TaxaDesconto) +
                              Biblioteca.FormataFloat("V", ListaDAVCabecalho[i].Desconto) +
                              Biblioteca.FormataFloat("V", ListaDAVCabecalho[i].Subtotal) +
                              Biblioteca.FormataFloat("V", ListaDAVCabecalho[i].Valor) +
                              ListaDAVCabecalho[i].NumeroDav +
                        //UCaixa.ACBrECF.NumECF +
                              "0";

                    Hash = Biblioteca.MD5String(Tripa);

                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    comando.Parameters.AddWithValue("?pId", ListaDAVCabecalho[i].Id);
                    comando.Parameters.AddWithValue("?pSituacao", "M");
                    comando.Parameters.AddWithValue("?pHashTripa", Hash);
                    comando.Parameters.AddWithValue("?pHashIncremento", -1);
                    comando.ExecuteNonQuery();

                }
                catch (Exception eError)
                {
                    Log.write(eError.ToString());
                }
                finally
                {
                }

            }


            // cria um novo dav
            ConsultaSQL =
              "insert into DAV_CABECALHO (" +
              "NUMERO_DAV," +
              "ID_EMPRESA," +
              "NOME_DESTINATARIO," +
              "CPF_CNPJ_DESTINATARIO," +
              "DATA_EMISSAO," +
              "HORA_EMISSAO," +
              "SITUACAO," +
              "SUBTOTAL," +
              "VALOR" +
              ") values (" +
              "?pNumeroDav," +
              "?pIdEmpresa," +
              "?pDestinatario," +
              "?pCPFCNPJ," +
              "?pDataEmissao," +
              "?pHoraEmissao," +
              "?pSituacao," +
              "?pSubTotal," +
              "?pValor)";
            try
            {
                comando = new MySqlCommand("select NUMERO_DAV from DAV_CABECALHO where id = (select max(id) from dav_cabecalho)", conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();
                NumeroUltimoDav = Convert.ToString(leitor["NUMERO_DAV"]);

                if ((NumeroUltimoDav == "") || (NumeroUltimoDav == "9999999999"))
                    NumeroNovoDav = "0000000001";
                else
                {
                    NumeroNovoDav = (Convert.ToInt32(NumeroUltimoDav) + 1).ToString();
                    NumeroNovoDav = new string('0', 10 - NumeroNovoDav.Length) + NumeroNovoDav;
                }

                leitor.Close();

                comando = new MySqlCommand(ConsultaSQL, conexao);
                comando.Parameters.AddWithValue("?pNumeroDav", NumeroNovoDav);
                comando.Parameters.AddWithValue("?pIdEmpresa", Constantes.EMPRESA_BALCAO);
                comando.Parameters.AddWithValue("?pDestinatario", NovoDAVCabecalho.NomeDestinatario);
                comando.Parameters.AddWithValue("?pCPFCNPJ", NovoDAVCabecalho.CpfCnpjDestinatario);
                comando.Parameters.AddWithValue("?pDataEmissao", NovoDAVCabecalho.DataEmissao);
                comando.Parameters.AddWithValue("?pHoraEmissao", NovoDAVCabecalho.HoraEmissao);
                comando.Parameters.AddWithValue("?pSituacao", NovoDAVCabecalho.Situacao);
                comando.Parameters.AddWithValue("?pSubTotal", ValorNovoDav);
                comando.Parameters.AddWithValue("?pValor", ValorNovoDav);
                comando.ExecuteNonQuery();

                comando = new MySqlCommand("select max(ID) as ID from DAV_CABECALHO", conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();
                NovoDAVCabecalho.Id = Convert.ToInt32(leitor["ID"]);
            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
            finally
            {
                if (leitor != null)
                    leitor.Close();
            }


            try
            {
                int Item = 1;
                for (int i = 0; i <= ListaDAVDetalhe.Count - 1; i++)
                {
                    // atualizar o hash dos detalhes mesclados
                    ConsultaSQL =
                      "update DAV_DETALHE set " +
                      "HASH_TRIPA = ?pHashTripa, " +
                      "HASH_INCREMENTO = ?pHashIncremento " +
                      " where ID = ?pId";

                    Tripa = Convert.ToString(ListaDAVDetalhe[i].Id) +
                              Convert.ToString(ListaDAVDetalhe[i].IdDavCabecalho) +
                              Convert.ToString(ListaDAVDetalhe[i].IdProduto) +
                              ListaDAVDetalhe[i].NumeroDav +
                              ListaDAVDetalhe[i].DataEmissao +
                              Convert.ToString(ListaDAVDetalhe[i].Item) +
                              Biblioteca.FormataFloat("V", ListaDAVDetalhe[i].Quantidade) +
                              Biblioteca.FormataFloat("V", ListaDAVDetalhe[i].ValorUnitario) +
                              Biblioteca.FormataFloat("V", ListaDAVDetalhe[i].ValorTotal) +
                              ListaDAVDetalhe[i].Cancelado +
                              ListaDAVDetalhe[i].MesclaProduto +
                              ListaDAVDetalhe[i].GtinProduto +
                              ListaDAVDetalhe[i].NomeProduto +
                              ListaDAVDetalhe[i].TotalizadorParcial +
                              ListaDAVDetalhe[i].UnidadeProduto + "0";
                    Hash = Biblioteca.MD5String(Tripa);

                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    comando.Parameters.AddWithValue("?pId", ListaDAVDetalhe[i].Id);
                    comando.Parameters.AddWithValue("?pHashTripa", Hash);
                    comando.Parameters.AddWithValue("?pHashIncremento", -1);
                    comando.ExecuteNonQuery();

                    // insere os novos detalhes
                    ConsultaSQL =
                      "insert into DAV_DETALHE (" +
                      "ID_PRODUTO," +
                      "ID_DAV_CABECALHO," +
                      "NUMERO_DAV," +
                      "DATA_EMISSAO," +
                      "ITEM," +
                      "QUANTIDADE," +
                      "VALOR_UNITARIO," +
                      "VALOR_TOTAL," +
                      "CANCELADO," +
                      "MESCLA_PRODUTO," +
                      "GTIN_PRODUTO," +
                      "NOME_PRODUTO," +
                      "TOTALIZADOR_PARCIAL," +
                      "HASH_TRIPA," +
                      "HASH_INCREMENTO," +
                      "UNIDADE_PRODUTO) values (" +
                      "?pIdProduto," +
                      "?pIdDavCabecalho," +
                      "?pNumeroDav," +
                      "?pDataEmissao," +
                      "?pItem," +
                      "?pQuantidade," +
                      "?pValorUnitario," +
                      "?pValorTotal," +
                      "?pCancelado," +
                      "?pMesclaProduto," +
                      "?pGtinProduto," +
                      "?pNomeProduto," +
                      "?pTOTALIZADOR_PARCIAL," +
                      "?pHashTripa," +
                      "?pHashIncremento," +
                      "?pUnidadeProduto)";

                    Tripa = Convert.ToString(ListaDAVDetalhe[i].Id) +
                              Convert.ToString(ListaDAVDetalhe[i].IdDavCabecalho) +
                              Convert.ToString(ListaDAVDetalhe[i].IdProduto) +
                              ListaDAVDetalhe[i].NumeroDav +
                              ListaDAVDetalhe[i].DataEmissao +
                              Convert.ToString(Item) +
                              Biblioteca.FormataFloat("V", ListaDAVDetalhe[i].Quantidade) +
                              Biblioteca.FormataFloat("V", ListaDAVDetalhe[i].ValorUnitario) +
                              Biblioteca.FormataFloat("V", ListaDAVDetalhe[i].ValorTotal) +
                              ListaDAVDetalhe[i].Cancelado +
                              ListaDAVDetalhe[i].MesclaProduto +
                              ListaDAVDetalhe[i].GtinProduto +
                              ListaDAVDetalhe[i].NomeProduto +
                              ListaDAVDetalhe[i].TotalizadorParcial +
                              ListaDAVDetalhe[i].UnidadeProduto + "0";
                    Hash = Biblioteca.MD5String(Tripa);


                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    comando.Parameters.AddWithValue("?pIdProduto", ListaDAVDetalhe[i].IdProduto);
                    comando.Parameters.AddWithValue("?pIdDavCabecalho", NovoDAVCabecalho.Id);
                    comando.Parameters.AddWithValue("?pNumeroDav", ListaDAVDetalhe[i].NumeroDav);
                    comando.Parameters.AddWithValue("?pDataEmissao", new DateTime()); //DateTime.Now.ToString("yyyy-mm-dd")
                    comando.Parameters.AddWithValue("?pItem", Item);
                    comando.Parameters.AddWithValue("?pQuantidade", ListaDAVDetalhe[i].Quantidade);
                    comando.Parameters.AddWithValue("?pValorUnitario", ListaDAVDetalhe[i].ValorUnitario);
                    comando.Parameters.AddWithValue("?pValorTotal", ListaDAVDetalhe[i].ValorTotal);
                    comando.Parameters.AddWithValue("?pCancelado", ListaDAVDetalhe[i].Cancelado);
                    comando.Parameters.AddWithValue("?pMesclaProduto", ListaDAVDetalhe[i].MesclaProduto);
                    comando.Parameters.AddWithValue("?pGtinProduto", ListaDAVDetalhe[i].GtinProduto);
                    comando.Parameters.AddWithValue("?pNomeProduto", ListaDAVDetalhe[i].NomeProduto);
                    comando.Parameters.AddWithValue("?pUnidadeProduto", ListaDAVDetalhe[i].UnidadeProduto);
                    comando.Parameters.AddWithValue("?pTOTALIZADOR_PARCIAL", ListaDAVDetalhe[i].TotalizadorParcial);
                    comando.Parameters.AddWithValue("?pHashIncremento", 0);
                    comando.Parameters.AddWithValue("?pHashTripa", Hash);
                    comando.ExecuteNonQuery();

                    Item++;
                }


            }
            catch (Exception eError)
            {
                Log.write(eError.ToString());
            }
            finally
            {
                if (leitor != null)
                    leitor.Close();
            }

            FCaixa.CarregaDAV(Convert.ToString(NovoDAVCabecalho.Id));
        }


        public List<DavCabecalhoVO> ListaDAVPeriodo(string pDataInicio, string pDataFim)
        {
            int TotalRegistros;

            ConsultaSQL =
              "select count(*) AS TOTAL from DAV_CABECALHO where SITUACAO =" + Biblioteca.QuotedStr("E") + " and (DATA_EMISSAO between '" + pDataInicio + "' and '" + pDataFim + "')";
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();
                TotalRegistros = Convert.ToInt32(leitor["TOTAL"]);
                leitor.Close();
                if (TotalRegistros > 0)
                {
                    List<DavCabecalhoVO> ListaDAV = new List<DavCabecalhoVO>();
                    ConsultaSQL =
                          "select * from DAV_CABECALHO where SITUACAO =" + Biblioteca.QuotedStr("E") + " and (DATA_EMISSAO between '" + pDataInicio + "' and '" + pDataFim + "')";

                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        DavCabecalhoVO DAVCabecalho = new DavCabecalhoVO();
                        DAVCabecalho.Id = Convert.ToInt32(leitor["ID"]);
                        DAVCabecalho.IdPessoa = Convert.ToInt32(leitor["ID_PESSOA"]);
                        DAVCabecalho.Ccf = Convert.ToInt32(leitor["CCF"]);
                        DAVCabecalho.Coo = Convert.ToInt32(leitor["COO"]);
                        DAVCabecalho.NumeroDav = Convert.ToString(leitor["NUMERO_DAV"]);
                        DAVCabecalho.NumeroEcf = Convert.ToString(leitor["NUMERO_ECF"]);
                        DAVCabecalho.NomeDestinatario = Convert.ToString(leitor["NOME_DESTINATARIO"]);
                        DAVCabecalho.CpfCnpjDestinatario = Convert.ToString(leitor["CPF_CNPJ_DESTINATARIO"]);
                        DAVCabecalho.DataEmissao = (DateTime)(leitor["DATA_EMISSAO"]);
                        DAVCabecalho.HoraEmissao = Convert.ToString(leitor["HORA_EMISSAO"]);
                        DAVCabecalho.Situacao = Convert.ToString(leitor["SITUACAO"]);
                        DAVCabecalho.TaxaAcrescimo = Convert.ToDecimal(leitor["TAXA_ACRESCIMO"]);
                        DAVCabecalho.Acrescimo = Convert.ToDecimal(leitor["ACRESCIMO"]);
                        DAVCabecalho.TaxaDesconto = Convert.ToDecimal(leitor["TAXA_DESCONTO"]);
                        DAVCabecalho.Desconto = Convert.ToDecimal(leitor["DESCONTO"]);
                        DAVCabecalho.Subtotal = Convert.ToDecimal(leitor["SUBTOTAL"]);
                        DAVCabecalho.Valor = Convert.ToDecimal(leitor["VALOR"]);
                        DAVCabecalho.HashTripa = Convert.ToString(leitor["HASH_TRIPA"]);
                        DAVCabecalho.HashIncremento = Convert.ToInt32(leitor["HASH_INCREMENTO"]);
                        ListaDAV.Add(DAVCabecalho);
                    }
                    return ListaDAV;
                }
                //  caso não exista a relacao, retorna um ponteiro nulo
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


        public DavCabecalhoVO ConsultaDAVId(int? pId)
        {

            ConsultaSQL =
              "select * from DAV_CABECALHO where ID = " + Convert.ToString(pId);

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();

                DavCabecalhoVO DAVCabecalho = new DavCabecalhoVO();

                DAVCabecalho.Id = Convert.ToInt32(leitor["ID"]);
                DAVCabecalho.IdPessoa = Convert.ToInt32(leitor["ID_PESSOA"]);
                DAVCabecalho.Ccf = Convert.ToInt32(leitor["CCF"]);
                DAVCabecalho.Coo = Convert.ToInt32(leitor["COO"]);
                DAVCabecalho.NumeroDav = Convert.ToString(leitor["NUMERO_DAV"]);
                DAVCabecalho.NumeroEcf = Convert.ToString(leitor["NUMERO_ECF"]);
                DAVCabecalho.NomeDestinatario = Convert.ToString(leitor["NOME_DESTINATARIO"]);
                DAVCabecalho.CpfCnpjDestinatario = Convert.ToString(leitor["CPF_CNPJ_DESTINATARIO"]);
                DAVCabecalho.DataEmissao = (DateTime)(leitor["DATA_EMISSAO"]);
                DAVCabecalho.HoraEmissao = Convert.ToString(leitor["HORA_EMISSAO"]);
                DAVCabecalho.Situacao = Convert.ToString(leitor["SITUACAO"]);
                DAVCabecalho.TaxaAcrescimo = Convert.ToDecimal(leitor["TAXA_ACRESCIMO"]);
                DAVCabecalho.Acrescimo = Convert.ToDecimal(leitor["ACRESCIMO"]);
                DAVCabecalho.TaxaDesconto = Convert.ToDecimal(leitor["TAXA_DESCONTO"]);
                DAVCabecalho.Desconto = Convert.ToDecimal(leitor["DESCONTO"]);
                DAVCabecalho.Subtotal = Convert.ToDecimal(leitor["SUBTOTAL"]);
                DAVCabecalho.Valor = Convert.ToDecimal(leitor["VALOR"]);
                DAVCabecalho.HashTripa = Convert.ToString(leitor["HASH_TRIPA"]);
                DAVCabecalho.HashIncremento = Convert.ToInt32(leitor["HASH_INCREMENTO"]);

                return DAVCabecalho;
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


        public List<DavDetalheVO> ListaDavDetalhe(int pId)
        {
            // verifica se existem itens para o ID passado
            ConsultaSQL =
              "select count(*) AS TOTAL from DAV_DETALHE where ID_DAV_CABECALHO = " + Convert.ToString(pId);
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                leitor.Read();
                int TotalRegistros = Convert.ToInt32(leitor["TOTAL"]);
                leitor.Close();
                if (TotalRegistros > 0)
                {
                    List<DavDetalheVO> ListaDavDetalhe = new List<DavDetalheVO>();
                    ConsultaSQL =
                      "select * " +
                      " from DAV_DETALHE " +
                      " where ID_DAV_CABECALHO = " + Convert.ToString(pId);

                    comando = new MySqlCommand(ConsultaSQL, conexao);
                    leitor = comando.ExecuteReader();
                    while (leitor.Read())
                    {
                        DavDetalheVO DavDetalhe = new DavDetalheVO();
                        DavDetalhe.Id = Convert.ToInt32(leitor["ID"]);
                        DavDetalhe.IdDavCabecalho = Convert.ToInt32(leitor["ID_DAV_CABECALHO"]);
                        DavDetalhe.IdProduto = Convert.ToInt32(leitor["ID_PRODUTO"]);
                        DavDetalhe.GtinProduto = Convert.ToString(leitor["GTIN_PRODUTO"]);
                        DavDetalhe.NomeProduto = Convert.ToString(leitor["NOME_PRODUTO"]);
                        DavDetalhe.UnidadeProduto = Convert.ToString(leitor["UNIDADE_PRODUTO"]);
                        DavDetalhe.IdProduto = Convert.ToInt32(leitor["ID_PRODUTO"]);
                        DavDetalhe.IdProduto = Convert.ToInt32(leitor["ID_PRODUTO"]);
                        DavDetalhe.Item = Convert.ToInt32(leitor["ITEM"]);
                        DavDetalhe.Quantidade = Convert.ToDecimal(leitor["QUANTIDADE"]);
                        DavDetalhe.ValorUnitario = Convert.ToDecimal(leitor["VALOR_UNITARIO"]);
                        DavDetalhe.ValorTotal = Convert.ToDecimal(leitor["VALOR_TOTAL"]);
                        DavDetalhe.Cancelado = Convert.ToString(leitor["CANCELADO"]);
                        DavDetalhe.MesclaProduto = Convert.ToString(leitor["MESCLA_PRODUTO"]);
                        DavDetalhe.TotalizadorParcial = Convert.ToString(leitor["TOTALIZADOR_PARCIAL"]);
                        DavDetalhe.HashTripa = Convert.ToString(leitor["HASH_TRIPA"]);
                        DavDetalhe.HashIncremento = Convert.ToInt32(leitor["HASH_INCREMENTO"]);
                        ListaDavDetalhe.Add(DavDetalhe);
                    }
                    return ListaDavDetalhe;
                }
                // caso não exista a relacao, retorna um ponteiro nulo
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


        public List<DavCabecalhoVO> TabelaDavCabecalho()
        {
            List<DavCabecalhoVO> ListaDavCabecalho = new List<DavCabecalhoVO>();

            ConsultaSQL = " SELECT * FROM DAV_CABECALHO WHERE SITUACAO = 'P' ORDER BY ID";

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    DavCabecalhoVO DavCabecalho = new DavCabecalhoVO();

                    DavCabecalho.Id = Convert.ToInt32(leitor["ID"]);
                    DavCabecalho.NumeroDav = leitor["NUMERO_DAV"].ToString();
                    DavCabecalho.NomeDestinatario = leitor["NOME_DESTINATARIO"].ToString();
                    DavCabecalho.CpfCnpjDestinatario = leitor["CPF_CNPJ_DESTINATARIO"].ToString();
                    DavCabecalho.DataEmissao = Convert.ToDateTime(leitor["DATA_EMISSAO"]);
                    DavCabecalho.HoraEmissao = leitor["HORA_EMISSAO"].ToString();
                    DavCabecalho.Valor = Convert.IsDBNull(leitor["VALOR"]) ? 0 : Convert.ToDecimal(leitor["VALOR"]);

                    ListaDavCabecalho.Add(DavCabecalho);
                }
                return ListaDavCabecalho;
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


        public List<DavDetalheVO> TabelaDavDetalhe(string pIdCabecalho)
        {
            List<DavDetalheVO> ListaDavDetalhe = new List<DavDetalheVO>();

            ConsultaSQL = " SELECT * FROM DAV_DETALHE WHERE ID_DAV_CABECALHO = " + pIdCabecalho;

            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    DavDetalheVO DavDetalhe = new DavDetalheVO();

                    DavDetalhe.Id = Convert.ToInt32(leitor["ID"]);
                    DavDetalhe.NomeProduto = leitor["NOME_PRODUTO"].ToString();
                    DavDetalhe.Quantidade = Convert.ToDecimal(leitor["QUANTIDADE"]);
                    DavDetalhe.ValorUnitario = Convert.ToDecimal(leitor["VALOR_UNITARIO"]);
                    DavDetalhe.ValorTotal = Convert.ToDecimal(leitor["VALOR_TOTAL"]);

                    ListaDavDetalhe.Add(DavDetalhe);
                }
                return ListaDavDetalhe;
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

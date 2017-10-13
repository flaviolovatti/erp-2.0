/********************************************************************************
Title: T2TiPDV
Description: Classe de controle do Turno

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

    public class TurnoController
    {
        private MySqlConnection conexao;
        private MySqlCommand comando;
        private MySqlDataReader leitor;
        public string ConsultaSQL;

        public TurnoController()
        {
            conexao = dbConnection.conectar();
        }


        public List<TurnoVO> TabelaTurno()
        {
            ConsultaSQL = "select * from ECF_TURNO";
            try
            {
                comando = new MySqlCommand(ConsultaSQL, conexao);
                leitor = comando.ExecuteReader();

                List<TurnoVO> ListaTurno = new List<TurnoVO>();

                while (leitor.Read())
                {
                    TurnoVO Turno = new TurnoVO();

                    Turno.Id = Convert.ToInt32(leitor["ID"]);
                    Turno.Descricao = Convert.ToString(leitor["DESCRICAO"]);
                    Turno.HoraInicio = Convert.ToString(leitor["HORA_INICIO"]);
                    Turno.HoraFim = Convert.ToString(leitor["HORA_FIM"]);

                    ListaTurno.Add(Turno);
                }
                return ListaTurno;
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

        public bool ConsultaIdTurno(int pId)
        {
            ConsultaSQL = "select ID from ECF_TURNO where id = " + Convert.ToString(pId);
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


        public bool GravaCargaTurno(string pTupla)
        {
            String[] tupla = pTupla.Split('|');
            int id;
            try
            {
                id = Convert.ToInt32(tupla[1]);   //ID  INTEGER NOT NULL,


                if (!ConsultaIdTurno(id))
                {
                    ConsultaSQL =
                            "insert into"
                            + " ECF_TURNO "
                            + " (ID, "
                            + " DESCRICAO, "
                            + " HORA_INICIO, "
                            + " HORA_FIM) "
                            + " values ("
                            + id + ", " //    ID      INTEGER NOT NULL,
                            + tupla[2] + ", " //    DESCRICAO    VARCHAR(10),
                            + tupla[3] + ", " //    HORA_INICIO  VARCHAR(8),
                            + tupla[4] + ")";   //    HORA_FIM     VARCHAR(8)
                }
                else
                {
                    ConsultaSQL =
                            "update "
                            + " ECF_TURNO "
                            + "set "
                            + " DESCRICAO = " + tupla[2] + ", " //    DESCRICAO    VARCHAR(10),
                            + " HORA_INICIO =" + tupla[3] + ", " //    HORA_INICIO  VARCHAR(8),
                            + " HORA_FIM =" + tupla[4] //    HORA_FIM     VARCHAR(8)
                            + " where "
                            + " ID =" + id;
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
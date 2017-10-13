/********************************************************************************
Title: T2TiPDV
Description: VO relacionado à tabela ECF_FUNCIONARIO

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

namespace PafEcf.VO
{


    public class FuncionarioVO
    {


        private int FID;
        private string FNOME;
        private string FTELEFONE;
        private string FCELULAR;
        private string FEMAIL;
        private decimal FCOMISSAO_VISTA;
        private decimal FCOMISSAO_PRAZO;
        private string FNIVEL_AUTORIZACAO;



        public int Id
        {
            get
            {
                return FID;
            }
            set
            {
                FID = value;
            }
        }


        public String Nome
        {
            get
            {
                return FNOME;
            }
            set
            {
                FNOME = value;
            }
        }


        public String Telefone
        {
            get
            {
                return FTELEFONE;
            }
            set
            {
                FTELEFONE = value;
            }
        }


        public String Celular
        {
            get
            {
                return FCELULAR;
            }
            set
            {
                FCELULAR = value;
            }
        }


        public String Email
        {
            get
            {
                return FEMAIL;
            }
            set
            {
                FEMAIL = value;
            }
        }


        public decimal ComissaoVista
        {
            get
            {
                return FCOMISSAO_VISTA;
            }
            set
            {
                FCOMISSAO_VISTA = value;
            }
        }


        public decimal ComissaoPrazo
        {
            get
            {
                return FCOMISSAO_PRAZO;
            }
            set
            {
                FCOMISSAO_PRAZO = value;
            }
        }


        public String NivelAutorizacao
        {
            get
            {
                return FNIVEL_AUTORIZACAO;
            }
            set
            {
                FNIVEL_AUTORIZACAO = value;
            }
        }


    }


}
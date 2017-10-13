/********************************************************************************
Title: T2TiPDV
Description: VO relacionado à tabela ECF_TIPO_PAGAMENTO

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


    public class TipoPagamentoVO
    {


        private int FID;
        private string FCODIGO;
        private string FDESCRICAO;
        private string FTEF;
        private string FIMPRIME_VINCULADO;
        private string FPERMITE_TROCO;
        private string FTEF_TIPO_GP;
        private string FGERA_PARCELAS;



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


        public String Codigo
        {
            get
            {
                return FCODIGO;
            }
            set
            {
                FCODIGO = value;
            }
        }


        public String Descricao
        {
            get
            {
                return FDESCRICAO;
            }
            set
            {
                FDESCRICAO = value;
            }
        }


        public String TEF
        {
            get
            {
                return FTEF;
            }
            set
            {
                FTEF = value;
            }
        }


        public String ImprimeVinculado
        {
            get
            {
                return FIMPRIME_VINCULADO;
            }
            set
            {
                FIMPRIME_VINCULADO = value;
            }
        }


        public String PermiteTroco
        {
            get
            {
                return FPERMITE_TROCO;
            }
            set
            {
                FPERMITE_TROCO = value;
            }
        }


        public String TipoGP
        {
            get
            {
                return FTEF_TIPO_GP;
            }
            set
            {
                FTEF_TIPO_GP = value;
            }
        }


        public String GeraParcelas
        {
            get
            {
                return FGERA_PARCELAS;
            }
            set
            {
                FGERA_PARCELAS = value;
            }
        }


    }


}
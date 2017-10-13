/********************************************************************************
Title: T2TiPDV
Description: VO relacionado à tabela R02

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


    public class R02VO
    {


        private int FID;
        private int FID_OPERADOR;
        private int FID_IMPRESSORA;
        private int FID_ECF_CAIXA;
        private string FSERIE_ECF;
        private int FCRZ;
        private int FCOO;
        private int FCRO;
        private DateTime FDATA_MOVIMENTO;
        private DateTime FDATA_EMISSAO;
        private string FHORA_EMISSAO;
        private decimal FVENDA_BRUTA;
        private decimal FGRANDE_TOTAL;
        private string FSINCRONIZADO;
        private string FHASH_TRIPA;
        private int FHASH_INCREMENTO;



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


        public int IdOperador
        {
            get
            {
                return FID_OPERADOR;
            }
            set
            {
                FID_OPERADOR = value;
            }
        }


        public int IdImpressora
        {
            get
            {
                return FID_IMPRESSORA;
            }
            set
            {
                FID_IMPRESSORA = value;
            }
        }


        public int IdCaixa
        {
            get
            {
                return FID_ECF_CAIXA;
            }
            set
            {
                FID_ECF_CAIXA = value;
            }
        }


        public String SerieEcf
        {
            get
            {
                return FSERIE_ECF;
            }
            set
            {
                FSERIE_ECF = value;
            }
        }


        public int CRZ
        {
            get
            {
                return FCRZ;
            }
            set
            {
                FCRZ = value;
            }
        }


        public int COO
        {
            get
            {
                return FCOO;
            }
            set
            {
                FCOO = value;
            }
        }


        public int CRO
        {
            get
            {
                return FCRO;
            }
            set
            {
                FCRO = value;
            }
        }


        public DateTime DataMovimento
        {
            get
            {
                return FDATA_MOVIMENTO;
            }
            set
            {
                FDATA_MOVIMENTO = value;
            }
        }


        public DateTime DataEmissao
        {
            get
            {
                return FDATA_EMISSAO;
            }
            set
            {
                FDATA_EMISSAO = value;
            }
        }


        public String HoraEmissao
        {
            get
            {
                return FHORA_EMISSAO;
            }
            set
            {
                FHORA_EMISSAO = value;
            }
        }


        public decimal VendaBruta
        {
            get
            {
                return FVENDA_BRUTA;
            }
            set
            {
                FVENDA_BRUTA = value;
            }
        }


        public decimal GrandeTotal
        {
            get
            {
                return FGRANDE_TOTAL;
            }
            set
            {
                FGRANDE_TOTAL = value;
            }
        }


        public String Sincronizado
        {
            get
            {
                return FSINCRONIZADO;
            }
            set
            {
                FSINCRONIZADO = value;
            }
        }


        public String HashTripa
        {
            get
            {
                return FHASH_TRIPA;
            }
            set
            {
                FHASH_TRIPA = value;
            }
        }


        public int HashIncremento
        {
            get
            {
                return FHASH_INCREMENTO;
            }
            set
            {
                FHASH_INCREMENTO = value;
            }
        }


    }


}
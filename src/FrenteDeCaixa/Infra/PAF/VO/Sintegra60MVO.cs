/********************************************************************************
Title: T2TiPDV
Description: VO relacionado à tabela SINTEGRA_60M

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


    public class Sintegra60MVO
    {


        private int FID;
        private DateTime FDATA_EMISSAO;
        private string FNUMERO_SERIE_ECF;
        private int FNUMERO_EQUIPAMENTO;
        private string FMODELO_DOCUMENTO_FISCAL;
        private int FCOO_INICIAL;
        private int FCOO_FINAL;
        private int FCRZ;
        private int FCRO;
        private decimal FVALOR_VENDA_BRUTA;
        private decimal FVALOR_GRANDE_TOTAL;



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


        public String SerieImpressora
        {
            get
            {
                return FNUMERO_SERIE_ECF;
            }
            set
            {
                FNUMERO_SERIE_ECF = value;
            }
        }


        public int NumeroEquipamento
        {
            get
            {
                return FNUMERO_EQUIPAMENTO;
            }
            set
            {
                FNUMERO_EQUIPAMENTO = value;
            }
        }


        public String ModeloDocumentoFiscal
        {
            get
            {
                return FMODELO_DOCUMENTO_FISCAL;
            }
            set
            {
                FMODELO_DOCUMENTO_FISCAL = value;
            }
        }


        public int COOInicial
        {
            get
            {
                return FCOO_INICIAL;
            }
            set
            {
                FCOO_INICIAL = value;
            }
        }


        public int COOFinal
        {
            get
            {
                return FCOO_FINAL;
            }
            set
            {
                FCOO_FINAL = value;
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


        public decimal VendaBruta
        {
            get
            {
                return FVALOR_VENDA_BRUTA;
            }
            set
            {
                FVALOR_VENDA_BRUTA = value;
            }
        }


        public decimal GrandeTotal
        {
            get
            {
                return FVALOR_GRANDE_TOTAL;
            }
            set
            {
                FVALOR_GRANDE_TOTAL = value;
            }
        }


    }

}

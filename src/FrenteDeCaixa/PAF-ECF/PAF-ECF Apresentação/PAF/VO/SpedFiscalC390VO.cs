/********************************************************************************
Title: T2TiPDV
Description: VO transiente que representa o registro 390 do Sped Fiscal.

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


    public class SpedFiscalC390VO
    {


        private string FCST;
        private int FCFOP;
        private decimal FTaxaICMS;
        private decimal FSomaValor;
        private decimal FSomaBaseICMS;
        private decimal FSomaICMS;
        private decimal FSomaICMSOutras;



        public String CST
        {
            get
            {
                return FCST;
            }
            set
            {
                FCST = value;
            }
        }


        public int CFOP
        {
            get
            {
                return FCFOP;
            }
            set
            {
                FCFOP = value;
            }
        }


        public decimal TaxaICMS
        {
            get
            {
                return FTaxaICMS;
            }
            set
            {
                FTaxaICMS = value;
            }
        }


        public decimal SomaValor
        {
            get
            {
                return FSomaValor;
            }
            set
            {
                FSomaValor = value;
            }
        }


        public decimal SomaBaseICMS
        {
            get
            {
                return FSomaBaseICMS;
            }
            set
            {
                FSomaBaseICMS = value;
            }
        }


        public decimal SomaICMS
        {
            get
            {
                return FSomaICMS;
            }
            set
            {
                FSomaICMS = value;
            }
        }


        public decimal SomaICMSOutras
        {
            get
            {
                return FSomaICMSOutras;
            }
            set
            {
                FSomaICMSOutras = value;
            }
        }


    }

}

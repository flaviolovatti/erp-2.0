/********************************************************************************
Title: T2TiPDV
Description: VO relacionado à tabela ECF_ALIQUOTAS

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


    public class AliquotasVO
    {


        private int FID;
        private string FTOTALIZADOR_PARCIAL;
        private string FECF_ICMS_ST;
        private string FPAF_P_ST;


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


        public String TotalizadorParcial
        {
            get
            {
                return FTOTALIZADOR_PARCIAL;
            }
            set
            {
                FTOTALIZADOR_PARCIAL = value;
            }
        }


        public String EcfIcmsSt
        {
            get
            {
                return FECF_ICMS_ST;
            }
            set
            {
                FECF_ICMS_ST = value;
            }
        }


        public String PafPSt
        {
            get
            {
                return FPAF_P_ST;
            }
            set
            {
                FPAF_P_ST = value;
            }
        }


    }

}

/********************************************************************************
Title: T2TiPDV
Description: VO relacionado à tabela R03

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


    public class R03VO
    {


        private int FID;
        private int FID_R02;
        private string FSERIE_ECF;
        private string FTOTALIZADOR_PARCIAL;
        private decimal FVALOR_ACUMULADO;
        private int FCRZ;
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


        public int IdR02
        {
            get
            {
                return FID_R02;
            }
            set
            {
                FID_R02 = value;
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


        public decimal ValorAcumulado
        {
            get
            {
                return FVALOR_ACUMULADO;
            }
            set
            {
                FVALOR_ACUMULADO = value;
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
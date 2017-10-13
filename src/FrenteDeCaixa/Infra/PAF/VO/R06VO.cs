/********************************************************************************
Title: T2TiPDV
Description: VO relacionado à tabela R06

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


    public class R06VO
    {


        private int FID;
        private int FID_OPERADOR;
        private int FID_IMPRESSORA;
        private int FID_ECF_CAIXA;
        private string FSERIE_ECF;
        private int FCOO;
        private int FGNF;
        private int FGRG;
        private int FCDC;
        private string FDENOMINACAO;
        private DateTime FDATA_EMISSAO;
        private string FHORA_EMISSAO;
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


        public int GNF
        {
            get
            {
                return FGNF;
            }
            set
            {
                FGNF = value;
            }
        }


        public int GRG
        {
            get
            {
                return FGRG;
            }
            set
            {
                FGRG = value;
            }
        }


        public int CDC
        {
            get
            {
                return FCDC;
            }
            set
            {
                FCDC = value;
            }
        }


        public String Denominacao
        {
            get
            {
                return FDENOMINACAO;
            }
            set
            {
                FDENOMINACAO = value;
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
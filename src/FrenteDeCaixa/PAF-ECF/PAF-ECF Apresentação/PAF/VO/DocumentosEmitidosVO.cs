/********************************************************************************
Title: T2TiPDV
Description: VO relacionado à tabela ECF_DOCUMENTOS_EMITIDOS

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


    public class DocumentosEmitidosVO
    {


        private int FID;
        private int ID_ECF_MOVIMENTO;
        private DateTime FDATA_EMISSAO;
        private string FHORA_EMISSAO;
        private string FTIPO;
        private int FCOO;
        private string FSINCRONIZADO;



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


        public int IdMovimento
        {
            get
            {
                return ID_ECF_MOVIMENTO;
            }
            set
            {
                ID_ECF_MOVIMENTO = value;
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


        public String Tipo
        {
            get
            {
                return FTIPO;
            }
            set
            {
                FTIPO = value;
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


    }


}
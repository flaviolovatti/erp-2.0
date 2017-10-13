/********************************************************************************
Title: T2TiPDV
Description: VO relacionado à tabela CONTADOR

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


    public class ContadorVO
    {


        private int FID;
        private int ID_ECF_EMPRESA;
        private string FCPF;
        private string FCNPJ;
        private string FNOME;
        private string FINSCRICAO_CRC;
        private string FFONE;
        private string FFAX;
        private string FLOGRADOURO;
        private int FNUMERO;
        private string FCOMPLEMENTO;
        private string FBAIRRO;
        private string FCEP;
        private int FCODIGO_MUNICIPIO;
        private string FUF;
        private string FEMAIL;



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


        public int IdEmpresa
        {
            get
            {
                return ID_ECF_EMPRESA;
            }
            set
            {
                ID_ECF_EMPRESA = value;
            }
        }


        public String CPF
        {
            get
            {
                return FCPF;
            }
            set
            {
                FCPF = value;
            }
        }


        public String CNPJ
        {
            get
            {
                return FCNPJ;
            }
            set
            {
                FCNPJ = value;
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


        public String CRC
        {
            get
            {
                return FINSCRICAO_CRC;
            }
            set
            {
                FINSCRICAO_CRC = value;
            }
        }


        public String Fone
        {
            get
            {
                return FFONE;
            }
            set
            {
                FFONE = value;
            }
        }


        public String Fax
        {
            get
            {
                return FFAX;
            }
            set
            {
                FFAX = value;
            }
        }


        public String Logradouro
        {
            get
            {
                return FLOGRADOURO;
            }
            set
            {
                FLOGRADOURO = value;
            }
        }


        public int Numero
        {
            get
            {
                return FNUMERO;
            }
            set
            {
                FNUMERO = value;
            }
        }


        public String Complemento
        {
            get
            {
                return FCOMPLEMENTO;
            }
            set
            {
                FCOMPLEMENTO = value;
            }
        }


        public String Bairro
        {
            get
            {
                return FBAIRRO;
            }
            set
            {
                FBAIRRO = value;
            }
        }


        public String CEP
        {
            get
            {
                return FCEP;
            }
            set
            {
                FCEP = value;
            }
        }


        public int CodigoMunicipio
        {
            get
            {
                return FCODIGO_MUNICIPIO;
            }
            set
            {
                FCODIGO_MUNICIPIO = value;
            }
        }


        public String UF
        {
            get
            {
                return FUF;
            }
            set
            {
                FUF = value;
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


    }
}

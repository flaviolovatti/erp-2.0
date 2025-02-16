/********************************************************************************
Title: T2TiPDV
Description: VO relacionado � tabela PRE_VENDA_CABECALHO

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


    public class PreVendaCabecalhoVO
    {


        private int FID;
        private int FID_PESSOA;
        private int FID_EMPRESA;
        private DateTime FDATA_PV;
        private string FHORA_PV;
        private string FSITUACAO;
        private int FCCF;
        private decimal FVALOR;
        private string FNOME_DESTINATARIO;
        private string FCPF_CNPJ_DESTINATARIO;
        private decimal FSUBTOTAL;
        private decimal FDESCONTO;
        private decimal FACRESCIMO;
        private decimal FTAXA_ACRESCIMO;
        private decimal FTAXA_DESCONTO;



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


        public int IdPessoa
        {
            get
            {
                return FID_PESSOA;
            }
            set
            {
                FID_PESSOA = value;
            }
        }


        public int IdEmpresa
        {
            get
            {
                return FID_EMPRESA;
            }
            set
            {
                FID_EMPRESA = value;
            }
        }


        public DateTime DataEmissao
        {
            get
            {
                return FDATA_PV;
            }
            set
            {
                FDATA_PV = value;
            }
        }


        public String HoraEmissao
        {
            get
            {
                return FHORA_PV;
            }
            set
            {
                FHORA_PV = value;
            }
        }


        public String Situacao
        {
            get
            {
                return FSITUACAO;
            }
            set
            {
                FSITUACAO = value;
            }
        }


        public int CCF
        {
            get
            {
                return FCCF;
            }
            set
            {
                FCCF = value;
            }
        }


        public decimal Valor
        {
            get
            {
                return FVALOR;
            }
            set
            {
                FVALOR = value;
            }
        }


        public String NomeDestinatario
        {
            get
            {
                return FNOME_DESTINATARIO;
            }
            set
            {
                FNOME_DESTINATARIO = value;
            }
        }


        public String CpfCnpjDestinatario
        {
            get
            {
                return FCPF_CNPJ_DESTINATARIO;
            }
            set
            {
                FCPF_CNPJ_DESTINATARIO = value;
            }
        }


        public decimal SubTotal
        {
            get
            {
                return FSUBTOTAL;
            }
            set
            {
                FSUBTOTAL = value;
            }
        }


        public decimal Desconto
        {
            get
            {
                return FDESCONTO;
            }
            set
            {
                FDESCONTO = value;
            }
        }


        public decimal Acrescimo
        {
            get
            {
                return FACRESCIMO;
            }
            set
            {
                FACRESCIMO = value;
            }
        }


        public decimal TaxaAcrescimo
        {
            get
            {
                return FTAXA_ACRESCIMO;
            }
            set
            {
                FTAXA_ACRESCIMO = value;
            }
        }


        public decimal TaxaDesconto
        {
            get
            {
                return FTAXA_DESCONTO;
            }
            set
            {
                FTAXA_DESCONTO = value;
            }
        }


    }

}
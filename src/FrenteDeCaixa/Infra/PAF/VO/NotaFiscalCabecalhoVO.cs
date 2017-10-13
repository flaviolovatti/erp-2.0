/********************************************************************************
Title: T2TiPDV
Description: VO relacionado à tabela NOTA_FISCAL_CABECALHO

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



    public class NotaFiscalCabecalhoVO
    {


        private int FID;
        private int FID_ECF_FUNCIONARIO;
        private int FID_CLIENTE;
        private int FCFOP;
        private string FNUMERO;
        private DateTime FDATA_EMISSAO;
        private string FHORA_EMISSAO;
        private string FSERIE;
        private string FSUBSERIE;
        private decimal FTOTAL_PRODUTOS;
        private decimal FTOTAL_NF;
        private decimal FBASE_ICMS;
        private decimal FICMS;
        private decimal FICMS_OUTRAS;
        private decimal FISSQN;
        private decimal FPIS;
        private decimal FCOFINS;
        private decimal FIPI;
        private decimal FTAXA_ACRESCIMO;
        private decimal FACRESCIMO;
        private decimal FACRESCIMO_ITENS;
        private decimal FTAXA_DESCONTO;
        private decimal FDESCONTO;
        private decimal FDESCONTO_ITENS;
        private string FCANCELADA;
        private string FSINCRONIZADO;
        private string FTIPO_NOTA;
        private string FCpfCnpjCliente;
        private int FNumOrdemInicial;
        private int FNumOrdemFinal;


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


        public int IdEcfFuncionario
        {
            get
            {
                return FID_ECF_FUNCIONARIO;
            }
            set
            {
                FID_ECF_FUNCIONARIO = value;
            }
        }


        public int IdCliente
        {
            get
            {
                return FID_CLIENTE;
            }
            set
            {
                FID_CLIENTE = value;
            }
        }


        public int Cfop
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


        public String Numero
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


        public String Serie
        {
            get
            {
                return FSERIE;
            }
            set
            {
                FSERIE = value;
            }
        }


        public String Subserie
        {
            get
            {
                return FSUBSERIE;
            }
            set
            {
                FSUBSERIE = value;
            }
        }


        public Decimal TotalProdutos
        {
            get
            {
                return FTOTAL_PRODUTOS;
            }
            set
            {
                FTOTAL_PRODUTOS = value;
            }
        }


        public Decimal TotalNf
        {
            get
            {
                return FTOTAL_NF;
            }
            set
            {
                FTOTAL_NF = value;
            }
        }


        public Decimal BaseIcms
        {
            get
            {
                return FBASE_ICMS;
            }
            set
            {
                FBASE_ICMS = value;
            }
        }


        public Decimal Icms
        {
            get
            {
                return FICMS;
            }
            set
            {
                FICMS = value;
            }
        }


        public Decimal IcmsOutras
        {
            get
            {
                return FICMS_OUTRAS;
            }
            set
            {
                FICMS_OUTRAS = value;
            }
        }


        public Decimal Issqn
        {
            get
            {
                return FISSQN;
            }
            set
            {
                FISSQN = value;
            }
        }


        public Decimal Pis
        {
            get
            {
                return FPIS;
            }
            set
            {
                FPIS = value;
            }
        }


        public Decimal Cofins
        {
            get
            {
                return FCOFINS;
            }
            set
            {
                FCOFINS = value;
            }
        }


        public Decimal Ipi
        {
            get
            {
                return FIPI;
            }
            set
            {
                FIPI = value;
            }
        }


        public Decimal TaxaAcrescimo
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


        public Decimal Acrescimo
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


        public Decimal AcrescimoItens
        {
            get
            {
                return FACRESCIMO_ITENS;
            }
            set
            {
                FACRESCIMO_ITENS = value;
            }
        }


        public Decimal TaxaDesconto
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


        public Decimal Desconto
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


        public Decimal DescontoItens
        {
            get
            {
                return FDESCONTO_ITENS;
            }
            set
            {
                FDESCONTO_ITENS = value;
            }
        }


        public String Cancelada
        {
            get
            {
                return FCANCELADA;
            }
            set
            {
                FCANCELADA = value;
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


        public String TipoNota
        {
            get
            {
                return FTIPO_NOTA;
            }
            set
            {
                FTIPO_NOTA = value;
            }
        }


        public String CpfCnpjCliente
        {
            get
            {
                return FCpfCnpjCliente;
            }
            set
            {
                FCpfCnpjCliente = value;
            }
        }


        public int NumOrdemFinal
        {
            get
            {
                return FNumOrdemFinal;
            }
            set
            {
                FNumOrdemFinal = value;
            }
        }


        public int NumOrdemInicial
        {
            get
            {
                return FNumOrdemInicial;
            }
            set
            {
                FNumOrdemInicial = value;
            }
        }


    }


}


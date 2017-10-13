/********************************************************************************
Title: T2TiPDV
Description: VO relacionado à tabela EMPRESA

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


    public class EmpresaVO
    {


        private int FID;
        private int FID_EMPRESA;
        private string FRAZAO_SOCIAL;
        private string FNOME_FANTASIA;
        private string FCNPJ;
        private string FINSCRICAO_ESTADUAL;
        private string FINSCRICAO_ESTADUAL_ST;
        private string FINSCRICAO_MUNICIPAL;
        private string FINSCRICAO_JUNTA_COMERCIAL;
        private DateTime FDATA_INSC_JUNTA_COMERCIAL;
        private string FMATRIZ_FILIAL;
        private DateTime FDATA_CADASTRO;
        private DateTime FDATA_INICIO_ATIVIDADES;
        private string FSUFRAMA;
        private string FEMAIL;
        private string FIMAGEM_LOGOTIPO;
        private string FCRT;
        private string FTIPO_REGIME;
        private decimal FALIQUOTA_PIS;
        private decimal FALIQUOTA_COFINS;
        private string FLOGRADOURO;
        private string FNUMERO;
        private string FCOMPLEMENTO;
        private string FCEP;
        private string FBAIRRO;
        private string FCIDADE;
        private string FUF;
        private string FFONE;
        private string FFAX;
        private string FCONTATO;
        private int FCODIGO_IBGE_CIDADE;
        private int FCODIGO_IBGE_UF;


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
                return FID_EMPRESA;
            }
            set
            {
                FID_EMPRESA = value;
            }
        }


        public String RazaoSocial
        {
            get
            {
                return FRAZAO_SOCIAL;
            }
            set
            {
                FRAZAO_SOCIAL = value;
            }
        }


        public String NomeFantasia
        {
            get
            {
                return FNOME_FANTASIA;
            }
            set
            {
                FNOME_FANTASIA = value;
            }
        }


        public String Cnpj
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


        public String InscricaoEstadual
        {
            get
            {
                return FINSCRICAO_ESTADUAL;
            }
            set
            {
                FINSCRICAO_ESTADUAL = value;
            }
        }


        public String InscricaoEstadualSt
        {
            get
            {
                return FINSCRICAO_ESTADUAL_ST;
            }
            set
            {
                FINSCRICAO_ESTADUAL_ST = value;
            }
        }


        public String InscricaoMunicipal
        {
            get
            {
                return FINSCRICAO_MUNICIPAL;
            }
            set
            {
                FINSCRICAO_MUNICIPAL = value;
            }
        }


        public String InscricaoJuntaComercial
        {
            get
            {
                return FINSCRICAO_JUNTA_COMERCIAL;
            }
            set
            {
                FINSCRICAO_JUNTA_COMERCIAL = value;
            }
        }


        public DateTime DataInscJuntaComercial
        {
            get
            {
                return FDATA_INSC_JUNTA_COMERCIAL;
            }
            set
            {
                FDATA_INSC_JUNTA_COMERCIAL = value;
            }
        }


        public String MatrizFilial
        {
            get
            {
                return FMATRIZ_FILIAL;
            }
            set
            {
                FMATRIZ_FILIAL = value;
            }
        }


        public DateTime DataCadastro
        {
            get
            {
                return FDATA_CADASTRO;
            }
            set
            {
                FDATA_CADASTRO = value;
            }
        }


        public DateTime DataInicioAtividades
        {
            get
            {
                return FDATA_INICIO_ATIVIDADES;
            }
            set
            {
                FDATA_INICIO_ATIVIDADES = value;
            }
        }


        public String Suframa
        {
            get
            {
                return FSUFRAMA;
            }
            set
            {
                FSUFRAMA = value;
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


        public String ImagemLogotipo
        {
            get
            {
                return FIMAGEM_LOGOTIPO;
            }
            set
            {
                FIMAGEM_LOGOTIPO = value;
            }
        }


        public String Crt
        {
            get
            {
                return FCRT;
            }
            set
            {
                FCRT = value;
            }
        }


        public String TipoRegime
        {
            get
            {
                return FTIPO_REGIME;
            }
            set
            {
                FTIPO_REGIME = value;
            }
        }


        public Decimal AliquotaPis
        {
            get
            {
                return FALIQUOTA_PIS;
            }
            set
            {
                FALIQUOTA_PIS = value;
            }
        }


        public Decimal AliquotaCofins
        {
            get
            {
                return FALIQUOTA_COFINS;
            }
            set
            {
                FALIQUOTA_COFINS = value;
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


        public String Cep
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


        public String Cidade
        {
            get
            {
                return FCIDADE;
            }
            set
            {
                FCIDADE = value;
            }
        }


        public String Uf
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


        public String Contato
        {
            get
            {
                return FCONTATO;
            }
            set
            {
                FCONTATO = value;
            }
        }


        public int CodigoIbgeCidade
        {
            get
            {
                return FCODIGO_IBGE_CIDADE;
            }
            set
            {
                FCODIGO_IBGE_CIDADE = value;
            }
        }


        public int CodigoIbgeUf
        {
            get
            {
                return FCODIGO_IBGE_UF;
            }
            set
            {
                FCODIGO_IBGE_UF = value;
            }
        }


    }

}

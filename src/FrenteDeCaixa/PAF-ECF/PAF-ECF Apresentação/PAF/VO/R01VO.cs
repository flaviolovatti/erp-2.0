/********************************************************************************
Title: T2TiPDV
Description: VO relacionado à tabela R01

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


    public class R01VO
    {


        private int FID;
        private string FSERIE_ECF;
        private string FCNPJ_EMPRESA;
        private string FCNPJ_SH;
        private string FINSCRICAO_ESTADUAL_SH;
        private string FINSCRICAO_MUNICIPAL_SH;
        private string FDENOMINACAO_SH;
        private string FNOME_PAF_ECF;
        private string FVERSAO_PAF_ECF;
        private string FMD5_PAF_ECF;
        private DateTime FDATA_INICIAL;
        private DateTime FDATA_FINAL;
        private string FVERSAO_ER;
        private string FNUMERO_LAUDO_PAF;
        private string FRAZAO_SOCIAL_SH;
        private string FENDERECO_SH;
        private string FNUMERO_SH;
        private string FCOMPLEMENTO_SH;
        private string FBAIRRO_SH;
        private string FCIDADE_SH;
        private string FCEP_SH;
        private string FUF_SH;
        private string FTELEFONE_SH;
        private string FCONTATO_SH;
        private string FPRINCIPAL_EXECUTAVEL;
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


        public String CnpjEmpresa
        {
            get
            {
                return FCNPJ_EMPRESA;
            }
            set
            {
                FCNPJ_EMPRESA = value;
            }
        }


        public String CnpjSh
        {
            get
            {
                return FCNPJ_SH;
            }
            set
            {
                FCNPJ_SH = value;
            }
        }


        public String InscricaoEstadualSh
        {
            get
            {
                return FINSCRICAO_ESTADUAL_SH;
            }
            set
            {
                FINSCRICAO_ESTADUAL_SH = value;
            }
        }


        public String InscricaoMunicipalSh
        {
            get
            {
                return FINSCRICAO_MUNICIPAL_SH;
            }
            set
            {
                FINSCRICAO_MUNICIPAL_SH = value;
            }
        }


        public String DenominacaoSh
        {
            get
            {
                return FDENOMINACAO_SH;
            }
            set
            {
                FDENOMINACAO_SH = value;
            }
        }


        public String NomePafEcf
        {
            get
            {
                return FNOME_PAF_ECF;
            }
            set
            {
                FNOME_PAF_ECF = value;
            }
        }


        public String VersaoPafEcf
        {
            get
            {
                return FVERSAO_PAF_ECF;
            }
            set
            {
                FVERSAO_PAF_ECF = value;
            }
        }


        public String Md5PafEcf
        {
            get
            {
                return FMD5_PAF_ECF;
            }
            set
            {
                FMD5_PAF_ECF = value;
            }
        }


        public DateTime DataInicial
        {
            get
            {
                return FDATA_INICIAL;
            }
            set
            {
                FDATA_INICIAL = value;
            }
        }


        public DateTime DataFinal
        {
            get
            {
                return FDATA_FINAL;
            }
            set
            {
                FDATA_FINAL = value;
            }
        }


        public String VersaoEr
        {
            get
            {
                return FVERSAO_ER;
            }
            set
            {
                FVERSAO_ER = value;
            }
        }


        public String NumeroLaudoPaf
        {
            get
            {
                return FNUMERO_LAUDO_PAF;
            }
            set
            {
                FNUMERO_LAUDO_PAF = value;
            }
        }


        public String RazaoSocialSh
        {
            get
            {
                return FRAZAO_SOCIAL_SH;
            }
            set
            {
                FRAZAO_SOCIAL_SH = value;
            }
        }


        public String EnderecoSh
        {
            get
            {
                return FENDERECO_SH;
            }
            set
            {
                FENDERECO_SH = value;
            }
        }


        public String NumeroSh
        {
            get
            {
                return FNUMERO_SH;
            }
            set
            {
                FNUMERO_SH = value;
            }
        }


        public String ComplementoSh
        {
            get
            {
                return FCOMPLEMENTO_SH;
            }
            set
            {
                FCOMPLEMENTO_SH = value;
            }
        }


        public String BairroSh
        {
            get
            {
                return FBAIRRO_SH;
            }
            set
            {
                FBAIRRO_SH = value;
            }
        }


        public String CidadeSh
        {
            get
            {
                return FCIDADE_SH;
            }
            set
            {
                FCIDADE_SH = value;
            }
        }


        public String CepSh
        {
            get
            {
                return FCEP_SH;
            }
            set
            {
                FCEP_SH = value;
            }
        }


        public String UfSh
        {
            get
            {
                return FUF_SH;
            }
            set
            {
                FUF_SH = value;
            }
        }


        public String TelefoneSh
        {
            get
            {
                return FTELEFONE_SH;
            }
            set
            {
                FTELEFONE_SH = value;
            }
        }


        public String ContatoSh
        {
            get
            {
                return FCONTATO_SH;
            }
            set
            {
                FCONTATO_SH = value;
            }
        }


        public String PrincipalExecutavel
        {
            get
            {
                return FPRINCIPAL_EXECUTAVEL;
            }
            set
            {
                FPRINCIPAL_EXECUTAVEL = value;
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


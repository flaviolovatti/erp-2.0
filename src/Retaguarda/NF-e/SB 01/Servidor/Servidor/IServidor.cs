using Servidor.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Servidor
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServidor
    {

        #region === Comum ===

        #region Usuario
        [OperationContract]
        UsuarioDTO SelectUsuario(String login, String senha);
        #endregion

        #region Empresa
        [OperationContract]
        EmpresaDTO SelectObjetoEmpresa(string pFiltro);
        [OperationContract]
        IList<EmpresaDTO> SelectEmpresa(EmpresaDTO empresa);
        [OperationContract]
        IList<EmpresaDTO> SelectEmpresaPagina(int primeiroResultado, int quantidadeResultados, EmpresaDTO empresa);
        #endregion

        #endregion

        #region === Cadastros ===

        #region EstadoCivil
        [OperationContract]
        void DeleteEstadoCivil(EstadoCivilDTO estadoCivil);
        [OperationContract]
        EstadoCivilDTO SalvarAtualizarEstadoCivil(EstadoCivilDTO estadoCivil);
        [OperationContract]
        IList<EstadoCivilDTO> SelectEstadoCivil(EstadoCivilDTO estadoCivil);
        [OperationContract]
        IList<EstadoCivilDTO> SelectEstadoCivilPagina(int primeiroResultado, int quantidadeResultados, EstadoCivilDTO estadoCivil);
        #endregion 

        #region AtividadeForCli
        [OperationContract]
        void DeleteAtividadeForCli(AtividadeForCliDTO atividadeForCli);
        [OperationContract]
        AtividadeForCliDTO SalvarAtualizarAtividadeForCli(AtividadeForCliDTO atividadeForCli);
        [OperationContract]
        IList<AtividadeForCliDTO> SelectAtividadeForCli(AtividadeForCliDTO atividadeForCli);
        [OperationContract]
        IList<AtividadeForCliDTO> SelectAtividadeForCliPagina(int primeiroResultado, int quantidadeResultados, AtividadeForCliDTO atividadeForCli);
        #endregion 

        #region Cargo
        [OperationContract]
        void DeleteCargo(CargoDTO cargo);
        [OperationContract]
        CargoDTO SalvarAtualizarCargo(CargoDTO cargo);
        [OperationContract]
        IList<CargoDTO> SelectCargo(CargoDTO cargo);
        [OperationContract]
        IList<CargoDTO> SelectCargoPagina(int primeiroResultado, int quantidadeResultados, CargoDTO cargo);
        #endregion 

        #region Cbo
        [OperationContract]
        void DeleteCbo(CboDTO cbo);
        [OperationContract]
        CboDTO SalvarAtualizarCbo(CboDTO cbo);
        [OperationContract]
        IList<CboDTO> SelectCbo(CboDTO cbo);
        [OperationContract]
        IList<CboDTO> SelectCboPagina(int primeiroResultado, int quantidadeResultados, CboDTO cbo);
        #endregion 

        #region OperadoraPlanoSaude
        [OperationContract]
        void DeleteOperadoraPlanoSaude(OperadoraPlanoSaudeDTO operadoraPlanoSaude);
        [OperationContract]
        OperadoraPlanoSaudeDTO SalvarAtualizarOperadoraPlanoSaude(OperadoraPlanoSaudeDTO operadoraPlanoSaude);
        [OperationContract]
        IList<OperadoraPlanoSaudeDTO> SelectOperadoraPlanoSaude(OperadoraPlanoSaudeDTO operadoraPlanoSaude);
        [OperationContract]
        IList<OperadoraPlanoSaudeDTO> SelectOperadoraPlanoSaudePagina(int primeiroResultado, int quantidadeResultados, OperadoraPlanoSaudeDTO operadoraPlanoSaude);
        #endregion 

        #region Pais
        [OperationContract]
        void DeletePais(PaisDTO pais);
        [OperationContract]
        PaisDTO SalvarAtualizarPais(PaisDTO pais);
        [OperationContract]
        IList<PaisDTO> SelectPais(PaisDTO pais);
        [OperationContract]
        IList<PaisDTO> SelectPaisPagina(int primeiroResultado, int quantidadeResultados, PaisDTO pais);
        #endregion 

        #region Produto
        [OperationContract]
        void DeleteProduto(ProdutoDTO produto);
        [OperationContract]
        ProdutoDTO SalvarAtualizarProduto(ProdutoDTO produto);
        [OperationContract]
        IList<ProdutoDTO> SelectProduto(ProdutoDTO produto);
        [OperationContract]
        IList<ProdutoDTO> SelectProdutoPagina(int primeiroResultado, int quantidadeResultados, ProdutoDTO produto);
        #endregion 

        #region ProdutoSubGrupo
        [OperationContract]
        void DeleteProdutoSubGrupo(ProdutoSubGrupoDTO produtoSubGrupo);
        [OperationContract]
        ProdutoSubGrupoDTO SalvarAtualizarProdutoSubGrupo(ProdutoSubGrupoDTO produtoSubGrupo);
        [OperationContract]
        IList<ProdutoSubGrupoDTO> SelectProdutoSubGrupo(ProdutoSubGrupoDTO produtoSubGrupo);
        [OperationContract]
        IList<ProdutoSubGrupoDTO> SelectProdutoSubGrupoPagina(int primeiroResultado, int quantidadeResultados, ProdutoSubGrupoDTO produtoSubGrupo);
        #endregion 

        #region ProdutoMarca
        [OperationContract]
        void DeleteProdutoMarca(ProdutoMarcaDTO produtoMarca);
        [OperationContract]
        ProdutoMarcaDTO SalvarAtualizarProdutoMarca(ProdutoMarcaDTO produtoMarca);
        [OperationContract]
        IList<ProdutoMarcaDTO> SelectProdutoMarca(ProdutoMarcaDTO produtoMarca);
        [OperationContract]
        IList<ProdutoMarcaDTO> SelectProdutoMarcaPagina(int primeiroResultado, int quantidadeResultados, ProdutoMarcaDTO produtoMarca);
        #endregion 

        #region Almoxarifado
        [OperationContract]
        void DeleteAlmoxarifado(AlmoxarifadoDTO almoxarifado);
        [OperationContract]
        AlmoxarifadoDTO SalvarAtualizarAlmoxarifado(AlmoxarifadoDTO almoxarifado);
        [OperationContract]
        IList<AlmoxarifadoDTO> SelectAlmoxarifado(AlmoxarifadoDTO almoxarifado);
        [OperationContract]
        IList<AlmoxarifadoDTO> SelectAlmoxarifadoPagina(int primeiroResultado, int quantidadeResultados, AlmoxarifadoDTO almoxarifado);
        #endregion 

        #region Pessoa
        [OperationContract]
        void DeletePessoa(PessoaDTO pessoa);
        [OperationContract]
        PessoaDTO SalvarAtualizarPessoa(PessoaDTO pessoa);
        [OperationContract]
        IList<PessoaDTO> SelectPessoa(PessoaDTO pessoa);
        [OperationContract]
        IList<PessoaDTO> SelectPessoaPagina(int primeiroResultado, int quantidadeResultados, PessoaDTO pessoa);
        #endregion 

        #region Banco
        [OperationContract]
        void DeleteBanco(BancoDTO banco);
        [OperationContract]
        BancoDTO SalvarAtualizarBanco(BancoDTO banco);
        [OperationContract]
        IList<BancoDTO> SelectBanco(BancoDTO banco);
        [OperationContract]
        IList<BancoDTO> SelectBancoPagina(int primeiroResultado, int quantidadeResultados, BancoDTO banco);
        #endregion 

        #region Contador
        [OperationContract]
        IList<ContadorDTO> SelectContador(ContadorDTO contador);
        [OperationContract]
        IList<ContadorDTO> SelectContadorPagina(int primeiroResultado, int quantidadeResultados, ContadorDTO contador);
        #endregion

        #region UnidadeProduto
        [OperationContract]
        void DeleteUnidadeProduto(UnidadeProdutoDTO unidadeProduto);
        [OperationContract]
        UnidadeProdutoDTO SalvarAtualizarUnidadeProduto(UnidadeProdutoDTO unidadeProduto);
        [OperationContract]
        IList<UnidadeProdutoDTO> SelectUnidadeProduto(UnidadeProdutoDTO unidadeProduto);
        [OperationContract]
        IList<UnidadeProdutoDTO> SelectUnidadeProdutoPagina(int primeiroResultado, int quantidadeResultados, UnidadeProdutoDTO unidadeProduto);
        #endregion 

        #endregion

        #region === NFe ===

        #region NfeCabecalho
        [OperationContract]
        void DeleteNfeCabecalho(NfeCabecalhoDTO nfeCabecalho);
        [OperationContract]
        NfeCabecalhoDTO SalvarAtualizarNfeCabecalho(NfeCabecalhoDTO nfeCabecalho);
        [OperationContract]
        IList<NfeCabecalhoDTO> SelectNfeCabecalho(NfeCabecalhoDTO nfeCabecalho);
        [OperationContract]
        IList<NfeCabecalhoDTO> SelectNfeCabecalhoPagina(int primeiroResultado, int quantidadeResultados, NfeCabecalhoDTO nfeCabecalho);
        [OperationContract]
        NfeCabecalhoDTO SelectNfeCabecalhoId(int id);
        #endregion

        #endregion

        #region === Tributação ===

        #region TributOperacaoFiscal
        [OperationContract]
        int DeleteTributOperacaoFiscal(TributOperacaoFiscalDTO tributOperacaoFiscal);
        [OperationContract]
        TributOperacaoFiscalDTO SalvarAtualizarTributOperacaoFiscal(TributOperacaoFiscalDTO tributOperacaoFiscal);
        [OperationContract]
        IList<TributOperacaoFiscalDTO> SelectTributOperacaoFiscal(TributOperacaoFiscalDTO tributOperacaoFiscal);
        [OperationContract]
        IList<TributOperacaoFiscalDTO> SelectTributOperacaoFiscalPagina(int primeiroResultado, int quantidadeResultados, TributOperacaoFiscalDTO tributOperacaoFiscal);
        #endregion 

        #region TributGrupoTributario
        [OperationContract]
        void DeleteTributGrupoTributario(TributGrupoTributarioDTO tributGrupoTributario);
        [OperationContract]
        TributGrupoTributarioDTO SalvarAtualizarTributGrupoTributario(TributGrupoTributarioDTO tributGrupoTributario);
        [OperationContract]
        IList<TributGrupoTributarioDTO> SelectTributGrupoTributario(TributGrupoTributarioDTO tributGrupoTributario);
        [OperationContract]
        IList<TributGrupoTributarioDTO> SelectTributGrupoTributarioPagina(int primeiroResultado, int quantidadeResultados, TributGrupoTributarioDTO tributGrupoTributario);
        #endregion 

        #region TributIcmsCustomCab
        [OperationContract]
        void DeleteTributIcmsCustomCab(TributIcmsCustomCabDTO tributIcmsCustomCab);
        [OperationContract]
        TributIcmsCustomCabDTO SalvarAtualizarTributIcmsCustomCab(TributIcmsCustomCabDTO tributIcmsCustomCab);
        [OperationContract]
        IList<TributIcmsCustomCabDTO> SelectTributIcmsCustomCab(TributIcmsCustomCabDTO tributIcmsCustomCab);
        [OperationContract]
        IList<TributIcmsCustomCabDTO> SelectTributIcmsCustomCabPagina(int primeiroResultado, int quantidadeResultados, TributIcmsCustomCabDTO tributIcmsCustomCab);
        #endregion 

        #region ViewTributacaoCofins
        [OperationContract]
        ViewTributacaoCofinsDTO SelectViewTributacaoCofins(ViewTributacaoCofinsDTO viewTributacaoCofins);
        #endregion 

        #region ViewTributacaoPis
        [OperationContract]
        ViewTributacaoPisDTO SelectViewTributacaoPis(ViewTributacaoPisDTO viewTributacaoPis);
        #endregion 

        #region ViewTributacaoIcms
        [OperationContract]
        ViewTributacaoIcmsDTO SelectViewTributacaoIcms(ViewTributacaoIcmsDTO viewTributacaoIcms);
        #endregion

        #endregion

        #region === Sintegra ===

        [OperationContract]
        byte[] GerarSintegra(string pDataIni, string pDataFim, string pCodigoConvenio, string pFinalidade, string pNaturezaInformacao, string pIdEmpresa, string pInventario, string pIdContador);

        #endregion

        #region === Sped ===

        [OperationContract]
        byte[] GerarSped(string pDataIni, string pDataFim, string pVersao, string pFinalidade, string pPerfil, string pIdEmpresa, string pInventario, string pIdContador);
        [OperationContract]
        byte[] GerarSpedContribuicoes(string pDataIni, string pDataFim, string pVersao, string pTipoEscrituracao, string pIdEmpresa, string pIdContador);
        
        #endregion

        #region === BalcaoPAF ===

        #region DavCabecalho
        [OperationContract]
        void DeleteDavCabecalho(DavCabecalhoDTO davCabecalho);
        [OperationContract]
        DavCabecalhoDTO SalvarAtualizarDavCabecalho(DavCabecalhoDTO davCabecalho);
        [OperationContract]
        IList<DavCabecalhoDTO> SelectDavCabecalho(DavCabecalhoDTO davCabecalho);
        [OperationContract]
        IList<DavCabecalhoDTO> SelectDavCabecalhoPagina(int primeiroResultado, int quantidadeResultados, DavCabecalhoDTO davCabecalho);
        [OperationContract]
        DavCabecalhoDTO SelectObjetoDavCabecalho(string pFiltro);
        #endregion

        #region PreVendaCabecalho
        [OperationContract]
        void DeletePreVendaCabecalho(PreVendaCabecalhoDTO preVendaCabecalho);
        [OperationContract]
        PreVendaCabecalhoDTO SalvarAtualizarPreVendaCabecalho(PreVendaCabecalhoDTO preVendaCabecalho);
        [OperationContract]
        IList<PreVendaCabecalhoDTO> SelectPreVendaCabecalho(PreVendaCabecalhoDTO preVendaCabecalho);
        [OperationContract]
        IList<PreVendaCabecalhoDTO> SelectPreVendaCabecalhoPagina(int primeiroResultado, int quantidadeResultados, PreVendaCabecalhoDTO preVendaCabecalho);
        [OperationContract]
        PreVendaCabecalhoDTO SelectObjetoPreVendaCabecalho(string pFiltro);
        #endregion

        #endregion

    }


    
}

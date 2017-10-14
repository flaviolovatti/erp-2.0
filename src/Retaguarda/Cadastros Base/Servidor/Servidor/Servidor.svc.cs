using NHibernate;
using Servidor.DAL;
using Servidor.Model;
using Servidor.NHibernate;
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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please Select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class ServiceServidor : IServidor
    {

        #region === Comum ===

        #region Empresa
        public IList<EmpresaDTO> SelectEmpresa(EmpresaDTO empresa)
        {
            try
            {
                IList<EmpresaDTO> resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    EmpresaDAL DAL = new EmpresaDAL(Session);
                    resultado = DAL.Select(empresa);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<EmpresaDTO> SelectEmpresaPagina(int primeiroResultado, int quantidadeResultados, EmpresaDTO empresa)
        {
            try
            {
                IList<EmpresaDTO> resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    EmpresaDAL DAL = new EmpresaDAL(Session);
                    resultado = DAL.SelectPagina(primeiroResultado, quantidadeResultados, empresa);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion

        #region Usuario
        public UsuarioDTO SelectUsuario(String login, String senha)
        {
            try
            {
                UsuarioDTO resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    UsuarioDAL DAL = new UsuarioDAL(Session);
                    resultado = DAL.Select(login, senha);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }
        #endregion

        #endregion

        #region === Cadastros ===

        #region EstadoCivil
        public void DeleteEstadoCivil(EstadoCivilDTO estadoCivil)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<EstadoCivilDTO> DAL = new NHibernateDAL<EstadoCivilDTO>(Session);
                    DAL.Delete(estadoCivil);
                    Session.Flush();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public EstadoCivilDTO SalvarAtualizarEstadoCivil(EstadoCivilDTO estadoCivil)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<EstadoCivilDTO> DAL = new NHibernateDAL<EstadoCivilDTO>(Session);
                    DAL.SaveOrUpdate(estadoCivil);
                    Session.Flush();
                }
                return estadoCivil;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<EstadoCivilDTO> SelectEstadoCivil(EstadoCivilDTO estadoCivil)
        {
            try
            {
                IList<EstadoCivilDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<EstadoCivilDTO> DAL = new NHibernateDAL<EstadoCivilDTO>(Session);
                    Resultado = DAL.Select(estadoCivil);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<EstadoCivilDTO> SelectEstadoCivilPagina(int primeiroResultado, int quantidadeResultados, EstadoCivilDTO estadoCivil)
        {
            try
            {
                IList<EstadoCivilDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<EstadoCivilDTO> DAL = new NHibernateDAL<EstadoCivilDTO>(Session);
                    Resultado = DAL.SelectPagina<EstadoCivilDTO>(primeiroResultado, quantidadeResultados, estadoCivil);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }

        #endregion 

        #region AtividadeForCli
        public void DeleteAtividadeForCli(AtividadeForCliDTO atividadeForCli)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<AtividadeForCliDTO> DAL = new NHibernateDAL<AtividadeForCliDTO>(Session);
                    DAL.Delete(atividadeForCli);
                    Session.Flush();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public AtividadeForCliDTO SalvarAtualizarAtividadeForCli(AtividadeForCliDTO atividadeForCli)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<AtividadeForCliDTO> DAL = new NHibernateDAL<AtividadeForCliDTO>(Session);
                    DAL.SaveOrUpdate(atividadeForCli);
                    Session.Flush();
                }
                return atividadeForCli;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<AtividadeForCliDTO> SelectAtividadeForCli(AtividadeForCliDTO atividadeForCli)
        {
            try
            {
                IList<AtividadeForCliDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<AtividadeForCliDTO> DAL = new NHibernateDAL<AtividadeForCliDTO>(Session);
                    Resultado = DAL.Select(atividadeForCli);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<AtividadeForCliDTO> SelectAtividadeForCliPagina(int primeiroResultado, int quantidadeResultados, AtividadeForCliDTO atividadeForCli)
        {
            try
            {
                IList<AtividadeForCliDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<AtividadeForCliDTO> DAL = new NHibernateDAL<AtividadeForCliDTO>(Session);
                    Resultado = DAL.SelectPagina<AtividadeForCliDTO>(primeiroResultado, quantidadeResultados, atividadeForCli);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region Cargo
        public void DeleteCargo(CargoDTO cargo)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<CargoDTO> DAL = new NHibernateDAL<CargoDTO>(Session);
                    DAL.Delete(cargo);
                    Session.Flush();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public CargoDTO SalvarAtualizarCargo(CargoDTO cargo)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<CargoDTO> DAL = new NHibernateDAL<CargoDTO>(Session);
                    DAL.SaveOrUpdate(cargo);
                    Session.Flush();
                }
                return cargo;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<CargoDTO> SelectCargo(CargoDTO cargo)
        {
            try
            {
                IList<CargoDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<CargoDTO> DAL = new NHibernateDAL<CargoDTO>(Session);
                    Resultado = DAL.Select(cargo);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<CargoDTO> SelectCargoPagina(int primeiroResultado, int quantidadeResultados, CargoDTO cargo)
        {
            try
            {
                IList<CargoDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<CargoDTO> DAL = new NHibernateDAL<CargoDTO>(Session);
                    Resultado = DAL.SelectPagina<CargoDTO>(primeiroResultado, quantidadeResultados, cargo);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region Cbo
        public void DeleteCbo(CboDTO cbo)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<CboDTO> DAL = new NHibernateDAL<CboDTO>(Session);
                    DAL.Delete(cbo);
                    Session.Flush();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public CboDTO SalvarAtualizarCbo(CboDTO cbo)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<CboDTO> DAL = new NHibernateDAL<CboDTO>(Session);
                    DAL.SaveOrUpdate(cbo);
                    Session.Flush();
                }
                return cbo;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<CboDTO> SelectCbo(CboDTO cbo)
        {
            try
            {
                IList<CboDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<CboDTO> DAL = new NHibernateDAL<CboDTO>(Session);
                    Resultado = DAL.Select(cbo);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<CboDTO> SelectCboPagina(int primeiroResultado, int quantidadeResultados, CboDTO cbo)
        {
            try
            {
                IList<CboDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<CboDTO> DAL = new NHibernateDAL<CboDTO>(Session);
                    Resultado = DAL.SelectPagina<CboDTO>(primeiroResultado, quantidadeResultados, cbo);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region OperadoraPlanoSaude
        public void DeleteOperadoraPlanoSaude(OperadoraPlanoSaudeDTO operadoraPlanoSaude)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<OperadoraPlanoSaudeDTO> DAL = new NHibernateDAL<OperadoraPlanoSaudeDTO>(Session);
                    DAL.Delete(operadoraPlanoSaude);
                    Session.Flush();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public OperadoraPlanoSaudeDTO SalvarAtualizarOperadoraPlanoSaude(OperadoraPlanoSaudeDTO operadoraPlanoSaude)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<OperadoraPlanoSaudeDTO> DAL = new NHibernateDAL<OperadoraPlanoSaudeDTO>(Session);
                    DAL.SaveOrUpdate(operadoraPlanoSaude);
                    Session.Flush();
                }
                return operadoraPlanoSaude;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<OperadoraPlanoSaudeDTO> SelectOperadoraPlanoSaude(OperadoraPlanoSaudeDTO operadoraPlanoSaude)
        {
            try
            {
                IList<OperadoraPlanoSaudeDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<OperadoraPlanoSaudeDTO> DAL = new NHibernateDAL<OperadoraPlanoSaudeDTO>(Session);
                    Resultado = DAL.Select(operadoraPlanoSaude);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<OperadoraPlanoSaudeDTO> SelectOperadoraPlanoSaudePagina(int primeiroResultado, int quantidadeResultados, OperadoraPlanoSaudeDTO operadoraPlanoSaude)
        {
            try
            {
                IList<OperadoraPlanoSaudeDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<OperadoraPlanoSaudeDTO> DAL = new NHibernateDAL<OperadoraPlanoSaudeDTO>(Session);
                    Resultado = DAL.SelectPagina<OperadoraPlanoSaudeDTO>(primeiroResultado, quantidadeResultados, operadoraPlanoSaude);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region Pais
        public void DeletePais(PaisDTO pais)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<PaisDTO> DAL = new NHibernateDAL<PaisDTO>(Session);
                    DAL.Delete(pais);
                    Session.Flush();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public PaisDTO SalvarAtualizarPais(PaisDTO pais)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<PaisDTO> DAL = new NHibernateDAL<PaisDTO>(Session);
                    DAL.SaveOrUpdate(pais);
                    Session.Flush();
                }
                return pais;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<PaisDTO> SelectPais(PaisDTO pais)
        {
            try
            {
                IList<PaisDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<PaisDTO> DAL = new NHibernateDAL<PaisDTO>(Session);
                    Resultado = DAL.Select(pais);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<PaisDTO> SelectPaisPagina(int primeiroResultado, int quantidadeResultados, PaisDTO pais)
        {
            try
            {
                IList<PaisDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<PaisDTO> DAL = new NHibernateDAL<PaisDTO>(Session);
                    Resultado = DAL.SelectPagina<PaisDTO>(primeiroResultado, quantidadeResultados, pais);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region Produto
        public void DeleteProduto(ProdutoDTO produto)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoDTO> DAL = new NHibernateDAL<ProdutoDTO>(Session);
                    DAL.Delete(produto);
                    Session.Flush();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public ProdutoDTO SalvarAtualizarProduto(ProdutoDTO produto)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoDTO> DAL = new NHibernateDAL<ProdutoDTO>(Session);
                    DAL.SaveOrUpdate(produto);
                    Session.Flush();
                }
                return produto;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ProdutoDTO> SelectProduto(ProdutoDTO produto)
        {
            try
            {
                IList<ProdutoDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoDTO> DAL = new NHibernateDAL<ProdutoDTO>(Session);
                    Resultado = DAL.Select(produto);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ProdutoDTO> SelectProdutoPagina(int primeiroResultado, int quantidadeResultados, ProdutoDTO produto)
        {
            try
            {
                IList<ProdutoDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoDTO> DAL = new NHibernateDAL<ProdutoDTO>(Session);
                    Resultado = DAL.SelectPagina<ProdutoDTO>(primeiroResultado, quantidadeResultados, produto);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region ProdutoSubGrupo
        public void DeleteProdutoSubGrupo(ProdutoSubGrupoDTO produtoSubGrupo)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoSubGrupoDTO> DAL = new NHibernateDAL<ProdutoSubGrupoDTO>(Session);
                    DAL.Delete(produtoSubGrupo);
                    Session.Flush();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public ProdutoSubGrupoDTO SalvarAtualizarProdutoSubGrupo(ProdutoSubGrupoDTO produtoSubGrupo)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoSubGrupoDTO> DAL = new NHibernateDAL<ProdutoSubGrupoDTO>(Session);
                    DAL.SaveOrUpdate(produtoSubGrupo);
                    Session.Flush();
                }
                return produtoSubGrupo;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ProdutoSubGrupoDTO> SelectProdutoSubGrupo(ProdutoSubGrupoDTO produtoSubGrupo)
        {
            try
            {
                IList<ProdutoSubGrupoDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoSubGrupoDTO> DAL = new NHibernateDAL<ProdutoSubGrupoDTO>(Session);
                    Resultado = DAL.Select(produtoSubGrupo);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ProdutoSubGrupoDTO> SelectProdutoSubGrupoPagina(int primeiroResultado, int quantidadeResultados, ProdutoSubGrupoDTO produtoSubGrupo)
        {
            try
            {
                IList<ProdutoSubGrupoDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoSubGrupoDTO> DAL = new NHibernateDAL<ProdutoSubGrupoDTO>(Session);
                    Resultado = DAL.SelectPagina<ProdutoSubGrupoDTO>(primeiroResultado, quantidadeResultados, produtoSubGrupo);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region ProdutoMarca
        public void DeleteProdutoMarca(ProdutoMarcaDTO produtoMarca)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoMarcaDTO> DAL = new NHibernateDAL<ProdutoMarcaDTO>(Session);
                    DAL.Delete(produtoMarca);
                    Session.Flush();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public ProdutoMarcaDTO SalvarAtualizarProdutoMarca(ProdutoMarcaDTO produtoMarca)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoMarcaDTO> DAL = new NHibernateDAL<ProdutoMarcaDTO>(Session);
                    DAL.SaveOrUpdate(produtoMarca);
                    Session.Flush();
                }
                return produtoMarca;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ProdutoMarcaDTO> SelectProdutoMarca(ProdutoMarcaDTO produtoMarca)
        {
            try
            {
                IList<ProdutoMarcaDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoMarcaDTO> DAL = new NHibernateDAL<ProdutoMarcaDTO>(Session);
                    Resultado = DAL.Select(produtoMarca);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ProdutoMarcaDTO> SelectProdutoMarcaPagina(int primeiroResultado, int quantidadeResultados, ProdutoMarcaDTO produtoMarca)
        {
            try
            {
                IList<ProdutoMarcaDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<ProdutoMarcaDTO> DAL = new NHibernateDAL<ProdutoMarcaDTO>(Session);
                    Resultado = DAL.SelectPagina<ProdutoMarcaDTO>(primeiroResultado, quantidadeResultados, produtoMarca);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region Almoxarifado
        public void DeleteAlmoxarifado(AlmoxarifadoDTO almoxarifado)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<AlmoxarifadoDTO> DAL = new NHibernateDAL<AlmoxarifadoDTO>(Session);
                    DAL.Delete(almoxarifado);
                    Session.Flush();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public AlmoxarifadoDTO SalvarAtualizarAlmoxarifado(AlmoxarifadoDTO almoxarifado)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<AlmoxarifadoDTO> DAL = new NHibernateDAL<AlmoxarifadoDTO>(Session);
                    DAL.SaveOrUpdate(almoxarifado);
                    Session.Flush();
                }
                return almoxarifado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<AlmoxarifadoDTO> SelectAlmoxarifado(AlmoxarifadoDTO almoxarifado)
        {
            try
            {
                IList<AlmoxarifadoDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<AlmoxarifadoDTO> DAL = new NHibernateDAL<AlmoxarifadoDTO>(Session);
                    Resultado = DAL.Select(almoxarifado);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<AlmoxarifadoDTO> SelectAlmoxarifadoPagina(int primeiroResultado, int quantidadeResultados, AlmoxarifadoDTO almoxarifado)
        {
            try
            {
                IList<AlmoxarifadoDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<AlmoxarifadoDTO> DAL = new NHibernateDAL<AlmoxarifadoDTO>(Session);
                    Resultado = DAL.SelectPagina<AlmoxarifadoDTO>(primeiroResultado, quantidadeResultados, almoxarifado);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region Pessoa
        public void DeletePessoa(PessoaDTO pessoa)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<PessoaDTO> DAL = new NHibernateDAL<PessoaDTO>(Session);
                    DAL.Delete(pessoa);
                    Session.Flush();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public PessoaDTO SalvarAtualizarPessoa(PessoaDTO pessoa)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<PessoaDTO> DAL = new NHibernateDAL<PessoaDTO>(Session);
                    DAL.SaveOrUpdate(pessoa);
                    Session.Flush();
                }
                return pessoa;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<PessoaDTO> SelectPessoa(PessoaDTO pessoa)
        {
            try
            {
                IList<PessoaDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<PessoaDTO> DAL = new NHibernateDAL<PessoaDTO>(Session);
                    Resultado = DAL.Select(pessoa);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<PessoaDTO> SelectPessoaPagina(int primeiroResultado, int quantidadeResultados, PessoaDTO pessoa)
        {
            try
            {
                IList<PessoaDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<PessoaDTO> DAL = new NHibernateDAL<PessoaDTO>(Session);
                    Resultado = DAL.SelectPagina<PessoaDTO>(primeiroResultado, quantidadeResultados, pessoa);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region Contador

        public IList<ContadorDTO> SelectContador(ContadorDTO contador)
        {
            try
            {
                IList<ContadorDTO> resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<ContadorDTO> DAL = new NHibernateDAL<ContadorDTO>(Session);
                    resultado = DAL.Select(contador);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<ContadorDTO> SelectContadorPagina(int primeiroResultado, int quantidadeResultados, ContadorDTO contador)
        {
            try
            {
                IList<ContadorDTO> resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<ContadorDTO> DAL = new NHibernateDAL<ContadorDTO>(Session);
                    resultado = DAL.SelectPagina<ContadorDTO>(primeiroResultado, quantidadeResultados, contador);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion

        #region Banco
        public void DeleteBanco(BancoDTO banco)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<BancoDTO> DAL = new NHibernateDAL<BancoDTO>(Session);
                    DAL.Delete(banco);
                    Session.Flush();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public BancoDTO SalvarAtualizarBanco(BancoDTO banco)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<BancoDTO> DAL = new NHibernateDAL<BancoDTO>(Session);
                    DAL.SaveOrUpdate(banco);
                    Session.Flush();
                }
                return banco;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<BancoDTO> SelectBanco(BancoDTO banco)
        {
            try
            {
                IList<BancoDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<BancoDTO> DAL = new NHibernateDAL<BancoDTO>(Session);
                    Resultado = DAL.Select(banco);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<BancoDTO> SelectBancoPagina(int primeiroResultado, int quantidadeResultados, BancoDTO banco)
        {
            try
            {
                IList<BancoDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<BancoDTO> DAL = new NHibernateDAL<BancoDTO>(Session);
                    Resultado = DAL.SelectPagina<BancoDTO>(primeiroResultado, quantidadeResultados, banco);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region UnidadeProduto
        public void DeleteUnidadeProduto(UnidadeProdutoDTO unidadeProduto)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<UnidadeProdutoDTO> DAL = new NHibernateDAL<UnidadeProdutoDTO>(Session);
                    DAL.Delete(unidadeProduto);
                    Session.Flush();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public UnidadeProdutoDTO SalvarAtualizarUnidadeProduto(UnidadeProdutoDTO unidadeProduto)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<UnidadeProdutoDTO> DAL = new NHibernateDAL<UnidadeProdutoDTO>(Session);
                    DAL.SaveOrUpdate(unidadeProduto);
                    Session.Flush();
                }
                return unidadeProduto;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<UnidadeProdutoDTO> SelectUnidadeProduto(UnidadeProdutoDTO unidadeProduto)
        {
            try
            {
                IList<UnidadeProdutoDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<UnidadeProdutoDTO> DAL = new NHibernateDAL<UnidadeProdutoDTO>(Session);
                    Resultado = DAL.Select(unidadeProduto);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<UnidadeProdutoDTO> SelectUnidadeProdutoPagina(int primeiroResultado, int quantidadeResultados, UnidadeProdutoDTO unidadeProduto)
        {
            try
            {
                IList<UnidadeProdutoDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<UnidadeProdutoDTO> DAL = new NHibernateDAL<UnidadeProdutoDTO>(Session);
                    Resultado = DAL.SelectPagina<UnidadeProdutoDTO>(primeiroResultado, quantidadeResultados, unidadeProduto);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #endregion

        #region === NFe ===

        #region NfeCabecalho
        public IList<NfeCabecalhoDTO> SelectNfeCabecalho(NfeCabecalhoDTO nfeCabecalho)
        {
            try
            {
                IList<NfeCabecalhoDTO> resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<NfeCabecalhoDTO> DAL = new NHibernateDAL<NfeCabecalhoDTO>(Session);
                    resultado = DAL.Select(nfeCabecalho);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<NfeCabecalhoDTO> SelectNfeCabecalhoPagina(int primeiroResultado, int quantidadeResultados, NfeCabecalhoDTO nfeCabecalho)
        {
            try
            {
                IList<NfeCabecalhoDTO> resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<NfeCabecalhoDTO> DAL = new NHibernateDAL<NfeCabecalhoDTO>(Session);
                    resultado = DAL.SelectPagina<NfeCabecalhoDTO>(primeiroResultado, quantidadeResultados, nfeCabecalho);
                }
                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }
        #endregion

        #endregion

        #region === Tributação ===

        #region TributGrupoTributario
        public void DeleteTributGrupoTributario(TributGrupoTributarioDTO tributGrupoTributario)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<TributGrupoTributarioDTO> DAL = new NHibernateDAL<TributGrupoTributarioDTO>(Session);
                    DAL.Delete(tributGrupoTributario);
                    Session.Flush();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public TributGrupoTributarioDTO SalvarAtualizarTributGrupoTributario(TributGrupoTributarioDTO tributGrupoTributario)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<TributGrupoTributarioDTO> DAL = new NHibernateDAL<TributGrupoTributarioDTO>(Session);
                    DAL.SaveOrUpdate(tributGrupoTributario);
                    Session.Flush();
                }
                return tributGrupoTributario;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<TributGrupoTributarioDTO> SelectTributGrupoTributario(TributGrupoTributarioDTO tributGrupoTributario)
        {
            try
            {
                IList<TributGrupoTributarioDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<TributGrupoTributarioDTO> DAL = new NHibernateDAL<TributGrupoTributarioDTO>(Session);
                    Resultado = DAL.Select(tributGrupoTributario);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<TributGrupoTributarioDTO> SelectTributGrupoTributarioPagina(int primeiroResultado, int quantidadeResultados, TributGrupoTributarioDTO tributGrupoTributario)
        {
            try
            {
                IList<TributGrupoTributarioDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<TributGrupoTributarioDTO> DAL = new NHibernateDAL<TributGrupoTributarioDTO>(Session);
                    Resultado = DAL.SelectPagina<TributGrupoTributarioDTO>(primeiroResultado, quantidadeResultados, tributGrupoTributario);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

        #region TributIcmsCustomCab
        public void DeleteTributIcmsCustomCab(TributIcmsCustomCabDTO tributIcmsCustomCab)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<TributIcmsCustomCabDTO> DAL = new NHibernateDAL<TributIcmsCustomCabDTO>(Session);
                    DAL.Delete(tributIcmsCustomCab);
                    Session.Flush();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public TributIcmsCustomCabDTO SalvarAtualizarTributIcmsCustomCab(TributIcmsCustomCabDTO tributIcmsCustomCab)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<TributIcmsCustomCabDTO> DAL = new NHibernateDAL<TributIcmsCustomCabDTO>(Session);
                    DAL.SaveOrUpdate(tributIcmsCustomCab);
                    Session.Flush();
                }
                return tributIcmsCustomCab;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<TributIcmsCustomCabDTO> SelectTributIcmsCustomCab(TributIcmsCustomCabDTO tributIcmsCustomCab)
        {
            try
            {
                IList<TributIcmsCustomCabDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<TributIcmsCustomCabDTO> DAL = new NHibernateDAL<TributIcmsCustomCabDTO>(Session);
                    Resultado = DAL.Select(tributIcmsCustomCab);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<TributIcmsCustomCabDTO> SelectTributIcmsCustomCabPagina(int primeiroResultado, int quantidadeResultados, TributIcmsCustomCabDTO tributIcmsCustomCab)
        {
            try
            {
                IList<TributIcmsCustomCabDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<TributIcmsCustomCabDTO> DAL = new NHibernateDAL<TributIcmsCustomCabDTO>(Session);
                    Resultado = DAL.SelectPagina<TributIcmsCustomCabDTO>(primeiroResultado, quantidadeResultados, tributIcmsCustomCab);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 
        
        #endregion

        #region === Sintegra ===

        public byte[] GerarSintegra(string pDataIni, string pDataFim, string pCodigoConvenio, string pFinalidade, string pNaturezaInformacao, string pIdEmpresa, string pInventario, string pIdContador)
        {
            SintegraDAL sintegra = new SintegraDAL();

            try
            {
                if (sintegra.GerarArquivoSintegra(pDataIni, pDataFim, int.Parse(pCodigoConvenio), int.Parse(pFinalidade), int.Parse(pNaturezaInformacao), int.Parse(pIdEmpresa), int.Parse(pInventario), int.Parse(pIdContador)))
                {
                    FileInfo fi = new FileInfo("C:\\T2Ti\\Arquivos\\Sintegra.txt");
                    FileStream fs = fi.OpenRead();
                    MemoryStream ms = new MemoryStream((int)fs.Length);
                    fs.CopyTo(ms);
                    fs.Close();
                    ms.Position = 0L;
                    return ms.ToArray();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region === Sped ===

        public byte[] GerarSped(string pDataIni, string pDataFim, string pVersao, string pFinalidade, string pPerfil, string pIdEmpresa, string pInventario, string pIdContador)
        {
            SpedFiscalDAL sped = new SpedFiscalDAL();

            try
            {
                if (sped.GerarArquivoSpedFiscal(pDataIni, pDataFim, int.Parse(pVersao), int.Parse(pFinalidade), int.Parse(pPerfil), int.Parse(pIdEmpresa), int.Parse(pInventario), int.Parse(pIdContador)))
                {
                    FileInfo fi = new FileInfo("C:\\T2Ti\\Arquivos\\SpedFiscal.txt");
                    FileStream fs = fi.OpenRead();
                    MemoryStream ms = new MemoryStream((int)fs.Length);
                    fs.CopyTo(ms);
                    fs.Close();
                    ms.Position = 0L;
                    return ms.ToArray();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] GerarSpedContribuicoes(string pDataIni, string pDataFim, string pVersao, string pTipoEscrituracao, string pIdEmpresa, string pIdContador)
        {
            SpedContribuicoesDAL sped = new SpedContribuicoesDAL();

            try
            {
                if (sped.GerarArquivoSpedContribuicoes(pDataIni, pDataFim, int.Parse(pVersao), int.Parse(pTipoEscrituracao), int.Parse(pIdEmpresa), int.Parse(pIdContador)))
                {
                    FileInfo fi = new FileInfo("C:\\T2Ti\\Arquivos\\SpedContribuicoes.txt");
                    FileStream fs = fi.OpenRead();
                    MemoryStream ms = new MemoryStream((int)fs.Length);
                    fs.CopyTo(ms);
                    fs.Close();
                    ms.Position = 0L;
                    return ms.ToArray();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region === BalcaoPAF ===

        #region DavCabecalho
        public void DeleteDavCabecalho(DavCabecalhoDTO davCabecalho)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<DavCabecalhoDTO> DAL = new NHibernateDAL<DavCabecalhoDTO>(Session);
                    DAL.Delete(davCabecalho);
                    Session.Flush();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public DavCabecalhoDTO SalvarAtualizarDavCabecalho(DavCabecalhoDTO davCabecalho)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<DavCabecalhoDTO> DAL = new NHibernateDAL<DavCabecalhoDTO>(Session);
                    DAL.SaveOrUpdate(davCabecalho);
                    Session.Flush();
                }
                return davCabecalho;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<DavCabecalhoDTO> SelectDavCabecalho(DavCabecalhoDTO davCabecalho)
        {
            try
            {
                IList<DavCabecalhoDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<DavCabecalhoDTO> DAL = new NHibernateDAL<DavCabecalhoDTO>(Session);
                    Resultado = DAL.Select(davCabecalho);

                    foreach (DavCabecalhoDTO objP in Resultado)
                    {
                        NHibernateDAL<DavDetalheDTO> DALDetalhe = new NHibernateDAL<DavDetalheDTO>(Session);
                        objP.ListaDavDetalhe = DAL.Select<DavDetalheDTO>(new DavDetalheDTO { IdDavCabecalho = objP.Id });

                        if (objP.ListaDavDetalhe == null)
                            objP.ListaDavDetalhe = new List<DavDetalheDTO>();
                    }
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<DavCabecalhoDTO> SelectDavCabecalhoPagina(int primeiroResultado, int quantidadeResultados, DavCabecalhoDTO davCabecalho)
        {
            try
            {
                IList<DavCabecalhoDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<DavCabecalhoDTO> DAL = new NHibernateDAL<DavCabecalhoDTO>(Session);
                    Resultado = DAL.SelectPagina<DavCabecalhoDTO>(primeiroResultado, quantidadeResultados, davCabecalho);

                    foreach (DavCabecalhoDTO objP in Resultado)
                    {
                        NHibernateDAL<DavDetalheDTO> DALDetalhe = new NHibernateDAL<DavDetalheDTO>(Session);
                        objP.ListaDavDetalhe = DAL.Select<DavDetalheDTO>(new DavDetalheDTO { IdDavCabecalho = objP.Id });

                        if (objP.ListaDavDetalhe == null)
                            objP.ListaDavDetalhe = new List<DavDetalheDTO>();
                    }
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public DavCabecalhoDTO SelectObjetoDavCabecalho(string pFiltro)
        {
            try
            {
                DavCabecalhoDTO Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<DavCabecalhoDTO> DAL = new NHibernateDAL<DavCabecalhoDTO>(Session);
                    Resultado = DAL.SelectObjetoSql<DavCabecalhoDTO>(pFiltro);

                    if (Resultado != null)
                    {
                        NHibernateDAL<DavDetalheDTO> DALDetalhe = new NHibernateDAL<DavDetalheDTO>(Session);
                        Resultado.ListaDavDetalhe = DAL.Select<DavDetalheDTO>(new DavDetalheDTO { IdDavCabecalho = Resultado.Id });

                        if (Resultado.ListaDavDetalhe == null)
                            Resultado.ListaDavDetalhe = new List<DavDetalheDTO>();
                    }
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }

        #endregion

        #region PreVendaCabecalho
        public void DeletePreVendaCabecalho(PreVendaCabecalhoDTO preVendaCabecalho)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<PreVendaCabecalhoDTO> DAL = new NHibernateDAL<PreVendaCabecalhoDTO>(Session);
                    DAL.Delete(preVendaCabecalho);
                    Session.Flush();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public PreVendaCabecalhoDTO SalvarAtualizarPreVendaCabecalho(PreVendaCabecalhoDTO preVendaCabecalho)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<PreVendaCabecalhoDTO> DAL = new NHibernateDAL<PreVendaCabecalhoDTO>(Session);
                    DAL.SaveOrUpdate(preVendaCabecalho);
                    Session.Flush();
                }
                return preVendaCabecalho;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<PreVendaCabecalhoDTO> SelectPreVendaCabecalho(PreVendaCabecalhoDTO preVendaCabecalho)
        {
            try
            {
                IList<PreVendaCabecalhoDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<PreVendaCabecalhoDTO> DAL = new NHibernateDAL<PreVendaCabecalhoDTO>(Session);
                    Resultado = DAL.Select(preVendaCabecalho);

                    foreach (PreVendaCabecalhoDTO objP in Resultado)
                    {
                        NHibernateDAL<PreVendaDetalheDTO> DALDetalhe = new NHibernateDAL<PreVendaDetalheDTO>(Session);
                        objP.ListaPreVendaDetalhe = DAL.Select<PreVendaDetalheDTO>(new PreVendaDetalheDTO { IdPreVendaCabecalho = objP.Id });

                        if (objP.ListaPreVendaDetalhe == null)
                            objP.ListaPreVendaDetalhe = new List<PreVendaDetalheDTO>();
                    }
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<PreVendaCabecalhoDTO> SelectPreVendaCabecalhoPagina(int primeiroResultado, int quantidadeResultados, PreVendaCabecalhoDTO preVendaCabecalho)
        {
            try
            {
                IList<PreVendaCabecalhoDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<PreVendaCabecalhoDTO> DAL = new NHibernateDAL<PreVendaCabecalhoDTO>(Session);
                    Resultado = DAL.SelectPagina<PreVendaCabecalhoDTO>(primeiroResultado, quantidadeResultados, preVendaCabecalho);

                    foreach (PreVendaCabecalhoDTO objP in Resultado)
                    {
                        NHibernateDAL<PreVendaDetalheDTO> DALDetalhe = new NHibernateDAL<PreVendaDetalheDTO>(Session);
                        objP.ListaPreVendaDetalhe = DAL.Select<PreVendaDetalheDTO>(new PreVendaDetalheDTO { IdPreVendaCabecalho = objP.Id });

                        if (objP.ListaPreVendaDetalhe == null)
                            objP.ListaPreVendaDetalhe = new List<PreVendaDetalheDTO>();
                    }
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public PreVendaCabecalhoDTO SelectObjetoPreVendaCabecalho(string pFiltro)
        {
            try
            {
                PreVendaCabecalhoDTO Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<PreVendaCabecalhoDTO> DAL = new NHibernateDAL<PreVendaCabecalhoDTO>(Session);
                    Resultado = DAL.SelectObjetoSql<PreVendaCabecalhoDTO>(pFiltro);

                    if (Resultado != null)
                    {
                        NHibernateDAL<PreVendaDetalheDTO> DALDetalhe = new NHibernateDAL<PreVendaDetalheDTO>(Session);
                        Resultado.ListaPreVendaDetalhe = DAL.Select<PreVendaDetalheDTO>(new PreVendaDetalheDTO { IdPreVendaCabecalho = Resultado.Id });

                        if (Resultado.ListaPreVendaDetalhe == null)
                            Resultado.ListaPreVendaDetalhe = new List<PreVendaDetalheDTO>();
                    }
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }

        #endregion 

        #endregion
    }
}

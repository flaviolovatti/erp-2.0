using NHibernate;
using Servidor.DAL;
using Servidor.Model;
using Servidor.NHibernate;
using Servidor.Util;
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

        public EmpresaDTO SelectObjetoEmpresa(string pFiltro)
        {
            try
            {
                EmpresaDTO Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<EmpresaDTO> DAL = new NHibernateDAL<EmpresaDTO>(Session);
                    Resultado = new EmpresaDAL(Session).SelectId(1);

                    if (Resultado != null)
                    {
                        Resultado.ListaEndereco = DAL.Select<EmpresaEnderecoDTO>(new EmpresaEnderecoDTO { IdEmpresa = Resultado.Id });

                        if (Resultado.ListaEndereco == null)
                            Resultado.ListaEndereco = new List<EmpresaEnderecoDTO>();
                        else
                        {
                            for (int i = 0; i <= Resultado.ListaEndereco.Count - 1; i++)
                            {
                                if (Resultado.ListaEndereco[i].Principal == "S")
                                    Resultado.EnderecoPrincipal = Resultado.ListaEndereco[i];
                            }
                        }
                    }
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }

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

        #region === Nfe ===

        #region NfeCabecalho
        public void DeleteNfeCabecalho(NfeCabecalhoDTO nfeCabecalho)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<NfeCabecalhoDTO> DAL = new NHibernateDAL<NfeCabecalhoDTO>(Session);
                    DAL.Delete(nfeCabecalho);
                    Session.Flush();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }

        public NfeCabecalhoDTO SalvarAtualizarNfeCabecalho(NfeCabecalhoDTO nfeCabecalho)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<NfeCabecalhoDTO> DAL = new NHibernateDAL<NfeCabecalhoDTO>(Session);

                    if (nfeCabecalho.Id == 0)
                    {
                        nfeCabecalho.ChaveAcesso = nfeCabecalho.NfeEmitente.CodigoMunicipio.ToString().Substring(0, 2) +
                                           ((DateTime)nfeCabecalho.DataHoraEmissao).ToString("yy") +
                                           ((DateTime)nfeCabecalho.DataHoraEmissao).ToString("MM") +
                                           nfeCabecalho.NfeEmitente.CpfCnpj +
                                           "55" +
                                           (int.Parse(nfeCabecalho.Serie)).ToString("000") +
                                           (int.Parse(nfeCabecalho.Numero)).ToString("000000000") +
                                           nfeCabecalho.FinalidadeEmissao +
                                           (int.Parse(nfeCabecalho.Numero)).ToString("00000000");
                        nfeCabecalho.DigitoChaveAcesso = Biblioteca.DigitoModulo11(nfeCabecalho.ChaveAcesso);

                        nfeCabecalho.Numero = (int.Parse(nfeCabecalho.Numero)).ToString("000000000");
                        nfeCabecalho.CodigoNumerico = (int.Parse(nfeCabecalho.Numero)).ToString("00000000");

                        //Ambiente, 2 - homologacao
                        nfeCabecalho.Ambiente = 2;
                    }

                    DAL.SaveOrUpdate(nfeCabecalho);

                    if (nfeCabecalho.NfeDestinatario != null)
                    {
                        NHibernateDAL<NfeDestinatarioDTO> nfeDest = new NHibernateDAL<NfeDestinatarioDTO>(Session);
                        nfeCabecalho.NfeDestinatario.IdNfeCabecalho = nfeCabecalho.Id;
                        nfeDest.SaveOrUpdate(nfeCabecalho.NfeDestinatario);
                    }

                    if (nfeCabecalho.NfeEmitente != null)
                    {
                        NHibernateDAL<NfeEmitenteDTO> nfeEmit = new NHibernateDAL<NfeEmitenteDTO>(Session);
                        nfeCabecalho.NfeEmitente.IdNfeCabecalho = nfeCabecalho.Id;
                        nfeEmit.SaveOrUpdate(nfeCabecalho.NfeEmitente);
                    }

                    if (nfeCabecalho.NfeFatura != null)
                    {
                        NHibernateDAL<NfeFaturaDTO> nfeFatura = new NHibernateDAL<NfeFaturaDTO>(Session);
                        nfeCabecalho.NfeFatura.IdNfeCabecalho = nfeCabecalho.Id;
                        nfeFatura.SaveOrUpdate(nfeCabecalho.NfeFatura);
                    }

                    if (nfeCabecalho.ListaNfeDuplicata.Count > 0)
                    {
                        NHibernateDAL<NfeDuplicataDTO> nfeDuplicata = new NHibernateDAL<NfeDuplicataDTO>(Session);

                        IList<NfeDuplicataDTO> listaDuplic = nfeCabecalho.ListaNfeDuplicata;
                        foreach (NfeDuplicataDTO duplic in listaDuplic)
                        {
                            duplic.IdNfeCabecalho = nfeCabecalho.Id;
                            nfeDuplicata.SaveOrUpdate((NfeDuplicataDTO)Session.Merge(duplic));
                        }
                    }

                    if (nfeCabecalho.ListaNfeCupomFiscalReferenciado.Count > 0)
                    {
                        NHibernateDAL<NfeCupomFiscalReferenciadoDTO> nfeCF = new NHibernateDAL<NfeCupomFiscalReferenciadoDTO>(Session);

                        IList<NfeCupomFiscalReferenciadoDTO> listaCupom = nfeCabecalho.ListaNfeCupomFiscalReferenciado;
                        foreach (NfeCupomFiscalReferenciadoDTO nfeCupom in listaCupom)
                        {
                            nfeCupom.IdNfeCabecalho = nfeCabecalho.Id;
                            nfeCF.SaveOrUpdate((NfeCupomFiscalReferenciadoDTO)Session.Merge(nfeCupom));
                        }
                    }

                    if (nfeCabecalho.ListaNfeDetalhe.Count > 0)
                    {
                        NHibernateDAL<NfeDetalheDTO> nfeDetDAL = new NHibernateDAL<NfeDetalheDTO>(Session);

                        IList<NfeDetalheDTO> listaDetalhe = nfeCabecalho.ListaNfeDetalhe;
                        foreach (NfeDetalheDTO nfeDet in listaDetalhe)
                        {
                            nfeDet.IdNfeCabecalho = nfeCabecalho.Id;
                            nfeDetDAL.SaveOrUpdate(nfeDet);

                            nfeDetDAL.SaveOrUpdate((NfeDetalheDTO)Session.Merge(nfeDet));

                            if (nfeDet.NfeDetalheImpostoIcms != null)
                            {
                                NHibernateDAL<NfeDetalheImpostoIcmsDTO> impostoIcms = new NHibernateDAL<NfeDetalheImpostoIcmsDTO>(Session);
                                nfeDet.NfeDetalheImpostoIcms.IdNfeDetalhe = nfeDet.Id;
                                impostoIcms.SaveOrUpdate(nfeDet.NfeDetalheImpostoIcms);
                            }

                            if (nfeDet.NfeDetalheImpostoCofins != null)
                            {
                                NHibernateDAL<NfeDetalheImpostoCofinsDTO> impostoCofins = new NHibernateDAL<NfeDetalheImpostoCofinsDTO>(Session);
                                nfeDet.NfeDetalheImpostoCofins.IdNfeDetalhe = nfeDet.Id;
                                impostoCofins.SaveOrUpdate(nfeDet.NfeDetalheImpostoCofins);
                            }

                            if (nfeDet.NfeDetalheImpostoPis != null)
                            {
                                NHibernateDAL<NfeDetalheImpostoPisDTO> impostoPis = new NHibernateDAL<NfeDetalheImpostoPisDTO>(Session);
                                nfeDet.NfeDetalheImpostoPis.IdNfeDetalhe = nfeDet.Id;
                                impostoPis.SaveOrUpdate(nfeDet.NfeDetalheImpostoPis);
                            }

                        }
                    }

                    if (nfeCabecalho.NfeLocalEntrega != null)
                    {
                        NHibernateDAL<NfeLocalEntregaDTO> nfeLE = new NHibernateDAL<NfeLocalEntregaDTO>(Session);
                        nfeCabecalho.NfeLocalEntrega.IdNfeCabecalho = nfeCabecalho.Id;
                        nfeLE.SaveOrUpdate(nfeCabecalho.NfeLocalEntrega);
                    }

                    if (nfeCabecalho.NfeLocalRetirada != null)
                    {
                        NHibernateDAL<NfeLocalRetiradaDTO> nfeLR = new NHibernateDAL<NfeLocalRetiradaDTO>(Session);
                        nfeCabecalho.NfeLocalRetirada.IdNfeCabecalho = nfeCabecalho.Id;
                        nfeLR.SaveOrUpdate(nfeCabecalho.NfeLocalRetirada);
                    }

                    if (nfeCabecalho.NfeTransporte != null)
                    {
                        NHibernateDAL<NfeTransporteDTO> nfeTransporte = new NHibernateDAL<NfeTransporteDTO>(Session);
                        nfeCabecalho.NfeTransporte.IdNfeCabecalho = nfeCabecalho.Id;
                        nfeTransporte.SaveOrUpdate(nfeCabecalho.NfeTransporte);
                    }

                    Session.Flush();
                }
                return nfeCabecalho;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }

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

        public NfeCabecalhoDTO SelectNfeCabecalhoId(int id)
        {
            try
            {
                NfeCabecalhoDTO resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<NfeCabecalhoDTO> nfeDAL = new NHibernateDAL<NfeCabecalhoDTO>(Session);
                    resultado = nfeDAL.SelectId<NfeCabecalhoDTO>(id);
                    
                    if (resultado != null)
                    { 

                        NHibernateDAL<NfeDestinatarioDTO> nfeDest = new NHibernateDAL<NfeDestinatarioDTO>(Session);
                        resultado.NfeDestinatario = nfeDest.SelectObjeto<NfeDestinatarioDTO>(new NfeDestinatarioDTO { IdNfeCabecalho = resultado.Id });
                    
                        NHibernateDAL<NfeEmitenteDTO> nfeEmit = new NHibernateDAL<NfeEmitenteDTO>(Session);
                        resultado.NfeEmitente = nfeEmit.SelectObjeto<NfeEmitenteDTO>(new NfeEmitenteDTO { IdNfeCabecalho = resultado.Id });

                        NHibernateDAL<NfeLocalEntregaDTO> nfeLE = new NHibernateDAL<NfeLocalEntregaDTO>(Session);
                        resultado.NfeLocalEntrega = nfeLE.SelectObjeto<NfeLocalEntregaDTO>(new NfeLocalEntregaDTO { IdNfeCabecalho = resultado.Id });

                        NHibernateDAL<NfeLocalRetiradaDTO> nfeLR = new NHibernateDAL<NfeLocalRetiradaDTO>(Session);
                        resultado.NfeLocalRetirada = nfeLR.SelectObjeto<NfeLocalRetiradaDTO>(new NfeLocalRetiradaDTO { IdNfeCabecalho = resultado.Id });

                        NHibernateDAL<NfeTransporteDTO> nfeTransporte = new NHibernateDAL<NfeTransporteDTO>(Session);
                        resultado.NfeTransporte = nfeTransporte.SelectObjeto<NfeTransporteDTO>(new NfeTransporteDTO { IdNfeCabecalho = resultado.Id });

                        NHibernateDAL<NfeFaturaDTO> nfeFatura = new NHibernateDAL<NfeFaturaDTO>(Session);
                        resultado.NfeFatura = nfeFatura.SelectObjeto<NfeFaturaDTO>(new NfeFaturaDTO { IdNfeCabecalho = resultado.Id });

                        NHibernateDAL<NfeCupomFiscalReferenciadoDTO> nfeCF = new NHibernateDAL<NfeCupomFiscalReferenciadoDTO>(Session);
                        resultado.ListaNfeCupomFiscalReferenciado = nfeCF.Select<NfeCupomFiscalReferenciadoDTO>(new NfeCupomFiscalReferenciadoDTO { IdNfeCabecalho = resultado.Id });

                        NHibernateDAL<NfeDetalheDTO> nfeDetDAL = new NHibernateDAL<NfeDetalheDTO>(Session);
                        resultado.ListaNfeDetalhe = nfeDetDAL.Select<NfeDetalheDTO>(new NfeDetalheDTO { IdNfeCabecalho = id });

                        foreach (NfeDetalheDTO item in resultado.ListaNfeDetalhe)
                        {
                            NHibernateDAL<NfeDetalheImpostoCofinsDTO> nfeDetCofins = new NHibernateDAL<NfeDetalheImpostoCofinsDTO>(Session);
                            item.NfeDetalheImpostoCofins = nfeDetCofins.SelectObjeto<NfeDetalheImpostoCofinsDTO>(new NfeDetalheImpostoCofinsDTO { IdNfeDetalhe = item.Id });

                            NHibernateDAL<NfeDetalheImpostoIcmsDTO> nfeDetIcms = new NHibernateDAL<NfeDetalheImpostoIcmsDTO>(Session);
                            item.NfeDetalheImpostoIcms = nfeDetIcms.SelectObjeto<NfeDetalheImpostoIcmsDTO>(new NfeDetalheImpostoIcmsDTO { IdNfeDetalhe = item.Id });

                            NHibernateDAL<NfeDetalheImpostoIssqnDTO> nfeDetIss = new NHibernateDAL<NfeDetalheImpostoIssqnDTO>(Session);
                            item.NfeDetalheImpostoIssqn = nfeDetIss.SelectObjeto<NfeDetalheImpostoIssqnDTO>(new NfeDetalheImpostoIssqnDTO { IdNfeDetalhe = item.Id });

                            NHibernateDAL<NfeDetalheImpostoPisDTO> nfeDetPis = new NHibernateDAL<NfeDetalheImpostoPisDTO>(Session);
                            item.NfeDetalheImpostoPis = nfeDetPis.SelectObjeto<NfeDetalheImpostoPisDTO>(new NfeDetalheImpostoPisDTO { IdNfeDetalhe = item.Id });

                            NHibernateDAL<NfeDetalheImpostoIpiDTO> nfeDetIpi = new NHibernateDAL<NfeDetalheImpostoIpiDTO>(Session);
                            item.NfeDetalheImpostoIpi = nfeDetIpi.SelectObjeto<NfeDetalheImpostoIpiDTO>(new NfeDetalheImpostoIpiDTO { IdNfeDetalhe = item.Id });

                            NHibernateDAL<NfeDetalheImpostoIiDTO> nfeDetII = new NHibernateDAL<NfeDetalheImpostoIiDTO>(Session);
                            item.NfeDetalheImpostoII = nfeDetII.SelectObjeto<NfeDetalheImpostoIiDTO>(new NfeDetalheImpostoIiDTO { IdNfeDetalhe = item.Id });
                        }


                        NHibernateDAL<NfeDuplicataDTO> nfeDupl = new NHibernateDAL<NfeDuplicataDTO>(Session);
                        resultado.ListaNfeDuplicata = nfeDupl.Select<NfeDuplicataDTO>(new NfeDuplicataDTO { IdNfeCabecalho = id });
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        #endregion

        #endregion

        #region === Tributação ===

        #region TributOperacaoFiscal
        public int DeleteTributOperacaoFiscal(TributOperacaoFiscalDTO tributOperacaoFiscal)
        {
            try
            {
                int Resultado = -1;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<TributOperacaoFiscalDTO> DAL = new NHibernateDAL<TributOperacaoFiscalDTO>(Session);
                    DAL.Delete(tributOperacaoFiscal);
                    Session.Flush();
                    Resultado = 0;
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public TributOperacaoFiscalDTO SalvarAtualizarTributOperacaoFiscal(TributOperacaoFiscalDTO tributOperacaoFiscal)
        {
            try
            {
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<TributOperacaoFiscalDTO> DAL = new NHibernateDAL<TributOperacaoFiscalDTO>(Session);
                    DAL.SaveOrUpdate(tributOperacaoFiscal);
                    Session.Flush();
                }
                return tributOperacaoFiscal;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<TributOperacaoFiscalDTO> SelectTributOperacaoFiscal(TributOperacaoFiscalDTO tributOperacaoFiscal)
        {
            try
            {
                IList<TributOperacaoFiscalDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<TributOperacaoFiscalDTO> DAL = new NHibernateDAL<TributOperacaoFiscalDTO>(Session);
                    Resultado = DAL.Select(tributOperacaoFiscal);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        public IList<TributOperacaoFiscalDTO> SelectTributOperacaoFiscalPagina(int primeiroResultado, int quantidadeResultados, TributOperacaoFiscalDTO tributOperacaoFiscal)
        {
            try
            {
                IList<TributOperacaoFiscalDTO> Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<TributOperacaoFiscalDTO> DAL = new NHibernateDAL<TributOperacaoFiscalDTO>(Session);
                    Resultado = DAL.SelectPagina<TributOperacaoFiscalDTO>(primeiroResultado, quantidadeResultados, tributOperacaoFiscal);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }


        #endregion 

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
        
        #region ViewTributacaoCofins
        public ViewTributacaoCofinsDTO SelectViewTributacaoCofins(ViewTributacaoCofinsDTO viewTributacaoCofins)
        {
            try
            {
                ViewTributacaoCofinsDTO Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewTributacaoCofinsDTO> DAL = new NHibernateDAL<ViewTributacaoCofinsDTO>(Session);
                    Resultado = DAL.SelectObjeto(viewTributacaoCofins);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }

        #endregion 

        #region ViewTributacaoPis
        public ViewTributacaoPisDTO SelectViewTributacaoPis(ViewTributacaoPisDTO viewTributacaoPis)
        {
            try
            {
                ViewTributacaoPisDTO Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewTributacaoPisDTO> DAL = new NHibernateDAL<ViewTributacaoPisDTO>(Session);
                    Resultado = DAL.SelectObjeto(viewTributacaoPis);
                }
                return Resultado;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + (ex.InnerException != null ? " " + ex.InnerException.Message : ""));
            }
        }

        #endregion 

        #region ViewTributacaoIcms
        public ViewTributacaoIcmsDTO SelectViewTributacaoIcms(ViewTributacaoIcmsDTO viewTributacaoIcms)
        {
            try
            {
                ViewTributacaoIcmsDTO Resultado = null;
                using (ISession Session = NHibernateHelper.GetSessionFactory().OpenSession())
                {
                    NHibernateDAL<ViewTributacaoIcmsDTO> DAL = new NHibernateDAL<ViewTributacaoIcmsDTO>(Session);
                    Resultado = DAL.SelectObjeto(viewTributacaoIcms);
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

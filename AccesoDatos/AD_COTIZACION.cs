using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
//RDIAZ - Inicio [Ctrl. Cambio II - MultiRiesgo]
//using System.Data.OracleClient;
using Oracle.DataAccess.Client;
using System.Configuration;
//RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]
using Entidades;
using Utilitarios;
using NLog;

namespace AccesoDatos
{
    //RDIAZ - Inicio [Ctrl. Cambio II - MultiRiesgo]
    //public class AD_COTIZACION : AD_Base
    public class AD_COTIZACION : AD_BaseAccess
    //RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]
    {
        Logger Log = General.getLogger();
        public EN_COTIZARESULT ObtenerCotizacion(EN_COTIZA objCotizar)
        {
            //OracleDataAdapter daAdaptador = new OracleDataAdapter();
            //DataSet dsResultado = new DataSet();
            Log.Debug(" Inicio acceso a datos Cotizacion ");
            OracleDataReader dr = null;
            EN_COTIZARESULT objCotizaResult = new EN_COTIZARESULT();

            //RDIAZ - Inicio [Ctrl. Cambio II - MultiRiesgo]
            string coneccion = ConfigurationManager.AppSettings["ConeccionTron"].ToString();
            OracleConnection Cnx = new OracleConnection(coneccion);
            //RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]

            try
            {
                OracleCommand cmdOracle = new OracleCommand();
                cmdOracle.CommandType = CommandType.StoredProcedure;
                //RDIAZ - Inicio [Ctrl. Cambio II - MultiRiesgo]
                //cmdOracle.CommandText = "TRON2000.em_k_cotiza_empresa_mpe.p_cotiza";
                cmdOracle.CommandText = "TRON2000.em_k_cotiza_salud_mpe.p_cotiza_multiriesgo";
                //RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]
                OracleParameter Param;

                #region PARAMETROS

                    #region "Input  => 0 - 10"  

                        Param = new OracleParameter("p_cod_Agt", OracleDbType.Int32);
                        Param.Value = Formateo_Numero(objCotizar.cod_agt);
                        cmdOracle.Parameters.Add(Param);
                        Param = new OracleParameter("p_cod_mon", OracleDbType.Int32);
                        Param.Value = Formateo_Numero(objCotizar.cod_mon);
                        cmdOracle.Parameters.Add(Param);
                        Param = new OracleParameter("p_mca_imp_mafre_dolar", OracleDbType.Varchar2);
                        Param.Value = Formateo_Texto(objCotizar.mca_imp_mapfre_dolar);
                        cmdOracle.Parameters.Add(Param);
                        Param = new OracleParameter("p_imp_mapfre_dolar", OracleDbType.Double);
                        Param.Value = Formateo_Numero(objCotizar.imp_mapfre_dolar);
                        cmdOracle.Parameters.Add(Param);
                        /*Param = new OracleParameter("p_pct_dsct_comercial", OracleDbType.Double);
                        Param.Value = Formateo_Numero(objCotizar.pct_dscto_comercial);
                        cmdOracle.Parameters.Add(Param);*/
                        Param = new OracleParameter("p_Cod_frac_pago", OracleDbType.Int32);
                        Param.Value = Formateo_Numero(objCotizar.cod_frac_pago);
                        cmdOracle.Parameters.Add(Param);
                        Param = new OracleParameter("p_cod_cia", OracleDbType.Int32);
                        Param.Value = Formateo_Numero(objCotizar.cod_cia);
                        cmdOracle.Parameters.Add(Param);
                        Param = new OracleParameter("p_cod_ramo", OracleDbType.Int32);
                        Param.Value = Formateo_Numero(objCotizar.cod_ramo);
                        cmdOracle.Parameters.Add(Param);
                        Param = new OracleParameter("p_Num_Poliza_Grupo", OracleDbType.Varchar2);
                        Param.Value = Formateo_Texto(objCotizar.num_poliza_grupo);
                        cmdOracle.Parameters.Add(Param);
                        Param = new OracleParameter("p_Cod_modalidad", OracleDbType.Int32);
                        Param.Value = Formateo_Numero(objCotizar.cod_modalidad);
                        cmdOracle.Parameters.Add(Param);
                        Param = new OracleParameter("p_Num_contrato", OracleDbType.Int32);
                        Param.Value = Formateo_Numero(objCotizar.num_contrato);
                        cmdOracle.Parameters.Add(Param);
                        Param = new OracleParameter("p_Num_sub_contrato", OracleDbType.Int32);
                        Param.Value = Formateo_Numero(objCotizar.num_sub_contrato);
                        cmdOracle.Parameters.Add(Param);
                        //JHUARCAYA - INICIO REQ SALUD 02022021
                        Param = new OracleParameter("p_Cod_Producto", OracleDbType.Int32);
                        Param.Value = Formateo_Numero(objCotizar.cod_producto);
                        cmdOracle.Parameters.Add(Param);
                        Param = new OracleParameter("p_Cod_Sub_Producto", OracleDbType.Int32);
                        Param.Value = Formateo_Numero(objCotizar.cod_sub_producto);
                        cmdOracle.Parameters.Add(Param);
                        Param = new OracleParameter("p_Cod_Plan", OracleDbType.Int32);
                        Param.Value = Formateo_Numero(objCotizar.cod_plan);
                        cmdOracle.Parameters.Add(Param);
                        //JHUARCAYA - FIN REQ SALUD 02022021
                        Param = new OracleParameter("p_fec_efec_poliza", OracleDbType.Date);
                        Param.Value = Formateo_Fecha(objCotizar.fec_efec_pol);
                        cmdOracle.Parameters.Add(Param);
                        Param = new OracleParameter("p_fec_vcto_poliza", OracleDbType.Date);
                        Param.Value = Formateo_Fecha(objCotizar.fec_vcto_pol);
                        cmdOracle.Parameters.Add(Param);
                        /*INICIO PBARRIOS 20190411*/
                        Param = new OracleParameter("p_mca_pct_int_red_plaza", OracleDbType.Varchar2);
                        Param.Value = Formateo_Texto(objCotizar.mca_pct_int_red_plaza);
                        cmdOracle.Parameters.Add(Param);
                        Param = new OracleParameter("p_pct_ajus_int_red_plaza", OracleDbType.Int32);
                        Param.Value = Formateo_Numero(objCotizar.pct_ajus_int_red_plaza);
                        cmdOracle.Parameters.Add(Param);/*FIN PBARRIOS 20190411*/

                        Param = new OracleParameter("p_riesgosXml", OracleDbType.XmlType);///TRAMA XML
                        Param.Value = objCotizar.xml_riesgos;  // Formateo_Xml(objCotizar.datos_riesgo_cotiza);
                        cmdOracle.Parameters.Add(Param);
                    #endregion
                    #region "Return  1 - 3"
                        Param = new OracleParameter("p_nRetorno", OracleDbType.Varchar2, 100);
                        Param.Direction = ParameterDirection.Output;
                        cmdOracle.Parameters.Add(Param);
                        Param = new OracleParameter("p_cMensaje", OracleDbType.Varchar2, 4000);
                        Param.Direction = ParameterDirection.Output;
                        cmdOracle.Parameters.Add(Param);

                        Param = new OracleParameter("o_cursor", OracleDbType.RefCursor);
                        Param.Direction = ParameterDirection.Output;
                        cmdOracle.Parameters.Add(Param);
                    #endregion
                
                #endregion

                //RDIAZ - Inicio [Ctrl. Cambio II - MultiRiesgo]
                //dr = base.GetDataReaderTron(cmdOracle);
                Cnx.Open();
                cmdOracle.Connection = Cnx;
                Log.Debug(" antes de ejecutar cmdOracle.ExecuteReader ");
                dr = cmdOracle.ExecuteReader();
                //RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]
              
                objCotizaResult.COD_ERROR = Convert.ToInt32(cmdOracle.Parameters["p_nRetorno"].Value.ToString());
                Log.Debug(" luego de ejecutar : " + objCotizaResult.COD_ERROR);
                Log.Debug(" luego de ejecutar : " + objCotizaResult.DESC_ERROR);

                if (objCotizaResult.COD_ERROR != 0)
                {
                    objCotizaResult.DESC_ERROR = cmdOracle.Parameters["p_cMensaje"].Value.ToString();
                }
                else 
                {
                    objCotizaResult.DESC_ERROR = "";
                    int i = 1;
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (!Convert.IsDBNull(dr["num_cotizacion"]) && !Convert.IsDBNull(dr["num_cotizacion"]))  //me aseguro que devuelva data y no la fila null
                            {
                                if (i == 1)
                                {
                                    if (!Convert.IsDBNull(dr["num_cotizacion"]))
                                        objCotizaResult.NUM_COTIZA = Convert.ToInt64(dr["num_cotizacion"].ToString());

                                    if (!Convert.IsDBNull(dr["cod_moneda"]))
                                        objCotizaResult.COD_MON = Convert.ToInt32(dr["cod_moneda"].ToString());

                                    if (!Convert.IsDBNull(dr["simb_moneda"]))
                                        objCotizaResult.SIMBL_MONEDA = dr["simb_moneda"].ToString();

                                    if (!Convert.IsDBNull(dr["cod_fracc_pago"]))
                                        objCotizaResult.COD_FRAC_PAGO = Convert.ToInt32(dr["cod_fracc_pago"].ToString());

                                    if (!Convert.IsDBNull(dr["prima_neta"]))
                                        objCotizaResult.IMP_NETA = Convert.ToDouble(dr["prima_neta"].ToString());

                                    if (!Convert.IsDBNull(dr["recargos"]))
                                        objCotizaResult.IMP_DERECHO_EMISION = Convert.ToDouble(dr["recargos"].ToString());

                                    if (!Convert.IsDBNull(dr["impuestos"]))
                                        objCotizaResult.IMP_IGV = Convert.ToDouble(dr["impuestos"].ToString());

                                    if (!Convert.IsDBNull(dr["bonificacion"]))
                                        objCotizaResult.IMP_BONIFI = Convert.ToDouble(dr["bonificacion"].ToString());

                                    if (!Convert.IsDBNull(dr["intereses"]))
                                        objCotizaResult.IMP_INTERES = Convert.ToDouble(dr["intereses"].ToString());

                                    if (!Convert.IsDBNull(dr["prima_net_sin"]))
                                        objCotizaResult.IMP_NETA_BONI = Convert.ToDouble(dr["prima_net_sin"].ToString());

                                    //RDIAZ - Inicio [24/10/2017]
                                    if (!Convert.IsDBNull(dr["Imp_Recibo_Total"]))
                                        objCotizaResult.IMP_RECIBO_TOTAL = Convert.ToDouble(dr["Imp_Recibo_Total"].ToString());
                                    //RDIAZ - Fin [24/10/2017]

                                }
                                EN_RECIBO objRecibo = new EN_RECIBO();
                                if (!Convert.IsDBNull(dr["num_cuota"]))
                                    objRecibo.NUM_CUOTA = Convert.ToInt32(dr["num_cuota"].ToString());

                                if (!Convert.IsDBNull(dr["fec_efec_recibo"]))
                                    objRecibo.FEC_EFEC_RECIBO = Convert.ToDateTime(dr["fec_efec_recibo"].ToString()).ToString("dd/MM/yyyy");

                                if (!Convert.IsDBNull(dr["fec_vcto_recibo"]))
                                    objRecibo.FEC_VCTO_RECIBO = Convert.ToDateTime(dr["fec_vcto_recibo"].ToString()).ToString("dd/MM/yyyy");

                                if (!Convert.IsDBNull(dr["imp_recibo"]))
                                    objRecibo.IMP_RECIBO = Convert.ToDouble(dr["imp_recibo"].ToString());
                                //
                                if (!Convert.IsDBNull(dr["imp_neta"]))
                                    objRecibo.IMP_NETA = Convert.ToDouble(dr["imp_neta"].ToString());

                                if (!Convert.IsDBNull(dr["imp_recargo"]))
                                    objRecibo.IMP_RECARGO = Convert.ToDouble(dr["imp_recargo"].ToString());

                                if (!Convert.IsDBNull(dr["imp_imptos"]))
                                    objRecibo.IMP_IMPTOS = Convert.ToDouble(dr["imp_imptos"].ToString());
                               
                                if (!Convert.IsDBNull(dr["imp_boni"]))
                                    objRecibo.IMP_BONI = Convert.ToDouble(dr["imp_boni"].ToString());

                                if (!Convert.IsDBNull(dr["imp_interes"]))
                                    objRecibo.IMP_INTERES = Convert.ToDouble(dr["imp_interes"].ToString());

                                if (!Convert.IsDBNull(dr["imp_imptos_interes"]))
                                    objRecibo.IMP_IMPTOS_INTERES = Convert.ToDouble(dr["imp_imptos_interes"].ToString());

                                objCotizaResult.RECIBOS.Add(objRecibo);
                                i++;
                            }
                        }
                    }
                }

                Log.Debug(" Fin acceso a datos Cotizacion ");
            }
            catch (OracleException ex)
            {
                Log.Error("Error al conectar en el Oracle COTIZA: " + ex.Message);
                //throw new Exception("Ocurrio un error inesperado." + "AD_COTIZACION:" + "ObtenerCotizacion");
                objCotizaResult.COD_ERROR = 99;
                objCotizaResult.DESC_ERROR = "OracleException, Error General en el proceso.(por algún error no controlado)";
            }
            catch (Exception ex)
            {
                Log.Error("Error acceso a datos COTIZA: " + ex.Message);
                //throw new Exception("Ocurrio un error inesperado." + "AD_COTIZACION:" + "ObtenerCotizacion");
                objCotizaResult.COD_ERROR = 99;
                objCotizaResult.DESC_ERROR = "Exception, Error General en el proceso (por algún error no controlado).";
            }
            finally
            {
                //RDIAZ - Inicio [Ctrl. Cambio II - MultiRiesgo]
                //CloseOracleConnection();
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                    dr.Dispose();
                }
                if (Cnx.State.Equals(ConnectionState.Open))
                {
                    Cnx.Close();
                }
                //RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]
            }
            return objCotizaResult;
        }

        
        public EN_USUARIO Recupera_usuario(String Usuario, String Password)
        {
            //OracleDataAdapter daAdaptador = new OracleDataAdapter();
            //DataSet dsResultado = new DataSet();
            OracleDataReader dr = null;
            EN_USUARIO objusurio = new EN_USUARIO();

            //RDIAZ - Inicio [Ctrl. Cambio II - MultiRiesgo]
            string coneccion = ConfigurationManager.AppSettings["ConeccionWeb"].ToString();
            OracleConnection Cnx = new OracleConnection(coneccion);
            //RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]

            try
            {
                Log.Debug(" Inicio acceso a datos LOGIN ");
                OracleCommand cmdOracle = new OracleCommand();
                cmdOracle.CommandType = CommandType.StoredProcedure;
                cmdOracle.CommandText = "oim.pkg_servicios_mapfre.sp_recuperar_usuario";
                OracleParameter Param;
                #region PARAMETROS
                Param = new OracleParameter("p_codigo", OracleDbType.Varchar2);
                Param.Value = Formateo_Texto(Usuario);
                cmdOracle.Parameters.Add(Param);

                Param = new OracleParameter("p_password", OracleDbType.Varchar2);
                Param.Value = Formateo_Texto(Password);
                cmdOracle.Parameters.Add(Param);

                Param = new OracleParameter("c_cur_res", OracleDbType.RefCursor);
                Param.Direction = ParameterDirection.Output;
                cmdOracle.Parameters.Add(Param);
                #endregion

                //RDIAZ - Inicio [Ctrl. Cambio II - MultiRiesgo]
                //dr = base.GetDataReader(cmdOracle);
                Cnx.Open();
                cmdOracle.Connection = Cnx;
                dr = cmdOracle.ExecuteReader();
                //RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]
                    
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {

                                    if (!Convert.IsDBNull(dr["cantidad"]))
                                        objusurio.cantidad = Convert.ToInt32(dr["cantidad"].ToString());

                                    if (!Convert.IsDBNull(dr["codigo"]))
                                        objusurio.codigo = dr["codigo"].ToString();

                                    if (!Convert.IsDBNull(dr["tip_doc"]))
                                        objusurio.tip_doc = dr["tip_doc"].ToString();

                                    if (!Convert.IsDBNull(dr["cod_docum"]))
                                        objusurio.cod_docum = dr["cod_docum"].ToString();

                                    if (!Convert.IsDBNull(dr["nom_tercero"]))
                                        objusurio.nom_tercero = dr["nom_tercero"].ToString();

                                    if (!Convert.IsDBNull(dr["ape1_tercero"]))
                                        objusurio.ape1_tercero = dr["ape1_tercero"].ToString();

                                    if (!Convert.IsDBNull(dr["ape2_tercero"]))
                                        objusurio.ape2_tercero = dr["ape2_tercero"].ToString();

                            
                        }

                }
                 Log.Debug(" FIN acceso a datos LOGIN ");
            }
            catch (OracleException ex)
            {
                Log.Error("Error acceso a datos LOGIN: " + ex.Message);
                //throw new Exception("Ocurrio un error inesperado." + "AD_COTIZACION:" + "ObtenerCotizacion");
                objusurio.COD_ERROR = "99";
                objusurio.DESC_ERROR = "OracleException, Ocurrio un error al certificar el ingreso " + objusurio.COD_ERROR + ". ";
            }
            catch (Exception ex)
            {
                Log.Error("Error acceso a datos LOGIN: " + ex.Message);
                //throw new Exception("Ocurrio un error inesperado." + "AD_COTIZACION:" + "ObtenerCotizacion");
                objusurio.COD_ERROR = "99";
                objusurio.DESC_ERROR = "Exception, Ocurrio un error al certificar el ingreso " + objusurio.COD_ERROR + ". ";
            }
            finally
            {
                //RDIAZ - Inicio [Ctrl. Cambio II - MultiRiesgo]
                //CloseOracleConnection();
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                    dr.Dispose();
                }
                if (Cnx.State.Equals(ConnectionState.Open))
                {
                    Cnx.Close();
                }
                //RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]
            }

            return objusurio;
        }

    }
}

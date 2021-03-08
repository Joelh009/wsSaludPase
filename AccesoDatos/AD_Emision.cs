using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
//using System.Data.OracleClient;
using System.Linq;
using System.Text;
using Entidades;

using Oracle.DataAccess.Client;
using Utilitarios;
using NLog;
using System.IO;

namespace AccesoDatos
{
    //RDIAZ - Inicio [Ctrl. Cambio II - MultiRiesgo]
    //public class AD_Emision
    public class AD_Emision : AD_BaseAccess
    //RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]
    {
        Logger Log = General.getLogger();

         public EN_EMITERESULT Emitir(EN_EMITE objEmitir)
        {
            //OracleDataAdapter daAdaptador = new OracleDataAdapter();
            //DataSet dsResultado = new DataSet();
            EscrbirLog(DateTime.Now.ToString() + "INGRESO EMITIR: AD_EMISION, METODO emitir");

            OracleDataReader dr = null;
            EN_EMITERESULT objEmiteResult = new EN_EMITERESULT();

            string coneccion = ConfigurationManager.AppSettings["ConeccionTron"].ToString();
            OracleConnection Cnx = new OracleConnection(coneccion);

            EscrbirLog(DateTime.Now.ToString() + "-- " + "coneccion: " + coneccion.ToString());
            try
            { 
                Log.Debug(" Inicio acceso a datos Emision ");



                OracleCommand cmdOracle = new OracleCommand();
                cmdOracle.CommandType = CommandType.StoredProcedure;
                cmdOracle.CommandText = "TRON2000.em_k_emis_salud_mpe.p_emite";
                OracleParameter Param;

                #region "PARAMETROS"

                    #region "Input"                
                
                        #region "Input  => 0 - 10"
                          
                            Param = new OracleParameter("p_cod_agt", OracleDbType.Int32);
                            Param.Value = Formateo_Numero(objEmitir.cod_agt);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "cod_agt: " + objEmitir.cod_agt.ToString());

                            Param = new OracleParameter("p_cod_mon", OracleDbType.Int32);
                            Param.Value = Formateo_Numero(objEmitir.cod_mon);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "cod_mon: " + objEmitir.cod_mon.ToString());

                            Param = new OracleParameter("p_Mca_imp_mafre_dolar", OracleDbType.Varchar2, 1);
                            Param.Value = Formateo_Texto(objEmitir.mca_imp_mapfre_dolar);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "mca_imp_mapfre_dolar: " + objEmitir.mca_imp_mapfre_dolar.ToString());


                            Param = new OracleParameter("p_Imp_mapfre_dolar", OracleDbType.Double);
                            Param.Value = Formateo_Numero(objEmitir.imp_mapfre_dolar);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "imp_mapfre_dolar: " + objEmitir.imp_mapfre_dolar.ToString());
               
                            Param = new OracleParameter("p_Cod_frac_pago", OracleDbType.Int32);
                            Param.Value = Formateo_Numero(objEmitir.cod_frac_pago);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "cod_frac_pago: " + objEmitir.cod_frac_pago.ToString());

                            Param = new OracleParameter("p_Cod_cia", OracleDbType.Int32);
                            Param.Value = Formateo_Numero(objEmitir.cod_cia);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "cod_cia: " + objEmitir.cod_cia.ToString());

                            Param = new OracleParameter("p_Cod_ramo", OracleDbType.Int32);
                            Param.Value = Formateo_Numero(objEmitir.cod_ramo);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "cod_ramo: " + objEmitir.cod_ramo.ToString());

                            Param = new OracleParameter("p_Num_Poliza_Grupo", OracleDbType.Varchar2, 13);
                            Param.Value = Formateo_Texto(objEmitir.num_poliza_grupo);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "num_poliza_grupo: " + objEmitir.num_poliza_grupo.ToString());

                            Param = new OracleParameter("p_Cod_modalidad", OracleDbType.Int32);
                            Param.Value = Formateo_Numero(objEmitir.cod_modalidad);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "cod_modalidad: " + objEmitir.cod_modalidad.ToString());

                            Param = new OracleParameter("p_Num_Contrato", OracleDbType.Int32);
                            Param.Value = Formateo_Numero(objEmitir.num_contrato);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "num_contrato: " + objEmitir.num_contrato.ToString());

                            Param = new OracleParameter("p_Num_Sub_contrato", OracleDbType.Int32);
                            Param.Value = Formateo_Numero(objEmitir.num_sub_contrato);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "num_sub_contrato: " + objEmitir.num_sub_contrato.ToString());

                            //JHUARCAYA - INICIO REQ SALUD 02022021
                            Param = new OracleParameter("p_Cod_Producto", OracleDbType.Int32);
                            Param.Value = Formateo_Numero(objEmitir.cod_producto);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "cod_producto: " + objEmitir.cod_producto.ToString());

                            Param = new OracleParameter("p_Cod_Sub_Producto", OracleDbType.Int32);
                            Param.Value = Formateo_Numero(objEmitir.cod_sub_producto);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "cod_sub_producto: " + objEmitir.cod_sub_producto.ToString());

                            Param = new OracleParameter("p_Cod_Plan", OracleDbType.Int32);
                            Param.Value = Formateo_Numero(objEmitir.cod_plan);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "cod_plan: " + objEmitir.cod_plan.ToString());

                            //JHUARCAYA - FIN REQ SALUD 02022021

                            Param = new OracleParameter("p_fec_efec_poliza", OracleDbType.Date);
                            Param.Value = Formateo_Fecha(objEmitir.fec_efec_pol);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "fec_efec_pol: " + objEmitir.fec_efec_pol.ToString());

                            Param = new OracleParameter("p_fec_vcto_poliza", OracleDbType.Date);
                            Param.Value = Formateo_Fecha(objEmitir.fec_vcto_pol);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "fec_vcto_pol: " + objEmitir.fec_vcto_pol.ToString());

                            Param = new OracleParameter("p_Mca_Fisico_Contratante", OracleDbType.Varchar2,1);
                            Param.Value = Formateo_Texto(objEmitir.mca_fisico_contratante);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "mca_fisico_contratante: " + objEmitir.mca_fisico_contratante.ToString());

                            Param = new OracleParameter("p_Tip_Documento_Contratante", OracleDbType.Varchar2, 30);
                            Param.Value = Formateo_Texto(objEmitir.tip_documento_contratante);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "tip_documento_contratante: " + objEmitir.tip_documento_contratante.ToString());

                            Param = new OracleParameter("p_Cod_Documento_Contratante", OracleDbType.Varchar2,20);
                            Param.Value = Formateo_Texto(objEmitir.cod_documento_contratante);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "cod_documento_contratante: " + objEmitir.cod_documento_contratante.ToString());

                            Param = new OracleParameter("p_Nom_Contratante", OracleDbType.Varchar2, 100);
                            Param.Value = Formateo_Texto(objEmitir.nombre_contratante);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "nombre_contratante: " + objEmitir.nombre_contratante.ToString());

                            Param = new OracleParameter("p_Ape_Paterno_Contratante", OracleDbType.Varchar2, 100);
                            Param.Value = Formateo_Texto(objEmitir.ape_paterno_contratante);
                            cmdOracle.Parameters.Add(Param);


                            EscrbirLog(DateTime.Now.ToString() + "-- " + "ape_paterno_contratante: " + objEmitir.ape_paterno_contratante.ToString());

                            Param = new OracleParameter("p_Ape_Materno_Contratante", OracleDbType.Varchar2, 100);
                            Param.Value = Formateo_Texto(objEmitir.ape_materno_contratante);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "ape_materno_contratante: " + objEmitir.ape_materno_contratante.ToString());

                            Param = new OracleParameter("p_Mail_Contratante", OracleDbType.Varchar2, 100);
                            Param.Value = Formateo_Texto(objEmitir.mail_contrantante);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "mail_contrantante: " + objEmitir.mail_contrantante.ToString());

                            Param = new OracleParameter("p_Tel_Fijo_Contratante", OracleDbType.Varchar2, 20);
                            Param.Value = Formateo_Texto(objEmitir.tel_fijo_contratante);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "tel_fijo_contratante: " + objEmitir.tel_fijo_contratante.ToString());

                            Param = new OracleParameter("p_Tel_Movil_Contratante", OracleDbType.Varchar2, 20);
                            Param.Value = Formateo_Texto(objEmitir.tel_movil_contratante);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "tel_movil_contratante: " + objEmitir.tel_movil_contratante.ToString());

                            Param = new OracleParameter("p_Representante", OracleDbType.Varchar2, 100);
                            Param.Value = Formateo_Texto(objEmitir.representante);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "representante: " + objEmitir.representante.ToString());

                            Param = new OracleParameter("p_Cargo_Representante", OracleDbType.Int32);
                            Param.Value = Formateo_Numero(objEmitir.cargo_representante);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "cargo_representante: " + objEmitir.cargo_representante.ToString());

                            Param = new OracleParameter("p_Tip_Economica", OracleDbType.Varchar2, 20);
                            Param.Value = Formateo_Texto(objEmitir.tip_economica);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "tip_economica: " + objEmitir.tip_economica.ToString());

                            Param = new OracleParameter("p_Fec_Nacimiento_Cliente", OracleDbType.Date);
                            Param.Value = Formateo_Fecha(objEmitir.fec_nacimiento_cliente);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "fec_nacimiento_cliente: " + objEmitir.fec_nacimiento_cliente.ToString());

                            Param = new OracleParameter("p_Mca_Sexo_Cliente", OracleDbType.Varchar2, 1);
                            Param.Value = Formateo_Texto(objEmitir.mca_sexo_cliente);
                            cmdOracle.Parameters.Add(Param);


                            EscrbirLog(DateTime.Now.ToString() + "-- " + "mca_sexo_cliente: " + objEmitir.mca_sexo_cliente.ToString());


                            Param = new OracleParameter("p_Profesion_Cliente", OracleDbType.Int32);
                            Param.Value = Formateo_Numero(objEmitir.profesion_cliente);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "profesion_cliente: " + objEmitir.profesion_cliente.ToString());

                            Param = new OracleParameter("p_Est_Civil", OracleDbType.Varchar2, 20);
                            Param.Value = Formateo_Texto(objEmitir.est_civil);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "est_civil: " + objEmitir.est_civil.ToString());

                            Param = new OracleParameter("p_Pais_Cliente", OracleDbType.Varchar2, 3);
                            Param.Value = Formateo_Texto(objEmitir.cod_pais);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "cod_pais: " + objEmitir.cod_pais.ToString());

                            Param = new OracleParameter("p_Nacionalidad_Cliente", OracleDbType.Varchar2, 3);
                            Param.Value = Formateo_Texto(objEmitir.cod_nacionalidad);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "cod_nacionalidad: " + objEmitir.cod_nacionalidad.ToString());

                            Param = new OracleParameter("p_Estado_Cliente", OracleDbType.Int32,3);
                            Param.Value = Formateo_Numero(objEmitir.cod_estado_cli);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "cod_estado_cli: " + objEmitir.cod_estado_cli.ToString());

                            Param = new OracleParameter("p_Provincia_Cliente", OracleDbType.Int32,3);
                            Param.Value = Formateo_Numero(objEmitir.cod_provincia);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "cod_provincia: " + objEmitir.cod_provincia.ToString());

                            Param = new OracleParameter("p_Distrito_Cliente", OracleDbType.Int32, 3);
                            Param.Value = Formateo_Numero(objEmitir.cod_distrito);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "cod_distrito: " + objEmitir.cod_distrito.ToString());

                            Param = new OracleParameter("p_Tipo_Domicilio_Cliente", OracleDbType.Int32, 20);
                            Param.Value = Formateo_Numero(objEmitir.tipo_domicilio);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "tipo_domicilio: " + objEmitir.tipo_domicilio.ToString());

                            Param = new OracleParameter("p_Domicilio_Cliente", OracleDbType.Varchar2, 20);
                            Param.Value = Formateo_Texto(objEmitir.domicilio_cliente);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "domicilio_cliente: " + objEmitir.domicilio_cliente.ToString());

                            Param = new OracleParameter("p_Tip_Numero_Cliente", OracleDbType.Int32);
                            Param.Value = Formateo_Numero(objEmitir.tip_numero_cliente);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "tip_numero_cliente: " + objEmitir.tip_numero_cliente.ToString());


                            Param = new OracleParameter("p_Numero_Cliente", OracleDbType.Varchar2,5);
                            Param.Value = Formateo_Texto(objEmitir.num_cliente);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "num_cliente: " + objEmitir.num_cliente.ToString());

                            Param = new OracleParameter("p_Tip_Interior_Cliente", OracleDbType.Int32);
                            Param.Value = Formateo_Numero(objEmitir.tip_interior_cliente);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "tip_interior_cliente: " + objEmitir.tip_interior_cliente.ToString());

                            Param = new OracleParameter("p_Interior_Cliente", OracleDbType.Varchar2, 20);
                            Param.Value = Formateo_Texto(objEmitir.interior_cliente);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "interior_cliente: " + objEmitir.interior_cliente.ToString());

                            Param = new OracleParameter("p_Tip_Zona_Cliente", OracleDbType.Int32);
                            Param.Value = Formateo_Numero(objEmitir.tip_zona_cliente);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "tip_zona_cliente: " + objEmitir.tip_zona_cliente.ToString());

                            Param = new OracleParameter("p_Zona_Cliente", OracleDbType.Varchar2, 20);
                            Param.Value = Formateo_Texto(objEmitir.zona_cliente);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "zona_cliente: " + objEmitir.zona_cliente.ToString());

                            Param = new OracleParameter("p_Lugar_Referencia", OracleDbType.Varchar2, 20);
                            Param.Value = Formateo_Texto(objEmitir.lugar_referencia);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "lugar_referencia: " + objEmitir.lugar_referencia.ToString());

                            Param = new OracleParameter("p_riesgosXml", OracleDbType.XmlType);///TRAMA XML
                            Param.Value = objEmitir.xml_riesgos; //Formateo_Xml(objEmitir.datos_riesgo_emite);
                            cmdOracle.Parameters.Add(Param);

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "xml_riesgos: " + objEmitir.xml_riesgos.ToString());

                            //INICIO, 17-12-2018 SE AGREGÁ PARÁMETROS NUEVOS AL SERVICIO
                            //Param = new OracleParameter("p_riesgosXmlExclusion", OracleDbType.XmlType);///TRAMA XML
                            //Param.Value = objEmitir.xml_riesgos_exclusion;
                            //cmdOracle.Parameters.Add(Param);
                            //FIN, 17-12-2018 SE AGREGÁ PARÁMETROS NUEVOS AL SERVICIO

                            Param = new OracleParameter("p_cod_sistema", OracleDbType.Varchar2,20);
                            Param.Value = Formateo_Texto(objEmitir.cod_sistema); 
                            cmdOracle.Parameters.Add(Param);

                            //JBOLIVAR
                            //PARAMETRO NUEVO 14-10-2019
                            Param = new OracleParameter("p_comentario", OracleDbType.Varchar2);
                            Param.Value = Formateo_Texto(objEmitir.observaciones);
                            cmdOracle.Parameters.Add(Param);
                            //END JBOLIVAR

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "cod_sistema: " + objEmitir.cod_sistema.ToString());

                        #endregion

                    #endregion

                    #region "Output  => 0 - 3"
                            Param = new OracleParameter("p_nRetorno", OracleDbType.Varchar2, 2000);
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
                            EscrbirLog(DateTime.Now.ToString() + "-- " + "abrimos conexión ");
                            Cnx.Open();
                            cmdOracle.Connection = Cnx;
                            dr = cmdOracle.ExecuteReader();

                            EscrbirLog(DateTime.Now.ToString() + "-- " + "ejecutamos cmdOracle.ExecuteReader()");

                            objEmiteResult.COD_ERROR = cmdOracle.Parameters["p_nRetorno"].Value.ToString();// +" error oracle";

                EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta oracle objEmiteResult.COD_ERROR " + objEmiteResult.COD_ERROR.ToString());
                 
                
                if (!Convert.IsDBNull(cmdOracle.Parameters["p_cMensaje"]))
                {
                    if (cmdOracle.Parameters["p_cMensaje"].Value.ToString() != "null")
                    {
                        objEmiteResult.DESC_ERROR = cmdOracle.Parameters["p_cMensaje"].Value.ToString();
                        EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta oracle objEmiteResult.DESC_ERROR " + objEmiteResult.DESC_ERROR.ToString());
                    }
                }                              


                 string flagError = string.Empty;
                 EscrbirLog(DateTime.Now.ToString() + "-- " + "ANTES DE FLAG ERROR : "    );
                 flagError = objEmiteResult.COD_ERROR.ToString().Trim();

                 if (flagError == "0")
                 {
                     EscrbirLog(DateTime.Now.ToString() + "-- " + "LUEGO CONDICIONAL  flagError: " + flagError);
                     int i = 1;

                     if (dr.HasRows)
                     {

                         while (dr.Read())
                         {

                             EscrbirLog(DateTime.Now.ToString() + ":::::::RETORNO OUTPUT ORACLE:::::::: ");
                             if (!Convert.IsDBNull(dr["Num_Poliza"]) && !Convert.IsDBNull(dr["num_cuota"]))  //me aseguro que devuelva data y no la fila null
                             {
                                 if (i == 1)
                                 {
                                     if (!Convert.IsDBNull(dr["Cod_cia"]))
                                         objEmiteResult.COD_CIA = Convert.ToInt32(dr["Cod_cia"].ToString());

                                     EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE " + objEmiteResult.COD_CIA.ToString());

                                     if (!Convert.IsDBNull(dr["Num_Poliza"]))
                                         objEmiteResult.NUM_POLIZA = dr["Num_Poliza"].ToString();

                                     EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE NUM_POLIZA " + objEmiteResult.NUM_POLIZA.ToString());

                                     if (!Convert.IsDBNull(dr["Num_Spto"]))
                                         objEmiteResult.NUM_SUP = Convert.ToInt32(dr["Num_Spto"].ToString());

                                     EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE NUM_SUP " + objEmiteResult.NUM_SUP.ToString());

                                     if (!Convert.IsDBNull(dr["Num_Apli"]))
                                         objEmiteResult.NUM_APLI = Convert.ToInt32(dr["Num_Apli"].ToString());

                                     EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE NUM_APLI " + objEmiteResult.NUM_APLI.ToString());

                                     if (!Convert.IsDBNull(dr["Num_Sup_Apli"]))
                                         objEmiteResult.NUM_SUP_APLI = Convert.ToInt32(dr["Num_Sup_Apli"].ToString());


                                     EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE NUM_SUP_APLI " + objEmiteResult.NUM_SUP_APLI.ToString());

                                     if (!Convert.IsDBNull(dr["Num_Cuotas"]))
                                         objEmiteResult.NUM_CUOTAS = Convert.ToInt32(dr["Num_Cuotas"].ToString());

                                     EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE NUM_CUOTAS " + objEmiteResult.NUM_CUOTAS.ToString());

                                     if (!Convert.IsDBNull(dr["Cod_Moneda"]))
                                         objEmiteResult.COD_MON = Convert.ToInt32(dr["Cod_Moneda"].ToString());

                                     EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE COD_MON " + objEmiteResult.COD_MON.ToString());


                                     if (!Convert.IsDBNull(dr["Des_Moneda"]))
                                         objEmiteResult.SIMBL_MONEDA = dr["Des_Moneda"].ToString();

                                     EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE SIMBL_MONEDA " + objEmiteResult.SIMBL_MONEDA.ToString());

                                     if (!Convert.IsDBNull(dr["Imp_neta"]))
                                         objEmiteResult.IMP_NETA = Convert.ToDouble(dr["Imp_neta"].ToString());

                                     EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE IMP_NETA " + objEmiteResult.IMP_NETA.ToString());

                                     if (!Convert.IsDBNull(dr["Imp_Derecho_Emision"]))
                                         objEmiteResult.IMP_DERECHO_EMISION = Convert.ToDouble(dr["Imp_Derecho_Emision"].ToString());

                                     EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE IMP_DERECHO_EMISION " + objEmiteResult.IMP_DERECHO_EMISION.ToString());

                                     if (!Convert.IsDBNull(dr["Imp_IGV"]))
                                         objEmiteResult.IMP_IGV = Convert.ToDouble(dr["Imp_IGV"].ToString());

                                     EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE IMP_IGV " + objEmiteResult.IMP_IGV.ToString());

                                     if (!Convert.IsDBNull(dr["Imp_Bonifi"]))
                                         objEmiteResult.IMP_BONIFI = Convert.ToDouble(dr["Imp_Bonifi"].ToString());

                                     EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE IMP_BONIFI " + objEmiteResult.IMP_BONIFI.ToString());

                                     if (!Convert.IsDBNull(dr["Imp_Interes"]))
                                         objEmiteResult.IMP_INTERES = Convert.ToDouble(dr["Imp_Interes"].ToString());

                                     EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE IMP_INTERES " + objEmiteResult.IMP_INTERES.ToString());

                                     if (!Convert.IsDBNull(dr["Imp_Neta_sin_bonifi"]))
                                         objEmiteResult.IMP_NETA_BONI = Convert.ToDouble(dr["Imp_Neta_sin_bonifi"].ToString());

                                     EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE IMP_NETA_BONI " + objEmiteResult.IMP_NETA_BONI.ToString());

                                     //RDIAZ - Inicio [24/10/2017]
                                     if (!Convert.IsDBNull(dr["Imp_Recibo_Total"]))
                                         objEmiteResult.PRI_BRUTA_TOTAL_CAL = Convert.ToDouble(dr["Imp_Recibo_Total"].ToString());
                                     //RDIAZ - Fin [24/10/2017]

                                     EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE PRI_BRUTA_TOTAL_CAL " + objEmiteResult.PRI_BRUTA_TOTAL_CAL.ToString());

                                     if (!Convert.IsDBNull(dr["COD_FRACC_PAGO"]))
                                         objEmiteResult.COD_FRAC_PAGO = Convert.ToInt32(dr["COD_FRACC_PAGO"].ToString());

                                     EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE COD_FRAC_PAGO " + objEmiteResult.COD_FRAC_PAGO.ToString());

                                     if (!Convert.IsDBNull(dr["MCA_PROVISIONAL"]))
                                         objEmiteResult.MCA_PROVISIONAL = dr["MCA_PROVISIONAL"].ToString();

                                     EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE MCA_PROVISIONAL " + objEmiteResult.MCA_PROVISIONAL.ToString());
                                 }
                                 EN_RECIBO objRecibo = new EN_RECIBO();
                                 if (!Convert.IsDBNull(dr["num_recibo"]))
                                     objRecibo.NUM_RECIBO = Convert.ToInt32(dr["num_recibo"].ToString());

                                 EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE NUM_RECIBO " + objRecibo.NUM_RECIBO.ToString());

                                 if (!Convert.IsDBNull(dr["num_cuota"]))
                                     objRecibo.NUM_CUOTA = Convert.ToInt32(dr["num_cuota"].ToString());

                                 EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE NUM_CUOTA " + objRecibo.NUM_CUOTA.ToString());

                                 if (!Convert.IsDBNull(dr["fec_efec_recibo"]))
                                     objRecibo.FEC_EFEC_RECIBO = Convert.ToDateTime(dr["fec_efec_recibo"].ToString()).ToString("dd/MM/yyyy");

                                 EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE NUM_CUOTA " + objRecibo.FEC_EFEC_RECIBO.ToString());

                                 if (!Convert.IsDBNull(dr["fec_vcto_recibo"]))
                                     objRecibo.FEC_VCTO_RECIBO = Convert.ToDateTime(dr["fec_vcto_recibo"].ToString()).ToString("dd/MM/yyyy");

                                 EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE FEC_VCTO_RECIBO " + objRecibo.FEC_VCTO_RECIBO.ToString());

                                 if (!Convert.IsDBNull(dr["Imp_recibo"]))
                                     objRecibo.IMP_RECIBO = Convert.ToDouble(dr["Imp_recibo"].ToString());

                                 EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE IMP_RECIBO " + objRecibo.IMP_RECIBO.ToString());

                                 objEmiteResult.RECIBOS.Add(objRecibo);
                                 i++;
                             }
                         }
                     }//RALMENDRAS - 06-02-2019, INICIO
                     else
                     {
                         EscrbirLog(DateTime.Now.ToString() + "INGRESO CONDICIONAL DISTINTO DE COD ERROR O ");
                         if (objEmiteResult.DESC_ERROR.ToString().Contains("Poliza:"))
                         { //obtenemos solo el num poliza
                             EscrbirLog(DateTime.Now.ToString() + "-- " + "respuesta OUTPUT  ORACLE CONTAINS Poliza: " + "TRUE");
                             objEmiteResult.NUM_POLIZA = objEmiteResult.DESC_ERROR.ToString().Substring(8, 13);
                             EscrbirLog(DateTime.Now.ToString() + "DESGROSANDO NUMERO POLIZA: " + objEmiteResult.NUM_POLIZA);
                         }
                     }
                     //RALMENDRAS - 06-02-2019, FIN
                 }  
                Log.Debug(" Fin acceso a datos Emision ");
            }
            catch (OracleException ex)
            {
                
                EscrbirLog(DateTime.Now.ToString() + "-- " + "OracleException " + ex.Message.ToString());

                Log.Error("OracleException, Error al conectar oracle EMITE " + ex.Message);
                //throw new Exception("Ocurrio un error inesperado." + "AD_COTIZACION:" + "ObtenerCotizacion");
                objEmiteResult.COD_ERROR = "990";
                objEmiteResult.DESC_ERROR = "OracleException: " + ex.Message + "Error inesperado #" + objEmiteResult.COD_ERROR + ". ";
            }
            catch (Exception ex)
            {
                EscrbirLog(DateTime.Now.ToString() + "-- " + "Exception " + ex.Message.ToString());

                Log.Error("Error acceso a datos EMITE: " + ex.Message);
                //throw new Exception("Ocurrio un error inesperado." + "AD_COTIZACION:" + "ObtenerCotizacion");
                objEmiteResult.COD_ERROR = "93";
                objEmiteResult.DESC_ERROR = "Exception: " + ex.Message + "Error inesperado #" + objEmiteResult.COD_ERROR + ". ";
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {                 
                dr.Close(); 
                dr.Dispose();   
                }
                if (Cnx.State.Equals(ConnectionState.Open))
                {
                    Cnx.Close();
                }
                //CloseOracleConnection();
                EscrbirLog("--------------------------------- FIN FLUJO -----------------------------------");
            }

            
            return objEmiteResult;
        }


        public void EscrbirLog (string texto){
          string path = ConfigurationManager.AppSettings["DirTemp"].ToString() + "log_salud.txt";             
          using (StreamWriter file = new StreamWriter(path, true))
          {
              file.WriteLine(texto); //se agrega información al documento
              file.Close();
          }

        }
    }
}

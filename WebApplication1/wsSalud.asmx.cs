using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using Entidades;
using LogicaNegocio;
using NLog;
using Utilitarios;

namespace wsSalud
{
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://wsSalud.pe/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class wsSalud : System.Web.Services.WebService
    {
        Logger Log = General.getLogger();
        [WebMethod]
        public EN_COTIZARESULT obtenerCotizacionSalud(string usuario, string password, int? cod_agt, string fec_efec_pol, string fec_vcto_pol, int? cod_mon, string mca_imp_mapfre_dolar, double?
            imp_mapfre_dolar, /*double? pct_dscto_comercial,*/ int? cod_frac_pago, int? cod_cia, int? cod_ramo, string num_poliza_grupo, int? cod_modalidad, int? num_contrato, int? num_sub_contrato,
            int? cod_producto, int? cod_sub_producto, string cod_plan, double? pct_ajus_int_red_plaza,
           List<riesgo_cotiza> datos_riesgo_cotiza
            //RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]
            )
        {
            EN_COTIZA objCotizar = new EN_COTIZA();
            EN_COTIZARESULT objCotizaResult = new EN_COTIZARESULT();
            String ln_poliza_grupo = String.Empty;
            string mensajeError = string.Empty;

            #region "Asignar valores"
            try
            {
                Log.Debug("usuario =" + usuario);
                objCotizar.usuario = usuario;
                Log.Debug("password =" + password);
                objCotizar.password = password;
                Log.Debug("cod_agt =" + cod_agt);
                objCotizar.cod_agt = cod_agt;
                Log.Debug("cod_mon =" + cod_mon);
                objCotizar.cod_mon = cod_mon;
                Log.Debug("mca_imp_mapfre_dolar =" + mca_imp_mapfre_dolar);
                objCotizar.mca_imp_mapfre_dolar = mca_imp_mapfre_dolar;
                Log.Debug("imp_mapfre_dolar =" + imp_mapfre_dolar);
                objCotizar.imp_mapfre_dolar = imp_mapfre_dolar;
                /*Log.Debug("pct_dscto_comercial =" + pct_dscto_comercial);
                objCotizar.pct_dscto_comercial = pct_dscto_comercial;*/
                Log.Debug("cod_frac_pago =" + cod_frac_pago);
                objCotizar.cod_frac_pago = cod_frac_pago == null ? 10001 : cod_frac_pago;
                Log.Debug("cod_cia =" + cod_cia);
                objCotizar.cod_cia = cod_cia;
                Log.Debug("cod_ramo =" + cod_ramo);
                objCotizar.cod_ramo = cod_ramo;
                Log.Debug("fec_efec_pol =" + fec_efec_pol);
                objCotizar.fec_efec_pol = fec_efec_pol;
                Log.Debug("fec_vcto_pol =" + fec_vcto_pol);
                objCotizar.fec_vcto_pol = fec_vcto_pol;
                Log.Debug("num_poliza_grupo =" + num_poliza_grupo);
                ln_poliza_grupo = (cod_ramo == null ? "" : cod_ramo.ToString()) + (num_contrato == null ? "" : num_contrato.ToString());
                objCotizar.num_poliza_grupo = ((num_poliza_grupo == null || num_poliza_grupo == "") ? ln_poliza_grupo : num_poliza_grupo);
                Log.Debug("cod_modalidad =" + cod_modalidad);
                objCotizar.cod_modalidad = cod_modalidad;
                Log.Debug("num_contrato =" + num_contrato);
                objCotizar.num_contrato = num_contrato;
                Log.Debug("num_sub_contrato =" + num_sub_contrato);
                objCotizar.num_sub_contrato = num_sub_contrato;
                //INICIO JHUARCAYA - HITSS - 02/02/2021
                Log.Debug("cod_producto =" + cod_producto);
                objCotizar.cod_producto = cod_producto;
                Log.Debug("cod_sub_producto =" + cod_sub_producto);
                objCotizar.cod_sub_producto = cod_sub_producto;
                Log.Debug("cod_plan =" + cod_plan);
                objCotizar.cod_plan = cod_plan;
                if (objCotizar.cod_ramo == 275)
                {
                    List<riesgo_cotiza> SortedList = datos_riesgo_cotiza.OrderBy(o => o.num_riesgo).ToList();
                    objCotizar.datos_riesgo_cotiza = SortedList;
                }
                else
                {
                    objCotizar.datos_riesgo_cotiza = datos_riesgo_cotiza;
                }
                //FIN JHUARCAYA - HITSS - 02/02/2021
                Log.Debug("pct_ajus_int_red_plaza =" + pct_ajus_int_red_plaza);
                objCotizar.pct_ajus_int_red_plaza = pct_ajus_int_red_plaza == null ? 0 : pct_ajus_int_red_plaza;

                objCotizar.mca_pct_int_red_plaza = pct_ajus_int_red_plaza == null || pct_ajus_int_red_plaza == 0 ? "N" : "S";
                Log.Debug("mca_pct_int_red_plaza =" + objCotizar.mca_pct_int_red_plaza); 
                 
                //objCotizar.datos_riesgo_cotiza = datos_riesgo_cotiza;

            }
            catch (Exception ex)
            {
                mensajeError = "Error al asignar variables: " + ex.Message;
                Log.Error("Error al asignar variables: " + ex.Message);
                //throw;
            }
            #endregion


            LN_ValidarCotizacionSalud objValidar = new LN_ValidarCotizacionSalud();

            if (objValidar.ValidarCotizacion(objCotizar, out mensajeError) || mensajeError.Length > 0)
            {
                objCotizaResult.COD_ERROR = 2;
                objCotizaResult.DESC_ERROR = mensajeError;
            }
            else
            {
                LN_COTIZACION oLNCotizacion = new LN_COTIZACION();
                objCotizaResult = oLNCotizacion.ObtenerCotizacion(objCotizar);
            }

            Log.Debug(" fin operacion de cotizacion ");
            return objCotizaResult;
        }

        [WebMethod]
        public EN_EMITERESULT obtenerEmisionSalud(string usuario, string password, string cod_sistema, int? cod_agt, int? cod_mon, string mca_imp_mapfre_dolar, double? imp_mapfre_dolar, string fec_efec_pol,
            string fec_vcto_pol, /*double? pct_dsc_comercial,*/ int? cod_frac_pago, int? cod_cia, int? cod_ramo, string num_poliza_grupo, int? cod_modalidad,
            int? num_contrato, int? num_sub_contrato, string mca_fisico_contratante, string tip_documento_contratante,  string cod_documento_contratante,
            string nombre_contratante, string ape_materno_contratante, string ape_paterno_contratante, string est_civil, string mca_sexo_cliente, string fec_nacimiento_cliente, string telf_fijo_contratante, string telf_movil_contratante,
            string mail_contrantante, string representante, int? cargo_representante, string tip_act_economico,
            int? profesion_cliente,  string cod_pais_cli, string cod_nacionalidad_cli,
            int? cod_estado_cli, int? cod_provincia_cli, int? cod_distrito_cli, int? tipo_domicilio,
            string domicilio_cliente, int? tip_num_cliente, string num_cliente, int? tip_interior, string interior_cliente, int? tip_zona_cliente, string zona_cliente, string lugar_referencia, string observaciones,
            int? cod_producto, int? cod_sub_producto, string cod_plan, List<riesgo_emite> datos_riesgo_emite
           )
        {
            //EN_COTIZA objCotizar = new EN_COTIZA();

            Log.Debug(" Inicio operacion de emision ");
            EN_EMITE objEmision = new EN_EMITE();
            EN_EMITERESULT objEmisionResult = new EN_EMITERESULT();
            String ln_poliza_grupo = String.Empty;
            //Utilitarios u = new Utilitarios;
          
            String mensajeError = "";
            #region "Asignar variables"
            LN_Emision LN = new LN_Emision();
            LN.EscrbirLog("--------------------------------- INICIO FLUJO -----------------------------------");
            try
            {
                Log.Debug("cod_sistema =" + cod_sistema);
                objEmision.cod_sistema = cod_sistema;
                Log.Debug("usuario =" + usuario);
                objEmision.usuario = usuario;
                Log.Debug(" password =" + password);
                objEmision.password = password;
                Log.Debug("cod_agt =" + cod_agt);
                objEmision.cod_agt = cod_agt;
                Log.Debug("cod_mon =" + cod_mon);
                objEmision.cod_mon = cod_mon;
                Log.Debug(" fec_efec_pol =" + fec_efec_pol);
                objEmision.fec_efec_pol = fec_efec_pol;
                Log.Debug(" fec_vcto_pol =" + fec_vcto_pol);
                objEmision.fec_vcto_pol = fec_vcto_pol;
                Log.Debug(" mca_imp_mapfre_dolar =" + mca_imp_mapfre_dolar);
                objEmision.mca_imp_mapfre_dolar = mca_imp_mapfre_dolar;
                Log.Debug(" imp_mapfre_dolar =" + imp_mapfre_dolar);
                objEmision.imp_mapfre_dolar = imp_mapfre_dolar;
                /*Log.Debug(" pct_dsc_comercial =" + pct_dsc_comercial);
                objEmision.pct_dscto_comercial = pct_dsc_comercial;*/
                Log.Debug(" cod_frac_pago =" + cod_frac_pago);
                objEmision.cod_frac_pago = cod_frac_pago;

                LN.EscrbirLog(DateTime.Now.ToString() + "1..cod_frac_pago: " + objEmision.cod_frac_pago);

                Log.Debug(" cod_cia =" + cod_cia);
                objEmision.cod_cia = cod_cia;
                Log.Debug(" cod_ramo =" + cod_ramo);
                objEmision.cod_ramo = cod_ramo;
                Log.Debug(" num_poliza_grupo =" + num_poliza_grupo);
                //objEmision.num_poliza_grupo = num_poliza_grupo;
                ln_poliza_grupo = (cod_ramo == null ? "" : cod_ramo.ToString()) + (num_contrato == null ? "" : num_contrato.ToString());
                objEmision.num_poliza_grupo = ((num_poliza_grupo == null || num_poliza_grupo == "") ? ln_poliza_grupo : num_poliza_grupo);
                Log.Debug(" cod_modalidad =" + cod_modalidad);
                objEmision.cod_modalidad = cod_modalidad;
                Log.Debug("num_contrato =" + num_contrato);
                objEmision.num_contrato = num_contrato;
                Log.Debug(" num_sub_contrato =" + num_sub_contrato);
                objEmision.num_sub_contrato = num_sub_contrato;
                Log.Debug(" mca_fisico_contratante =" + mca_fisico_contratante);
                objEmision.mca_fisico_contratante = mca_fisico_contratante;
                Log.Debug(" tip_documento_contratante =" + tip_documento_contratante);
                objEmision.tip_documento_contratante = tip_documento_contratante;
                Log.Debug(" cod_documento_contratante =" + cod_documento_contratante);
                objEmision.cod_documento_contratante = cod_documento_contratante;
                Log.Debug(" nombre_contratante =" + nombre_contratante);
                objEmision.nombre_contratante = nombre_contratante;
                Log.Debug(" ape_paterno_contratante =" + ape_paterno_contratante);
                objEmision.ape_paterno_contratante = ape_paterno_contratante;
                Log.Debug("telf_fijo_contratante=" + telf_fijo_contratante);
                objEmision.tel_fijo_contratante = telf_fijo_contratante;
                Log.Debug("telf_movil_contratante=" + telf_movil_contratante);
                objEmision.tel_movil_contratante = telf_movil_contratante;
                Log.Debug("ape_materno_contratante=" + ape_materno_contratante);
                objEmision.ape_materno_contratante = ape_materno_contratante;
                Log.Debug("mail_contrantante=" + mail_contrantante);
                objEmision.mail_contrantante = mail_contrantante;
                Log.Debug("representante=" + representante);
                objEmision.representante = representante;
                Log.Debug("cargo_representante=" + cargo_representante);
                objEmision.cargo_representante = cargo_representante;
                Log.Debug("tip_act_economico=" + tip_act_economico);
                objEmision.tip_economica = tip_act_economico;
                Log.Debug("fec_nacimiento_cliente=" + fec_nacimiento_cliente);
                objEmision.fec_nacimiento_cliente = fec_nacimiento_cliente;
                Log.Debug("mca_sexo_cliente=" + mca_sexo_cliente);
                objEmision.mca_sexo_cliente = mca_sexo_cliente;
                Log.Debug("profesion_cliente=" + profesion_cliente);
                objEmision.profesion_cliente = profesion_cliente;
                Log.Debug("est_civil=" + est_civil);
                objEmision.est_civil = est_civil;
                //Log.Debug("cod_est_civil=" + cod_est_civil);
                //objEmision.cod_est_civil = cod_est_civil;
                Log.Debug("cod_pais_cli=" + cod_pais_cli);
                objEmision.cod_pais = cod_pais_cli;
                Log.Debug("cod_nacionalidad_cli=" + cod_nacionalidad_cli);
                objEmision.cod_nacionalidad = cod_nacionalidad_cli;
                Log.Debug("cod_provincia_cli=" + cod_provincia_cli);
                objEmision.cod_provincia = cod_provincia_cli;
                Log.Debug("cod_distrito_cli=" + cod_distrito_cli);
                objEmision.cod_distrito = cod_distrito_cli;
                Log.Debug("tipo_domicilio=" + tipo_domicilio);
                objEmision.tipo_domicilio = tipo_domicilio;
                Log.Debug("domicilio_cliente=" + domicilio_cliente);
                objEmision.domicilio_cliente = domicilio_cliente;
                Log.Debug("tip_num_cliente=" + tip_num_cliente);
                objEmision.tip_numero_cliente = tip_num_cliente;
                Log.Debug("num_cliente=" + num_cliente);
                objEmision.num_cliente = num_cliente;
                Log.Debug("tip_interior=" + tip_interior);
                objEmision.tip_interior_cliente = tip_interior;
                Log.Debug("interior_cliente=" + interior_cliente);
                objEmision.interior_cliente = interior_cliente;
                Log.Debug("tip_zona_cliente=" + tip_zona_cliente);
                objEmision.tip_zona_cliente = tip_zona_cliente;
                Log.Debug("zona_cliente=" + zona_cliente);
                objEmision.zona_cliente = zona_cliente;
                Log.Debug("lugar_referencia=" + lugar_referencia);
                objEmision.lugar_referencia = lugar_referencia;
                Log.Debug("observaciones=" + observaciones);
                objEmision.observaciones = observaciones;
                //Log.Debug("mca_sexo_aseg=" + mca_sexo_aseg);
                //objEmision.mca_sexo_aseg = mca_sexo_aseg;
                Log.Debug("cod_estado_cli=" + cod_estado_cli);
                objEmision.cod_estado_cli = cod_estado_cli;
                //INICIO JHUARCAYA - HITSS - 02/02/2021
                if (objEmision.cod_ramo == 275)
                {
                    List<riesgo_emite> SortedList = datos_riesgo_emite.OrderBy(o => o.num_riesgo).ToList();
                    objEmision.datos_riesgo_emite = SortedList;
                }
                else
                {
                    objEmision.datos_riesgo_emite = datos_riesgo_emite;
                }
                Log.Debug("cod_producto =" + cod_producto);
                objEmision.cod_producto = cod_producto;
                Log.Debug("cod_sub_producto =" + cod_sub_producto);
                objEmision.cod_sub_producto = cod_sub_producto;
                Log.Debug("cod_plan =" + cod_plan);
                objEmision.cod_plan = cod_plan;
                //FIN JHUARCAYA - HITSS - 02/02/2021
            }
            catch (Exception ex)
            {
                mensajeError = "Error al asignar variables: " + ex.Message;
                Log.Error("Error al asignar variables: " + ex.Message);
            }

            #endregion

            LN_ValidarEmisionSalud objValidar = new LN_ValidarEmisionSalud();
          

            LN.EscrbirLog(DateTime.Now.ToString() + "ws salud.asmx, metodo: ValidarEmitir");
            if (objValidar.ValidarEmitir(objEmision, out mensajeError) || mensajeError.Length > 0)
            {
                objEmisionResult.COD_ERROR = "2";
                objEmisionResult.DESC_ERROR = mensajeError;

                

            }
            else
            {
                /*LN_COTIZACION oLNCotizacion = new LN_COTIZACION();
                objEmisionResult = oLNCotizacion.Emitir(objEmision);*/
                LN_Emision oLNCotizacion = new LN_Emision();

                LN.EscrbirLog(DateTime.Now.ToString() + "ws salud.asmx, metodo: oLNCotizacion.Emitir");
                objEmisionResult = oLNCotizacion.Emitir(objEmision);

                


            }
            Log.Debug(" fin operacion de emision ");
            return objEmisionResult;
        }

        #region "Solo para pruebas"

        private string pdfToSstringBase64(string file)
        {
            string AsBase64String = "";

            if (ValidarArchivo(file))
            {
                byte[] bytes = null;

                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    int numBytesToRead = Convert.ToInt32(fs.Length);
                    bytes = new byte[(numBytesToRead)];
                    fs.Read(bytes, 0, numBytesToRead);
                }

                AsBase64String = Convert.ToBase64String(bytes);

            }
            return AsBase64String;
        }

        protected bool ValidarArchivo(string file)
        {
            bool existe = false;
            if (System.IO.File.Exists(file))
            {
                existe = true;
            }

            return existe;
        }

        #endregion
    }


}

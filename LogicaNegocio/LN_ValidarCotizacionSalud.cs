using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades;
using System.Configuration;   //RDIAZ - [Ctrl. Cambio II - MultiRiesgo]

namespace LogicaNegocio
{
    public class LN_ValidarCotizacionSalud : LN_ValidarDatos
    {
        //Valido
        public bool ValidarCotizacion( EN_COTIZA objCotizar, out string mensaje ) {
                bool flagError = false;
                Int32 MaxLongXmlString = Convert.ToInt32(ConfigurationManager.AppSettings["MaxLongXmlString"].ToString());
                Int32 MaxLongXml = Convert.ToInt32(ConfigurationManager.AppSettings["MaxLongXml"].ToString());
                mensaje = "";
                
                ValidaNulos(objCotizar.usuario, "usuario", ref mensaje);
                ValidaNulos(objCotizar.password, "password", ref mensaje);
                ValidaAcceso(objCotizar.usuario, objCotizar.password, ref mensaje);
                ValidaValor(objCotizar.cod_agt, null, "cod_agt", ref mensaje);
                ValidaValor(objCotizar.cod_mon, null, "cod_mon", ref mensaje);
                ValidaValor(objCotizar.cod_cia, null, "cod_cia", ref mensaje);
                ValidaValor(objCotizar.cod_ramo, null, "cod_ramo", ref mensaje);
                ValidaNulos(objCotizar.cod_frac_pago, "cod_frac_pago", ref mensaje);
                ValidaNulos(objCotizar.cod_modalidad, "cod_modalidad", ref mensaje);
                ValidaFecha(objCotizar.fec_efec_pol, "fec_efec_pol", ref mensaje);
                ValidaFecha(objCotizar.fec_vcto_pol, "fec_vcto_pol", ref mensaje);
                //INICIO JHUARCAYA - HITSS - 02/02/2021
                if (objCotizar.cod_ramo == 275)
                {
                    ValidaValorTamaño(objCotizar.cod_producto, null, "cod_producto", ref mensaje);
                    ValidaValorTamaño(objCotizar.cod_sub_producto, null, "cod_sub_producto", ref mensaje);
                    ValidaNulosTamaño(objCotizar.cod_plan, "cod_plan", ref mensaje);
                }
                else
                {
                    ValidaNulos(objCotizar.num_contrato, "num_contrato", ref mensaje);
                    ValidaNulos(objCotizar.num_sub_contrato, "num_sub_contrato", ref mensaje);
                    ValidaMarca(objCotizar.mca_imp_mapfre_dolar, "mca_imp_mafre_dolar", ref mensaje);
                    ValidaValor(objCotizar.imp_mapfre_dolar, objCotizar.mca_imp_mapfre_dolar, "imp_mapfre_dolar", ref mensaje);
                    //ValidaValor(objCotizar.pct_dscto_comercial, null, "pct_dscto_comercial", ref mensaje,true);
                }
                //FIN JHUARCAYA - HITSS - 02/02/2021

            //RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]
            //***************  

            if (objCotizar.datos_riesgo_cotiza.Count == 0)
                {
                    if (mensaje == "") mensaje = "datos_riesgo_cotiza" + " incorrecto, no vacío.";
                    else mensaje = mensaje + "|" + "datos_riesgo_cotiza" + " incorrecto, no vacío.";
                }
          
                if (objCotizar.datos_riesgo_cotiza.Count > 0)
                {
                    int indice = 1;
                    foreach (riesgo_cotiza objRiesgo in objCotizar.datos_riesgo_cotiza)
                    {
                        string mensaje_riesgo = "";

                        //ValidaNulos(objRiesgo.mca_sexo, "mca_sexo", ref mensaje_riesgo);JHUARCAYA - HITSS - 02/02/2021
                        ValidaFecha(objRiesgo.fec_nacimiento, "fec_nacimiento", ref mensaje_riesgo);
                        ValidaNulos(objRiesgo.cod_parentesco, "cod_parentesco", ref mensaje_riesgo);

                        if (objCotizar.cod_ramo == 275)
                        {
                            if (indice == objRiesgo.num_riesgo)
                            {
                                ValidaValorTamaño(objRiesgo.num_riesgo, null, "num_riesgo", ref mensaje_riesgo);
                                ValidaNulosTamaño(objRiesgo.num_certificado, "num_certificado", ref mensaje_riesgo);
                                ValidaNulosTamaño(objRiesgo.desc_certificado, "desc_certificado", ref mensaje_riesgo);
                            }
                            else
                            {
                                mensaje_riesgo = "Falta el número de riesgo : " + Convert.ToString(indice);
                            }
                        }
                        else
                        {
                            ValidaNulos(objRiesgo.mca_sexo, "mca_sexo", ref mensaje_riesgo);
                            ValidaValor(objRiesgo.pct_dscto, null, "pct_dscto", ref mensaje_riesgo, true, false);
                            ValidaNulos(objRiesgo.val_estatura, "val_estatura", ref mensaje_riesgo);
                            ValidaNulos(objRiesgo.val_peso, "val_peso", ref mensaje_riesgo);
                            ValidaNulos(objRiesgo.mca_zurdo, "mca_zurdo", ref mensaje_riesgo);
                        }
                        
                        //ValidaNulos(objRiesgo.cod_ocupacion, "cod_ocupacion", ref mensaje_riesgo);
                        //ValidaNulos(objRiesgo.cod_especialidad, "cod_especialidad", ref mensaje_riesgo);
                        //ValidaNulos(objRiesgo.num_dependientes, "num_dependientes", ref mensaje_riesgo);

                        //ValidaNulos(objRiesgo.imp_pneta_mensual, "imp_pneta_mensual", ref mensaje_riesgo);
                    //RDIAZ - Inicio [Ctrl. Cambio II - MultiRiesgo]
                    if (mensaje_riesgo.Length > 0)
                        {
                            mensaje = mensaje + ". [ERRORES EN RIESGO #" + indice + "]: " + mensaje_riesgo;
                        }
                        indice++;
                    }
                }

                if (mensaje.Length > 0)
                {
                    flagError = true;
                }
                //RDIAZ - Inicio [Ctrl. Cambio II - MultiRiesgo]
                else
                {
                    objCotizar.xml_riesgos = Formateo_Xml(objCotizar.datos_riesgo_cotiza, "riesgo_cotiza");
                    if (objCotizar.xml_riesgos.Length > MaxLongXml && MaxLongXml > 0)
                    {
                        mensaje = mensaje + " datos_riesgo_cotiza excede longuitud máxima (" + objCotizar.xml_riesgos.Length + "/" + MaxLongXml + ").";
                        flagError = true;
                    }
                }
                //RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]


                return flagError;
        }
 
    }
}

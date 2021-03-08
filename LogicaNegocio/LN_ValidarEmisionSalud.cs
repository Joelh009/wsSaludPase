using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Entidades;
using System.Xml.Serialization;
using Utilitarios;

namespace LogicaNegocio
{
    public class LN_ValidarEmisionSalud : LN_ValidarDatos
    {
        public bool ValidarEmitir(EN_EMITE objEmitir, out string mensaje)
        {
            bool flagError = false;
            Int32 MaxLongXmlString = Convert.ToInt32(ConfigurationManager.AppSettings["MaxLongXmlString"].ToString());
            Int32 MaxLongXml = Convert.ToInt32(ConfigurationManager.AppSettings["MaxLongXml"].ToString());
            mensaje = "";
            ValidaNulos(objEmitir.usuario, "usuario", ref mensaje);
            ValidaNulos(objEmitir.password, "password", ref mensaje);
            ValidaAcceso(objEmitir.usuario, objEmitir.password, ref mensaje);
            ValidaValor(objEmitir.cod_agt, null, "cod_agt", ref mensaje, false, false);
            ValidaValor(objEmitir.cod_mon, null, "cod_mon", ref mensaje);
                
            //ValidaValor(objEmitir.pct_dscto_comercial, null, "pct_dscto", ref mensaje, true, false);
            //ValidaNulos(objEmitir.cod_frac_pago, "cod_frac_pago", ref mensaje);
            ValidaValor(objEmitir.cod_cia, null, "cod_cia", ref mensaje);
            ValidaValor(objEmitir.cod_ramo, null, "cod_ramo", ref mensaje);
            ValidaNulos(objEmitir.cod_modalidad, "cod_modalidad", ref mensaje);
                
            ValidaMarca(objEmitir.mca_fisico_contratante, "mca_fisico_contratante", ref mensaje);
            ValidaNulos(objEmitir.tip_documento_contratante, "tip_documento_contratante", ref mensaje);
            ValidaNulos(objEmitir.cod_documento_contratante, "cod_documento_contratante", ref mensaje);
            ValidaNulos(objEmitir.nombre_contratante, "nombre_contratante", ref mensaje);
            ValidaFecha(objEmitir.fec_efec_pol, "fec_efec_pol", ref mensaje);

            ValidaFecha(objEmitir.fec_vcto_pol, "fec_vcto_pol", ref mensaje);
            ValidaNulos(objEmitir.mail_contrantante, "mail_contrantante", ref mensaje);
            ValidaNulos(objEmitir.tel_fijo_contratante, "tel_fijo_contratante", ref mensaje);
            ValidaNulos(objEmitir.tel_movil_contratante, "tel_movil_contratante", ref mensaje);
            if (objEmitir.cod_ramo == 275)
            {
                ValidaValorTamaño(objEmitir.cod_producto, null , "cod_producto", ref mensaje);
                ValidaValorTamaño(objEmitir.cod_sub_producto, null, "cod_sub_producto", ref mensaje);
                ValidaNulosTamaño(objEmitir.cod_plan, "cod_plan", ref mensaje);
            }
            else
            {
                ValidaMarca(objEmitir.mca_imp_mapfre_dolar, "mca_imp_mapfre_dolar", ref mensaje);
                ValidaValor(objEmitir.imp_mapfre_dolar, objEmitir.mca_imp_mapfre_dolar, "imp_mapfre_dolar", ref mensaje, false, false);
                ValidaNulos(objEmitir.num_contrato, "num_contrato", ref mensaje);
                ValidaNulos(objEmitir.num_sub_contrato, "num_sub_contrato", ref mensaje);
            }

            if (objEmitir.mca_fisico_contratante.Equals("N"))
            {
                ValidaNulos(objEmitir.representante, "representante", ref mensaje);
                ValidaNulos(objEmitir.cargo_representante, "cargo_representante", ref mensaje);
                ValidaNulos(objEmitir.tip_economica, "tip_economica", ref mensaje);
          
            }
            if (objEmitir.mca_fisico_contratante.Equals("S"))
            {
                ValidaNulos(objEmitir.ape_paterno_contratante, "ape_paterno_contratante", ref mensaje);
                ValidaNulos(objEmitir.ape_materno_contratante, "ape_materno_contratante", ref mensaje);
                ValidaNulos(objEmitir.fec_nacimiento_cliente, "fec_nacimiento_cliente", ref mensaje);
                ValidaNulos(objEmitir.mca_sexo_cliente, "mca_sexo_cliente", ref mensaje);
                ValidaNulos(objEmitir.profesion_cliente, "profesion_cliente", ref mensaje);
                ValidaNulos(objEmitir.est_civil, "est_civil", ref mensaje);
               
            }

            //cargo_representante
            ValidaNulos(objEmitir.cod_pais, "cod_pais", ref mensaje);
            //ValidaNulos(objEmitir.cod_estado_cli, "cod_estado_cli", ref mensaje);
            ValidaNulos(objEmitir.cod_provincia, "cod_provincia", ref mensaje);
            ValidaNulos(objEmitir.cod_distrito, "cod_distrito", ref mensaje);

            ValidaNulos(objEmitir.tipo_domicilio, "tipo_domicilio", ref mensaje);
            ValidaNulos(objEmitir.domicilio_cliente,  "domicilio_cliente", ref mensaje);
            ValidaNulos(objEmitir.tip_numero_cliente, "tip_numero_cliente", ref mensaje);
            ValidaNulos(objEmitir.num_cliente, "num_cliente", ref mensaje);

            if (objEmitir.datos_riesgo_emite.Count > 0)
            {
                int indice = 1;
                foreach (riesgo_emite objRiesgo in objEmitir.datos_riesgo_emite)
                {
                    string mensaje_riesgo = "";
                    //objRiesgo.indice = indice;
                        
                    ValidaNulos(objRiesgo.cod_parentesco, "cod_parentesco", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.tip_persona, "tip_persona", ref mensaje_riesgo);

                    ValidaNulos(objRiesgo.tip_docum, "tip_docum", ref mensaje_riesgo);
                    ValidaNulos(objRiesgo.cod_docum, "cod_docum", ref mensaje_riesgo);

                    //ValidaNulos(objRiesgo.est_civil, "est_civil", ref mensaje_riesgo);
                    //ValidaMarca(objRiesgo.cod_est_civil, "cod_est_civil", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.mca_sexo, "mca_sexo", ref mensaje_riesgo);JHUARCAYA - HITSS - 02/02/2021
                    ValidaFecha(objRiesgo.fec_nacimiento, "fec_nacimiento", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.cod_ocupacion, "cod_ocupacion", ref mensaje_riesgo);
                    //INICIO JHUARCAYA - HITSS - 02/02/2021
                    if (objEmitir.cod_ramo == 275)
                    {   
                        if(indice == objRiesgo.num_riesgo)
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
                        ValidaNulos(objRiesgo.nombre, "nombre", ref mensaje_riesgo);
                        ValidaNulos(objRiesgo.ape_paterno, "ape_paterno", ref mensaje_riesgo);
                        ValidaNulos(objRiesgo.ape_materno, "ape_materno", ref mensaje_riesgo);
                        ValidaNulos(objRiesgo.val_peso, "val_peso", ref mensaje_riesgo);
                        ValidaNulos(objRiesgo.val_estatura, "val_estatura", ref mensaje_riesgo);
                        ValidaNulos(objRiesgo.mca_zurdo, "mca_zurdo", ref mensaje_riesgo);
                    }
                    //FIN JHUARCAYA - HITSS - 02/02/2021
                    //ValidaNulos(objRiesgo.cod_especialidad,  "cod_especialidad", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.texto_beneficiario, "texto_beneficiario", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.num_dependientes, "num_dependientes", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.num_poliza_aseg_ant, "num_poliza_aseg_ant", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.nom_aseg_ant, "nom_aseg_ant", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.fec_efec_pol_ant, "fec_efec_pol_ant", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.fec_vcto_pol_ant, "fec_vcto_pol_ant", ref mensaje_riesgo);

                    //ValidaNulos(objRiesgo.imp_pneta_mensual, "imp_pneta_mensual", ref mensaje_riesgo);

                    //RALMENDRAS- INICIO 20/12/2018, los parámetros nuevos serán opcionales     


                    //ValidaNulos(objRiesgo.mca_continuidad_seg, "mca_continuidad_seg", ref mensaje_riesgo);
                    //ValidaValor(objRiesgo.cod_motivo_dscto_recargo, null, "cod_motivo_dscto_recargo", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.des_motivo_dscto_recargo, "des_motivo_dscto_recargo", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.mca_continuidad_maternidad, "mca_continuidad_maternidad", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.val_carencia_maternidad, "val_carencia_maternidad", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.mca_continuidad_enfcong, "mca_continuidad_enfcong", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.val_carencia_enfcongnts, "val_carencia_enfcongnts", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.mca_continuidad_oncologico, "mca_continuidad_oncologico", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.val_carencia_oncologico, "val_carencia_oncologico", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.mca_continuidad_transplnorg, "mca_continuidad_transplnorg", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.val_carencia_transplnorg, "val_carencia_transplnorg", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.mca_continuidad_nino_sano, "mca_continuidad_nino_sano", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.val_carencia_nino_sano, "val_carencia_nino_sano", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.mca_exclusion, "mca_exclusion", ref mensaje_riesgo);
                    //ValidaNulos(objRiesgo.mca_observaciones ,"mca_observaciones", ref mensaje_riesgo);


                    //if (objRiesgo.datos_riesgo_exclusion.Count > 0)
                    //{
                    //    int indicex = 1;
                    //    foreach (riesgo_exclusion objRiesgoE in objRiesgo.datos_riesgo_exclusion)
                    //    {
                    //        string mensaje_exclusion = "";

                    //        ValidaNulos(objRiesgoE.cod_exclusion, "cod_exclusion", ref mensaje_exclusion);
                    //        ValidaNulos(objRiesgoE.obs_exclusion, "obs_exclusion", ref mensaje_exclusion);

                    //        if (mensaje_exclusion.Length > 0)
                    //        {
                    //            mensaje = mensaje + ". [ERRORES EN RIESGO EXCLUSION #" + indice + "]: " + mensaje_exclusion;
                    //        }

                    //        indicex++;
                    //    }
                    //}

                    //RALMENDRAS- FIN 20/12/2018, los parámetros nuevos serán opcionales      
                    if (mensaje_riesgo.Length > 0)
                    {
                        mensaje = mensaje + ". [ERRORES EN RIESGO #"+indice+"]: "+ mensaje_riesgo;
                    }

                    indice++;
                }
            }
            //RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]
                  

            if (mensaje.Length > 0)
            {
                flagError = true;
            }
            //RDIAZ - Inicio [Ctrl. Cambio II - MultiRiesgo]
            else
            {
                objEmitir.xml_riesgos = Formateo_Xml(objEmitir.datos_riesgo_emite, "riesgo_emite");
                if (objEmitir.xml_riesgos.Length > MaxLongXml && MaxLongXml > 0)
                {
                    mensaje = mensaje + " datos_riesgo_emite excede longuitud máxima (" + objEmitir.xml_riesgos.Length + "/" + MaxLongXml + ").";
                    flagError = true;
                }
            }
            //RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]
            return flagError;
        }
         

        public string TramaEnXML(string tramaBase64, int? num_registros, string nombre, ref string mensaje)
        {
            DataTable table = new DataTable();
            string dataXML =string.Empty;
            string rutaCompleta = string.Empty;

            rutaCompleta = StringBase64ToExcel(nombre, tramaBase64);
            
            try
            {
                table = General.LeerExcel(rutaCompleta);
                table.TableName = "PERSONA";

                if (nombre.Equals("trama_aseg_VI"))
                    ValidarCampos_trama_aseg_VI(table, nombre, ref   mensaje);

                if (nombre.Equals("trama_aseg_VII"))
                    ValidarCampos_trama_aseg_VII(table, nombre, ref   mensaje);

                int filas = table.Rows.Count;
                if (filas.Equals(num_registros))
                {
                    DataSet ds = new DataSet();
                    ds.Tables.Add(table);
                    ds.DataSetName = "DATOS_PERSONA";
                    dataXML = ds.GetXml();
                }
                else if (filas > num_registros)
                {
                    if (mensaje == "") mensaje = nombre + "los registros de la trama es mayo ral total de registros indicados";
                    else mensaje = mensaje + "|" + nombre + "los registros de la trama es mayo ral total de registros indicados";
                }
                else if (filas < num_registros)
                {
                    if (mensaje == "") mensaje = nombre + "el total de registros indicados es mayor a los registros de la trama";
                    else mensaje = mensaje + "|" + nombre + "el total de registros indicados es mayor a los registros de la trama";
                }
            }
            catch (Exception )
            {
                if (mensaje == "") mensaje = nombre + "error al obtener datos de la trama excel";
                else mensaje = mensaje + "|" + nombre + "error al obtener datos de la trama excel";
            }
            finally {
                if (File.Exists(rutaCompleta))
                {
                    File.Delete(rutaCompleta);
                }
            }

            //RDIAZ - Inicio [Ctrl. Cambio II - MultiRiesgo]
            //return dataXML;
            return dataXML.Replace("\r\n    ", "").Replace("\r\n   ", "").Replace("\r\n  ", "").Replace("\r\n ", "").Replace("\r\n","");
            //RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]
         }

        public string StringBase64ToExcel(string NombreTrama, string TramaExcel64Bytes)
        {
            string cRutaCargarTramas =  ConfigurationManager.AppSettings["RutaCargarTramas"].ToString();

            String filename = "NombreTrama" + "_" + System.DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "").Replace(".", "") + ".xls";
            String rutaCompleta = cRutaCargarTramas + filename;

            if (File.Exists(rutaCompleta))
            {
                File.Delete(rutaCompleta);
            }
            using (FileStream stream = System.IO.File.Create(rutaCompleta))
            {
                byte[] bytes = Convert.FromBase64String(TramaExcel64Bytes);
                stream.Write(bytes, 0, bytes.Length);
            }
            return rutaCompleta;
        }

        private void ValidarCampos_trama_aseg_VI(DataTable dt,string NombreTrama,  ref string mensaje)
        {
            string[] namecolumnlog = new string[] { "TIP_DOCUM_ASEG", "COD_DOCUM_ASEG", "TIP_DOCUM_BENEF", "COD_DOCUM_BENEF", "PCT_PART_BENEF" };
            string nombre_columna = string.Empty;
            foreach (string co in namecolumnlog)
            {
                if (!dt.Columns.Contains(co))
                {
                    nombre_columna += co + ",";
                }
            }

            if (!string.IsNullOrEmpty(nombre_columna))
            {
                mensaje = mensaje + "| formato " + NombreTrama + "  incorrecto, el excel debe contener las columnas: " + nombre_columna;
            }
            else
            {
                string TIP_DOCUM_ASEG;
                string COD_DOCUM_ASEG;
                string TIP_DOCUM_BENEF;
                string COD_DOCUM_BENEF;
                string PCT_PART_BENEF; 

                int fila = 1;

                foreach (DataRow item in dt.Rows)
                {
                    TIP_DOCUM_ASEG = Convert.ToString(item["TIP_DOCUM_ASEG"]).Trim();
                    COD_DOCUM_ASEG = Convert.ToString(item["COD_DOCUM_ASEG"]).Trim();
                    TIP_DOCUM_BENEF = Convert.ToString(item["TIP_DOCUM_BENEF"]).Trim();
                    COD_DOCUM_BENEF = Convert.ToString(item["COD_DOCUM_BENEF"]).Trim();
                    PCT_PART_BENEF = Convert.ToString(item["PCT_PART_BENEF"]).Trim();

                    if (TIP_DOCUM_ASEG.Equals("RUC"))
                    {
                        if (!COD_DOCUM_ASEG.Length.Equals(11)) 
                        {
                            mensaje = mensaje + "| " + " Trama " + NombreTrama + " - COD_DOCUM_ASEG incorrecto en fila" + fila.ToString() + ", debe tener 8 digitos";
                        }
                    }
                    else if (TIP_DOCUM_ASEG.Equals("DNI"))
                    {
                        if (!COD_DOCUM_ASEG.Length.Equals(8))
                        {
                            mensaje = mensaje + "| " + " Trama " + NombreTrama + " - COD_DOCUM_ASEG incorrecto en fila" + fila.ToString() + ", debe tener 11 digitos";
                        }
                    }

                    if (TIP_DOCUM_BENEF.Equals("RUC"))
                    {
                        if (!COD_DOCUM_BENEF.Length.Equals(11))
                        {
                            mensaje = mensaje + "| " + " Trama " + NombreTrama + " - COD_DOCUM_ASEG incorrecto en fila" + fila.ToString() + ", debe tener 8 digitos";
                        }
                    }
                    else if (TIP_DOCUM_BENEF.Equals("DNI"))
                    {
                        if (!COD_DOCUM_BENEF.Length.Equals(8))
                        {
                            mensaje = mensaje + "| " + " Trama " + NombreTrama + " - COD_DOCUM_ASEG incorrecto en fila" + fila.ToString() + ", debe tener 11 digitos";
                        }
                    }
                     
                        var regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
                        if (!regex.IsMatch(PCT_PART_BENEF))
                        {
                            mensaje = mensaje + "| " + " Trama " + NombreTrama + " - PCT_PART_BENEF incorrecto en fila" + fila.ToString() + ", debe ser un numero positivo y separador decimal un punto(.) ";                            
                        } 

                    fila = 1+fila;  
                }
            }
        }

        private void ValidarCampos_trama_aseg_VII(DataTable dt, string NombreTrama, ref string mensaje)
        {
            string[] namecolumnlog = new string[] { "NOM_PERSONA_CUBRIR", "CARGO_PERSONA_CUBRIR" };
            string nombre_columna = string.Empty;
            foreach (string co in namecolumnlog)
            {
                if (!dt.Columns.Contains(co))
                {
                    nombre_columna += co + ",";
                }
            }

            if (!string.IsNullOrEmpty(nombre_columna))
            {
                mensaje = mensaje + "| formato " + NombreTrama + "  incorrecto, el excel debe contener las columnas: " + nombre_columna;
            } 
        }

    }
}

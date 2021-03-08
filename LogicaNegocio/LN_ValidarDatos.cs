using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Entidades;
//RDIAZ - Inicio [Ctrl. Cambio II - MultiRiesgo]
using System.Xml.Serialization;
using System.Configuration;
using System.IO;
using System.Data;
//RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]

namespace LogicaNegocio
{
   public  class LN_ValidarDatos
    {
        
        protected void ValidaNulos(object campo, string nombre, ref string mensaje)
        {
            if (campo == null || campo == "")
            {
                if (mensaje == "") mensaje = nombre + " incorrecto, no nulo.";
                else mensaje = mensaje + "|" + nombre + " incorrecto, no nulo.";
            }
        }

        protected void ValidaNulosxxx(object campo, string nombre, ref string mensaje)
        {
            if (campo == null || campo == "")
            {
                if (mensaje == "") mensaje = nombre + " incorrecto, no nulo.";
                else mensaje = mensaje + "|" + nombre + " incorrecto, no nulo.";
            }
        }

        public void ValidaAcceso(string usuario, string password, ref string mensaje)
        {
            EN_USUARIO objusuario = new EN_USUARIO();
            LN_COTIZACION objcotizacion = new LN_COTIZACION();
            objusuario = objcotizacion.Recupera_usuario(usuario, password);

            if (objusuario.COD_ERROR != null)
            {
                mensaje = mensaje + objusuario.DESC_ERROR;
            }
            else
            {
                if (objusuario.cantidad != 1)
                    mensaje = mensaje + " usuario y pasword incorrectos. ";
            }
        }

        public void ValidaValor(double? valor, string marca, string nombre, ref string mensaje, bool allowZero = false, bool allowNullValor = false)
        {

            if ((marca != null && marca == "S") || marca == null)
            {
                if (allowNullValor == false)
                    ValidaNulos(valor, nombre, ref mensaje);

                if (valor != null)
                {
                    if (valor <= 0 && allowZero == false)
                    {
                        if (mensaje == "") mensaje = nombre + " incorrecto, solo admite numérico mayor a 0.";
                        else mensaje = mensaje + "|" + nombre + " incorrecto, solo admite numérico mayor a 0.";
                    }
                    else
                    {
                        if (valor < 0 && allowZero == true)
                        {
                            if (mensaje == "") mensaje = nombre + " incorrecto, solo admite numérico mayor o igual a 0.";
                            else mensaje = mensaje + "|" + nombre + " incorrecto, solo admite numérico mayor o igual a 0.";
                        }
                    }
                }

            }
        }

        //INICIO JHUARCAYA - HITSS - 02/02/2021
        public void ValidaValorTamaño(double? valor, string marca, string nombre, ref string mensaje, bool allowZero = false, bool allowNullValor = false)
        {

            if ((marca != null && marca == "S") || marca == null)
            {
                if (allowNullValor == false)
                    ValidaNulos(valor, nombre, ref mensaje);

                if (valor != null)
                {
                    if (valor <= 0 && allowZero == false)
                    {
                        if (mensaje == "") mensaje = nombre + " incorrecto, solo admite numérico mayor a 0.";
                        else mensaje = mensaje + "|" + nombre + " incorrecto, solo admite numérico mayor a 0.";
                    }
                    else
                    {
                        if (valor < 0 && allowZero == true)
                        {
                            if (mensaje == "") mensaje = nombre + " incorrecto, solo admite numérico mayor o igual a 0.";
                            else mensaje = mensaje + "|" + nombre + " incorrecto, solo admite numérico mayor o igual a 0.";
                        }
                    }
                    if (Convert.ToString(valor).Length > Convert.ToInt32(ConfigurationManager.AppSettings["max" + nombre].ToString()))
                    {
                        if (mensaje == "") mensaje = nombre + " incorrecto, la cantidad máxima de dígitos para el parámetro es " + Convert.ToInt32(ConfigurationManager.AppSettings["max" + nombre].ToString());
                        else mensaje = mensaje + "|" + nombre + " incorrecto, la cantidad máxima de dígitos para el parámetro es " + Convert.ToInt32(ConfigurationManager.AppSettings["max" + nombre].ToString());
                    }
                }
            }
        }

        protected void ValidaNulosTamaño(object campo, string nombre, ref string mensaje)
        {
            if (campo == null || campo == "")
            {
                if (mensaje == "") mensaje = nombre + " incorrecto, no nulo.";
                else mensaje = mensaje + "|" + nombre + " incorrecto, no nulo.";
            }
            if (Convert.ToString(campo).Length > Convert.ToInt32(ConfigurationManager.AppSettings["max" + nombre].ToString()))
            {
                if (mensaje == "") mensaje = nombre + " incorrecto, la cantidad máxima de dígitos para el parámetro es " + Convert.ToInt32(ConfigurationManager.AppSettings["max" + nombre].ToString());
                else mensaje = mensaje + "|" + nombre + " incorrecto, la cantidad máxima de dígitos para el parámetro es " + Convert.ToInt32(ConfigurationManager.AppSettings["max" + nombre].ToString());
            }
        }
        //FIN JHUARCAYA - HITSS - 02/02/2021
        public void ValidaFecha(string fecha, string nombre, ref string mensaje)
        {
            if (fecha == null || fecha.ToString().Trim().Equals(""))
            {
                if (mensaje == "") mensaje = nombre + " incorrecto, no nulo.";
                else mensaje = mensaje + "|" + nombre + " incorrecto, no nulo.";
            }
            else
            {

                String[] formats = { "dd/MM/yyyy" };
                DateTime dateValue;
                String str_fecha = fecha.ToString().Trim();
                System.IFormatProvider miFp = new System.Globalization.CultureInfo("es-ES", false);
                try
                {
                    if (!System.DateTime.TryParseExact(str_fecha, formats, miFp, DateTimeStyles.None, out dateValue))
                    {
                        if (mensaje == "") mensaje = nombre + " incorrecto, formato dd/mm/yyyy .";
                        else mensaje = mensaje + "|" + nombre + " incorrecto, formato dd/mm/yyyy .";
                    }
                    else
                    {
                        if (Convert.ToDateTime(fecha.ToString().Trim()).Year < 1900)
                        {
                            if (mensaje == "") mensaje = nombre + " incorrecto, año debe ser mayor a 1900.";
                            else mensaje = mensaje + "|" + nombre + " incorrecto, año debe ser mayor a 1900.";
                        }
                    }
                }
                catch (Exception)
                {
                    if (mensaje == "") mensaje = nombre + " incorrecto.";
                    else mensaje = mensaje + "|" + nombre + " incorrecto.";
                }

                
            }
        }

        public void ValidaMarca(string marca, string nombre, ref string mensaje)
        {
            ValidaNulos(marca, nombre, ref mensaje);
            if (marca != null && marca != string.Empty)
            {
                if (!("SN".Contains(marca)))
                {
                    if (mensaje == "") mensaje = nombre + " incorrecto, solo se admite valores de S o N.";
                    else mensaje = mensaje + "|" + nombre + " incorrecto, solo se admite valores de S o N.";
                }
            }
        }

        
       public void ValidarNullOrNumero(double? valor, string marca, string nombre, ref string mensaje, bool allowZero = false, bool allowNullValor = false)
        {

        }

       //RDIAZ - Inicio [Ctrl. Cambio II - MultiRiesgo]
       public string Formateo_Xml(object lstEntidad, string entidad)
       {
           MemoryStream ms = new MemoryStream();
           StreamWriter writer = new StreamWriter(ms);
           string xml = "";
           try
           {
               if (lstEntidad != null)
               {
                   entidad = entidad.ToLower();
                   XmlSerializer serializer = new XmlSerializer(lstEntidad.GetType());

                   // StreamWriter("formateo.xml");
                   serializer.Serialize(writer, lstEntidad);
                   StreamReader reader = new StreamReader(writer.BaseStream, Encoding.UTF8, true);
                   writer.BaseStream.Seek(0, SeekOrigin.Begin);
                   /*Type tipo = typeof(string);
                   try
                   {
                       tipo = lstEntidad.GetType().GetProperties()[lstEntidad.GetType().GetProperties().Count() - 1].PropertyType;  
                   }
                   catch (Exception ee)
                   {
                       tipo = typeof(string);
                   }
                    Log.Error(" Formateo_Xml - entidad: " + tipo.Name);
                    */

                   if (lstEntidad.GetType().Name.ToLower().Contains("list"))
                   {
                       //string entidad = tipo.Name; //.Substring(0,1).ToUpper()+ tipo.Name.Substring(1, tipo.Name.Length-1);
                       string entidadPrimeraMayuscula = entidad.Substring(0, 1).ToUpper() + entidad.Substring(1, entidad.Length - 1);
                       xml = reader.ReadToEnd();
                       while (xml.Contains("ArrayOf" + entidadPrimeraMayuscula) || xml.Contains(entidad))
                       {
                           xml = xml.Replace("ArrayOf" + entidadPrimeraMayuscula, "ROOT").Replace(entidad, "OBJECT");
                       }
                   }
                   else
                   {
                       xml = reader.ReadToEnd();
                   }
                   xml = xml.Replace("\r\n    ", "")
                            .Replace("\r\n   ", "")
                            .Replace("\r\n  ", "")
                            .Replace("\r\n ", "")
                            .Replace("\r\n", "")
                            .Replace("&lt;", "<")
                            .Replace("&gt;", ">");

                   //Log.Error(" Formateo_Xml - xml: " + xml);
                   return xml;
               }
               else
               {
                   return xml;
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {
               writer.Close();
               writer.Dispose();
               ms.Close();
               ms.Dispose();
           }
       }
        //RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]
    }
}

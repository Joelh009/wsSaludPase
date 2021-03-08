//RDIAZ - Inicio [Ctrl. Cambio II - MultiRiesgo]
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Xml.Serialization;
using System.IO;
using Entidades;
//using System.Data.OracleClient;


namespace AccesoDatos
{
    public class AD_BaseAccess
    {

        #region Metodos Formateo
        //  <summary>
        //  Formateo Dato Decimal
        //  </summary>
        //  <param name="objValor">Dato a Formatear</param>
        //  <returns></returns>
        public object Formateo_Numero(object ObjValor)
        {
            //if (ObjValor != null)
            //{
            //    ObjValor = Convert.ToInt32(ObjValor);
            //}

            if (((ObjValor != null) && !String.IsNullOrEmpty(ObjValor.ToString())
                        && !(ObjValor.Equals(0) || ObjValor.Equals(0.0))))
            {
                try { Convert.ToDouble(ObjValor); return ObjValor; }
                catch { return DBNull.Value; }
            }
            else
            {
                return DBNull.Value;
            }
        }

        //  <summary>
        //  Formateo Dato Cadena 
        //  </summary>
        //  <param name="objValor">Dato a Formatear</param>
        //  <returns></returns>
        public object Formateo_Texto(string ObjValor)
        {
            if (((ObjValor != null)
                        && (ObjValor != String.Empty)))
            {
                return ObjValor;
            }
            else
            {
                return DBNull.Value;
            }
        }

        //  <summary>
        //  Formateo Dato Fecha
        //  </summary>
        //  <param name="objValor">Dato a Formatear</param>
        //  <returns></returns>
        public object Formateo_Fecha(object ObjValor)
        {
            if (ObjValor != null && ObjValor!= String.Empty)
            {
                    var anio = Convert.ToDateTime(ObjValor).Year;
                    if (anio == 1)
                        return DBNull.Value;
                    else
                        return Convert.ToDateTime(ObjValor);
            }
            else
            {
                return DBNull.Value;
            }
        }

        public object Formateo_NumericoNull(object ObjValor)
        {

            if (ObjValor != null)
            {
                try { Convert.ToDouble(ObjValor); return ObjValor; }
                catch { return DBNull.Value; }
            }
            else
            {
                return DBNull.Value;
            }
        }

        public object Formateo_EnteroNull(object ObjValor)
        {

            if (ObjValor != null)
            {
                try { Convert.ToInt32(ObjValor); return ObjValor; }
                catch { return DBNull.Value; }
            }
            else
            {
                return DBNull.Value;
            }
        }

        //public string Formateo_Xml(object lstEntidad)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    StreamWriter writer = new StreamWriter(ms);
        //    string xml = "";
        //    try
        //    {
        //        if (lstEntidad != null)
        //        {
        //            XmlSerializer serializer = new XmlSerializer(lstEntidad.GetType());
                    
        //             // StreamWriter("formateo.xml");
        //            serializer.Serialize(writer, lstEntidad);
        //            StreamReader reader = new StreamReader(writer.BaseStream, Encoding.UTF8, true);
        //            writer.BaseStream.Seek(0, SeekOrigin.Begin);
        //            Type tipo = typeof(string);
        //            try
        //            {
        //                tipo = lstEntidad.GetType().GetProperties()[lstEntidad.GetType().GetProperties().Count() - 1].PropertyType;
        //            }catch(Exception ee){
        //                tipo = typeof(string);
        //            }
        //            if (lstEntidad.GetType().Name.ToLower().Contains("list") || tipo.FullName.ToLower().Contains("entidades"))
        //            {
        //                string entidad = tipo.Name;
        //                xml = reader.ReadToEnd();
        //                while (xml.Contains("ArrayOf" + entidad) || xml.Contains(entidad))
        //                {
        //                    xml = xml.Replace("ArrayOf" + entidad, "ROOT").Replace(entidad, "OBJECT");
        //                }
        //            }
        //            else
        //            {
        //                xml = reader.ReadToEnd();
        //            }
        //            xml = xml.Replace("\r\n    ", "")
        //                     .Replace("\r\n    ", "")
        //                     .Replace("\r\n    ", "")
        //                     .Replace("\r\n    ", "")
        //                     .Replace("\r\n  ","")
        //                     .Replace("\r\n","");
        //            return xml;
        //        }
        //        else
        //        {
        //            return xml;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        writer.Close();
        //        writer.Dispose();
        //        ms.Close();
        //        ms.Dispose();
        //    }
        //}
        #endregion

    }
}

//RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]
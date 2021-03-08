using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Net;
using System.Web;
using System.Net.Mail;
using Utilitario;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using NLog;


namespace Utilitarios
{
    public class General
    {
        #region "Variables Locales"
        string Host = Utilitario.General.Obtener_Key("Host");
        string CredUsuario = Utilitario.General.Obtener_Key("CredUsuario");
        string CredPass = Utilitario.General.Obtener_Key("CredPass");
        #endregion

        /// <summary>
        /// Enviar Correo
        /// </summary>
        /// <param name="p_cDe">De</param>
        /// <param name="p_cPara">Para</param>
        /// <param name="p_cCopia">CC</param>
        /// <param name="p_cCopiaO">CCO</param>
        /// <param name="p_cCuerpo">Cuerpo Mensaje</param>
        /// <param name="p_cAsunto">Asunto Mensaje</param>
        /// <param name="p_cFile">Archivo(s) Adjunto(s)</param>
        /// <param name="p_bAdjunto">Adjunto</param>
        public static void EnviarCorreo(String p_cDe, String p_cPara, String p_cCopia, String p_cCopiaO, String p_cCuerpo, String p_cAsunto, String p_cFile, Boolean p_bAdjunto, String p_RutaFisica)
        {
            MailMessage correo = new MailMessage();
            Attachment attach1;
            String[] varPara;
            String[] varCopia = new String[0];
            String[] varCopiaO = new String[0];
            String[] archivo = new String[0];

            varPara = p_cPara.Split(';');
            archivo = p_cFile.Split(';');
            if (p_cCopia != "")
            {
                varCopia = p_cCopia.Split(';');
            }
            if (p_cCopiaO != "")
            {
                varCopiaO = p_cCopiaO.Split(';');
            }

            for (int nCont = 0; nCont < varPara.Length; nCont++)
            {
                correo.To.Add(varPara[nCont]);
            }
            for (int nCont = 0; nCont < varCopia.Length; nCont++)
            {
                correo.CC.Add(varCopia[nCont]);
            }
            for (int nCont = 0; nCont < varCopiaO.Length; nCont++)
            {
                correo.Bcc.Add(varCopiaO[nCont]);
            }
            correo.Subject = p_cAsunto;
            correo.From = new MailAddress(p_cDe);
            correo.Body = p_cCuerpo;
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = fr_Key("Host");
            smtp.Port = 25;
            smtp.Credentials = new NetworkCredential(fr_Key("CredUsuario"), fr_Key("CredPass"));
            smtp.EnableSsl = false;
            if (p_bAdjunto)
            {
                for (int i = 0; i < archivo.Length; i++)
                {
                    if (archivo[i].Length > 0)
                    {
                        attach1 = new Attachment(p_RutaFisica + archivo[i]);
                        correo.Attachments.Add(attach1);
                    }
                }
            }
            smtp.Send(correo);
        }

        public static string fr_Content(string p_cNomArchivo)
        {
            string exten = "", content = "";
            string[] cadena = p_cNomArchivo.Split('.');
            exten = cadena[cadena.Length - 1].ToLower();
            switch (exten)
            {
                case "pdf":
                    content = "application/pdf";
                    break;
                case "doc":
                case "docx":
                    content = "application/msword";
                    break;
                case "xls":
                case "xlsx":
                    content = "application/vnd.ms-excel";
                    break;
                case "ppt":
                case "pps":
                case "pptx":
                case "ppsx":
                    content = "application/vnd.ms-powerpoint";
                    break;
                case "png":
                    content = "image/png";
                    break;
                case "jpeg":
                    content = "image/jpeg";
                    break;
                case "jpg":
                    content = "image/jpg";
                    break;
                case "msg":
                    content = "application/vnd.ms-outlook";
                    break;
            }
            return content;
        }

        public static string fr_Key(string p_cKey)
        {
            try
            {

                return Utilitario.General.Obtener_Key(p_cKey);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static bool ValidarRUC(string RUC)
        {
            return Utilitario.General.ValidacionRUC(RUC);
        }

        public DataTable Convertir_DataTable(Object[] arr)
        {
            try
            {

                return Utilitario.General.ConvertToDatatable(arr);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void LogError(Exception ex, string p_cFrmNombre)
        {
            try
            {

                string Ruta = @Utilitario.General.Obtener_Key("DirTemp");
                Utilitario.Log.LogError(ex, Ruta, p_cFrmNombre);

                if (Utilitario.General.Obtener_Key("EnvioLog").Equals("S"))
                {
                    string[] Adjunto = new string[1];
                    Adjunto[0] = @Utilitario.General.Obtener_Key("DirTemp") + "Error_" + DateTime.Now.ToString("ddMMyyyy") + ".txt";
                    new General().EnviarMail("Log de Funeraria",
                               "Se adjunta Log",
                               fr_Key("EmailRemiteLog"),
                               fr_Key("EmailEnviaLog"),
                               null,
                               null);

                }
            }
            catch (Exception e)
            { }
        }

        public static void LogError(Exception ex)
        {
            try
            {

                string Ruta = @Utilitario.General.Obtener_Key("DirTemp");
                Utilitario.Log.LogError(ex, Ruta, String.Empty);

                if (Utilitario.General.Obtener_Key("EnvioLog").Equals("S"))
                {
                    EnviarMailLog();
                }
            }
            catch
            { }
        }

        public static void EnviarMailLog()
        {

            try
            {
                System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                MailAddress direccion = new MailAddress(fr_Key("EmailDestinatLog"));
                correo.To.Add(direccion);
                correo.Subject = "Log de Errores - WebGot ";
                correo.From = new MailAddress(fr_Key("EmailRemiteLog"));

                correo.Body = "Detalle de Errores - Web de Gestión de Orden de Trabajo";
                correo.IsBodyHtml = true;
                correo.Priority = System.Net.Mail.MailPriority.Normal;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = fr_Key("Host");
                smtp.Port = 25;
                smtp.Credentials = new System.Net.NetworkCredential(fr_Key("CredUsuario"), fr_Key("CredPass"));
                smtp.EnableSsl = false;

                Attachment attach1 = new Attachment(fr_Key("DirTemp") + DateTime.Now.ToString("ddMMyyyy") + ".txt");
                correo.Attachments.Add(attach1);

                smtp.Send(correo);
            }
            catch (Exception ex)
            {
            }
        }

        public static void LogTrace(string p_cLog)
        {
            try
            {

                string Ruta = @Utilitario.General.Obtener_Key("DirTemp");
                Utilitario.Log.LogTrace(p_cLog, Ruta);

                if (Utilitario.General.Obtener_Key("EnvioLog").Equals("S"))
                {
                    string[] Adjunto = new string[1];
                    Adjunto[0] = @Utilitario.General.Obtener_Key("DirTemp") + "Error_" + DateTime.Now.ToString("ddMMyyyy") + ".txt";
                    new General().EnviarMail("Log de APP_WEBGOT",
                               "Se adjunta Log",
                               fr_Key("EmailRemiteLog"),
                               fr_Key("EmailEnviaLog"),
                               null,
                               null);

                }
            }
            catch
            { }
        }


        public bool EnviarMail(string p_cAsunto,
                                      string p_cBody,
                                      string p_cRemitente,
                                      string p_cDestino,
                                      string[] p_arrCopia,
                                      string[] p_arrAdjunto)
        {
            Utilitario.General.EnviarMail(p_cAsunto, p_cBody, p_cRemitente, p_cDestino, p_arrCopia, Host, 25, CredUsuario, CredPass, p_arrAdjunto);
            return true;
        }

        public static string ObtenerDia(string p_cDia)
        {
            string dia;
            switch (p_cDia)
            {
                case "Mon": dia = "Lun";
                    break;
                case "Tue": dia = "Mar";
                    break;
                case "Wed": dia = "Mie";
                    break;
                case "Thu": dia = "Jue";
                    break;
                case "Fri": dia = "Vie";
                    break;
                case "Sat": dia = "Sab";
                    break;
                case "Sun": dia = "Dom";
                    break;
                default: dia = "";
                    break;
            }
            return dia;
        }

        public static string ObtenerMes(DateTime dt)
        {
            return dt.ToString("MMMM");
        }

        public static void pr_MostrarInfo(string p_cMensaje, Label p_Label)
        {
            p_Label.Text = p_cMensaje;
            p_Label.CssClass = "LblMensajeInfo";
        }

        public static void pr_MostrarError(string p_cMensaje, Label p_Label)
        {
            p_Label.Text = p_cMensaje;
            p_Label.CssClass = "LblMensajeError";
        }

        #region metodos y funciones genericos
        /// <summary>
        /// Agrega un atributo OnClick de un boton en una grilla
        /// </summary>
        /// <param name="e"></param>
        /// <param name="nBtn"></param>
        /// <param name="function"></param>
        /// <param name="parametros"></param>
        public static void AddAttribut(GridViewRowEventArgs e, string nBtn, string function, params Object[] parametros)
        {
            ImageButton b = (ImageButton)e.Row.FindControl(nBtn);
            string param = "";

            foreach (Object ob in parametros)
            {
                try
                {
                    param += param.Equals("") ? "'" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, ob.ToString())) + "'" : ",'" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, ob.ToString())) + "'";
                }
                catch
                {
                    param += param.Equals("") ? "''" : ",''";
                }
            }
            b.Attributes.Add("onclick", "javascript:return " + function + "(" + param + ")");
        }
        /// <summary>
        /// Devuelve la cantidad de paginas que tiene una lista
        /// </summary>
        /// <param name="Tamaño"></param>
        /// <param name="NroTotal"></param>
        /// <returns></returns>
        public static int PageCount(int Tamaño, int NroTotal)
        {
            int nNroPag = Convert.ToInt32(Math.Floor(Convert.ToDouble((NroTotal / Tamaño))));
            int nResto = Convert.ToInt32(NroTotal % Tamaño);
            return nResto > 0 ? nNroPag + 1 : nNroPag;
        }
        /// <summary>
        /// Llena una grilla y elimina la ultima fila
        /// -->(Su proposito es que cuando la grilla no tenga datos se vea la cabecera)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gv"></param>
        /// <param name="L"></param>
        public static void llenarGrid<T>(GridView gv, List<T> L)
        {
            gv.DataSource = L;
            gv.DataBind();
            gv.Rows[gv.Rows.Count - 1].Visible = false;
        }
        /// <summary>
        /// Agrega un funcion al atributo onkeypress de un TextBox
        /// </summary>
        /// <param name="txt"></param>
        public static void validaText(TextBox txt, TiposDatos tp)
        {
            int value = (int)Enum.Parse(typeof(TiposDatos), Enum.GetName(typeof(TiposDatos), tp));
            txt.Attributes.Add("onkeypress", "return Valida_Dato(event," + value.ToString() + ")");
        }
        /// <summary>
        /// tipos de datos para validar
        /// </summary>
        public enum TiposDatos : int
        {
            /// <summary>
            /// Acepta algunos caracteres especiales como el @ etc.
            /// </summary>
            Email = 7,
            /// <summary>
            /// acepta solo letras
            /// </summary>
            Nombres = 6,
            /// <summary>
            /// acepta numeros #*-
            /// </summary>
            Telefono = 3,
            /// <summary>
            /// acepta letras, numeros .-
            /// </summary>
            RazonSocial = 11,
            /// <summary>
            /// acepta letras y numeros
            /// </summary>
            NumeroDocumento = 10,
            /// <summary>
            /// No acepta enter.
            /// </summary>
            TextoGeneral = 2,
            /// <summary>
            /// Acepta enter.
            /// </summary>
            TextoGeneral2 = 12
        }
        /// <summary>
        /// Establece el titulo del formulario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tit"></param>
        public static void Titulo(object sender, string tit)
        {
            Page page = (Page)sender;
            if ((page.Master != null))
            {
                Label Titulo = (Label)page.Master.FindControl("lblTitulo");
                if ((Titulo != null))
                {
                    Titulo.Text = tit;
                    page.Title = ".:: - MAPFRE PERU - ::.";
                }
            }
        }
        /// <summary>
        /// Ejecuta un js
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="function"></param>
        /// <param name="parametros"></param>
        public static void execJs(object sender, string function, params Object[] parametros)
        {
            Page page = (Page)sender;
            string param = "";
            foreach (Object ob in parametros)
                param += param.Equals("") ? "'" + ob.ToString().Trim() + "'" : ",'" + ob.ToString().Trim() + "'";
            page.ClientScript.RegisterStartupScript(page.GetType(), "myscript", function + "(" + param + ");", true);
        }
        /// <summary>
        /// Requiere updatePanel
        /// </summary>
        /// <param name="up"></param>
        /// <param name="function"></param>
        /* public static void pr_functionJavaScrip(UpdatePanel up, String function)
         {
             Guid oGuid = Guid.NewGuid();
             ScriptManager.RegisterStartupScript(up, up.GetType(), oGuid.ToString(), function, true);
         }*/
        /// <summary>
        /// Cuando la pagina no tiene updatePanel
        /// </summary>
        /// <param name="function"></param>
        public static void pr_functionJavaScrip(object obj, String function)
        {
            Page page = (Page)obj;
            page.ClientScript.RegisterStartupScript(page.GetType(), "myScript", "<script>javascript: " + function + "</script>");
        }

        /*public static void pr_AlertJavaScrip(UpdatePanel up, String msg)
        {
            String script = "alert('" + msg + "');";
            Guid oGuid = Guid.NewGuid();
            ScriptManager.RegisterStartupScript(up, up.GetType(), oGuid.ToString(), script, true);
        }*/
        public static string LeerRutaFisica(String Carpeta)
        {
            return System.AppDomain.CurrentDomain.BaseDirectory + Carpeta + "\\";
        }

        public static DataTable LeerExcel(String Ruta)
        {
            return Utilitario.General.LeerExcel(Ruta);
        }

        /// <summary>
        /// obtenemos la extención del archivo adjunto(verificar si existe este método caso contrario agregarlo)
        /// </summary>        
        public static string exten(string fullName)
        {
            return fullName.Substring(fullName.LastIndexOf(".") + 1).ToLower();
        }

        /// <summary>
        /// obtenemos el nombre del archivo adjunto(verificar si existe este método caso contrario agregarlo)
        /// </summary>
        private static string nameFile(string fullName)
        {
            return fullName.Substring(0, fullName.LastIndexOf("."));
        }

        /// <summary>
        /// Guardar Archivo adjunto(verificar si existe este método caso contrario agregarlo)
        /// </summary>
        public static string[] GuardarArchivo(string ruta, System.Web.UI.WebControls.FileUpload upSave, string newName, bool reemplazar, bool creaRuta)
        {
            try
            {

                string Name = newName == null ? nameFile(upSave.FileName) : newName;

                if (creaRuta != null && creaRuta.Equals(true))
                {
                    if (!System.IO.Directory.Exists(ruta))
                        System.IO.Directory.CreateDirectory(ruta);
                    else if (reemplazar != null && reemplazar.Equals(true) && System.IO.File.Exists(ruta + "\\" + Name))
                        System.IO.File.Delete(ruta + "\\" + Name);
                }
                int x = 0;
                bool guardar = false;
                String archiv = ruta + "\\" + Name;
                do
                {
                    newName = Name + (x.Equals(0) ? "" : "(" + x.ToString() + ")") + "." + exten(upSave.FileName);
                    archiv = ruta + "\\" + newName;
                    if (System.IO.File.Exists(archiv)) x++;
                    else
                    {
                        upSave.SaveAs(archiv);
                        guardar = true;
                    }
                } while (guardar != true);
                return (archiv + ";" + newName).Split(';');
            }
            catch (Exception ex)
            {
                return ("error - " + ex.Message.ToString()).Split(';');
            }
        }

        
        //inicio: ftello
        public static Logger getLogger()
        {
            return LogManager.GetCurrentClassLogger();
        }
 
        //fin: ftello 
        
        
        #endregion

    }

    public class NombreForm
    {
        public static string frmBusquedaEvAct = "frmBusquedaEvAct";
    }

   

}

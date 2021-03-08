using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using AccesoDatos;
using System.Data.OracleClient;

namespace LogicaNegocio
{
    public class LN_COTIZACION
    {
        AD_COTIZACION objAD_Cotizacion;
        public LN_COTIZACION()
        {
            objAD_Cotizacion = new AD_COTIZACION();
        }

        public EN_COTIZARESULT ObtenerCotizacion(EN_COTIZA objCotizar)
        {
            return objAD_Cotizacion.ObtenerCotizacion(objCotizar);
        }

        public EN_USUARIO Recupera_usuario(String Usuario, String Password)
        {
            return objAD_Cotizacion.Recupera_usuario(Usuario, Password);
        }
    }
}

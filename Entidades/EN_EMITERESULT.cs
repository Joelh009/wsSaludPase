using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entidades
{
    [Serializable]
    [DataContract]
    public class EN_EMITERESULT
    {

        #region "VARIABLES LOCALES"
        private List<EN_RECIBO> LST_RECIBO;
        #endregion

        #region "CONSTRUCTOR"
        public EN_EMITERESULT()
        {
            LST_RECIBO = new List<EN_RECIBO>();
        }
        #endregion

        /// <summary>
        /// CODIGO DE ERROR
        /// </summary>
        [DataMember]
        public string COD_ERROR { get; set; }

        /// <summary>
        /// DESCRIPCION DE ERROR
        /// </summary>
        [DataMember]
        public String DESC_ERROR { get; set; }

        /// <summary>
        /// CODIGO DE COMPANIA
        /// </summary>
        [DataMember]
        public Nullable<int> COD_CIA { get; set; }

        /// <summary>
        /// NUMERO DE POLIZA
        /// </summary>
        [DataMember]
        public string NUM_POLIZA { get; set; }

        /// <summary>
        /// NUMERO DE SUPLEMENTO
        /// </summary>
        [DataMember]
        public Nullable<int> NUM_SUP { get; set; }

        /// <summary>
        /// NUMERO DE APLICACION
        /// </summary>
        [DataMember]
        public Nullable<int> NUM_APLI { get; set; }

        /// <summary>
        /// NUMERO DE SUPLEMENTO APLICACION
        /// </summary>
        [DataMember]
        public Nullable<int> NUM_SUP_APLI { get; set; }

        /// <summary>
        /// NUMERO DE CUOTAS
        /// </summary>
        [DataMember]
        public Nullable<int> NUM_CUOTAS { get; set; }

        /// <summary>
        /// CODIGO DE MONEDA
        /// </summary>
        [DataMember]
        public Nullable<int> COD_MON { get; set; }

        /// <summary>
        /// SIMBOLO DE MONEDA
        /// </summary>
        [DataMember]
        public string SIMBL_MONEDA { get; set; }

        /// <summary>
        /// IMPORTE NETA
        /// </summary>
        [DataMember]
        public Nullable<double> IMP_NETA { get; set; }

        /// <summary>
        /// IMPORTE DERECHO DE EMISION
        /// </summary>
        [DataMember]
        public Nullable<double> IMP_DERECHO_EMISION { get; set; }

        /// <summary>
        /// IMPORTE IGV
        /// </summary>
        [DataMember]
        public Nullable<double> IMP_IGV { get; set; }

        /// <summary>
        /// IMPORTE BONIFICACION
        /// </summary>
        [DataMember]
        public Nullable<double> IMP_BONIFI { get; set; }

        /// <summary>
        /// IMPORTE BONIFICACION
        /// </summary>
        [DataMember]
        public Nullable<double> IMP_INTERES { get; set; }

        /// <summary>
        /// IMPORTE NETA SIN BONIFICACION
        /// </summary>
        [DataMember]
        public Nullable<double> IMP_NETA_BONI { get; set; }

        //RDIAZ - Inicio [24/10/2017]
        /// <summary>
        /// IMPORTE RECIBO TOTAL
        /// </summary>
        [DataMember]
        public Nullable<double> PRI_BRUTA_TOTAL_CAL { get; set; }
        //RDIAZ - Fin [24/10/2017]

        /// <summary>
        /// CODIGO DE FRACCIONAMIENTO DE PAGO
        /// </summary>
        [DataMember]
        public Nullable<int> COD_FRAC_PAGO { get; set; }

        /// <summary>
        /// DESCRIPCION DE ERROR
        /// </summary>
        [DataMember]
        public string MCA_PROVISIONAL { get; set; }

        /// <summary>
        /// DETALLE DE RECIBO
        /// </summary>
        [DataMember]
        public List<EN_RECIBO> RECIBOS { get { return LST_RECIBO; } set { LST_RECIBO = value; } }

    }
}

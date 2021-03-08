using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entidades
{
    [Serializable]
    [DataContract]
    public class EN_RECIBO
    {
        /// <summary>
        /// CODIGO DE FRACCIONAMIENTO DE PAGO
        /// </summary>
        //[DataMember]
        //public Nullable<int> COD_FRAC_PAGO { get; set; }

        /// <summary>
        /// NUMERO DE RECIBO
        /// </summary>
        [DataMember]
        public Nullable<int> NUM_RECIBO { get; set; }

        /// <summary>
        /// NUMERO DE CUOTA
        /// </summary>
        [DataMember]
        public Nullable<int> NUM_CUOTA { get; set; }

        /// <summary>
        /// FECHA EFECTO DE RECIBO
        /// </summary>
        [DataMember]
        public string FEC_EFEC_RECIBO { get; set; }

        /// <summary>
        /// FECHA VENCIMIENTO DE RECIBO
        /// </summary>
        [DataMember]
        public string FEC_VCTO_RECIBO { get; set; }

        /// <summary>
        /// IMPORTE RECIBO
        /// </summary>
        [DataMember]
        public Nullable<double> IMP_RECIBO { get; set; }

       /// <summary>
       /// IMPORTE NETA
       /// </summary>
       [DataMember]
       public Nullable<double> IMP_NETA { get; set; }

       /// <summary>
       /// IMPORTE RECARGO
       /// </summary>
       [DataMember]
       public Nullable<double> IMP_RECARGO { get; set; }

       /// <summary>
       /// IMPORTE IMPUESTO
       /// </summary>
       [DataMember]
       public Nullable<double> IMP_IMPTOS { get; set; }

       /// <summary>
       /// IMPORTE BONIFICACIÓN
       /// </summary>
       [DataMember]
       public Nullable<double> IMP_BONI { get; set; }

       /// <summary>
       /// IMPORTE INTERES
       /// </summary>
       [DataMember]
       public Nullable<double> IMP_INTERES { get; set; }
       /// <summary>
       /// IMPORTE IMPUESTOS INTERES
       /// </summary>
       [DataMember]
       public Nullable<double> IMP_IMPTOS_INTERES { get; set; }
    }
}

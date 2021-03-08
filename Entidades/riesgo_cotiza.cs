//RDIAZ - Inicio [Ctrl. Cambio II - MultiRiesgo]
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entidades
{
    [Serializable]
    [DataContract]

    public class riesgo_cotiza
    {

        private double? _pct_dscto = 0;
        [DataMember]
        public double? pct_dscto
        {
            get { return _pct_dscto; }
            set { if (value != null)_pct_dscto = value; }
        }

        [DataMember]
        public string mca_sexo { get; set; }
        [DataMember]
        public string fec_nacimiento { get; set; }
        [DataMember]
        public string cod_parentesco { get; set; }
        [DataMember]
        public int? val_estatura { get; set; }
        [DataMember]
        public int? val_peso { get; set; }
        [DataMember]
        public string mca_zurdo { get; set; }
        [DataMember]
        public int? num_riesgo { get; set; }//JHUARCAYA - HITSS - 02/02/2021
        [DataMember]
        public string num_certificado { get; set; }//JHUARCAYA - HITSS - 02/02/2021
        [DataMember]
        public string desc_certificado { get; set; }//JHUARCAYA - HITSS - 02/02/2021


    }
}

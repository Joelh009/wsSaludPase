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
    public class riesgo_emite
    {
         private List<riesgo_exclusion> LST_DATOS_RIESGO_EXCLUSION;

        public riesgo_emite()
        {
            LST_DATOS_RIESGO_EXCLUSION = new List<riesgo_exclusion>();
        }

       
        private double? _pct_dscto = 0;
        [DataMember]
        public double? pct_dscto
        {
            get { return _pct_dscto; }
            set { if (value != null)_pct_dscto = value; }
        }

        [DataMember]
        public string cod_parentesco { get; set; }
        [DataMember]
        public string tip_docum { get; set; }
        [DataMember]
        public string cod_docum { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string ape_paterno { get; set; }
        [DataMember]
        public string ape_materno { get; set; }
        [DataMember]
        public string est_civil { get; set; }
        [DataMember]
        public string mca_sexo { get; set; }
        [DataMember]
        public string fec_nacimiento { get; set; }
        /*
        [DataMember]
        public string texto_beneficiario { get; set; } */
        [DataMember]
        public int? num_poliza_aseg_ant { get; set; }
        [DataMember]
        public string nom_aseg_ant { get; set; }
        [DataMember]
        public string fec_efec_pol_ant { get; set; }
        [DataMember]
        public string fec_vcto_pol_ant { get; set; }
        [DataMember]
        public int? val_peso { get; set; }
        [DataMember]
        public int? val_estatura { get; set; }
        [DataMember]
        public string mca_zurdo { get; set; } 
        /*
        [DataMember]
        public string tip_persona { get; set; } */


        //RALMENDRAS-INICIO 20/21/2018
        [DataMember]
        public string mca_continuidad_seg { get; set; }
        [DataMember]
        public int? cod_motivo_dscto_recargo { get; set; }
        [DataMember]
        public string des_motivo_dscto_recargo { get; set; }
        [DataMember]
        public string mca_continuidad_maternidad { get; set; }
        [DataMember]
        public string val_carencia_maternidad { get; set; }
        [DataMember]
        public string mca_continuidad_enfcong { get; set; }
        [DataMember]
        public string val_carencia_enfcongnts { get; set; }
        [DataMember]
        public string mca_continuidad_oncologico { get; set; }
        [DataMember]
        public string val_carencia_oncologico { get; set; }
        [DataMember]
        public string mca_continuidad_transplnorg { get; set; }
        [DataMember]
        public string val_carencia_transplnorg { get; set; }
        [DataMember]
        public string mca_continuidad_nino_sano { get; set; }
        [DataMember]
        public string val_carencia_nino_sano { get; set; }
        [DataMember]
        public string mca_exclusion { get; set; }
        [DataMember]
        public string mca_observaciones { get; set; }
        [DataMember]
        public int? imp_ajuste_gen { get; set; }
        [DataMember]
        public int? pct_ajuste_gen { get; set; }
        [DataMember]
        public string mca_poliza_latina { get; set; }
        [DataMember]
        public int? num_riesgo { get; set; }//JHUARCAYA - HITSS - 02/02/2021
        [DataMember]
        public string num_certificado { get; set; }//JHUARCAYA - HITSS - 02/02/2021
        [DataMember]
        public string desc_certificado { get; set; }//JHUARCAYA - HITSS - 02/02/2021

        [DataMember]
        public List<riesgo_exclusion> datos_riesgo_exclusion
        {
            set { LST_DATOS_RIESGO_EXCLUSION = value; }
            get { return LST_DATOS_RIESGO_EXCLUSION; }
        }
        //RALMENDRAS-FIN 20/21/2018
    }
}

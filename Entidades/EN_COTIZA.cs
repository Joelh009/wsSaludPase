using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entidades
{
    [Serializable]
    [DataContract]

    
    public class EN_COTIZA
    {
        //RDIAZ - Inicio [Ctrl. Cambio II - MultiRiesgo]
        private List<riesgo_cotiza> LST_DATOS_RIESGO_COTIZA;
        public EN_COTIZA()
        {
            LST_DATOS_RIESGO_COTIZA = new List<riesgo_cotiza>();
        }
        //RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]

        [DataMember]
        public string usuario { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public int? cod_agt { get; set; }
       
        [DataMember]
        public string fec_efec_pol { get; set; }
        [DataMember]
        public string fec_vcto_pol { get; set; }
        [DataMember]
        public int? cod_modalidad { get; set; }

        [DataMember]
        public int? cod_mon { get; set; }
        [DataMember]
        public string mca_imp_mapfre_dolar { get; set; }
        [DataMember]
        public double? imp_mapfre_dolar { get; set; }
        //[DataMember]
        //public double? pct_dscto_comercial { get; set; }

        /*private double? _pct_dscto_comercial = 0;
        [DataMember]
        public double? pct_dscto_comercial
        {
            get { return _pct_dscto_comercial; }
            set { if (value != null)_pct_dscto_comercial = value; }
        }*/

        [DataMember]
        public int? tip_empresa { get; set; }

        [DataMember]
        public int? giro_negocio { get; set; }
        [DataMember]
        public int? cod_frac_pago { get; set; }
        [DataMember]
        public int? cod_cia { get; set; }
     
        [DataMember]
        public int? cod_ramo { get; set; }
        [DataMember]
        public string num_poliza_grupo { get; set; }
        [DataMember]
        public int? num_contrato { get; set; }
        [DataMember]
        public int? num_sub_contrato { get; set; }
        //INICIO JHUARCAYA - HITSS - 02/02/2021
        [DataMember]
        public int? cod_producto { get; set; }
        [DataMember]
        public int? cod_sub_producto { get; set; }
        [DataMember]
        public String cod_plan { get; set; }
        //FIN JHUARCAYA - HITSS - 02/02/2021
        [DataMember]
        public string mca_sexo_aseg { get; set; }
        [DataMember]
        public string fec_nacimiento_aseg { get; set; }
        [DataMember]
        public int? cod_parentesco { get; set; }
        [DataMember]
        public int? cod_ocupacion { get; set; }
        [DataMember]
        public int? cod_especialidad { get; set; }
        [DataMember]
        public int? num_dependientes { get; set; }
        [DataMember]
        public int? val_estatura { get; set; }
        [DataMember]
        public int? val_peso { get; set; }
        [DataMember]  
        public string mca_zurdo { get; set; }
        [DataMember]
        public double? imp_pneta_mensual { get; set; }


        [DataMember]
        public List<riesgo_cotiza> datos_riesgo_cotiza
        {
            
            set { LST_DATOS_RIESGO_COTIZA = value; }
            get { return LST_DATOS_RIESGO_COTIZA;  }
        }
        [DataMember]
        public String xml_riesgos { get; set; }


        [DataMember]
        public double? pct_ajus_int_red_plaza { get; set; } /*PBARRIOS 20190411*/
        [DataMember]
        public string mca_pct_int_red_plaza { get; set; } /*PBARRIOS 20190411*/
    }
}

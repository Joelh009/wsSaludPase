using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entidades
{
    [Serializable]
    [DataContract]
    public class EN_EMITE
    {
        //RDIAZ - Inicio [Ctrl. Cambio II - MultiRiesgo]
        private List<riesgo_emite> LST_DATOS_RIESGO_EMITE;
        //private List<riesgo_exclusion> LST_DATOS_RIESGO_EXCLUSION;
        public EN_EMITE()
        {
            LST_DATOS_RIESGO_EMITE = new List<riesgo_emite>();
            //LST_DATOS_RIESGO_EXCLUSION = new List<riesgo_exclusion>();
        }
        //RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]

        [DataMember]
        public string usuario { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string cod_sistema { get; set; } 
        [DataMember]
        public int? cod_agt { get; set; }
        [DataMember]
        public string fec_efec_pol { get; set; }

        [DataMember]
        public int? cod_modalidad { get; set; }
        [DataMember]
        public string fec_vcto_pol { get; set; }
        [DataMember]
        public int? cod_frac_pago { get; set; }
        [DataMember]
        public int? cod_mon { get; set; }
        [DataMember]
        public string mca_imp_mapfre_dolar { get; set; }
        [DataMember]
        public double? imp_mapfre_dolar { get; set; }

        //[DataMember]
        //public double? pct_dsc_comercial { get; set; }

        /*private double? _pct_dscto_comercial = 0;
        [DataMember]
        public double? pct_dscto_comercial
        {
            get { return _pct_dscto_comercial; }
            set { if (value != null) _pct_dscto_comercial = value; }
        } */

        [DataMember]
        public int? tip_empresa { get; set; }
        [DataMember]
        public int? giro_negocio { get; set; }
        [DataMember]
        public string mca_insp_prev { get; set; }
        [DataMember]
        public string cod_cat_insp { get; set; }

        [DataMember]
        public int? cod_cia { get; set; }
        [DataMember]
        public int? cod_ramo { get; set; }
        [DataMember]
        public string num_poliza_grupo { get; set; }
        [DataMember]
        public string cod_pais { get; set; }
        [DataMember]
        public string cod_nacionalidad { get; set; }
        [DataMember]
        public int? num_contrato { get; set; }
        [DataMember]
        public int? num_sub_contrato { get; set; }
        [DataMember]
        public string mca_fisico { get; set; }
        [DataMember]
        public string tip_docum { get; set; }
        [DataMember]
        public string cod_docum { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string ape_paterno { get; set; }
        [DataMember]
        public string mca_fisico_contratante { get; set; }
        [DataMember]
        public string ape_materno { get; set; }
        [DataMember]
        public string fec_nacimiento { get; set; }
        [DataMember]
        public string mca_sexo { get; set; }
        [DataMember]
        public string cod_est_civil { get; set; }
        [DataMember]
        public int? cod_profesion { get; set; }
        [DataMember]
        public string tip_documento_contratante { get; set; }
        [DataMember]
        public string cod_documento_contratante { get; set; }
        [DataMember]
        public string nombre_contratante { get; set; }
        [DataMember]
        public string ape_paterno_contratante { get; set; }
        [DataMember]
        public string ape_materno_contratante { get; set; }
        [DataMember]
        public string mail_contrantante { get; set; }
        [DataMember]
        public string tel_fijo_contratante { get; set; }
        [DataMember]
        public string tel_movil_contratante { get; set; }
        [DataMember]
        public string representante { get; set; }
        [DataMember]
        public int? cargo_representante { get; set; }
        [DataMember]
        public string tip_economica { get; set; }
        [DataMember]
        public string fec_nacimiento_cliente { get; set; }
        [DataMember]
        public string mca_sexo_cliente { get; set; }
        [DataMember]
        public int? profesion_cliente { get; set; }
        [DataMember]
        public string est_civil { get; set; }
        [DataMember]
        public string cod_pais_cli { get; set; }
        [DataMember]
        public string cod_nacionalidad_cli { get; set; }
        [DataMember]
        public int? cod_estado_cli { get; set; }
        [DataMember]
        public string cod_provincia_cli { get; set; }
        [DataMember]
        public string cod_distrito_cli { get; set; }
        [DataMember]
        public int? tipo_domicilio { get; set; }
        [DataMember]
        public string domicilio_cliente { get; set; }
        [DataMember]
        public int? tip_numero_cliente { get; set; }
      
        [DataMember]
        public int? tip_interior_cliente { get; set; }
        [DataMember]
        public string interior_cliente { get; set; }
        [DataMember]
        public int? tip_zona_cliente { get; set; }
        [DataMember]
        public string zona_cliente { get; set; }
        [DataMember]
        public string lugar_referencia { get; set; }
        [DataMember]
        public int? cod_parentesco { get; set; }
        [DataMember]
        public int? cod_provincia { get; set; }
        [DataMember]
        public int? cod_distrito{ get; set; }
        [DataMember]
        public string tip_persona { get; set; }
        [DataMember]
        public string tip_documento_aseg { get; set; }
        [DataMember]
        public int? cod_documento_aseg { get; set; }
        [DataMember]
        public string nom_asegurado { get; set; }
        [DataMember]
        public string ape_pat_aseg { get; set; }
        [DataMember]
        public string ape_mat_aseg { get; set; }
        [DataMember]
        public string mca_sexo_aseg { get; set; }
        [DataMember]
        public string fec_nacimiento_aseg { get; set; }
        [DataMember]
        public int? cod_ocupacion { get; set; }
        [DataMember]
        public int? cod_especialidad { get; set; }
        [DataMember]
        public string texto_beneficiario { get; set; }
        [DataMember]
        public int? num_dependientes { get; set; }
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
        [DataMember]
        public string num_cliente { get; set; }
         [DataMember]
        public double? imp_pneta_mensual { get; set; }
        //INICIO JHUARCAYA - HITSS - 02/02/2021
        [DataMember]
        public int? cod_producto { get; set; }
        [DataMember]
        public int? cod_sub_producto { get; set; }
        [DataMember]
        public String cod_plan { get; set; }
        //FIN JHUARCAYA - HITSS - 02/02/2021
        [DataMember]
        public List<riesgo_emite> datos_riesgo_emite
        {
            set { LST_DATOS_RIESGO_EMITE = value; }
            get { return LST_DATOS_RIESGO_EMITE; }
        }

        //[DataMember]
        //public List<riesgo_exclusion> datos_riesgo_exclusion
        //{
        //    set { LST_DATOS_RIESGO_EXCLUSION = value; }
        //    get { return LST_DATOS_RIESGO_EXCLUSION; }
        //}


        [DataMember]
        public String xml_riesgos { get; set; }
        //RDIAZ - Fin [Ctrl. Cambio II - MultiRiesgo]
        //******************************************
         
        //INICIO, 17-12-2018 SE AGREGÁ PARÁMETROS NUEVOS AL SERVICIO

        //[DataMember]
        //public String num_poliza_aseg_ant  { get; set; }
        //[DataMember]
        //public int nom_aseg_ant { get; set; }
        //[DataMember]
        //public String xml_riesgos_exclusion { get; set; }

      

        //FINAL, 17-12-2018 SE AGREGÁ PARÁMETROS NUEVOS AL SERVICIO

        //JBOLIVAR
        //PARAMETRO NUEVO 14-10-2019
        [DataMember]
        public string observaciones { get; set; }
        //END JBOLIVAR

        
    }
}

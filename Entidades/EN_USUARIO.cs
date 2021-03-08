using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entidades
{
    [Serializable]
    [DataContract]
    public class EN_USUARIO
    {

        /// <summary>
        /// CODIGO DE ERROR
        /// </summary>
        [DataMember]
        public string COD_ERROR { get; set; }

        /// <summary>
        /// DESCRIPCION DE ERROR
        /// </summary>
        [DataMember]
        public string DESC_ERROR { get; set; }

        /// <summary>
        /// CANTIDAD DE REGISTROS
        /// </summary>
        [DataMember]
        public Nullable<int> cantidad { get; set; }

        /// <summary>
        /// CODIGO DE USUARIO
        /// </summary>
        [DataMember]
        public String codigo { get; set; }

        /// <summary>
        /// CONTRASEÑA DE LOGGUEO
        /// </summary>
        [DataMember]
        public String password { get; set; }

        /// <summary>
        /// TIPO DE DOCUMENTO
        /// </summary>
        [DataMember]
        public string tip_doc { get; set; }

        /// <summary>
        /// CODIGO DE DOCUMENTO
        /// </summary>
        [DataMember]
        public string cod_docum { get; set; }

        /// <summary>
        /// NOMBRE DE TERCERO
        /// </summary>
        [DataMember]
        public String nom_tercero { get; set; }

        /// <summary>
        /// APELLIDO PATERNO
        /// </summary>
        [DataMember]
        public string ape1_tercero { get; set; }

        /// <summary>
        /// APELLIDO MATERNO
        /// </summary>
        [DataMember]
        public string ape2_tercero { get; set; }

        /// <summary>
        /// USUARIO DE CREACION
        /// </summary>
        [DataMember]
        public string usu_cre { get; set; }
    }
}

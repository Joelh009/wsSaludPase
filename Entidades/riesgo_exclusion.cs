using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entidades
{
    [Serializable]
    [DataContract]
    public class riesgo_exclusion
    {

        [DataMember]
        public string cod_exclusion { get; set; }
        [DataMember]
        public string obs_exclusion { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AccesoDatos;
using Entidades;

namespace LogicaNegocio
{
    public class LN_Emision
    {
        AD_Emision objAD_Emision;
        public LN_Emision()
        {
            objAD_Emision = new AD_Emision();
        }

        public EN_EMITERESULT Emitir(EN_EMITE objEmitir)
        {
            return objAD_Emision.Emitir(objEmitir);
        }

        public void EscrbirLog(string texto)
        {
            AD_Emision AD = new AD_Emision();
            AD.EscrbirLog(texto);
        }
    }
}

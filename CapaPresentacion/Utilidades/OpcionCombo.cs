using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaPresentacion.Utilidades
{
    public class OpcionCombo
    {
        public string Texto { get; set; }
        public object Valor { get; set; }

        //public OpcionCombo(string texto, object valor) // Se le pasa un objeto porque puede ser booleano,entero, lo que sea.
        //{
        //    Texto = texto;
        //    Valor = valor;
        //}

        //public override string ToString()
        //{
        //    return Texto;
        //}
    }
}

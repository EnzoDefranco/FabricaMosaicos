using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class opcionesVenta
    {
        public static List<OpcionComboBox> ObtenerOpcionesCondicion()
        {
            return new List<OpcionComboBox>
            {
                new OpcionComboBox() { Valor = "", Texto = "" }, // Opción vacía
                new OpcionComboBox() { Valor = "A 15 días", Texto = "A 15 días" },
                new OpcionComboBox() { Valor = "A 30 / 45 días", Texto = "A 30 / 45 días" }
            };
        }

        public static List<OpcionComboBox> ObtenerOpcionesFormaPago()
        {
            return new List<OpcionComboBox>
            {
                new OpcionComboBox() { Valor = "", Texto = "" }, // Opción vacía,
                new OpcionComboBox() { Valor = "Efectivo / Transferencia", Texto = "Efectivo / Transferencia" },
                new OpcionComboBox() { Valor = "Cheque / Transferencia", Texto = "Cheque / Transferencia" },
                new OpcionComboBox() { Valor = "Efectivo", Texto = "Efectivo" },
                new OpcionComboBox() { Valor = "Tarjeta", Texto = "Tarjeta" },
                new OpcionComboBox() { Valor = "Transferencia", Texto = "Transferencia" },
                new OpcionComboBox() { Valor = "Cheque", Texto = "Cheque" },
                new OpcionComboBox() { Valor = "Otro", Texto = "Otro" }

            };
        }


    }
}

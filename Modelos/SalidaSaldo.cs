using Comunes.Respuesta;

namespace Cuenta.Modelos
{
    public class Saldo
    {
        public decimal saldo { get ; set; }
        public int cuenta { get; set; }
    }

    public class SalidaSaldo
    {
        public Saldo? saldo { get; set; }
        internal RespuestaBD RespuestaBD { get; set; }
    }

    public class SalidaSaldoTotal
    {
        public decimal saldo { get; set; }
        internal RespuestaBD RespuestaBD { get; set; }
    }
}
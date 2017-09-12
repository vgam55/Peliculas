using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Peliculas.MODELO.Exceptions
{
    class DatoIncorrectoException: Exception
    {
        public const String salida= "Ha introducido un número en lugar de un caracter (o viceversa). Vuelva a introducirlo";
        private string mensaje = null;

        public DatoIncorrectoException()
        {
            this.mensaje = "";
        }


        public DatoIncorrectoException(String mensaje)
            : base(mensaje)
        {
            this.mensaje = mensaje;
        }

        public DatoIncorrectoException(String mensaje, Exception inner)
            : base(mensaje, inner)
        {
            this.mensaje = mensaje;
        }

        

        public String rMensaje()
        {
            this.mensaje = salida;
            return this.mensaje;
        }
    }
}

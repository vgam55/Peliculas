using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Peliculas.MODELO.Exceptions
{
    class OperacionFallidaException: Exception
    {
       private const String  mensaje = "No se pudo realizar la operación. Intentelo de nuevo.";
 
        private String salida;

        public OperacionFallidaException()
        {
            this.salida = "";
        }

        public OperacionFallidaException(String mensaje)
            : base(mensaje)
        {
            this.salida = mensaje;
        }

        public String rMensaje()
        {
            this.salida = mensaje;
            return this.salida;
        }

      
    }
}

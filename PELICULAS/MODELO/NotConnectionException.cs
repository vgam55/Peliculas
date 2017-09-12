using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Peliculas.MODELO.Exceptions
{
    class NotConnectionException:Exception
    {
        public const String message="No se pudo establecer conexión";
        private String mensaje;
        public NotConnectionException()
        {
            mensaje = "";
        }

        public NotConnectionException(String mensaje): base(mensaje)
        {
            this.mensaje = mensaje;
        }

        public NotConnectionException(String mensaje, Exception inner)
            : base(mensaje, inner)
        {
            this.mensaje = mensaje;
        }

        public String rMensaje()
        {
            return this.mensaje;
        }

        public String rMensaje(String mensaje)
        {
            this.mensaje = mensaje;
            return this.mensaje;
        }
    }
}

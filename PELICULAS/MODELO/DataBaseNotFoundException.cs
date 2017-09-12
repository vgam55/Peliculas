using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Peliculas.MODELO.Exceptions
{
    class DataBaseNotFoundException : Exception
    {
        public const String salida = "No se encontro la base de datos";
        public String mensaje = "";

        public DataBaseNotFoundException()
        {
            mensaje = salida;
        }



        public DataBaseNotFoundException(String mensaje, Exception inner)
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

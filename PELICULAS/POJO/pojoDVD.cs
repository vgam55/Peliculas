using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Peliculas.POJO
{
    class pojoDVD
    {
        public int idDvd;          //Guarda el id del dvd. Autonumerico 
        public int partes;        //Número de partes que tiene esas por pelicula
        public String titulo;    //Guarda el titulo con el que se conoce la pelicula en España
        public String subtitulo; //Subtitulo de la pelicula (si tiene)  
        public DateTime fecha;   //Guarda la fecha en la que se acabo la descarga
        public String series;    //Indica si han sacado una serie de esa pelicula o si es una serie
        public String copiaSeg;  //Guarda si se ha hecho copia de seguridad y donde
        public String editadoPor;//Indica si se ha hecho una edición por el dueño del DVD o si es una copia directa del DVD
        public String estuches;  //Indica si se ha guardado en un estuche y en cual
        public int copiaBR;      //Indica si se ha hecho copia en blu-ray
        public int menuHecho;    //Indica si se ha hecho un menu dentro de ese DVD para acceder a las distintas partes que pueda tener

        public pojoDVD(int idDvd, int partes, String titulo, String subtitulo, DateTime fecha, String series,
            String copiaSeg, String editadoPor, String estuches, int copiaBR, int menuHecho)
        {
            this.idDvd = idDvd;
            this.partes = partes;
            this.titulo = titulo;
            this.subtitulo = subtitulo;
            this.fecha = fecha;
            this.series = series;
            this.copiaSeg = copiaSeg;
            this.editadoPor = editadoPor;
            this.estuches = estuches;
            this.copiaBR = copiaBR;
            this.menuHecho = menuHecho;
        }
    }
}

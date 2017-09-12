using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Peliculas.POJO;
using System.Data.OleDb;

namespace Peliculas.MODELO
{
    interface iDVDDao
    {
        void insert(POJO.pojoDVD dvd);
        DataTable selectAll();
        DataTable selectCustom(POJO.pojoDVD dvd);
        void delete(POJO.pojoDVD dvd);
        void update(POJO.pojoDVD dvd);
    }
}

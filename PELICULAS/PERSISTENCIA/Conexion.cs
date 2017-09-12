using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using Peliculas.MODELO.Exceptions;

namespace Peliculas.PERSISTENCIA
{
    class Conexion
    {
        private string pDireccion=null;
        private OleDbConnection pConn=null;

        public OleDbConnection conn
        {
            get
            {
                return pConn;
            }
        }
      
        public String direccion
        {
            get
            {
                return pDireccion;
            }
            set
            {
                pDireccion = direccion;
            }

        }

        //Metodo que sirve para elegir la Base de Datos que queremos abrir
        public void Direccion()
        {
            OpenFileDialog archivo;
            pDireccion = "";
            archivo = new OpenFileDialog();
            archivo.Title = "Selección Base de Datos";
            archivo.Filter = "Selecciona una Base de Datos|*.*";
            if(archivo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pDireccion = archivo.FileName;
            }
            Properties.Settings.Default.direccionBD = pDireccion;
            Properties.Settings.Default.Save();
        }
        //Abre la conexión con la Base de Datos
        public void abrirBD()
        {
           if (!File.Exists(Properties.Settings.Default.direccionBD))
           {
             throw new DataBaseNotFoundException();
           }
            pConn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Properties.Settings.Default.direccionBD);
            pConn.Open();
        }

        //Cierra la conexión con la Base de Datos
        public void cerrarBD()
        {
            pConn.Close();
        }

    }
}

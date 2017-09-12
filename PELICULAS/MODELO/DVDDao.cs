using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using Peliculas.MODELO.Exceptions;
using Peliculas.POJO;
namespace Peliculas.MODELO
{
    class DVDDao : iDVDDao
    {
        private String selectAllSql = null;
        private String selectCustomSql = null;
        private String updateSql = null;
        private String deleteSql = null;
        private String insertSql = null;
        private OleDbConnection conn = null;

        public DVDDao(OleDbConnection conn)
        {
            selectAllSql = "SELECT * FROM DVD ";
            selectCustomSql = "SELECT * FROM DVD WHERE 1=1";
            deleteSql = "DELETE FROM dvd WHERE id_Dvd=@idDvd";
            insertSql = "INSERT INTO dvd (titulo,subtitulo,copia_bluray, partes,FECHA_CREACION, series, copia_seg,editado_por, estuches,menu_hecho) VALUES(@titulo,@subtitulo,@copiaBluRay,@fecha,@partes,@series,@copiaSeg,@editadoPor,@estuches,@hechoMenu)";
            this.conn = conn;
        }
        private Boolean esta(pojoDVD dvd)
        {
            Boolean es = false;
            DataTable x = null;
            OleDbCommand command = null;
            OleDbDataAdapter adapter = null;
            if (dvd.idDvd > 0)
            {
                selectCustomSql = selectCustomSql + " AND id_Dvd=" + dvd.idDvd;
            }

            if (!dvd.titulo.Equals(""))
            {
                selectCustomSql = selectCustomSql + " AND titulo = '" + dvd.titulo + "'";
            }

            if (!dvd.subtitulo.Equals(""))
            {
                selectCustomSql = selectCustomSql + " AND subtitulo = '" + dvd.subtitulo + "'";
            }

            if (dvd.copiaBR == 0)
            {
                selectCustomSql = selectCustomSql + " AND copia_BluRay=" + "false";
            }
            else
            {
                if (dvd.copiaBR == 1)
                {
                    selectCustomSql = selectCustomSql + " AND copia_BluRay=" + "true";
                }
            }

            if (dvd.partes > -1)
            {
                selectCustomSql = selectCustomSql + " AND partes=" + dvd.partes;
            }

            if (!dvd.series.Equals(""))
            {
                selectCustomSql = selectCustomSql + " AND series = '" + dvd.series + "'";
            }

            if (!dvd.copiaSeg.Equals(""))
            {
                selectCustomSql = selectCustomSql + " AND copia_seg = '" + dvd.copiaSeg + "'";
            }

            if (dvd.menuHecho == 0)
            {
                selectCustomSql = selectCustomSql + " AND Menu_Hecho=" + "false";
            }
            else
            {
                if (dvd.menuHecho == 1)
                {
                    selectCustomSql = selectCustomSql + " AND Menu_Hecho=" + "true";
                }
            }

            if (!dvd.editadoPor.Equals(""))
            {
                selectCustomSql = selectCustomSql + " AND Editado_Por = '" + dvd.editadoPor + "'";
            }

            if (!dvd.estuches.Equals(""))
            {
                selectCustomSql = selectCustomSql + " AND estuches = '" + dvd.estuches + "'";
            }

            command = new OleDbCommand(selectCustomSql, conn);
            adapter = new OleDbDataAdapter(command);
            x = new DataTable();
            adapter.Fill(x);
            if (x.Rows.Count > 0)
            { es = true; }
            return es;
        
        }
       
        public void insert(POJO.pojoDVD dvd)
        {
            OleDbCommand comando=null;
            if (esta(dvd)) 
            {
                throw new OperacionFallidaException();
            }
            if (this.conn == null)
            {
                throw new NotConnectionException();
            }
            comando = new OleDbCommand(insertSql, conn);
            comando.Parameters.AddWithValue("@titulo", dvd.titulo);
            comando.Parameters.AddWithValue("@subtitulo", dvd.subtitulo);
            comando.Parameters.AddWithValue("@copiaBluRay", dvd.copiaBR);
            comando.Parameters.AddWithValue("@partes", dvd.partes);
            comando.Parameters.AddWithValue("@fecha", dvd.fecha);
            comando.Parameters.AddWithValue("@series", dvd.series);
            comando.Parameters.AddWithValue("@copiaSeg", dvd.copiaSeg);
            comando.Parameters.AddWithValue("@editadoPor", dvd.editadoPor);
            comando.Parameters.AddWithValue("@estuches", dvd.estuches);
            comando.Parameters.AddWithValue("@hechoMenu", dvd.menuHecho);
            comando.ExecuteNonQuery();
        }

        public DataTable selectAll()
        {
            OleDbCommand command = null;
            OleDbDataAdapter adapter = null;
            DataTable resultadoSA = null;
            if (this.conn == null)
            {
                throw new NotConnectionException();
            }
            selectAllSql = "SELECT * FROM dvd";
            command = new OleDbCommand(selectAllSql, conn);
            adapter = new OleDbDataAdapter(command);
            resultadoSA = new DataTable();
            adapter.Fill(resultadoSA);
            selectAllSql = "";
            return resultadoSA;
        }

        public DataTable selectCustom(POJO.pojoDVD dvd)
        {
            if (this.conn == null)
            {
                throw new NotConnectionException();
            }
            DataTable x = null;
            OleDbCommand command = null;
            OleDbDataAdapter adapter = null;
            if (dvd.idDvd > 0)
            {
                selectCustomSql = selectCustomSql + " AND id_Dvd=" + dvd.idDvd;
            }

            if (!dvd.titulo.Equals(""))
            {
                selectCustomSql = selectCustomSql + " AND titulo like '%" + dvd.titulo + "%'";
            }

            if (!dvd.subtitulo.Equals(""))
            {
                selectCustomSql = selectCustomSql + " AND subtitulo LIKE '%" + dvd.subtitulo + "%'";
            }

            if (dvd.copiaBR == 0)
            {
                selectCustomSql = selectCustomSql + " AND copia_BluRay=" + "false";
            }
            else
            {
                if (dvd.copiaBR == 1)
                {
                    selectCustomSql = selectCustomSql + " AND copia_BluRay=" + "true";
                }
            }

            if (dvd.partes > -1)
            {
                selectCustomSql = selectCustomSql + " AND partes=" + dvd.partes;
            }

                       
            if (!dvd.series.Equals(""))
            {
                selectCustomSql = selectCustomSql + " AND series LIKE '%" + dvd.series + "%'";
            }

            if (!dvd.copiaSeg.Equals(""))
            {
                selectCustomSql = selectCustomSql + " AND copia_seg LIKE '%" + dvd.copiaSeg + "%'";
            }

            if (dvd.menuHecho == 0)
            {
                selectCustomSql = selectCustomSql + " AND Menu_Hecho=" + "false";
            }
            else
            {
                if (dvd.menuHecho == 1)
                {
                    selectCustomSql = selectCustomSql + " AND Menu_Hecho=" + "true";
                }
            }

            if (!dvd.editadoPor.Equals(""))
            {
                selectCustomSql = selectCustomSql + " AND Editado_Por LIKE '%" + dvd.editadoPor + "%'";
            }

            if (!dvd.estuches.Equals(""))
            {
                selectCustomSql = selectCustomSql + " AND estuches LIKE '%" + dvd.estuches + "%'";
            }

            command = new OleDbCommand(selectCustomSql, conn);
            adapter = new OleDbDataAdapter(command);
            x = new DataTable();
            adapter.Fill(x);
            return x;
        }

        public void delete(POJO.pojoDVD dvd)
        {
            if (this.conn == null)
            {
                throw new NotConnectionException();
            }
            OleDbCommand comando;
            comando = new OleDbCommand(deleteSql, conn);
            comando.Parameters.AddWithValue("@idDvd", dvd.idDvd);
            comando.ExecuteNonQuery();
        }

        public void update(POJO.pojoDVD dvd)
        {
            if (this.conn == null)
            {
                throw new NotConnectionException();
            }

            
            OleDbCommand comando = null;
            updateSql = "UPDATE dvd SET titulo='" + dvd.titulo + "', subtitulo='" + dvd.subtitulo+"'";
            switch (dvd.copiaBR)
            {
                case 1:
                    updateSql = updateSql + ", copia_BluRay=true";
                    break;
                case 0:
                    updateSql = updateSql + ", copia_BluRay=true";
                    break;
                default:
                    throw new DatoIncorrectoException();
            }

            switch (dvd.menuHecho)
            {
                case 1:
                    updateSql = updateSql + ", menu_hecho=true";
                    break;
                case 0:
                    updateSql = updateSql + ", menu_hecho=false";
                    break;
                default:
                    throw new DatoIncorrectoException();
            }

            updateSql = updateSql + ", copia_Seg='" + dvd.copiaSeg+"'"+ ", editado_por='"+dvd.editadoPor+"'"+", estuches='"+dvd.estuches+"'";
            updateSql = updateSql + ", partes="+dvd.partes+", series='"+dvd.series+"'"+", fecha_creacion=#"+dvd.fecha+"#"+" WHERE id_Dvd= "+dvd.idDvd;
            comando = new OleDbCommand(updateSql, conn);
            comando.ExecuteNonQuery();
            /*comando = new OleDbCommand(updateSql, conn);
            comando.Parameters.AddWithValue("@idDvd", dvd.idDvd);
            comando.Parameters.AddWithValue("@titulo", "'"+dvd.titulo+"'");
            comando.Parameters.AddWithValue("@subtitulo", "'"+dvd.subtitulo+"'");
            if (dvd.copiaBR == 1)
            {
                comando.Parameters.AddWithValue("@copiaBluRay", true);
            }
            else
            {
                comando.Parameters.AddWithValue("@copiaBluRay", false);
            }
          
            comando.Parameters.AddWithValue("@partes", dvd.partes);
            comando.Parameters.AddWithValue("@hechoPor", dvd.hechoPor);
            comando.Parameters.AddWithValue("@series", dvd.series);
            comando.Parameters.AddWithValue("@copiaSeg", dvd.copiaSeg);
            comando.Parameters.AddWithValue("@editadoPor", dvd.editadoPor);
            comando.Parameters.AddWithValue("@estuches", dvd.estuches);
            if (dvd.menuHecho == 1)
            {
                comando.Parameters.AddWithValue("@hechoMenu", true);
            }
            else 
            {
                comando.Parameters.AddWithValue("@hechoMenu", false);
            }*/
            
        }

    }
}

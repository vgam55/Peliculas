using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Peliculas.PERSISTENCIA;
using Peliculas.MODELO;
using Peliculas.POJO;
using Peliculas.MODELO.Exceptions;
namespace Peliculas
{
    public partial class iDVD : Form
    {
        pojoDVD datosForm;
        private Conexion conection = null;
        iDVDDao dvdDaoL = null;
              
        private void inicializarDatosForm()
        {
            datosForm = new pojoDVD(-1, -1, "", "", new DateTime(1900,01,01), "", "", "", "", -1, -1);
        }

        //Comprueba si el contenido de un textbox es númerico o no
        public static Boolean IsNumeric(string valor)
        {
            int result;
            return int.TryParse(valor, out result);
        }

        private void cogerDatosFil()
        {
            if (!tbIdDvdFil.Text.Equals(""))
            {
                if (IsNumeric(tbIdDvdFil.Text))
                { datosForm.idDvd = Int32.Parse(tbIdDvdFil.Text); }
                else
                {
                   throw new DatoIncorrectoException();
                }
            }

            if (!tbTituloDvdFil.Text.Equals(""))
            {
                datosForm.titulo = tbTituloDvdFil.Text;
            }

            if (!tbSeriesFilDvd.Text.Equals(""))
            {
                datosForm.series = tbSeriesFilDvd.Text;
            }

            if (!tbTituloDvdFil.Text.Equals(""))
            {
                datosForm.titulo = tbTituloDvdFil.Text;
            }

            if (!tbSubtituloFil.Text.Equals(""))
            {
                datosForm.subtitulo = tbSubtituloFil.Text;
            }

             datosForm.fecha = dtpFechaFil.Value;
            
            if (rbMenuV.Checked)
            {
                datosForm.menuHecho = 1;
            }
            else
            {
                if (rbMenuF.Checked)
                {
                    datosForm.menuHecho = 0;
                }
            }

            if (rbCopBRV.Checked)
            {
                datosForm.copiaBR = 1;
            }
            else
            {
                if (rbCopBRF.Checked)
                {
                    datosForm.copiaBR = 0;
                }
            }

            if (!tbCopSegFil.Text.Equals(""))
            {
                datosForm.copiaSeg = tbCopSegFil.Text;
            }

            if (!tbEditadorPorFil.Text.Equals(""))
            {
                datosForm.editadoPor = tbEditadorPorFil.Text;
            }

            if (!tbEstuchesFil.Text.Equals(""))
            {
                    datosForm.estuches = tbEstuchesFil.Text;
            }

            if (!tbPartesFil.Text.Equals(""))
            {
                if(IsNumeric(tbPartesFil.Text))
                {
                 datosForm.partes = Int32.Parse(tbPartesFil.Text);
                }
                else
                {
                    throw new DatoIncorrectoException();
                }
                
            }
        }

        private void cogerDatosEd()
        {
            if (!tbIdDvdEd.Text.Equals(""))
            {
                if (IsNumeric(tbIdDvdEd.Text))
                { datosForm.idDvd = Int32.Parse(tbIdDvdEd.Text); }
                else
                {
                    throw new DatoIncorrectoException();
                }
            }

            if (!tbTituloEd.Text.Equals(""))
            {
                datosForm.titulo = tbTituloEd.Text;
            }

            if (!tbSeriesEd.Text.Equals(""))
            {
                datosForm.series = tbSeriesEd.Text;
            }

            if (!tbSubtituloEd.Text.Equals(""))
            {
                datosForm.subtitulo = tbSubtituloEd.Text;
            }
            String resultado = String.Format("{0:d}", dtpFechaED.Value);

            datosForm.fecha = Convert.ToDateTime(resultado);

            if (rbMenuVEd.Checked)
            {
                datosForm.menuHecho = 1;
            }
            else
            {
                datosForm.menuHecho = 0;
            }

            if (rbCopiaBRVEd.Checked)
            {
                datosForm.copiaBR = 1;
            }
            else
            {
                datosForm.copiaBR = 0;
            }

            if (!tbCopSegEd.Text.Equals(""))
            {
                datosForm.copiaSeg = tbCopSegEd.Text;
            }

            if (!tbEditadoPorEd.Text.Equals(""))
            {
                datosForm.editadoPor = tbEditadoPorEd.Text;
            }

            if (!tbEstuchesEd.Text.Equals(""))
            {
                datosForm.estuches = tbEstuchesEd.Text;
            }

            if (!tbPartesEd.Text.Equals("") && IsNumeric(tbPartesEd.Text))
            {
                datosForm.partes = Int32.Parse(tbPartesEd.Text);
            }
        }

        private void limpiarDatosEd()
        {
            datosForm = new pojoDVD(-1, -1, "", "", new DateTime(1900,01,01), "", "", "", "", -1, -1);
            tbCopSegEd.Text = "";
            tbEditadoPorEd.Text = "";
            tbEstuchesEd.Text = "";
            dtpFechaED.Value = DateTime.Today;
            tbIdDvdEd.Text = "";
            tbPartesEd.Text = "";
            tbSeriesEd.Text = "";
            tbSubtituloEd.Text = "";
            tbTituloEd.Text = "";
            rbCopiaBRFEd.Checked = false;
            rbCopiaBRVEd.Checked = false;
            rbMenuFEd.Checked = false;
            rbMenuVEd.Checked = false;
            lblMensajesEd.Text = "";
        }

        private void limpiarDatosFil()
        {
            datosForm = new pojoDVD(-1, -1, "", "", new DateTime(1900, 01, 01), "", "", "", "", -1, -1);
            tbCopSegFil.Text = "";
            tbEditadorPorFil.Text = "";
            tbEstuchesFil.Text = "";
            dtpFechaFil.Value = DateTime.Today;
            tbIdDvdFil.Text = "";
            tbPartesFil.Text = "";
            tbSeriesFilDvd.Text =
            tbSubtituloFil.Text = "";
            tbTituloDvdFil.Text = "";
            limpiarMenu();
            limpiarRbBR();

        }

        public iDVD()
        {
            InitializeComponent();
        }

        private void abrirBD()
        {
            try
            {
                conection = new Conexion();
                conection.abrirBD();
            }
            catch (DataBaseNotFoundException dbnf)
            {
                while (!File.Exists(Properties.Settings.Default.direccionBD))
                {
                    // conection = new Conexion();
                    //dvdDaoL = new DVDDao(conection.conn);
                    conection.Direccion();
                }
            }
            finally
            {
                conection.abrirBD();
            }

            try
            {
                dvdDaoL = new DVDDao(conection.conn);
                dgvDVD.DataSource = dvdDaoL.selectAll();
                dvdDaoL = null;
                lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros.";
                inicializarDatosForm();
            }
            catch (NotConnectionException nce)
            {
                lblMensajesFil.Text = nce.rMensaje(nce.Message);
            }
            catch (OperacionFallidaException ofe)
            {
                lblMensajesFil.Text = ofe.rMensaje();

            }
        }
       
        private void iDVD_Load(object sender, EventArgs e)
        {
            abrirBD();           
        }

        private void tbIdDvdFil_TextChanged(object sender, EventArgs e)
        {
            try
            {
                inicializarDatosForm();
                dvdDaoL = new DVDDao(conection.conn);
                cogerDatosFil();
                dgvDVD.DataSource = dvdDaoL.selectCustom(datosForm);
                dvdDaoL = null;
                lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros.";
            }
            catch (DatoIncorrectoException di)
            {
                lblMensajesFil.Text = di.rMensaje();
            }
            catch (OperacionFallidaException of)
            {
                lblMensajesFil.Text = of.rMensaje()+" Ya hay un registro con esos datos";
            }
            catch (NotConnectionException nce)
            {
                lblMensajesFil.Text = nce.rMensaje();
            }
        }

        private void tbTituloDvdFil_TextChanged(object sender, EventArgs e)
        {
            try
            {
                inicializarDatosForm();
                dvdDaoL = new DVDDao(conection.conn);
                cogerDatosFil();
                dgvDVD.DataSource = dvdDaoL.selectCustom(datosForm);
                dvdDaoL = null;
                lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros.";
            }
            catch (DatoIncorrectoException di)
            {
                lblMensajesFil.Text = di.rMensaje() ;
            }
            catch (OperacionFallidaException of)
            {
                lblMensajesFil.Text = of.rMensaje() + " Ya hay un registro con esos datos";
            }
            catch (NotConnectionException nce)
            {
                lblMensajesFil.Text = nce.rMensaje();
            }
        }

        private void tbSubtituloFil_TextChanged(object sender, EventArgs e)
        {

            try
            {
                inicializarDatosForm();
                dvdDaoL = new DVDDao(conection.conn);
                cogerDatosFil();
                dgvDVD.DataSource = dvdDaoL.selectCustom(datosForm);
                dvdDaoL = null;
                lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros.";
            }
            catch (DatoIncorrectoException di)
            {
                lblMensajesFil.Text = di.rMensaje();
            }
            catch (OperacionFallidaException of)
            {
                lblMensajesFil.Text = of.rMensaje() + " Ya hay un registro con esos datos";
            }
            catch (NotConnectionException nce)
            {
                lblMensajesFil.Text = nce.rMensaje();
            }
        }

        private void tbPartesFil_TextChanged(object sender, EventArgs e)
        {
            try
            {
                inicializarDatosForm();
                dvdDaoL = new DVDDao(conection.conn);
                cogerDatosFil();
                dgvDVD.DataSource = dvdDaoL.selectCustom(datosForm);
                dvdDaoL = null;
                lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros.";
            }
            catch (DatoIncorrectoException di)
            {
                lblMensajesFil.Text = di.rMensaje();
            }
            catch (OperacionFallidaException of)
            {
                lblMensajesFil.Text = of.rMensaje() + " Ya hay un registro con esos datos";
            }
            catch (NotConnectionException nce)
            {
                lblMensajesFil.Text = nce.rMensaje();
            }
        }

        private void tbSeriesFilDvd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                inicializarDatosForm();
                dvdDaoL = new DVDDao(conection.conn);
                cogerDatosFil();
                dgvDVD.DataSource = dvdDaoL.selectCustom(datosForm);
                dvdDaoL = null;
                lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros.";
            }
            catch (DatoIncorrectoException di)
            {
                lblMensajesFil.Text = di.rMensaje();
            }
            catch (OperacionFallidaException of)
            {
                lblMensajesFil.Text = of.rMensaje() + " Ya hay un registro con esos datos";
            }
            catch (NotConnectionException nce)
            {
                lblMensajesFil.Text = nce.rMensaje();
            }
        }

        private void tbCopSegFil_TextChanged(object sender, EventArgs e)
        {
            try
            {
                inicializarDatosForm();
                dvdDaoL = new DVDDao(conection.conn);
                cogerDatosFil();
                dgvDVD.DataSource = dvdDaoL.selectCustom(datosForm);
                dvdDaoL = null;
                lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros.";
            }
            catch (DatoIncorrectoException di)
            {
                lblMensajesFil.Text = di.rMensaje();
            }
            catch (OperacionFallidaException of)
            {
                lblMensajesFil.Text = of.rMensaje() + " Ya hay un registro con esos datos";
            }
            catch (NotConnectionException nce)
            {
                lblMensajesFil.Text = nce.rMensaje();
            }
        }

        private void tbEditadorPorFil_TextChanged(object sender, EventArgs e)
        {
            try
            {
                inicializarDatosForm();
                dvdDaoL = new DVDDao(conection.conn);
                cogerDatosFil();
                dgvDVD.DataSource = dvdDaoL.selectCustom(datosForm);
                dvdDaoL = null;
                lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros.";
            }
            catch (DatoIncorrectoException di)
            {
                lblMensajesFil.Text = di.rMensaje();
            }
            catch (OperacionFallidaException of)
            {
                lblMensajesFil.Text = of.rMensaje() + " Ya hay un registro con esos datos";
            }
            catch (NotConnectionException nce)
            {
                lblMensajesFil.Text = nce.rMensaje();
            }
        }

        private void tbEstuchesFil_TextChanged(object sender, EventArgs e)
        {
            try
            {
                inicializarDatosForm();
                dvdDaoL = new DVDDao(conection.conn);
                cogerDatosFil();
                dgvDVD.DataSource = dvdDaoL.selectCustom(datosForm);
                dvdDaoL = null;
                lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros.";
            }
            catch (DatoIncorrectoException di)
            {
                lblMensajesFil.Text = di.rMensaje();
            }
            catch (OperacionFallidaException of)
            {
                lblMensajesFil.Text = of.rMensaje() + " Ya hay un registro con esos datos";
            }
            catch (NotConnectionException nce)
            {
                lblMensajesFil.Text = nce.rMensaje();
            }
        }

        private void tbHechoPorFil_TextChanged(object sender, EventArgs e)
        {
            try
            {
                inicializarDatosForm();
                dvdDaoL = new DVDDao(conection.conn);
                cogerDatosFil();
                dgvDVD.DataSource = dvdDaoL.selectCustom(datosForm);
                dvdDaoL = null;
                lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros.";
            }
            catch (DatoIncorrectoException di)
            {
                lblMensajesFil.Text = di.rMensaje();
            }
            catch (OperacionFallidaException of)
            {
                lblMensajesFil.Text = of.rMensaje() + " Ya hay un registro con esos datos";
            }
            catch (NotConnectionException nce)
            {
                lblMensajesFil.Text = nce.rMensaje();
            }
        }

        private void rbCopBRV_Click(object sender, EventArgs e)
        {
            try
            {
                inicializarDatosForm();
                dvdDaoL = new DVDDao(conection.conn);
                cogerDatosFil();
                dgvDVD.DataSource = dvdDaoL.selectCustom(datosForm);
                dvdDaoL = null;
                lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros.";
            }
            catch (DatoIncorrectoException di)
            {
                lblMensajesFil.Text = di.rMensaje();
            }
            catch (OperacionFallidaException of)
            {
                lblMensajesFil.Text = of.rMensaje() + " Ya hay un registro con esos datos";
            }
            catch (NotConnectionException nce)
            {
                lblMensajesFil.Text = nce.rMensaje();
            }
        }

        private void rbCopBRF_Click(object sender, EventArgs e)
        {
            try
            {
                inicializarDatosForm();
                dvdDaoL = new DVDDao(conection.conn);
                cogerDatosFil();
                dgvDVD.DataSource = dvdDaoL.selectCustom(datosForm);
                dvdDaoL = null;
                lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros.";
            }
            catch (DatoIncorrectoException di)
            {
                lblMensajesFil.Text = di.rMensaje();
            }
            catch (OperacionFallidaException of)
            {
                lblMensajesFil.Text = of.rMensaje() + " Ya hay un registro con esos datos";
            }
            catch (NotConnectionException nce)
            {
                lblMensajesFil.Text = nce.rMensaje();
            }
        }

        private void rbMenuV_Click(object sender, EventArgs e)
        {
            try
            {
                inicializarDatosForm();
                dvdDaoL = new DVDDao(conection.conn);
                cogerDatosFil();
                dgvDVD.DataSource = dvdDaoL.selectCustom(datosForm);
                dvdDaoL = null;
                lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros.";
            }
            catch (DatoIncorrectoException di)
            {
                lblMensajesFil.Text = di.rMensaje();
            }
            catch (OperacionFallidaException of)
            {
                lblMensajesFil.Text = of.rMensaje() + " Ya hay un registro con esos datos";
            }
            catch (NotConnectionException nce)
            {
                lblMensajesFil.Text = nce.rMensaje();
            }
        }

        private void rbMenuF_Click(object sender, EventArgs e)
        {
            try
            {
                inicializarDatosForm();
                dvdDaoL = new DVDDao(conection.conn);
                cogerDatosFil();
                dgvDVD.DataSource = dvdDaoL.selectCustom(datosForm);
                dvdDaoL = null;
                lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros.";
            }
            catch (DatoIncorrectoException di)
            {
                lblMensajesFil.Text = di.rMensaje();
            }
            catch (OperacionFallidaException of)
            {
                lblMensajesFil.Text = of.rMensaje() + " Ya hay un registro con esos datos";
            }
            catch (NotConnectionException nce)
            {
                lblMensajesFil.Text = nce.rMensaje();
            }
       }

        private void limpiarRbBR()
        {
               rbCopBRV.Checked = false;
               rbCopBRF.Checked = false; 
        }
      
        private void btLimpiarBR_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarRbBR();
                inicializarDatosForm();
                cogerDatosFil();
                dvdDaoL = new DVDDao(conection.conn);
                cogerDatosFil();
                dgvDVD.DataSource = dvdDaoL.selectCustom(datosForm);
                dvdDaoL = null;
                lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros.";
            }
            catch (DatoIncorrectoException di)
            {
                lblMensajesFil.Text = di.rMensaje();
            }
            catch (OperacionFallidaException of)
            {
                lblMensajesFil.Text = of.rMensaje() + " Ya hay un registro con esos datos";
            }
            catch (NotConnectionException nce)
            {
                lblMensajesFil.Text = nce.rMensaje();
            }
        }

        private void limpiarMenu()
        {
            rbMenuF.Checked = false;
            rbMenuV.Checked = false;
         }

        private void btLimpiarMenu_Click(object sender, EventArgs e)
        {
            try
            {
                limpiarMenu();
                inicializarDatosForm();
                cogerDatosFil();
                dvdDaoL = new DVDDao(conection.conn);
                cogerDatosFil();
                dgvDVD.DataSource = dvdDaoL.selectCustom(datosForm);
                dvdDaoL = null;
                lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros.";
            }
            catch (DatoIncorrectoException di)
            {
                lblMensajesFil.Text = di.rMensaje();
            }
            catch (OperacionFallidaException of)
            {
                lblMensajesFil.Text = of.rMensaje() + " Ya hay un registro con esos datos";
            }
            catch (NotConnectionException nce)
            {
                lblMensajesFil.Text = nce.rMensaje();
            }
        }

        private void btLimpiarFilt_Click(object sender, EventArgs e)
        {
            limpiarDatosFil();
            dvdDaoL = new DVDDao(conection.conn);
            cogerDatosFil();
            dgvDVD.DataSource = dvdDaoL.selectAll();
            dvdDaoL = null;
            lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros."; 
        }

        private void dgvDVD_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (DBNull.Value.Equals(Convert.ToString(dgvDVD.CurrentRow.Cells[0].Value)))
                {
                    tbIdDvdEd.Text = "";
                }
                else 
                {
                    tbIdDvdEd.Text = Convert.ToString(dgvDVD.CurrentRow.Cells[0].Value);
                }


                if (DBNull.Value.Equals(Convert.ToString(dgvDVD.CurrentRow.Cells[1].Value)))
                {
                    tbTituloEd.Text = "";
                }
                else
                {
                    tbTituloEd.Text = Convert.ToString(dgvDVD.CurrentRow.Cells[1].Value);
                }

                if (DBNull.Value.Equals(Convert.ToString(dgvDVD.CurrentRow.Cells[2].Value)))
                {
                    tbSubtituloEd.Text = "";
                }
                else
                {
                    tbSubtituloEd.Text = Convert.ToString(dgvDVD.CurrentRow.Cells[2].Value);
                }

                
                if (Convert.ToBoolean(dgvDVD.CurrentRow.Cells[3].Value) == true)
                { rbCopiaBRVEd.Checked = true; }
                else
                { rbCopiaBRFEd.Checked = true; }

                if (DBNull.Value.Equals( Convert.ToString(dgvDVD.CurrentRow.Cells[4].Value)))
                {
                    tbPartesEd.Text = "";
                }
                else
                {
                    tbPartesEd.Text = Convert.ToString(dgvDVD.CurrentRow.Cells[4].Value);
                }

                if(dgvDVD.CurrentRow.Cells[5].Value.Equals(DBNull.Value))              
                {
                    dtpFechaED.Value = DateTime.Today;
                }
                else
                {
                   dtpFechaED.Value  = Convert.ToDateTime(dgvDVD.CurrentRow.Cells[5].Value);
                }

                if (DBNull.Value.Equals( Convert.ToString(dgvDVD.CurrentRow.Cells[6].Value)))
                {
                    tbSeriesEd.Text = "";
                }
                else
                {
                   tbSeriesEd.Text = Convert.ToString(dgvDVD.CurrentRow.Cells[6].Value);
                }

                if (DBNull.Value.Equals(Convert.ToString(dgvDVD.CurrentRow.Cells[7].Value)))
                {
                    tbCopSegEd.Text = "";
                }
                else
                {
                    tbCopSegEd.Text = Convert.ToString(dgvDVD.CurrentRow.Cells[7].Value);
                }

                if (DBNull.Value.Equals(Convert.ToString(dgvDVD.CurrentRow.Cells[8].Value)))
                {
                    tbEditadoPorEd.Text = "";
                }
                else
                {
                    tbEditadoPorEd.Text = Convert.ToString(dgvDVD.CurrentRow.Cells[8].Value);
                }

                if (DBNull.Value.Equals(Convert.ToString(dgvDVD.CurrentRow.Cells[9].Value)))
                {
                    tbEstuchesEd.Text = "";
                }
                else
                {
                    tbEstuchesEd.Text = Convert.ToString(dgvDVD.CurrentRow.Cells[9].Value);
                }
               
                
                if (Convert.ToBoolean(dgvDVD.CurrentRow.Cells[10].Value) == true)
                {
                    rbMenuVEd.Checked = true;
                }
                else
                {
                    rbMenuFEd.Checked = true;
                }
            }
            catch(DatoIncorrectoException di)
            { lblMensajesFil.Text = di.rMensaje(); }
        }

        private void btLimpiarEd_Click(object sender, EventArgs e)
        {
            limpiarDatosEd();
            try
            {
                dvdDaoL = new DVDDao(conection.conn);
                cogerDatosEd();
                dgvDVD.DataSource = dvdDaoL.selectAll();
                dvdDaoL = null;
                lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros.";
            }
            catch (OperacionFallidaException of)
            {
                lblMensajesEd.Text = of.rMensaje();
            }

        }

        private void btActualizarEdi_Click(object sender, EventArgs e)
        {
            try
            {
                dvdDaoL = new DVDDao(conection.conn);
                cogerDatosEd();
                dvdDaoL.update(datosForm);
                dgvDVD.DataSource = dvdDaoL.selectCustom(datosForm);
                dvdDaoL = null;
                inicializarDatosForm();
                lblMensajesEd.Text = "Registro actualizado con exito";
                lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros.";
            }
            catch (DatoIncorrectoException di)
            {
                lblMensajesEd.Text = di.rMensaje();
            }
            catch (OperacionFallidaException of)
            {
                lblMensajesEd.Text = of.rMensaje();
            }
            catch (NotConnectionException nce)
            {
                lblMensajesEd.Text = nce.rMensaje();
            }
        }

        private void btGuardarEdi_Click(object sender, EventArgs e)
        {
            try
            {
                dvdDaoL = new DVDDao(conection.conn);
                cogerDatosEd();
                dvdDaoL.insert(datosForm);               
                dgvDVD.DataSource = dvdDaoL.selectAll();
                dvdDaoL = null;
                inicializarDatosForm();
                lblMensajesEd.Text = "Registro guardado con exito";
                lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros.";
            }
            catch (DatoIncorrectoException di)
            {
                lblMensajesEd.Text = di.rMensaje();
            }
            catch (OperacionFallidaException of)
            {
                lblMensajesEd.Text = of.rMensaje() ;
            }
            catch (NotConnectionException nce)
            {
                lblMensajesEd.Text = nce.rMensaje();
            }
        }

        private void btBorrarEdi_Click(object sender, EventArgs e)
        {
            try
            {
                dvdDaoL = new DVDDao(conection.conn);
                cogerDatosEd();
                dvdDaoL.delete(datosForm);
                dgvDVD.DataSource = dvdDaoL.selectAll();
                dvdDaoL = null;
                inicializarDatosForm();
                lblMensajesEd.Text = "Registro borrado con exito";
                lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros.";
            }
            catch (DatoIncorrectoException di)
            {
                lblMensajesEd.Text = di.rMensaje();
            }
            catch (OperacionFallidaException of)
            {
                lblMensajesEd.Text = of.rMensaje() + " Ya hay un registro con esos datos";
            }
            catch (NotConnectionException nce)
            {
                lblMensajesEd.Text = nce.rMensaje();
            }
        }

        private void btLimpiarBREd_Click(object sender, EventArgs e)
        {
            rbCopiaBRFEd.Checked = false;
            rbCopiaBRVEd.Checked = false;
        }

        private void btLimpiarMenuEd_Click(object sender, EventArgs e)
        {
            rbMenuFEd.Checked = false;
            rbMenuVEd.Checked= false;
        }

        private void btCerrar_Click(object sender, EventArgs e)
        {
            conection.cerrarBD();
            this.Dispose();
        }

        private void msAbrirBDToolStrip_Click(object sender, EventArgs e)
        {
            conection.Direccion();
            abrirBD();
            lblMensajesFil.Text = "Hay " + dgvDVD.RowCount + " registros.";
        }

        private void cerrarProgramaToolStrip_Click(object sender, EventArgs e)
        {
            conection.cerrarBD();
            this.Dispose();
        }

        private void dgvDVD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (DBNull.Value.Equals(Convert.ToString(dgvDVD.CurrentRow.Cells[0].Value)))
                {
                    tbIdDvdEd.Text = "";
                }
                else
                {
                    tbIdDvdEd.Text = Convert.ToString(dgvDVD.CurrentRow.Cells[0].Value);
                }


                if (DBNull.Value.Equals(Convert.ToString(dgvDVD.CurrentRow.Cells[1].Value)))
                {
                    tbTituloEd.Text = "";
                }
                else
                {
                    tbTituloEd.Text = Convert.ToString(dgvDVD.CurrentRow.Cells[1].Value);
                }

                if (DBNull.Value.Equals(Convert.ToString(dgvDVD.CurrentRow.Cells[2].Value)))
                {
                    tbSubtituloEd.Text = "";
                }
                else
                {
                    tbSubtituloEd.Text = Convert.ToString(dgvDVD.CurrentRow.Cells[2].Value);
                }


                if (Convert.ToBoolean(dgvDVD.CurrentRow.Cells[3].Value) == true)
                { rbCopiaBRVEd.Checked = true; }
                else
                { rbCopiaBRFEd.Checked = true; }

                if (DBNull.Value.Equals(Convert.ToString(dgvDVD.CurrentRow.Cells[4].Value)))
                {
                    tbPartesEd.Text = "";
                }
                else
                {
                    tbPartesEd.Text = Convert.ToString(dgvDVD.CurrentRow.Cells[4].Value);
                }

                if (dgvDVD.CurrentRow.Cells[5].Value.Equals(DBNull.Value))
                {
                    dtpFechaED.Value = DateTime.Today;
                }
                else
                {
                    dtpFechaED.Value = Convert.ToDateTime(dgvDVD.CurrentRow.Cells[5].Value);
                }

                if (DBNull.Value.Equals(Convert.ToString(dgvDVD.CurrentRow.Cells[6].Value)))
                {
                    tbSeriesEd.Text = "";
                }
                else
                {
                    tbSeriesEd.Text = Convert.ToString(dgvDVD.CurrentRow.Cells[6].Value);
                }

                if (DBNull.Value.Equals(Convert.ToString(dgvDVD.CurrentRow.Cells[7].Value)))
                {
                    tbCopSegEd.Text = "";
                }
                else
                {
                    tbCopSegEd.Text = Convert.ToString(dgvDVD.CurrentRow.Cells[7].Value);
                }

                if (DBNull.Value.Equals(Convert.ToString(dgvDVD.CurrentRow.Cells[8].Value)))
                {
                    tbEditadoPorEd.Text = "";
                }
                else
                {
                    tbEditadoPorEd.Text = Convert.ToString(dgvDVD.CurrentRow.Cells[8].Value);
                }

                if (DBNull.Value.Equals(Convert.ToString(dgvDVD.CurrentRow.Cells[9].Value)))
                {
                    tbEstuchesEd.Text = "";
                }
                else
                {
                    tbEstuchesEd.Text = Convert.ToString(dgvDVD.CurrentRow.Cells[9].Value);
                }


                if (Convert.ToBoolean(dgvDVD.CurrentRow.Cells[10].Value) == true)
                {
                    rbMenuVEd.Checked = true;
                }
                else
                {
                    rbMenuFEd.Checked = true;
                }
            }
            catch (DatoIncorrectoException di)
            { lblMensajesFil.Text = di.rMensaje(); }
        }
              
    }
}

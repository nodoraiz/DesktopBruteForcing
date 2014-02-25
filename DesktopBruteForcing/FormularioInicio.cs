using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;


/**
    Copyright (C) 2014 Miguel Angel García
  
    This file is part of DesktopBruteForcing.

    DesktopBruteForcing is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    DesktopBruteForcing is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with DesktopBruteForcing.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * 
 * 
 * 
 * Icon made by Freepik from Flaticon.com
*/

namespace DesktopBruteForcing
{
    public partial class FormularioInicio : Form
    {
        #region Imports
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", EntryPoint = "GetWindowTextLength", SetLastError = true)]
        internal static extern int GetWindowTextLength(IntPtr hwnd);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, [Out, MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpString, int nMaxCount);
        #endregion

        #region Constantes
        private const string SEPARADOR_TIEMPO = "##";
        private const string PALABRA_RESERVADA = "$$";
        private const string FICHERO_LOG = "log.txt";
        #endregion

        #region Propiedades
        private List<string> Diccionario { get; set; }
        private bool AtaqueEnMarcha { get; set; }
        private string TituloVentanaAtaque { get; set; }
        private IntPtr? PunteroVentanaAplicacion { get; set; }
        #endregion

        #region Constructor
        public FormularioInicio()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        #endregion

        #region Eventos
        private void Form1_Activated(object sender, EventArgs e)
        {
            if (this.PunteroVentanaAplicacion == null)
            {
                this.PunteroVentanaAplicacion = GetForegroundWindow();
            }
        }

        private void dButtonExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog lObjDialog = new OpenFileDialog();
            lObjDialog.Multiselect = false;
            if (lObjDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.dTextBoxRuta.Text = lObjDialog.FileName;
            }
        }

        private void dButtonIniciar_Click(object sender, EventArgs e)
        {
            if (!this.AtaqueEnMarcha && this.ValidarFormulario())
            {
                this.PrepararDiccionarioAtaque();

                MessageBox.Show("After you close this window you have 3 seconds to set the focus on the field you want to brute force",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Thread.Sleep(3000);

                Thread lObjHilo = new Thread(new ThreadStart(this.IniciarAtaque));
                lObjHilo.Start();
            }
        }
        #endregion

        #region Miembros privados
        public void Registro(string pStrMensaje, bool pBlnIncluirFecha = true)
        {
            string lStrMensaje = "";

            if (pBlnIncluirFecha)
            {
                lStrMensaje = "[" + DateTime.Now + "] ";
            }

            lStrMensaje += pStrMensaje + Environment.NewLine;

            this.dTextBoxLog.Text = lStrMensaje + this.dTextBoxLog.Text;
        }

        private string ObtenerTextoDeTituloVentanaFoco()
        {
            IntPtr lPtrVentana = GetForegroundWindow();
            int lIntLongitud = GetWindowTextLength(lPtrVentana);
            StringBuilder lObjStringBuilder = new StringBuilder(lIntLongitud + 1);
            GetWindowText(lPtrVentana, lObjStringBuilder, lObjStringBuilder.Capacity);
            return lObjStringBuilder.ToString();
        }

        private void IniciarAtaque()
        {
            try
            {
                string lStrMensaje = "";

                this.TituloVentanaAtaque = this.ObtenerTextoDeTituloVentanaFoco();

                foreach (string lStrPalabra in this.Diccionario)
                {
                    this.Registro("Trying the password -" + lStrPalabra + "-");

                    string lStrPulsacionFinal = this.dTextBoxPulsacionesEntrePalabras.Text.Replace(PALABRA_RESERVADA, lStrPalabra);
                    while (lStrPulsacionFinal.Length > 0)
                    {
                        if (this.PunteroVentanaAplicacion.Value == GetForegroundWindow())
                        {
                            lStrMensaje = "Process cancelled by the user";
                            break;
                        }

                        string lStrToken = lStrPulsacionFinal;

                        int lIntInicio = lStrPulsacionFinal.IndexOf(SEPARADOR_TIEMPO);
                        int lIntFin = lStrPulsacionFinal.IndexOf(SEPARADOR_TIEMPO, lIntInicio + 1);

                        if (lIntInicio != -1 && lIntFin != -1)
                        {
                            lStrToken = lStrPulsacionFinal
                                .Substring(0, lIntInicio);
                        }
                        else if ((lIntInicio != -1 && lIntFin == -1) || (lIntInicio == -1 && lIntFin != -1))
                        {
                            lStrMensaje = "Syntax error, process cancelled";
                            break;
                        }

                        SendKeys.SendWait(lStrToken);

                        if (lIntInicio != -1 && lIntFin != -1)
                        {
                            string lStrValor = lStrPulsacionFinal.Substring(lIntInicio + SEPARADOR_TIEMPO.Length, lIntFin - lIntInicio - SEPARADOR_TIEMPO.Length);
                            int lIntValor = int.Parse(lStrValor);

                            Thread.Sleep(lIntValor);
                        }

                        lStrPulsacionFinal = lStrPulsacionFinal.Substring(lIntFin + SEPARADOR_TIEMPO.Length);
                    }

                    if (this.TituloVentanaAtaque != this.ObtenerTextoDeTituloVentanaFoco())
                    {
                        lStrMensaje = "The process identifies the password -" + lStrPalabra + "-";
                        this.Registro(lStrMensaje);
                        break;
                    }


                    if (lStrMensaje != "")
                    {
                        break;
                    }
                }

                if (lStrMensaje == "")
                {
                    lStrMensaje = "Password not found";
                }

                MessageBox.Show(lStrMensaje, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception lObjExcepcion)
            {
                this.Registro("An exception was caught: " + lObjExcepcion.ToString());
            }
        }

        private void PrepararDiccionarioAtaque()
        {
            this.Diccionario = File.ReadAllLines(this.dTextBoxRuta.Text).ToList();
        }

        private bool ValidarFormulario()
        {
            bool lBlnResultado = true;

            if (!File.Exists(this.dTextBoxRuta.Text))
            {
                MessageBox.Show("A dictionary file it's needed in order to brute force a form", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lBlnResultado = false;
            }

            return lBlnResultado;
        }
        #endregion
    }
}
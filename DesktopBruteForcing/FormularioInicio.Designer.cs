namespace DesktopBruteForcing
{
    partial class FormularioInicio
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormularioInicio));
            this.dTextBoxRuta = new System.Windows.Forms.TextBox();
            this.dButtonExaminar = new System.Windows.Forms.Button();
            this.dButtonIniciar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dTextBoxPulsacionesEntrePalabras = new System.Windows.Forms.TextBox();
            this.Log = new System.Windows.Forms.GroupBox();
            this.dTextBoxLog = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.Log.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dTextBoxRuta
            // 
            this.dTextBoxRuta.Location = new System.Drawing.Point(7, 25);
            this.dTextBoxRuta.Name = "dTextBoxRuta";
            this.dTextBoxRuta.Size = new System.Drawing.Size(449, 20);
            this.dTextBoxRuta.TabIndex = 0;
            this.dTextBoxRuta.Text = "./Top100pwd.txt";
            // 
            // dButtonExaminar
            // 
            this.dButtonExaminar.Location = new System.Drawing.Point(462, 23);
            this.dButtonExaminar.Name = "dButtonExaminar";
            this.dButtonExaminar.Size = new System.Drawing.Size(75, 22);
            this.dButtonExaminar.TabIndex = 2;
            this.dButtonExaminar.Text = "Search";
            this.dButtonExaminar.UseVisualStyleBackColor = true;
            this.dButtonExaminar.Click += new System.EventHandler(this.dButtonExaminar_Click);
            // 
            // dButtonIniciar
            // 
            this.dButtonIniciar.Location = new System.Drawing.Point(222, 143);
            this.dButtonIniciar.Name = "dButtonIniciar";
            this.dButtonIniciar.Size = new System.Drawing.Size(126, 23);
            this.dButtonIniciar.TabIndex = 3;
            this.dButtonIniciar.Text = "Smash the form!";
            this.dButtonIniciar.UseVisualStyleBackColor = true;
            this.dButtonIniciar.Click += new System.EventHandler(this.dButtonIniciar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dTextBoxRuta);
            this.groupBox1.Controls.Add(this.dButtonExaminar);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(543, 66);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Path to dictionary file";
            // 
            // dTextBoxPulsacionesEntrePalabras
            // 
            this.dTextBoxPulsacionesEntrePalabras.Location = new System.Drawing.Point(6, 19);
            this.dTextBoxPulsacionesEntrePalabras.Name = "dTextBoxPulsacionesEntrePalabras";
            this.dTextBoxPulsacionesEntrePalabras.Size = new System.Drawing.Size(527, 20);
            this.dTextBoxPulsacionesEntrePalabras.TabIndex = 3;
            this.dTextBoxPulsacionesEntrePalabras.Text = "$${ENTER}{ENTER}##100##";
            // 
            // Log
            // 
            this.Log.Controls.Add(this.dTextBoxLog);
            this.Log.Location = new System.Drawing.Point(15, 172);
            this.Log.Name = "Log";
            this.Log.Size = new System.Drawing.Size(543, 118);
            this.Log.TabIndex = 10;
            this.Log.TabStop = false;
            this.Log.Text = "Log";
            // 
            // dTextBoxLog
            // 
            this.dTextBoxLog.Location = new System.Drawing.Point(7, 19);
            this.dTextBoxLog.Multiline = true;
            this.dTextBoxLog.Name = "dTextBoxLog";
            this.dTextBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dTextBoxLog.Size = new System.Drawing.Size(530, 93);
            this.dTextBoxLog.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dTextBoxPulsacionesEntrePalabras);
            this.groupBox2.Location = new System.Drawing.Point(16, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(542, 53);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Keystrokes";
            // 
            // FormularioInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 300);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Log);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dButtonIniciar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormularioInicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DeBF";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Log.ResumeLayout(false);
            this.Log.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox dTextBoxRuta;
        private System.Windows.Forms.Button dButtonExaminar;
        private System.Windows.Forms.Button dButtonIniciar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox dTextBoxPulsacionesEntrePalabras;
        private System.Windows.Forms.GroupBox Log;
        private System.Windows.Forms.TextBox dTextBoxLog;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}


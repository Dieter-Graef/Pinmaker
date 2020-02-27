using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Drawing.Imaging;
namespace STM32_Pinmaker
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.Datei = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.GPIODatei = new System.Windows.Forms.TextBox();
            this.gpioladen = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.csvPfad = new System.Windows.Forms.Button();
            this.csv = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.cubepfad = new System.Windows.Forms.TextBox();
            this.pfadcube = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.chip = new System.Windows.Forms.TextBox();
            this.chipload = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Datei
            // 
            this.Datei.Location = new System.Drawing.Point(11, 218);
            this.Datei.Name = "Datei";
            this.Datei.Size = new System.Drawing.Size(670, 20);
            this.Datei.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 195);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "csv";
            // 
            // GPIODatei
            // 
            this.GPIODatei.Location = new System.Drawing.Point(12, 172);
            this.GPIODatei.Name = "GPIODatei";
            this.GPIODatei.Size = new System.Drawing.Size(669, 20);
            this.GPIODatei.TabIndex = 3;
            // 
            // gpioladen
            // 
            this.gpioladen.Location = new System.Drawing.Point(700, 172);
            this.gpioladen.Name = "gpioladen";
            this.gpioladen.Size = new System.Drawing.Size(88, 20);
            this.gpioladen.TabIndex = 4;
            this.gpioladen.Text = "load";
            this.gpioladen.UseVisualStyleBackColor = true;
            this.gpioladen.Click += new System.EventHandler(this.gpioladen_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(274, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Chip Family - GPIO-STM32.....xml";
            // 
            // csvPfad
            // 
            this.csvPfad.Location = new System.Drawing.Point(700, 73);
            this.csvPfad.Name = "csvPfad";
            this.csvPfad.Size = new System.Drawing.Size(88, 22);
            this.csvPfad.TabIndex = 6;
            this.csvPfad.Text = "set";
            this.csvPfad.UseVisualStyleBackColor = true;
            this.csvPfad.Click += new System.EventHandler(this.csvPfad_Click);
            // 
            // csv
            // 
            this.csv.Location = new System.Drawing.Point(12, 75);
            this.csv.Name = "csv";
            this.csv.Size = new System.Drawing.Size(669, 20);
            this.csv.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Path to save the csv file";
            // 
            // cubepfad
            // 
            this.cubepfad.Location = new System.Drawing.Point(12, 25);
            this.cubepfad.Name = "cubepfad";
            this.cubepfad.Size = new System.Drawing.Size(669, 20);
            this.cubepfad.TabIndex = 9;
            // 
            // pfadcube
            // 
            this.pfadcube.Location = new System.Drawing.Point(700, 25);
            this.pfadcube.Name = "pfadcube";
            this.pfadcube.Size = new System.Drawing.Size(88, 22);
            this.pfadcube.TabIndex = 10;
            this.pfadcube.Text = "set ";
            this.pfadcube.UseVisualStyleBackColor = true;
            this.pfadcube.Click += new System.EventHandler(this.pfadcube_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(170, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Path to STM32Cube";
            // 
            // chip
            // 
            this.chip.Location = new System.Drawing.Point(12, 121);
            this.chip.Name = "chip";
            this.chip.Size = new System.Drawing.Size(669, 20);
            this.chip.TabIndex = 12;
            // 
            // chipload
            // 
            this.chipload.Location = new System.Drawing.Point(700, 121);
            this.chipload.Name = "chipload";
            this.chipload.Size = new System.Drawing.Size(88, 20);
            this.chipload.TabIndex = 13;
            this.chipload.Text = "load";
            this.chipload.UseVisualStyleBackColor = true;
            this.chipload.Click += new System.EventHandler(this.chipload_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Chip - STM32.....xml";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 260);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chipload);
            this.Controls.Add(this.chip);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pfadcube);
            this.Controls.Add(this.cubepfad);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.csv);
            this.Controls.Add(this.csvPfad);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.gpioladen);
            this.Controls.Add(this.GPIODatei);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Datei);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox Datei;
        private OpenFileDialog openFileDialog1;
        private Label label1;
        private TextBox GPIODatei;
        private Button gpioladen;
        private Label label2;
        private Button csvPfad;
        private TextBox csv;
        private FolderBrowserDialog folderBrowserDialog1;
        private Label label3;
        private TextBox cubepfad;
        private Button pfadcube;
        private Label label4;
        private TextBox chip;
        private Button chipload;
        private Label label5;
    }
}


namespace Excel2SQliteExporter
{
    partial class frmThrowsParserT7
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label2 = new System.Windows.Forms.Label();
			this.txtFighter = new System.Windows.Forms.TextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.txtGrabs = new System.Windows.Forms.TextBox();
			this.btnProcess = new System.Windows.Forms.Button();
			this.btnClean = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.btnSaveToBD = new System.Windows.Forms.Button();
			this.txtRegex = new System.Windows.Forms.TextBox();
			this.txtRegexResult = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(43, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(49, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Peleador";
			// 
			// txtFighter
			// 
			this.txtFighter.Location = new System.Drawing.Point(44, 45);
			this.txtFighter.Name = "txtFighter";
			this.txtFighter.Size = new System.Drawing.Size(165, 20);
			this.txtFighter.TabIndex = 3;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(44, 81);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(608, 348);
			this.tabControl1.TabIndex = 6;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.textBox1);
			this.tabPage1.Controls.Add(this.button1);
			this.tabPage1.Controls.Add(this.txtRegexResult);
			this.tabPage1.Controls.Add(this.txtRegex);
			this.tabPage1.Controls.Add(this.btnClean);
			this.tabPage1.Controls.Add(this.btnProcess);
			this.tabPage1.Controls.Add(this.txtGrabs);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(600, 322);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Agarres";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.btnSaveToBD);
			this.tabPage2.Controls.Add(this.dataGridView1);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(600, 322);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Grid Resultado";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// txtGrabs
			// 
			this.txtGrabs.Location = new System.Drawing.Point(35, 35);
			this.txtGrabs.Multiline = true;
			this.txtGrabs.Name = "txtGrabs";
			this.txtGrabs.Size = new System.Drawing.Size(458, 173);
			this.txtGrabs.TabIndex = 5;
			// 
			// btnProcess
			// 
			this.btnProcess.Location = new System.Drawing.Point(499, 35);
			this.btnProcess.Name = "btnProcess";
			this.btnProcess.Size = new System.Drawing.Size(75, 23);
			this.btnProcess.TabIndex = 6;
			this.btnProcess.Text = "Procesar";
			this.btnProcess.UseVisualStyleBackColor = true;
			this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
			// 
			// btnClean
			// 
			this.btnClean.Location = new System.Drawing.Point(499, 64);
			this.btnClean.Name = "btnClean";
			this.btnClean.Size = new System.Drawing.Size(75, 21);
			this.btnClean.TabIndex = 7;
			this.btnClean.Text = "Limpiar";
			this.btnClean.UseVisualStyleBackColor = true;
			this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(23, 17);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.Size = new System.Drawing.Size(467, 268);
			this.dataGridView1.TabIndex = 0;
			// 
			// btnSaveToBD
			// 
			this.btnSaveToBD.Location = new System.Drawing.Point(505, 17);
			this.btnSaveToBD.Name = "btnSaveToBD";
			this.btnSaveToBD.Size = new System.Drawing.Size(75, 23);
			this.btnSaveToBD.TabIndex = 1;
			this.btnSaveToBD.Text = "Salvar BD";
			this.btnSaveToBD.UseVisualStyleBackColor = true;
			this.btnSaveToBD.Click += new System.EventHandler(this.btnSaveToBD_Click);
			// 
			// txtRegex
			// 
			this.txtRegex.Location = new System.Drawing.Point(35, 229);
			this.txtRegex.Name = "txtRegex";
			this.txtRegex.Size = new System.Drawing.Size(458, 20);
			this.txtRegex.TabIndex = 8;
			// 
			// txtRegexResult
			// 
			this.txtRegexResult.Location = new System.Drawing.Point(35, 255);
			this.txtRegexResult.Name = "txtRegexResult";
			this.txtRegexResult.Size = new System.Drawing.Size(458, 20);
			this.txtRegexResult.TabIndex = 9;
			this.txtRegexResult.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(499, 229);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 21);
			this.button1.TabIndex = 10;
			this.button1.Text = "regex";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(35, 281);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(458, 20);
			this.textBox1.TabIndex = 11;
			// 
			// frmThrowsParser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(745, 600);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.txtFighter);
			this.Controls.Add(this.label2);
			this.Name = "frmThrowsParser";
			this.Text = "frmThrowsParser";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtFighter;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Button btnClean;
		private System.Windows.Forms.Button btnProcess;
		private System.Windows.Forms.TextBox txtGrabs;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Button btnSaveToBD;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox txtRegexResult;
		private System.Windows.Forms.TextBox txtRegex;
		private System.Windows.Forms.TextBox textBox1;
	}
}
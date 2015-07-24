namespace BottleGreen.Main
{
    partial class BottleGreen
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
            this.btn_XmlToMssql = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_OracleToMssql = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_XmlToMssql
            // 
            this.btn_XmlToMssql.Location = new System.Drawing.Point(81, 71);
            this.btn_XmlToMssql.Name = "btn_XmlToMssql";
            this.btn_XmlToMssql.Size = new System.Drawing.Size(113, 23);
            this.btn_XmlToMssql.TabIndex = 1;
            this.btn_XmlToMssql.Text = "XML To MSSQL";
            this.btn_XmlToMssql.UseVisualStyleBackColor = true;
            this.btn_XmlToMssql.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_OracleToMssql);
            this.groupBox1.Controls.Add(this.btn_XmlToMssql);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(202, 101);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Problem 2";
            // 
            // btn_OracleToMssql
            // 
            this.btn_OracleToMssql.Location = new System.Drawing.Point(81, 32);
            this.btn_OracleToMssql.Name = "btn_OracleToMssql";
            this.btn_OracleToMssql.Size = new System.Drawing.Size(112, 23);
            this.btn_OracleToMssql.TabIndex = 0;
            this.btn_OracleToMssql.Text = "Oracle To MSSQL";
            this.btn_OracleToMssql.UseVisualStyleBackColor = true;
            this.btn_OracleToMssql.Click += new System.EventHandler(this.btn_OracleToMssql_Click);
            // 
            // BottleGreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 279);
            this.Controls.Add(this.groupBox1);
            this.Name = "BottleGreen";
            this.Text = "Team BottleGreen";
            this.Load += new System.EventHandler(this.BottleGreen_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_XmlToMssql;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_OracleToMssql;
    }
}


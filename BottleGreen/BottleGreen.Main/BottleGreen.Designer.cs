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
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.endDateLabel = new System.Windows.Forms.Label();
            this.endDate = new System.Windows.Forms.TextBox();
            this.startDateLabel = new System.Windows.Forms.Label();
            this.startDate = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.PDF_Reports = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.exit = new System.Windows.Forms.Button();
            this.btn_OracleToMssql = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 131);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 40);
            this.button1.TabIndex = 1;
            this.button1.Text = "ZIP To MSSQL";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_OracleToMssql);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(100, 177);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Problem 2";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Location = new System.Drawing.Point(436, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(100, 177);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Problem 6";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 131);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 40);
            this.button2.TabIndex = 0;
            this.button2.Text = "XML File To MSSQL";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.XMLtoMSSQL_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.endDateLabel);
            this.groupBox3.Controls.Add(this.endDate);
            this.groupBox3.Controls.Add(this.startDateLabel);
            this.groupBox3.Controls.Add(this.startDate);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Location = new System.Drawing.Point(224, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(100, 177);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Problem 4";
            // 
            // endDateLabel
            // 
            this.endDateLabel.AutoSize = true;
            this.endDateLabel.Location = new System.Drawing.Point(6, 78);
            this.endDateLabel.Name = "endDateLabel";
            this.endDateLabel.Size = new System.Drawing.Size(52, 13);
            this.endDateLabel.TabIndex = 4;
            this.endDateLabel.Text = "end date:";
            this.endDateLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // endDate
            // 
            this.endDate.Location = new System.Drawing.Point(6, 94);
            this.endDate.Multiline = true;
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(88, 20);
            this.endDate.TabIndex = 3;
            this.endDate.TabStop = false;
            this.endDate.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // startDateLabel
            // 
            this.startDateLabel.AutoSize = true;
            this.startDateLabel.Location = new System.Drawing.Point(6, 35);
            this.startDateLabel.Name = "startDateLabel";
            this.startDateLabel.Size = new System.Drawing.Size(54, 13);
            this.startDateLabel.TabIndex = 2;
            this.startDateLabel.Text = "start date:";
            // 
            // startDate
            // 
            this.startDate.Location = new System.Drawing.Point(6, 51);
            this.startDate.Multiline = true;
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(88, 20);
            this.startDate.TabIndex = 1;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 131);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(88, 40);
            this.button3.TabIndex = 0;
            this.button3.Text = "MSSQL to XML File";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.MsqlToXML_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.PDF_Reports);
            this.groupBox4.Location = new System.Drawing.Point(118, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(100, 177);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Problem 3";
            // 
            // PDF_Reports
            // 
            this.PDF_Reports.Location = new System.Drawing.Point(6, 131);
            this.PDF_Reports.Name = "PDF_Reports";
            this.PDF_Reports.Size = new System.Drawing.Size(88, 40);
            this.PDF_Reports.TabIndex = 0;
            this.PDF_Reports.Text = "PDF Reports";
            this.PDF_Reports.UseVisualStyleBackColor = true;
            this.PDF_Reports.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(330, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(100, 177);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Problem 5";
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(460, 295);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(70, 38);
            this.exit.TabIndex = 6;
            this.exit.Text = "Exit";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // btn_OracleToMssql
            // 
            this.btn_OracleToMssql.Location = new System.Drawing.Point(6, 78);
            this.btn_OracleToMssql.Name = "btn_OracleToMssql";
            this.btn_OracleToMssql.Size = new System.Drawing.Size(88, 40);
            this.btn_OracleToMssql.TabIndex = 0;
            this.btn_OracleToMssql.Text = "Oracle to MSSQL";
            this.btn_OracleToMssql.UseVisualStyleBackColor = true;
            this.btn_OracleToMssql.Click += new System.EventHandler(this.btn_OracleToMssql_Click);
            // 
            // BottleGreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 345);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "BottleGreen";
            this.Text = "Team BottleGreen";
            this.Load += new System.EventHandler(this.BottleGreen_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button PDF_Reports;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label endDateLabel;
        private System.Windows.Forms.TextBox endDate;
        private System.Windows.Forms.Label startDateLabel;
        private System.Windows.Forms.TextBox startDate;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Button btn_OracleToMssql;
    }
}


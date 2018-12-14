namespace BulkBhavCopiesLoader
{
    partial class BulkBhavCopiesUploader
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.resultLabel = new System.Windows.Forms.Label();
            this.button_StopUpload = new System.Windows.Forms.Button();
            this.button_StartUpload = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox_Folder = new System.Windows.Forms.TextBox();
            this.button_ScanHousebreaks = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button_CandleScan = new System.Windows.Forms.Button();
            this.button_housebreakperformance = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.resultLabel);
            this.groupBox1.Controls.Add(this.button_StopUpload);
            this.groupBox1.Controls.Add(this.button_StartUpload);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.textBox_Folder);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(482, 183);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(206, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Bhav Copies % completed";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(40, 72);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(413, 23);
            this.progressBar1.TabIndex = 5;
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Location = new System.Drawing.Point(37, 155);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(79, 13);
            this.resultLabel.TabIndex = 4;
            this.resultLabel.Text = "Display Results";
            // 
            // button_StopUpload
            // 
            this.button_StopUpload.Location = new System.Drawing.Point(334, 118);
            this.button_StopUpload.Name = "button_StopUpload";
            this.button_StopUpload.Size = new System.Drawing.Size(119, 23);
            this.button_StopUpload.TabIndex = 3;
            this.button_StopUpload.Text = "Stop Upload";
            this.button_StopUpload.UseVisualStyleBackColor = true;
            this.button_StopUpload.Click += new System.EventHandler(this.button3_Click);
            // 
            // button_StartUpload
            // 
            this.button_StartUpload.Location = new System.Drawing.Point(209, 118);
            this.button_StartUpload.Name = "button_StartUpload";
            this.button_StartUpload.Size = new System.Drawing.Size(119, 23);
            this.button_StartUpload.TabIndex = 2;
            this.button_StartUpload.Text = "Start Upload";
            this.button_StartUpload.UseVisualStyleBackColor = true;
            this.button_StartUpload.Click += new System.EventHandler(this.button_StartUpload_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(40, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 20);
            this.button1.TabIndex = 1;
            this.button1.Text = "Select Folder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox_Folder
            // 
            this.textBox_Folder.Location = new System.Drawing.Point(40, 20);
            this.textBox_Folder.Name = "textBox_Folder";
            this.textBox_Folder.ReadOnly = true;
            this.textBox_Folder.Size = new System.Drawing.Size(413, 20);
            this.textBox_Folder.TabIndex = 0;
            // 
            // button_ScanHousebreaks
            // 
            this.button_ScanHousebreaks.Location = new System.Drawing.Point(12, 201);
            this.button_ScanHousebreaks.Name = "button_ScanHousebreaks";
            this.button_ScanHousebreaks.Size = new System.Drawing.Size(119, 23);
            this.button_ScanHousebreaks.TabIndex = 7;
            this.button_ScanHousebreaks.Text = "Housebreaks";
            this.button_ScanHousebreaks.UseVisualStyleBackColor = true;
            this.button_ScanHousebreaks.Click += new System.EventHandler(this.button_ScanHousebreaks_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 230);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "House break report";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(331, 201);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(163, 23);
            this.button3.TabIndex = 9;
            this.button3.Text = "Populate Housebreaks";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(156, 230);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(161, 23);
            this.button4.TabIndex = 10;
            this.button4.Text = "Quick Housebreak Report";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(331, 230);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(161, 23);
            this.button5.TabIndex = 11;
            this.button5.Text = "Index Names";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(12, 281);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(233, 23);
            this.button6.TabIndex = 12;
            this.button6.Text = "Populate Volume Moving Average";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button_CandleScan
            // 
            this.button_CandleScan.Location = new System.Drawing.Point(270, 281);
            this.button_CandleScan.Name = "button_CandleScan";
            this.button_CandleScan.Size = new System.Drawing.Size(233, 23);
            this.button_CandleScan.TabIndex = 13;
            this.button_CandleScan.Text = "Scan For Candle Patterns";
            this.button_CandleScan.UseVisualStyleBackColor = true;
            this.button_CandleScan.Click += new System.EventHandler(this.button_CandleScan_Click);
            // 
            // button_housebreakperformance
            // 
            this.button_housebreakperformance.Location = new System.Drawing.Point(12, 331);
            this.button_housebreakperformance.Name = "button_housebreakperformance";
            this.button_housebreakperformance.Size = new System.Drawing.Size(233, 23);
            this.button_housebreakperformance.TabIndex = 14;
            this.button_housebreakperformance.Text = "Housebreak Performance";
            this.button_housebreakperformance.UseVisualStyleBackColor = true;
            this.button_housebreakperformance.Click += new System.EventHandler(this.button_housebreakperformance_Click);
            // 
            // BulkBhavCopiesUploader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 459);
            this.Controls.Add(this.button_housebreakperformance);
            this.Controls.Add(this.button_CandleScan);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button_ScanHousebreaks);
            this.Controls.Add(this.groupBox1);
            this.Name = "BulkBhavCopiesUploader";
            this.Text = "Bhav Copy Application";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox_Folder;
        private System.Windows.Forms.Button button_StopUpload;
        private System.Windows.Forms.Button button_StartUpload;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_ScanHousebreaks;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button_CandleScan;
        private System.Windows.Forms.Button button_housebreakperformance;
    }
}


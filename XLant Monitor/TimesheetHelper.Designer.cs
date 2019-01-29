using System;
namespace XLant_Monitor
{
    partial class TimesheetHelper
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
            this.label1 = new System.Windows.Forms.Label();
            this.StartDateBox = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.EndDateBox = new System.Windows.Forms.DateTimePicker();
            this.GoBtn = new System.Windows.Forms.Button();
            this.ResultsGrid = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ResultsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start Date:";
            // 
            // StartDateBox
            // 
            this.StartDateBox.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.StartDateBox.Location = new System.Drawing.Point(78, 13);
            this.StartDateBox.Name = "StartDateBox";
            this.StartDateBox.Size = new System.Drawing.Size(200, 20);
            this.StartDateBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "End Date:";
            // 
            // EndDateBox
            // 
            this.EndDateBox.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.EndDateBox.Location = new System.Drawing.Point(78, 40);
            this.EndDateBox.Name = "EndDateBox";
            this.EndDateBox.Size = new System.Drawing.Size(200, 20);
            this.EndDateBox.TabIndex = 3;
            // 
            // GoBtn
            // 
            this.GoBtn.Location = new System.Drawing.Point(328, 40);
            this.GoBtn.Name = "GoBtn";
            this.GoBtn.Size = new System.Drawing.Size(75, 23);
            this.GoBtn.TabIndex = 4;
            this.GoBtn.Text = "Go";
            this.GoBtn.UseVisualStyleBackColor = true;
            this.GoBtn.Click += new System.EventHandler(this.GoBtn_Click);
            // 
            // ResultsGrid
            // 
            this.ResultsGrid.AllowUserToAddRows = false;
            this.ResultsGrid.AllowUserToDeleteRows = false;
            this.ResultsGrid.AllowUserToOrderColumns = true;
            this.ResultsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultsGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ResultsGrid.Location = new System.Drawing.Point(7, 95);
            this.ResultsGrid.Name = "ResultsGrid";
            this.ResultsGrid.Size = new System.Drawing.Size(1001, 604);
            this.ResultsGrid.TabIndex = 5;
            this.ResultsGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.ResultsGrid_CellEndEdit);
            this.ResultsGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ResultsGrid_CellValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(911, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(713, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Add Completed Items to Timesheet";
            // 
            // TimesheetHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 711);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ResultsGrid);
            this.Controls.Add(this.GoBtn);
            this.Controls.Add(this.EndDateBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.StartDateBox);
            this.Controls.Add(this.label1);
            this.Name = "TimesheetHelper";
            this.Text = "TimesheetHelper";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ResultsGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker StartDateBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker EndDateBox;
        private System.Windows.Forms.Button GoBtn;
        private System.Windows.Forms.DataGridView ResultsGrid;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
    }
}
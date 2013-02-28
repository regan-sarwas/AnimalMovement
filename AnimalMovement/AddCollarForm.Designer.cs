﻿namespace AnimalMovement
{
    partial class AddCollarForm
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
            this.components = new System.ComponentModel.Container();
            this.cancelButton = new System.Windows.Forms.Button();
            this.CreateButton = new System.Windows.Forms.Button();
            this.NotesTextBox = new System.Windows.Forms.TextBox();
            this.OwnerTextBox = new System.Windows.Forms.TextBox();
            this.CollarIdTextBox = new System.Windows.Forms.TextBox();
            this.ArgosIdTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ManufacturerComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ModelComboBox = new System.Windows.Forms.ComboBox();
            this.ManagerComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SerialNumberTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.FrequencyTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.PeriodTextBox = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.HasGpsCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(218, 337);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(65, 23);
            this.cancelButton.TabIndex = 20;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // CreateButton
            // 
            this.CreateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateButton.Location = new System.Drawing.Point(289, 337);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(65, 23);
            this.CreateButton.TabIndex = 21;
            this.CreateButton.Text = "Create";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // NotesTextBox
            // 
            this.NotesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NotesTextBox.Location = new System.Drawing.Point(95, 249);
            this.NotesTextBox.Multiline = true;
            this.NotesTextBox.Name = "NotesTextBox";
            this.NotesTextBox.Size = new System.Drawing.Size(259, 82);
            this.NotesTextBox.TabIndex = 19;
            // 
            // OwnerTextBox
            // 
            this.OwnerTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OwnerTextBox.Location = new System.Drawing.Point(95, 119);
            this.OwnerTextBox.MaxLength = 100;
            this.OwnerTextBox.Name = "OwnerTextBox";
            this.OwnerTextBox.Size = new System.Drawing.Size(259, 20);
            this.OwnerTextBox.TabIndex = 14;
            this.toolTip1.SetToolTip(this.OwnerTextBox, "This should be an organization (NPS, FWS, USGS, AKF&G,  etc)");
            // 
            // CollarIdTextBox
            // 
            this.CollarIdTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CollarIdTextBox.Location = new System.Drawing.Point(95, 93);
            this.CollarIdTextBox.MaxLength = 16;
            this.CollarIdTextBox.Name = "CollarIdTextBox";
            this.CollarIdTextBox.Size = new System.Drawing.Size(259, 20);
            this.CollarIdTextBox.TabIndex = 13;
            this.toolTip1.SetToolTip(this.CollarIdTextBox, "Unique identifier provided by manufacturer.\r\nFor Telonics this is the CTN number." +
        "");
            this.CollarIdTextBox.TextChanged += new System.EventHandler(this.CollarIdTextBox_TextChanged);
            // 
            // ArgosIdTextBox
            // 
            this.ArgosIdTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ArgosIdTextBox.Location = new System.Drawing.Point(95, 145);
            this.ArgosIdTextBox.MaxLength = 16;
            this.ArgosIdTextBox.Name = "ArgosIdTextBox";
            this.ArgosIdTextBox.Size = new System.Drawing.Size(188, 20);
            this.ArgosIdTextBox.TabIndex = 15;
            this.toolTip1.SetToolTip(this.ArgosIdTextBox, "For Telonics collars this is the Argos Id");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(53, 252);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Notes:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(52, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Model:";
            this.toolTip1.SetToolTip(this.label4, "Required, but currently not used.");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Collar Id:";
            this.toolTip1.SetToolTip(this.label2, "Unique identifier provided by manufacturer.\r\nFor Telonics this is the CTN number." +
        "");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Manager:";
            this.toolTip1.SetToolTip(this.label1, "If you assign this collar to another PI you will not be able to edit it.");
            // 
            // ManufacturerComboBox
            // 
            this.ManufacturerComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ManufacturerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ManufacturerComboBox.FormattingEnabled = true;
            this.ManufacturerComboBox.Location = new System.Drawing.Point(95, 39);
            this.ManufacturerComboBox.Name = "ManufacturerComboBox";
            this.ManufacturerComboBox.Size = new System.Drawing.Size(259, 21);
            this.ManufacturerComboBox.TabIndex = 11;
            this.ManufacturerComboBox.SelectedIndexChanged += new System.EventHandler(this.ManufacturerComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Manufacturer:";
            // 
            // ModelComboBox
            // 
            this.ModelComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ModelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ModelComboBox.FormattingEnabled = true;
            this.ModelComboBox.Location = new System.Drawing.Point(95, 66);
            this.ModelComboBox.Name = "ModelComboBox";
            this.ModelComboBox.Size = new System.Drawing.Size(259, 21);
            this.ModelComboBox.TabIndex = 12;
            this.toolTip1.SetToolTip(this.ModelComboBox, "Required to decide how to decode raw Telonics data files");
            this.ModelComboBox.SelectedIndexChanged += new System.EventHandler(this.ModelComboBox_SelectedIndexChanged);
            // 
            // ManagerComboBox
            // 
            this.ManagerComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ManagerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ManagerComboBox.FormattingEnabled = true;
            this.ManagerComboBox.Location = new System.Drawing.Point(95, 12);
            this.ManagerComboBox.Name = "ManagerComboBox";
            this.ManagerComboBox.Size = new System.Drawing.Size(259, 21);
            this.ManagerComboBox.TabIndex = 10;
            this.toolTip1.SetToolTip(this.ManagerComboBox, "If you assign this collar to another PI you will not be able to edit it.");
            this.ManagerComboBox.SelectedIndexChanged += new System.EventHandler(this.ManagerComboBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Argos Id:";
            this.toolTip1.SetToolTip(this.label6, "For Telonics collars this is the Argos Id");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(50, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Owner:";
            this.toolTip1.SetToolTip(this.label7, "This should be an organization (NPS, FWS, USGS, AKF&G,  etc)");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 174);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Serial Number:";
            this.toolTip1.SetToolTip(this.label8, "For Telonics collars this is the same as the ID (optional)");
            // 
            // SerialNumberTextBox
            // 
            this.SerialNumberTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SerialNumberTextBox.Location = new System.Drawing.Point(95, 171);
            this.SerialNumberTextBox.MaxLength = 100;
            this.SerialNumberTextBox.Name = "SerialNumberTextBox";
            this.SerialNumberTextBox.Size = new System.Drawing.Size(259, 20);
            this.SerialNumberTextBox.TabIndex = 16;
            this.toolTip1.SetToolTip(this.SerialNumberTextBox, "For Telonics collars this is the same as the ID (optional)");
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(29, 200);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Frequency:";
            // 
            // FrequencyTextBox
            // 
            this.FrequencyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FrequencyTextBox.Location = new System.Drawing.Point(95, 197);
            this.FrequencyTextBox.MaxLength = 32;
            this.FrequencyTextBox.Name = "FrequencyTextBox";
            this.FrequencyTextBox.Size = new System.Drawing.Size(259, 20);
            this.FrequencyTextBox.TabIndex = 17;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 226);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 13);
            this.label10.TabIndex = 33;
            this.label10.Text = "Period (minutes):";
            this.toolTip1.SetToolTip(this.label10, "Instructions on downloading collar data from mfgr (currently not used)");
            // 
            // PeriodTextBox
            // 
            this.PeriodTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PeriodTextBox.Location = new System.Drawing.Point(95, 223);
            this.PeriodTextBox.MaxLength = 200;
            this.PeriodTextBox.Name = "PeriodTextBox";
            this.PeriodTextBox.Size = new System.Drawing.Size(259, 20);
            this.PeriodTextBox.TabIndex = 18;
            this.toolTip1.SetToolTip(this.PeriodTextBox, "Period (in minutes) between fixes.  Only for processing Argos messages from Telon" +
        "ics Gen 3 collars");
            // 
            // HasGpsCheckBox
            // 
            this.HasGpsCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.HasGpsCheckBox.AutoSize = true;
            this.HasGpsCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.HasGpsCheckBox.Location = new System.Drawing.Point(287, 147);
            this.HasGpsCheckBox.Name = "HasGpsCheckBox";
            this.HasGpsCheckBox.Size = new System.Drawing.Size(67, 17);
            this.HasGpsCheckBox.TabIndex = 84;
            this.HasGpsCheckBox.Text = "Has Gps";
            this.toolTip1.SetToolTip(this.HasGpsCheckBox, "Check this box if the collar has an onboard GPS");
            this.HasGpsCheckBox.UseVisualStyleBackColor = true;
            // 
            // AddCollarForm
            // 
            this.AcceptButton = this.CreateButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(376, 370);
            this.Controls.Add(this.HasGpsCheckBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.PeriodTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.FrequencyTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.SerialNumberTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ManagerComboBox);
            this.Controls.Add(this.ModelComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ManufacturerComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.NotesTextBox);
            this.Controls.Add(this.OwnerTextBox);
            this.Controls.Add(this.CollarIdTextBox);
            this.Controls.Add(this.ArgosIdTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(300, 346);
            this.Name = "AddCollarForm";
            this.Text = "Create New Collar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.TextBox NotesTextBox;
        private System.Windows.Forms.TextBox OwnerTextBox;
        private System.Windows.Forms.TextBox CollarIdTextBox;
        private System.Windows.Forms.TextBox ArgosIdTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ManufacturerComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ModelComboBox;
        private System.Windows.Forms.ComboBox ManagerComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox SerialNumberTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox FrequencyTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox PeriodTextBox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox HasGpsCheckBox;

    }
}
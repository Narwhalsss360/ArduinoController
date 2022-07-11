namespace Controller
{
    partial class ArduinoController
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArduinoController));
            this.DoComboBox = new System.Windows.Forms.ComboBox();
            this.PinControlModeComboBox = new System.Windows.Forms.ComboBox();
            this.PinControlValueComboBox = new System.Windows.Forms.ComboBox();
            this.DoButton = new System.Windows.Forms.Button();
            this.PortSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.PortConnectionButton = new System.Windows.Forms.Button();
            this.OutputTextBox = new System.Windows.Forms.TextBox();
            this.ClearTextBoxButton = new System.Windows.Forms.Button();
            this.SaveTextButton = new System.Windows.Forms.Button();
            this.PinSelectTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // DoComboBox
            // 
            this.DoComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DoComboBox.Enabled = false;
            this.DoComboBox.FormattingEnabled = true;
            this.DoComboBox.Items.AddRange(new object[] {
            "Get Uptime",
            "Pin Control"});
            this.DoComboBox.Location = new System.Drawing.Point(12, 12);
            this.DoComboBox.Name = "DoComboBox";
            this.DoComboBox.Size = new System.Drawing.Size(121, 21);
            this.DoComboBox.TabIndex = 18;
            this.DoComboBox.DropDownClosed += new System.EventHandler(this.DoComboBox_DropDownClosed);
            // 
            // PinControlModeComboBox
            // 
            this.PinControlModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PinControlModeComboBox.Enabled = false;
            this.PinControlModeComboBox.FormattingEnabled = true;
            this.PinControlModeComboBox.Location = new System.Drawing.Point(12, 39);
            this.PinControlModeComboBox.Name = "PinControlModeComboBox";
            this.PinControlModeComboBox.Size = new System.Drawing.Size(82, 21);
            this.PinControlModeComboBox.TabIndex = 19;
            this.PinControlModeComboBox.DropDownClosed += new System.EventHandler(this.PinControlComboBox_DropDownClosed);
            // 
            // PinControlValueComboBox
            // 
            this.PinControlValueComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PinControlValueComboBox.Enabled = false;
            this.PinControlValueComboBox.FormattingEnabled = true;
            this.PinControlValueComboBox.Location = new System.Drawing.Point(11, 66);
            this.PinControlValueComboBox.Name = "PinControlValueComboBox";
            this.PinControlValueComboBox.Size = new System.Drawing.Size(121, 21);
            this.PinControlValueComboBox.TabIndex = 20;
            this.PinControlValueComboBox.SelectedIndexChanged += new System.EventHandler(this.PinControlValueComboBox_SelectedIndexChanged);
            // 
            // DoButton
            // 
            this.DoButton.Enabled = false;
            this.DoButton.Location = new System.Drawing.Point(12, 93);
            this.DoButton.Name = "DoButton";
            this.DoButton.Size = new System.Drawing.Size(120, 23);
            this.DoButton.TabIndex = 21;
            this.DoButton.Text = "Do";
            this.DoButton.UseVisualStyleBackColor = true;
            this.DoButton.Click += new System.EventHandler(this.DoButton_Click);
            // 
            // PortSelectionComboBox
            // 
            this.PortSelectionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PortSelectionComboBox.FormattingEnabled = true;
            this.PortSelectionComboBox.Location = new System.Drawing.Point(12, 122);
            this.PortSelectionComboBox.Name = "PortSelectionComboBox";
            this.PortSelectionComboBox.Size = new System.Drawing.Size(121, 21);
            this.PortSelectionComboBox.TabIndex = 23;
            this.PortSelectionComboBox.DropDown += new System.EventHandler(this.PortSelectionComboBox_DropDown);
            // 
            // PortConnectionButton
            // 
            this.PortConnectionButton.Location = new System.Drawing.Point(12, 149);
            this.PortConnectionButton.Name = "PortConnectionButton";
            this.PortConnectionButton.Size = new System.Drawing.Size(121, 23);
            this.PortConnectionButton.TabIndex = 24;
            this.PortConnectionButton.Text = "Connect";
            this.PortConnectionButton.UseVisualStyleBackColor = true;
            this.PortConnectionButton.Click += new System.EventHandler(this.PortConnectionButton_Click);
            // 
            // OutputTextBox
            // 
            this.OutputTextBox.Enabled = false;
            this.OutputTextBox.Location = new System.Drawing.Point(139, 12);
            this.OutputTextBox.Multiline = true;
            this.OutputTextBox.Name = "OutputTextBox";
            this.OutputTextBox.Size = new System.Drawing.Size(327, 160);
            this.OutputTextBox.TabIndex = 25;
            // 
            // ClearTextBoxButton
            // 
            this.ClearTextBoxButton.Location = new System.Drawing.Point(418, 12);
            this.ClearTextBoxButton.Name = "ClearTextBoxButton";
            this.ClearTextBoxButton.Size = new System.Drawing.Size(25, 25);
            this.ClearTextBoxButton.TabIndex = 26;
            this.ClearTextBoxButton.Text = "C";
            this.ClearTextBoxButton.UseVisualStyleBackColor = true;
            this.ClearTextBoxButton.Click += new System.EventHandler(this.ClearTextBoxButton_Click);
            // 
            // SaveTextButton
            // 
            this.SaveTextButton.Location = new System.Drawing.Point(441, 12);
            this.SaveTextButton.Name = "SaveTextButton";
            this.SaveTextButton.Size = new System.Drawing.Size(25, 25);
            this.SaveTextButton.TabIndex = 27;
            this.SaveTextButton.Text = "S";
            this.SaveTextButton.UseVisualStyleBackColor = true;
            this.SaveTextButton.Click += new System.EventHandler(this.SaveTextButton_Click);
            // 
            // PinSelectTextBox
            // 
            this.PinSelectTextBox.Enabled = false;
            this.PinSelectTextBox.Location = new System.Drawing.Point(100, 39);
            this.PinSelectTextBox.Name = "PinSelectTextBox";
            this.PinSelectTextBox.Size = new System.Drawing.Size(33, 20);
            this.PinSelectTextBox.TabIndex = 28;
            this.PinSelectTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ArduinoController
            // 
            this.AcceptButton = this.DoButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(480, 183);
            this.Controls.Add(this.PinSelectTextBox);
            this.Controls.Add(this.SaveTextButton);
            this.Controls.Add(this.ClearTextBoxButton);
            this.Controls.Add(this.OutputTextBox);
            this.Controls.Add(this.PortConnectionButton);
            this.Controls.Add(this.PortSelectionComboBox);
            this.Controls.Add(this.DoButton);
            this.Controls.Add(this.PinControlValueComboBox);
            this.Controls.Add(this.PinControlModeComboBox);
            this.Controls.Add(this.DoComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ArduinoController";
            this.Text = "Arduino Controller";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox DoComboBox;
        private System.Windows.Forms.ComboBox PinControlModeComboBox;
        private System.Windows.Forms.ComboBox PinControlValueComboBox;
        private System.Windows.Forms.Button DoButton;
        private System.Windows.Forms.ComboBox PortSelectionComboBox;
        private System.Windows.Forms.Button PortConnectionButton;
        private System.Windows.Forms.TextBox OutputTextBox;
        private System.Windows.Forms.Button ClearTextBoxButton;
        private System.Windows.Forms.Button SaveTextButton;
        private System.Windows.Forms.TextBox PinSelectTextBox;
    }
}


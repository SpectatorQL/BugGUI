namespace BugGUI
{
    partial class NewGameDirectoryForm
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
            if(disposing && (components != null))
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
            this.nameLabel = new System.Windows.Forms.Label();
            this.pathLabel = new System.Windows.Forms.Label();
            this.extensionsLabel = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.pathBox = new System.Windows.Forms.TextBox();
            this.extensionsBox = new System.Windows.Forms.TextBox();
            this.addDirectoryButton = new System.Windows.Forms.Button();
            this.browseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 9);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(35, 13);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name";
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Location = new System.Drawing.Point(12, 39);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(29, 13);
            this.pathLabel.TabIndex = 1;
            this.pathLabel.Text = "Path";
            // 
            // extensionsLabel
            // 
            this.extensionsLabel.AutoSize = true;
            this.extensionsLabel.Location = new System.Drawing.Point(12, 71);
            this.extensionsLabel.Name = "extensionsLabel";
            this.extensionsLabel.Size = new System.Drawing.Size(58, 13);
            this.extensionsLabel.TabIndex = 2;
            this.extensionsLabel.Text = "Extensions";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(74, 6);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(372, 20);
            this.nameBox.TabIndex = 3;
            // 
            // pathBox
            // 
            this.pathBox.Enabled = false;
            this.pathBox.Location = new System.Drawing.Point(74, 36);
            this.pathBox.Name = "pathBox";
            this.pathBox.Size = new System.Drawing.Size(372, 20);
            this.pathBox.TabIndex = 4;
            // 
            // extensionsBox
            // 
            this.extensionsBox.Location = new System.Drawing.Point(74, 68);
            this.extensionsBox.Name = "extensionsBox";
            this.extensionsBox.Size = new System.Drawing.Size(372, 20);
            this.extensionsBox.TabIndex = 5;
            // 
            // addDirectoryButton
            // 
            this.addDirectoryButton.Location = new System.Drawing.Point(168, 94);
            this.addDirectoryButton.Name = "addDirectoryButton";
            this.addDirectoryButton.Size = new System.Drawing.Size(125, 58);
            this.addDirectoryButton.TabIndex = 6;
            this.addDirectoryButton.Text = "Add";
            this.addDirectoryButton.UseVisualStyleBackColor = true;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(417, 36);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(29, 20);
            this.browseButton.TabIndex = 7;
            this.browseButton.Text = "...";
            this.browseButton.UseVisualStyleBackColor = true;
            // 
            // NewGameDirectoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 164);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.addDirectoryButton);
            this.Controls.Add(this.extensionsBox);
            this.Controls.Add(this.pathBox);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.extensionsLabel);
            this.Controls.Add(this.pathLabel);
            this.Controls.Add(this.nameLabel);
            this.Name = "NewGameDirectoryForm";
            this.Text = "NewGameDirectoryForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.Label extensionsLabel;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.TextBox pathBox;
        private System.Windows.Forms.TextBox extensionsBox;
        private System.Windows.Forms.Button addDirectoryButton;
        private System.Windows.Forms.Button browseButton;
    }
}
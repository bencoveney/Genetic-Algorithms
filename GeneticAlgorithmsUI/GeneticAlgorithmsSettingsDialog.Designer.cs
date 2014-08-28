namespace GeneticAlgorithmsUI
{
    partial class GeneticAlgorithmsSettingsDialog
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
            this.selectionComboBox = new System.Windows.Forms.ComboBox();
            this.selectionLabel = new System.Windows.Forms.Label();
            this.crossoverComboBox = new System.Windows.Forms.ComboBox();
            this.crossoverLabel = new System.Windows.Forms.Label();
            this.mutationRateLabel = new System.Windows.Forms.Label();
            this.mutationRateUpDown = new System.Windows.Forms.NumericUpDown();
            this.geneDuplicationLabel = new System.Windows.Forms.Label();
            this.geneDuplicationUpDown = new System.Windows.Forms.NumericUpDown();
            this.geneDropLabel = new System.Windows.Forms.Label();
            this.geneDropUpDown = new System.Windows.Forms.NumericUpDown();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mutationRateUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.geneDuplicationUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.geneDropUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // selectionComboBox
            // 
            this.selectionComboBox.FormattingEnabled = true;
            this.selectionComboBox.Location = new System.Drawing.Point(153, 12);
            this.selectionComboBox.Name = "selectionComboBox";
            this.selectionComboBox.Size = new System.Drawing.Size(257, 24);
            this.selectionComboBox.TabIndex = 0;
            // 
            // selectionLabel
            // 
            this.selectionLabel.AutoSize = true;
            this.selectionLabel.Location = new System.Drawing.Point(12, 15);
            this.selectionLabel.Name = "selectionLabel";
            this.selectionLabel.Size = new System.Drawing.Size(117, 17);
            this.selectionLabel.TabIndex = 1;
            this.selectionLabel.Text = "Selection Method";
            // 
            // crossoverComboBox
            // 
            this.crossoverComboBox.FormattingEnabled = true;
            this.crossoverComboBox.Location = new System.Drawing.Point(153, 42);
            this.crossoverComboBox.Name = "crossoverComboBox";
            this.crossoverComboBox.Size = new System.Drawing.Size(257, 24);
            this.crossoverComboBox.TabIndex = 0;
            // 
            // crossoverLabel
            // 
            this.crossoverLabel.AutoSize = true;
            this.crossoverLabel.Location = new System.Drawing.Point(12, 45);
            this.crossoverLabel.Name = "crossoverLabel";
            this.crossoverLabel.Size = new System.Drawing.Size(123, 17);
            this.crossoverLabel.TabIndex = 1;
            this.crossoverLabel.Text = "Crossover Method";
            // 
            // mutationRateLabel
            // 
            this.mutationRateLabel.AutoSize = true;
            this.mutationRateLabel.Location = new System.Drawing.Point(12, 74);
            this.mutationRateLabel.Name = "mutationRateLabel";
            this.mutationRateLabel.Size = new System.Drawing.Size(171, 17);
            this.mutationRateLabel.TabIndex = 2;
            this.mutationRateLabel.Text = "Gene Mutation Probability";
            // 
            // mutationRateUpDown
            // 
            this.mutationRateUpDown.Location = new System.Drawing.Point(286, 72);
            this.mutationRateUpDown.Name = "mutationRateUpDown";
            this.mutationRateUpDown.Size = new System.Drawing.Size(124, 22);
            this.mutationRateUpDown.TabIndex = 3;
            // 
            // geneDuplicationLabel
            // 
            this.geneDuplicationLabel.AutoSize = true;
            this.geneDuplicationLabel.Location = new System.Drawing.Point(12, 102);
            this.geneDuplicationLabel.Name = "geneDuplicationLabel";
            this.geneDuplicationLabel.Size = new System.Drawing.Size(187, 17);
            this.geneDuplicationLabel.TabIndex = 2;
            this.geneDuplicationLabel.Text = "Gene Duplication Probability";
            // 
            // geneDuplicationUpDown
            // 
            this.geneDuplicationUpDown.Location = new System.Drawing.Point(286, 100);
            this.geneDuplicationUpDown.Name = "geneDuplicationUpDown";
            this.geneDuplicationUpDown.Size = new System.Drawing.Size(124, 22);
            this.geneDuplicationUpDown.TabIndex = 3;
            // 
            // geneDropLabel
            // 
            this.geneDropLabel.AutoSize = true;
            this.geneDropLabel.Location = new System.Drawing.Point(12, 130);
            this.geneDropLabel.Name = "geneDropLabel";
            this.geneDropLabel.Size = new System.Drawing.Size(148, 17);
            this.geneDropLabel.TabIndex = 2;
            this.geneDropLabel.Text = "Gene Drop Probability";
            // 
            // geneDropUpDown
            // 
            this.geneDropUpDown.Location = new System.Drawing.Point(286, 128);
            this.geneDropUpDown.Name = "geneDropUpDown";
            this.geneDropUpDown.Size = new System.Drawing.Size(124, 22);
            this.geneDropUpDown.TabIndex = 3;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(188, 156);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(108, 33);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(302, 156);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(108, 33);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // GeneticAlgorithmsSettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 202);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.geneDropUpDown);
            this.Controls.Add(this.geneDropLabel);
            this.Controls.Add(this.geneDuplicationUpDown);
            this.Controls.Add(this.geneDuplicationLabel);
            this.Controls.Add(this.mutationRateUpDown);
            this.Controls.Add(this.mutationRateLabel);
            this.Controls.Add(this.crossoverLabel);
            this.Controls.Add(this.crossoverComboBox);
            this.Controls.Add(this.selectionLabel);
            this.Controls.Add(this.selectionComboBox);
            this.Name = "GeneticAlgorithmsSettingsDialog";
            this.Text = "GeneticAlgorithmsSettingsDialog";
            ((System.ComponentModel.ISupportInitialize)(this.mutationRateUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.geneDuplicationUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.geneDropUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox selectionComboBox;
        private System.Windows.Forms.Label selectionLabel;
        private System.Windows.Forms.ComboBox crossoverComboBox;
        private System.Windows.Forms.Label crossoverLabel;
        private System.Windows.Forms.Label mutationRateLabel;
        private System.Windows.Forms.NumericUpDown mutationRateUpDown;
        private System.Windows.Forms.Label geneDuplicationLabel;
        private System.Windows.Forms.NumericUpDown geneDuplicationUpDown;
        private System.Windows.Forms.Label geneDropLabel;
        private System.Windows.Forms.NumericUpDown geneDropUpDown;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
    }
}
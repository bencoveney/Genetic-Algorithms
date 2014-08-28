using System.Windows.Forms.DataVisualization.Charting;
namespace GeneticAlgorithmsUI
{
    partial class GeneticAlgorithmsUI<Gene>
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.configurationPanel = new System.Windows.Forms.Panel();
            this.simulationControlsGroup = new System.Windows.Forms.GroupBox();
            this.numberOfGenerationsLabel = new System.Windows.Forms.Label();
            this.numberOfGenerationsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.simulateMultipleButton = new System.Windows.Forms.Button();
            this.simulateSingleButton = new System.Windows.Forms.Button();
            this.configurationGroup = new System.Windows.Forms.GroupBox();
            this.launchSettingsButton = new System.Windows.Forms.Button();
            this.resetSimulationButton = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.simulationDataGridView = new System.Windows.Forms.DataGridView();
            this.fitnessGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.configurationPanel.SuspendLayout();
            this.simulationControlsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfGenerationsNumericUpDown)).BeginInit();
            this.configurationGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.simulationDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fitnessGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // configurationPanel
            // 
            this.configurationPanel.Controls.Add(this.simulationControlsGroup);
            this.configurationPanel.Controls.Add(this.configurationGroup);
            this.configurationPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.configurationPanel.Location = new System.Drawing.Point(0, 0);
            this.configurationPanel.Name = "configurationPanel";
            this.configurationPanel.Size = new System.Drawing.Size(854, 124);
            this.configurationPanel.TabIndex = 0;
            // 
            // simulationControlsGroup
            // 
            this.simulationControlsGroup.Controls.Add(this.numberOfGenerationsLabel);
            this.simulationControlsGroup.Controls.Add(this.numberOfGenerationsNumericUpDown);
            this.simulationControlsGroup.Controls.Add(this.simulateMultipleButton);
            this.simulationControlsGroup.Controls.Add(this.simulateSingleButton);
            this.simulationControlsGroup.Location = new System.Drawing.Point(171, 12);
            this.simulationControlsGroup.Name = "simulationControlsGroup";
            this.simulationControlsGroup.Size = new System.Drawing.Size(384, 105);
            this.simulationControlsGroup.TabIndex = 3;
            this.simulationControlsGroup.TabStop = false;
            this.simulationControlsGroup.Text = "Simulation Controls";
            // 
            // numberOfGenerationsLabel
            // 
            this.numberOfGenerationsLabel.AutoSize = true;
            this.numberOfGenerationsLabel.Location = new System.Drawing.Point(204, 70);
            this.numberOfGenerationsLabel.Name = "numberOfGenerationsLabel";
            this.numberOfGenerationsLabel.Size = new System.Drawing.Size(109, 17);
            this.numberOfGenerationsLabel.TabIndex = 3;
            this.numberOfGenerationsLabel.Text = "Number To Run";
            // 
            // numberOfGenerationsNumericUpDown
            // 
            this.numberOfGenerationsNumericUpDown.Location = new System.Drawing.Point(319, 68);
            this.numberOfGenerationsNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numberOfGenerationsNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberOfGenerationsNumericUpDown.Name = "numberOfGenerationsNumericUpDown";
            this.numberOfGenerationsNumericUpDown.Size = new System.Drawing.Size(59, 22);
            this.numberOfGenerationsNumericUpDown.TabIndex = 2;
            this.numberOfGenerationsNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // simulateMultipleButton
            // 
            this.simulateMultipleButton.Location = new System.Drawing.Point(195, 21);
            this.simulateMultipleButton.Name = "simulateMultipleButton";
            this.simulateMultipleButton.Size = new System.Drawing.Size(183, 34);
            this.simulateMultipleButton.TabIndex = 1;
            this.simulateMultipleButton.Text = "Run Multiple Generations";
            this.simulateMultipleButton.UseVisualStyleBackColor = true;
            this.simulateMultipleButton.Click += new System.EventHandler(this.simulateMultipleButton_Click);
            // 
            // simulateSingleButton
            // 
            this.simulateSingleButton.Location = new System.Drawing.Point(6, 21);
            this.simulateSingleButton.Name = "simulateSingleButton";
            this.simulateSingleButton.Size = new System.Drawing.Size(183, 34);
            this.simulateSingleButton.TabIndex = 0;
            this.simulateSingleButton.Text = "Run Single Generation";
            this.simulateSingleButton.UseVisualStyleBackColor = true;
            this.simulateSingleButton.Click += new System.EventHandler(this.simulateSingleButton_Click);
            // 
            // configurationGroup
            // 
            this.configurationGroup.Controls.Add(this.launchSettingsButton);
            this.configurationGroup.Controls.Add(this.resetSimulationButton);
            this.configurationGroup.Location = new System.Drawing.Point(12, 12);
            this.configurationGroup.Name = "configurationGroup";
            this.configurationGroup.Size = new System.Drawing.Size(153, 105);
            this.configurationGroup.TabIndex = 2;
            this.configurationGroup.TabStop = false;
            this.configurationGroup.Text = "Configuration";
            // 
            // launchSettingsButton
            // 
            this.launchSettingsButton.Location = new System.Drawing.Point(8, 21);
            this.launchSettingsButton.Name = "launchSettingsButton";
            this.launchSettingsButton.Size = new System.Drawing.Size(139, 34);
            this.launchSettingsButton.TabIndex = 0;
            this.launchSettingsButton.Text = "Settings";
            this.launchSettingsButton.UseVisualStyleBackColor = true;
            this.launchSettingsButton.Click += new System.EventHandler(this.launchSettingsButton_Click);
            // 
            // resetSimulationButton
            // 
            this.resetSimulationButton.Location = new System.Drawing.Point(6, 61);
            this.resetSimulationButton.Name = "resetSimulationButton";
            this.resetSimulationButton.Size = new System.Drawing.Size(139, 34);
            this.resetSimulationButton.TabIndex = 1;
            this.resetSimulationButton.Text = "Reset Simulation";
            this.resetSimulationButton.UseVisualStyleBackColor = true;
            this.resetSimulationButton.Click += new System.EventHandler(this.resetSimulationButton_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 124);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.simulationDataGridView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.fitnessGraph);
            this.splitContainer.Size = new System.Drawing.Size(854, 507);
            this.splitContainer.SplitterDistance = 284;
            this.splitContainer.TabIndex = 1;
            // 
            // simulationDataGridView
            // 
            this.simulationDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.simulationDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.simulationDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simulationDataGridView.Location = new System.Drawing.Point(0, 0);
            this.simulationDataGridView.Name = "simulationDataGridView";
            this.simulationDataGridView.RowTemplate.Height = 24;
            this.simulationDataGridView.Size = new System.Drawing.Size(284, 507);
            this.simulationDataGridView.TabIndex = 0;
            // 
            // fitnessGraph
            // 
            chartArea1.Name = "Fitness Chart Area";
            this.fitnessGraph.ChartAreas.Add(chartArea1);
            this.fitnessGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "Fitness Lines";
            this.fitnessGraph.Legends.Add(legend1);
            this.fitnessGraph.Location = new System.Drawing.Point(0, 0);
            this.fitnessGraph.Name = "fitnessGraph";
            this.fitnessGraph.Size = new System.Drawing.Size(566, 507);
            this.fitnessGraph.TabIndex = 0;
            this.fitnessGraph.Text = "chart1";
            // 
            // GeneticAlgorithmsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 631);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.configurationPanel);
            this.Name = "GeneticAlgorithmsUI";
            this.Text = "Form1";
            this.configurationPanel.ResumeLayout(false);
            this.simulationControlsGroup.ResumeLayout(false);
            this.simulationControlsGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfGenerationsNumericUpDown)).EndInit();
            this.configurationGroup.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.simulationDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fitnessGraph)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel configurationPanel;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView simulationDataGridView;
        private System.Windows.Forms.DataVisualization.Charting.Chart fitnessGraph;
        private System.Windows.Forms.Button resetSimulationButton;
        private System.Windows.Forms.Button launchSettingsButton;
        private System.Windows.Forms.GroupBox simulationControlsGroup;
        private System.Windows.Forms.Button simulateSingleButton;
        private System.Windows.Forms.GroupBox configurationGroup;
        private System.Windows.Forms.Label numberOfGenerationsLabel;
        private System.Windows.Forms.NumericUpDown numberOfGenerationsNumericUpDown;
        private System.Windows.Forms.Button simulateMultipleButton;
    }
}


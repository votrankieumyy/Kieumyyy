using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace BlueStacksFpsOptimizer
{
    public partial class MainForm : Form
    {
        private BlueStacksOptimizer optimizer;
        private BlueStacksConfigOptimizer configOptimizer;
        private BlueStacksInstallOptimizer installOptimizer;
        private LowLatencyInputOptimizer inputOptimizer;
        private LowLatencyInputCmdOptimizer cmdInputOptimizer;
        private bool isOptimized = false;

        public MainForm()
        {
            InitializeComponent();
            optimizer = new BlueStacksOptimizer();
            configOptimizer = new BlueStacksConfigOptimizer();
            installOptimizer = new BlueStacksInstallOptimizer();
            inputOptimizer = new LowLatencyInputOptimizer();
            cmdInputOptimizer = new LowLatencyInputCmdOptimizer();
            this.Text = "🚀 BlueStacks 5 FPS Optimizer v4.5 ELITE";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Size = new System.Drawing.Size(850, 800);
        }

        private void InitializeComponent()
        {
            // Main Panel
            var mainPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                RowCount = 9,
                ColumnCount = 1,
                Padding = new Padding(20)
            };
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 280));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            // Title Label
            var titleLabel = new Label()
            {
                Text = "🚀 BlueStacks 5 FPS Optimizer v4.5 ELITE",
                Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.Red,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            mainPanel.Controls.Add(titleLabel, 0, 0);

            // Status Label
            var statusLabel = new Label()
            {
                Text = "Status: Not Optimized",
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.Red,
                Dock = DockStyle.Fill,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };
            mainPanel.Controls.Add(statusLabel, 0, 1);

            // Output TextBox
            var outputBox = new TextBox()
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                Font = new System.Drawing.Font("Courier New", 7.5f),
                BackColor = System.Drawing.Color.Black,
                ForeColor = System.Drawing.Color.LimeGreen
            };
            outputBox.Text = "🔍 Waiting for optimization...\n";
            mainPanel.Controls.Add(outputBox, 0, 2);

            // Button Panel 1 - FPS Optimization
            var buttonPanel1 = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            var optimizeBtn = new Button()
            {
                Text = "🚀 FPS",
                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold),
                Width = 90,
                Height = 40,
                BackColor = System.Drawing.Color.LimeGreen,
                ForeColor = System.Drawing.Color.Black,
                Cursor = Cursors.Hand
            };
            optimizeBtn.Click += (s, e) => RunOptimization(outputBox, statusLabel, optimizeBtn, "FPS", () => optimizer.Optimize());
            buttonPanel1.Controls.Add(optimizeBtn);

            mainPanel.Controls.Add(buttonPanel1, 0, 3);

            // Button Panel 2 - Deep Config
            var buttonPanel2 = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            var deepOptimizeBtn = new Button()
            {
                Text = "⚙️ CONFIG",
                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold),
                Width = 110,
                Height = 40,
                BackColor = System.Drawing.Color.DodgerBlue,
                ForeColor = System.Drawing.Color.White,
                Cursor = Cursors.Hand
            };
            deepOptimizeBtn.Click += (s, e) => RunOptimization(outputBox, statusLabel, deepOptimizeBtn, "CONFIG", () => configOptimizer.OptimizeConfigs());
            buttonPanel2.Controls.Add(deepOptimizeBtn);

            mainPanel.Controls.Add(buttonPanel2, 0, 4);

            // Button Panel 3 - Install
            var buttonPanel3 = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            var installOptimizeBtn = new Button()
            {
                Text = "🔍 INSTALL",
                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold),
                Width = 120,
                Height = 40,
                BackColor = System.Drawing.Color.DarkOrange,
                ForeColor = System.Drawing.Color.White,
                Cursor = Cursors.Hand
            };
            installOptimizeBtn.Click += (s, e) => RunOptimization(outputBox, statusLabel, installOptimizeBtn, "INSTALL", () => installOptimizer.OptimizeInstallation());
            buttonPanel3.Controls.Add(installOptimizeBtn);

            mainPanel.Controls.Add(buttonPanel3, 0, 5);

            // Button Panel 4 - Low Latency Input
            var buttonPanel4 = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            var inputOptimizeBtn = new Button()
            {
                Text = "⚡ LATENCY",
                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold),
                Width = 130,
                Height = 40,
                BackColor = System.Drawing.Color.Purple,
                ForeColor = System.Drawing.Color.White,
                Cursor = Cursors.Hand
            };
            inputOptimizeBtn.Click += (s, e) => RunOptimization(outputBox, statusLabel, inputOptimizeBtn, "LOW LATENCY", () => inputOptimizer.OptimizeLowLatencyInput());
            buttonPanel4.Controls.Add(inputOptimizeBtn);

            mainPanel.Controls.Add(buttonPanel4, 0, 6);

            // Button Panel 5 - CMD Low Latency Input
            var buttonPanel5 = new FlowLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            var cmdInputOptimizeBtn = new Button()
            {
                Text = "💥 CMD LATENCY",
                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold),
                Width = 150,
                Height = 40,
                BackColor = System.Drawing.Color.Crimson,
                ForeColor = System.Drawing.Color.White,
                Cursor = Cursors.Hand
            };
            cmdInputOptimizeBtn.Click += (s, e) => RunOptimization(outputBox, statusLabel, cmdInputOptimizeBtn, "CMD LOW LATENCY", () => cmdInputOptimizer.OptimizeLowLatencyInputCMD());
            buttonPanel5.Controls.Add(cmdInputOptimizeBtn);

            var resetBtn = new Button()
            {
                Text = "🔄 RESET",
                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold),
                Width = 100,
                Height = 40,
                BackColor = System.Drawing.Color.Gray,
                ForeColor = System.Drawing.Color.White,
                Cursor = Cursors.Hand
            };
            resetBtn.Click += (s, e) =>
            {
                outputBox.Clear();
                statusLabel.Text = "Status: Not Optimized";
                statusLabel.ForeColor = System.Drawing.Color.Red;
                isOptimized = false;
                outputBox.AppendText("🔄 Reset to default settings...\n");
            };
            buttonPanel5.Controls.Add(resetBtn);

            mainPanel.Controls.Add(buttonPanel5, 0, 7);

            this.Controls.Add(mainPanel);
        }

        private void RunOptimization(TextBox outputBox, Label statusLabel, Button button, string type, Func<string> optimizationFunc)
        {
            outputBox.AppendText($"\n{"=".PadRight(70, '=')}\n");
            button.Enabled = false;

            try
            {
                outputBox.AppendText($"🔍 Starting {type} optimization...\n");
                var result = optimizationFunc();

                outputBox.AppendText(result);
                statusLabel.Text = $"Status: ✅ {type} OPTIMIZED";
                statusLabel.ForeColor = System.Drawing.Color.Green;
                isOptimized = true;
            }
            catch (Exception ex)
            {
                outputBox.AppendText($"❌ Error: {ex.Message}\n");
                statusLabel.Text = $"Status: ❌ {type} FAILED";
                statusLabel.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                button.Enabled = true;
                outputBox.AppendText("\n✅ Complete!\n");
            }
        }
    }
}
using MapGenerator;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;
using System.Diagnostics;

namespace NoiseMapGeneratorArray
{
    public partial class Form1 : Form
    {
        Settings settings = new Settings();
        BackgroundWorker bgwArrayBuilder = new BackgroundWorker();
        BackgroundWorker bgwPainter = new BackgroundWorker();

        int total = 1;

        public Form1()
        {
            InitializeComponent();
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Idle;

            bgwArrayBuilder.DoWork += BgwArrayBuilder_DoWork!;
            bgwArrayBuilder.ProgressChanged += BgwArrayBuilder_ProgressChanged!;
            bgwArrayBuilder.WorkerReportsProgress = true;

            bgwPainter.DoWork += BgwPainter_DoWork!;
            bgwPainter.ProgressChanged += BgwPainter_ProgressChanged!;
            bgwPainter.WorkerReportsProgress = true;

            var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            settings = config.GetSection("Settings").Get<Settings>();
        }


        #region Array
        private void button_BuildArray_Click(object sender, EventArgs e)
        {
            total = (int)(settings.MaxX * settings.MaxY);
            progressBar2.Maximum = total;
            GenerationTimes.Reset();
            if (!bgwArrayBuilder.IsBusy)
            {
                bgwArrayBuilder.RunWorkerAsync();
            }
        }
        private void BgwArrayBuilder_DoWork(object sender, DoWorkEventArgs e)
        {
            ArrayGeneration arrayGeneration = new ArrayGeneration();
            arrayGeneration.Generate(bgwArrayBuilder, settings);
        }

        private void BgwArrayBuilder_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdateProgress(e);
        }



        private DateTime CalculateETA()
        {
            TimeSpan elapsedTime = DateTime.Now.Subtract(GenerationTimes.Start);
            double averageSeconds = elapsedTime.TotalSeconds / GenerationTimes.Count;

            int amountLeft = progressBar2.Maximum - progressBar2.Value;
            double timeLeft = amountLeft * averageSeconds;
            return DateTime.Now.AddSeconds(timeLeft);
        }
        #endregion Array


        #region Paint
        private void button_BuildPictures_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar2.Value = 0;
            progressBar1.Maximum = 100;
            if (!bgwPainter.IsBusy)
            {
                bgwPainter.RunWorkerAsync();
            }
        }

        private void BgwPainter_DoWork(object sender, DoWorkEventArgs e)
        {
            ArrayPainter painter = new ArrayPainter();
            painter.Generate(bgwPainter, settings);
        }

        private void BgwPainter_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdateProgress(e);
        }
        #endregion Paint


        #region Common
        private void UpdateProgress(ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                switch (e.UserState.ToString())
                {
                    case "1":
                        progressBar1.Value = e.ProgressPercentage;
                        break;
                    case "2":
                        progressBar2.Value = e.ProgressPercentage;
                        GenerationTimes.Add(DateTime.Now);
                        toolStripStatusLabel_ETA.Text = string.Format("ETA: - {0}", CalculateETA());
                        break;
                }
            }
        }
        #endregion Common

    }
}

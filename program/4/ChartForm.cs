using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4
{
    public partial class ChartForm : Form
    {
        public ChartForm()
        {
            InitializeComponent();
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            string[,] stats = SQL.GetStatistics();
            chart1.Series[0].Points.AddXY(stats[0, 0], int.Parse(stats[0, 1]));
            chart1.Series[0].Points.AddXY(stats[1, 0], int.Parse(stats[1, 1]));
            chart1.Series[0].Points.AddXY(stats[2, 0], int.Parse(stats[2, 1]));

            chart1.ChartAreas[0].BackColor = Color.FromArgb(255, 30, 33, 36);
            chart1.Legends[0].BackColor = Color.FromArgb(255, 30, 33, 36);
            chart1.Legends[0].ForeColor = Color.FromArgb(255, 188, 188, 188);
            chart1.Series[0].Color = Color.FromArgb(255, 188, 188, 188);
        }
    }
}

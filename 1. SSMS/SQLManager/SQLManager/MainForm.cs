using SQLManager.dal;
using SQLManager.models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SQLManager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadDatabases();
        }

        private void LoadDatabases()
        {
            tlpResults.AutoScroll = true;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) => Application.Exit();

        private void btnExecute_Click(object sender, EventArgs e)
        {
            ClearData();
            try
            {
                DrawResults(RepositoryFactory.GetRepository().Query(tbQuery.Text.Trim()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DrawResults(QueryData queryData)
        {
            foreach (var table in queryData.Data.Tables)
            {
                DataGridView dgvRes = new DataGridView();
                tlpResults.Controls.Add(dgvRes);
                dgvRes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dgvRes.Width = tlpResults.Width;
                dgvRes.Height = tlpResults.Height;
                dgvRes.DataSource = table;
            }
            tbMessage.Text = queryData.Message;
            tbParsed.Text = string.Join("\r\n", queryData.Parsed);
        }

        private void ClearData()
        {
            tlpResults.Controls.Clear();
            tlpResults.RowCount = 1;
            tbParsed.Text = string.Empty;
        }

        private void dgvResults_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }
    }
}

using SQLManager.dal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLManager
{
    public partial class LoginForm : Form
    {
        public LoginForm() {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            // Load ServerTypes
            DataSet ds = new DataSet();
            ds.ReadXml("../../Resources/ServerTypes.xml");
            DataTable dt = ds.Tables[0];
            cbServerType.DataSource = dt;
            cbServerType.DisplayMember = "name";
            cbServerType.Enabled = false;

            // Load AuthenticationTypes
            DataSet ds2 = new DataSet();
            ds2.ReadXml("../../Resources/AuthenticationTypes.xml");
            DataTable dt2 = ds2.Tables[0];
            cbAuthentication.DataSource = dt2;
            cbAuthentication.DisplayMember = "name";
            cbAuthentication.Enabled = false;

        }

        private void btnConnect_Click(object sender, EventArgs e) => Connect();

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        internal void Connect(){
            try
            {
                RepositoryFactory.GetRepository().Connect(tbServername.Text.Trim(), tbUsername.Text.Trim(), tbPassword.Text.Trim());
                new MainForm().Show();
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Connect();

        }
    }
}

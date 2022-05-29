using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;


namespace AppExecute
{
    public partial class FrmExecute : Form
    {

        public FrmExecute()
        {
                InitializeComponent();
            user.Text = "postgres";
            host.Text = "localhost";



        }

        private void cmdFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmExecute_Load(object sender, EventArgs e)
        {
            cmdExecute.Enabled = false;
        }

        public void checktextboxes()
        {
            if (!String.IsNullOrEmpty(dir.Text) && !String.IsNullOrEmpty(pass.Text) && !String.IsNullOrEmpty(user.Text) && !String.IsNullOrEmpty(dbname.Text) && !String.IsNullOrEmpty(host.Text))
            {
                cmdExecute.Enabled = true;
            }
            else { cmdExecute.Enabled = false; }
        }

        private void cmdOuvrir_Click(object sender, EventArgs e)
        {
            try
            {
                
                openFile.InitialDirectory = "E:";
                openFile.ValidateNames = false;
                openFile.CheckFileExists = false;
                openFile.CheckPathExists = true;
                openFile.ShowDialog();
                dir.Text = openFile.FileName;
                if (!String.IsNullOrEmpty(dir.Text))
                {
                    cmdExecute.Enabled = true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erreur " + ex.Message);
            }
            
           
        }

        private void txtFile_ChangeUICues(object sender, UICuesEventArgs e)
        {
            if (!String.IsNullOrEmpty(dir.Text))
            {
                cmdExecute.Enabled = false;
            }
        }

        


        private void cmdExecute_Click(object sender, EventArgs e)
        {
            var cs = "Host=" + host.Text + ";Username=" + user.Text + ";Password=" + pass.Text + ";Database=" + dbname.Text + "";
            using var con = new NpgsqlConnection(cs);
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Closed)
                {
                    MessageBox.Show("Connection error");
                }


            }



            if (con.State == ConnectionState.Open)
            {
                var sql = "SELECT usesuper FROM pg_user where usename='" + user.Text + "';";
                using var command = new NpgsqlCommand(sql, con);
                var issuperuser = command.ExecuteScalar().ToString();

                if (issuperuser != "True")
                    MessageBox.Show(user.Text + " is not a Superuser");
                else
                {
                    string cmd = "set pgpassword=" + pass.Text + "& pg_dump -Fc -h " + host.Text + " -U " + user.Text + " " + dbname.Text + " > " + dir.Text + "";
                    try
                    {
                        var str = ExecuteExtension.executeCommand("pg_dump", cmd);
                        toolStripStatusLabel1.Text = str;
                    }
                    catch (Exception ex)
                    {

                        toolStripStatusLabel1.Text = ex.Message;
                    }
                }
                
            }

            


        }

        private void txtFile_TextChanged(object sender, EventArgs e)
        {
            checktextboxes();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pass_TextChanged(object sender, EventArgs e)
        {
            checktextboxes();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            checktextboxes();

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void host_TextChanged(object sender, EventArgs e)
        {
            checktextboxes();
            host.Enabled = false;

        }

        private void dbname_TextChanged(object sender, EventArgs e)
        {
            checktextboxes();

        }
    }
}

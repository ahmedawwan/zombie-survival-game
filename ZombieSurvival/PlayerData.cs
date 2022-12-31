using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ZombieSurvival
{
    public partial class PlayerData : Form
    {
        public PlayerData(int a)
        {
            InitializeComponent();
            Score = a;
        }

        private void btnEnterData_Click(object sender, EventArgs e)
        {
            Name = txtName.Text;
            getID();
            SaveData();
            this.Hide();
        }
        int Id;
        string Name;
        int Score;
        string conString = @"";
        void getID()
        {

            string query = "Select max (Id) as ID from Players;";
            try
            {
                SqlConnection con = new SqlConnection(conString);            
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                Id = Convert.ToInt32(cmd.ExecuteScalar());
                Id++;
                con.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void SaveData()
        {
            try
            {
                string query = "Insert into Players values('{0}', '{1}', '{2}');";
                query = string.Format(query, Id, Name, Score);
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Save Successful");
            }
            catch(System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

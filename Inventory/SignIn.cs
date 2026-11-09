using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventory;

namespace Inventory
{
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();

            btnSignIn.Click += btnSignIn_Click;
            txtPassword.UseSystemPasswordChar = true;
            linkRegister.LinkClicked += linkRegister_LinkClicked;
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            btnSignIn_Click1(sender, e);
        }

        private void linkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 register = new Form2();
            register.Show();
            this.Hide();
        }

        private void btnSignIn_Click1(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (username == "" || password == "")
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (OleDbConnection con = Database.GetConnection())
                {
                    con.Open();

                    string query = "SELECT * FROM Users WHERE Username = ?";

                    using (OleDbCommand cmd = new OleDbCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);

                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            string storedHash = reader["Password"].ToString();
                            string fullname = reader["FullName"].ToString();
                            int userId = Convert.ToInt32(reader["UserID"]);


                            if (!PasswordHelper.VerifyPassword(password, storedHash))
                            {
                                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            MessageBox.Show($"Welcome, {fullname}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            reader.Close();

                            Dashboard dashboard = new Dashboard();
                            dashboard.Show();
                            this.Hide();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while signing in: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SignInForm_Load(object sender, EventArgs e)
        {

        }
    }
}
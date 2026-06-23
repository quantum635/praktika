using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;

namespace WindowsFormsApp2
{
    public partial class Login : Form
    {
        private string connect = "Database=college;server=localhost;user id=root;password=1111";

        public Login()
        {
            InitializeComponent();

            textBox1.Enter += textBox1_Enter;
            textBox1.Leave += textBox1_Leave;
            textBox2.Enter += textBox2_Enter;
            textBox2.Leave += textBox2_Leave;

            textBox1.Text = "Логин";
            textBox1.ForeColor = Color.Gray;
            textBox2.Text = "Пароль";
            textBox2.ForeColor = Color.Gray;
            textBox2.PasswordChar = '\0';

        }

        private void enter(TextBox textBox)
        {
            if (textBox.Text == "Логин" || textBox.Text == "Пароль")
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }

        private void leave(string text, TextBox textBox)
        {
            if (textBox.Text == "")
            {
                textBox.Text = text;
                textBox.ForeColor = Color.Gray;
            }
                
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            enter(textBox1);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            leave("Логин", textBox1);
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            enter(textBox2);
            if (textBox2.Text == "")
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            leave("Пароль", textBox2);
            if (textBox2.Text == "Пароль")
            {
                textBox2.PasswordChar = '\0';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            if((login == "Логин" || string.IsNullOrWhiteSpace(login)) && (password == "Пароль" || string.IsNullOrWhiteSpace(password)))
            {
                MessageBox.Show("Введите логин и пароль");
                return;
            }

            string query = @"
            SELECT surname, name, patronymic, role
            FROM users
            WHERE login = @login AND password = @password
            LIMIT 1";

            using (MySqlConnection conn = new MySqlConnection(connect))
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@login", login);
                cmd.Parameters.AddWithValue("@password", password);

                try
                {
                    conn.Open();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Session.Role = reader["role"].ToString();
                            Session.Fio =
                                $"{reader["surname"]} " +
                                $"{reader["name"]} " +
                                $"{reader["patronymic"]}";

                            MainForm mainForm = new MainForm();
                            mainForm.Show();

                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Неверный логин или пароль");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка подключения: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Session.Role = "guest";
            Session.Fio = "Гость";
            MainForm main = new MainForm();
            main.Show();
            this.Hide();
        }
    }
}
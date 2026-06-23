using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class MainForm : Form
    {
        public static string table;
        public static string table_id;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            label2.Text = "Пользователь: " + Session.Fio;
            label2.Left = this.ClientSize.Width - label2.Width - 20;

            if (Session.Role == "guest")
            {
                button3.Visible = false;
                button4.Visible = false;

                button1.Location = new Point(169, 120);
                button2.Location = new Point(169, 165);
            }
        }

        private void CreateForm2()
        {
            EditForm form2 = new EditForm();
            form2.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            table = "specialities";
            table_id = "speciality_code";
            CreateForm2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            table = "student_groups";
            table_id = "group_id";
            CreateForm2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Card card = new Card();
            card.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            table = "schedule";
            table_id = "schedule_id";
            CreateForm2();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
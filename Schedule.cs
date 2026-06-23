using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Data;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp2
{
    public partial class Schedule : Form
    {
        private int? scheduleId = null;

        //добавление
        public Schedule()
        {
            InitializeComponent();
            comboBox2.SelectionChangeCommitted += comboBox2_1_SelectionChangeCommitted;
            comboBox3.SelectionChangeCommitted += comboBox3_1_SelectionChangeCommitted;
        }

        //добавление
        public Schedule(int id)
        {
            InitializeComponent();
            scheduleId = id;
            this.Text = "Редактирование занятия";
            button3.Text = "редактировать\r\nзанятие";
            comboBox2.SelectionChangeCommitted += comboBox2_SelectionChangeCommitted;
            comboBox3.SelectionChangeCommitted += comboBox3_SelectionChangeCommitted;
        }

        private void Schedule_Load(object sender, EventArgs e)
        {
            //ФИО пользователя
            label12.Text = "Пользователь: " + Session.Fio;
            label12.Left = this.ClientSize.Width - label2.Width - 300;

            //добавление
            if (scheduleId == null)
            {
                LoadGroups();

                int groupId = Convert.ToInt32(comboBox2.SelectedValue);
                LoadDisciplines(groupId);
            }
            //редактирование
            else
            {
                LoadGroups();
                string connect = "Database=college;server=localhost;user id=root;password=1111";

                using (MySqlConnection conn = new MySqlConnection(connect))
                {
                    conn.Open();

                    string query = @"
                    SELECT 
                        s.schedule_id,
                        s.academic_year,
                        s.semester,
                        s.day_of_week,
                        s.pair_no,
                        s.start_time,
                        s.end_time,

                        s.group_id,
                        g.group_number,

                        s.discipline_id,
                        d.discipline_name,

                        s.teacher_id,
                        CONCAT(t.surname, ' ', t.name, ' ', t.patronymic) AS teacher_name,

                        s.cabinet,
                        s.lesson_type

                    FROM schedule s

                    JOIN student_groups g ON g.group_id = s.group_id
                    JOIN disciplines d ON d.discipline_id = s.discipline_id
                    JOIN teachers t ON t.teacher_id = s.teacher_id

                    WHERE s.schedule_id = @id;";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", scheduleId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox1.Text = reader["academic_year"].ToString();
                                textBox2.Text = reader["semester"].ToString();
                                comboBox1.Text = reader["day_of_week"].ToString();
                                textBox3.Text = reader["pair_no"].ToString();
                                textBox4.Text = reader["start_time"].ToString();
                                textBox5.Text = reader["end_time"].ToString();
                                comboBox2.Text = reader["group_number"].ToString();
                                comboBox3.Text = reader["discipline_name"].ToString();
                                LoadDisciplines((int)reader["group_id"]);
                                LoadTeacherByDiscipline();
                                comboBox4.Text = reader["teacher_name"].ToString();
                                textBox6.Text = reader["cabinet"].ToString();
                                comboBox5.Text = reader["lesson_type"].ToString();
                            }
                        }
                    }
                }
            } 
        }

        private void LoadGroups()
        {
            string connect = "Database=college;server=localhost;user id=root;password=1111";

            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string query = @"
                SELECT group_id, group_number
                FROM student_groups
                ORDER BY group_number";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);

                comboBox2.DataSource = table;
                comboBox2.DisplayMember = "group_number";
                comboBox2.ValueMember = "group_id";
            }
        }

        private void LoadDisciplines(int groupId)
        {
            string connect = "Database=college;server=localhost;user id=root;password=1111";

            using (var conn = new MySqlConnection(connect))
            {
                conn.Open();

                string query = @"
                SELECT discipline_id, discipline_name
                FROM disciplines
                WHERE speciality_code = (
                    SELECT speciality_code
                    FROM student_groups
                    WHERE group_id = @id)";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", groupId);

                DataTable dt = new DataTable();
                new MySqlDataAdapter(cmd).Fill(dt);

                comboBox3.DataSource = dt;
                comboBox3.DisplayMember = "discipline_name";
                comboBox3.ValueMember = "discipline_id";
            }

            LoadTeacherByDiscipline();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditForm edit = new EditForm();
            edit.Show();
            this.Hide();
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadDisciplineByGroup();
        }

        private void comboBox2_1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadDisciplineByGroup();
        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadTeacherByDiscipline();
        }

        private void comboBox3_1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadTeacherByDiscipline();
        }

        private void LoadDisciplineByGroup()
        {
            if (comboBox2.SelectedValue == null)
                return;

            if (comboBox2.SelectedValue is DataRowView)
                return;

            if (!int.TryParse(comboBox2.SelectedValue.ToString(), out int groupId))
                return;

            string connect = "Database=college;server=localhost;user id=root;password=1111";

            using (var conn = new MySqlConnection(connect))
            {
                conn.Open();

                string query = @"
                SELECT discipline_id, discipline_name
                FROM disciplines
                WHERE speciality_code = (
                    SELECT speciality_code
                    FROM student_groups
                    WHERE group_id = @id)";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", groupId);

                DataTable dt = new DataTable();
                new MySqlDataAdapter(cmd).Fill(dt);

                comboBox3.DataSource = dt;
                comboBox3.DisplayMember = "discipline_name";
                comboBox3.ValueMember = "discipline_id";
            }

            LoadTeacherByDiscipline();
        }

        private void LoadTeacherByDiscipline()
        {
            string disciplineId = comboBox3.SelectedValue.ToString();

            string connect = "Database=college;server=localhost;user id=root;password=1111";

            using (var conn = new MySqlConnection(connect))
            {
                conn.Open();

                string query = @"
                SELECT t.teacher_id,
                       CONCAT(t.surname, ' ', t.name, ' ', t.patronymic) AS teacher_name
                FROM teachers t
                JOIN disciplines d ON d.teacher_id = t.teacher_id
                WHERE d.discipline_id = @id";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", disciplineId);

                DataTable dt = new DataTable();
                new MySqlDataAdapter(cmd).Fill(dt);

                comboBox4.DataSource = dt;
                comboBox4.DisplayMember = "teacher_name";
                comboBox4.ValueMember = "teacher_id";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //добавление
            if (scheduleId == null)
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(comboBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text) ||
                    string.IsNullOrWhiteSpace(textBox5.Text) ||
                    string.IsNullOrWhiteSpace(comboBox2.Text) ||
                    string.IsNullOrWhiteSpace(comboBox3.Text) ||
                    string.IsNullOrWhiteSpace(comboBox4.Text) ||
                    string.IsNullOrWhiteSpace(textBox6.Text) ||
                    string.IsNullOrWhiteSpace(comboBox5.Text))
                {
                    MessageBox.Show("Сначала заполните все поля!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    string connect = "Database=college;server=localhost;user id=root;password=1111";

                    using (var conn = new MySqlConnection(connect))
                    {
                        conn.Open();

                        string query = @"
                        INSERT INTO schedule
                        (academic_year, semester, day_of_week, pair_no, start_time, end_time, group_id,
                            discipline_id, teacher_id, cabinet, lesson_type)
                        VALUES (@year, @semester, @day, @pair, @start, @end, @groupId, @disciplineId,
                            @teacherId, @cabinet, @type);";

                        using (var cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@year", textBox1.Text.Trim());
                            cmd.Parameters.AddWithValue("@semester", textBox2.Text.Trim());
                            cmd.Parameters.AddWithValue("@day", comboBox1.Text);

                            cmd.Parameters.AddWithValue("@pair", textBox3.Text.Trim());
                            cmd.Parameters.AddWithValue("@start", textBox4.Text.Trim());
                            cmd.Parameters.AddWithValue("@end", textBox5.Text.Trim());

                            cmd.Parameters.AddWithValue("@groupId", comboBox2.SelectedValue);
                            cmd.Parameters.AddWithValue("@disciplineId", comboBox3.SelectedValue);
                            cmd.Parameters.AddWithValue("@teacherId", comboBox4.SelectedValue);

                            cmd.Parameters.AddWithValue("@cabinet", textBox6.Text.Trim());
                            cmd.Parameters.AddWithValue("@type", comboBox5.Text);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    EditForm edit = new EditForm();
                    edit.Show();
                    this.Hide();

                    MessageBox.Show(
                        "Занятие успешно добавлено!",
                        "Успех",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            //редактирование
            else
            {
                string connect = "Database=college;server=localhost;user id=root;password=1111";

                using (var conn = new MySqlConnection(connect))
                {
                    conn.Open();

                    string query = @"
                    UPDATE schedule
                    SET 
                        academic_year = @year,
                        semester = @semester,
                        day_of_week = @day,
                        pair_no = @pair,
                        start_time = @start,
                        end_time = @end,
                        group_id = @groupId,
                        discipline_id = @disciplineId,
                        teacher_id = @teacherId,
                        cabinet = @cabinet,
                        lesson_type = @type
                    WHERE schedule_id = @id;";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@year", textBox1.Text);
                        cmd.Parameters.AddWithValue("@semester", textBox2.Text);
                        cmd.Parameters.AddWithValue("@day", comboBox1.Text);

                        cmd.Parameters.AddWithValue("@pair", textBox3.Text);
                        cmd.Parameters.AddWithValue("@start", textBox4.Text);
                        cmd.Parameters.AddWithValue("@end", textBox5.Text);

                        cmd.Parameters.AddWithValue("@groupId", comboBox2.SelectedValue);
                        cmd.Parameters.AddWithValue("@disciplineId", comboBox3.SelectedValue);
                        cmd.Parameters.AddWithValue("@teacherId", comboBox4.SelectedValue);

                        cmd.Parameters.AddWithValue("@cabinet", textBox6.Text);
                        cmd.Parameters.AddWithValue("@type", comboBox5.Text);

                        cmd.Parameters.AddWithValue("@id", scheduleId);

                        cmd.ExecuteNonQuery();
                    }
                }

                EditForm edit = new EditForm();
                edit.Show();
                this.Hide();

                MessageBox.Show("Изменнения сохранены", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        } 
    }
}
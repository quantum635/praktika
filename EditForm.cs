using MySql.Data.MySqlClient;
using Mysqlx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class EditForm : Form
    {
        private string connect = "Database=college;server=localhost;user id=root;password=1111";
        private bool _loadingCombos = false;

        public EditForm()
        {
            InitializeComponent();

            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;

            if (Session.Role == "admin")
            {
                dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            }
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            string table = MainForm.table;

            if (table == "specialities")
            {
                this.Text = "Специальности";

                LoadTableData(table);

                comboBox1.Visible = false;
                comboBox2.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
            }
            else if (table == "student_groups")
            {
                this.Text = "Группы";

                LoadTableData(table);

                comboBox1.Visible = false;
                comboBox2.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
            }
            else
            {
                this.Text = "Расписание";

                LoadGroups();
                LoadSemesters();
                LoadScheduleData();

                button2.Visible = false;
                label1.Visible = false;
                textBox1.Visible = false;
                button3.Visible = false;
                button5.Location = new Point(12, 401);
                button6.Location = new Point(214, 401);
            }

            label2.Text = "Пользователь: " + Session.Fio;
            label2.Left = this.ClientSize.Width - label2.Width - 20;

            if (Session.Role == "guest")
            {
                button2.Visible = false;
                label1.Visible = false;
                textBox1.Visible = false;
                button3.Visible = false;

            }
            else if (Session.Role == "employee")
            {
                button2.Visible = false;
                label1.Visible = false;
                textBox1.Visible = false;
                button3.Visible = false;
                comboBox1.Visible = false;
                comboBox2.Visible = false;
                button5.Visible = false;
                button6.Visible = false;

            }
            else if (Session.Role == "manager")
            {
                button2.Visible = false;
                label1.Visible = false;
                textBox1.Visible = false;
                button3.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm A = new MainForm();
            A.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string table = MainForm.table;
                string table_id = MainForm.table_id;

                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Введите ID для удаления.");
                    return;
                }

                int id = Convert.ToInt32(textBox1.Text);

                using (MySqlConnection myConnection = new MySqlConnection(connect))
                {
                    myConnection.Open();

                    using (MySqlCommand disableFK = new MySqlCommand("SET FOREIGN_KEY_CHECKS = 0;", myConnection))
                    {
                        disableFK.ExecuteNonQuery();
                    }

                    try
                    {
                        string deleteRecord = $"DELETE FROM {table} WHERE {table_id} = @id";
                        using (MySqlCommand cmdDeleteRecord = new MySqlCommand(deleteRecord, myConnection))
                        {
                            cmdDeleteRecord.Parameters.AddWithValue("@id", id);
                            cmdDeleteRecord.ExecuteNonQuery();
                        }
                    }
                    finally
                    {
                        using (MySqlCommand enableFK = new MySqlCommand("SET FOREIGN_KEY_CHECKS = 1;", myConnection))
                        {
                            enableFK.ExecuteNonQuery();
                        }
                    }
                }

                EditForm_Load(sender, e);
                MessageBox.Show("Удаление выполнено.");
            }
            catch (MySqlException ex)
            {
                if (MainForm.table == "Authors1")
                {
                    MessageBox.Show(
                        "Нельзя удалить автора, пока у него есть книги!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else if (MainForm.table == "Genres")
                {
                    MessageBox.Show(
                        "Нельзя удалить жанр, пока у него есть книги!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(
                        "Ошибка базы данных:\n" + ex.Message,
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MainForm.table == "schedule")
            {
                using (Schedule form = new Schedule())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadScheduleData();
                    }
                }

                return;
            }

            string table = MainForm.table;

            using (Form popup = new Form())
            {
                popup.Text = $"Добавить запись в {table}";
                popup.Size = new Size(400, 500);
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.FormBorderStyle = FormBorderStyle.FixedDialog;
                popup.MaximizeBox = false;
                popup.MinimizeBox = false;

                List<TextBox> textBoxes = new List<TextBox>();
                List<string> columnNames = new List<string>();

                int yPosition = 20;

                try
                {
                    string connect = "Database=college;server=localhost;user id=root;password=1111";

                    using (MySqlConnection myConnection = new MySqlConnection(connect))
                    {
                        myConnection.Open();

                        string query = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{table}' ORDER BY ORDINAL_POSITION";
                        MySqlCommand cmd = new MySqlCommand(query, myConnection);
                        MySqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string columnName = reader["COLUMN_NAME"].ToString();
                            columnNames.Add(columnName);

                            Label label = new Label();
                            label.Text = columnName;
                            label.Location = new Point(20, yPosition);
                            label.Size = new Size(120, 20);
                            popup.Controls.Add(label);

                            TextBox textBox = new TextBox();
                            textBox.Location = new Point(150, yPosition);
                            textBox.Size = new Size(200, 20);
                            textBox.Tag = columnName;
                            popup.Controls.Add(textBox);
                            textBoxes.Add(textBox);

                            yPosition += 30;
                        }
                        reader.Close();
                    }

                    Button addButton = new Button();
                    addButton.Text = "Добавить";
                    addButton.Location = new Point(150, yPosition + 20);
                    addButton.Size = new Size(100, 30);
                    popup.Controls.Add(addButton);

                    addButton.Click += (s, args) =>
                    {
                        try
                        {
                            using (MySqlConnection myConnection = new MySqlConnection(connect))
                            {
                                myConnection.Open();

                                List<string> insertColumns = new List<string>();
                                List<string> parameterNames = new List<string>();
                                List<MySqlParameter> parameters = new List<MySqlParameter>();

                                for (int i = 0; i < textBoxes.Count; i++)
                                {
                                    string columnName = textBoxes[i].Tag.ToString();
                                    string value = textBoxes[i].Text;

                                    if (!string.IsNullOrWhiteSpace(value))
                                    {
                                        insertColumns.Add(columnName);
                                        parameterNames.Add($"@{columnName}");
                                        parameters.Add(new MySqlParameter($"@{columnName}", value));
                                    }
                                }

                                if (insertColumns.Count == 0)
                                {
                                    MessageBox.Show("Заполните хотя бы одно поле!", "Предупреждение",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                string columns = string.Join(", ", insertColumns);
                                string parametersStr = string.Join(", ", parameterNames);
                                string insertQuery = $"INSERT INTO {table} ({columns}) VALUES ({parametersStr})";

                                using (MySqlCommand cmd = new MySqlCommand(insertQuery, myConnection))
                                {
                                    cmd.Parameters.AddRange(parameters.ToArray());
                                    int rowsAffected = cmd.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        MessageBox.Show("Запись успешно добавлена!", "Успех",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        popup.Close();
                                        EditForm_Load(sender, e);
                                    }
                                }
                            }
                        }
                        catch (MySqlException ex)
                        {
                            MessageBox.Show($"Ошибка базы данных: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };

                    Button cancelButton = new Button();
                    cancelButton.Text = "Отмена";
                    cancelButton.Location = new Point(260, yPosition + 20);
                    cancelButton.Size = new Size(100, 30);
                    cancelButton.DialogResult = DialogResult.Cancel;
                    popup.Controls.Add(cancelButton);

                    popup.AcceptButton = addButton;
                    popup.CancelButton = cancelButton;

                    DialogResult result = popup.ShowDialog();

                    if (result == DialogResult.Cancel) { }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке структуры таблицы: {ex.Message}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loadingCombos && MainForm.table == "schedule")
                LoadScheduleData();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loadingCombos && MainForm.table == "schedule")
                LoadScheduleData();
        }

        private void LoadScheduleData()
        {
            if (_loadingCombos)
                return;

            int groupId = 0;
            int semester = 0;

            //Получаем ID группы
            if (comboBox1.SelectedValue != null &&
                !(comboBox1.SelectedValue is DataRowView))
            {
                int.TryParse(comboBox1.SelectedValue.ToString(), out groupId);
            }

            //Получаем номер семестра
            if (comboBox2.SelectedValue != null &&
                !(comboBox2.SelectedValue is DataRowView))
            {
                int.TryParse(comboBox2.SelectedValue.ToString(), out semester);
            }

            using (MySqlConnection myConnection = new MySqlConnection(connect))
            {
                myConnection.Open();

                string query = @"
                SELECT 
                    s.schedule_id,
                    s.academic_year AS 'Учебный год',
                    s.semester AS 'Семестр',
                    s.day_of_week AS 'День недели',
                    s.pair_no AS 'Номер пары',
                    s.start_time AS 'Время начала',
                    s.end_time AS 'Время окончания',
                    g.group_number AS 'Группа',
                    d.discipline_name AS 'Дисциплина',
                    CONCAT(t.surname, ' ', t.name, ' ', t.patronymic) AS 'ФИО преподавателя',
                    s.cabinet AS 'Кабинет',
                    s.lesson_type AS 'Вид занятий'
                FROM schedule s
                LEFT JOIN student_groups g ON s.group_id = g.group_id
                LEFT JOIN disciplines d ON s.discipline_id = d.discipline_id
                LEFT JOIN teachers t ON s.teacher_id = t.teacher_id
                WHERE 1 = 1";

                if (groupId > 0)
                {
                    query += " AND s.group_id = @group_id";
                }

                if (semester > 0)
                {
                    query += " AND s.semester = @semester";
                }

                query += @"
                ORDER BY 
                    FIELD(s.day_of_week,
                        'Понедельник',
                        'Вторник',
                        'Среда',
                        'Четверг',
                        'Пятница',
                        'Суббота'
                    ),
                    s.pair_no";

                using (MySqlCommand cmd = new MySqlCommand(query, myConnection))
                {
                    if (groupId > 0)
                    {
                        cmd.Parameters.AddWithValue("@group_id", groupId);
                    }

                    if (semester > 0)
                    {
                        cmd.Parameters.AddWithValue("@semester", semester);
                    }

                    MySqlDataAdapter tab = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    tab.Fill(dt);

                    dataGridView1.DataSource = dt;

                    if (dataGridView1.Columns.Contains("schedule_id"))
                    {
                        dataGridView1.Columns["schedule_id"].Visible = false;
                    }
                }
            }
        }

        private void LoadGroups()
        {
            _loadingCombos = true;

            using (MySqlConnection myConnection = new MySqlConnection(connect))
            {
                myConnection.Open();

                string query = @"
                SELECT 0 AS group_id, 'Все группы' AS group_number, 0 AS sort_order
                UNION ALL
                SELECT group_id, group_number, 1 AS sort_order
                FROM student_groups
                ORDER BY sort_order, group_number";

                MySqlDataAdapter da = new MySqlDataAdapter(query, myConnection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox1.DisplayMember = "group_number";
                comboBox1.ValueMember = "group_id";
                comboBox1.DataSource = dt;
                comboBox1.SelectedIndex = 0;
            }

            _loadingCombos = false;
        }

        private void LoadSemesters()
        {
            _loadingCombos = true;

            using (MySqlConnection myConnection = new MySqlConnection(connect))
            {
                myConnection.Open();

                string query = @"
                SELECT 0 AS semester, 'Все семестры' AS semester_text, 0 AS sort_order
                UNION ALL
                SELECT DISTINCT CAST(semester AS UNSIGNED) AS semester, semester AS semester_text, 1 AS sort_order
                FROM schedule
                ORDER BY sort_order, semester";

                MySqlDataAdapter da = new MySqlDataAdapter(query, myConnection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox2.DisplayMember = "semester_text";
                comboBox2.ValueMember = "semester";
                comboBox2.DataSource = dt;
                comboBox2.SelectedIndex = 0;
            }

            _loadingCombos = false;
        }

        private void LoadTableData(string table)
        {
            using (MySqlConnection myConnection = new MySqlConnection(connect))
            {
                myConnection.Open();

                string CommandText = "SELECT * FROM `" + table + "`";
                MySqlDataAdapter tab = new MySqlDataAdapter(CommandText, myConnection);
                DataTable dt = new DataTable();
                tab.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MainForm.table != "schedule")
                return;

            if (e.RowIndex < 0)
                return;

            object value = dataGridView1.Rows[e.RowIndex]
                .Cells["schedule_id"]
                .Value;

            if (value == null || value == DBNull.Value)
                return;

            int scheduleId = Convert.ToInt32(value);

            Schedule schedule = new Schedule(scheduleId);
            schedule.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Schedule schedule = new Schedule();
            schedule.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                return;
            }

            object value = dataGridView1.CurrentRow.Cells["schedule_id"].Value;

            if (value == null || value == DBNull.Value)
            {
                MessageBox.Show("Не удалось определить ID занятия.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int scheduleId = Convert.ToInt32(value);

            DialogResult result = MessageBox.Show(
                "Вы действительно хотите удалить занятие?\n\nЭто действие нельзя отменить.",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes)
                return;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connect))
                {
                    conn.Open();

                    string query = "DELETE FROM schedule WHERE schedule_id = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", scheduleId);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadScheduleData();

                MessageBox.Show("Занятие удалено.", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ошибка базы данных:\n" + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка:\n" + ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
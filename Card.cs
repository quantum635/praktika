using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace WindowsFormsApp2
{
    public partial class Card : Form
    {
        private string selectedSpec = "Все специальности";
        private List<UserControl1> allCards = new List<UserControl1>();
        private int sortMode = 0;
        private string booknum = "";
        private UserControl1 selectedCard = null;

        public Card()
        {
            InitializeComponent();

            textBox1.Enter += textBox1_Enter;
            textBox1.Leave += textBox1_Leave;

            textBox1.Text = "Поиск";
            textBox1.ForeColor = Color.Gray;
        }

        private void Card_Load(object sender, EventArgs e)
        {
            if (Session.Role == "employee")
            {
                textBox1.Visible = false;
                label1.Visible = false;
                button2.Visible = false;
                label3.Visible = false;
                comboBox1.Visible = false;
                button3.Visible = false;
                button6.Visible = false;
            }
            if (Session.Role == "manager")
            {
                button3.Visible = false;
                button6.Visible = false;
            }

            string connect = "Database=college;server=localhost;user id=root;password=1111";
            MySqlConnection conn = new MySqlConnection(connect);
            conn.Open();

            string query = @"
            SELECT s.*, g.group_number, g.admission_year, sp.speciality_name,
            t.surname AS teacher_surname, t.name AS teacher_name, t.patronymic AS teacher_patronymic
            FROM students s
            LEFT JOIN student_groups g ON s.group_id = g.group_id
            LEFT JOIN specialities sp ON g.speciality_code = sp.speciality_code
            LEFT JOIN teachers t ON g.teacher_id = t.teacher_id";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            allCards.Clear();
            flowLayoutPanel1.Controls.Clear();

            while (reader.Read())
            {
                // Получаем номер группы
                string group = reader.IsDBNull(reader.GetOrdinal("group_number"))
                ? "Не назначена" : reader.GetString("group_number");
                // Получаем название специальности
                string speciality = reader.IsDBNull(reader.GetOrdinal("speciality_name"))
                ? "Не назначена" : reader.GetString("speciality_name");
                // Получаем фио куратора
                string teacherSurname = reader.IsDBNull(reader.GetOrdinal("teacher_surname"))
                    ? ""
                    : reader.GetString("teacher_surname");
                string teacherName = reader.IsDBNull(reader.GetOrdinal("teacher_name"))
                    ? ""
                    : reader.GetString("teacher_name");
                string teacherPatronymic = reader.IsDBNull(reader.GetOrdinal("teacher_patronymic"))
                    ? ""
                    : reader.GetString("teacher_patronymic");
                string teacher = string.IsNullOrEmpty(teacherSurname)
                ? "Куратор не назначен" : $"{teacherSurname} {teacherName} {teacherPatronymic}".Trim();
                // Получаем год поступления
                string year = reader.IsDBNull(reader.GetOrdinal("admission_year"))
                ? "Год поступления не указан" : reader.GetInt32("admission_year").ToString();

                UserControl1 card = new UserControl1
                {
                    Fio = $"{reader.GetString("surname")} {reader.GetString("name")} {reader.GetString("patronymic")}",
                    Group = $"{group} | ",
                    Speciality = $"{speciality} | ",
                    BirthDate = reader.GetDateTime("birth_date"),
                    Teacher = "Куратор: " + teacher,
                    AdmissionYear = "Год поступления: " + year,
                    Status = reader.GetString("student_status"),
                    StudyForm = "Форма обучения: " + reader.GetString("study_form"),
                    RecordBookNumber = "№ зачётной книжки: " + reader.GetString("record_book_no"),
                };
                card.OriginalFio = card.Fio;
                card.OriginalGroup = card.Group;
                card.OriginalSpeciality = card.Speciality;
                card.OriginalTeacher = card.Teacher;
                card.OriginalStatus = card.Status;
                card.OriginalStudyForm = card.StudyForm;
                card.OriginalRecordBookNumber = card.RecordBookNumber;
                card.OriginalBirthDateText = card.BirthDate.ToString("dd.MM.yyyy");
                card.OriginalAdmissionYearText = year;

                card.MouseDoubleClick += Card_DoubleClick;
                card.label2.MouseDoubleClick += Card_DoubleClick;
                card.label4.MouseDoubleClick += Card_DoubleClick;
                card.label5.MouseDoubleClick += Card_DoubleClick;
                card.label6.MouseDoubleClick += Card_DoubleClick;
                card.label7.MouseDoubleClick += Card_DoubleClick;
                card.label8.MouseDoubleClick += Card_DoubleClick;
                card.label9.MouseDoubleClick += Card_DoubleClick;
                card.pictureBox1.MouseDoubleClick += Card_DoubleClick;
                card.MouseDoubleClick += Card_DoubleClick;

                card.Click += Card_Click;
                card.MouseClick += Card_Click;
                foreach (Control c in card.Controls)
                {
                    c.Click += Card_Click;
                }

                card.SearchBlob =
                    Normalize(card.OriginalFio) + " " +
                    Normalize(card.OriginalGroup) + " " +
                    Normalize(card.OriginalSpeciality) + " " +
                    Normalize(card.OriginalTeacher) + " " +
                    Normalize(card.OriginalStatus) + " " +
                    Normalize(card.OriginalStudyForm) + " " +
                    Normalize(card.OriginalRecordBookNumber) + " " +
                    Normalize(card.OriginalBirthDateText) + " " +
                    Normalize(card.OriginalAdmissionYearText) + " " +
                    "датарождения датарожд годпоступления годпост";

                card.label2.Text = card.Group + card.Speciality + card.Fio;
                card.label4.Text = "Дата рождения: " + card.BirthDate.ToString("dd.MM.yyyy");
                card.label5.Text = card.Teacher;
                card.label6.Text = "Год поступления: " + year;
                card.label7.Text = card.Status;

                card.label7.BackColor = Color.Transparent;
                if (card.Status == "отчислен")
                {
                    card.label7.BackColor = ColorTranslator.FromHtml("#FFCCCC");

                    card.label7.AutoSize = true;
                    card.label7.TextAlign = ContentAlignment.MiddleRight;
                    card.label7.Left = card.Width - card.label7.PreferredWidth - 90;
                    //card.label7.Left = this.ClientSize.Width - label2.Width + 10;
                }
                else if (card.Status == "академический отпуск")
                {
                    card.label7.BackColor = ColorTranslator.FromHtml("#FFF2CC");
                }
                else
                {
                    card.label7.AutoSize = true;
                    card.label7.TextAlign = ContentAlignment.MiddleRight;
                    card.label7.Left = card.Width - card.label7.PreferredWidth - 85;
                    //card.label7.Left = this.ClientSize.Width - label2.Width + 10;
                }

                card.label8.Text = card.StudyForm;
                card.label9.Text = card.RecordBookNumber;
                string photoPath = reader.IsDBNull(reader.GetOrdinal("photo_path"))
                ? ""
                : reader.GetString("photo_path");

                if (string.IsNullOrWhiteSpace(photoPath) || !File.Exists(photoPath))
                {
                    photoPath = @"C:\Users\User\source\repos\WindowsFormsApp2\Resources\picture.png";
                }

                card.Photo = photoPath;
                card.pictureBox1.Image?.Dispose();
                card.pictureBox1.Image = LoadImageNoLock(photoPath);
                allCards.Add(card);
                flowLayoutPanel1.Controls.Add(card);
            }

            label2.Text = "Пользователь: " + Session.Fio;
            label2.Left = this.ClientSize.Width - label2.Width - 20;

            reader.Close();

            var specs = new List<string>();

            string specQuery = "SELECT DISTINCT speciality_name FROM specialities";
            MySqlCommand specCmd = new MySqlCommand(specQuery, conn);
            MySqlDataReader specReader = specCmd.ExecuteReader();

            while (specReader.Read())
            {
                specs.Add(specReader.GetString(0));
            }

            specReader.Close();

            comboBox1.Items.Clear();
            comboBox1.Items.Add("Все специальности");
            comboBox1.Items.AddRange(specs.ToArray());
            comboBox1.SelectedIndex = 0;

            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Поиск")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Поиск";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            RefreshView();
        }

        string Normalize(string s)
        {
            return (s ?? "")
                .ToLower()
                .Replace(" ", "")
                .Replace(":", "")
                .Replace("|", "")
                .Replace(".", "")
                .Replace(",", "");
        }

        void ResetColors(UserControl1 student)
        {
            student.label2.BackColor = Color.Transparent;
            student.label4.BackColor = Color.Transparent;
            student.label5.BackColor = Color.Transparent;
            student.label6.BackColor = Color.Transparent;
            student.label7.BackColor = Color.Transparent;
            student.label8.BackColor = Color.Transparent;
            student.label9.BackColor = Color.Transparent;
        }

        void ApplyStatusColor(UserControl1 card)
        {
            string status = Normalize(card.OriginalStatus);

            if (status == "отчислен")
                card.label7.BackColor = ColorTranslator.FromHtml("#FFCCCC");
            else if (status == "академическийотпуск")
                card.label7.BackColor = ColorTranslator.FromHtml("#FFF2CC");
            else
                card.label7.BackColor = Color.Transparent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sortMode++;

            if (sortMode > 2)
                sortMode = 0;

            button2.Text = sortMode == 0
                ? "Без сортировки"
                : sortMode == 1
                    ? "Год поступления↑"
                    : "Год поступления↓";

            RefreshView();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedSpec = comboBox1.SelectedItem.ToString();
            RefreshView();
        }

        private void RefreshView()
        {
            string rawSearch = textBox1.Text.Trim();
            bool isSearchEmpty = string.IsNullOrWhiteSpace(rawSearch) || rawSearch == "Поиск";
            string searchNorm = Normalize(rawSearch);

            IEnumerable<UserControl1> result = allCards;

            if (selectedSpec != "Все специальности")
            {
                result = result.Where(c =>
                    c.OriginalSpeciality != null &&
                    c.OriginalSpeciality.Contains(selectedSpec));
            }

            if (!isSearchEmpty)
            {
                result = result.Where(c =>
                    c.SearchBlob != null &&
                    c.SearchBlob.Contains(searchNorm));
            }

            if (sortMode == 1)
            {
                result = result.OrderBy(c =>
                    int.TryParse(c.OriginalAdmissionYearText, out int y) ? y : 0);
            }
            else if (sortMode == 2)
            {
                result = result.OrderByDescending(c =>
                    int.TryParse(c.OriginalAdmissionYearText, out int y) ? y : 0);
            }

            foreach (var card in allCards)
            {
                ResetColors(card);
                ApplyStatusColor(card);
            }

            if (!isSearchEmpty)
            {
                foreach (var card in result)
                {
                    string nStatus = Normalize(card.OriginalStatus);
                    string nFio = Normalize(card.OriginalFio);
                    string nGroup = Normalize(card.OriginalGroup);
                    string nSpeciality = Normalize(card.OriginalSpeciality);
                    string nTeacher = Normalize(card.OriginalTeacher);
                    string nBirth = Normalize(card.OriginalBirthDateText);
                    string nYear = Normalize(card.OriginalAdmissionYearText);
                    string nStudy = Normalize(card.OriginalStudyForm);
                    string nRecord = Normalize(card.OriginalRecordBookNumber);

                    if (nStatus.Contains(searchNorm))
                        card.label7.BackColor = Color.Yellow;

                    if (nFio.Contains(searchNorm))
                        card.label2.BackColor = Color.Yellow;

                    if (nGroup.Contains(searchNorm))
                        card.label2.BackColor = Color.Yellow;

                    if (nSpeciality.Contains(searchNorm))
                        card.label2.BackColor = Color.Yellow;

                    if (nTeacher.Contains(searchNorm))
                        card.label5.BackColor = Color.Yellow;

                    if (nBirth.Contains(searchNorm) ||
                        searchNorm.Contains("дата") || searchNorm.Contains("рожд") ||
                        searchNorm.Contains("дн") || searchNorm.Contains("рож"))
                    {
                        card.label4.BackColor = Color.Yellow;
                    }

                    if (nYear.Contains(searchNorm) || searchNorm.Contains("год") || searchNorm.Contains("поступ"))
                        card.label6.BackColor = Color.Yellow;

                    if (nStudy.Contains(searchNorm))
                        card.label8.BackColor = Color.Yellow;

                    if (nRecord.Contains(searchNorm))
                        card.label9.BackColor = Color.Yellow;
                }
            }

            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.Controls.Clear();

            foreach (var card in result)
                flowLayoutPanel1.Controls.Add(card);

            flowLayoutPanel1.ResumeLayout();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Students students = new Students('+');
            students.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm();
            main.Show();
            this.Hide();
        }

        private void Card_DoubleClick(object sender, MouseEventArgs e)
        {
            if (Session.Role == "employee" || Session.Role == "manager")
                return;

            UserControl1 card = FindCard(sender as Control);
            if (card == null) return;

            string recordBookNo = card.RecordBookNumber
                .Replace("№ зачётной книжки: ", "")
                .Trim();

            Students form = new Students('=');
            form.LoadStudent(recordBookNo);

            form.Show();
            this.Hide();
        }

        private void Card_Click(object sender, EventArgs e)
        {
            if (Session.Role == "employee" || Session.Role == "manager")
                return;

            UserControl1 card = FindCard(sender as Control);
            if (card == null) return;

            SetSelectedCard(card);

            booknum = card.RecordBookNumber
                .Replace("№ зачётной книжки: ", "")
                .Trim();
        }

        private void SetSelectedCard(UserControl1 card)
        {
            if (selectedCard != null)
                selectedCard.BackColor = Color.White;

            selectedCard = card;

            if (selectedCard != null)
                selectedCard.BackColor = Color.LightSkyBlue;
        }

        private UserControl1 FindCard(Control ctrl)
        {
            while (ctrl != null && !(ctrl is UserControl1))
                ctrl = ctrl.Parent;

            return ctrl as UserControl1;
        }

        public void ReloadData()
        {
            selectedCard = null;
            booknum = "";
            Card_Load(null, null);
        }

        private Image LoadImageNoLock(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var ms = new MemoryStream())
            {
                fs.CopyTo(ms);
                ms.Position = 0;
                return Image.FromStream(ms);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (selectedCard == null)
            {
                return;
            }

            string connect = "Database=college;server=localhost;user id=root;password=1111";
            string photoPath = null;

            //Получаем путь к фото из БД
            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string photoQuery = "SELECT photo_path FROM students WHERE record_book_no = @booknum";

                using (MySqlCommand cmd = new MySqlCommand(photoQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@booknum", booknum);

                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        photoPath = result.ToString();
                }

                //Проверка оценок
                string checkQuery = "SELECT COUNT(*) FROM performance WHERE record_book_no = @booknum";

                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@booknum", booknum);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show(
                            "Нельзя удалить студента!\n\nУ него есть оценки.",
                            "Ошибка удаления",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            //Подтверждение удаления
            DialogResult dialog = MessageBox.Show(
                "Вы действительно хотите удалить студента?\n\nЭто действие нельзя отменить.",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (dialog != DialogResult.Yes)
                return;

            //Удаление из БД
            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string deleteQuery = "DELETE FROM students WHERE record_book_no = @booknum";

                using (MySqlCommand cmd = new MySqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@booknum", booknum);
                    cmd.ExecuteNonQuery();
                }
            }

            //Удаление фото с диска
            if (!string.IsNullOrWhiteSpace(photoPath) &&
                photoPath != @"C:\Users\User\source\repos\WindowsFormsApp2\Resources\picture.png" &&
                File.Exists(photoPath))
            {
                File.Delete(photoPath);
            }

            this.ReloadData();
            this.flowLayoutPanel1.Refresh();
            this.flowLayoutPanel1.Update();

            MessageBox.Show(
                "Студент успешно удалён",
                "Готово",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
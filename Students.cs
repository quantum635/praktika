using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WindowsFormsApp2
{
    public partial class Students : Form
    {
        private string photoPath = "";
        private string pendingPhotoSourcePath = "";

        public Students(char action)
        {
            InitializeComponent();

            if (action == '+')
            {
                //группы для comboBox1
                string connect = "Database=college;server=localhost;user id=root;password=1111";

                using (MySqlConnection conn = new MySqlConnection(connect))
                {
                    conn.Open();

                    string query = "SELECT group_id, group_number FROM student_groups";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    comboBox1.DataSource = table;
                    comboBox1.DisplayMember = "group_number";
                    comboBox1.ValueMember = "group_id";
                }
                //специальность для comboBox2
                int groupId;

                if (!int.TryParse(comboBox1.SelectedValue.ToString(), out groupId))
                    return;

                connect = "Database=college;server=localhost;user id=root;password=1111";

                using (MySqlConnection conn = new MySqlConnection(connect))
                {
                    conn.Open();

                    string query = @"
                    SELECT speciality_name
                    FROM student_groups g
                    JOIN specialities s
                        ON g.speciality_code = s.speciality_code
                    WHERE g.group_id = @id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", groupId);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                        comboBox2.Text = result.ToString();
                        comboBox2.Items.Insert(0, (string)result);
                }
            }
            else
            {
                this.Text = "Редактирование студента";

                button3.Click -= button3_Click;
                button3.Click += button3_1_Click;
                button2.Click -= button2_Click;
                button2.Click += button2_1_Click;

                button4.Text = "изменить\nданные";
                button4.Size = new Size(156, 58);
                button4.Location = new Point(745, 171);
                button4.Click -= button4_Click;
                button4.Click += button4_1_Click;

                button1.Click -= button1_Click;
                button1.Click += button1_1_Click;
            }

            label11.Text = "Пользователь: " + Session.Fio;
            label11.Left = this.ClientSize.Width - label2.Width - 350;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login login = new Login();

            //удаление изображения
            if (string.IsNullOrEmpty(photoPath))
            {
                login.Show();
                this.Hide();
                return;
            }

            try
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }

                if (File.Exists(photoPath))
                {
                    File.Delete(photoPath);

                    string connect = "Database=college;server=localhost;user id=root;password=1111";
                    try
                    {
                        string recordBookNo = textBox5.Text.Trim();

                        using (MySqlConnection conn = new MySqlConnection(connect))
                        {
                            conn.Open();

                            string selectQuery = "SELECT photo_path FROM students WHERE record_book_no = @recordBookNo";

                            using (MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn))
                            {
                                selectCmd.Parameters.AddWithValue("@recordBookNo", recordBookNo);

                                object resultPath = selectCmd.ExecuteScalar();

                                if (resultPath == null)
                                {
                                    return;
                                }
                            }

                            string deleteQuery = "DELETE FROM students WHERE record_book_no = @recordBookNo";

                            using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn))
                            {
                                deleteCmd.Parameters.AddWithValue("@recordBookNo", recordBookNo);

                                deleteCmd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка удаления: " + ex.Message);
                    }
                }

                photoPath = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении: " + ex.Message);
            }

            login.Show();
            this.Hide();
        }

        private void button2_1_Click(object sender, EventArgs e)
        {
            pendingPhotoSourcePath = "";
            new Login().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Card card = new Card();

            //удаление изображения
            if (string.IsNullOrEmpty(photoPath))
            {
                
                card.Show();
                this.Hide();
                return;
            }

            try
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }

                if (File.Exists(photoPath))
                {
                    File.Delete(photoPath);

                    string connect = "Database=college;server=localhost;user id=root;password=1111";
                    try
                    {
                        string recordBookNo = textBox5.Text.Trim();

                        using (MySqlConnection conn = new MySqlConnection(connect))
                        {
                            conn.Open();

                            string selectQuery = "SELECT photo_path FROM students WHERE record_book_no = @recordBookNo";

                            using (MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn))
                            {
                                selectCmd.Parameters.AddWithValue("@recordBookNo", recordBookNo);

                                object resultPath = selectCmd.ExecuteScalar();

                                if (resultPath == null)
                                {
                                    return;
                                }
                            }

                            string deleteQuery = "DELETE FROM students WHERE record_book_no = @recordBookNo";

                            using (MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn))
                            {
                                deleteCmd.Parameters.AddWithValue("@recordBookNo", recordBookNo);

                                deleteCmd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка удаления: " + ex.Message);
                    }
                }

                photoPath = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении: " + ex.Message);
            }

            card.Show();
            this.Hide();
        }

        private void button3_1_Click(object sender, EventArgs e)
        {
            pendingPhotoSourcePath = "";
            new Card().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileName = "";
            if (textBox5.Text.Length == 0 || textBox1.Text.Length == 0
                || textBox2.Text.Length == 0 || textBox3.Text.Length == 0
                || textBox4.Text.Length == 0 || textBox6.Text.Length == 0)
            {
                MessageBox.Show(
                    $"Сначала заполните другие поля!",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            else
            {
                fileName = $"picture({textBox5.Text}).png";
            }

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = "Выберите фотографию студента";
            ofd.Filter = "Изображение|*.png";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (var fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (Image img = Image.FromStream(fs))
                {
                    if (img.Width != 300 || img.Height != 200)
                    {
                        MessageBox.Show(
                        $"Размер изображения {img.Width}x{img.Height}.\nМаксимально допустимо 300x200 пикселей!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                        return;
                    }
                }

                string imagesFolder = @"C:\Users\User\source\repos\WindowsFormsApp2\Resources";

                if (!Directory.Exists(imagesFolder))
                    Directory.CreateDirectory(imagesFolder);

                string newPath = Path.Combine(imagesFolder, fileName);

                //сохранение пути в БД
                string connect = "Database=college;server=localhost;user id=root;password=1111";

                string insertQuery = @"
                INSERT INTO students (record_book_no, surname, name, patronymic, birth_date, group_id,
                student_status, study_form, photo_path)
                VALUES (@recordBookNo, @surname, @name, @patronymic, @birthDate, @groupId, @studentStatus,
                @studyForm, @photoPath)";

                using (MySqlConnection conn = new MySqlConnection(connect))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@recordBookNo", textBox5.Text.Trim());

                        cmd.Parameters.AddWithValue("@surname", textBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@name", textBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@patronymic", textBox3.Text.Trim());

                        DateTime birthDate;

                        if (!DateTime.TryParse(textBox4.Text, out birthDate))
                        {
                            MessageBox.Show("Неверная дата рождения. Пример: 20.06.2006");
                            return;
                        }

                        cmd.Parameters.AddWithValue("@birthDate", birthDate);

                        cmd.Parameters.AddWithValue(
                            "@groupId",
                            Convert.ToInt32(comboBox1.SelectedValue)
                        );

                        cmd.Parameters.AddWithValue("@studentStatus", comboBox4.Text);
                        cmd.Parameters.AddWithValue("@studyForm", comboBox3.Text);

                        cmd.Parameters.AddWithValue("@photoPath", newPath);

                        cmd.ExecuteNonQuery();
                    }
                }

                if (File.Exists(newPath))
                {
                    try { File.Delete(newPath); }
                    catch { }
                }

                try
                {
                    if (File.Exists(newPath))
                    {
                        File.Delete(newPath);
                    }

                    File.Copy(ofd.FileName, newPath, true);

                    SetPicture(newPath);
                    photoPath = newPath;
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Файл используется системой. Закройте изображение и попробуйте снова.\n" + ex.Message);
                    return;
                }

                photoPath = newPath;
            }
        }

        private void button1_1_Click(object sender, EventArgs e)
        {
            if (textBox5.Text.Length == 0 || textBox1.Text.Length == 0 ||
                textBox2.Text.Length == 0 || textBox3.Text.Length == 0 ||
                textBox4.Text.Length == 0 || textBox6.Text.Length == 0)
            {
                MessageBox.Show("Сначала заполните все поля!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Выберите фотографию студента",
                Filter = "Изображение|*.png"
            };

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            using (var fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var img = Image.FromStream(fs))
            {
                if (img.Width != 300 || img.Height != 200)
                {
                    MessageBox.Show(
                        $"Размер изображения {img.Width}x{img.Height}.\nМаксимально допустимо 300x200 пикселей!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            }

            pendingPhotoSourcePath = ofd.FileName;
            SetPicture(ofd.FileName);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null)
                return;

            int groupId;

            if (!int.TryParse(comboBox1.SelectedValue.ToString(), out groupId))
                return;

            string connect = "Database=college;server=localhost;user id=root;password=1111";

            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                string query = @"
                    SELECT speciality_name
                    FROM student_groups g
                    JOIN specialities s
                        ON g.speciality_code = s.speciality_code
                    WHERE g.group_id = @id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", groupId);

                object result = cmd.ExecuteScalar();

                if (result != null)
                    comboBox2.Text = result.ToString();
                    try
                    {
                    comboBox2.Items.RemoveAt(0);
                    }
                    catch { }
                    comboBox2.Items.Insert(0, (string)result);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox5.Text.Length == 0 || textBox1.Text.Length == 0
                || textBox2.Text.Length == 0 || textBox3.Text.Length == 0
                || textBox4.Text.Length == 0 || textBox6.Text.Length == 0)
            {
                MessageBox.Show(
                    $"Сначала заполните другие поля",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (photoPath == "")
                {
                    //сохранение в БД
                    string connect = "Database=college;server=localhost;user id=root;password=1111";

                    string insertQuery = @"
                        INSERT INTO students (record_book_no, surname, name, patronymic, birth_date, group_id,
                        student_status, study_form, photo_path)
                        VALUES (@recordBookNo, @surname, @name, @patronymic, @birthDate, @groupId, @studentStatus,
                        @studyForm, @photoPath)";

                    using (MySqlConnection conn = new MySqlConnection(connect))
                    {
                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@recordBookNo", textBox5.Text.Trim());

                            cmd.Parameters.AddWithValue("@surname", textBox1.Text.Trim());
                            cmd.Parameters.AddWithValue("@name", textBox2.Text.Trim());
                            cmd.Parameters.AddWithValue("@patronymic", textBox3.Text.Trim());

                            DateTime birthDate;

                            if (!DateTime.TryParse(textBox4.Text, out birthDate))
                            {
                                MessageBox.Show("Неверная дата рождения. Пример: 20.06.2006");
                                return;
                            }

                            cmd.Parameters.AddWithValue("@birthDate", birthDate);

                            cmd.Parameters.AddWithValue(
                                "@groupId",
                                Convert.ToInt32(comboBox1.SelectedValue)
                            );

                            cmd.Parameters.AddWithValue("@studentStatus", comboBox4.Text);
                            cmd.Parameters.AddWithValue("@studyForm", comboBox3.Text);

                            cmd.Parameters.AddWithValue("@photoPath", "C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png");

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                Card card = new Card();
                card.Show();
                this.Hide();

                MessageBox.Show(
                    $"Студент был успешно добавлен!",
                    "Успех",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void button4_1_Click(object sender, EventArgs e)
        {
            if (textBox5.Text.Length == 0 || textBox1.Text.Length == 0 ||
                textBox2.Text.Length == 0 || textBox3.Text.Length == 0 ||
                textBox4.Text.Length == 0 || textBox6.Text.Length == 0)
            {
                MessageBox.Show("Сначала заполните другие поля",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connect = "Database=college;server=localhost;user id=root;password=1111";

            string imagesFolder = @"C:\Users\User\source\repos\WindowsFormsApp2\Resources";
            if (!Directory.Exists(imagesFolder))
                Directory.CreateDirectory(imagesFolder);

            string finalPhotoPath = photoPath;

            if (!string.IsNullOrEmpty(pendingPhotoSourcePath))
            {
                string fileName = $"picture({textBox5.Text}).png";
                finalPhotoPath = Path.Combine(imagesFolder, fileName);

                try
                {
                    File.Copy(pendingPhotoSourcePath, finalPhotoPath, true);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Не удалось сохранить фото:\n" + ex.Message);
                    return;
                }
            }

            string updateQuery = @"
                UPDATE students SET
                surname = @surname,
                name = @name,
                patronymic = @patronymic,
                birth_date = @birthDate,
                group_id = @groupId,
                student_status = @studentStatus,
                study_form = @studyForm,
                photo_path = @photoPath
                WHERE record_book_no = @recordBookNo";

            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@recordBookNo", textBox5.Text.Trim());
                    cmd.Parameters.AddWithValue("@surname", textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@name", textBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@patronymic", textBox3.Text.Trim());

                    if (!DateTime.TryParse(textBox4.Text, out DateTime birthDate))
                    {
                        MessageBox.Show("Неверная дата рождения", "Ошибка");
                        return;
                    }

                    cmd.Parameters.AddWithValue("@birthDate", birthDate);
                    cmd.Parameters.AddWithValue("@groupId", Convert.ToInt32(comboBox1.SelectedValue));
                    cmd.Parameters.AddWithValue("@studentStatus", comboBox4.Text);
                    cmd.Parameters.AddWithValue("@studyForm", comboBox3.Text);
                    cmd.Parameters.AddWithValue("@photoPath", finalPhotoPath);

                    cmd.ExecuteNonQuery();
                }
            }

            if (!string.IsNullOrEmpty(photoPath) &&
                photoPath != finalPhotoPath &&
                File.Exists(photoPath))
            {
                try { File.Delete(photoPath); } catch { }
            }

            photoPath = finalPhotoPath;
            pendingPhotoSourcePath = "";

            var card = Application.OpenForms.OfType<Card>().FirstOrDefault();
            if (card != null)
            {
                card.ReloadData();
                card.Show();
            }

            this.Close();

            MessageBox.Show("Студент успешно обновлён!", "Успех",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void LoadStudent(string recordBookNo)
        {
            string connect = "Database=college;server=localhost;user id=root;password=1111";

            string query = @"
                SELECT s.*, g.group_id, g.group_number, g.admission_year, sp.speciality_name
                FROM students s
                LEFT JOIN student_groups g ON s.group_id = g.group_id
                LEFT JOIN specialities sp ON g.speciality_code = sp.speciality_code
                WHERE s.record_book_no = @recordBookNo";

            using (MySqlConnection conn = new MySqlConnection(connect))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@recordBookNo", recordBookNo);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBox5.Text = reader["record_book_no"].ToString();

                            textBox1.Text = reader["surname"].ToString();
                            textBox2.Text = reader["name"].ToString();
                            textBox3.Text = reader["patronymic"].ToString();

                            textBox4.Text = Convert.ToDateTime(reader["birth_date"])
                                .ToString("dd.MM.yyyy");

                            //группы для comboBox1
                            string connect1 = "Database=college;server=localhost;user id=root;password=1111";

                            using (MySqlConnection conn1 = new MySqlConnection(connect1))
                            {
                                conn1.Open();

                                string query1 = "SELECT group_id, group_number FROM student_groups";

                                MySqlDataAdapter adapter = new MySqlDataAdapter(query1, conn1);
                                DataTable table = new DataTable();
                                adapter.Fill(table);

                                comboBox1.DataSource = table;
                                comboBox1.DisplayMember = "group_number";
                                comboBox1.ValueMember = "group_id";
                            }
                            comboBox1.Text = reader["group_number"].ToString();
                            
                            textBox6.Text = reader["admission_year"].ToString();

                            comboBox4.Text = reader["student_status"].ToString();
                            comboBox3.Text = reader["study_form"].ToString();

                            photoPath = reader["photo_path"].ToString();
                            if(photoPath == "")
                            {
                                string group = reader["record_book_no"].ToString();
                                photoPath = $"C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture({group}).png";
                                if (!File.Exists(photoPath))
                                {
                                    photoPath = $"C:\\Users\\User\\source\\repos\\WindowsFormsApp2\\Resources\\picture.png";
                                }
                            }

                            if (!string.IsNullOrEmpty(photoPath) && File.Exists(photoPath))
                            {
                                SetPicture(photoPath);
                            }
                            else
                            {
                                pictureBox1.Image = null;
                            }
                        }
                    }
                }
            }
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

        private void SetPicture(string path)
        {
            if (pictureBox1.Image != null)
            {
                var old = pictureBox1.Image;
                pictureBox1.Image = null;
                old.Dispose();
            }

            pictureBox1.Image = LoadImageNoLock(path);
        }
    }
}

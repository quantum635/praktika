using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class UserControl1 : UserControl
    {
        public string OriginalFio;
        public string OriginalGroup;
        public string OriginalSpeciality;
        public string OriginalTeacher;
        public string OriginalStatus;
        public string OriginalStudyForm;
        public string OriginalRecordBookNumber;
        public string OriginalBirthDateText;
        public string OriginalAdmissionYearText;
        public string SearchBlob;

        public string Fio { get; set; }
        public string Group { get; set; }
        public string Speciality { get; set; }
        public DateTime BirthDate { get; set; }
        public string Teacher { get; set; }
        public string AdmissionYear { get; set; }
        public string Status { get; set; }
        public string StudyForm { get; set; }
        public string RecordBookNumber { get; set; }
        public string Photo { get; set; } = string.Empty;

        public event EventHandler CardClicked;

        public UserControl1()
        {
            InitializeComponent();
            
            this.Click += Card_Click;
            foreach (Control c in this.Controls)
            {
                c.Click += Card_Click;
            }
        }

        private void Card_Click(object sender, EventArgs e)
        {
            CardClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
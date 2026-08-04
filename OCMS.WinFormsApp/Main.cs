using System;
using System.Windows.Forms;
using System.Linq;
using Domain.features.SubClass;
using Domain.features.Enrollment;
using Domain.models;

namespace OCMSWindowForm;

public partial class Main : Form
{
    private SubClassService _subClassService;
    private EnrollmentService _enrollmentService;

    public Main()
    {
        InitializeComponent();
        _subClassService = new SubClassService();
        _enrollmentService = new EnrollmentService();
        
        dgvSubClasses.DataBindingComplete += (s, e) => { if (s is DataGridView dgv) dgv.ClearSelection(); };
        dgvEnrollments.DataBindingComplete += (s, e) => { if (s is DataGridView dgv) dgv.ClearSelection(); };
    }

    private void Main_Load(object sender, EventArgs e)
    {
        LoadSubClasses();
        LoadEnrollments();
        
        btnClearSubClass_Click(null, null);
        ToggleSubClassInputs(false);
        
        btnClearEnrollment_Click(null, null);
        ToggleEnrollmentInputs(false);
    }

    private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (sender is DataGridView dgv && dgv.Columns.Contains("NoColumn") && e.ColumnIndex == dgv.Columns["NoColumn"].Index && e.RowIndex >= 0)
        {
            e.Value = (e.RowIndex + 1).ToString();
            e.CellStyle.ForeColor = System.Drawing.Color.Black;
            e.CellStyle.BackColor = System.Drawing.Color.White;
            e.CellStyle.SelectionBackColor = System.Drawing.Color.White;
            e.CellStyle.SelectionForeColor = System.Drawing.Color.Black;
            e.FormattingApplied = true;
        }
    }

    private void ToggleSubClassInputs(bool enabled)
    {
        txtClassName.Enabled = enabled;
        txtLocation.Enabled = enabled;
        numStudentLimit.Enabled = enabled;
        dtpOpenDate.Enabled = enabled;
        dtpOpenTime.Enabled = enabled;
        btnSaveSubClass.Enabled = enabled;
    }

    private void ToggleEnrollmentInputs(bool enabled)
    {
        cmbEnrSubClass.Enabled = enabled;
        txtStudentName.Enabled = enabled;
        txtStudentContact.Enabled = enabled;
        txtPaymentInfo.Enabled = enabled;
        txtFatherName.Enabled = enabled;
        btnSaveEnrollment.Enabled = enabled;
    }

    private void AddNoColumn(DataGridView dgv)
    {
        dgv.EnableHeadersVisualStyles = false;
        if (!dgv.Columns.Contains("NoColumn"))
        {
            var noCol = new DataGridViewTextBoxColumn();
            noCol.Name = "NoColumn";
            noCol.HeaderText = "No.";
            noCol.ReadOnly = true;
            noCol.Width = 50;
            noCol.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            noCol.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            noCol.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            noCol.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            noCol.HeaderCell.Style.BackColor = System.Drawing.Color.Black;
            noCol.HeaderCell.Style.ForeColor = System.Drawing.Color.White;
            dgv.Columns.Insert(0, noCol);
        }
        
        dgv.Columns["NoColumn"].DisplayIndex = 0;
    }

    private void AddActionColumns(DataGridView dgv)
    {
        if (!dgv.Columns.Contains("EditColumn"))
        {
            var editBtn = new DataGridViewButtonColumn();
            editBtn.Name = "EditColumn";
            editBtn.HeaderText = "Edit";
            editBtn.Text = "Edit";
            editBtn.UseColumnTextForButtonValue = true;
            dgv.Columns.Add(editBtn);
        }
        if (!dgv.Columns.Contains("DeleteColumn"))
        {
            var delBtn = new DataGridViewButtonColumn();
            delBtn.Name = "DeleteColumn";
            delBtn.HeaderText = "Delete";
            delBtn.Text = "Delete";
            delBtn.UseColumnTextForButtonValue = true;
            dgv.Columns.Add(delBtn);
        }
    }

    // --- SubClasses Tab ---

    private void btnLoadSubClasses_Click(object sender, EventArgs e)
    {
        LoadSubClasses();
    }

    private void LoadSubClasses()
    {
        var response = _subClassService.GetSubClasses(new SubClassListRequestModel());
        if (response.IsSuccess)
        {
            object dataSource = response.SubClasses;
            try
            {
                using var db = new OCMS.Database.AppDbContextModels.AppDbContext();
                var dbClasses = db.TblSubClasses.Where(x => !x.IsDelete).ToList();
                var resolvedClasses = response.SubClasses.Select(sc => 
                {
                    var match = dbClasses.FirstOrDefault(x => x.ClassName == sc.ClassName && x.Location == sc.Location && x.CreatedDateTime == sc.CreatedDateTime);
                    return new 
                    {
                        SubClassId = match?.SubClassId,
                        sc.ClassName,
                        sc.Location,
                        sc.OpenDate,
                        sc.StudentLimit,
                        sc.StudentCount,
                        sc.OpenTime
                    };
                }).ToList();
                dataSource = resolvedClasses;

                var dropdownList = resolvedClasses.Where(x => x.SubClassId.HasValue).Select(x => new 
                {
                    x.SubClassId,
                    Display = $"{x.ClassName} ({x.Location})"
                }).ToList();
                
                cmbEnrSubClass.DataSource = dropdownList;
                cmbEnrSubClass.DisplayMember = "Display";
                cmbEnrSubClass.ValueMember = "SubClassId";
                cmbEnrSubClass.SelectedIndex = -1;
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Could not apply ID workaround: " + ex.Message);
            }

            dgvSubClasses.DataSource = null;
            dgvSubClasses.Columns.Clear();
            dgvSubClasses.DataSource = dataSource;
            AddActionColumns(dgvSubClasses);
            if (dgvSubClasses.Columns.Contains("SubClassId")) dgvSubClasses.Columns["SubClassId"].Visible = false;
            
            AddNoColumn(dgvSubClasses);
            dgvSubClasses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSubClasses.ClearSelection();
        }
        else
        {
            MessageBox.Show(response.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnClearSubClass_Click(object sender, EventArgs e)
    {
        txtSelectedSubClassId.Text = string.Empty;
        txtClassName.Text = string.Empty;
        txtLocation.Text = string.Empty;
        numStudentLimit.Value = 0;
        dtpOpenDate.Value = DateTime.Today;
        dtpOpenTime.Value = DateTime.Now;
        dgvSubClasses.ClearSelection();
        ToggleSubClassInputs(true);
    }

    private void btnSaveSubClass_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtSelectedSubClassId.Text))
        {
            var req = new SubClassCreateRequestModel
            {
                ClassName = txtClassName.Text,
                Location = txtLocation.Text,
                StudentLimit = (int)numStudentLimit.Value,
                OpenDate = DateOnly.FromDateTime(dtpOpenDate.Value),
                OpenTime = TimeOnly.FromDateTime(dtpOpenTime.Value),
            };
            var res = _subClassService.CreateSubClass(req);
            MessageBox.Show(res.Message, "Create SubClass", MessageBoxButtons.OK, res.IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (res.IsSuccess) 
            {
                LoadSubClasses();
                btnClearSubClass_Click(null, null);
                ToggleSubClassInputs(false);
            }
        }
        else
        {
            if (int.TryParse(txtSelectedSubClassId.Text, out int id))
            {
                var req = new SubClassPatchRequestModel
                {
                    ClassName = txtClassName.Text,
                    Location = txtLocation.Text,
                    StudentLimit = (int)numStudentLimit.Value,
                    OpenDate = DateOnly.FromDateTime(dtpOpenDate.Value),
                    OpenTime = TimeOnly.FromDateTime(dtpOpenTime.Value)
                };
                var res = _subClassService.PatchSubClass(id, req);
                MessageBox.Show(res.Message, "Update SubClass", MessageBoxButtons.OK, res.IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                if (res.IsSuccess) 
                {
                    LoadSubClasses();
                    btnClearSubClass_Click(null, null);
                    ToggleSubClassInputs(false);
                }
            }
        }
    }

    private void dgvSubClasses_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            var row = dgvSubClasses.Rows[e.RowIndex];
            
            if (dgvSubClasses.Columns.Contains("EditColumn") && e.ColumnIndex == dgvSubClasses.Columns["EditColumn"].Index)
            {
                txtSelectedSubClassId.Text = row.Cells["SubClassId"].Value?.ToString();
                txtClassName.Text = row.Cells["ClassName"].Value?.ToString();
                txtLocation.Text = row.Cells["Location"].Value?.ToString();
                
                if (int.TryParse(row.Cells["StudentLimit"].Value?.ToString(), out int limit))
                    numStudentLimit.Value = limit;
                    
                try 
                {
                    DateTime parsedDate = DateTime.Today;
                    if (row.Cells["OpenDate"].Value is DateOnly dateOnly)
                        parsedDate = dateOnly.ToDateTime(TimeOnly.MinValue);
                    else if (DateTime.TryParse(row.Cells["OpenDate"].Value?.ToString(), out DateTime dt))
                        parsedDate = dt;

                    if (parsedDate >= dtpOpenDate.MinDate && parsedDate <= dtpOpenDate.MaxDate)
                        dtpOpenDate.Value = parsedDate;
                } catch { }

                try 
                {
                    DateTime parsedTime = DateTime.Now;
                    if (row.Cells["OpenTime"].Value is TimeOnly timeOnly)
                        parsedTime = DateTime.Today.Add(timeOnly.ToTimeSpan());
                    else if (DateTime.TryParse(row.Cells["OpenTime"].Value?.ToString(), out DateTime t))
                        parsedTime = t;

                    if (parsedTime >= dtpOpenTime.MinDate && parsedTime <= dtpOpenTime.MaxDate)
                        dtpOpenTime.Value = parsedTime;
                } catch { }

                ToggleSubClassInputs(true);
            }
            else if (dgvSubClasses.Columns.Contains("DeleteColumn") && e.ColumnIndex == dgvSubClasses.Columns["DeleteColumn"].Index)
            {
                if (int.TryParse(row.Cells["SubClassId"].Value?.ToString(), out int id))
                {
                    var dialogResult = MessageBox.Show("Are you sure you want to delete this SubClass?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        var req = new SubClassDeleteRequestModel { SubClassId = id };
                        var res = _subClassService.DeleteSubClass(req);
                        MessageBox.Show(res.Message, "Delete SubClass", MessageBoxButtons.OK, res.IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                        if (res.IsSuccess) 
                        {
                            LoadSubClasses();
                            btnClearSubClass_Click(null, null);
                            ToggleSubClassInputs(false);
                        }
                    }
                }
            }
            else
            {
                ToggleSubClassInputs(false);
            }
        }
    }

    // --- Enrollments Tab ---

    private void btnLoadEnrollments_Click(object sender, EventArgs e)
    {
        LoadEnrollments();
    }

    private void LoadEnrollments()
    {
        var response = _enrollmentService.GetEnrollments(new EnrollmentListRequestModel());
        if (response.IsSuccess)
        {
            object dataSource = response.Enrollments;
            try
            {
                using var db = new OCMS.Database.AppDbContextModels.AppDbContext();
                var dbEnrollments = db.TblEnrollments.ToList();
                dataSource = response.Enrollments.Select(en => 
                {
                    var match = dbEnrollments.FirstOrDefault(x => x.SubClassId == en.SubClassId && x.StudentName == en.StudentName && x.CreatedDateTime == en.CreatedDateTime);
                    return new 
                    {
                        EnrollmentId = match?.EnrollmentId,
                        en.SubClassId,
                        en.StudentName,
                        en.StudentContact,
                        en.PaymentInfo,
                        en.FatherName
                    };
                }).ToList();
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Could not apply ID workaround: " + ex.Message);
            }

            dgvEnrollments.DataSource = null;
            dgvEnrollments.Columns.Clear();
            dgvEnrollments.DataSource = dataSource;
            if (dgvEnrollments.Columns.Contains("EnrollmentId")) dgvEnrollments.Columns["EnrollmentId"].Visible = false;
            
            AddNoColumn(dgvEnrollments);
            dgvEnrollments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEnrollments.ClearSelection();
        }
        else
        {
            MessageBox.Show(response.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    private void btnClearEnrollment_Click(object sender, EventArgs e)
    {
        txtSelectedEnrollmentId.Text = string.Empty;
        cmbEnrSubClass.SelectedIndex = -1;
        txtStudentName.Text = string.Empty;
        txtStudentContact.Text = string.Empty;
        txtPaymentInfo.Text = string.Empty;
        txtFatherName.Text = string.Empty;
        dgvEnrollments.ClearSelection();
        ToggleEnrollmentInputs(true);
    }

    private void btnSaveEnrollment_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtSelectedEnrollmentId.Text))
        {
            if (cmbEnrSubClass.SelectedValue == null || !int.TryParse(cmbEnrSubClass.SelectedValue.ToString(), out int subClassId))
            {
                MessageBox.Show("Please select a valid SubClass.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var req = new EnrollmentCreateRequestModel
            {
                SubClassId = subClassId,
                StudentName = txtStudentName.Text,
                StudentContact = txtStudentContact.Text,
                PaymentInfo = txtPaymentInfo.Text,
                FatherName = txtFatherName.Text
            };
            var res = _enrollmentService.CreateEnrollment(req);
            MessageBox.Show(res.Message, "Create Enrollment", MessageBoxButtons.OK, res.IsSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
            if (res.IsSuccess) 
            {
                LoadEnrollments();
                btnClearEnrollment_Click(null, null);
                ToggleEnrollmentInputs(false);
            }
        }
        else
        {
            MessageBox.Show("Editing Enrollments is not currently supported by the EnrollmentService.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void dgvEnrollments_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            var row = dgvEnrollments.Rows[e.RowIndex];

            if (dgvEnrollments.Columns.Contains("EnrollmentId"))
                txtSelectedEnrollmentId.Text = row.Cells["EnrollmentId"].Value?.ToString();
            
            if (dgvEnrollments.Columns.Contains("SubClassId"))
            {
                if (int.TryParse(row.Cells["SubClassId"].Value?.ToString(), out int subClassId))
                    cmbEnrSubClass.SelectedValue = subClassId;
            }
            
            if (dgvEnrollments.Columns.Contains("StudentName"))
                txtStudentName.Text = row.Cells["StudentName"].Value?.ToString();
            
            if (dgvEnrollments.Columns.Contains("StudentContact"))
                txtStudentContact.Text = row.Cells["StudentContact"].Value?.ToString();
            
            if (dgvEnrollments.Columns.Contains("PaymentInfo"))
                txtPaymentInfo.Text = row.Cells["PaymentInfo"].Value?.ToString();

            if (dgvEnrollments.Columns.Contains("FatherName"))
                txtFatherName.Text = row.Cells["FatherName"].Value?.ToString();
            

            ToggleEnrollmentInputs(false);
        }
    }
}

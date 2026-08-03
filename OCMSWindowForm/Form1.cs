using System;
using System.Windows.Forms;
using System.Linq;
using Domain.features.SubClass;
using Domain.features.Enrollment;
using Domain.models;

namespace OCMSWindowForm;

public partial class Form1 : Form
{
    private SubClassService _subClassService;
    private EnrollmentService _enrollmentService;

    public Form1()
    {
        InitializeComponent();
        _subClassService = new SubClassService();
        _enrollmentService = new EnrollmentService();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        LoadSubClasses();
        LoadEnrollments();
        
        // Initial state: clear and disable forms until an action is selected
        btnClearSubClass_Click(null, null);
        ToggleSubClassInputs(false);
        
        btnClearEnrollment_Click(null, null);
        ToggleEnrollmentInputs(false);
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
        txtEnrSubClassId.Enabled = enabled;
        txtStudentName.Enabled = enabled;
        txtStudentContact.Enabled = enabled;
        txtPaymentInfo.Enabled = enabled;
        txtStatus.Enabled = enabled;
        btnSaveEnrollment.Enabled = enabled;
    }

    private void AddNoColumn(DataGridView dgv)
    {
        if (!dgv.Columns.Contains("NoColumn"))
        {
            var noCol = new DataGridViewTextBoxColumn();
            noCol.Name = "NoColumn";
            noCol.HeaderText = "No.";
            noCol.ReadOnly = true;
            noCol.Width = 50;
            dgv.Columns.Insert(0, noCol);
        }
        
        // Ensure No column stays on the left even if DataSource recreates bound columns
        dgv.Columns["NoColumn"].DisplayIndex = 0;
        
        for (int i = 0; i < dgv.Rows.Count; i++)
        {
            dgv.Rows[i].Cells["NoColumn"].Value = (i + 1).ToString();
        }
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
            // WORKAROUND: The domain layer currently fails to map SubClassId. 
            // Since we cannot modify the domain, we retrieve the IDs directly from the database context.
            try
            {
                using var db = new June2026.OCMSDatabase.AppDbContextModels.AppDbContext();
                var dbClasses = db.TblSubClasses.Where(x => !x.IsDelete).ToList();
                dataSource = response.SubClasses.Select(sc => 
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
                        sc.OpenTime,
                        sc.CreatedDateTime,
                        sc.ModifiedDateTime,
                    };
                }).ToList();
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Could not apply ID workaround: " + ex.Message);
            }

            dgvSubClasses.DataSource = dataSource;
            AddActionColumns(dgvSubClasses);
            if (dgvSubClasses.Columns.Contains("SubClassId")) dgvSubClasses.Columns["SubClassId"].Visible = false;
            
            AddNoColumn(dgvSubClasses);
            dgvSubClasses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
            // Create
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
            // Update
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
            
            // Check if Edit button clicked
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
            // Check if Delete button clicked
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
                // If clicked anywhere else, just show data but don't enable editing
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
                using var db = new June2026.OCMSDatabase.AppDbContextModels.AppDbContext();
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
                        en.Status,
                        en.CreatedDateTime,
                        en.ModifiedDateTime,
                    };
                }).ToList();
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Could not apply ID workaround: " + ex.Message);
            }

            dgvEnrollments.DataSource = dataSource;
            if (dgvEnrollments.Columns.Contains("EnrollmentId")) dgvEnrollments.Columns["EnrollmentId"].Visible = false;
            
            AddNoColumn(dgvEnrollments);
            dgvEnrollments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        else
        {
            MessageBox.Show(response.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    private void btnClearEnrollment_Click(object sender, EventArgs e)
    {
        txtSelectedEnrollmentId.Text = string.Empty;
        txtEnrSubClassId.Text = string.Empty;
        txtStudentName.Text = string.Empty;
        txtStudentContact.Text = string.Empty;
        txtPaymentInfo.Text = string.Empty;
        txtStatus.Text = string.Empty;
        dgvEnrollments.ClearSelection();
        ToggleEnrollmentInputs(true);
    }

    private void btnSaveEnrollment_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtSelectedEnrollmentId.Text))
        {
            // Create Enrollment
            if (!int.TryParse(txtEnrSubClassId.Text, out int subClassId))
            {
                MessageBox.Show("Please enter a valid SubClass ID.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var req = new EnrollmentCreateRequestModel
            {
                SubClassId = subClassId,
                StudentName = txtStudentName.Text,
                StudentContact = txtStudentContact.Text,
                PaymentInfo = txtPaymentInfo.Text,
                Status = txtStatus.Text,
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

            // Note: DataGridView for enrollments is strictly readonly now (no Edit/Delete buttons per user request)
            // But if a user clicks a row anyway, we could just display the data for viewing.
            if (dgvEnrollments.Columns.Contains("EnrollmentId"))
                txtSelectedEnrollmentId.Text = row.Cells["EnrollmentId"].Value?.ToString();
            
            if (dgvEnrollments.Columns.Contains("SubClassId"))
                txtEnrSubClassId.Text = row.Cells["SubClassId"].Value?.ToString();
            
            if (dgvEnrollments.Columns.Contains("StudentName"))
                txtStudentName.Text = row.Cells["StudentName"].Value?.ToString();
            
            if (dgvEnrollments.Columns.Contains("StudentContact"))
                txtStudentContact.Text = row.Cells["StudentContact"].Value?.ToString();
            
            if (dgvEnrollments.Columns.Contains("PaymentInfo"))
                txtPaymentInfo.Text = row.Cells["PaymentInfo"].Value?.ToString();
            
            if (dgvEnrollments.Columns.Contains("Status"))
                txtStatus.Text = row.Cells["Status"].Value?.ToString();
                
                

            // Form must be disabled since we cannot edit enrollments
            ToggleEnrollmentInputs(false);
        }
    }
}

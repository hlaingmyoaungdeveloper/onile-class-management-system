namespace OCMSWindowForm;

partial class Main
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.TabControl tabControl;
    private System.Windows.Forms.TabPage tabSubClasses;
    private System.Windows.Forms.TabPage tabEnrollments;
    
    private System.Windows.Forms.SplitContainer splitSubClasses;
    private System.Windows.Forms.DataGridView dgvSubClasses;
    private System.Windows.Forms.GroupBox gbSubClassInput;
    private System.Windows.Forms.Button btnLoadSubClasses;
    private System.Windows.Forms.Button btnClearSubClass;
    
    // Hidden ID tracker
    private System.Windows.Forms.TextBox txtSelectedSubClassId;
    
    private System.Windows.Forms.Label lblClassName;
    private System.Windows.Forms.TextBox txtClassName;
    private System.Windows.Forms.Label lblLocation;
    private System.Windows.Forms.TextBox txtLocation;
    private System.Windows.Forms.Label lblStudentLimit;
    private System.Windows.Forms.NumericUpDown numStudentLimit;
    private System.Windows.Forms.Label lblOpenDate;
    private System.Windows.Forms.DateTimePicker dtpOpenDate;
    private System.Windows.Forms.Label lblOpenTime;
    private System.Windows.Forms.DateTimePicker dtpOpenTime;
    private System.Windows.Forms.Button btnSaveSubClass;

    private System.Windows.Forms.SplitContainer splitEnrollments;
    private System.Windows.Forms.DataGridView dgvEnrollments;
    private System.Windows.Forms.GroupBox gbEnrollmentInput;
    private System.Windows.Forms.Button btnLoadEnrollments;
    private System.Windows.Forms.Button btnClearEnrollment;

    // Hidden ID tracker
    private System.Windows.Forms.TextBox txtSelectedEnrollmentId;
    
    private System.Windows.Forms.Label lblEnrSubClassId;
    private System.Windows.Forms.ComboBox cmbEnrSubClass;
    private System.Windows.Forms.Label lblStudentName;
    private System.Windows.Forms.TextBox txtStudentName;
    private System.Windows.Forms.Label lblStudentContact;
    private System.Windows.Forms.TextBox txtStudentContact;
    private System.Windows.Forms.Label lblPaymentInfo;
    private System.Windows.Forms.TextBox txtPaymentInfo;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.TextBox txtStatus;
    private System.Windows.Forms.Button btnSaveEnrollment;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.tabControl = new System.Windows.Forms.TabControl();
        this.tabSubClasses = new System.Windows.Forms.TabPage();
        this.tabEnrollments = new System.Windows.Forms.TabPage();
        
        this.splitSubClasses = new System.Windows.Forms.SplitContainer();
        this.dgvSubClasses = new System.Windows.Forms.DataGridView();
        this.gbSubClassInput = new System.Windows.Forms.GroupBox();
        
        this.btnLoadSubClasses = new System.Windows.Forms.Button();
        this.btnClearSubClass = new System.Windows.Forms.Button();
        
        this.txtSelectedSubClassId = new System.Windows.Forms.TextBox();
        this.lblClassName = new System.Windows.Forms.Label();
        this.txtClassName = new System.Windows.Forms.TextBox();
        this.lblLocation = new System.Windows.Forms.Label();
        this.txtLocation = new System.Windows.Forms.TextBox();
        this.lblStudentLimit = new System.Windows.Forms.Label();
        this.numStudentLimit = new System.Windows.Forms.NumericUpDown();
        this.lblOpenDate = new System.Windows.Forms.Label();
        this.dtpOpenDate = new System.Windows.Forms.DateTimePicker();
        this.lblOpenTime = new System.Windows.Forms.Label();
        this.dtpOpenTime = new System.Windows.Forms.DateTimePicker();
        this.btnSaveSubClass = new System.Windows.Forms.Button();

        this.splitEnrollments = new System.Windows.Forms.SplitContainer();
        this.dgvEnrollments = new System.Windows.Forms.DataGridView();
        this.gbEnrollmentInput = new System.Windows.Forms.GroupBox();
        
        this.btnLoadEnrollments = new System.Windows.Forms.Button();
        this.btnClearEnrollment = new System.Windows.Forms.Button();
        
        this.txtSelectedEnrollmentId = new System.Windows.Forms.TextBox();
        this.lblEnrSubClassId = new System.Windows.Forms.Label();
        this.cmbEnrSubClass = new System.Windows.Forms.ComboBox();
        this.lblStudentName = new System.Windows.Forms.Label();
        this.txtStudentName = new System.Windows.Forms.TextBox();
        this.lblStudentContact = new System.Windows.Forms.Label();
        this.txtStudentContact = new System.Windows.Forms.TextBox();
        this.lblPaymentInfo = new System.Windows.Forms.Label();
        this.txtPaymentInfo = new System.Windows.Forms.TextBox();
        this.lblStatus = new System.Windows.Forms.Label();
        this.txtStatus = new System.Windows.Forms.TextBox();
        this.btnSaveEnrollment = new System.Windows.Forms.Button();

        this.tabControl.SuspendLayout();
        this.tabSubClasses.SuspendLayout();
        this.tabEnrollments.SuspendLayout();
        
        ((System.ComponentModel.ISupportInitialize)(this.splitSubClasses)).BeginInit();
        this.splitSubClasses.Panel1.SuspendLayout();
        this.splitSubClasses.Panel2.SuspendLayout();
        this.splitSubClasses.SuspendLayout();
        
        ((System.ComponentModel.ISupportInitialize)(this.dgvSubClasses)).BeginInit();
        this.gbSubClassInput.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numStudentLimit)).BeginInit();

        ((System.ComponentModel.ISupportInitialize)(this.splitEnrollments)).BeginInit();
        this.splitEnrollments.Panel1.SuspendLayout();
        this.splitEnrollments.Panel2.SuspendLayout();
        this.splitEnrollments.SuspendLayout();

        ((System.ComponentModel.ISupportInitialize)(this.dgvEnrollments)).BeginInit();
        this.gbEnrollmentInput.SuspendLayout();
        this.SuspendLayout();

        // 
        // tabControl
        // 
        this.tabControl.Controls.Add(this.tabSubClasses);
        this.tabControl.Controls.Add(this.tabEnrollments);
        this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        this.tabControl.Location = new System.Drawing.Point(0, 0);
        this.tabControl.Name = "tabControl";
        this.tabControl.SelectedIndex = 0;
        this.tabControl.Size = new System.Drawing.Size(1200, 800);
        this.tabControl.TabIndex = 0;

        // 
        // tabSubClasses
        // 
        this.tabSubClasses.Controls.Add(this.splitSubClasses);
        this.tabSubClasses.Location = new System.Drawing.Point(4, 32);
        this.tabSubClasses.Name = "tabSubClasses";
        this.tabSubClasses.Padding = new System.Windows.Forms.Padding(10);
        this.tabSubClasses.Size = new System.Drawing.Size(1192, 764);
        this.tabSubClasses.TabIndex = 0;
        this.tabSubClasses.Text = "SubClasses Management";
        this.tabSubClasses.UseVisualStyleBackColor = true;

        // 
        // splitSubClasses
        // 
        this.splitSubClasses.Dock = System.Windows.Forms.DockStyle.Fill;
        this.splitSubClasses.Orientation = System.Windows.Forms.Orientation.Horizontal;
        this.splitSubClasses.Location = new System.Drawing.Point(10, 10);
        this.splitSubClasses.Name = "splitSubClasses";
        // 
        // splitSubClasses.Panel1
        // 
        this.splitSubClasses.Panel1.Controls.Add(this.gbSubClassInput);
        this.splitSubClasses.Panel1.Padding = new System.Windows.Forms.Padding(5);
        // 
        // splitSubClasses.Panel2
        // 
        this.splitSubClasses.Panel2.Controls.Add(this.dgvSubClasses);
        this.splitSubClasses.Panel2.Padding = new System.Windows.Forms.Padding(5);
        this.splitSubClasses.Size = new System.Drawing.Size(1172, 744);
        this.splitSubClasses.SplitterDistance = 290;
        this.splitSubClasses.TabIndex = 0;

        // 
        // gbSubClassInput
        // 
        this.gbSubClassInput.Dock = System.Windows.Forms.DockStyle.Fill;
        this.gbSubClassInput.Location = new System.Drawing.Point(5, 5);
        this.gbSubClassInput.Name = "gbSubClassInput";
        this.gbSubClassInput.Size = new System.Drawing.Size(1162, 280);
        this.gbSubClassInput.TabIndex = 0;
        this.gbSubClassInput.TabStop = false;
        this.gbSubClassInput.Text = "SubClass Details";

        int col1 = 20, col2 = 400, col3 = 780;
        int row1 = 30, row2 = 90, row4 = 210;
        int fieldWidth = 350;

        // Hidden ID
        this.txtSelectedSubClassId.Location = new System.Drawing.Point(0, 0);
        this.txtSelectedSubClassId.Name = "txtSelectedSubClassId";
        this.txtSelectedSubClassId.Visible = false;

        // Row 1
        this.lblClassName.Location = new System.Drawing.Point(col1, row1);
        this.lblClassName.Name = "lblClassName";
        this.lblClassName.Size = new System.Drawing.Size(fieldWidth, 23);
        this.lblClassName.Text = "Class Name";
        this.txtClassName.Location = new System.Drawing.Point(col1, row1 + 25);
        this.txtClassName.Name = "txtClassName";
        this.txtClassName.Size = new System.Drawing.Size(fieldWidth, 30);

        this.lblLocation.Location = new System.Drawing.Point(col2, row1);
        this.lblLocation.Name = "lblLocation";
        this.lblLocation.Size = new System.Drawing.Size(fieldWidth, 23);
        this.lblLocation.Text = "Location";
        this.txtLocation.Location = new System.Drawing.Point(col2, row1 + 25);
        this.txtLocation.Name = "txtLocation";
        this.txtLocation.Size = new System.Drawing.Size(fieldWidth, 30);

        // Row 2
        this.lblStudentLimit.Location = new System.Drawing.Point(col1, row2);
        this.lblStudentLimit.Name = "lblStudentLimit";
        this.lblStudentLimit.Size = new System.Drawing.Size(fieldWidth, 23);
        this.lblStudentLimit.Text = "Student Limit";
        this.numStudentLimit.Location = new System.Drawing.Point(col1, row2 + 25);
        this.numStudentLimit.Name = "numStudentLimit";
        this.numStudentLimit.Size = new System.Drawing.Size(fieldWidth, 30);
        this.numStudentLimit.Maximum = 10000;

        this.lblOpenDate.Location = new System.Drawing.Point(col2, row2);
        this.lblOpenDate.Name = "lblOpenDate";
        this.lblOpenDate.Size = new System.Drawing.Size(fieldWidth, 23);
        this.lblOpenDate.Text = "Open Date";
        this.dtpOpenDate.Location = new System.Drawing.Point(col2, row2 + 25);
        this.dtpOpenDate.Name = "dtpOpenDate";
        this.dtpOpenDate.Size = new System.Drawing.Size(fieldWidth, 30);
        this.dtpOpenDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;

        this.lblOpenTime.Location = new System.Drawing.Point(col3, row2);
        this.lblOpenTime.Name = "lblOpenTime";
        this.lblOpenTime.Size = new System.Drawing.Size(fieldWidth, 23);
        this.lblOpenTime.Text = "Open Time";
        this.dtpOpenTime.Location = new System.Drawing.Point(col3, row2 + 25);
        this.dtpOpenTime.Name = "dtpOpenTime";
        this.dtpOpenTime.Size = new System.Drawing.Size(fieldWidth, 30);
        this.dtpOpenTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
        this.dtpOpenTime.ShowUpDown = true;

        // Row 4
        this.btnSaveSubClass.Location = new System.Drawing.Point(col1, row4);
        this.btnSaveSubClass.Name = "btnSaveSubClass";
        this.btnSaveSubClass.Size = new System.Drawing.Size(150, 45);
        this.btnSaveSubClass.Text = "Save";
        this.btnSaveSubClass.BackColor = System.Drawing.Color.LightGreen;
        this.btnSaveSubClass.Click += new System.EventHandler(this.btnSaveSubClass_Click);
        
        this.btnClearSubClass.Location = new System.Drawing.Point(col1 + 170, row4);
        this.btnClearSubClass.Name = "btnClearSubClass";
        this.btnClearSubClass.Size = new System.Drawing.Size(150, 45);
        this.btnClearSubClass.Text = "Add New";
        this.btnClearSubClass.Click += new System.EventHandler(this.btnClearSubClass_Click);

        this.btnLoadSubClasses.Location = new System.Drawing.Point(col1 + 340, row4);
        this.btnLoadSubClasses.Name = "btnLoadSubClasses";
        this.btnLoadSubClasses.Size = new System.Drawing.Size(150, 45);
        this.btnLoadSubClasses.Text = "Refresh Grid";
        this.btnLoadSubClasses.Click += new System.EventHandler(this.btnLoadSubClasses_Click);

        this.gbSubClassInput.Controls.Add(this.txtSelectedSubClassId);
        this.gbSubClassInput.Controls.Add(this.lblClassName);
        this.gbSubClassInput.Controls.Add(this.txtClassName);
        this.gbSubClassInput.Controls.Add(this.lblLocation);
        this.gbSubClassInput.Controls.Add(this.txtLocation);
        this.gbSubClassInput.Controls.Add(this.lblStudentLimit);
        this.gbSubClassInput.Controls.Add(this.numStudentLimit);
        this.gbSubClassInput.Controls.Add(this.lblOpenDate);
        this.gbSubClassInput.Controls.Add(this.dtpOpenDate);
        this.gbSubClassInput.Controls.Add(this.lblOpenTime);
        this.gbSubClassInput.Controls.Add(this.dtpOpenTime);
        this.gbSubClassInput.Controls.Add(this.btnSaveSubClass);
        this.gbSubClassInput.Controls.Add(this.btnClearSubClass);
        this.gbSubClassInput.Controls.Add(this.btnLoadSubClasses);

        // 
        // dgvSubClasses
        // 
        this.dgvSubClasses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvSubClasses.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dgvSubClasses.Location = new System.Drawing.Point(5, 5);
        this.dgvSubClasses.Name = "dgvSubClasses";
        this.dgvSubClasses.RowHeadersWidth = 51;
        this.dgvSubClasses.Size = new System.Drawing.Size(1162, 444);
        this.dgvSubClasses.TabIndex = 1;
        this.dgvSubClasses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dgvSubClasses.MultiSelect = false;
        this.dgvSubClasses.ReadOnly = true;
        this.dgvSubClasses.AllowUserToAddRows = false;
        this.dgvSubClasses.BackgroundColor = System.Drawing.Color.White;
        this.dgvSubClasses.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSubClasses_CellClick);
        this.dgvSubClasses.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_CellFormatting);

        // 
        // tabEnrollments
        // 
        this.tabEnrollments.Controls.Add(this.splitEnrollments);
        this.tabEnrollments.Location = new System.Drawing.Point(4, 32);
        this.tabEnrollments.Name = "tabEnrollments";
        this.tabEnrollments.Padding = new System.Windows.Forms.Padding(10);
        this.tabEnrollments.Size = new System.Drawing.Size(1192, 764);
        this.tabEnrollments.TabIndex = 1;
        this.tabEnrollments.Text = "Enrollments Management";
        this.tabEnrollments.UseVisualStyleBackColor = true;

        // 
        // splitEnrollments
        // 
        this.splitEnrollments.Dock = System.Windows.Forms.DockStyle.Fill;
        this.splitEnrollments.Orientation = System.Windows.Forms.Orientation.Horizontal;
        this.splitEnrollments.Location = new System.Drawing.Point(10, 10);
        this.splitEnrollments.Name = "splitEnrollments";
        // 
        // splitEnrollments.Panel1
        // 
        this.splitEnrollments.Panel1.Controls.Add(this.gbEnrollmentInput);
        this.splitEnrollments.Panel1.Padding = new System.Windows.Forms.Padding(5);
        // 
        // splitEnrollments.Panel2
        // 
        this.splitEnrollments.Panel2.Controls.Add(this.dgvEnrollments);
        this.splitEnrollments.Panel2.Padding = new System.Windows.Forms.Padding(5);
        this.splitEnrollments.Size = new System.Drawing.Size(1172, 744);
        this.splitEnrollments.SplitterDistance = 290;
        this.splitEnrollments.TabIndex = 0;

        // 
        // gbEnrollmentInput
        // 
        this.gbEnrollmentInput.Dock = System.Windows.Forms.DockStyle.Fill;
        this.gbEnrollmentInput.Location = new System.Drawing.Point(5, 5);
        this.gbEnrollmentInput.Name = "gbEnrollmentInput";
        this.gbEnrollmentInput.Size = new System.Drawing.Size(1162, 280);
        this.gbEnrollmentInput.TabIndex = 0;
        this.gbEnrollmentInput.TabStop = false;
        this.gbEnrollmentInput.Text = "Enrollment Details";

        // Hidden ID
        this.txtSelectedEnrollmentId.Location = new System.Drawing.Point(0, 0);
        this.txtSelectedEnrollmentId.Name = "txtSelectedEnrollmentId";
        this.txtSelectedEnrollmentId.Visible = false;

        // Row 1
        this.lblEnrSubClassId.Location = new System.Drawing.Point(col1, row1);
        this.lblEnrSubClassId.Name = "lblEnrSubClassId";
        this.lblEnrSubClassId.Size = new System.Drawing.Size(fieldWidth, 23);
        this.lblEnrSubClassId.Text = "SubClass ID";
        this.cmbEnrSubClass.Location = new System.Drawing.Point(col1, row1 + 25);
        this.cmbEnrSubClass.Name = "cmbEnrSubClass";
        this.cmbEnrSubClass.Size = new System.Drawing.Size(fieldWidth, 30);
        this.cmbEnrSubClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

        this.lblStudentName.Location = new System.Drawing.Point(col2, row1);
        this.lblStudentName.Name = "lblStudentName";
        this.lblStudentName.Size = new System.Drawing.Size(fieldWidth, 23);
        this.lblStudentName.Text = "Student Name";
        this.txtStudentName.Location = new System.Drawing.Point(col2, row1 + 25);
        this.txtStudentName.Name = "txtStudentName";
        this.txtStudentName.Size = new System.Drawing.Size(fieldWidth, 30);

        this.lblStudentContact.Location = new System.Drawing.Point(col3, row1);
        this.lblStudentContact.Name = "lblStudentContact";
        this.lblStudentContact.Size = new System.Drawing.Size(fieldWidth, 23);
        this.lblStudentContact.Text = "Student Contact";
        this.txtStudentContact.Location = new System.Drawing.Point(col3, row1 + 25);
        this.txtStudentContact.Name = "txtStudentContact";
        this.txtStudentContact.Size = new System.Drawing.Size(fieldWidth, 30);

        // Row 2
        this.lblPaymentInfo.Location = new System.Drawing.Point(col1, row2);
        this.lblPaymentInfo.Name = "lblPaymentInfo";
        this.lblPaymentInfo.Size = new System.Drawing.Size(fieldWidth, 23);
        this.lblPaymentInfo.Text = "Payment Info";
        this.txtPaymentInfo.Location = new System.Drawing.Point(col1, row2 + 25);
        this.txtPaymentInfo.Name = "txtPaymentInfo";
        this.txtPaymentInfo.Size = new System.Drawing.Size(fieldWidth, 30);

        this.lblStatus.Location = new System.Drawing.Point(col2, row2);
        this.lblStatus.Name = "lblStatus";
        this.lblStatus.Size = new System.Drawing.Size(fieldWidth, 23);
        this.lblStatus.Text = "Status";
        this.txtStatus.Location = new System.Drawing.Point(col2, row2 + 25);
        this.txtStatus.Name = "txtStatus";
        this.txtStatus.Size = new System.Drawing.Size(fieldWidth, 30);

        // Row 4
        this.btnSaveEnrollment.Location = new System.Drawing.Point(col1, row4);
        this.btnSaveEnrollment.Name = "btnSaveEnrollment";
        this.btnSaveEnrollment.Size = new System.Drawing.Size(150, 45);
        this.btnSaveEnrollment.Text = "Save";
        this.btnSaveEnrollment.BackColor = System.Drawing.Color.LightGreen;
        this.btnSaveEnrollment.Click += new System.EventHandler(this.btnSaveEnrollment_Click);

        this.btnClearEnrollment.Location = new System.Drawing.Point(col1 + 170, row4);
        this.btnClearEnrollment.Name = "btnClearEnrollment";
        this.btnClearEnrollment.Size = new System.Drawing.Size(150, 45);
        this.btnClearEnrollment.Text = "Add New";
        this.btnClearEnrollment.Click += new System.EventHandler(this.btnClearEnrollment_Click);

        this.btnLoadEnrollments.Location = new System.Drawing.Point(col1 + 340, row4);
        this.btnLoadEnrollments.Name = "btnLoadEnrollments";
        this.btnLoadEnrollments.Size = new System.Drawing.Size(150, 45);
        this.btnLoadEnrollments.Text = "Refresh Grid";
        this.btnLoadEnrollments.Click += new System.EventHandler(this.btnLoadEnrollments_Click);

        this.gbEnrollmentInput.Controls.Add(this.txtSelectedEnrollmentId);
        this.gbEnrollmentInput.Controls.Add(this.lblEnrSubClassId);
        this.gbEnrollmentInput.Controls.Add(this.cmbEnrSubClass);
        this.gbEnrollmentInput.Controls.Add(this.lblStudentName);
        this.gbEnrollmentInput.Controls.Add(this.txtStudentName);
        this.gbEnrollmentInput.Controls.Add(this.lblStudentContact);
        this.gbEnrollmentInput.Controls.Add(this.txtStudentContact);
        this.gbEnrollmentInput.Controls.Add(this.lblPaymentInfo);
        this.gbEnrollmentInput.Controls.Add(this.txtPaymentInfo);
        this.gbEnrollmentInput.Controls.Add(this.lblStatus);
        this.gbEnrollmentInput.Controls.Add(this.txtStatus);
        this.gbEnrollmentInput.Controls.Add(this.btnSaveEnrollment);
        this.gbEnrollmentInput.Controls.Add(this.btnClearEnrollment);
        this.gbEnrollmentInput.Controls.Add(this.btnLoadEnrollments);

        // 
        // dgvEnrollments
        // 
        this.dgvEnrollments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvEnrollments.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dgvEnrollments.Location = new System.Drawing.Point(5, 5);
        this.dgvEnrollments.Name = "dgvEnrollments";
        this.dgvEnrollments.RowHeadersWidth = 51;
        this.dgvEnrollments.Size = new System.Drawing.Size(1162, 444);
        this.dgvEnrollments.TabIndex = 1;
        this.dgvEnrollments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dgvEnrollments.MultiSelect = false;
        this.dgvEnrollments.ReadOnly = true;
        this.dgvEnrollments.AllowUserToAddRows = false;
        this.dgvEnrollments.BackgroundColor = System.Drawing.Color.White;
        this.dgvEnrollments.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEnrollments_CellClick);
        this.dgvEnrollments.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_CellFormatting);

        // 
        // Main
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1200, 800);
        this.Controls.Add(this.tabControl);
        this.Name = "Main";
        this.Text = "OCMS Management System";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Load += new System.EventHandler(this.Main_Load);
        
        this.tabControl.ResumeLayout(false);
        this.tabSubClasses.ResumeLayout(false);
        this.splitSubClasses.Panel1.ResumeLayout(false);
        this.splitSubClasses.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitSubClasses)).EndInit();
        this.splitSubClasses.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.dgvSubClasses)).EndInit();
        this.gbSubClassInput.ResumeLayout(false);
        this.gbSubClassInput.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.numStudentLimit)).EndInit();
        
        this.tabEnrollments.ResumeLayout(false);
        this.splitEnrollments.Panel1.ResumeLayout(false);
        this.splitEnrollments.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitEnrollments)).EndInit();
        this.splitEnrollments.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.dgvEnrollments)).EndInit();
        this.gbEnrollmentInput.ResumeLayout(false);
        this.gbEnrollmentInput.PerformLayout();
        
        this.ResumeLayout(false);
    }
}

namespace PreL
{
    partial class SubjectManagementForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dgvSubjects = new System.Windows.Forms.DataGridView();
            this.gbSubjectDetails = new System.Windows.Forms.GroupBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtInstructorID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodeID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbSearch = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbSearchBy = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSearchTerm = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubjects)).BeginInit();
            this.gbSubjectDetails.SuspendLayout();
            this.gbSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSubjects
            // 
            this.dgvSubjects.AllowUserToAddRows = false;
            this.dgvSubjects.AllowUserToDeleteRows = false;
            this.dgvSubjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSubjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubjects.Location = new System.Drawing.Point(12, 12);
            this.dgvSubjects.MultiSelect = false;
            this.dgvSubjects.Name = "dgvSubjects";
            this.dgvSubjects.ReadOnly = true;
            this.dgvSubjects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSubjects.Size = new System.Drawing.Size(776, 250);
            this.dgvSubjects.TabIndex = 0;
            this.dgvSubjects.SelectionChanged += new System.EventHandler(this.dgvSubjects_SelectionChanged);
            // 
            // gbSubjectDetails
            // 
            this.gbSubjectDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSubjectDetails.Controls.Add(this.txtDescription);
            this.gbSubjectDetails.Controls.Add(this.label4);
            this.gbSubjectDetails.Controls.Add(this.txtInstructorID);
            this.gbSubjectDetails.Controls.Add(this.label3);
            this.gbSubjectDetails.Controls.Add(this.txtName);
            this.gbSubjectDetails.Controls.Add(this.label2);
            this.gbSubjectDetails.Controls.Add(this.txtCodeID);
            this.gbSubjectDetails.Controls.Add(this.label1);
            this.gbSubjectDetails.Enabled = false;
            this.gbSubjectDetails.Location = new System.Drawing.Point(12, 268);
            this.gbSubjectDetails.Name = "gbSubjectDetails";
            this.gbSubjectDetails.Size = new System.Drawing.Size(776, 150);
            this.gbSubjectDetails.TabIndex = 1;
            this.gbSubjectDetails.TabStop = false;
            this.gbSubjectDetails.Text = "Subject Details";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(90, 97);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(680, 47);
            this.txtDescription.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Description:";
            // 
            // txtInstructorID
            // 
            this.txtInstructorID.Location = new System.Drawing.Point(90, 71);
            this.txtInstructorID.Name = "txtInstructorID";
            this.txtInstructorID.Size = new System.Drawing.Size(100, 20);
            this.txtInstructorID.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Instructor ID:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(90, 45);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(680, 20);
            this.txtName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Name:";
            // 
            // txtCodeID
            // 
            this.txtCodeID.Location = new System.Drawing.Point(90, 19);
            this.txtCodeID.Name = "txtCodeID";
            this.txtCodeID.Size = new System.Drawing.Size(100, 20);
            this.txtCodeID.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Code:";
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Location = new System.Drawing.Point(632, 424);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(551, 424);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(470, 424);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(632, 424);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(713, 424);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gbSearch
            // 
            this.gbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.gbSearch.Controls.Add(this.btnSearch);
            this.gbSearch.Controls.Add(this.cmbSearchBy);
            this.gbSearch.Controls.Add(this.label6);
            this.gbSearch.Controls.Add(this.txtSearchTerm);
            this.gbSearch.Controls.Add(this.label5);
            this.gbSearch.Location = new System.Drawing.Point(12, 424);
            this.gbSearch.Name = "gbSearch";
            this.gbSearch.Size = new System.Drawing.Size(452, 52);
            this.gbSearch.TabIndex = 7;
            this.gbSearch.TabStop = false;
            this.gbSearch.Text = "Search";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(371, 19);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbSearchBy
            // 
            this.cmbSearchBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchBy.FormattingEnabled = true;
            this.cmbSearchBy.Items.AddRange(new object[] {
            "Code",
            "Name",
            "InstructorID"});
            this.cmbSearchBy.Location = new System.Drawing.Point(248, 20);
            this.cmbSearchBy.Name = "cmbSearchBy";
            this.cmbSearchBy.Size = new System.Drawing.Size(117, 21);
            this.cmbSearchBy.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(186, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Search by:";
            // 
            // txtSearchTerm
            // 
            this.txtSearchTerm.Location = new System.Drawing.Point(80, 21);
            this.txtSearchTerm.Name = "txtSearchTerm";
            this.txtSearchTerm.Size = new System.Drawing.Size(100, 20);
            this.txtSearchTerm.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Search term:";
            // 
            // SubjectManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 488);
            this.Controls.Add(this.gbSearch);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.gbSubjectDetails);
            this.Controls.Add(this.dgvSubjects);
            this.Name = "SubjectManagementForm";
            this.Text = "Subject Management";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubjects)).EndInit();
            this.gbSubjectDetails.ResumeLayout(false);
            this.gbSubjectDetails.PerformLayout();
            this.gbSearch.ResumeLayout(false);
            this.gbSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSubjects;
        private System.Windows.Forms.GroupBox gbSubjectDetails;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtInstructorID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCodeID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gbSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cmbSearchBy;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSearchTerm;
        private System.Windows.Forms.Label label5;
    }
}
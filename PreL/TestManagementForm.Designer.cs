namespace PreL
{
    partial class TestManagementForm
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

        private void InitializeComponent()
        {
            this.dgvTests = new System.Windows.Forms.DataGridView();
            this.grpTestDetails = new System.Windows.Forms.GroupBox();
            this.btnRemoveQuestion = new System.Windows.Forms.Button();
            this.btnAddQuestion = new System.Windows.Forms.Button();
            this.lstQuestionIDs = new System.Windows.Forms.ListBox();
            this.txtQuestionID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTestBankID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkPublished = new System.Windows.Forms.CheckBox();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.cboPublishedStatus = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTests)).BeginInit();
            this.grpTestDetails.SuspendLayout();
            this.SuspendLayout();

            // dgvTests
            this.dgvTests.AllowUserToAddRows = false;
            this.dgvTests.AllowUserToDeleteRows = false;
            this.dgvTests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTests.Location = new System.Drawing.Point(12, 41);
            this.dgvTests.Name = "dgvTests";
            this.dgvTests.ReadOnly = true;
            this.dgvTests.Size = new System.Drawing.Size(644, 300);
            this.dgvTests.TabIndex = 0;
            this.dgvTests.SelectionChanged += new System.EventHandler(this.dgvTests_SelectionChanged);

            // grpTestDetails
            this.grpTestDetails.Controls.Add(this.btnRemoveQuestion);
            this.grpTestDetails.Controls.Add(this.btnAddQuestion);
            this.grpTestDetails.Controls.Add(this.lstQuestionIDs);
            this.grpTestDetails.Controls.Add(this.txtQuestionID);
            this.grpTestDetails.Controls.Add(this.label6);
            this.grpTestDetails.Controls.Add(this.txtTestBankID);
            this.grpTestDetails.Controls.Add(this.label5);
            this.grpTestDetails.Controls.Add(this.chkPublished);
            this.grpTestDetails.Controls.Add(this.txtDuration);
            this.grpTestDetails.Controls.Add(this.label4);
            this.grpTestDetails.Controls.Add(this.txtTitle);
            this.grpTestDetails.Controls.Add(this.label3);
            this.grpTestDetails.Controls.Add(this.txtID);
            this.grpTestDetails.Controls.Add(this.label2);
            this.grpTestDetails.Location = new System.Drawing.Point(12, 347);
            this.grpTestDetails.Name = "grpTestDetails";
            this.grpTestDetails.Size = new System.Drawing.Size(644, 200);
            this.grpTestDetails.TabIndex = 1;
            this.grpTestDetails.TabStop = false;
            this.grpTestDetails.Text = "Test Details";

            // btnRemoveQuestion
            this.btnRemoveQuestion.Location = new System.Drawing.Point(553, 120);
            this.btnRemoveQuestion.Name = "btnRemoveQuestion";
            this.btnRemoveQuestion.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveQuestion.TabIndex = 13;
            this.btnRemoveQuestion.Text = "Remove";
            this.btnRemoveQuestion.UseVisualStyleBackColor = true;
            this.btnRemoveQuestion.Click += new System.EventHandler(this.btnRemoveQuestion_Click);

            // btnAddQuestion
            this.btnAddQuestion.Location = new System.Drawing.Point(553, 80);
            this.btnAddQuestion.Name = "btnAddQuestion";
            this.btnAddQuestion.Size = new System.Drawing.Size(75, 23);
            this.btnAddQuestion.TabIndex = 12;
            this.btnAddQuestion.Text = "Add";
            this.btnAddQuestion.UseVisualStyleBackColor = true;
            this.btnAddQuestion.Click += new System.EventHandler(this.btnAddQuestion_Click);

            // lstQuestionIDs
            this.lstQuestionIDs.FormattingEnabled = true;
            this.lstQuestionIDs.Location = new System.Drawing.Point(400, 50);
            this.lstQuestionIDs.Name = "lstQuestionIDs";
            this.lstQuestionIDs.Size = new System.Drawing.Size(120, 95);
            this.lstQuestionIDs.TabIndex = 11;

            // txtQuestionID
            this.txtQuestionID.Location = new System.Drawing.Point(400, 20);
            this.txtQuestionID.Name = "txtQuestionID";
            this.txtQuestionID.Size = new System.Drawing.Size(120, 20);
            this.txtQuestionID.TabIndex = 10;

            // label6
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(320, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Question ID:";

            // txtTestBankID
            this.txtTestBankID.Location = new System.Drawing.Point(100, 120);
            this.txtTestBankID.Name = "txtTestBankID";
            this.txtTestBankID.Size = new System.Drawing.Size(200, 20);
            this.txtTestBankID.TabIndex = 8;

            // label5
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Test Bank ID:";

            // chkPublished
            this.chkPublished.AutoSize = true;
            this.chkPublished.Location = new System.Drawing.Point(100, 150);
            this.chkPublished.Name = "chkPublished";
            this.chkPublished.Size = new System.Drawing.Size(73, 17);
            this.chkPublished.TabIndex = 6;
            this.chkPublished.Text = "Published";
            this.chkPublished.UseVisualStyleBackColor = true;

            // txtDuration
            this.txtDuration.Location = new System.Drawing.Point(100, 90);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(200, 20);
            this.txtDuration.TabIndex = 5;

            // label4
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Duration:";

            // txtTitle
            this.txtTitle.Location = new System.Drawing.Point(100, 60);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(200, 20);
            this.txtTitle.TabIndex = 3;

            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Title:";

            // txtID
            this.txtID.Location = new System.Drawing.Point(100, 30);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(200, 20);
            this.txtID.TabIndex = 1;

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "ID:";

            // label1
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Test Management";

            // btnNew
            this.btnNew.Location = new System.Drawing.Point(662, 347);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 3;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(662, 377);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(662, 407);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // cboPublishedStatus
            this.cboPublishedStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPublishedStatus.FormattingEnabled = true;
            this.cboPublishedStatus.Location = new System.Drawing.Point(500, 12);
            this.cboPublishedStatus.Name = "cboPublishedStatus";
            this.cboPublishedStatus.Size = new System.Drawing.Size(120, 21);
            this.cboPublishedStatus.TabIndex = 6;
            this.cboPublishedStatus.SelectedIndexChanged += new System.EventHandler(this.cboPublishedStatus_SelectedIndexChanged);

            // label7
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(400, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Filter by Published:";

            // btnRefresh
            this.btnRefresh.Location = new System.Drawing.Point(626, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(165, 14);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 9;

            // TestManagementForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 559);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboPublishedStatus);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpTestDetails);
            this.Controls.Add(this.dgvTests);
            this.Name = "TestManagementForm";
            this.Text = "Test Management System";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTests)).EndInit();
            this.grpTestDetails.ResumeLayout(false);
            this.grpTestDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.DataGridView dgvTests;
        private System.Windows.Forms.GroupBox grpTestDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkPublished;
        private System.Windows.Forms.TextBox txtTestBankID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtQuestionID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lstQuestionIDs;
        private System.Windows.Forms.Button btnAddQuestion;
        private System.Windows.Forms.Button btnRemoveQuestion;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ComboBox cboPublishedStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblStatus;
    }
}
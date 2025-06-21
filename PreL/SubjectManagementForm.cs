using BLL.SubjectHandling.Managers;
using DAL.Entity.SubjectHandling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PreL
{
    public partial class SubjectManagementForm : Form
    {
        private Subject _currentSubject;
        private bool _isEditMode = false;

        public SubjectManagementForm()
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadSubjects();
            SetFormState(true); //may be false
        }

        private void InitializeDataGridView()
        {
            dgvSubjects.AutoGenerateColumns = false;
            dgvSubjects.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Add columns
            dgvSubjects.Columns.Add("ID", "ID");
            dgvSubjects.Columns.Add("CodeID", "Code");
            dgvSubjects.Columns.Add("Name", "Name");
            dgvSubjects.Columns.Add("InstructorID", "Instructor ID");
            dgvSubjects.Columns.Add("Description", "Description");
            dgvSubjects.Columns.Add("CreatedAt", "Created At");
            dgvSubjects.Columns.Add("UpdatedAt", "Updated At");

            // Set data properties
            dgvSubjects.Columns["ID"].DataPropertyName = "ID";
            dgvSubjects.Columns["CodeID"].DataPropertyName = "CodeID";
            dgvSubjects.Columns["Name"].DataPropertyName = "Name";
            dgvSubjects.Columns["InstructorID"].DataPropertyName = "InstructorID";
            dgvSubjects.Columns["Description"].DataPropertyName = "Description";
            dgvSubjects.Columns["CreatedAt"].DataPropertyName = "CreatedAt";
            dgvSubjects.Columns["UpdatedAt"].DataPropertyName = "UpdatedAt";
        }

        private void LoadSubjects()
        {
            try
            {
                var subjects = SubjectManager.GetAllSubjects();
                dgvSubjects.DataSource = new BindingList<Subject>(new List<Subject>(subjects));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading subjects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            _isEditMode = false;
            _currentSubject = null;
            ClearForm();
            SetFormState(true);
            txtCodeID.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSubjects.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a subject to edit.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _isEditMode = true;
            _currentSubject = (Subject)dgvSubjects.SelectedRows[0].DataBoundItem;
            LoadSubjectDetails(_currentSubject);
            SetFormState(true);
            txtCodeID.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSubjects.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a subject to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var subjectToDelete = (Subject)dgvSubjects.SelectedRows[0].DataBoundItem;

            if (MessageBox.Show($"Are you sure you want to delete subject '{subjectToDelete.Name}'?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    SubjectManager.DeleteSubject(subjectToDelete.ID);
                    LoadSubjects();
                    MessageBox.Show("Subject deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting subject: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            try
            {
                var questionsBankIDs = new List<int>(); // You can implement this part as needed

                var subject = SubjectManager.CreateSubject(
                    _isEditMode ? _currentSubject.ID : 0,
                    int.Parse(txtInstructorID.Text),
                    txtCodeID.Text,
                    txtName.Text,
                    txtDescription.Text,
                    questionsBankIDs);

                if (_isEditMode)
                {
                    SubjectManager.UpdateSubject(subject);
                    MessageBox.Show("Subject updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SubjectManager.AddSubject(subject);
                    MessageBox.Show("Subject added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadSubjects();
                SetFormState(false);
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving subject: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetFormState(false);
            ClearForm();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var searchTerm = txtSearchTerm.Text.Trim();
            var searchBy = cmbSearchBy.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(searchTerm) || string.IsNullOrEmpty(searchBy))
            {
                LoadSubjects();
                return;
            }

            try
            {
                var results = SubjectManager.SearchSubjects(searchTerm, searchBy);
                dgvSubjects.DataSource = new BindingList<Subject>(new List<Subject>(results));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching subjects: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSubjectDetails(Subject subject)
        {
            txtCodeID.Text = subject.CodeID;
            txtName.Text = subject.Name;
            txtInstructorID.Text = subject.InstructorID.ToString();
            txtDescription.Text = subject.Description;
        }

        private void ClearForm()
        {
            txtCodeID.Clear();
            txtName.Clear();
            txtInstructorID.Clear();
            txtDescription.Clear();
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtCodeID.Text))
            {
                MessageBox.Show("Please enter a subject code.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodeID.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a subject name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (!int.TryParse(txtInstructorID.Text, out _))
            {
                MessageBox.Show("Please enter a valid instructor ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtInstructorID.Focus();
                return false;
            }

            return true;
        }

        private void SetFormState(bool editMode)
        {
            gbSubjectDetails.Enabled = editMode;
            btnSave.Enabled = editMode;
            btnCancel.Enabled = editMode;
            btnNew.Enabled = !editMode;
            btnEdit.Enabled = !editMode && dgvSubjects.SelectedRows.Count > 0;
            btnDelete.Enabled = !editMode && dgvSubjects.SelectedRows.Count > 0;
        }

        private void dgvSubjects_SelectionChanged(object sender, EventArgs e)
        {
            btnEdit.Enabled = dgvSubjects.SelectedRows.Count > 0 && !gbSubjectDetails.Enabled;
            btnDelete.Enabled = dgvSubjects.SelectedRows.Count > 0 && !gbSubjectDetails.Enabled;
        }
    }
}
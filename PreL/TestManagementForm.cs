using BLL.SubjectHandling.Managers;
using DAL.Entity.SubjectHandling;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace PreL
{
    public partial class TestManagementForm : Form
    {
        private Test _currentTest;
        private List<int> _currentQuestionIDs = new List<int>();

        public TestManagementForm()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Set up DataGridView
            dgvTests.AutoGenerateColumns = false;
            dgvTests.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Add columns
            dgvTests.Columns.Add("ID", "ID");
            dgvTests.Columns.Add("Title", "Title");
            dgvTests.Columns.Add("Duration", "Duration");
            dgvTests.Columns.Add("IsPublished", "Published");
            dgvTests.Columns.Add("TestBankID", "Test Bank ID");
            dgvTests.Columns.Add("QuestionsCount", "Questions Count");

            // Set up combo boxes
            cboPublishedStatus.Items.AddRange(new object[] { "All", "Published Only", "Unpublished Only" });
            cboPublishedStatus.SelectedIndex = 0;

            // Load initial data
            RefreshTestsList();
            ClearForm();
        }

        private void RefreshTestsList()
        {
            try
            {
                dgvTests.Rows.Clear();

                IEnumerable<Test> tests;
                switch (cboPublishedStatus.SelectedIndex)
                {
                    case 1:
                        tests = TestManager.GetPublishedTests();
                        break;
                    case 2:
                        tests = TestManager.GetAllTests().Where(t => !t.IsPublished);
                        break;
                    default:
                        tests = TestManager.GetAllTests();
                        break;
                }

                foreach (var test in tests)
                {
                    dgvTests.Rows.Add(
                        test.ID,
                        test.Title,
                        test.Duration,
                        test.IsPublished ? "Yes" : "No",
                        test.TestBankID,
                        test.QuestionsIDs.Count
                    );
                }

                lblStatus.Text = $"Loaded {dgvTests.Rows.Count} tests";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tests: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            _currentTest = null;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearForm();
            txtID.Text = "0";
            txtTitle.Clear();
            txtDuration.Clear();
            chkPublished.Checked = false;
            txtTestBankID.Clear();
            lstQuestionIDs.Items.Clear();
            _currentQuestionIDs.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                var test = TestManager.CreateTest(
                    int.Parse(txtID.Text),
                    int.Parse(txtTestBankID.Text),
                    txtTitle.Text,
                    txtDuration.Text,
                    chkPublished.Checked,
                    _currentQuestionIDs
                );

                if (test.ID == 0)
                {
                    TestManager.AddTest(test);
                    MessageBox.Show("Test added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    TestManager.UpdateTest(test);
                    MessageBox.Show("Test updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                RefreshTestsList();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving test: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Title is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitle.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDuration.Text))
            {
                MessageBox.Show("Duration is required", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDuration.Focus();
                return false;
            }

            if (!int.TryParse(txtTestBankID.Text, out _))
            {
                MessageBox.Show("Test Bank ID must be a valid number", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTestBankID.Focus();
                return false;
            }

            return true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_currentTest == null || _currentTest.ID == 0)
            {
                MessageBox.Show("No test selected to delete", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this test?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    TestManager.DeleteTest(_currentTest.ID);
                    RefreshTestsList();
                    ClearForm();
                    MessageBox.Show("Test deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting test: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvTests_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTests.SelectedRows.Count == 0) return;

            var selectedRow = dgvTests.SelectedRows[0];
            int testId = Convert.ToInt32(selectedRow.Cells["ID"].Value);

            try
            {
                _currentTest = TestManager.GetTestById(testId);
                if (_currentTest != null)
                {
                    txtID.Text = _currentTest.ID.ToString();
                    txtTitle.Text = _currentTest.Title;
                    txtDuration.Text = _currentTest.Duration;
                    chkPublished.Checked = _currentTest.IsPublished;
                    txtTestBankID.Text = _currentTest.TestBankID.ToString();

                    _currentQuestionIDs = _currentTest.QuestionsIDs;
                    lstQuestionIDs.Items.Clear();
                    foreach (var id in _currentQuestionIDs)
                    {
                        lstQuestionIDs.Items.Add(id);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading test details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddQuestion_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtQuestionID.Text, out int questionId))
            {
                if (!_currentQuestionIDs.Contains(questionId))
                {
                    _currentQuestionIDs.Add(questionId);
                    lstQuestionIDs.Items.Add(questionId);
                    txtQuestionID.Clear();
                }
                else
                {
                    MessageBox.Show("This question ID is already in the list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid question ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveQuestion_Click(object sender, EventArgs e)
        {
            if (lstQuestionIDs.SelectedIndex >= 0)
            {
                int selectedId = (int)lstQuestionIDs.SelectedItem;
                _currentQuestionIDs.Remove(selectedId);
                lstQuestionIDs.Items.RemoveAt(lstQuestionIDs.SelectedIndex);
            }
        }

        private void cboPublishedStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshTestsList();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshTestsList();
        }
    }
}
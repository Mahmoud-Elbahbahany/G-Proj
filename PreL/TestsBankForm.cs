using BLL.SubjectHandling.Managers;
using DAL.Entity.SubjectHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Prel
{
    public partial class TestsBankForm : Form
    {
        // Initialize nullable fields to resolve non-nullable field errors
        private Question? _currentQuestion = null;
        private Test? _currentTest = null;

        public TestsBankForm()
        {
            InitializeComponent();
            InitializeDataGridView();
            InitializeComboBoxes();
            LoadTestsBanks();
        }

        private void InitializeDataGridView()
        {
            // Configure DataGridView properties
            dgvTestsBanks.AutoGenerateColumns = false;
            dgvTestsBanks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTestsBanks.MultiSelect = false;

            // Clear existing columns
            dgvTestsBanks.Columns.Clear();

            // Add columns manually with proper null handling
            dgvTestsBanks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ID",
                DataPropertyName = "ID",
                HeaderText = "ID"
            });
            dgvTestsBanks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "InstructorID",
                DataPropertyName = "InstructorID",
                HeaderText = "Instructor ID"
            });
            dgvTestsBanks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Title",
                DataPropertyName = "Title",
                HeaderText = "Title"
            });
            dgvTestsBanks.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Description",
                DataPropertyName = "Description",
                HeaderText = "Description"
            });
            dgvTestsBanks.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "IsActive",
                DataPropertyName = "IsActive",
                HeaderText = "Active"
            });
        }

        private void InitializeComboBoxes()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new object[] { "Active", "Inactive" });
            cmbStatus.SelectedIndex = 0;
        }

        private void LoadTestsBanks()
        {
            try
            {
                var testsBanks = TestsBankManager.GetAllTestsBanks()?.ToList() ?? new List<TestsBank>();
                dgvTestsBanks.DataSource = new BindingSource(testsBanks, null);
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tests banks: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvTestsBanks?.Columns == null || dgvTestsBanks.Columns.Count == 0)
                return;

            foreach (DataGridViewColumn column in dgvTestsBanks.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                var testsBank = new TestsBank(
                    _id: 0,
                    _instructorid: int.Parse(txtInstructorID.Text),
                    _title: txtTitle.Text.Trim(),
                    _description: txtDescription.Text.Trim(),
                    _isactive: cmbStatus.SelectedIndex == 0,
                    _testsids: new List<int>()
                );

                TestsBankManager.AddTestsBank(testsBank);
                MessageBox.Show("Tests bank added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTestsBanks();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding tests bank: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtTitle?.Text))
            {
                MessageBox.Show("Title is required", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtInstructorID?.Text, out _))
            {
                MessageBox.Show("Invalid Instructor ID", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvTestsBanks?.SelectedRows == null || dgvTestsBanks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a tests bank to update.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!ValidateInputs()) return;

            try
            {
                var selectedTestsBank = dgvTestsBanks.SelectedRows[0].DataBoundItem as TestsBank;
                if (selectedTestsBank == null) return;

                selectedTestsBank.Title = txtTitle.Text.Trim();
                selectedTestsBank.Description = txtDescription.Text.Trim();
                selectedTestsBank.IsActive = cmbStatus.SelectedIndex == 0;
                selectedTestsBank.InstructorID = int.Parse(txtInstructorID.Text);

                TestsBankManager.UpdateTestsBank(selectedTestsBank);
                MessageBox.Show("Tests bank updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTestsBanks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating tests bank: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTestsBanks?.SelectedRows == null || dgvTestsBanks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a tests bank to delete.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var selectedTestsBank = dgvTestsBanks.SelectedRows[0].DataBoundItem as TestsBank;
                if (selectedTestsBank == null) return;

                if (MessageBox.Show("Are you sure you want to delete this tests bank?", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    TestsBankManager.DeleteTestsBank(selectedTestsBank.ID);
                    MessageBox.Show("Tests bank deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTestsBanks();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting tests bank: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTestsBanks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTestsBanks?.SelectedRows == null || dgvTestsBanks.SelectedRows.Count == 0)
                return;

            var selectedTestsBank = dgvTestsBanks.SelectedRows[0].DataBoundItem as TestsBank;
            if (selectedTestsBank == null) return;

            txtTitle.Text = selectedTestsBank.Title ?? string.Empty;
            txtDescription.Text = selectedTestsBank.Description ?? string.Empty;
            cmbStatus.SelectedIndex = selectedTestsBank.IsActive ? 0 : 1;
            txtInstructorID.Text = selectedTestsBank.InstructorID.ToString();
        }

        private void ClearForm()
        {
            txtTitle.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtInstructorID.Text = string.Empty;
            cmbStatus.SelectedIndex = 0;
            dgvTestsBanks?.ClearSelection();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearch?.Text))
                {
                    LoadTestsBanks();
                    return;
                }

                var searchText = txtSearch.Text.ToLower();
                var allTestsBanks = TestsBankManager.GetAllTestsBanks() ?? Enumerable.Empty<TestsBank>();

                var filtered = allTestsBanks
                    .Where(tb =>
                        (tb.Title?.ToLower().Contains(searchText) ?? false) ||
                        (tb.Description?.ToLower().Contains(searchText) ?? false) ||
                        tb.InstructorID.ToString().Contains(searchText))
                    .ToList();

                dgvTestsBanks.DataSource = new BindingSource(filtered, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching tests banks: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Add the missing btnClear_Click handler
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}
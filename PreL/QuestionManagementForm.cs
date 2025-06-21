using BLL.SubjectHandling.Managers;
using DAL.Entity.SubjectHandling;
using DAL.Enum.SubjectHandling_Mod;

namespace PreL
{
    public partial class QuestionManagementForm : Form
    {
        private Question _currentQuestion;
        private List<string> _currentAnswers = new List<string>();

        public QuestionManagementForm()
        {
            InitializeComponent();
            InitializeControls();
            LoadQuestions();
        }

        private void InitializeControls()
        {
            // Initialize combo boxes
            cmbQuestionType.DataSource = Enum.GetValues(typeof(QuestionType));
            cmbDifficultyLevel.DataSource = Enum.GetValues(typeof(QuestionDifficultyLevel));

            // Set up DataGridView
            dgvQuestions.AutoGenerateColumns = false;
            dgvQuestions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvQuestions.ReadOnly = true;
        }

        private void LoadQuestions()
        {
            try
            {
                dgvQuestions.DataSource = null;
                dgvQuestions.DataSource = QuestionManager.GetAllQuestions();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading questions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAddAnswer_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtAnswer.Text))
            {
                _currentAnswers.Add(txtAnswer.Text);
                lstAnswers.Items.Add(txtAnswer.Text);
                txtAnswer.Clear();
                txtAnswer.Focus();
            }
        }

        private void BtnRemoveAnswer_Click(object sender, EventArgs e)
        {
            if (lstAnswers.SelectedIndex >= 0)
            {
                _currentAnswers.RemoveAt(lstAnswers.SelectedIndex);
                lstAnswers.Items.RemoveAt(lstAnswers.SelectedIndex);
            }
        }

        private void BtnNewQuestion_Click(object sender, EventArgs e)
        {
            ClearForm();
            _currentQuestion = null;
        }

        private void BtnSaveQuestion_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                try
                {
                    var question = QuestionManager.CreateQuestion(
                        _currentQuestion?.ID ?? 0,
                        int.Parse(txtSubjectId.Text),
                        (QuestionType)cmbQuestionType.SelectedItem,
                        (QuestionDifficultyLevel)cmbDifficultyLevel.SelectedItem,
                        txtQuestionText.Text,
                        _currentAnswers,
                        txtCorrectAnswer.Text
                    );

                    if (_currentQuestion == null)
                    {
                        QuestionManager.AddQuestion(question);
                    }
                    else
                    {
                        QuestionManager.UpdateQuestion(question);
                    }

                    LoadQuestions();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving question: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnDeleteQuestion_Click(object sender, EventArgs e)
        {
            if (_currentQuestion != null)
            {
                try
                {
                    QuestionManager.DeleteQuestion(_currentQuestion.ID);
                    LoadQuestions();
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting question: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DgvQuestions_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvQuestions.SelectedRows.Count > 0)
            {
                _currentQuestion = (Question)dgvQuestions.SelectedRows[0].DataBoundItem;
                DisplayQuestion(_currentQuestion);
            }
        }

        private void DisplayQuestion(Question question)
        {
            txtSubjectId.Text = question.SubjectID.ToString();
            cmbQuestionType.SelectedItem = question.Type;
            cmbDifficultyLevel.SelectedItem = question.Difficulty_lvl;
            txtQuestionText.Text = question.Text;
            txtCorrectAnswer.Text = question.CorrectAnswer;

            _currentAnswers = new List<string>(question.Answers);
            lstAnswers.Items.Clear();
            lstAnswers.Items.AddRange(_currentAnswers.ToArray());
        }

        private void ClearForm()
        {
            txtSubjectId.Clear();
            cmbQuestionType.SelectedIndex = 0;
            cmbDifficultyLevel.SelectedIndex = 0;
            txtQuestionText.Clear();
            txtCorrectAnswer.Clear();
            _currentAnswers.Clear();
            lstAnswers.Items.Clear();
            txtAnswer.Clear();
            _currentQuestion = null;
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtSubjectId.Text) || !int.TryParse(txtSubjectId.Text, out _))
            {
                MessageBox.Show("Please enter a valid Subject ID", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtQuestionText.Text))
            {
                MessageBox.Show("Please enter question text", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (_currentAnswers.Count == 0)
            {
                MessageBox.Show("Please add at least one answer", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCorrectAnswer.Text))
            {
                MessageBox.Show("Please specify the correct answer", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtSearchSubjectId.Text, out int subjectId))
            {
                try
                {
                    dgvQuestions.DataSource = QuestionManager.GetQuestionsBySubject(subjectId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error searching questions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid Subject ID for search", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchSubjectId.Clear();
            LoadQuestions();
        }
    }
}
namespace PreL
{
    partial class QuestionManagementForm
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
            // Main controls
            dgvQuestions = new DataGridView();
            txtSearchSubjectId = new TextBox();
            btnSearch = new Button();
            btnClearSearch = new Button();

            // Question details controls
            txtSubjectId = new TextBox();
            cmbQuestionType = new ComboBox();
            cmbDifficultyLevel = new ComboBox();
            txtQuestionText = new TextBox();
            txtCorrectAnswer = new TextBox();
            txtAnswer = new TextBox();
            lstAnswers = new ListBox();

            // Buttons
            btnAddAnswer = new Button();
            btnRemoveAnswer = new Button();
            btnNewQuestion = new Button();
            btnSaveQuestion = new Button();
            btnDeleteQuestion = new Button();

            // Labels
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();

            // DataGridView setup
            ((System.ComponentModel.ISupportInitialize)dgvQuestions).BeginInit();
            SuspendLayout();

            // 
            // dgvQuestions
            // 
            dgvQuestions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvQuestions.Location = new Point(12, 50);
            dgvQuestions.Name = "dgvQuestions";
            dgvQuestions.RowHeadersWidth = 51;
            dgvQuestions.RowTemplate.Height = 29;
            dgvQuestions.Size = new Size(600, 300);
            dgvQuestions.TabIndex = 0;
            dgvQuestions.SelectionChanged += DgvQuestions_SelectionChanged;

            // 
            // txtSearchSubjectId
            // 
            txtSearchSubjectId.Location = new Point(12, 15);
            txtSearchSubjectId.Name = "txtSearchSubjectId";
            txtSearchSubjectId.Size = new Size(150, 27);
            txtSearchSubjectId.TabIndex = 1;

            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(168, 15);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(94, 29);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Search";
            btnSearch.Click += BtnSearch_Click;

            // 
            // btnClearSearch
            // 
            btnClearSearch.Location = new Point(268, 15);
            btnClearSearch.Name = "btnClearSearch";
            btnClearSearch.Size = new Size(94, 29);
            btnClearSearch.TabIndex = 3;
            btnClearSearch.Text = "Clear";
            btnClearSearch.Click += BtnClearSearch_Click;

            // Other controls initialization would follow similar pattern
            // with proper positioning and styling

            // 
            // QuestionManagementForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 700);
            Controls.Add(dgvQuestions);
            Controls.Add(txtSearchSubjectId);
            Controls.Add(btnSearch);
            Controls.Add(btnClearSearch);
            // Add other controls here
            Name = "QuestionManagementForm";
            Text = "Question Management";
            ((System.ComponentModel.ISupportInitialize)dgvQuestions).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private DataGridView dgvQuestions;
        private TextBox txtSearchSubjectId;
        private Button btnSearch;
        private Button btnClearSearch;
        private TextBox txtSubjectId;
        private ComboBox cmbQuestionType;
        private ComboBox cmbDifficultyLevel;
        private TextBox txtQuestionText;
        private TextBox txtCorrectAnswer;
        private TextBox txtAnswer;
        private ListBox lstAnswers;
        private Button btnAddAnswer;
        private Button btnRemoveAnswer;
        private Button btnNewQuestion;
        private Button btnSaveQuestion;
        private Button btnDeleteQuestion;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
    }
}
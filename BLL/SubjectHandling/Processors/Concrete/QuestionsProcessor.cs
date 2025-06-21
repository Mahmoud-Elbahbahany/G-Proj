using BLL.SubjectHandling.Processors.Interface;
using DAL.Entity.SubjectHandling;
using DAL.Enum.SubjectHandling_Mod;
using Newtonsoft.Json;

namespace BLL.SubjectHandling.Processors.Concrete
{
    public class QuestionsProcessor : IQuestionProcessor
    {
        #region Fields: +1
        private Question _question;
        #endregion

        #region Constructor: +1
        public QuestionsProcessor()
        {
            Initialize();
        }
        #endregion

        #region Accessors(Getters): +10

        #region Major: +4
        public int getID() => this._question.ID;
        public int getSubjectID() => this._question.SubjectID;
        public string getText() => this._question.Text;
        public string getCorrectAnswer() => this._question.CorrectAnswer;
        #endregion

        #region Enum: +2
        public QuestionType getType() => this._question.Type;
        public QuestionDifficultyLevel getDifficultyLevel() => this._question.Difficulty_lvl;
        #endregion

        #region List: +1
        public List<string> getAnswerList() => this._question.Answers;
        #endregion

        #region Timestamps: +2
        public string getCreatedAt() => this._question.CreatedAt;
        public string getUpdatedAt() => this._question.UpdatedAt;
        #endregion

        #region Reference: +1
        public Question getQuestion() => this._question.Clone();
        #endregion

        #endregion

        #region Mutators(Setters): +9

        #region Major: +4
        public void setID(int _id) => this._question.ID = _id;
        public void setSubjectID(int _subjectId) => this._question.SubjectID = _subjectId;
        public void setText(string _text) => this._question.Text = _text;
        public void setCorrectAnswer(string _correctAnswer) => this._question.CorrectAnswer = _correctAnswer;
        #endregion

        #region Enum: +2
        public void setType(QuestionType _type) => this._question.Type = _type;
        public void setDifficultyLevel(QuestionDifficultyLevel _difflvl) => this._question.Difficulty_lvl = _difflvl;
        #endregion

        #region List: +1
        public void setAnswerList(List<string> _answers) => this._question.Answers = _answers;
        #endregion

        #region Timestamps: +2
        public void setCreatedAt() => this._question.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public void setUpdatedAt() => this._question.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        #endregion

        #endregion

        #region Methods: +4

        #region Serialization: +1
        public string ConvertAnswersToJsonFormat() => JsonConvert.SerializeObject(this._question.Answers);
        #endregion

        #region Lifecycle Methods: +3
        public void Initialize()
        {
            _question = new Question(
                0, 0, QuestionType.UnDefined,
                QuestionDifficultyLevel.NA,
                "", new List<string>(), ""
            );
        }

        public void Terminate()
        {
            _question?.Dispose();
            Initialize();
        }

        public void Dispose()
        {
            Terminate();
            GC.SuppressFinalize(this);
        }
        #endregion

        #endregion

        #region DeCtor: +1
        ~QuestionsProcessor() => Dispose();
        #endregion
    }
}
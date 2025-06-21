using BLL.SubjectHandling.Interface;
using BLL.SubjectHandling.Processors.Interface;
using DAL.Entity.SubjectHandling;
using DAL.Enum.SubjectHandling_Mod;

namespace BLL.SubjectHandling.Concrete
{
    public class QuestionBuilder : IQuestionBuilder
    {
        #region Fields: +1
        private readonly IQuestionProcessor _questionprocessor;
        #endregion

        #region Constructor: +1
        public QuestionBuilder(IQuestionProcessor _questionprocessor)
        {
            this._questionprocessor = _questionprocessor;
            Initialize();
        }
        #endregion

        #region Builder Methods: +7
        public IQuestionBuilder WithID(int id)
        {
            this._questionprocessor.setID(id);
            return this;
        }

        public IQuestionBuilder WithSubjectID(int subjectId)
        {
            this._questionprocessor.setSubjectID(subjectId);
            return this;
        }

        public IQuestionBuilder WithType(QuestionType type)
        {
            this._questionprocessor.setType(type);
            return this;
        }

        public IQuestionBuilder WithDifficulty(QuestionDifficultyLevel difficulty)
        {
            this._questionprocessor.setDifficultyLevel(difficulty);
            return this;
        }

        public IQuestionBuilder WithText(string text)
        {
            this._questionprocessor.setText(text);
            return this;
        }

        public IQuestionBuilder WithAnswers(List<string> answers)
        {
            this._questionprocessor.setAnswerList(answers);
            return this;
        }

        public IQuestionBuilder WithCorrectAnswer(string correctAnswer)
        {
            this._questionprocessor.setCorrectAnswer(correctAnswer);
            return this;
        }

        public Question Build()
        {
            var question = this._questionprocessor.getQuestion();
            Reset();
            return question;
        }
        #endregion

        #region Lifecycle Methods: +3
        public void Initialize() => this._questionprocessor.Initialize();

        public void Terminate() => this._questionprocessor.Terminate();

        public void Dispose()
        {
            this._questionprocessor.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Utility Methods: +1
        public void Reset() => Initialize();
        #endregion

        #region DeCtor: +1
        ~QuestionBuilder() => Dispose();
        #endregion
    }
}
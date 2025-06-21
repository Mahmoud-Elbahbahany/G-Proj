using DAL.Entity.SubjectHandling;
using DAL.Enum.SubjectHandling_Mod;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Interface
{
    public interface IQuestionBuilder : IDisposable
    {
        #region Builder Methods: +7
        IQuestionBuilder WithID(int id);
        IQuestionBuilder WithSubjectID(int subjectId);
        IQuestionBuilder WithType(QuestionType type);
        IQuestionBuilder WithDifficulty(QuestionDifficultyLevel difficulty);
        IQuestionBuilder WithText(string text);
        IQuestionBuilder WithAnswers(List<string> answers);
        IQuestionBuilder WithCorrectAnswer(string correctAnswer);
        Question Build();
        void Reset();
        #endregion

        #region Lifecycle Methods: +3
        void Initialize();
        void Terminate();
        void Dispose();
        #endregion
    }
}
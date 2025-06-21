using DAL.Entity.SubjectHandling;

namespace BLL.SubjectHandling.Interface
{
    public interface IQuestionsBankBuilder
    {
        #region Builder Methods
        IQuestionsBankBuilder WithID(int id);
        IQuestionsBankBuilder WithSubjectID(int subjectId);
        IQuestionsBankBuilder WithTitle(string title);
        IQuestionsBankBuilder WithDescription(string description);
        IQuestionsBankBuilder WithIsActive(bool isActive);
        IQuestionsBankBuilder WithQuestionsIDs(List<int> questionsIDs);
        QuestionsBank Build();
        void Reset();
        #endregion
    }
}
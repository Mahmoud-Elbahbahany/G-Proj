using BLL.SubjectHandling.Concrete;
using BLL.SubjectHandling.Interface;
using BLL.SubjectHandling.Processors.Concrete;
using BLL.SubjectHandling.Processors.Interface;
using DAL.Entity.SubjectHandling;
using DAL.Repository.Concrete;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Managers
{
    public static class QuestionsBankManager
    {
        private static readonly QuestionsBankRepository _repository = new QuestionsBankRepository();
        private static readonly IQuestionBankProcessor _processor = new QuestionsBankProcessor(new QuestionsBank());
        private static readonly IQuestionsBankBuilder _builder = new QuestionsBankBuilder(_processor);

        public static IEnumerable<QuestionsBank> GetAllQuestionBanks()
        {
            return _repository.GetAll();
        }

        public static QuestionsBank GetQuestionBankById(int id)
        {
            return _repository.GetById(id);
        }

        public static IEnumerable<QuestionsBank> GetQuestionBanksBySubject(int subjectId)
        {
            return _repository.GetBySubjectId(subjectId);
        }

        public static IEnumerable<QuestionsBank> GetActiveQuestionBanks()
        {
            return _repository.GetActiveBanks();
        }

        public static void AddQuestionBank(QuestionsBank questionBank)
        {
            _repository.Add(questionBank);
        }

        public static void UpdateQuestionBank(QuestionsBank questionBank)
        {
            _repository.Update(questionBank);
        }

        public static void DeleteQuestionBank(int id)
        {
            _repository.Delete(id);
        }

        public static QuestionsBank CreateQuestionBank(
            int id,
            int subjectId,
            string title,
            string description,
            bool isActive,
            List<int> questionsIds)
        {
            return _builder
                .WithID(id)
                .WithSubjectID(subjectId)
                .WithTitle(title)
                .WithDescription(description)
                .WithIsActive(isActive)
                .WithQuestionsIDs(questionsIds)
                .Build();
        }
    }
}
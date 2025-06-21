using BLL.SubjectHandling.Concrete;
using BLL.SubjectHandling.Interface;
using BLL.SubjectHandling.Processors.Concrete;
using DAL.Entity.SubjectHandling;
using DAL.Enum.SubjectHandling_Mod;
using DAL.Repository.Concrete;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Managers
{
    public static class QuestionManager
    {
        private static readonly QuestionRepository _repository = new QuestionRepository();
        private static readonly QuestionsProcessor _processor = new QuestionsProcessor();
        private static readonly IQuestionBuilder _builder = new QuestionBuilder(_processor);

        public static IEnumerable<Question> GetAllQuestions()
        {
            return _repository.GetAll();
        }

        public static Question GetQuestionById(int id)
        {
            return _repository.GetById(id);
        }

        public static IEnumerable<Question> GetQuestionsBySubject(int subjectId)
        {
            return _repository.GetBySubjectId(subjectId);
        }

        public static IEnumerable<Question> GetQuestionsByType(QuestionType type)
        {
            return _repository.GetByType(type);
        }

        public static IEnumerable<Question> GetQuestionsByDifficulty(QuestionDifficultyLevel difficulty)
        {
            return _repository.GetByDifficulty(difficulty);
        }

        public static void AddQuestion(Question question)
        {
            _repository.Add(question);
        }

        public static void UpdateQuestion(Question question)
        {
            _repository.Update(question);
        }

        public static void DeleteQuestion(int id)
        {
            _repository.Delete(id);
        }

        public static Question CreateQuestion(int id, int subjectId, QuestionType type,
            QuestionDifficultyLevel difficulty, string text, List<string> answers, string correctAnswer)
        {
            return _builder
                .WithID(id)
                .WithSubjectID(subjectId)
                .WithType(type)
                .WithDifficulty(difficulty)
                .WithText(text)
                .WithAnswers(answers)
                .WithCorrectAnswer(correctAnswer)
                .Build();
        }
    }
}
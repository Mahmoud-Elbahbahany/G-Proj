using BLL.SubjectHandling.Concrete;
using BLL.SubjectHandling.Interface;
using BLL.SubjectHandling.Processors.Concrete;
using BLL.SubjectHandling.Processors.Interface;
using DAL.Entity.SubjectHandling;
using DAL.Repository.Concrete;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Managers
{
    public static class TestManager
    {
        private static readonly TestRepository _repository = new TestRepository();
        private static readonly ITestProcessor _processor = new TestProcessor();
        private static readonly ITestBuilder _builder = new TestBuilder(_processor);

        public static IEnumerable<Test> GetAllTests()
        {
            return _repository.GetAll();
        }

        public static Test GetTestById(int id)
        {
            return _repository.GetById(id);
        }

        public static IEnumerable<Test> GetTestsByTestBankId(int testBankId)
        {
            return _repository.GetByTestBankId(testBankId);
        }

        public static IEnumerable<Test> GetPublishedTests()
        {
            return _repository.GetPublishedTests();
        }

        public static void AddTest(Test test)
        {
            _repository.Add(test);
        }

        public static void UpdateTest(Test test)
        {
            _repository.Update(test);
        }

        public static void DeleteTest(int id)
        {
            _repository.Delete(id);
        }

        public static Test CreateTest(int id, int testBankId, string title,
            string duration, bool isPublished, List<int> questionsIDs)
        {
            return _builder
                .WithID(id)
                .WithTestBankID(testBankId)
                .WithTitle(title)
                .WithDuration(duration)
                .WithIsPublished(isPublished)
                .WithQuestionsIDs(questionsIDs)
                .Build();
        }
    }
}
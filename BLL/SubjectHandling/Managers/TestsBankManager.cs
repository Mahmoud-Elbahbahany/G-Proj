using BLL.SubjectHandling.Concrete;
using BLL.SubjectHandling.Interface;
using BLL.SubjectHandling.Processors.Concrete;
using BLL.SubjectHandling.Processors.Interface;
using DAL.Entity.SubjectHandling;
using DAL.Repository.Concrete;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Managers
{
    public static class TestsBankManager
    {
        private static readonly TestsBankRepository _repository = new TestsBankRepository();
        private static readonly ITestsBankProcessor _processor = new TestsBankProcessor();
        private static readonly ITestsBankBuilder _builder = new TestsBankBuilder(_processor);

        public static IEnumerable<TestsBank> GetAllTestsBanks()
        {
            return _repository.GetAll();
        }

        public static TestsBank GetTestsBankById(int id)
        {
            return _repository.GetById(id);
        }

        public static IEnumerable<TestsBank> GetTestsBanksByInstructorId(int instructorId)
        {
            return _repository.GetByInstructorId(instructorId);
        }

        public static IEnumerable<TestsBank> GetActiveTestsBanks()
        {
            return _repository.GetActiveTestsBanks();
        }

        public static void AddTestsBank(TestsBank testsBank)
        {
            _repository.Add(testsBank);
        }

        public static void UpdateTestsBank(TestsBank testsBank)
        {
            _repository.Update(testsBank);
        }

        public static void DeleteTestsBank(int id)
        {
            _repository.Delete(id);
        }

        public static TestsBank CreateTestsBank(int id, int instructorId, string title,
            string description, bool isActive, List<int> testsIDs)
        {
            return _builder
                .WithID(id)
                .WithInstructorID(instructorId)
                .WithTitle(title)
                .WithDescription(description)
                .WithIsActive(isActive)
                .WithTestsIDs(testsIDs)
                .Build();
        }
    }
}
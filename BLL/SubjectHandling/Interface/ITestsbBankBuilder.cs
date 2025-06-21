using DAL.Entity.SubjectHandling;
using System;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Interface
{
    public interface ITestsBankBuilder : IDisposable
    {
        #region Builder Methods: +7
        public ITestsBankBuilder WithID(int id);
        public ITestsBankBuilder WithInstructorID(int instructorID);
        public ITestsBankBuilder WithTitle(string title);
        public ITestsBankBuilder WithDescription(string description);
        public ITestsBankBuilder WithIsActive(bool isActive);
        public ITestsBankBuilder WithTestsIDs(List<int> testsIDs);
        public TestsBank Build();
        public void Reset();
        #endregion

        #region Lifecycle Methods: +3
        public void Initialize();
        public void Terminate();
        public void Dispose();
        #endregion
    }
}
using BLL.SubjectHandling.Interface;
using BLL.SubjectHandling.Processors.Interface;
using DAL.Entity.SubjectHandling;
using System;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Concrete
{
    public class TestsBankBuilder : ITestsBankBuilder
    {
        #region Fields: +1
        private readonly ITestsBankProcessor _testbankprocessor;
        #endregion

        #region Constructor: +1
        public TestsBankBuilder(ITestsBankProcessor _testbankprocessor)
        {
            this._testbankprocessor = _testbankprocessor;
            this.Initialize();
        }
        #endregion

        #region Builder Methods: +7
        public ITestsBankBuilder WithID(int id)
        {
            this._testbankprocessor.setID(id);
            return this;
        }

        public ITestsBankBuilder WithInstructorID(int instructorID)
        {
            this._testbankprocessor.setInstructorID(instructorID);
            return this;
        }

        public ITestsBankBuilder WithTitle(string title)
        {
            _testbankprocessor.setTitle(title);
            return this;
        }

        public ITestsBankBuilder WithDescription(string description)
        {
            this._testbankprocessor.setDescription(description);
            return this;
        }

        public ITestsBankBuilder WithIsActive(bool isActive)
        {
            this._testbankprocessor.setIsActive(isActive);
            return this;
        }

        public ITestsBankBuilder WithTestsIDs(List<int> testsIDs)
        {
            this._testbankprocessor.setTestsIDs(testsIDs);
            return this;
        }

        public TestsBank Build()
        {
            var testsBank = this._testbankprocessor.getTestsBank();
            Reset();
            return testsBank;
        }
        #endregion

        #region Lifecycle Methods: +3
        public void Initialize() => this._testbankprocessor.Initialize();

        public void Terminate() => this._testbankprocessor.Terminate();

        public void Dispose()
        {
            this._testbankprocessor.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Utility Methods: +1
        public void Reset() => Initialize();
        #endregion

        #region DeCtor: +1
        ~TestsBankBuilder() => Dispose();
        #endregion
    }
}
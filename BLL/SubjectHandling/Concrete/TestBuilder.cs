using BLL.SubjectHandling.Interface;
using BLL.SubjectHandling.Processors.Interface;
using DAL.Entity.SubjectHandling;
using System;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Concrete
{
    public class TestBuilder : ITestBuilder
    {
        #region Fields: +1
        private readonly ITestProcessor _testprocessor;
        #endregion

        #region Constructor: +1
        public TestBuilder(ITestProcessor _testProcessor)
        {
            this._testprocessor = _testProcessor;
            Initialize();
        }
        #endregion

        #region Builder Methods: +7
        public ITestBuilder WithID(int id)
        {
            _testprocessor.setID(id);
            return this;
        }

        public ITestBuilder WithTestBankID(int testBankID)
        {
            _testprocessor.setTestBankID(testBankID);
            return this;
        }

        public ITestBuilder WithTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be null or empty", nameof(title));
            _testprocessor.setTitle(title);
            return this;
        }

        public ITestBuilder WithDuration(string duration)
        {
            if (string.IsNullOrWhiteSpace(duration)) throw new ArgumentException("Duration cannot be null or empty", nameof(duration));
            _testprocessor.setDuration(duration);
            return this;
        }

        public ITestBuilder WithIsPublished(bool isPublished)
        {
            _testprocessor.setIsPublished(isPublished);
            return this;
        }

        public ITestBuilder WithQuestionsIDs(List<int> questionsIDs)
        {
            _testprocessor.setQuestionsIDs(questionsIDs ?? new List<int>());
            return this;
        }

        public Test Build()
        {
            var test = _testprocessor.getTest();
            Reset();
            return test;
        }
        #endregion

        #region Lifecycle Methods: +3
        public void Initialize()
        {
            _testprocessor.Initialize();
        }

        public void Terminate()
        {
            _testprocessor.Terminate();
        }

        public void Dispose()
        {
            if (_testprocessor != null)
            {
                _testprocessor.Dispose();
            }
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Utility Methods: +1
        public void Reset()
        {
            Initialize();
        }
        #endregion

        #region DeCtor: +1
        ~TestBuilder() => Dispose();
        #endregion
    }
}
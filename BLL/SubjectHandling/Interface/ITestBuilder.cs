using DAL.Entity.SubjectHandling;
using System;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Interface
{
    public interface ITestBuilder : IDisposable
    {
        #region Builder Methods: +7
        ITestBuilder WithID(int id);
        ITestBuilder WithTestBankID(int testBankID);
        ITestBuilder WithTitle(string title);
        ITestBuilder WithDuration(string duration);
        ITestBuilder WithIsPublished(bool isPublished);
        ITestBuilder WithQuestionsIDs(List<int> questionsIDs);
        Test Build();
        void Reset();
        #endregion

        #region Lifecycle Methods: +3
        void Initialize();
        void Terminate();
        void Dispose();
        #endregion
    }
}
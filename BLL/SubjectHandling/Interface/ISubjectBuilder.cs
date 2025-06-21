using DAL.Entity.SubjectHandling;
using System;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Interface
{
    public interface ISubjectBuilder : IDisposable
    {
        #region Builder Methods: +7
        public ISubjectBuilder WithID(int id);
        public ISubjectBuilder WithInstructorID(int instructorID);
        public ISubjectBuilder WithCodeID(string codeID);
        public ISubjectBuilder WithName(string name);
        public ISubjectBuilder WithDescription(string description);
        public ISubjectBuilder WithQuestionsBankIDs(List<int> questionsBankIDs);
        public Subject Build();
        public void Reset();
        #endregion

        #region Lifecycle Methods: +3
        public void Initialize();
        public void Terminate();
        public void Dispose();
        #endregion
    }
}
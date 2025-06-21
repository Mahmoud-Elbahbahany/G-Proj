using BLL.SubjectHandling.Interface;
using BLL.SubjectHandling.Processors.Interface;
using DAL.Entity.SubjectHandling;
using System;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Concrete
{
    public class SubjectBuilder : ISubjectBuilder
    {
        #region Fields: +1
        private readonly ISubjectProcessor _subjprocessor;
        #endregion

        #region Constructor: +1
        public SubjectBuilder(ISubjectProcessor _subjprocessor)
        {
            this._subjprocessor = _subjprocessor;
            Initialize();
        }
        #endregion

        #region Builder Methods: +7
        public ISubjectBuilder WithID(int id)
        {
            _subjprocessor.setID(id);
            return this;
        }

        public ISubjectBuilder WithInstructorID(int instructorID)
        {
            _subjprocessor.setInstructorID(instructorID);
            return this;
        }

        public ISubjectBuilder WithCodeID(string codeID)
        {
            _subjprocessor.setCodeID(codeID);
            return this;
        }

        public ISubjectBuilder WithName(string name)
        {
            _subjprocessor.setName(name);
            return this;
        }

        public ISubjectBuilder WithDescription(string description)
        {
            _subjprocessor.setDescription(description);
            return this;
        }

        public ISubjectBuilder WithQuestionsBankIDs(List<int> questionsBankIDs)
        {
            _subjprocessor.setQuestionsBankIDs(questionsBankIDs);
            return this;
        }

        public Subject Build()
        {
            var subject = _subjprocessor.getSubject();
            Reset();
            return subject;
        }
        #endregion

        #region Lifecycle Methods: +3
        public void Initialize() => _subjprocessor.Initialize();

        public void Terminate() => _subjprocessor.Terminate();

        public void Dispose()
        {
            _subjprocessor.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Utility Methods: +1
        public void Reset() => Initialize();
        #endregion

        #region DeCtor: +1
        ~SubjectBuilder() => Dispose();
        #endregion
    }
}
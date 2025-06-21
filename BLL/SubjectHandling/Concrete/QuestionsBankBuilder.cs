using BLL.SubjectHandling.Interface;
using BLL.SubjectHandling.Processors.Interface;
using DAL.Entity.SubjectHandling;
using System;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Concrete
{
    public class QuestionsBankBuilder : IQuestionsBankBuilder
    {
        #region Fields
        private IQuestionBankProcessor _questionsBankProcessor;
        #endregion

        #region Constructor
        public QuestionsBankBuilder(IQuestionBankProcessor _questionsBankProcessor) =>
            this._questionsBankProcessor = _questionsBankProcessor;
        #endregion

        #region Builder Methods
        public IQuestionsBankBuilder WithID(int id)
        {
            this._questionsBankProcessor.setID(id);
            return this;
        }

        public IQuestionsBankBuilder WithSubjectID(int subjectId)
        {
            this._questionsBankProcessor.setSubjectID(subjectId);
            return this;
        }

        public IQuestionsBankBuilder WithTitle(string title)
        {
            this._questionsBankProcessor.setTitle(title);
            return this;
        }

        public IQuestionsBankBuilder WithDescription(string description)
        {
            this._questionsBankProcessor.setDescription(description);
            return this;
        }

        public IQuestionsBankBuilder WithIsActive(bool isActive)
        {
            this._questionsBankProcessor.setIsActive(isActive);
            return this;
        }

        public IQuestionsBankBuilder WithQuestionsIDs(List<int> questionsIDs)
        {
            this._questionsBankProcessor.setQuestionsIDs(questionsIDs);
            return this;
        }

        public QuestionsBank Build()
        {
            var result = this._questionsBankProcessor.ReturnQuestionBankObj().Clone();
            Reset();
            return result;
        }

        public void Reset()
        {
            _questionsBankProcessor.Dispose();
            _questionsBankProcessor = null;
        }
        #endregion
    }
}
using BLL.SubjectHandling.Processors.Interface;
using DAL.Entity.SubjectHandling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Processors.Concrete
{
    public class QuestionsBankProcessor : IQuestionBankProcessor
    {
        #region Fields: +1
        private QuestionsBank _questionsBank;
        #endregion

        #region Constructor: +1
        public QuestionsBankProcessor(QuestionsBank questionsBank) => Initialize();

        #endregion

        #region Accessors(Getters)

        #region Major: +5
        public int getID() { return this._questionsBank.ID; }
        public int getSubjectID() { return this._questionsBank.SubjectID; }
        public string getTitle() { return this._questionsBank.Title; }
        public string getDescription() { return this._questionsBank.Description; }
        public bool getIsActive() { return this._questionsBank.IsActive; }
        public string getCreatedAt() { return this._questionsBank.CreatedAt; }
        public string getUpdateAt() { return this._questionsBank.UpdatedAt; }
        #endregion

        #region JSON: +1
        public string getQuestionsIDsJson() { return JsonConvert.SerializeObject(this._questionsBank.QuestionsIDs); }
        #endregion

        #region List: +1
        public List<int>? getQuestionsIDs() { return this._questionsBank.QuestionsIDs; }
        #endregion

        #region Obj: +1
        public QuestionsBank ReturnQuestionBankObj() { return this._questionsBank.Clone(); }
        #endregion

        #endregion

        #region Mutators(Setters): +7

        #region Major: +5
        public void setID(int _ID) => this._questionsBank.ID = _ID;
        public void setSubjectID(int _SubjectID) => this._questionsBank.SubjectID = _SubjectID;
        public void setTitle(string _Title) => this._questionsBank.Title = _Title;
        public void setDescription(string _Description) => this._questionsBank.Description = _Description;
        public void setIsActive(bool _IsActive) => this._questionsBank.IsActive = _IsActive;
        public void setCreatedAt() => this._questionsBank.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public void setUpdateAt() => this._questionsBank.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        #endregion

        #region JSON: +1
        public void setQuestionsIDsJson(string _QuestionsIDsJson) =>
            this._questionsBank.QuestionsIDs = JsonConvert.DeserializeObject<List<int>>(_QuestionsIDsJson) ?? new List<int>();
        #endregion

        #region List: +1
        public void setQuestionsIDs(List<int> _QuestionsIDs) => this._questionsBank.QuestionsIDs = _QuestionsIDs;
        #endregion

        #endregion

        #region Conversion_Methods: +2
        public void ConvertQuestionsIDsToJSON() => 
            JsonConvert.SerializeObject(this._questionsBank.QuestionsIDs);
        public void ConvertQuestionsIDsToList() =>
            this._questionsBank.QuestionsIDs = JsonConvert.DeserializeObject<List<int>>(getQuestionsIDsJson()) ?? new List<int>();
        #endregion

        #region Lifecycle Methods
        public void Initialize()
        {
            this._questionsBank = new QuestionsBank(0, 0, "", "", false, new List<int>());
        }

        public void Terminate()
        {
            this._questionsBank.QuestionsIDs?.Clear();
        }

        public void Dispose()
        {
            this.Terminate();
            this._questionsBank.Dispose();
            this._questionsBank = null;
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
using BLL.SubjectHandling.Processors.Interface;
using DAL.Entity.SubjectHandling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Processors.Concrete
{
    public class SubjectProcessor : ISubjectProcessor
    {
        #region Fields: +1
        private Subject _subject;
        #endregion

        #region Constructor: +1
        public SubjectProcessor() => Initialize();
        #endregion

        #region Accessors(Getters): +9

        #region Major: +5
        public int getID() => _subject.ID;
        public int getInstructorID() => _subject.InstructorID;
        public string getCodeID() => _subject.CodeID;
        public string getName() => _subject.Name;
        public string getDescription() => _subject.Description;
        #endregion

        #region List: +1
        public List<int> getQuestionsBankIDs() => _subject.QuestionsBankIDs;
        #endregion

        #region Timestamps: +2
        public string getCreatedAt() => this._subject.CreatedAt;
        public string getUpdatedAt() => this._subject.UpdatedAt;
        #endregion

        #region Reference: +1
        public Subject getSubject() => this._subject.Clone();
        #endregion

        #endregion

        #region Mutators(Setters): +8

        #region Major: +5
        public void setID(int id) => _subject.ID = id;
        public void setInstructorID(int instructorID) => _subject.InstructorID = instructorID;
        public void setCodeID(string codeID) => _subject.CodeID = codeID;
        public void setName(string name) => _subject.Name = name;
        public void setDescription(string description) => _subject.Description = description;
        #endregion

        #region List: +1
        public void setQuestionsBankIDs(List<int> questionsBankIDs) => _subject.QuestionsBankIDs = questionsBankIDs;
        #endregion

        #region Timestamps: +2
        public void setCreatedAt() => _subject.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public void setUpdatedAt() => _subject.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        #endregion

        #endregion

        #region Methods: +5

        #region Serialization: +1
        public string ConvertQuestionsBankIDsToJsonFormat() => JsonConvert.SerializeObject(_subject.QuestionsBankIDs);
        #endregion

        #region Lifecycle Methods: +4
        public void Initialize()
        {
            _subject = new Subject(0, 0, "", "", "", new List<int>());
            setCreatedAt();
        }

        public void Terminate()
        {
            _subject?.Dispose();
            Initialize();
        }

        public void Dispose()
        {
            Terminate();
            GC.SuppressFinalize(this);
        }
        #endregion

        #endregion
    }
}
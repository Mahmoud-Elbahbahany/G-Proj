using DAL.Entity.SubjectHandling;
using System;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Processors.Interface
{
    public interface ISubjectProcessor : IDisposable
    {
        #region Accessors(Getters): +9

        #region Major: +5
        public int getID();
        public int getInstructorID();
        public string getCodeID();
        public string getName();
        public string getDescription();
        #endregion

        #region List: +1
        public List<int> getQuestionsBankIDs();
        #endregion

        #region Timestamps: +2
        public string getCreatedAt();
        public string getUpdatedAt();
        #endregion

        #region Reference: +1
        public Subject getSubject();
        #endregion

        #endregion

        #region Mutators(Setters): +8

        #region Major: +5
        public void setID(int id);
        public void setInstructorID(int instructorID);
        public void setCodeID(string codeID);
        public void setName(string name);
        public void setDescription(string description);
        #endregion

        #region List: +1
        public void setQuestionsBankIDs(List<int> questionsBankIDs);
        #endregion

        #region Timestamps: +2
        public void setCreatedAt();
        public void setUpdatedAt();
        #endregion

        #endregion

        #region Methods: +5

        #region Serialization: +1
        public string ConvertQuestionsBankIDsToJsonFormat();
        #endregion

        #region Lifecycle Methods: +4
        public void Initialize();
        public void Terminate();
        public void Dispose();
        #endregion

        #endregion
    }
}
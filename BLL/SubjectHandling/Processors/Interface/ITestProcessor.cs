using DAL.Entity.SubjectHandling;
using System;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Processors.Interface
{
    public interface ITestProcessor : IDisposable
    {
        #region Accessors(Getters): +8

        #region Major: +5
        public int getID();
        public int getTestBankID();
        public string getTitle();
        public string getDuration();
        public bool getIsPublished();
        #endregion

        #region List: +1
        public List<int> getQuestionsIDs();
        #endregion

        #region Timestamps: +2
        public string getCreatedAt();
        public string getUpdatedAt();
        #endregion

        #region Reference: +1
        public Test getTest();
        #endregion

        #endregion

        #region Mutators(Setters): +8

        #region Major: +5
        public void setID(int id);
        public void setTestBankID(int testBankID);
        public void setTitle(string title);
        public void setDuration(string duration);
        public void setIsPublished(bool isPublished);
        #endregion

        #region List: +1
        public void setQuestionsIDs(List<int> questionsIDs);
        #endregion

        #region Timestamps: +2
        public void setCreatedAt();
        public void setUpdatedAt();
        #endregion

        #endregion

        #region Methods: +6

        #region Serialization: +1
        public string ConvertQuestionsIDsToJsonFormat();
        #endregion

        #region Deserialization: +1
        public List<int> ConvertQuestionsIDsToListFormat();
        #endregion

        #region Lifecycle Methods: +4
        public void Initialize();
        public void Terminate();
        public void Dispose();
        #endregion

        #endregion
    }
}
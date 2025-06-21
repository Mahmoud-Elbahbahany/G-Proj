using BLL.SubjectHandling.Processors.Interface;
using DAL.Entity.SubjectHandling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Processors.Concrete
{
    public class TestProcessor : ITestProcessor
    {
        #region Fields: +1
        private Test _test;
        #endregion

        #region Constructor: +1
        public TestProcessor() => Initialize();
        #endregion

        #region Accessors(Getters): +9

        #region Major: +5
        public int getID() => _test.ID;
        public int getTestBankID() => _test.TestBankID;
        public string getTitle() => _test.Title;
        public string getDuration() => _test.Duration;
        public bool getIsPublished() => _test.IsPublished;
        #endregion

        #region List: +1
        public List<int> getQuestionsIDs() => _test.QuestionsIDs;
        #endregion

        #region Timestamps: +2
        public string getCreatedAt() => _test.CreatedAt;
        public string getUpdatedAt() => _test.UpdatedAt;
        #endregion

        #region Reference: +1
        public Test getTest() => _test.Clone();
        #endregion

        #endregion

        #region Mutators(Setters): +8

        #region Major: +5
        public void setID(int id) => _test.ID = id;
        public void setTestBankID(int testBankID) => _test.TestBankID = testBankID;
        public void setTitle(string title) => _test.Title = title;
        public void setDuration(string duration) => _test.Duration = duration;
        public void setIsPublished(bool isPublished) => _test.IsPublished = isPublished;
        #endregion

        #region List: +1
        public void setQuestionsIDs(List<int> questionsIDs) => _test.QuestionsIDs = questionsIDs;
        #endregion

        #region Timestamps: +2
        public void setCreatedAt() => _test.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public void setUpdatedAt() => _test.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        #endregion

        #endregion

        #region Methods: +6

        #region Serialization: +1
        public string ConvertQuestionsIDsToJsonFormat() => JsonConvert.SerializeObject(_test.QuestionsIDs);
        #endregion

        #region Deserialization: +1
        public List<int> ConvertQuestionsIDsToListFormat() => JsonConvert.DeserializeObject<List<int>>(ConvertQuestionsIDsToJsonFormat());
        #endregion

        #region Lifecycle Methods: +4
        public void Initialize() => _test = new Test(0, 0, "", "", false, new List<int>());

        public void Terminate()
        {
            _test?.Dispose();
            Initialize();
        }

        public void Dispose()
        {
            Terminate();
            GC.SuppressFinalize(this);
        }
        #endregion

        #endregion

        #region DeCtor: +1
        ~TestProcessor() => Dispose();
        #endregion
    }
}
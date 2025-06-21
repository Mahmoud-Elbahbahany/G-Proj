using BLL.SubjectHandling.Processors.Interface;
using DAL.Entity.SubjectHandling;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Processors.Concrete
{
    public class TestsBankProcessor : ITestsBankProcessor
    {
        #region Fields: +1
        private TestsBank _testsBank;
        #endregion

        #region Constructor: +1
        public TestsBankProcessor() => Initialize();
        
        #endregion

        #region Accessors(Getters): +8

        #region Major: +5
        public int getID() => _testsBank.ID;
        public int getInstructorID() => _testsBank.InstructorID;
        public string getTitle() => _testsBank.Title;
        public string getDescription() => _testsBank.Description;
        public bool getIsActive() => _testsBank.IsActive;
        #endregion

        #region List: +1
        public List<int> getTestsIDs() => _testsBank.TestsIDs;
        #endregion

        #region Timestamps: +2
        public string getCreatedAt() => _testsBank.CreatedAt;
        public string getUpdatedAt() => _testsBank.UpdatedAt;
        #endregion

        #region Reference: +1
        public TestsBank getTestsBank() => _testsBank.Clone();
        #endregion

        #endregion

        #region Mutators(Setters): +8

        #region Major: +5
        public void setID(int id) => _testsBank.ID = id;
    
        public void setInstructorID(int instructorID) => _testsBank.InstructorID = instructorID;
        public void setTitle(string title) => _testsBank.Title = title;
        public void setDescription(string description) => _testsBank.Description = description;
        public void setIsActive(bool isActive) => _testsBank.IsActive = isActive;
        #endregion

        #region List: +1
        public void setTestsIDs(List<int> testsIDs) => _testsBank.TestsIDs = testsIDs;
        #endregion

        #region Timestamps: +2
        public void setCreatedAt() => _testsBank.CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public void setUpdatedAt() => _testsBank.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        #endregion

        #endregion

        #region Methods: +6

        #region Serialization: +1
        public string ConvertTestsIDsToJsonFormat() => JsonConvert.SerializeObject(_testsBank.TestsIDs);
        #endregion

        #region Deserialization: +1
        public List<int> ConvertTestsIDsToListFormat() => JsonConvert.DeserializeObject<List<int>>(ConvertTestsIDsToJsonFormat());
        #endregion

        #region Lifecycle Methods: +4
        public void Initialize() => _testsBank = new TestsBank(0, 0, "", "", false, new List<int>());
        public void Terminate()
        {
            _testsBank?.Dispose();
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
        ~TestsBankProcessor() => Dispose();
        #endregion
    }
}
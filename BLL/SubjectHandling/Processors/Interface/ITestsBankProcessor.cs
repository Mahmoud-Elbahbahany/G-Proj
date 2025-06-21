using DAL.Entity.SubjectHandling;
using System;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Processors.Interface
{
    public interface ITestsBankProcessor : IDisposable
    {
        #region Accessors(Getters): +8

        #region Major: +5
        public int getID();
        public int getInstructorID();
        public string getTitle();
        public string getDescription();
        public bool getIsActive();
        #endregion

        #region List: +1
        public List<int> getTestsIDs();
        #endregion

        #region Timestamps: +2
        public string getCreatedAt();
        public string getUpdatedAt();
        #endregion

        #region Reference: +1
        public TestsBank getTestsBank();
        #endregion

        #endregion

        #region Mutators(Setters): +8

        #region Major: +5
        public void setID(int id);
        public void setInstructorID(int instructorID);
        public void setTitle(string title);
        public void setDescription(string description);
        public void setIsActive(bool isActive);
        #endregion

        #region List: +1
        public void setTestsIDs(List<int> testsIDs);
        #endregion

        #region Timestamps: +2
        public void setCreatedAt();
        public void setUpdatedAt();
        #endregion

        #endregion

        #region Methods: +6

        #region Serialization: +1
        public string ConvertTestsIDsToJsonFormat();
        #endregion

        #region Deserialization: +1
        public List<int> ConvertTestsIDsToListFormat();
        #endregion

        #region Lifecycle Methods: +4
        public void Initialize();
        public void Terminate();
        public void Dispose();
        #endregion

        #endregion
    }
}
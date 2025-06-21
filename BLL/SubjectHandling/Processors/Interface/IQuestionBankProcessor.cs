using DAL.Entity.SubjectHandling;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Processors.Interface
{
    public interface IQuestionBankProcessor : IDisposable
    {
        #region Accessors(Getters)

        #region Major: +5
        public int getID();
        public int getSubjectID();
        public string getTitle() ;
        public string getDescription() ;
        public bool getIsActive() ;
        public string getCreatedAt() ;
        public string getUpdateAt() ;
        #endregion

        #region JSON: +1
        public string getQuestionsIDsJson();
        #endregion

        #region List: +1
        public List<int>? getQuestionsIDs() ;
        #endregion

        #region Obj: +1
        public QuestionsBank ReturnQuestionBankObj();  
        #endregion

        #endregion

        #region Mutators(Setters): +7

        #region Major: +5
        public void setID(int _ID);
        public void setSubjectID(int _SubjectID);
        public void setTitle(string _Title);
        public void setDescription(string _Description);
        public void setIsActive(bool _IsActive);
        public void setCreatedAt();
        public void setUpdateAt();
        #endregion

        #region JSON: +1
        public void setQuestionsIDsJson(string _QuestionsIDsJson);
        #endregion

        #region List: +1
        public void setQuestionsIDs(List<int> _QuestionsIDs);
        #endregion

        #endregion

        #region Conversion_Methods: +2
        public void ConvertQuestionsIDsToJSON();
        public void ConvertQuestionsIDsToList();
        #endregion

        #region Lifecycle Methods
        public void Initialize();

        public void Terminate();

        public void Dispose();

        #endregion
    }
}
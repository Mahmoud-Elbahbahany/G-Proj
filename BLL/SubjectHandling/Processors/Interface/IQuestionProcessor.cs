using DAL.Entity.SubjectHandling;
using DAL.Enum.SubjectHandling_Mod;
using System.Collections.Generic;

namespace BLL.SubjectHandling.Processors.Interface
{
    public interface IQuestionProcessor : IDisposable
    {
        #region Accessors(Getters): +10

        #region Major: +4
        public int getID();
        public int getSubjectID();
        public string getText();
        public string getCorrectAnswer();
        #endregion

        #region Enum: +2
        public QuestionType getType();
        public QuestionDifficultyLevel getDifficultyLevel();
        #endregion

        #region List: +1
        public List<string> getAnswerList();
        #endregion

        #region Timestamps: +2
        public string getCreatedAt();
        public string getUpdatedAt();
        #endregion

        #region Reference: +1
        public Question getQuestion();
        #endregion

        #endregion

        #region Mutators(Setters): +9

        #region Major: +4
        public void setID(int _id);
        public void setSubjectID(int _subjectId);
        public void setText(string _text);
        public void setCorrectAnswer(string _correctAnswer);
        #endregion

        #region Enum: +2
        public void setType(QuestionType _type);
        public void setDifficultyLevel(QuestionDifficultyLevel _difflvl);
        #endregion

        #region List: +1
        public void setAnswerList(List<string> _answers);
        #endregion

        #region Timestamps: +2
        public void setCreatedAt();
        public void setUpdatedAt();
        #endregion

        #endregion

        #region Serialization: +1
        public string ConvertAnswersToJsonFormat();
        #endregion

        #region Lifecycle Methods: +3
        public void Initialize();
        public void Terminate();
        public void Dispose();
        #endregion
    }
}
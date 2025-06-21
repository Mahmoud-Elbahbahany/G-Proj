using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity.SubjectHandling
{
    public class QuestionsBank : ICloneable, IDisposable
    {
        #region Properties: +10

        #region Core: +2
        public const bool ISDISPOSABLE = true;
        public volatile bool _disposed = false;
        #endregion

        #region Major: +5

        #region ID_Constraints:
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID = default;
        #endregion

        #region SubjectID_Constraints:
        [Required(ErrorMessage = "SubjectID is required.")]
        public int SubjectID = default;
        #endregion

        #region Title_Constraints:
        [Required(ErrorMessage = "Question bank title is required.")]
        [StringLength(50, ErrorMessage = "Question bank title cannot exceed 50 characters.")]
        public string Title = string.Empty;
        #endregion

        #region Description_Constraints:
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description = string.Empty;
        #endregion

        #region Activation_Constraints:
        [Required]
        public bool IsActive = false;
        #endregion

        #endregion

        #region Json: +1

        #region QuestionsIDs_Constraints:
        [JsonProperty]
        public List<int> QuestionsIDs = new();
        #endregion

        #endregion

        #region Timestamp: +2

        #region CreatedAt_Constraints:
        [Required]
        [StringLength(19)]
        [DataType(DataType.DateTime)]
        public string CreatedAt = string.Empty;
        #endregion

        #region UpdatedAt_Constraints:
        [Required]
        [StringLength(19)]
        [DataType(DataType.DateTime)]
        public string UpdatedAt = string.Empty;
        #endregion

        #endregion

        #endregion

        #region Ctors: +2
        public QuestionsBank() { }

        public QuestionsBank(int _id, int _subjectid, string _title, string _description, bool _isactive, List<int> _questionsids)
        {
            ID = _id;
            SubjectID = _subjectid;
            Title = _title;
            Description = _description;
            IsActive = _isactive;
            QuestionsIDs = _questionsids;
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            UpdatedAt = CreatedAt;
        }
        #endregion

        #region Methods: +5

        #region Testing: +1
        public string ReturnQuestionsBankDetails()
        {
            return @$"
            Questions Bank ID: {this.ID}
            Subject ID: {this.SubjectID}
            Title: {this.Title}
            Description: {this.Description}
            Is Active: {this.IsActive}
            Contains Questions: {JsonConvert.SerializeObject(this.QuestionsIDs)}
            Created At: {this.CreatedAt}
            Updated At: {UpdatedAt}";
        }
        #endregion

        #region Disposition: +2
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                QuestionsIDs?.Clear();
                ID = default;
                SubjectID = default;
                Title = string.Empty;
                Description = string.Empty;
                IsActive = false;
                CreatedAt = string.Empty;
                UpdatedAt = string.Empty;
                QuestionsIDs = new List<int>();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Cloning: +2
        public QuestionsBank Clone()
        {
            return new QuestionsBank(
                ID,
                SubjectID,
                Title,
                Description,
                IsActive,
                new List<int>(QuestionsIDs))
            {
                CreatedAt = CreatedAt,
                UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
        }
        object ICloneable.Clone() => Clone();
        #endregion

        #region Utility: +1
        public int QuestionsCount => QuestionsIDs.Count;
        #endregion

        #endregion

        #region DeCtor: +1
        ~QuestionsBank() => Dispose(false);
        #endregion
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.Entity.SubjectHandling
{
    public class Test : ICloneable , IDisposable
    {
        #region Properties: +10

        #region Core: +2
        public const bool ISDISPOSABLE = true;
        public volatile bool _disposed = false;
        #endregion

        #region Major: +5

        #region ID_Constraints
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID = default;
        #endregion

        #region TestBankID_Constraints:
        public int TestBankID = default;
        #endregion

        #region Title_Constraints
        [Required(ErrorMessage = "Test's Title is required.")]
        [StringLength(25, ErrorMessage = "Test's Title cannot exceed 25 characters.")]
        public string Title = string.Empty;
        #endregion

        #region Duration_Constraints
        [Required]
        [StringLength(19)]
        [DataType(DataType.DateTime)]
        public string Duration = string.Empty;
        #endregion

        #region IsPublished_Constraints
        [Required]
        public bool IsPublished = false;
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
        public string CreatedAt = "";
        #endregion

        #region UpdatedAt_Constraints:
        [Required]
        [StringLength(19)]
        [DataType(DataType.DateTime)]
        public string UpdatedAt = "";
        #endregion

        #endregion

        #endregion

        #region Ctor: +2
        // Unusable Private Ctor:
        public Test() { }
        // Usabel Public Ctor:
        public Test(int _id, int _testbankid,string _title, string _duration, bool _ispublished, List<int> _questionsids)
        {
            this.ID = _id;
            this.TestBankID = _testbankid;
            this.Title = _title;    
            this.Duration = _duration;
            this.IsPublished = _ispublished;
            this.QuestionsIDs = _questionsids;
        }

        #endregion

        #region Methods: +5

        #region Testing: +1
        public string ReturnTestDetails()
        {
            return @$" 
            Test ID: {this.ID}
            Test Bank ID: {this.TestBankID}
            Test Title: {this.Title}
            Test Duration: {this.Duration}
            Test Is Published: {this.IsPublished}
            Questions IDs: {JsonConvert.SerializeObject(this.QuestionsIDs)}
            Created At: {this.CreatedAt}
            Update At: {this.UpdatedAt}";
        }
        #endregion

        #region Disposition: +2
        protected void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // Clear managed resources
                this.QuestionsIDs?.Clear();

                // Reset all values to defaults
                this.ID = default;
                this.TestBankID = default;
                this.Title = string.Empty;  // Changed from null to empty string
                this.Duration = string.Empty;
                this.IsPublished = false;
                this.CreatedAt = string.Empty;  // Changed from null
                this.UpdatedAt = string.Empty;
                this.QuestionsIDs = new List<int>();  // New list instead of null
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
        public Test Clone()
        {
            return new Test(
                this.ID,
                this.TestBankID,
                this.Title,
                this.Duration,
                this.IsPublished,
                new List<int>(this.QuestionsIDs)
                )
            {
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
        }
        object ICloneable.Clone() => Clone();
        #endregion

        #endregion

        #region DeCtor: +1
        ~Test() => Dispose(false);
        #endregion
    }

}

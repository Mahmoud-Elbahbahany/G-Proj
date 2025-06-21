using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity.SubjectHandling
{
    public class TestsBank : ICloneable, IDisposable
    {
        #region Properties: +10

        #region Core: +2
        public const bool ISDISPOSABLE = true;
        public volatile bool _disposed = false;
        #endregion

        #region Major +5

        #region ID_Constraints:
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID = default;
        #endregion

        #region InstructorID_Constraints:
        [Required]
        public int InstructorID = default;
        #endregion

        #region Title_Constraints:
        [Required(ErrorMessage = "Test bank title is required.")]
        [StringLength(50, ErrorMessage = "Test bank title cannot exceed 50 characters.")]
        public string Title = string.Empty;
        #endregion

        #region Description_Constraints:
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description = string.Empty; // Added description field
        #endregion

        #region Activation_Constraints:
        [Required]
        public bool IsActive = false; // Added to enable/disable test bank
        #endregion

        #endregion

        #region Json: +1

        #region TestsID_Constraints:
        [JsonProperty]
        public List<int> TestsIDs = new();
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
        // Unusable Private Ctor:
        private TestsBank() { }

        // Usabel Public Ctor:
        public TestsBank(int _id, int _instructorid, string _title, string _description, bool _isactive, List<int> _testsids)
        {
            this.ID = _id;
            this.InstructorID = _instructorid;
            this.Title = _title;
            this.Description = _description;
            this.IsActive = _isactive;
            this.TestsIDs = _testsids;
        }
        #endregion

        #region Methods: +5

        #region Testing: +1
        public string ReturnTestsBankDetails()
        {
            return @$"
            Tests Bank ID: {this.ID}
            Instructor ID: {this.InstructorID}
            Title: {this.Title}
            Description: {this.Description}
            Is Active: {this.IsActive}
            Contains Tests: {JsonConvert.SerializeObject(this.TestsIDs)} 
            Created At: {this.CreatedAt}
            Updated At: {this.UpdatedAt}";
        }
        #endregion

        #region Disposition: +2
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                this.TestsIDs?.Clear();
                this.ID = default;
                this.InstructorID = default;
                this.Title = string.Empty;
                this.Description = string.Empty;
                this.IsActive = false;
                this.CreatedAt = string.Empty;
                this.UpdatedAt = string.Empty;
                this.TestsIDs = new List<int>();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Cloning: +1
        public TestsBank Clone()
        {
            return new TestsBank(
                this.ID,
                this.InstructorID,
                this.Title,
                this.Description,
                this.IsActive,
                new List<int>(this.TestsIDs)
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
        ~TestsBank() => Dispose(false);
        #endregion
    }
}
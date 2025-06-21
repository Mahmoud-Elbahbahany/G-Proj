using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entity.SubjectHandling
{
    public class Subject : ICloneable, IDisposable
    {
        #region Properties: +10

        #region Major +5

        #region ID_Constraints
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID = default;
        #endregion

        #region InstructorID_Constraints
        [ForeignKey("Instructor")]
        [Required(ErrorMessage = "Instructor ID is required.")]
        public int InstructorID = default;
        #endregion

        #region Code_Constraints
        [Required(ErrorMessage = "Subject code is required.")]
        [StringLength(10, ErrorMessage = "Subject code cannot exceed 10 characters.")]
        public string CodeID = string.Empty;
        #endregion

        #region Name_Constraints
        [Required(ErrorMessage = "Subject name is required.")]
        [StringLength(15, ErrorMessage = "Subject name cannot exceed 15 characters.")]
        public string Name = string.Empty;
        #endregion

        #region Description_Constraints
        [StringLength(100, ErrorMessage = "Description cannot exceed 100 characters.")]
        public string Description = string.Empty;
        #endregion

        #endregion

        #region JSON: +1

        #region QuestionsBank_Constraints
        [Required(ErrorMessage = "QuestionBank creation is required.")] //May be Modified!
        [ForeignKey("QuestionsBank")] //May be Modified!
        [Column("QuestionsBank_IDs")] // Maps to "Answers" column in database
        [JsonProperty("QuestionsBank_IDs")] // Optional: For Newtonsoft.Json serialization
        public List<int> QuestionsBankIDs = new();
        #endregion

        #endregion

        #region Timestamps: +2

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

        #region Core: +2
        private volatile bool _disposed = false;
        public const bool IS_DISPOSABLE = true;
        #endregion 

        #endregion

        #region Ctor: +2

        /// <summary>
        /// Unusable Private Ctor:
        /// </summary>
        private Subject()
        {

        }

        /// <summary>
        /// Usabel Public Ctor:
        /// </summary>
        public Subject(int Id, int _InstructorID, string _codeID, string _name, string _description, List<int> _questionsBanksIDs)
        {
            this.ID = Id;
            this.InstructorID = _InstructorID;
            this.CodeID = _codeID;
            this.Name = _name;
            this.Description = _description;
            this.QuestionsBankIDs = _questionsBanksIDs;
        }

        #endregion

        #region Methods: +5

        #region Testing: +1
        public string ReturnSubjectDetails()
        {
            return @$" 
            Subject ID: {this.ID}
            Instructor ID : {this.InstructorID}
            Subject CodeID: {this.CodeID}
            Subject Name: {this.Name}
            Subject Description: {this.Description}
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
                // Managed resources
                this.QuestionsBankIDs?.Clear();
                // Nullify references
                this.CodeID = null;
                this.Name = null;
                this.Description = null;
                this.CreatedAt = null;
                this.UpdatedAt = null;
                this.QuestionsBankIDs = null;
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
        public Subject Clone()
        { //Deep Copy
            return new Subject(
                this.ID,
                this.InstructorID,
                this.CodeID,
                this.Name,
                this.Description,
                new List<int>(this.QuestionsBankIDs)
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
        ~Subject() => Dispose(false);
        #endregion
    }
}




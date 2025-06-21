using DAL.Enum.SubjectHandling_Mod;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace DAL.Entity.SubjectHandling
{
    public class Question : ICloneable, IDisposable
    {
        #region Properties:

        #region Reference:
        //private Question _instance = null;
        #endregion

        #region Major:

        #region ID_Constraints:
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID = default;
        #endregion

        #region SubjectID_Constraints:
        [Required(ErrorMessage = "SubjectID is required.")]
        [ForeignKey("Subject")]
        public int SubjectID = default;
        public Subject? Subject = default; //this Property will be removed!(Unnecessary) at Deployment statge.
        #endregion

        #region QuestionText_Constraints:
        [Required(ErrorMessage = "Question text is required.")]
        [StringLength(500, ErrorMessage = "Question text cannot exceed 500 characters.")]
        public string Text = string.Empty;
        #endregion

        #region CorrectAnswer_Constraints:
        [Required(ErrorMessage = "Correct answer is required.")]
        public string CorrectAnswer = string.Empty;
        #endregion

        #endregion

        #region Enum:

        #region Type_Constraints:
        [Required(ErrorMessage = "Question type is required.")]
        [EnumDataType(typeof(QuestionType), ErrorMessage = "Invalid question type.")]
        public QuestionType Type = default;
        #endregion

        #region Difficulty_Constraints:
        [Required(ErrorMessage = "Question Difficulty Level is required.")]
        [EnumDataType(typeof(QuestionDifficultyLevel), ErrorMessage = "Invalid question difficulty level.")]
        public QuestionDifficultyLevel Difficulty_lvl = default;
        #endregion

        #endregion

        #region JSON:

        #region AnswersJson_Constraints:
        [Column("Answers")] // Maps to "Answers" column in database
        [JsonProperty("Answers")] // Optional: For Newtonsoft.Json serialization
        public List<string> Answers = new();
        #endregion

        #endregion

        #region Timestamps:

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
        private bool disposedValue;
        #endregion

        #endregion

        #region Core:
        private volatile bool _disposed = false;
        #endregion

        #endregion

        #region Ctors: +2
        /// Unusable Private Ctor:
        private Question() { }

        /// Usabel Public Ctor:
        public Question(int _id, int _subjectId, QuestionType _type, QuestionDifficultyLevel _difflvl, string _text, List<string> _answers, string _correctAnswer)
        {
            this.ID = _id;
            this.SubjectID = _subjectId;
            this.Type = _type;
            this.Difficulty_lvl = _difflvl;
            this.Text = _text;
            this.Answers = _answers;
            this.CorrectAnswer = _correctAnswer;
        }
        #endregion

        #region Methods:

        #region Core: 0
        #endregion

        #region Testing:
        public string ReturnQuestionDetails()
        {
            return @$" 
            Question ID: {this.ID}
            Subject ID: {this.SubjectID}
            Question Text: {this.Text}
            Question CorrectAnswer: {this.CorrectAnswer}
            Question Answers: {JsonConvert.SerializeObject(this.Answers)}
            Question Type: {this.Type.ToString()}
            Question Difficulty: {this.Difficulty_lvl.ToString()}
            Created At: {this.CreatedAt}
            Update At: {this.UpdatedAt}";
        }
        #endregion

        #region Disposition:
        protected void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // Managed resources
                Answers?.Clear();
                (Subject as IDisposable)?.Dispose();

                // Nullify references
                Answers = null;
                Subject = null;
                Text = null;
                CorrectAnswer = null;
                CreatedAt = null;
                UpdatedAt = null;
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Cloning:
        public Question Clone()
        {   //Deep Copy
            return new Question(
                this.ID,
                this.SubjectID,
                this.Type,
                this.Difficulty_lvl,
                this.Text,
                new List<string>(this.Answers),
                this.CorrectAnswer
            );
        }
        object ICloneable.Clone() => Clone();
        #endregion

        #endregion

        #region DeCtor:
        /// <summary>
        /// Auto Usable DeCtor:
        /// </summary>
        ~Question() => Dispose(false);
        #endregion
    }
}

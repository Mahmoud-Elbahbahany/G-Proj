using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Enum.SubjectHandling_Mod
{
    public enum QuestionType
    {
        UnDefined,
        MCQ,
        TrueFalse,
        Essay
    }

    public enum QuestionDifficultyLevel
    {
        NA,
        VeryEasy,
        Easy, 
        Moderate,
        Difficult,
        VeryDifficult,
    }
}

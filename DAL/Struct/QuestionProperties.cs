using DAL.Enum.SubjectHandling_Mod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Struct
{
    public struct QuestionProperties
    {
        private int ID;
        private string Text;
        private QuestionType Type;
        private IEnumerable<string> Options;
        private string CorrectAnswer;
    }
}

using NagagQuizWebv2.Viewmodels;
using System.Collections.Generic;

namespace NagagQuizWebv2.Models
{
    public class QuizResultResolve
    {
        public string matchToken { get; set; }
        public string quizStartTime { get; set; }
        public string timeInMilliseconds { get; set; }
        public List<ChoiceAnswer> matchResultChoicePairs { get; set; }
    }
}

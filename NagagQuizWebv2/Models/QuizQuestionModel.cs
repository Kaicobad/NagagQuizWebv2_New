using NagagQuizWebv2.Viewmodels;
using System.Collections.Generic;

namespace NagagQuizWebv2.Models
{
    public class QuizQuestionModel
    {
        public int questionId { get; set; }
        public string question { get; set; }
        public string imageLocation { get; set; }
        public bool isActive { get; set; }
        public int quizCategoryId { get; set; }
        public int questionScore { get; set; }
        public int quizType { get; set; }
        public List<ChoiceVm> choices { get; set; }
    }
}

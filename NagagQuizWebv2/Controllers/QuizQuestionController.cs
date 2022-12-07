using Microsoft.AspNetCore.Mvc;
using NagagQuizWebv2.Models;
using NagagQuizWebv2.Repository;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;
using NagagQuizWebv2.Viewmodels;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Schema;

namespace NagagQuizWebv2.Controllers
{
    public class QuizQuestionController : Controller
    {
        private readonly QuizQuestion quizQuestion;
        private readonly CheckAnswer checkAnswer;
        private readonly SubmitResult submitResult;
        private readonly Prediction prediction;
        private readonly NagadPoll nagadPoll;

        public QuizQuestionController(QuizQuestion quizQuestion, CheckAnswer checkAnswer,
            SubmitResult submitResult, Prediction prediction, NagadPoll nagadPoll)
        {
            this.quizQuestion = quizQuestion;
            this.checkAnswer = checkAnswer;
            this.submitResult = submitResult;
            this.prediction = prediction;
            this.nagadPoll = nagadPoll;
        }
        public async Task<IActionResult> QuizStep()
        {
            return View();
        }
        //select random elements from list
        public static List<t> GetRandomElements<t>(IEnumerable<t> list, int elementsCount)
        {
            return list.OrderBy(x => Guid.NewGuid()).Take(elementsCount).ToList();
        }
        public async Task<IActionResult> Index()
        {
            List<QuizQuestionModel> aList =  new List<QuizQuestionModel>();

            List<QuizQuestionModel> QuestionQuiz = new List<QuizQuestionModel>();
            List<QuizQuestionModel> ImageQuiz = new List<QuizQuestionModel>();
            List<QuizQuestionModel> ViewQuestionList = new List<QuizQuestionModel>();

            using (StreamReader r = new StreamReader(@"JsonData/QuizQuestion.json"))
            {
                string json = r.ReadToEnd();
                ResponseDataCustom<List<QuizQuestionModel>> con = JsonConvert.DeserializeObject<ResponseDataCustom<List<QuizQuestionModel>>>(json);

                aList = con.data;

                foreach (QuizQuestionModel item in aList)
                {
                    if (item.quizType == 1)
                    {
                        QuestionQuiz.Add(item);
                    }
                    if (item.quizType == 2)
                    {
                        ImageQuiz.Add(item);
                    }
                }

                var randomQuestionList =  GetRandomElements(QuestionQuiz, 3);

                foreach (var item in randomQuestionList)
                {
                    ViewQuestionList.Add(item);
                }

                var randomImageQuestionList = GetRandomElements(ImageQuiz, 2);
                foreach (var item in randomImageQuestionList)
                {
                    ViewQuestionList.Add(item);
                }


                foreach (QuizQuestionModel items in aList)
                {
                    int questionScore = Convert.ToInt32(items.questionScore);
                    ViewBag.Qmark = questionScore;
                    break;
                }
                ViewBag.quentions = ViewQuestionList;
                ViewBag.quizTime = Convert.ToString(DateTime.Now);
            }
            return View("Index"); ;
        }
        public IActionResult GetQuiz()
        {
            List<QuizQuestionModel> aList = new List<QuizQuestionModel>();

            List<QuizQuestionModel> QuestionQuiz = new List<QuizQuestionModel>();
            List<QuizQuestionModel> ImageQuiz = new List<QuizQuestionModel>();
            List<QuizQuestionModel> ViewQuestionList = new List<QuizQuestionModel>();

            using (StreamReader r = new StreamReader(@"JsonData/QuizQuestion.json"))
            {
                string json = r.ReadToEnd();
                ResponseDataCustom<List<QuizQuestionModel>> con = JsonConvert.DeserializeObject<ResponseDataCustom<List<QuizQuestionModel>>>(json);

                aList = con.data;

                foreach (var item in aList)
                {
                    if (item.quizType == 1)
                    {
                        QuestionQuiz.Add(item);
                    }
                    if (item.quizType == 2)
                    {
                        ImageQuiz.Add(item);
                    }
                }

                var randomQuestionList = GetRandomElements(QuestionQuiz, 3);

                foreach (var item in randomQuestionList)
                {
                    ViewQuestionList.Add(item);
                }

                var randomImageQuestionList = GetRandomElements(ImageQuiz, 2);
                foreach (var item in randomImageQuestionList)
                {
                    ViewQuestionList.Add(item);
                }


                foreach (QuizQuestionModel items in aList)
                {
                    int questionScore = Convert.ToInt32(items.questionScore);
                    ViewBag.Qmark = questionScore;
                    break;
                }
                ViewBag.quentions2 = ViewQuestionList;
                ViewBag.quizTime = Convert.ToString(DateTime.Now);
            }
            return View(); ;
        }
        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    var token = HttpContext.Session.GetString("Token");
        //    if (string.IsNullOrEmpty(token))
        //    {
        //        return RedirectToAction("Index", "Profile");
        //    }
        //    var data = await quizQuestion.GetQuestion(token);
        //    var result = data.Item1;
        //    var matchToken = data.Item2;

        //    foreach (var items in result)
        //    {
        //        int questionScore = Convert.ToInt32(items.questionScore);
        //        ViewBag.Qmark = questionScore;
        //        break;
        //    }



        //    ViewBag.quentions = result;
        //    ViewBag.matchToken = matchToken;

        //    return View("Index");
        //}
        [HttpGet]
        public async Task<ResponseModel>  CheckAnswer(int questionId, int choiceId)
        {
            var token = HttpContext.Session.GetString("Token");
            var result =await checkAnswer.AnswerStatus(questionId, choiceId, token);
            return result;
        }

        public async Task<IActionResult>  SubmitMatchResult(int Score, int timetoAnswer)
        {
            MatchResultModel resultmodel = new MatchResultModel();
            resultmodel.Score = Score;
            resultmodel.OnDate = DateTime.Now;
            resultmodel.TimeInSeconds = timetoAnswer > 22 ? timetoAnswer - 1 : timetoAnswer;
            var token = HttpContext.Session.GetString("Token");
            var result =await submitResult.PostSubmitResult(resultmodel, token);
            if (result.status == "200")
            {
                var QuizResult = new QuizResult
                {
                    score = Score.ToString(),
                    scoretime = resultmodel.TimeInSeconds.ToString(),
                    bestscore = "0",
                    bestscoretime = "0",
                };

                return RedirectToAction("QuizResult", new RouteValueDictionary(QuizResult));
            }
            else
            {
                ViewBag.msg = "Something went wrong";
                return View("ErrorView");
            }
        }

        [HttpPost]
        public async Task<IActionResult> QuizSubmitQ([FromBody] QuizResultResolve quizResult)
        {
            var token = HttpContext.Session.GetString("Token");

            DateTime quizT = DateTime.Parse(quizResult.quizStartTime);
            DateTime newT = DateTime.Now;

            TimeSpan td = newT - quizT;
            int timeinMili = (int)td.TotalMilliseconds;

            if(timeinMili > 22000)
            {
                timeinMili = 22000;
            }

            int count = 0;
            var trueanswercount = 0;
            var totalScore = 0;

            foreach (var item in quizResult.matchResultChoicePairs)
            {
                if (item.isCorrect == true)
                {
                    count = count +1;
                    trueanswercount = count;

                    totalScore = trueanswercount * 5;
                }
            }

            MatchSummery matchResult = new MatchSummery
            {
                timeInMilliseconds = timeinMili,
                score = totalScore > 25? 25 : totalScore,
                numberOfCorrectChoices = trueanswercount
            };


            var result = await submitResult.PostSubmitResul_NEWwithList(matchResult, token);


            return Ok(matchResult);
        }

        //public IActionResult PoleQuestion()
        //{
        //    List<PredictionModel> aList = new List<PredictionModel>();
        //    using (StreamReader r = new StreamReader(@"JsonData/Prediction.json"))
        //    {
        //        string json = r.ReadToEnd();
        //        ResponseDataCustom<List<PredictionModel>> con = JsonConvert.DeserializeObject<ResponseDataCustom<List<PredictionModel>>>(json);
        //        aList = con.data;

        //        List<PredictionModel> TodaysPrediction = new List<PredictionModel>();

        //       foreach (PredictionModel item in aList)
        //        {
        //            var onlEventDate = item.EventDateTime.ToString("dd MMMM yyyy");
        //            var todayDate = DateTime.Now.ToString("dd MMMM yyyy");
        //            if (onlEventDate == todayDate)
        //            {
        //                TodaysPrediction.Add(item);
        //            }
        //        }
        //        ViewBag.QuestionOptions = TodaysPrediction;
        //    }
        //    return View();
        //}

        public async Task<IActionResult> PoleQuestion()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Profile");
            }

            var predictlistOfTheDay = await prediction.GetPredictionEventList(token);

            if (predictlistOfTheDay == null)
            {
                return RedirectToAction("PoleTime", "QuizQuestion");
            }
            ViewBag.QuestionOptions = predictlistOfTheDay;
            return View();
        }
        public async Task<IActionResult> SubmitPrediction(int PredictedWinningTeamId, int EventId)
        {
            var token = HttpContext.Session.GetString("Token");

            var result =await prediction.PostPrediction(EventId, PredictedWinningTeamId, token);

            return Ok("Success");
        }

        public async Task<IActionResult> QuizResult(int score,int timeInMilliseconds,int numberOfCorrectChoices)
        {
            ViewBag.score = score;
            
            double nbd = Math.Round((double)timeInMilliseconds / 1000,2);
            ViewBag.bstt  = nbd.ToString();
            ViewBag.totalQestion = numberOfCorrectChoices;
            return View();

        }
        public async Task<IActionResult> PoleTime()
        {
            return View();
        }

        public IActionResult NagadPoll()
        {
            List<NagadPollModel> aList = new List<NagadPollModel>();
            using (StreamReader r = new StreamReader(@"JsonData/NagadPoll.json"))
            {
                string json = r.ReadToEnd();
                ResponseDataCustom<List<NagadPollModel>> con = JsonConvert.DeserializeObject<ResponseDataCustom<List<NagadPollModel>>>(json);
                aList = con.data;
                ViewBag.NagadPoles = aList;
            }
            return View();
        }

        //public async Task<IActionResult> NagadPoll()
        //{
        //    var token = HttpContext.Session.GetString("Token");
        //    if (string.IsNullOrEmpty(token))
        //    {
        //        return RedirectToAction("Index", "Profile");
        //    }
        //    List<NagadPollModel> nagadPolls =await nagadPoll.GetPoll(token);
        //    if (nagadPolls == null)
        //    {
        //        return RedirectToAction("PoleTime", "QuizQuestion");
        //    }
        //    ViewBag.NagadPoles = nagadPolls;
        //    return View();
        //}
        public async Task<IActionResult> SubmitNagadPoll(int questionId, int choiceId)
        {
            var token = HttpContext.Session.GetString("Token");
            var pollresult =await nagadPoll.PostPoll(token, questionId, choiceId);
            return Ok("Success");
        }
        public async Task<IActionResult> GotoHome()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercises.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Exercises.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        Dictionary<Question, int> mistakes = new Dictionary<Question, int>();
        Exercise Exercise;
        Question currentQuestion;
        int currentQuestionIndex = -1;
        public TestPage(Exercise exercise)
        {
            InitializeComponent();
            if (exercise.Questions.Length == 0) Navigation.PopAsync();
            Exercise = exercise;
            foreach (Question question in exercise.Questions) mistakes.Add(question, 1);
            NextQuestion();
        }
        public void NextQuestion()
        {
            if (currentQuestionIndex++ > Exercise.Questions.Length-2) currentQuestionIndex = 0;
            Question question = currentQuestionIndex % 2 == 0 && mistakes.Count > 0 ? mistakes.Where(x => x.Value == mistakes.Values.Max()).Last().Key  : Exercise.Questions[currentQuestionIndex];
            currentQuestion = question;
            QuestionLayout.Children.Clear();
            question.ConstructUI(QuestionLayout);
            Button submit = new Button() {Text="Potvrdit", Margin = new Thickness(100,0,100,0)};
            submit.Clicked += Submit_Clicked;
            QuestionLayout.Children.Add(submit);
        }

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            Question question = currentQuestion;
            Other.QuestionResult result = Other.QuestionResult.Mistake;
            if (question.Evaluate())
            {
                mistakes[question] = Math.Max(0, mistakes[question] - 1);
                result = Other.QuestionResult.Correct;
            }
            else
            {
                mistakes[question]++;
            }
            await Navigation.PushAsync(new ResultPage(result));
            if (mistakes.Values.Max() == 0)
                if (await DisplayActionSheet("Dokončili jste toto cvičení blahopřejeme!", null, null, new string[] { "Opakovat cvičení", "Návrat do menu" }) == "Opakovat cvičení")
                {
                    Reload();
                }
                else await Navigation.PopAsync();
            NextQuestion();
            question.UserAnswer = "";
        }
        private void Reload()
        {
            mistakes.Clear();
            foreach (Question question in Exercise.Questions) mistakes.Add(question, 1);
            NextQuestion();
        }
    }
}
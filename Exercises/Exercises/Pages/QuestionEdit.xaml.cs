using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Exercises.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Exercises.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionEdit : ContentPage
    {
        private Question Question;
        private CreateExercise CreateExercise;
        public QuestionEdit(Question question,CreateExercise exercise)
        {
            InitializeComponent();
            Question = question;
            BindingContext = question;
            CreateExercise = exercise;
            ReconstructAnswerUI();
            InitPicker();
        }
        private void InitPicker()
        {
            if (Question is TextQuestion) picker.SelectedIndex = 0;
            if (Question is ABCQuestion) picker.SelectedIndex = 1;
        }
        private void ReconstructAnswerUI()
        {
            answerLayout.Children.Clear();
            Question.ConstructAnswerEditUI(answerLayout);
        }
        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (picker.SelectedIndex)
            {
                case 0:
                    if(!(Question is TextQuestion))
                        Question = new TextQuestion() { Header = headerEntry.Text, Description = descriptionEntry.Text };
                    break;
                case 1:
                    if (!(Question is ABCQuestion))
                        Question = new ABCQuestion() { Header = headerEntry.Text, Description = descriptionEntry.Text };
                    break;
                case 2:
                    Question = new ImgQuestion() { Header = headerEntry.Text, Description = descriptionEntry.Text };
                    break;
            }
            CreateExercise.UpdateQuestionType(Question);
            BindingContext = Question;
            ReconstructAnswerUI();
        }
    }
}
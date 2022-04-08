using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercises.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Exercises.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateExercise : ContentPage
    {
        Exercise Model;
        ObservableCollection<Question> questions = new ObservableCollection<Question>(); 
        public CreateExercise(Exercise exercise)
        {
            InitializeComponent();
            Model = exercise;
            Appearing += CreateExercise_Appearing;
            BindingContext = Model;
            if(Model.Questions != null)
            foreach (Question question in Model.Questions) questions.Add(question);
        }

        public void UpdateQuestionType(Question question)
        {
            questions.Remove(questions.Where(x => x.Header == question.Header && x.Description == question.Description).First());
            questions.Add(question);
        }

        private void CreateExercise_Appearing(object sender, EventArgs e)
        {
            questionsListView.ItemsSource = null;
            questionsListView.ItemsSource = questions;
        }

        private Question GetSelectedQuestion()
        {
            bool b = questionsListView.SelectedItem is ABCQuestion;
            Question question = (Question)questionsListView.SelectedItem;
            if (question == null)
            {
                DisplayAlert("Chyba", "Nejdříve musíte vybrat otázku!", "Ok");
                return null;
            }
            return question;
        }

        private void addButton_Clicked(object sender, EventArgs e)
        {
            questions.Add(new ABCQuestion() {Header=$"Otázka{questions.Count}"});
            questionsListView.ItemsSource = questions;
        }

        private void editButton_Clicked(object sender, EventArgs e)
        {
            Question question = GetSelectedQuestion();
            if (question == null) return;
            Navigation.PushAsync(new QuestionEdit(question,this));
        }

        private void deleteButton_Clicked(object sender, EventArgs e)
        {
            Question question = GetSelectedQuestion();
            if (question == null) return;
            questionsListView.SelectedItem = null;
            questions.Remove(question);
        }
        private bool SaveExercise()
        {
            Model.Questions = questions.ToArray();
            Other.ValidationResult result = Model.Validate();
            if (Model.Validate().IsValid)
            {
                return Exercises.Other.Exercises.SaveExercise(Model);
            }
            DisplayAlert("Pozor", result.Message, "Ok");
            return false;
        }
        private void checkButton_Clicked(object sender, EventArgs e)
        {
            if(SaveExercise())
            Navigation.PopAsync();
        }

        private void exitButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void deleteButton_Clicked_1(object sender, EventArgs e)
        {
            if (await DisplayActionSheet("Upozornění touto akcí nevratně smažete toto cvičení!", "Zrušit", "Beru na vědomí", new string[] {}) == "Zrušit") return;
            Exercises.Other.Exercises.DeleteExercise(Model);
            await Navigation.PopAsync();
        }
    }
}
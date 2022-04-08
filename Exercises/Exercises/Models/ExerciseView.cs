using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace Exercises.Models
{
    public class ExerciseView : Frame
    {
        public Exercise Exercise { get; set; }

        private CheckBox checkBox;
        private MainPage MainPage;

        public ExerciseView(Exercise exercise, MainPage mainPage)
        {
            Exercise = exercise;
            MainPage = mainPage;

            checkBox = new CheckBox() {IsEnabled = false};

            StackLayout stackLayout = new StackLayout();
            Grid horLayout = new Grid();
            horLayout.ColumnDefinitions.Add(new ColumnDefinition());
            horLayout.ColumnDefinitions.Add(new ColumnDefinition() {Width=100});
            horLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = 45 });
            horLayout.Children.Add(new Label() { Text = exercise.Name, FontSize = 24 },0,0);
            horLayout.Children.Add(new Label() { Text = $"Otázek: {exercise.Questions.Length}", VerticalOptions = LayoutOptions.Center },1,0);
            horLayout.Children.Add(checkBox,2,0);

            stackLayout.Children.Add(horLayout);
            stackLayout.Children.Add(new Label() { Text = exercise.Description });

            TapGestureRecognizer tapGesture = new TapGestureRecognizer() {NumberOfTapsRequired=1};
            tapGesture.Tapped += TapGesture_Tapped;
            TapGestureRecognizer tapGesture1 = new TapGestureRecognizer() { NumberOfTapsRequired = 2 };
            tapGesture1.Tapped += TapGesture1_Tapped;


            Margin = new Thickness(8);
            CornerRadius = 8;
            Content = stackLayout;
            GestureRecognizers.Add(tapGesture);
            GestureRecognizers.Add(tapGesture1);
        }
        public void UpdateStatus()
        {
            checkBox.IsChecked = MainPage.selectedExercise == Exercise;
        }
        private void TapGesture1_Tapped(object sender, EventArgs e)
        {
            SelectExercise();
            MainPage.OpenSelectedExercise();
        }
        private void SelectExercise()
        {
            MainPage.selectedExercise = Exercise;
            foreach (ExerciseView view in MainPage.exerciseViews)
            {
                view.UpdateStatus();
            }
        }
        private void TapGesture_Tapped(object sender, EventArgs e)
        {
            SelectExercise();
        }
    }
}

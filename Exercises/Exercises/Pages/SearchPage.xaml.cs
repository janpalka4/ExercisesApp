using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Exercises
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        Models.Exercise[] exercises;
        Dictionary<CheckBox, Models.Exercise> checks = new Dictionary<CheckBox, Models.Exercise>();
        bool Managment = false;
        public SearchPage(bool managment)
        {
            InitializeComponent();
            exercises = Exercises.Other.Exercises.LoadExercises();
            Managment = managment;
            UpdateList();
            if (!managment)
            {
                toolbarDelete.IsEnabled = false;
                toolbarEdit.IsEnabled = false;
            }
        }

        private void searchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void UpdateList()
        {
            PolozkyLayout.Children.Clear();
            checks.Clear();
            Models.Exercise[] _exercises = searchBar.Text == null ? exercises : exercises.Where(x => x.Name.ToLower().Contains(searchBar.Text.ToLower()) || x.Description.ToLower().Contains(searchBar.Text.ToLower())).ToArray();
            foreach (Models.Exercise exercise in _exercises)
            {
                AddExercise(exercise);
            }
        }

        private void AddExercise(Models.Exercise exercise)
        {
            Frame frame = new Frame() { Margin = new Thickness(20), CornerRadius = 8 };
            CheckBox checkBox = new CheckBox() {HorizontalOptions = LayoutOptions.End};
            TapGestureRecognizer gesture = new TapGestureRecognizer();
            if(Managment)gesture.Tapped += delegate { checkBox.IsChecked = !checkBox.IsChecked; };
            if (!Managment) gesture.Tapped += delegate { Navigation.PopAsync(); Navigation.PushAsync(new Pages.TestPage(exercise)); };
            frame.GestureRecognizers.Add(gesture);
            StackLayout stackLayout = new StackLayout();
            if(Managment) stackLayout.Children.Add(checkBox);
            stackLayout.Children.Add(new Label() { Text = exercise.Name, FontSize = 24, Margin = new Thickness(0,Managment ? -40 : 0,0,0) });
            stackLayout.Children.Add(new Label() { Text = exercise.Description });
            frame.Content = stackLayout;
            checks.Add(checkBox, exercise);
            PolozkyLayout.Children.Add(frame);
        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateList();
        }

        private void toolbarEdit_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            Navigation.PushAsync(new Pages.CreateExercise(checks.Where(x => x.Key.IsChecked).First().Value));
        }

        private void toolbarDelete_Clicked(object sender, EventArgs e)
        {
            foreach(Models.Exercise exercise in checks.Where(x => x.Key.IsChecked).Select(x => x.Value))
            {
                Other.Exercises.DeleteExercise(exercise);
            }
            exercises = Other.Exercises.LoadExercises();
            UpdateList();
        }
    }
}
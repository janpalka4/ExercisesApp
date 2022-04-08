using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

using Exercises.Models;

namespace Exercises
{
    public partial class MainPage : ContentPage
    {
        Exercise[] exercises;
        public Exercise selectedExercise;
        public List<ExerciseView> exerciseViews = new List<ExerciseView>();
        public ObservableCollection<UCarouselView> carouselViews { get; set; } = new ObservableCollection<UCarouselView>();
        StackLayout empty;
        public MainPage()
        {
            InitializeComponent();
            empty = emptyLayout;
            Appearing += MainPage_Appearing;
            if (VersionTracking.IsFirstLaunchEver)
            {
                ExercisesLayout.IsEnabled = false;
                grid.RaiseChild(introductionView);
                InitCarousel();
            }
            else grid.Children.Remove(introductionView);
            
            BindingContext = this;
        }

        private void InitCarousel()
        {
            carouselViews.Add(new UCarouselView() { Image = "tut1.gif", Text = "Vítejte v aplikaci :). Než začnete procvičovat, vytvořte si nejdříve cvičení klepnutím na ikonku plus v dolní liště." });
            carouselViews.Add(new UCarouselView() { Image = "tut2.gif", Text = "Cvičení můžete kdykoliv upravit klepnutím na editaci cvičení." });
            carouselViews.Add(new UCarouselView() { Image = "tut3.gif", Text = "Dvojtým klepnutím cvičení spustíte." });
        }

        private void MainPage_Appearing(object sender, EventArgs e)
        {
            exercises = Exercises.Other.Exercises.LoadExercises();
            exerciseViews.Clear();
            ExercisesLayout.Children.Clear();
            if (exercises.Length == 0) ExercisesLayout.Children.Add(empty);
            foreach(Exercise exercise in exercises)
            {
                ExerciseView view = new ExerciseView(exercise,this);
                exerciseViews.Add(view);
                ExercisesLayout.Children.Add(view);
            }
            //Navigation.PushAsync(new Pages.IntroductionPage());
        }

        public void OpenSelectedExercise()
        {
            Navigation.PushAsync(new Exercises.Pages.TestPage(selectedExercise));
        }
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchPage(false));
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MorePage());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Exercises.Pages.CreateExercise(new Exercise()));
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Exercises.Pages.CreateExercise(new Exercise()));
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            if(selectedExercise == null)
            {
                DisplayAlert("Pozor", "Nejdříve musíte vybrat cvičení, které chcete editovat!", "Ok");
                return;
            }
            Navigation.PushAsync(new Exercises.Pages.CreateExercise(selectedExercise));
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            grid.Children.Remove(introductionView);
            ExercisesLayout.IsEnabled = true;
        }
    }
}

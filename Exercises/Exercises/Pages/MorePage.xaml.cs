using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Exercises
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MorePage : ContentPage
    {
        public MorePage()
        {
            InitializeComponent();
            InfoLabel.Text = InfoLabel.Text.Replace("{0}", VersionTracking.CurrentVersion);
            InfoLabel.Text = InfoLabel.Text.Replace("{1}", VersionTracking.CurrentBuild);
        }

        private void addButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            Navigation.PushAsync(new Pages.CreateExercise(new Models.Exercise()));
        }

        private void managmentButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            Navigation.PushAsync(new SearchPage(true));
        }

        private void settingsButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            Navigation.PushAsync(new Pages.SettingsPage());
        }

        private void aboutButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            Navigation.PushAsync(new Pages.aboutPage());
        }
    }
}
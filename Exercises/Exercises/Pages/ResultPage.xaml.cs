using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Exercises.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    {
        public ResultPage(Other.QuestionResult result)
        {
            InitializeComponent();
            Init(result);
        }

        private async void Init(Other.QuestionResult result)
        {
            string sound = "";
            switch (result)
            {
                case Other.QuestionResult.Correct:
                    pageLayout.BackgroundColor = Color.YellowGreen;
                    resultLabel.Text = "Správně!";
                    sound = "OkSound.wav";
                    break;
                case Other.QuestionResult.Mistake:
                    resultLabel.Text = "Špatná odpověd";
                    sound = "WrongSound.wav";
                    pageLayout.BackgroundColor = Color.Red;
                    break;
            }
            if (!Global.Setting.SilentApp)
            {
                var audio = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                audio.Load(Other.ResourceManager.GetResourceStream(sound));
                audio.Volume = 200;
                audio.Play();
            }
            BackgroundColor = pageLayout.BackgroundColor;
            await Task.Delay(1500);
            await Navigation.PopAsync();
        }
    }
}
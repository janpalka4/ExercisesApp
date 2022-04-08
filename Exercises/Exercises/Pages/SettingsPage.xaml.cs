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
    public partial class SettingsPage : ContentPage
    {
        Models.Setting Setting;
        public SettingsPage()
        {
            InitializeComponent();
            Setting = Global.Setting;
            BindingContext = Setting;

            Disappearing += SettingsPage_Disappearing;
        }

        private void SettingsPage_Disappearing(object sender, EventArgs e)
        {
            Other.ResourceManager.SaveAppConfiguration(Setting);
            Global.Setting = Setting;
        }

        private void renewButton_Clicked(object sender, EventArgs e)
        {
            Global.Setting = Models.Setting.GetDefault();
            Other.ResourceManager.SaveAppConfiguration(Global.Setting);
            Setting = Global.Setting;
            BindingContext = Setting;
        }
    }
}
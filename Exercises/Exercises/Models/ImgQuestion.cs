using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Exercises.Models
{
    [Serializable]
    public class ImgQuestion : Question
    {
        public override string Header { get; set; }
        public override string Description { get; set; }
        public override string Answer { get; set; }
        public override string Image { get; set; }
        public override string UserAnswer { get; set; }

        private byte[][] _Questions { get; set; }

        public ImgQuestion()
        {
            Answer = "0,0,0,";
            UserAnswer = Answer;
            _Questions = new byte[3][];
        }
        private string GetAnswerString(bool[] options)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < options.Length; i++)
            {
                stringBuilder.Append(options[i] ? "1," : "0,");
            }
            return stringBuilder.ToString();
        }
        private bool[] GetAnswer()
        {
            string[] s = Answer.Split(',');
            bool[] arr = new bool[s.Length];
            for (int i = 0; i < arr.Length; i++) arr[i] = s[i] == "1";
            return arr;
        }

        private async void LoadImage(Image image, int index)
        {
            System.IO.Stream stream = await DependencyService.Get<IImagePicker>().GetImageStream();
            if (stream != null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                stream.CopyTo(ms);
                _Questions[index] = ms.ToArray();
                System.IO.MemoryStream ms1 = new System.IO.MemoryStream(_Questions[index]);
                image.Source = ImageSource.FromStream(() => ms1);
                stream.Close();
                ms.Close();
                return;
            }
        }

        private ImageSource GetImageFromList(int index, bool editor = false)
        {
            if(_Questions[index] != null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream(_Questions[index]);
                return ImageSource.FromStream(() => ms);
            }
            return editor ? ImageSource.FromResource("ic_folder_open.png") : null;
        }

        public override void ConstructAnswerEditUI(StackLayout view)
        {
            view.Orientation = StackOrientation.Vertical;
            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = 50 });
            grid.RowDefinitions.Add(new RowDefinition() { Height = 50 });
            grid.RowDefinitions.Add(new RowDefinition() { Height = 50 });
            grid.RowDefinitions.Add(new RowDefinition() { Height = 50 });

            Switch a = new Switch() { IsToggled = GetAnswer()[0] };
            Switch b = new Switch() { IsToggled = GetAnswer()[1] };
            Switch c = new Switch() { IsToggled = GetAnswer()[2] };

            a.Toggled += delegate { Answer = GetAnswerString(new bool[] { a.IsToggled, b.IsToggled, c.IsToggled }); };
            b.Toggled += delegate { Answer = GetAnswerString(new bool[] { a.IsToggled, b.IsToggled, c.IsToggled }); };
            c.Toggled += delegate { Answer = GetAnswerString(new bool[] { a.IsToggled, b.IsToggled, c.IsToggled }); };

            Image aImg = new Image() { Source = _Questions[0] == null ? "ic_folder_open.png" : GetImageFromList(0,true)};
            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += delegate { LoadImage(aImg,0); };
            aImg.GestureRecognizers.Add(tapGesture);
            Image bImg = new Image() { Source = _Questions[1] == null ? "ic_folder_open.png" : GetImageFromList(1, true) };
            TapGestureRecognizer tapGesture1 = new TapGestureRecognizer();
            tapGesture1.Tapped += delegate { LoadImage(bImg,1); };
            bImg.GestureRecognizers.Add(tapGesture1);
            Image cImg = new Image() { Source = _Questions[2] == null ? "ic_folder_open.png" : GetImageFromList(2, true) };
            TapGestureRecognizer tapGesture2 = new TapGestureRecognizer();
            tapGesture2.Tapped += delegate { LoadImage(cImg,2); };
            cImg.GestureRecognizers.Add(tapGesture2);

            grid.Children.Add(aImg, 0, 0);
            grid.Children.Add(a, 1, 0);

            grid.Children.Add(bImg, 0, 1);
            grid.Children.Add(b, 1, 1);

            grid.Children.Add(cImg, 0, 2);
            grid.Children.Add(c, 1, 2);

            view.Children.Add(grid);
        }

        public override bool Evaluate()
        {
            return UserAnswer == Answer;
        }

        protected override void ConstructAnswerUI(StackLayout view)
        {
            Switch a = new Switch() { IsToggled = false };
            Switch b = new Switch() { IsToggled = false };
            Switch c = new Switch() { IsToggled = false };

            a.Toggled += delegate { UserAnswer = GetAnswerString(new bool[] { a.IsToggled, b.IsToggled, c.IsToggled }); };
            b.Toggled += delegate { UserAnswer = GetAnswerString(new bool[] { a.IsToggled, b.IsToggled, c.IsToggled }); };
            c.Toggled += delegate { UserAnswer = GetAnswerString(new bool[] { a.IsToggled, b.IsToggled, c.IsToggled }); };

            Image aImg = new Image() { Source = GetImageFromList(0) };

            Image bImg = new Image()
            {
                Source = GetImageFromList(1)
            };

            Image cImg = new Image()
            {
                Source = GetImageFromList(2)
            };

            StackLayout aLayout = new StackLayout() { Orientation = StackOrientation.Vertical };
            aLayout.Children.Add(aImg);
            aLayout.Children.Add(a);
            StackLayout bLayout = new StackLayout() { Orientation = StackOrientation.Vertical };
            aLayout.Children.Add(bImg);
            aLayout.Children.Add(b);
            StackLayout cLayout = new StackLayout() { Orientation = StackOrientation.Vertical };
            aLayout.Children.Add(cImg);
            aLayout.Children.Add(c);

            view.Children.Add(aLayout);
            view.Children.Add(bLayout);
            view.Children.Add(cLayout);

            view.Orientation = StackOrientation.Vertical;
        }
    }
}

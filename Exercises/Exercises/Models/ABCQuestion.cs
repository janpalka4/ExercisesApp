using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Exercises.Models
{
    [Serializable]
    public class ABCQuestion : Question
    {
        public override string Header { get; set; }
        public override string Description { get; set; }
        public override string Answer { get; set; }
        public override string Image { get; set; }
        private string[] _Questions { get; set; }

        public override string UserAnswer { get; set; }

        public ABCQuestion() : base()
        {
            _Questions = new string[3];
            Answer = "0,0,0,";
            UserAnswer = Answer;
        }
        private string GetAnswerString(bool[] options)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for(int i = 0; i < options.Length; i++)
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

            Entry aEntry = new Entry() { Placeholder = "Sem zadejte odpoved A", Text = _Questions[0]};
            aEntry.TextChanged += delegate { _Questions[0] = aEntry.Text; };
            Entry bEntry = new Entry() { Placeholder = "Sem zadejte odpoved B", Text = _Questions[1] };
            bEntry.TextChanged += delegate { _Questions[1] = bEntry.Text; };
            Entry cEntry = new Entry() { Placeholder = "Sem zadejte odpoved C", Text = _Questions[2]};
            cEntry.TextChanged += delegate { _Questions[2] = cEntry.Text; };

            grid.Children.Add(aEntry, 0, 0);
            grid.Children.Add(a, 1, 0);

            grid.Children.Add(bEntry, 0, 1);
            grid.Children.Add(b, 1, 1);

            grid.Children.Add(cEntry, 0, 2);
            grid.Children.Add(c, 1, 2);

            view.Children.Add(grid);

            
        }
        protected override void ConstructAnswerUI(StackLayout view)
        {
            Switch a = new Switch() { IsToggled = false };
            Switch b = new Switch() { IsToggled = false };
            Switch c = new Switch() { IsToggled = false };

            a.Toggled += delegate { UserAnswer = GetAnswerString(new bool[] { a.IsToggled, b.IsToggled, c.IsToggled }); };
            b.Toggled += delegate { UserAnswer = GetAnswerString(new bool[] { a.IsToggled, b.IsToggled, c.IsToggled }); };
            c.Toggled += delegate { UserAnswer = GetAnswerString(new bool[] { a.IsToggled, b.IsToggled, c.IsToggled }); };

            Label aEntry = new Label() { Text = _Questions[0] };
            Label bEntry = new Label() { Text = _Questions[1] };
            Label cEntry = new Label() { Text = _Questions[2] };

            StackLayout aLayout = new StackLayout() { Orientation = StackOrientation.Horizontal };
            aLayout.Children.Add(aEntry);
            aLayout.Children.Add(a);
            StackLayout bLayout = new StackLayout() { Orientation = StackOrientation.Horizontal };
            aLayout.Children.Add(bEntry);
            aLayout.Children.Add(b);
            StackLayout cLayout = new StackLayout() { Orientation = StackOrientation.Horizontal };
            aLayout.Children.Add(cEntry);
            aLayout.Children.Add(c);

            view.Children.Add(aLayout);
            view.Children.Add(bLayout);
            view.Children.Add(cLayout);

            view.Orientation = StackOrientation.Vertical;
        }

        public override bool Evaluate()
        {
            return UserAnswer == Answer;
        }
    }
}

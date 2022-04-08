using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Exercises.Models
{
    [Serializable]
    public class TextQuestion : Question
    {
        public override string Header { get; set; }
        public override string Description { get; set; }
        public override string Answer { get; set; }
        public override string Image { get; set; }

        public override string UserAnswer { get; set; }

        public TextQuestion()
        {
            Answer = "";
        }

        protected override void ConstructAnswerUI(StackLayout view)
        {
            Entry entry = new Entry() { Placeholder = "Vaše odpoved" };
            entry.TextChanged += delegate { UserAnswer = entry.Text; };
            view.Children.Add(entry);
        }
        public override void ConstructAnswerEditUI(StackLayout view)
        {
            Entry entry = new Entry() { Placeholder = "Sem zadejte odpoved.", Text = Answer };
            entry.TextChanged += delegate { Answer = entry.Text; };
            view.Children.Add(entry);
        }

        public override bool Evaluate()
        {
            return UserAnswer == Answer;
        }
    }
}

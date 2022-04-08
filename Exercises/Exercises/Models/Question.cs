using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Xamarin.Forms;

namespace Exercises.Models
{
    [Serializable]
    public abstract class Question
    {
        public abstract string Header { get; set; }
        public abstract string Description { get; set; }
        public abstract string Answer { get; set; }
        public abstract string Image { get; set; }

        public abstract string UserAnswer { get; set; }

        public Question()
        {

        }

        protected void ConstructDescUI(StackLayout view)
        {
            Label header = new Label() {Text=Header};
            header.FontSize = Device.GetNamedSize(NamedSize.Title, typeof(Label));
            Label desc = new Label();
            desc.Text = Description;

            view.Children.Add(header);
            view.Children.Add(desc);
        }
        public void ConstructUI(StackLayout view)
        {
            ConstructDescUI(view);
            ConstructAnswerUI(view);
        }
        protected abstract void ConstructAnswerUI(StackLayout view);
        public abstract void ConstructAnswerEditUI(StackLayout view);

        public abstract bool Evaluate();
    }
}

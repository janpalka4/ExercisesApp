using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using Xamarin.Forms;
using Exercises.Other;

namespace Exercises.Models
{
    [Serializable]
    public class Exercise
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Question[] Questions { get; set; }

        public Exercise()
        {

        }

        public Exercise(string Name, string Description, Question[] questions)
        {
            this.Name = Name;
            this.Description = Description;
            this.Questions = questions;
        }

        public ValidationResult Validate()
        {
            ValidationResult result = new ValidationResult() {Message="",IsValid = true};
            Regex nameValidation = new Regex(@"([A-Za-z0-9_?-])\w+");
            if(Name == null)
            {
                result.Message += "*Název cvičení nesmí být prázdný! \n";
                result.IsValid = false;
            }
            else
            if (!nameValidation.Match(Name).Success)
            {
                result.Message = "*Název cvičení není validní! \n";
                result.IsValid = false;
            }
            if (Questions.Length == 0)
            {
                result.Message += "*Cvičení musí obsahovat alespon jednu otázku! \n";
                result.IsValid = false;
            }
            return result;
        }
    }
}

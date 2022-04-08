using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Exercises.Models;
using Xamarin.Forms;

namespace Exercises.Other
{
    public static class Exercises
    {
        public static string ExercisesPath { get; set; }

        static Exercises()
        {
            ExercisesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"Exercises");
            if (!Directory.Exists(ExercisesPath)) Directory.CreateDirectory(ExercisesPath);
        }
        public static bool SaveExercise(Exercise exercise)
        {
            try
            {
                string _path = Path.Combine(ExercisesPath, $"{exercise.Name}.bin");
                //if (File.Exists(_path))  return false;
                Stream stream = File.Create(_path);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, exercise);
                stream.Close();
            }
            catch (IOException ex) {
                
            }
            return true;
        }
        public static void DeleteExercise(Exercise exercise)
        {
            string _path = Path.Combine(ExercisesPath, $"{exercise.Name}.bin");
            if (!File.Exists(_path)) return;
            File.Delete(_path);
        }
        public static Exercise[] LoadExercises()
        {
            List<Exercise> exercises = new List<Exercise>();
            string[] paths = Directory.GetFiles(ExercisesPath);
            foreach (string path in paths)
            {
                try
                {
                    Stream stream = File.OpenRead(path);
                    BinaryFormatter formatter = new BinaryFormatter();
                    Exercise ex = (Exercise)formatter.Deserialize(stream);
                    if (ex != null) exercises.Add(ex);
                    stream.Close();
                }catch(SerializationException exception)
                {

                }
            }
            return exercises.ToArray();
        }
    }
    public struct ValidationResult
    {
        public string Message;
        public bool IsValid;
    }
}

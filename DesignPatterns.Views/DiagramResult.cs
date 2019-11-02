using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DesignPatterns;
using DesignPatterns.Views;

namespace DesignPatterns.Views
{
    public class DiagramResult:AbstractResult
    {
        public DiagramResult() { }

        public DiagramResult(PatternView pattern)
        {
            Pattern = pattern;
        }

        public DiagramResult(string message)
        {
            Message = message;

            IsModelValid = false;
        }
        
        public Dictionary<string, double> ComparationResult { get; private set; } = new Dictionary<string, double>()
        {
            {"SubjectComparation",  0},
            {"SubjectMethodComparation",  0},
            {"SubjectPropertyComparation",  0},
            {"SubjectReferenceComparation",  0},
            {"MethodParameterComparation",  0},
        };

        public Dictionary<string, double> ExtraErrors { get; private set; } = new Dictionary<string, double>()
        {
            {"SubjectComparation",  0},
            {"SubjectMethodComparation",  0},
            {"SubjectPropertyComparation",  0},
            {"SubjectReferenceComparation",  0},
            {"MethodParameterComparation",  0},
        };

        public List<string> ErrorMessages { get; set; } = new List<string>();

        private string _GetKey<T>() where T : IDiagramElement
        {
            var instance = Activator.CreateInstance<T>();

            var key = instance.GetType().Name;

            key = key.Replace("View", "Comparation");

            return key;
        }
        
        public void AddComparationSuccess<T>(double percentage) where T : IDiagramElement
        {
            var key = _GetKey<T>();

            ComparationResult[key] += percentage;
        }

        public void AddExtraErrorPercentage<T>(double percentage) where T : IDiagramElement
        {
            var key = _GetKey<T>();

            ExtraErrors[key] += percentage;
        }

        public void AddError<T>(string message) where T : IDiagramElement
        {
            var key = _GetKey<T>();

            ErrorMessages.Add(key + ": " + message);
        }
        
        public void Finish(Difficulty difficulty)
        {
            if(difficulty == Difficulty.Hard)
            {
                Percentage = (int)(ComparationResult.Values.Average() - ExtraErrors.Values.Average());
            }
            else if(difficulty == Difficulty.Medium)
            {
                double truePercent = (ComparationResult["SubjectComparation"] +
                    ComparationResult["SubjectReferenceComparation"]) / 2;

                double errorPercent = (ExtraErrors["SubjectComparation"] +
                    ExtraErrors["SubjectReferenceComparation"]) / 2;

                Percentage = (int)(truePercent - errorPercent);
            }
            else
            {
                throw new Exception("Easy not for diagrams");
            }
            
            Mark = Percentage / 20;

            IsModelValid = true;

            Message = "Ok";
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append("------------------------\nDiagram result: \nPattern: " + Pattern.Name + "\n");

            result.Append("Message: " + Message + "\nIsModelValid: " + IsModelValid + "\n");

            result.Append("Percentage: " + Percentage + "\nMark: " + Mark + "\n");

            result.Append("Comparation result:\n");

            foreach(var item in ComparationResult)
            {
                result.Append(item.Key + " : " + item.Value + "\n");
            }

            result.Append("Extra Errors:\n");

            foreach(var item in ExtraErrors)
            {
                result.Append(item.Key + " : " + item.Value + "\n");
            }

            result.Append("Error messages:\n");

            foreach(var item in ErrorMessages)
            {
                result.Append(item + "\n");
            }

            result.Append("------------------------\n");

            return result.ToString();
        }
    }
}
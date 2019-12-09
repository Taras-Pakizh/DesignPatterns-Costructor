using DesignPatterns.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Client.View
{
    public static class IdGenerator
    {
        private static int _subjectId = 11;
        private static int _propertyId = 0;
        private static int _methodId = 0;
        private static int _parameterId = 0;
        private static int _referenceId = 0;
        private static int _questionId = 0;
        private static int _answerId = 0;

        public static int GetId(IdTypes type)
        {
            switch (type)
            {
                case IdTypes.Answer:
                    return _answerId++;
                case IdTypes.Method:
                    return _methodId++;
                case IdTypes.Parameter:
                    return _parameterId++;
                case IdTypes.Property:
                    return _propertyId++;
                case IdTypes.Question:
                    return _questionId++;
                case IdTypes.Reference:
                    return _referenceId++;
                case IdTypes.Subject:
                    return _subjectId++;
            }

            throw new Exception("You have broke IdGenerator. I have no idea how");
        }
    }
    
    public enum IdTypes
    {
        Subject,
        Reference,
        Property,
        Method,
        Parameter,
        Question,
        Answer
    }
}

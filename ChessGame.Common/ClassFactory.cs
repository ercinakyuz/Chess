using System;
using System.Reflection;

namespace ChessGame.Common
{
    public static class ClassFactory
    {
        public static dynamic CreateClass(this string className)
        {
            string nameSpace = "Business.Piece";
            var assembly = Assembly.GetCallingAssembly();
            var type = assembly.GetType($"{nameSpace}.{className}");
            return Activator.CreateInstance(type).ConvertToType(type);
        }
    }
}

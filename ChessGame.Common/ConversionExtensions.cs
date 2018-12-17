using System;

namespace ChessGame.Common
{
    public static class ConversionExtensions
    {
        public static dynamic ConvertToType(this object source, Type dest)
        {
            return Convert.ChangeType(source, dest);
        }
    }
}

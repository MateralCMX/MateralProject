using Materal.Common;
using System;

namespace MateralProject.Core
{
    public class MateralProjectException : MateralException
    {
        public MateralProjectException()
        {
        }

        public MateralProjectException(string message) : base(message)
        {
        }

        public MateralProjectException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

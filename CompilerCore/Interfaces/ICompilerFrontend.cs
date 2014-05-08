using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompilerCore.Interfaces
{
    public interface ICompilerFrontend
    {
        void Go(string outputPath = null);
    }
}

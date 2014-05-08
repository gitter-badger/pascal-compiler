using CompilerCore.Impl;

namespace CompilerCore
{
    public static class Factory
    {
        public static IScanner ScannerFor(string path)
        {
            return new ScannerImpl(path);
        }

        public static ISymbolTable SymbolTable()
        {
            return new SymbolTableLinkedImpl();
        }

        public static ICompilerFrontend FrontendFor(string path)
        {
            return new CompilerFrontendImpl(path);
        }
    }
}

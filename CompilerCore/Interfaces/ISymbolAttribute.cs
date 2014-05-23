using System.Security.Cryptography.X509Certificates;

namespace CompilerCore
{
    public interface ISymbolAttribute
    {
        TokenType TokenType { get; set; }

        SemanticType SemanticType { get; set; }

        DataType DataType { get; set; }

        int IntValue { get; set; }

        double DoubleValue { get; set; }

        ISymbolAttribute ParentAttribute { get; }
    }
}

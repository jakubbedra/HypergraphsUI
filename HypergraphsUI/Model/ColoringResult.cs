using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Model;

public class ColoringResult
{
    public GeneratorType HypergraphType { get; set; }
    public HypergraphSize Size { get; set; }
    public int Delta { get; set; }
    public int ChromaticNumber { get; set; }
    public int Iterations { get; set; }
    public long ExecutionTime { get; set; }
}
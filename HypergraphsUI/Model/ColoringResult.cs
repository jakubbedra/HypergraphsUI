using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Model;

public class ColoringResult
{
    public Algorithm Algorithm { get; set; }
    public GeneratorType HypergraphType { get; set; }
    public string Size { get; set; }
    public double AvgUsedColors { get; set; }
    public int MinUsedColors { get; set; }
    public int MaxUsedColors { get; set; }
    public int DistinctHypergraphs { get; set; }
    public long AvgExecutionTime { get; set; }
    public long MinExecutionTime { get; set; }
    public long MaxExecutionTime { get; set; }
    public long AvgExecutionTimeTicks { get; set; }
    public long MinExecutionTimeTicks { get; set; }
    public long MaxExecutionTimeTicks { get; set; }
}
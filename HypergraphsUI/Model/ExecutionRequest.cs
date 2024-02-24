using System.Collections.Generic;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Model;

public class ExecutionRequest//todo: validator
{
    
    public int IterationCount { get; set; }
    public List<Algorithm> ChosenAlgorithms { get; set; }
    public List<GeneratorType> GeneratorTypes { get; set; }
    public List<HypergraphSize> HypergraphSizes { get; set; }

}
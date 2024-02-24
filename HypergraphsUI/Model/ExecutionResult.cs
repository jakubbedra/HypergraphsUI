using System.Collections.Generic;

namespace HypergraphsUI.Model;

public class ExecutionResult
{
    public int SuccessfulColorings { get; set; }
    public List<ColoringResult> Results { get; set; }
}
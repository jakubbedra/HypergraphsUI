using System.Collections.Generic;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Model;

public class HypergraphRequest
{
    public GeneratorType GeneratorType { get; set; }
    public List<string> Sizes { get; set; }
}
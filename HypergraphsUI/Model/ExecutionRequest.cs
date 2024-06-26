﻿using System.Collections.Generic;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Model;

public class ExecutionRequest
{
    public int HypergraphsCount { get; set; }
    public List<Algorithm> ChosenAlgorithms { get; set; }
    public List<HypergraphRequest> Hypergraphs { get; set; }
}
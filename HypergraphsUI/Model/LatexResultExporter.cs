using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HypergraphsUI.ViewModel;

namespace HypergraphsUI.Model;

public class LatexResultExporter
{
    private static string Path = @"C:\Users\theKonfyrm\Desktop\results\";
    
    public void Export(ExecutionResult result)
    {
        string size = "papaj2137";
        List<string> rows = new List<string>();
        rows.Add(result.Results.First().HypergraphType + "\n \n");
        string hypergraphType = result.Results.First().HypergraphType + "";
        foreach (ColoringResult coloringResult in result.Results)
        {
            if (!coloringResult.Size.Equals(size))
            {
                size = coloringResult.Size;
                rows.Add($"\n \n \n  Rozmiar: {size}");
            }
            rows.Add($"\\hline {GetAlgoName(coloringResult.Algorithm)} & {coloringResult.AvgUsedColors} & " +
                     $"{coloringResult.MinUsedColors} & {coloringResult.MaxUsedColors} & {coloringResult.AvgExecutionTimeTicks} " +
                     $"& {coloringResult.MinExecutionTimeTicks} & {coloringResult.MaxExecutionTimeTicks}"+ "\\\\" + "\n");
        }

        string file = string.Join("\n", rows);
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string path = $"{Path}{hypergraphType}_results_{timestamp}.txt";
        File.WriteAllText(path, file);
    }

    private string GetAlgoName(Algorithm algorithm)
    {
        switch (algorithm)
        {
            case Algorithm.CUDABruteForce:
                return "Algorytm \\newline siłowy";
            case Algorithm.HyperstarColoring:
                return "Kolorowanie \\newline hipergwiazd";
            case Algorithm.HypertreeColoring:
                return "Kolorowanie \\newline hiperdrzew";
            case Algorithm.HyperpathColoring:
                return "Kolorowanie \\newline hiperścieżek";
            case Algorithm.HypercycleColoring:
                return "Kolorowanie \\newline hipercykli";
            case Algorithm.ClassicGreedy:
                return "Klasyczny \\newline zachłanny";
            case Algorithm.BaseGreedy:
                return "Usuwanie \\newline monochromatyczności";
            case Algorithm.OtherGreedy:
                return "Drugi wariant \\newline zachłannego";
            case Algorithm.DSatur:
                return "DSatur";
            case Algorithm.LargestFirst:
                return "LF";
            case Algorithm.LargestFirstNeighbours:
                return "LF \\newline (liczba sąsiadów)";
            case Algorithm.LargestFirstEdgeSizes:
                return "LF \\newline (rozmiar krawędzi)";
            case Algorithm.Lovasz3Uniform:
                return "Lovasz dla \\newline $3$-równomiernych";
            case Algorithm.LovaszUniform:
                return "Lovasz dla \\newline równomiernych";
            case Algorithm.NestedMonteCarloSearch:
                return "NMCS";
            case Algorithm.NestedRolloutPolicyAdaptation:
                return "NRPA";
        }

        return "";
    }

    private string GetPath(GeneratorType type)
    {
        switch (type)
        {
            
        }

        return "";
    }
}
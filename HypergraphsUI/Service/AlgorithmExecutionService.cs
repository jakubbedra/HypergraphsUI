﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Hypergraphs.Model;
using HypergraphsUI.Algorithms;
using HypergraphsUI.Algorithms.Exact;
using HypergraphsUI.Algorithms.Heuristics;
using HypergraphsUI.Generators;
using HypergraphsUI.Model;
using HypergraphsUI.ViewModel;

public class AlgorithmExecutionService
{
    private Dictionary<GeneratorType, BaseGenerator> _generators;
    private Dictionary<Algorithm, BaseAlgorithm> _algorithms;

    public AlgorithmExecutionService()
    {
        _generators = new Dictionary<GeneratorType, BaseGenerator>();
        _generators.Add(GeneratorType.Random, new RandomConnectedHypergraphGenerator());
        _generators.Add(GeneratorType.Hyperstar, new HyperstarGenerator());
        _generators.Add(GeneratorType.Hypertree, new HypertreeGenerator());
        _generators.Add(GeneratorType.Hyperpath, new HyperpathGenerator());
        _generators.Add(GeneratorType.Hypercycle, new HypercycleGenerator());
        _generators.Add(GeneratorType.Uniform, new UniformHypergraphGenerator());
        _generators.Add(GeneratorType.ThreeUniform, new ThreeUniformHypergraphGenerator());

        _algorithms = new Dictionary<Algorithm, BaseAlgorithm>();
        _algorithms.Add(Algorithm.BaseGreedy, new BaseGreedy());
        _algorithms.Add(Algorithm.DSatur, new DSatur());
        _algorithms.Add(Algorithm.VertexDegreeGreedy, new VertexDegreeGreedy());
        _algorithms.Add(Algorithm.HyperstarColoring, new HyperstarColoringAdapter());
        _algorithms.Add(Algorithm.HypertreeColoring, new HypertreeColoringAdapter());
        _algorithms.Add(Algorithm.HyperpathColoring, new HyperpathColoringAdapter());
        _algorithms.Add(Algorithm.HypercycleColoring, new HypercycleColoringAdapter());
        _algorithms.Add(Algorithm.CUDABruteForce, new BruteForceVariationColoring());
        _algorithms.Add(Algorithm.Lovasz3Uniform, new Lovasz3Uniform());
        _algorithms.Add(Algorithm.LovaszUniform, new LovaszUniform());
        _algorithms.Add(Algorithm.NestedMonteCarloSearch, new NestedMonteCarloSearch());
        _algorithms.Add(Algorithm.NestedRolloutPolicyAdaptation, new NestedRolloutPolicyAdaptation());
    }

    public Task<ExecutionResult> Execute(ExecutionRequest request, IProgress<double> progress)
    {
        List<ColoringResult> results = new List<ColoringResult>();

        int doneSamples = 0;
        int totalSamples = request.Hypergraphs
            .Select(h => h.Sizes.Count * request.HypergraphsCount * request.ChosenAlgorithms.Count)
            .Sum();
        foreach (HypergraphRequest hypergraphRequest in request.Hypergraphs)
        {
            foreach (string size in hypergraphRequest.Sizes)// todo: avg used colors should be double?
            {
                Dictionary<Algorithm, long> avgExecutionTimes = new Dictionary<Algorithm, long>();
                Dictionary<Algorithm, long> minExecutionTimes = new Dictionary<Algorithm, long>();
                Dictionary<Algorithm, long> maxExecutionTimes = new Dictionary<Algorithm, long>();
                Dictionary<Algorithm, double> avgUsedColors = new Dictionary<Algorithm, double>();
                Dictionary<Algorithm, int> minUsedColors = new Dictionary<Algorithm, int>();
                Dictionary<Algorithm, int> maxUsedColors = new Dictionary<Algorithm, int>();
                for (int i = 0; i < request.HypergraphsCount; i++)
                {
                    Hypergraph hypergraph = GenerateHypergraph(size.Split(";").Select(s => int.Parse(s)).ToArray(), hypergraphRequest.GeneratorType);
                    foreach (Algorithm algorithm in request.ChosenAlgorithms)
                    {
                        avgExecutionTimes.TryAdd(algorithm, 0);
                        minExecutionTimes.TryAdd(algorithm, long.MaxValue);
                        maxExecutionTimes.TryAdd(algorithm, 0);
                        avgUsedColors.TryAdd(algorithm, 0);
                        minUsedColors.TryAdd(algorithm, int.MaxValue);
                        maxUsedColors.TryAdd(algorithm, 0);
                        SimpleColoringResult coloringResult = ExecuteHypergraphColoring(hypergraph, algorithm, request.IterationCount);
                        avgExecutionTimes[algorithm] += coloringResult.Time;
                        minExecutionTimes[algorithm] = Math.Min(minExecutionTimes[algorithm], coloringResult.Time);
                        maxExecutionTimes[algorithm] = Math.Max(maxExecutionTimes[algorithm], coloringResult.Time);
                        
                        avgUsedColors[algorithm] += coloringResult.UsedColors;
                        minUsedColors[algorithm] = Math.Min(minUsedColors[algorithm], coloringResult.UsedColors);
                        maxUsedColors[algorithm] = Math.Max(maxUsedColors[algorithm], coloringResult.UsedColors);
                        doneSamples++;
                        progress.Report((double) doneSamples * 100.0 / (double) totalSamples);
                    }
                }
                foreach (Algorithm algorithm in request.ChosenAlgorithms)
                {
                    ColoringResult result = new ColoringResult()
                    {
                        Algorithm = algorithm,
                        HypergraphType = hypergraphRequest.GeneratorType,
                        AvgExecutionTime = avgExecutionTimes[algorithm] / request.HypergraphsCount,
                        MinExecutionTime = minExecutionTimes[algorithm],
                        MaxExecutionTime = maxExecutionTimes[algorithm],
                        DistinctHypergraphs = request.HypergraphsCount,
                        Iterations = request.IterationCount,
                        Size = size,
                        AvgUsedColors = avgUsedColors[algorithm] / (double)request.HypergraphsCount,
                        MinUsedColors = minUsedColors[algorithm],
                        MaxUsedColors = maxUsedColors[algorithm]
                    };
                    results.Add(result);
                }
            }

            // return Task.FromResult(new ExecutionResult()
            // {
            //     SuccessfulColorings = 0,
            //     Results = results
            // });
        }

        return Task.FromResult(new ExecutionResult()
        {
            SuccessfulColorings = results.Count,
            Results = results
        });
    }

    public bool IsAlgorithmCompatible(Algorithm algorithm, List<GeneratorType> generators)
    {
        return generators.All(generator => _algorithms[algorithm].GetAllowedGeneratorTypes().Contains(generator));
    }

    public bool IsGeneratorCompatible(GeneratorType generator, List<Algorithm> algorithms)
    {
        return algorithms.All(algorithm => _algorithms[algorithm].GetAllowedGeneratorTypes().Contains(generator));
    }
    
    private Hypergraph GenerateHypergraph(int[] size, GeneratorType generatorType)
    {
        BaseGenerator generator = _generators[generatorType];
        if (size.Length < 2) throw new ArgumentException("Invalid size.");
        if (size.Length == 3)
        {
            return generator.Generate(size[0], size[1], size[2]);
        }

        return generator.Generate(size[0], size[1]);
    }

    private SimpleColoringResult ExecuteHypergraphColoring(Hypergraph hypergraph, Algorithm algorithm, int iterations)
    {
        BaseAlgorithm coloringAlgorithm = _algorithms[algorithm];
        int[] colors = new int [0];
        int tmp = 0;
        
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        if (!IsMonteCarlo(algorithm) && !IsBruteForce(algorithm))
        {
            for (int i = 0; i < iterations; i++)
            {
                tmp = i;
                colors = coloringAlgorithm.ComputeColoring(hypergraph);
            }
        }
        else
        {
            try
            {
                colors = coloringAlgorithm.ComputeColoring(hypergraph);
            }
            catch (Exception)
            {
                // ignored
            }
        }
        stopwatch.Stop();
        var executionMillis = stopwatch.ElapsedMilliseconds;
        
        Stopwatch stopwatchLoop = new Stopwatch();
        stopwatchLoop.Start();
        if (!IsMonteCarlo(algorithm) && !IsBruteForce(algorithm))
        {
            for (int i = 0; i < iterations; i++)
            {
                tmp = i;
            }
        }
        stopwatchLoop.Stop();
        long loopMillis = stopwatchLoop.ElapsedMilliseconds;
        
        return new SimpleColoringResult()
        {
            UsedColors = colors.Distinct().Count(),
            Time = ((executionMillis - loopMillis) / (IsMonteCarlo(algorithm) || IsBruteForce(algorithm) ? 1 : iterations))// todo: if monte-carlo -> iteration = 0
        };
    }

    private bool IsBruteForce(Algorithm algorithm)
    {
        return algorithm == Algorithm.CUDABruteForce;
    }

    private bool IsMonteCarlo(Algorithm algorithm)
    {
        return algorithm == Algorithm.NestedMonteCarloSearch || algorithm == Algorithm.NestedRolloutPolicyAdaptation;
    }
    
}
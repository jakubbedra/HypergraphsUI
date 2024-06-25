using System;
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
        _generators.Add(GeneratorType.ThreeColorableHypercycle, new ThreeColorableHypercycleGenerator());
        _generators.Add(GeneratorType.ThreeUniform, new ThreeUniformHypergraphGenerator());
        _generators.Add(GeneratorType.Uniform, new UniformHypergraphGenerator());

        _algorithms = new Dictionary<Algorithm, BaseAlgorithm>();
        _algorithms.Add(Algorithm.ClassicGreedy, new ClassicGreedy());
        _algorithms.Add(Algorithm.BaseGreedy, new BaseGreedy());
        _algorithms.Add(Algorithm.OtherGreedy, new OtherGreedy());
        _algorithms.Add(Algorithm.DSatur, new DSatur());
        _algorithms.Add(Algorithm.LargestFirst, new LargestFirst());
        _algorithms.Add(Algorithm.LargestFirstNeighbours, new LargestFirstNeighbours());
        _algorithms.Add(Algorithm.LargestFirstEdgeSizes, new LargestFirstEdgeSizes());
        _algorithms.Add(Algorithm.HyperstarColoring, new HyperstarColoringAdapter());
        _algorithms.Add(Algorithm.HypertreeColoring, new HypertreeColoringAdapter());
        _algorithms.Add(Algorithm.HyperpathColoring, new HyperpathColoringAdapter());
        _algorithms.Add(Algorithm.HyperpathGreedyColoring, new HyperpathGreedyColoringAdapter());
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
            foreach (string size in hypergraphRequest.Sizes) 
            {
                Dictionary<Algorithm, long> avgExecutionTimesMillis = new Dictionary<Algorithm, long>();
                Dictionary<Algorithm, long> minExecutionTimesMillis = new Dictionary<Algorithm, long>();
                Dictionary<Algorithm, long> maxExecutionTimesMillis = new Dictionary<Algorithm, long>();
                Dictionary<Algorithm, long> avgExecutionTimesTicks = new Dictionary<Algorithm, long>();
                Dictionary<Algorithm, long> minExecutionTimesTicks = new Dictionary<Algorithm, long>();
                Dictionary<Algorithm, long> maxExecutionTimesTicks = new Dictionary<Algorithm, long>();
                Dictionary<Algorithm, double> avgUsedColors = new Dictionary<Algorithm, double>();
                Dictionary<Algorithm, int> minUsedColors = new Dictionary<Algorithm, int>();
                Dictionary<Algorithm, int> maxUsedColors = new Dictionary<Algorithm, int>();
                for (int i = 0; i < request.HypergraphsCount; i++)
                {
                    Hypergraph hypergraph = GenerateHypergraph(size.Split(";").Select(s => int.Parse(s)).ToArray(),
                        hypergraphRequest.GeneratorType);
                    foreach (Algorithm algorithm in request.ChosenAlgorithms)
                    {
                        avgExecutionTimesMillis.TryAdd(algorithm, 0);
                        minExecutionTimesMillis.TryAdd(algorithm, long.MaxValue);
                        maxExecutionTimesMillis.TryAdd(algorithm, 0);
                        avgExecutionTimesTicks.TryAdd(algorithm, 0);
                        minExecutionTimesTicks.TryAdd(algorithm, long.MaxValue);
                        maxExecutionTimesTicks.TryAdd(algorithm, 0);
                        avgUsedColors.TryAdd(algorithm, 0);
                        minUsedColors.TryAdd(algorithm, int.MaxValue);
                        maxUsedColors.TryAdd(algorithm, 0);
                        SimpleColoringResult coloringResult = ExecuteHypergraphColoring(hypergraph, algorithm);
                        avgExecutionTimesMillis[algorithm] += coloringResult.TimeMillis;
                        minExecutionTimesMillis[algorithm] = Math.Min(minExecutionTimesMillis[algorithm], coloringResult.TimeMillis);
                        maxExecutionTimesMillis[algorithm] = Math.Max(maxExecutionTimesMillis[algorithm], coloringResult.TimeMillis);

                        avgExecutionTimesTicks[algorithm] += coloringResult.TimeTicks;
                        minExecutionTimesTicks[algorithm] = Math.Min(minExecutionTimesTicks[algorithm], coloringResult.TimeTicks);
                        maxExecutionTimesTicks[algorithm] = Math.Max(maxExecutionTimesTicks[algorithm], coloringResult.TimeTicks);

                        avgUsedColors[algorithm] += coloringResult.UsedColors;
                        minUsedColors[algorithm] = Math.Min(minUsedColors[algorithm], coloringResult.UsedColors);
                        maxUsedColors[algorithm] = Math.Max(maxUsedColors[algorithm], coloringResult.UsedColors);
                        doneSamples++;
                        progress.Report((double)doneSamples * 100.0 / (double)totalSamples);
                    }
                }

                foreach (Algorithm algorithm in request.ChosenAlgorithms)
                {
                    ColoringResult result = new ColoringResult()
                    {
                        Algorithm = algorithm,
                        HypergraphType = hypergraphRequest.GeneratorType,
                        AvgExecutionTime = avgExecutionTimesMillis[algorithm] / request.HypergraphsCount,
                        MinExecutionTime = minExecutionTimesMillis[algorithm],
                        MaxExecutionTime = maxExecutionTimesMillis[algorithm],
                        AvgExecutionTimeTicks = avgExecutionTimesTicks[algorithm] / request.HypergraphsCount,
                        MinExecutionTimeTicks = minExecutionTimesTicks[algorithm],
                        MaxExecutionTimeTicks = maxExecutionTimesTicks[algorithm],
                        DistinctHypergraphs = request.HypergraphsCount,
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

    private SimpleColoringResult ExecuteHypergraphColoring(Hypergraph hypergraph, Algorithm algorithm)
    {
        BaseAlgorithm coloringAlgorithm = _algorithms[algorithm];
        int[] colors = new int [0];
        int tmp = 0;

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        
        colors = coloringAlgorithm.ComputeColoring(hypergraph);
        
        stopwatch.Stop();
        long millis = stopwatch.ElapsedMilliseconds;
        long ticks = stopwatch.ElapsedTicks;


        int count = colors.Distinct().Count();

        return new SimpleColoringResult()
        {
            UsedColors = colors.Distinct().Count(),
            TimeMillis = millis,
            TimeTicks = ticks
        };
    }

}
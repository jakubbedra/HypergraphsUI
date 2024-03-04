using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using HypergraphsUI.Algorithms;
using HypergraphsUI.Model;
using HypergraphsUI.ViewModel.Commands;

namespace HypergraphsUI.ViewModel;

public class MainWindowViewModel : INotifyCollectionChanged, INotifyPropertyChanged
{

    private ObservableCollection<Algorithm> _availableAlgorithms;
    public ObservableCollection<Algorithm> AvailableAlgorithms
    {
        get { return _availableAlgorithms; }
        set
        {
            _availableAlgorithms = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvailableAlgorithms)));
        }
    }
    private ObservableCollection<Algorithm> _chosenAlgorithms;
    public ObservableCollection<Algorithm> ChosenAlgorithm
    {
        get => _chosenAlgorithms;
        set
        {
            _chosenAlgorithms = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChosenAlgorithm)));
        }
    }
    
    public Algorithm? SelectedAlgorithm { get; set; }
    public Algorithm? SelectedChosenAlgorithm { get; set; }
    
    private ObservableCollection<GeneratorType> _availableGenerators;
    public ObservableCollection<GeneratorType> AvailableGenerators
    {
        get { return _availableGenerators; }
        set
        {
            _availableGenerators = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvailableGenerators)));
        }
    }
    
    private ObservableCollection<GeneratorType> _chosenGenerators;
    public ObservableCollection<GeneratorType> ChosenGenerators
    {
        get => _chosenGenerators;
        set
        {
            _chosenGenerators = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChosenGenerators)));
        }
    }
    
    public int IterationsCount { get; set; }
    
    // count of hypergraphs of each size 
    public int HypergraphsCount { get; set; }
    
    private Dictionary<GeneratorType, List<string>> _hypergraphSizes;
    
    private ObservableCollection<string> _selectedHypergraphSizes;//todo: maybe update this instead
    public ObservableCollection<string> SelectedHypergraphSizes
    {
        get => _selectedHypergraphSizes;
        set
        {
            _selectedHypergraphSizes = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedHypergraphSizes)));
        }
    }

    public string? CurrentSizeValue { get; set; }
    public string? SelectedSize { get; set; }
    
    public GeneratorType? SelectedGenerator { get; set; }
    
    private GeneratorType? _selectedChosenGenerator;
    public GeneratorType? SelectedChosenGenerator
    {
        get => _selectedChosenGenerator;
        set
        {
            _selectedChosenGenerator = value;
            OnPropertyChanged(nameof(SelectedChosenGenerator));
        }
    }

    private ObservableCollection<ColoringResult> _results;
    public ObservableCollection<ColoringResult> Results { 
        get => _results;
        set
        {
            _results = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Results)));
        }
    }
    
    private CollectionView _collectionView;

    public CollectionView CollectionView
    {
        get
        {
            _collectionView = (CollectionView)CollectionViewSource.GetDefaultView(Results);
            return _collectionView;
        }
    }
    
    public ICommand AddAlgorithmCommand { get; set; }
    public ICommand AddGeneratorCommand { get; set; }
    public ICommand RemoveAlgorithmCommand { get; set; }
    public ICommand RemoveGeneratorCommand { get; set; }
    public ICommand RemoveSizeCommand { get; set; }
    public ICommand AddSizeCommand { get; set; }
    public ICommand ChangeSizesListCommand { get; set; }
    public ICommand SortListViewCommand { get; set; }
    public ICommand ExecuteAlgorithmsCommand { get; set; }

    private AlgorithmExecutionService _service;
    
    private double? _progress;
    public double? Progress
    {
        get  { return _progress; }
        set
        {
            _progress = value;
            OnPropertyChanged(nameof(Progress));
        }
    }
    private string _dupa;
    public string Dupa
    {
        get  { return _dupa; }
        set
        {
            _dupa = value;
            OnPropertyChanged(nameof(Dupa));
        }
    }
    
    public MainWindowViewModel()
    {
        _progress = 0.0;
        Dupa = "";
        
        _availableAlgorithms = new ObservableCollection<Algorithm>(AlgorithmConstants.AllAlgorithms);
        _availableGenerators = new ObservableCollection<GeneratorType>(AlgorithmConstants.AllGeneratorTypes);
        _chosenGenerators = new ObservableCollection<GeneratorType>();
        _chosenAlgorithms = new ObservableCollection<Algorithm>();
        _selectedHypergraphSizes = new ObservableCollection<string>();
        _results = new ObservableCollection<ColoringResult>();

        AddAlgorithmCommand = new AddAlgorithmCommand(this);
        AddGeneratorCommand = new AddGeneratorCommand(this);
        RemoveAlgorithmCommand = new RemoveAlgorithmCommand(this);
        RemoveGeneratorCommand = new RemoveGeneratorCommand(this);
        RemoveSizeCommand = new RemoveSizeCommand(this);
        AddSizeCommand = new AddSizeCommand(this);
        ChangeSizesListCommand = new ChangeSizesListCommand(this);
        SortListViewCommand = new SortListViewCommand(this);
        ExecuteAlgorithmsCommand = new ExecuteAlgorithmsCommand(this);
        
        _service = new AlgorithmExecutionService();

        IterationsCount = 10;
        HypergraphsCount = 1;
        _hypergraphSizes = new Dictionary<GeneratorType, List<string>>();
        foreach (GeneratorType generator in AvailableGenerators)
            _hypergraphSizes[generator] = new List<string>();
    }

    public async Task ExecuteAlgorithms()
    { 
        Application.Current.Dispatcher.Invoke(() =>
        {
            Progress = 0.0;
        });
        List<HypergraphRequest> hypergraphRequests = _chosenGenerators.Select(generator => new HypergraphRequest()
        {
            GeneratorType = generator, Sizes = _hypergraphSizes[generator]
        }).ToList();
        
        ExecutionRequest request = new ExecutionRequest()
        {
            ChosenAlgorithms = _chosenAlgorithms.ToList(),
            Hypergraphs = hypergraphRequests,
            HypergraphsCount = HypergraphsCount,
            IterationCount = IterationsCount
        };
        
        var progressReporter = new Progress<double>(value =>
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Progress = value;
            });
        });
        
        // ExecutionResult executionResult = await _service.Execute(request, progressReporter);
        // _results.Clear();
        // executionResult.Results.ForEach(_results.Add);
        
        try
        {
            // Execute algorithms asynchronously
            ExecutionResult executionResult = await Task.Run(() => _service.Execute(request, progressReporter));

            // Clear previous results and add new results
            _results.Clear();
            executionResult.Results.ForEach(_results.Add);

            // Set progress to 100% upon completion
            Application.Current.Dispatcher.Invoke(() =>
            {
                Progress = 100.0;
            });
        }
        catch (Exception ex)
        {
            // Handle any exceptions here
            // For example: log the exception, show an error message, etc.
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
    
    public void SortResults(string columnName)
    {
        SortDescription newSortDescription = new SortDescription(columnName, ListSortDirection.Ascending);
        if (CollectionView.SortDescriptions.Count > 0)
        {
            SortDescription oldSortDescription = CollectionView.SortDescriptions[0];
            if (oldSortDescription.PropertyName.Equals(columnName))
            {
                newSortDescription = new SortDescription(
                    columnName,
                    oldSortDescription.Direction == ListSortDirection.Ascending
                        ? ListSortDirection.Descending
                        : ListSortDirection.Ascending
                );
            }
        }

        CollectionView.SortDescriptions.Clear();
        CollectionView.SortDescriptions.Add(newSortDescription);
    }
    
    public bool IsGeneratorCompatible()
    {
        if (SelectedGenerator == null) return false;
        if (_chosenAlgorithms.Count == 0) return true;
        return _service.IsGeneratorCompatible(SelectedGenerator.Value, _chosenAlgorithms.ToList());
    }

    public bool IsAlgorithmCompatible()
    {
        if (SelectedAlgorithm == null) return false;
        if (_chosenGenerators.Count == 0) return true;
        return _service.IsAlgorithmCompatible(SelectedAlgorithm.Value, _chosenGenerators.ToList());
    }
    
    public event EventHandler? RefreshUIRequested;

    
    public void ChangeSizesList()
    {
        if (SelectedChosenGenerator == null) return;
        
        _selectedHypergraphSizes.Clear();
        _hypergraphSizes[SelectedChosenGenerator.Value].ForEach(_selectedHypergraphSizes.Add);
    }
    
    public void RemoveSelectedSize()
    {
        if (SelectedChosenGenerator == null) return;
        
        _hypergraphSizes[SelectedChosenGenerator.Value].Remove(CurrentSizeValue);
        
        _selectedHypergraphSizes.Clear();
        _hypergraphSizes[SelectedChosenGenerator.Value].ForEach(_selectedHypergraphSizes.Add);
    }

    public void AddSelectedSize()
    {
        if (SelectedChosenGenerator == null) return;

        _hypergraphSizes[SelectedChosenGenerator.Value].Add(CurrentSizeValue);
        
        _selectedHypergraphSizes.Clear();
        _hypergraphSizes[SelectedChosenGenerator.Value].ForEach(_selectedHypergraphSizes.Add);
    }
    
    public void AddSelectedAlgorithm()
    {
        if (SelectedAlgorithm == null || !IsAlgorithmCompatible()) return;
        
        _chosenAlgorithms.Add(SelectedAlgorithm.Value);
        _availableAlgorithms.Remove(SelectedAlgorithm.Value);
    }
    
    public void RemoveSelectedAlgorithm()
    {
        if (SelectedChosenAlgorithm == null) return;

        _availableAlgorithms.Add(SelectedChosenAlgorithm.Value);
        _chosenAlgorithms.Remove(SelectedChosenAlgorithm.Value);
    }
    
    public void AddSelectedGenerator()
    {
        if (SelectedGenerator == null || !IsGeneratorCompatible()) return;
        
        _chosenGenerators.Add(SelectedGenerator.Value);
        _availableGenerators.Remove(SelectedGenerator.Value);
    }
    
    public void RemoveSelectedGenerator()
    {
        if (SelectedChosenGenerator == null) return;

        _availableGenerators.Add(SelectedChosenGenerator.Value);
        _chosenGenerators.Remove(SelectedChosenGenerator.Value);
    }

    
    // =================================================================================================
    
    
    public event PropertyChangedEventHandler? PropertyChanged;
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    protected virtual void OnPropertyChanged(string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}
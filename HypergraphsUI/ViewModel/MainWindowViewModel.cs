using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using HypergraphsUI.Algorithms;

namespace HypergraphsUI.ViewModel;

public class MainWindowViewModel : INotifyCollectionChanged
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
        get { return _chosenAlgorithms; }
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
        get { return _chosenGenerators; }
        set
        {
            _chosenGenerators = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ChosenGenerators)));
        }
    }
    
    public GeneratorType? SelectedGenerator { get; set; }
    public GeneratorType? SelectedChosenGenerator { get; set; }

    public ICommand AddAlgorithmCommand { get; set; }
    public ICommand AddGeneratorCommand { get; set; }

    public ICommand RemoveAlgorithmCommand { get; set; }
    public ICommand RemoveGeneratorCommand { get; set; }

    private AlgorithmExecutionService _service;
    
    public MainWindowViewModel()
    {
        _availableAlgorithms = new ObservableCollection<Algorithm>(AlgorithmConstants.AllAlgorithms);
        _availableGenerators = new ObservableCollection<GeneratorType>(AlgorithmConstants.AllGeneratorTypes);
        _chosenGenerators = new ObservableCollection<GeneratorType>();
        _chosenAlgorithms = new ObservableCollection<Algorithm>();

        AddAlgorithmCommand = new AddAlgorithmCommand(this);
        AddGeneratorCommand = new AddGeneratorCommand(this);
        RemoveAlgorithmCommand = new RemoveAlgorithmCommand(this);
        RemoveGeneratorCommand = new RemoveGeneratorCommand(this);

        _service = new AlgorithmExecutionService();
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
    
    public void AddSelectedAlgorithm()
    {
        if (SelectedAlgorithm == null) return;
        
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
        if (SelectedGenerator == null) return;
        
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
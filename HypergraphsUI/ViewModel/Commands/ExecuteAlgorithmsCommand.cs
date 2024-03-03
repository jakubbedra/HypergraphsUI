using HypergraphsUI.ViewModel;
using HypergraphsUI.ViewModel.Commands;

public class ExecuteAlgorithmsCommand : CommandBase<MainWindowViewModel>
{
    public ExecuteAlgorithmsCommand(MainWindowViewModel viewModel) : base(viewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        _viewModel.ExecuteAlgorithms();
    }
}
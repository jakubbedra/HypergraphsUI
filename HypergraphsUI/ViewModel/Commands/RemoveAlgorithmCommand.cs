using HypergraphsUI.ViewModel;
using HypergraphsUI.ViewModel.Commands;

public class RemoveAlgorithmCommand : CommandBase<MainWindowViewModel>
{
    public RemoveAlgorithmCommand(MainWindowViewModel viewModel) : base(viewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        _viewModel.RemoveSelectedAlgorithm();
    }
}
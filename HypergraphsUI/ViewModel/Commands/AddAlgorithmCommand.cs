using HypergraphsUI.ViewModel;
using HypergraphsUI.ViewModel.Commands;

public class AddAlgorithmCommand : CommandBase<MainWindowViewModel>
{
    public AddAlgorithmCommand(MainWindowViewModel viewModel) : base(viewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        _viewModel.AddSelectedAlgorithm();
    }
}
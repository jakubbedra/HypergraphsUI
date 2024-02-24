using HypergraphsUI.ViewModel;
using HypergraphsUI.ViewModel.Commands;

public class RemoveGeneratorCommand : CommandBase<MainWindowViewModel>
{
    public RemoveGeneratorCommand(MainWindowViewModel viewModel) : base(viewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        _viewModel.RemoveSelectedGenerator();
    }
}
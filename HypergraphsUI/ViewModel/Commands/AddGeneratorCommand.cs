using HypergraphsUI.ViewModel;
using HypergraphsUI.ViewModel.Commands;

public class AddGeneratorCommand : CommandBase<MainWindowViewModel>
{
    public AddGeneratorCommand(MainWindowViewModel viewModel) : base(viewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        _viewModel.AddSelectedGenerator();
    }
}
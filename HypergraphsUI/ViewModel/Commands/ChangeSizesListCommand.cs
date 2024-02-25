namespace HypergraphsUI.ViewModel.Commands;

public class ChangeSizesListCommand : CommandBase<MainWindowViewModel>
{
    public ChangeSizesListCommand(MainWindowViewModel viewModel) : base(viewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        _viewModel.ChangeSizesList();
    }
}
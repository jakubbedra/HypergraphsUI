using HypergraphsUI.ViewModel;
using HypergraphsUI.ViewModel.Commands;

public class RemoveSizeCommand : CommandBase<MainWindowViewModel>
{
    public RemoveSizeCommand(MainWindowViewModel viewModel) : base(viewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        _viewModel.RemoveSelectedSize();
    }
}
using HypergraphsUI.ViewModel;
using HypergraphsUI.ViewModel.Commands;

public class AddSizeCommand : CommandBase<MainWindowViewModel>
{
    public AddSizeCommand(MainWindowViewModel viewModel) : base(viewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        _viewModel.AddSelectedSize();
    }
}
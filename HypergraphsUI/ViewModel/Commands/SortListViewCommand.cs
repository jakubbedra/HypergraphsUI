using HypergraphsUI.ViewModel;
using HypergraphsUI.ViewModel.Commands;

public class SortListViewCommand : CommandBase<MainWindowViewModel>
{
    public SortListViewCommand(MainWindowViewModel viewModel) : base(viewModel)
    {
    }

    public override void Execute(object? parameter)
    {
        if (parameter is string name)
        {
            _viewModel.SortResults(name);
        }
    }
}
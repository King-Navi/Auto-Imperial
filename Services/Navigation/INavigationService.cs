namespace Services.Navigation
{
    public interface INavigationService
    {
        ViewModel CurrentView { get; }
        void NavigateTo<T>() where T : ViewModel;
        void NavigateTo<T>(object parameter) where T : ViewModel;

    }
}

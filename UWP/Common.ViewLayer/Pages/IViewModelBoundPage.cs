namespace Common.ViewLayer.Pages
{
    public interface IViewModelBoundPage<TViewModel>
    {
        TViewModel ViewModel { get; set; }
    }
}

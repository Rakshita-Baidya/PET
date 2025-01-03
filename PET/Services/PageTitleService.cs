public class PageTitleService
{
    public event Action? OnPageTitleChanged;

    private string _pageTitle;
    public string PageTitle
    {
        get => _pageTitle;
        set
        {
            if (_pageTitle != value)
            {
                _pageTitle = value;
                OnPageTitleChanged?.Invoke();
            }
        }
    }
}

namespace BasementBlog.Utilities;

public enum PredefineCategoryNames
{
    General,
    Powershell,
    MAUIDevelopment,
    Xamarin,
    WorkLog,
    Pipeline,
}

public static class PredefineCategoryNameExt
{
    public static string ToString(this PredefineCategoryNames name)
    {
        switch (name)
        {
            case PredefineCategoryNames.MAUIDevelopment:
                return "MAUI Development";
            case PredefineCategoryNames.WorkLog:
                return "Work Log";
            default:
                return string.Empty;
        }
    }
}

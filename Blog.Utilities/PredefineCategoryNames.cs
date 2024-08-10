namespace Blog.Utilities;

public enum PredefineCategoryNames
{
    General,
    Powershell,
    MAUIDevelopment,
    Xamarin,
    WorkLogs,
    Pipeline,
}

// TODO: Remove work log category out of its existent
public static class PredefineCategoryNameExt
{
    public static string ToString(this PredefineCategoryNames name)
    {
        switch (name)
        {
            case PredefineCategoryNames.MAUIDevelopment:
                return "MAUI Development";
            case PredefineCategoryNames.WorkLogs:
                return "Work Logs";
            default:
                return string.Empty;
        }
    }
}

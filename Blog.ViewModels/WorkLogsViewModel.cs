using Blog.DTO;
using Blog.Services.Interfaces;
using Blog.Utilities;
using MudBlazor;
using System.Collections.ObjectModel;

namespace Blog.ViewModels;
public class WorkLogsViewModel : BaseViewModel
{
    private readonly IWorkLogService workLogService;
    private readonly IMarkdownService markdownService;
    private readonly ISnackbar snackbar;
    public WorkLogsViewModel(IWorkLogService workLogService, IMarkdownService markdownService, ISnackbar snackbar)
    {
        this.workLogService = workLogService;
        this.markdownService = markdownService;
        this.snackbar = snackbar;
    }

    private ObservableCollection<WorkLogDTO> workLogs;
    public ObservableCollection<WorkLogDTO> WorkLogs
    {
        get => workLogs;
        set
        {
            workLogs = value;
            OnPropertyChanged();
        }
    }

    private WorkLogDTO selectedWorkLog;
    public WorkLogDTO SelectedWorkLog
    {
        get => selectedWorkLog;
        set
        {
            selectedWorkLog = value;
            if (selectedWorkLog != null)
            {
                Body = selectedWorkLog.Body;
            }

            OnPropertyChanged();
        }
    }

    private string body;
    public string Body
    {
        get => body;
        set
        {
            body = value;
            OnPropertyChanged();
        }
    }

    private DateTime? dateTime;
    public DateTime? SelectedDate
    {
        get => dateTime;
        set
        {
            dateTime = value;
            if (WorkLogs.Any() && value != null)
            {
                SelectedWorkLog = WorkLogs.SingleOrDefault(s => s.LoggedDate == SelectedDate.Value.ToString(Common.DefaultDateTimeFormat));
            }

            if (SelectedWorkLog == null)
            {
                SelectedWorkLog = null;
                Body = null;
            }

            OnPropertyChanged();
        }
    }

    public string PreviewBody => string.IsNullOrEmpty(Body) ? string.Empty : markdownService.TextToHtml(Body);

    public async Task Setup()
    {
        var workLogs = await workLogService.GetAllWorkLogs();
        if (workLogs.Any())
        {
            WorkLogs = new ObservableCollection<WorkLogDTO>(workLogs);
        }
        else
        {
            WorkLogs = new ObservableCollection<WorkLogDTO>();
        }

        SelectedDate = DateTime.Now;
    }

    public async Task GetWorkLogById(string id)
    {
        var res = await workLogService.GetWorkLogById(id);
        if (res is not null)
        {
            SelectedWorkLog = new WorkLogDTO
            {
                Id = res.Id,
                Body = Body = res.Body,
                ModifiedDate = res.ModifiedDate,
                LoggedDate = res.LoggedDate,
            };

            var dt = DateTime.Parse(SelectedWorkLog.LoggedDate);
            SelectedDate = dt;
        }
    }

    public async Task SaveOrUpdateWorkLog()
    {
        if (string.IsNullOrEmpty(Body))
        {
            Body = string.Empty;
            return;
        }

        if (SelectedWorkLog == null)
        {
            SelectedWorkLog = new WorkLogDTO
            {
                Body = Body,
                ModifiedDate = DateTime.Now,
            };
        }
        else
        {
            SelectedWorkLog.Body = Body;
            SelectedWorkLog.ModifiedDate = DateTime.Now;
        }

        SelectedWorkLog.LoggedDate = SelectedDate.Value.ToString(Common.DefaultDateTimeFormat);

        var id = await workLogService.SaveOrUpdateWorkLog(SelectedWorkLog);
        if (!string.IsNullOrEmpty(id))
        {
            if (WorkLogs.Contains(SelectedWorkLog))
            {
                WorkLogs.Remove(SelectedWorkLog);
            }

            SelectedWorkLog.Id = id;
            WorkLogs.Add(SelectedWorkLog);
        }

        snackbar.Add("Save work log successfully", Severity.Success, config =>
        {
            config.CloseAfterNavigation = true;
            config.ShowTransitionDuration = 250;
            config.HideTransitionDuration = 250;
            config.DuplicatesBehavior = SnackbarDuplicatesBehavior.Prevent;
        });
    }
}

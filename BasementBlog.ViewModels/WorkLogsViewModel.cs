using BasementBlog.DTO;
using BasementBlog.Services.Interfaces;

namespace BasementBlog.ViewModels;

public class WorkLogsViewModel : BaseViewModel
{
    private readonly IWorkLogService workLogService;
    private readonly IMarkdownService markdownService;
    public WorkLogsViewModel(IWorkLogService workLogService, IMarkdownService markdownService)
    {
        this.workLogService = workLogService;
        this.markdownService = markdownService;
    }

    public List<WorkLogDTO> WorkLogs = new List<WorkLogDTO>();
    public WorkLogDTO SelectedWorkLog { get; set; }

    private string WorkLogId { get; set; }
    public string Body { get; set; }
    public string PreviewBody => string.IsNullOrEmpty(Body) ? "Type here" : markdownService.TextToHtml(Body);

    public async Task GetAllWorkLog()
    {
        var workLogs = await workLogService.GetAllWorkLogs();
        if (workLogs.Any())
        {
            WorkLogs = workLogs.ToList();
        }
    }

    public async Task GetWorkLogById(string id)
    {
        var res = await workLogService.GetWorkLogById(id);
        if (res is not null)
        {
            WorkLogId = res.Id;
            Body = res.Body;
        }
    }

    public async Task SaveOrUpdateWorkLog()
    {
        if (string.IsNullOrEmpty(Body))
        {
            return;
        }

        var dto = new WorkLogDTO
        {
            Id = WorkLogId,
            Body = Body,
            ModifiedDate = DateTime.Now,
            LoggedDate = DateTime.Now,
        };

        var result = await workLogService.SaveOrUpdateWorkLog(dto);
        if (result && WorkLogs.Any(s => s.Id != WorkLogId))
        {
            WorkLogs.Add(dto);
        }
    }
}

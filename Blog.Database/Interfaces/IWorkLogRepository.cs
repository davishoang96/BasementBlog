using Blog.DTO;

namespace Blog.Services.Interfaces;

public interface IWorkLogRepository
{
    Task<WorkLogDTO> GetWorkLogById(string id);
    Task<IEnumerable<WorkLogDTO>> GetAllWorkLogs();
    Task<IEnumerable<WorkLogDTO>> GetAllWorkLogsWithoutBody();
    Task<string> SaveOrUpdateWorkLog(WorkLogDTO workLogDTO);
    Task<bool> ClearAllWorkLogs();
}

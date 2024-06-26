using BasementBlog.DTO;

namespace BasementBlog.Services.Interfaces;

public interface IWorkLogService
{
    Task<WorkLogDTO> GetWorkLogById(string id);
    Task<IEnumerable<WorkLogDTO>> GetAllWorkLogs();
    Task<string> SaveOrUpdateWorkLog(WorkLogDTO workLogDTO);
    Task<bool> ClearAllWorkLogs();
}

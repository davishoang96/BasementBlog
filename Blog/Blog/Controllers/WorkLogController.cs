using Blog.DTO;
using Blog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Blog.Controllers;

[Route("[controller]")]
public class WorkLogController : BaseAuthorizedController
{
    private IWorkLogRepository workLogRepository;
    public WorkLogController(IWorkLogRepository workLogRepository)
    {
        this.workLogRepository = workLogRepository;
    }

    [HttpGet(nameof(GetWorkLogById))]
    [SwaggerOperation(OperationId = nameof(GetWorkLogById))]
    public async Task<ActionResult<WorkLogDTO>> GetWorkLogById(string id)
    {
        var result = await workLogRepository.GetWorkLogById(id);
        return Ok(result);
    }

    [HttpGet(nameof(GetAllWorkLogs))]
    [SwaggerOperation(OperationId = nameof(GetAllWorkLogs))]
    public async Task<ActionResult<IEnumerable<WorkLogDTO>>> GetAllWorkLogs()
    {
        var result = await workLogRepository.GetAllWorkLogs();
        return Ok(result);
    }

    [HttpGet(nameof(GetAllWorkLogsWithoutBody))]
    [SwaggerOperation(OperationId = nameof(GetAllWorkLogsWithoutBody))]
    public async Task<ActionResult<IEnumerable<WorkLogDTO>>> GetAllWorkLogsWithoutBody()
    {
        var result = await workLogRepository.GetAllWorkLogsWithoutBody();
        return Ok(result);
    }

    [HttpPost(nameof(SaveOrUpdateWorkLog))]
    [Produces("text/plain")]
    [SwaggerOperation(OperationId = nameof(SaveOrUpdateWorkLog))]
    public async Task<string> SaveOrUpdateWorkLog([FromBody] WorkLogDTO workLogDTO)
    {
        var result = await workLogRepository.SaveOrUpdateWorkLog(workLogDTO);
        return result;
    }

    [HttpDelete]
    public async Task<bool> ClearAllWorkLogs()
    {
        return await workLogRepository.ClearAllWorkLogs();
    }
}

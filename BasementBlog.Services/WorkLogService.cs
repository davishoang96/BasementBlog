using BasementBlog.Database;
using BasementBlog.Database.Models;
using BasementBlog.DTO;
using BasementBlog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BasementBlog.Services;

public class WorkLogService : IWorkLogService
{
    private readonly DatabaseContext db;
    private ISqidService sqidService;
    public WorkLogService(DatabaseContext databaseContext, ISqidService sqidService)
    {
        this.sqidService = sqidService;
        db = databaseContext;
    }

    public async Task<IEnumerable<WorkLogDTO>> GetAllWorkLogs()
    {
        return await db.WorkLogs.Select(model => new WorkLogDTO
        {
            Id = sqidService.EncryptId(model.Id),
            Body = model.Body,
            IsDeleted = model.IsDeleted,
            LoggedDate = model.LoggedDate, 
            ModifiedDate = model.ModifiedDate,  
        }).ToListAsync();
    }

    public async Task<WorkLogDTO> GetWorkLogById(string id)
    {
        var scr = sqidService.DecryptId(id);
        var model = await db.WorkLogs.SingleOrDefaultAsync(s=>s.Id == scr);
        if(model is null)
        {
            return null;
        }

        return new WorkLogDTO
        {
            Id = sqidService.EncryptId(model.Id),
            Body = model.Body,
            IsDeleted = model.IsDeleted,
            LoggedDate = model.LoggedDate, 
            ModifiedDate = model.ModifiedDate,
        };
    }

    public async Task<bool> SaveOrUpdateWorkLog(WorkLogDTO workLogDTO)
    {
        if (workLogDTO is null)
        {
            return false;
        }

        WorkLog? workModel = null;

        if (workLogDTO.Id is not null)
        {
            var logId = sqidService.DecryptId(workLogDTO.Id);
            workModel = db.WorkLogs.Single(s => s.Id == logId);
        }

        if (workModel is null)
        {
            workModel = new WorkLog
            {
                ModifiedDate = DateTime.Now,
                LoggedDate = DateTime.Now,
                Body = workLogDTO.Body,
            };

            db.Add(workModel);
        }
        else
        {
            workModel.ModifiedDate = DateTime.Now;
            workModel.Body = workLogDTO.Body;
            db.Update(workModel);
        }

        await db.SaveChangesAsync();
        return true;
    }
}
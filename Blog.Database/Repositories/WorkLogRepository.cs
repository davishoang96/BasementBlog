using Blog.Database.Models;
using Blog.DTO;
using Blog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Blog.Database.Repositories;

public class WorkLogRepository : IWorkLogRepository
{
   private readonly DatabaseContext db;
   private ISqidService sqidService;
   public WorkLogRepository(DatabaseContext databaseContext, ISqidService sqidService)
   {
       this.sqidService = sqidService;
       db = databaseContext;
   }

   public async Task<bool> ClearAllWorkLogs()
   {
       var res = await db.WorkLogs.ExecuteDeleteAsync();
       if (res < 0)
       {
           return false;
       }

       return true;
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

    public async Task<IEnumerable<WorkLogDTO>> GetAllWorkLogsWithoutBody()
    {
        return await db.WorkLogs.Select(model => new WorkLogDTO
        {
            Id = sqidService.EncryptId(model.Id),
            Body = null,
            IsDeleted = model.IsDeleted,
            LoggedDate = model.LoggedDate,
            ModifiedDate = model.ModifiedDate,
        }).ToListAsync();
    }

    public async Task<WorkLogDTO> GetWorkLogById(string id)
    {
       var scr = sqidService.DecryptId(id);
       var model = await db.WorkLogs.SingleOrDefaultAsync(s => s.Id == scr);
       if (model is null)
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

   public async Task<string> SaveOrUpdateWorkLog(WorkLogDTO workLogDTO)
   {
       if (workLogDTO is null)
       {
           return string.Empty;
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
               ModifiedDate = workLogDTO.ModifiedDate,
               LoggedDate = workLogDTO.LoggedDate,
               Body = workLogDTO.Body,
           };

           db.Add(workModel);
       }
       else
       {
           workModel.ModifiedDate = workLogDTO.ModifiedDate;
           workModel.Body = workLogDTO.Body;
           db.Update(workModel);
       }

       await db.SaveChangesAsync();
       return sqidService.EncryptId(workModel.Id);
   }
}
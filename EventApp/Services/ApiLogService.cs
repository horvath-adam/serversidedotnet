using EventApp.Models;
using EventApp.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Services
{
    public interface IApiLogService
    {
        void Log(ApiLogEntry entry);

        IQueryable<ApiLogEntry> GetEntries();
    }

    public class ApiLogService : AbstractService, IApiLogService
    {
        public ApiLogService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public void Log(ApiLogEntry entry)
        {
            try
            {
                UnitOfWork.GetRepository<ApiLogEntry>().InsertRange(new[] { entry });
                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                //TODO: log
            }
        }

        public IQueryable<ApiLogEntry> GetEntries()
        {
            return UnitOfWork.GetRepository<ApiLogEntry>().GetAll();
        }
    }
}

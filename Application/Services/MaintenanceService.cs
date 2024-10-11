using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Services
{
    public class MaintenanceService : BaseService<Domain.Entities.MaintenanceService>, IMaintenanceService
    {
        public MaintenanceService(IGenericRepository<Domain.Entities.MaintenanceService> repository) : base(repository)
        {
        }
    }
}

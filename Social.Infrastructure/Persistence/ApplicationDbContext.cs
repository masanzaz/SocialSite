using Microsoft.EntityFrameworkCore;
using Social.Application.Interfaces;
using Social.Domain.Common;
using Social.Domain.Entities;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Social.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IDateTime dateTime,
            ICurrentUserService currentUserService) : base(options)
        {
            _dateTime = dateTime;
            _currentUserService = currentUserService;
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.IsActive = true;
                        entry.Entity.CreatedBy = _currentUserService.UserName;
                        entry.Entity.CreatedAt = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = _currentUserService.UserName;
                        entry.Entity.ModifiedAt = _dateTime.Now;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.IsActive = false;
                        entry.Entity.ModifiedBy = _currentUserService.UserName;
                        entry.Entity.ModifiedAt = _dateTime.Now;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
        public DbSet<Disability> disability { get; set; }
    }
}

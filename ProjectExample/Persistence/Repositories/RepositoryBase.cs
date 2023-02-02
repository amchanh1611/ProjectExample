using Microsoft.EntityFrameworkCore;
using ProjectExample.Modules.Medias.Entities;
using ProjectExample.Persistence.Contexts;
using System.Linq.Expressions;

namespace ProjectExample.Persistence.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ApplicationDbContext context;

        public RepositoryBase(ApplicationDbContext context)
        {
            this.context = context;
        }

        public T Create(T entity) => context.Set<T>().Add(entity).Entity;

        public void Delete(T entity) => context.Set<T>().Remove(entity);

        public IQueryable<T> FindAll() => context.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
            => context.Set<T>().Where(expression).AsNoTracking();

        public T Update(T entity) => context.Set<T>().Update(entity).Entity;
    }

    public class MediaRepository : RepositoryBase<Media>, IMediaRepository
    {
        public MediaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

    public class ScheduleRepositoy : RepositoryBase<Schedule>, IScheduleRepository
    {
        public ScheduleRepositoy(ApplicationDbContext context) : base(context)
        {
        }
    }

    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationDbContext context;
        private IMediaRepository? media;
        private IScheduleRepository? schedule;

        public RepositoryWrapper(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IMediaRepository Media
        {
            get
            {
                if (this.media == null)
                {
                    this.media = new MediaRepository(context);
                }
                return this.media;
            }
        }

        public IScheduleRepository Schedule
        {
            get
            {
                if (this.schedule == null)
                {
                    this.schedule = new ScheduleRepositoy(context);
                }
                return this.schedule;
            }
        }

        public bool Save()
        {
            int check = context.SaveChanges();
            return check > 0 ? true : false;
        }
    }
}
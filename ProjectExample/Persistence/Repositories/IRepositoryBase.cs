using ProjectExample.Modules.Medias.Entities;
using System.Linq.Expressions;

namespace ProjectExample.Persistence.Repositories
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        T Create(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
    public interface IMediaRepository : IRepositoryBase<Media>
    {

    }
    public interface IScheduleRepository : IRepositoryBase<Schedule>
    {

    }
    public interface IRepositoryWrapper
    {
        IMediaRepository Media { get; }
        IScheduleRepository Schedule { get; }
        bool Save();
    }
}

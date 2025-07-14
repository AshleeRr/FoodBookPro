using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Interfaces.Common;
using Microsoft.EntityFrameworkCore;

namespace FoodBookPro.Data.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public virtual async Task<OperationResult<ICollection<TEntity>>> GetAllAsync()
        {
            try
            {
                var entities = await _context.Set<TEntity>().ToListAsync();
                return entities is not null && entities.Count != 0
                    ? OperationResult<ICollection<TEntity>>.Success(entities, "Database data found")
                    : OperationResult<ICollection<TEntity>>.Success(entities, "Nothing found in the database");
            }
            catch (Exception e)
            {
                return OperationResult<ICollection<TEntity>>.Failure($"Error Octeniendo los datos de la base de datos: {e.Message}", null, null);
            }
        }

        public virtual async Task<OperationResult<ICollection<TEntity>>> GetAllIncludeAsync(ICollection<string> properties)
        {
            try
            {
                var query = _context.Set<TEntity>().AsQueryable();
                foreach (var property in properties)
                {
                    query = query.Include(property);
                }
                var entities = await query.ToListAsync();

                return entities is not null && entities.Count != 0
                    ? OperationResult<ICollection<TEntity>>.Success(entities, "Database data found")
                    : OperationResult<ICollection<TEntity>>.Success(entities, "Nothing found in the database");
            }
            catch (Exception e)
            {
                return OperationResult<ICollection<TEntity>>.Failure($"Error Obteniendo los datos de la base de datos: {e.Message}", null, null);
            }
        }

        public virtual async Task<OperationResult<IQueryable<TEntity>>> GetAllQueryAsync()
        {
            try
            {
                var queryable = _context.Set<TEntity>().AsQueryable();
                var haveElement = await queryable.AnyAsync();
                return haveElement
                    ? OperationResult<IQueryable<TEntity>>.Success(queryable, "Database data found")
                    : OperationResult<IQueryable<TEntity>>.Success(queryable, "Nothing found in the database");
            }
            catch (Exception e)
            {
                return OperationResult<IQueryable<TEntity>>.Failure($"Error Obteniendo los datos de la base de datos: {e.Message}", null, null);
            }
        }

        public virtual async Task<OperationResult<IQueryable<TEntity>>> GetAllQueryIncludeAsync(ICollection<string> properties)
        {
            try
            {
                var queryable = _context.Set<TEntity>().AsQueryable();
                foreach (var property in properties)
                {
                    queryable = queryable.Include(property);
                }
                var haveElement = await queryable.AnyAsync();
                return haveElement
                    ? OperationResult<IQueryable<TEntity>>.Success(queryable, "Database data found")
                    : OperationResult<IQueryable<TEntity>>.Success(queryable, "Nothing found in the database");
            }
            catch (Exception e)
            {
                return OperationResult<IQueryable<TEntity>>.Failure($"Error Obteniendo los datos de la base de datos: {e.Message}", null, null);
            }
        }

        public virtual async Task<OperationResult<TEntity>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _context.Set<TEntity>().FindAsync(id);
                return entity != null
                    ? OperationResult<TEntity>.Success(entity, "Entity found")
                    : OperationResult<TEntity>.Failure("Entity not found", null, null);
            }
            catch (Exception e)
            {
                return OperationResult<TEntity>.Failure($"Error getting entity by id: {e.Message}", null, null);
            }
        }

        public virtual async Task<OperationResult<TEntity>> AddAsync(TEntity entity)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return OperationResult<TEntity>.Success(entity, "Entity added successfully");
            }
            catch (Exception e)
            {
                return OperationResult<TEntity>.Failure($"Error adding entity: {e.Message}", null, null);
            }
        }

        public virtual async Task<OperationResult<ICollection<TEntity>>> AddRangeAsync(ICollection<TEntity> entities)
        {
            try
            {
                await _context.Set<TEntity>().AddRangeAsync(entities);
                await _context.SaveChangesAsync();
                return OperationResult<ICollection<TEntity>>.Success(entities, "Entities added successfully");
            }
            catch (Exception e)
            {
                return OperationResult<ICollection<TEntity>>.Failure($"Error adding entities: {e.Message}", null, null);
            }
        }

        public virtual async Task<OperationResult<TEntity>> Update(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity);
                await _context.SaveChangesAsync();
                return OperationResult<TEntity>.Success(entity, "Entity updated successfully");
            }
            catch (Exception e)
            {
                return OperationResult<TEntity>.Failure($"Error updating entity: {e.Message}", null, null);
            }
        }

        public virtual async Task<OperationResult<TEntity>> RemoveAsync(int id)
        {
            try
            {
                var entity = await _context.Set<TEntity>().FindAsync(id);
                if (entity == null)
                    return OperationResult<TEntity>.Failure("Entity not found", null, null);

                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
                return OperationResult<TEntity>.Success(entity, "Entity removed successfully");
            }
            catch (Exception e)
            {
                return OperationResult<TEntity>.Failure($"Error removing entity: {e.Message}", null, null);
            }
        }
    } 
}
using AutoMapper;
using FoodBookPro.Data.Application.ViewModels.Users;
using FoodBookPro.Data.Domain.Common;
using FoodBookPro.Data.Domain.Entities;
using FoodBookPro.Data.Domain.Interfaces.Common;

namespace FoodBookPro.Data.Application.Services
{
    public class GenericService<SaveVm, Vm, Entity> : IGenericService<SaveVm, Vm, Entity>
        where SaveVm : class
        where Vm : class
        where Entity : class
    {
        private readonly IGenericRepository<Entity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Entity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<OperationResult<Vm>> AddAsync(SaveVm vm)
        {
            Entity entity = _mapper.Map<Entity>(vm);

            OperationResult<Vm> result = _mapper.Map<OperationResult<Vm>>(await _repository.AddAsync(entity));

            return result;
        }

        public virtual async Task<OperationResult<ICollection<Vm>>> AddRangeAsync(ICollection<SaveVm> entity)
        {
            ICollection<Entity> entities = _mapper.Map<ICollection<Entity>>(entity);

            OperationResult<ICollection<Vm>> result = _mapper.Map<OperationResult<ICollection<Vm>>>(await _repository.AddRangeAsync(entities));

            return result;
        }

        public virtual async Task<OperationResult<ICollection<Vm>>> GetAllAsync()
        {
            OperationResult<ICollection<Vm>> result = _mapper.Map<OperationResult<ICollection<Vm>>>(await _repository.GetAllAsync());

            return result;
        }

        public virtual async Task<OperationResult<ICollection<Vm>>> GetAllIncludeAsync(ICollection<string> properties)
        {
            OperationResult<ICollection<Vm>> result = _mapper.Map<OperationResult<ICollection<Vm>>>(await _repository.GetAllIncludeAsync(properties));

            return result;
        }

        public virtual async Task<OperationResult<IQueryable<Vm>>> GetAllQueryAsync()
        {
            OperationResult<IQueryable<Vm>> result = _mapper.Map<OperationResult<IQueryable<Vm>>>(await _repository.GetAllQueryAsync());

            return result;
        }

        public virtual async Task<OperationResult<IQueryable<Vm>>> GetAllQueryIncludeAsync(ICollection<string> properties)
        {
            OperationResult<IQueryable<Vm>> result = _mapper.Map<OperationResult<IQueryable<Vm>>>(await _repository.GetAllQueryIncludeAsync(properties));

            return result;
        }

        public virtual async Task<OperationResult<Vm>> GetByIdAsync(int id)
        {
            OperationResult<Vm> result = _mapper.Map<OperationResult<Vm>>(await _repository.GetByIdAsync(id));

            return result;
        }

        public virtual async Task<OperationResult<Vm>> RemoveAsync(int id)
        {
            OperationResult<Vm> result = _mapper.Map<OperationResult<Vm>>(await _repository.RemoveAsync(id));

            return result;
        }

        public virtual async Task<OperationResult<Vm>> Update(SaveVm vm)
        {
            Entity entity = _mapper.Map<Entity>(vm);

            OperationResult<Vm> result = _mapper.Map<OperationResult<Vm>>(await _repository.Update(entity));

            return result;
        }
    }
}

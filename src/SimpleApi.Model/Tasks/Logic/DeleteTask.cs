using MediatR;
using SimpleApi.Model.Base;

namespace SimpleApi.Model.Tasks.Logic;

public record DeleteTaskRequest(Guid Id): IRequest;

public class DeleteTaskRequestHandler : IRequestHandler<DeleteTaskRequest>
{
    private IUnitOfWorkFactory _unitOfWorkFactory;
    private IRepository<TaskEntity, Guid> _repository;

    public DeleteTaskRequestHandler(IUnitOfWorkFactory unitOfWorkFactory, IRepository<TaskEntity, Guid> repository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteTaskRequest request, CancellationToken cancellationToken)
    {
        var unitOfWork = _unitOfWorkFactory.Begin();
        var taskToDelete =await _repository.Get(request.Id);
        if (taskToDelete is null)
            throw new NullReferenceException();
        await _repository.Delete(taskToDelete);
        await unitOfWork.End();
        return Unit.Value;
    }
}
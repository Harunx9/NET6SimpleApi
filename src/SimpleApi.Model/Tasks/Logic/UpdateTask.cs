using MediatR;
using SimpleApi.Model.Base;

namespace SimpleApi.Model.Tasks.Logic;

public record UpdateTaskRequest
    (Guid Id, string Title, DateTime DueDate, string Description) : IRequest<UpdateTaskResponse>;

public record UpdateTaskResponse(Guid Id);

public class UpdateTaskRequestHandler : IRequestHandler<UpdateTaskRequest, UpdateTaskResponse>
{
    private readonly IRepository<TaskEntity, Guid> _repository;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;

    public UpdateTaskRequestHandler(IRepository<TaskEntity, Guid> repository, IUnitOfWorkFactory unitOfWorkFactory)
    {
        _repository = repository;
        _unitOfWorkFactory = unitOfWorkFactory;
    }

    public async Task<UpdateTaskResponse> Handle(UpdateTaskRequest request, CancellationToken cancellationToken)
    {
        var unitOfWork = _unitOfWorkFactory.Begin();
        var taskToUpdate = await _repository.Get(request.Id);
        taskToUpdate.Title = request.Title;
        taskToUpdate.Desctiption = request.Description;
        taskToUpdate.DueDate = request.DueDate;
        await _repository.Update(taskToUpdate);
        await unitOfWork.End();
        return new UpdateTaskResponse(taskToUpdate.Id);
    }
}
using MediatR;
using SimpleApi.Model.Base;

namespace SimpleApi.Model.Tasks.Logic;

public record CreateTaskRequest(string Title, string Description, DateTime DueDate) : IRequest<CreateTaskResponse>;

public record CreateTaskResponse(Guid Id);

public class CreateTaskHandler : IRequestHandler<CreateTaskRequest, CreateTaskResponse>
{
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IRepository<TaskEntity, Guid> _repository;

    public CreateTaskHandler(IUnitOfWorkFactory unitOfWorkFactory, IRepository<TaskEntity, Guid> repository)
    {
        _unitOfWorkFactory = unitOfWorkFactory;
        _repository = repository;
    }

    public async Task<CreateTaskResponse> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var unitOfWork = _unitOfWorkFactory.Begin();
        var task = new TaskEntity(request.Title, request.DueDate, request.Description);
        await _repository.Create(new TaskEntity(request.Title, request.DueDate, request.Description));
        await unitOfWork.End();
        return new CreateTaskResponse(task.Id);
    }
}
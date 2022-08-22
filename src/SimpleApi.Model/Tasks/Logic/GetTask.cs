using MediatR;
using SimpleApi.Model.Base;

namespace SimpleApi.Model.Logic;

public record GetTaskByIdRequest(Guid TaskId) : IRequest<GetTaskByIdResponse> ;

public record GetTaskByIdResponse(Guid TaskId, string Title, string Description, DateTime? DueDate);

public class GetTaskById : IRequestHandler<GetTaskByIdRequest, GetTaskByIdResponse>
{
    private readonly IReadRepository<TaskEntity, Guid> _readRepository;

    public GetTaskById(IReadRepository<TaskEntity, Guid> readRepository)
    {
        _readRepository = readRepository;
    }
    
    public async Task<GetTaskByIdResponse?> Handle(GetTaskByIdRequest request, CancellationToken cancellationToken)
    {
        var task = await _readRepository.Get(request.TaskId);
        if (task is null)
            throw new NullReferenceException();
        return new GetTaskByIdResponse(task.Id, task.Title, task.Desctiption, task.DueDate);
    }
}
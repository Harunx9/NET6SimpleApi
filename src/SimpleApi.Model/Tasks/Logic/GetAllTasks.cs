using MediatR;
using SimpleApi.Model.Base;

namespace SimpleApi.Model.Logic;

public record TaskListItem(Guid Id, string Title, string Description, DateTime? DueDate);

public record GetAllTasksRequest() : IRequest<GetAllTasksResponse>;

public record GetAllTasksResponse(IEnumerable<TaskListItem> Items);

public class GetAllTasksRequestHandler : IRequestHandler<GetAllTasksRequest, GetAllTasksResponse>
{
    private readonly IReadRepository<TaskEntity, Guid> _repository;

    public GetAllTasksRequestHandler(IReadRepository<TaskEntity, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<GetAllTasksResponse> Handle(GetAllTasksRequest request, CancellationToken cancellationToken)
    {
        //TODO: Add paging and projection on db side
        var allTasks = await _repository.All();

        return new GetAllTasksResponse(allTasks.Select(x => new TaskListItem(x.Id, x.Title, x.Desctiption, x.DueDate)));
    }
}
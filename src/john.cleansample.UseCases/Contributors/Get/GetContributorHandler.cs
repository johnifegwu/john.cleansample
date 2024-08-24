using Ardalis.Result;
using Ardalis.SharedKernel;
using Data.Repositories;
using john.cleansample.Core.ContributorAggregate;
using Microsoft.EntityFrameworkCore;

namespace john.cleansample.UseCases.Contributors.Get;

/// <summary>
/// Queries don't necessarily need to use repository methods, but they can if it's convenient
/// </summary>
public class GetContributorHandler(IUnitOfWorkCore _unitOfWork)
  : IQueryHandler<GetContributorQuery, Result<ContributorDTO>>
{
  public async Task<Result<ContributorDTO>> Handle(GetContributorQuery request, CancellationToken cancellationToken)
  {
    var entity = await _unitOfWork.Repository<Contributor>()
      .Read().Where(contributor => contributor.Id == request.ContributorId).FirstOrDefaultAsync();
    
    if (entity == null) return Result.NotFound();

    return new ContributorDTO(entity.Id, entity.Name, entity.PhoneNumber?.Number ?? "");
  }
}

using Ardalis.Result;
using Ardalis.SharedKernel;

namespace john.cleansample.UseCases.Contributors.Get;

public record GetContributorQuery(int ContributorId) : IQuery<Result<ContributorDTO>>;

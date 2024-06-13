using Ardalis.Result;
using Ardalis.SharedKernel;

namespace john.cleansample.UseCases.Contributors.Update;

public record UpdateContributorCommand(int ContributorId, string NewName) : ICommand<Result<ContributorDTO>>;

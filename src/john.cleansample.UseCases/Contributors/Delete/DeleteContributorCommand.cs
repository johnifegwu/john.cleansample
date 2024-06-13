using Ardalis.Result;
using Ardalis.SharedKernel;

namespace john.cleansample.UseCases.Contributors.Delete;

public record DeleteContributorCommand(int ContributorId) : ICommand<Result>;

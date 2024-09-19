using System.Reflection;

namespace Architecture.Tests;

public abstract class BaseTest
{
    // Arrange
    protected static readonly Assembly APIAssembly = typeof(API.AssemblyReference).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(Application.AssemblyReference).Assembly;
    protected static readonly Assembly ContractsAssembly = typeof(Contracts.AssemblyReference).Assembly;
    protected static readonly Assembly DomainAssembly = typeof(Domain.AssemblyReference).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(Infrastructure.AssemblyReference).Assembly;
}

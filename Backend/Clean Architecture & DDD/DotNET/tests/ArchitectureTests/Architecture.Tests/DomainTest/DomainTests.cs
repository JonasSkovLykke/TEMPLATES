using FluentAssertions;
using NetArchTest.Rules;
using SharedKernel.Primitives;
using System.Reflection;

namespace Architecture.Tests.DomainTest;

public class DomainTests : BaseTest
{
    [Fact]
    public void DomainEvents_Should_BeSealed()
    {
        // Act
        var result = Types.InAssembly(DomainAssembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .BeSealed()
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void DomainEvents_Should_HaveDomainEventPostfix()
    {
        // Act
        var result = Types.InAssembly(DomainAssembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .HaveNameEndingWith("DomainEvent")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Entities_Should_HavePrivateParameterlessConstructor()
    {
        // Act
        var entityTypes = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(Entity<>))
            .GetTypes();

        var failingTypes = new List<Type>();

        foreach (var entityType in entityTypes)
        {
            var constructors = entityType.GetConstructors(
                BindingFlags.NonPublic |
                BindingFlags.Instance);

            if (!constructors.Any(c => c.IsPrivate && c.GetParameters().Length is 0))
            {
                failingTypes.Add(entityType);
            }
        }

        // Assert
        failingTypes.Should().BeEmpty();
    }

    [Fact]
    public void AggregateRoots_Should_HavePrivateParameterlessConstructor()
    {
        // Act
        var aggregateRootTypes = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(AggregateRoot<>))
            .GetTypes();

        var failingTypes = new List<Type>();

        foreach (var aggregateRootType in aggregateRootTypes)
        {
            var constructors = aggregateRootType.GetConstructors(
                BindingFlags.NonPublic |
                BindingFlags.Instance);

            if (!constructors.Any(c => c.IsPrivate && c.GetParameters().Length is 0))
            {
                failingTypes.Add(aggregateRootType);
            }
        }

        // Assert
        failingTypes.Should().BeEmpty();
    }
}

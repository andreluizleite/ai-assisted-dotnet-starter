namespace {{Namespace}}.Domain.Entities;

// Base template for domain entities
// Replace {{EntityName}} and {{PropertiesBlock}}

public class {{EntityName}}
{
    public Guid Id { get; private set; }

    {{PropertiesBlock}}

    // Example of factory method enforcing invariants
    public static {{EntityName}} Create(
        Guid id{{FactoryParameters}})
    {
        // TODO: Add guard clauses and invariants
        return new {{EntityName}}(id{{FactoryArguments}});
    }

    // Constructor kept private to force use of factory method
    private {{EntityName}}(Guid id{{CtorParameters}})
    {
        Id = id;
        {{AssignmentsBlock}}
    }
}

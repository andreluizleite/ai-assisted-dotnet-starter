# xUnit Unit Test Rules

## 1. Test Structure
- Use the Arrange-Act-Assert (AAA) pattern in all tests.
- Each test method must test a single behavior or scenario.
- Use descriptive method names: `MethodName_StateUnderTest_ExpectedBehavior`.

## 2. Organization
- Place all unit tests in the `/tests` directory, mirroring the `/src` structure.
- Group tests by the class or feature under test.
- Use partial classes or nested classes to organize tests for large classes.

## 3. Test Naming
- Prefix test classes with the name of the class under test, suffixed with `Tests` (e.g., `PostServiceTests`).
- Test method names should clearly describe the scenario and expected outcome.

## 4. Assertions
- Use `Assert` methods from xUnit or FluentAssertions for clarity.
- Prefer single, clear assertions per test. If multiple assertions are needed, ensure they are logically related.

## 5. Test Data
- Use inline data or `[Theory]` for parameterized tests.
- Use `[Fact]` for single-scenario tests.
- Avoid magic strings and numbers; use constants or test data builders where appropriate.

## 6. Isolation
- Mock dependencies using Moq or a similar library.
- Do not access external resources (databases, files, network) in unit tests.
- Use in-memory implementations or fakes for dependencies.

## 7. Naming Conventions
- Use PascalCase for test class and method names.
- Use camelCase for local variables and parameters.

## 8. Coverage
- Ensure all public methods and business logic paths are covered by unit tests.
- Write tests for both positive and negative scenarios.

## 9. Readability
- Keep tests concise and focused.
- Add comments only when necessary to explain intent.

## 10. Maintenance
- Remove or update tests that are no longer relevant.
- Refactor tests to improve clarity and reduce duplication.

---
*Follow these rules to ensure unit tests are reliable, maintainable, and provide meaningful feedback on your codebase.*

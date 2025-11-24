# xUnit Unit Test Rules

- Use xUnit as the test framework for all unit tests.
- Place all unit tests in the `/tests` directory, mirroring the `/src` structure.
- Test class names must end with `Tests` (e.g., `CustomerServiceTests`).
- Each test method must be public and marked with `[Fact]` or `[Theory]`.
- Test method names should clearly state the scenario and expected outcome (e.g., `MethodName_StateUnderTest_ExpectedBehavior`).
- Use FluentAssertions for assertions.
- Use Moq for mocking dependencies.
- Avoid testing private methods; test only public APIs.
- Each test should be independent and not rely on shared state.
- Use Test Data Builders or AutoFixture for complex object creation.
- Clean up any resources or data created during tests.
- Keep tests fast and focused on a single behavior.
- Prefer constructor injection for test dependencies.
- Use the Arrange-Act-Assert pattern in all tests.

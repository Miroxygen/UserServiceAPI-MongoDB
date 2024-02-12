
namespace App.Tests;
public class MyTest
{
    [Fact]
    public void TestAddition()
    {
        // Arrange
        int a = 5;
        int b = 3;

        // Act
        int result = a + b;

        // Assert
        Assert.Equal(8, result);
    }
}

using App.Models;

namespace App.Tests;

public class UserTests
{
    [Fact]
    public void TheRisingSocialStarTest()
    {
        var axelina = new User { UserId = 1, Name = "Axelina", Username = "axelina_crusebjörn", Followers = 100, Following = [2, 3] };

        axelina.Followers += 10000; 

        var newFollowings = new List<int> { 4, 5, 6 };
        foreach (var following in newFollowings)
        {
            if (!axelina.Following.Contains(following))
            {
                axelina.Following.Add(following);
            }
        }

        Assert.Equal(10100, axelina.Followers);
        Assert.Contains(4, axelina.Following);
        Assert.Contains(5, axelina.Following);
        Assert.Contains(6, axelina.Following);
        Assert.Equal(5, axelina.Following.Count);
    }
}

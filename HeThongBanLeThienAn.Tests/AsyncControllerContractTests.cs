using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using MiniSupermarket.API.Controllers;
using Xunit;

namespace HeThongBanLeThienAn.Tests;

public class AsyncControllerContractTests
{
    [Fact]
    public void EveryHttpActionReturnsTask()
    {
        var actions = typeof(AuthController).Assembly.GetTypes()
            .Where(t => typeof(ControllerBase).IsAssignableFrom(t))
            .SelectMany(t => t.GetMethods())
            .Where(m => m.GetCustomAttributes(true).OfType<HttpMethodAttribute>().Any());
        Assert.NotEmpty(actions);
        Assert.All(actions, action => Assert.True(typeof(Task).IsAssignableFrom(action.ReturnType),
            $"{action.DeclaringType!.Name}.{action.Name} must return Task."));
    }
}

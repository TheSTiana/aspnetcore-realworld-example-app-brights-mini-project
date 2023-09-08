using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Conduit.Application.Features.Articles.Commands;
using Conduit.Application.Features.Articles.Queries;
using Conduit.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

using Xunit.Abstractions;

using Xunit;

namespace Conduit.IntegrationTests.Features.Articles;
public class ArticleOrderTests :TestBase
{
    public ArticleOrderTests(ConduitApiFactory factory, ITestOutputHelper output) : base(factory, output) { }

    [Fact]
    public async Task Guest_Cannot_Order_Article()
    {
        var response = await Act(HttpMethod.Post, "/articles/slug-article/order");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Cannot_Order_Non_Existent_Article()
    {
        await ActingAs(new User
        {
            Name = "John Doe",
            Email = "john.doe@example.com",
        });

        var response = await Act(HttpMethod.Post, "/articles/slug-article/order");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Can_Order_Article()
    {
        await ActingAs(new User
        {
            Name = "John Doe",
            Email = "john.doe@example.com",
        });

        await Mediator.Send(new NewArticleCommand(
            new NewArticleDto
            {
                Title = "Test Title",
                Description = "Test Description",
                Body = "Test Body",
            }
        ));

        var response = await Act<SingleArticleResponse>(HttpMethod.Post, "/articles/test-title/order");

        response.Article.Should().BeEquivalentTo(new
        {
            
            Ordered = true,
            OrderCount = 1,
        }, options => options.Including(x => x.Ordered).Including(x => x.OrderCount));

        (await Context.Set<Order>().CountAsync()).Should().Be(1);
    }
}

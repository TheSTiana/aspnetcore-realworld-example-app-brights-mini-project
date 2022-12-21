using Application.Features.Tags.Queries;
using Domain.Entities;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Application.IntegrationTests.Features.Tags;

public class TagsListTests : TestBase
{
    public TagsListTests(ConduitApiFactory factory, ITestOutputHelper output) : base(factory, output) { }

    [Fact]
    public async Task Can_List_All_Tags()
    {
        await Context.Tags.AddRangeAsync(
            new Tag { Name = "Tag3" },
            new Tag { Name = "Tag2" },
            new Tag { Name = "Tag1" }
        );
        await Context.SaveChangesAsync();

        var response = await Act<TagsResponse>(HttpMethod.Get, "/tags");

        response.Tags.Should().Equal("Tag1", "Tag2", "Tag3");
    }
}
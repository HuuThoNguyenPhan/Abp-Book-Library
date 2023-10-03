using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Acme.BookLibrary.Pages;

public class Index_Tests : BookLibraryWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}

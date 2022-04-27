using System.Threading.Tasks;

namespace belgosles_test_app.Infrastructure
{
    internal delegate Task ActionAsync();

    internal delegate Task ActionAsync<in T>(T parameter);
}

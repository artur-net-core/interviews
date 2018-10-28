using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace LoadBalancer.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            string[] responses;
            const int tasksCount = 100;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/");

                var tasks = new Task<HttpResponseMessage>[tasksCount];

                for (int i = 0; i < tasks.Length; i++)
                {
                    tasks[i] = client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "/api/values"));
                }

                var responseMessages = await Task.WhenAll(tasks);
                responses = await Task.WhenAll(responseMessages.Select(async m => await m.Content.ReadAsStringAsync()));
            }

            Assert.Equal(responses.Count(r => r.Equals("[\"ApiTwo: value1\",\"ApiTwo: value2\"]", StringComparison.InvariantCulture)), tasksCount / 2);
        }
    }
}

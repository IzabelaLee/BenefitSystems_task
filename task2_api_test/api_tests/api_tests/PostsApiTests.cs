using System.Text;
using Newtonsoft.Json.Linq;

namespace task2.PostsApiTests
{
    public class PostsApiTests
    {
        private readonly HttpClient _httpClient;

        public PostsApiTests()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri("https://jsonplaceholder.typicode.com")
            };
        }

        [Fact]
        public async Task GetPosts()
        {
            var endpoint = "/posts";
            var response = await _httpClient.GetAsync(endpoint);

            // Check status code and if Json array consist of at least 1 element
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var posts = JArray.Parse(responseBody);
            Assert.True(posts.Count > 0);
        }

        [Fact]
        public async Task GetSinglePost()
        {
            var postId = 5;
            var endpoint = $"/posts/{postId}";
            var response = await _httpClient.GetAsync(endpoint);

            // Check status code and if the value of postId and id value of the JSON are equal.
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var post = JObject.Parse(responseBody);
            Assert.Equal(postId, (int)post["id"]);
        }

        [Fact]
        public async Task CreatePost()
        {
            var endpoint = "/posts";
            var newPost = new
            {
                title = "Test title",
                body = "Test body",
                userId = 1
            };

            var content = new StringContent(JObject.FromObject(newPost).ToString(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(endpoint, content);

            // Check status code and if the values of newPost fields and values of the JSON are equal.
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var post = JObject.Parse(responseBody);
            Assert.Equal(newPost.title, (string)post["title"]);
            Assert.Equal(newPost.body, (string)post["body"]);
            Assert.Equal(newPost.userId, (int)post["userId"]);
        }

        [Fact]
        public async Task UpdatePost()
        {
            var postId = 1;
            var endpoint = $"/posts/{postId}";
            var updatedPost = new
            {
                id = postId,
                title = "Updated title",
                body = "Updated body is tested.",
                userId = 1
            };

            var content = new StringContent(JObject.FromObject(updatedPost).ToString(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(endpoint, content);

            // Check status code and if the values of updatedPost fields and values of the JSON are equal.
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var post = JObject.Parse(responseBody);
            Assert.Equal(updatedPost.title, (string)post["title"]);
            Assert.Equal(updatedPost.body, (string)post["body"]);
            Assert.Equal(updatedPost.userId, (int)post["userId"]);
        }

        [Fact]
        public async Task DeletePost()
        {
            var postId = 1;
            var endpoint = $"/posts/{postId}";
            var response = await _httpClient.DeleteAsync(endpoint);

            // Check status code
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }

}



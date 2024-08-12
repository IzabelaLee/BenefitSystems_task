using System.Text;
using Newtonsoft.Json.Linq;


namespace task2.CommentsApiTests
{
    public class CommentsApiTests
    {
        private readonly HttpClient _httpClient;

        public CommentsApiTests()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri("https://jsonplaceholder.typicode.com")
            };
        }

        // GET: /comments
        [Fact]
        public async Task GetComments()
        {
            var endpoint = "/comments";
            var response = await _httpClient.GetAsync(endpoint);

            // Check status code and if Json array consist of at least 1 element
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var comments = JArray.Parse(responseBody);
            Assert.True(comments.Count > 0);
        }

        // GET: /comments/{id}
        [Fact]
        public async Task GetSingleComment()
        {
            var commentId = 4;
            var endpoint = $"/comments/{commentId}";

            var response = await _httpClient.GetAsync(endpoint);

            // Check status code and if the value of commentId and id value of the JSON are equal.
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var comment = JObject.Parse(responseBody);
            Assert.Equal(commentId, (int)comment["id"]);
        }

        // POST: /comments
        [Fact]
        public async Task CreateComment()
        {
            var endpoint = "/comments";
            var newComment = new
            {
                postId = 1,
                name = "Jack Smith",
                email = "jack@example.com",
                body = "Comments are tested."
            };

            var content = new StringContent(JObject.FromObject(newComment).ToString(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(endpoint, content);

            // Check status code and if the values of newComents fields and values of the JSON are equal.
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var createdComment = JObject.Parse(responseBody);
            Assert.Equal(newComment.name, createdComment["name"]);
            Assert.Equal(newComment.email, createdComment["email"]);
            Assert.Equal(newComment.body, createdComment["body"]);
        }

        // PUT: /comments/{id}
        [Fact]
        public async Task UpdateComment()
        {
            var commentId = 4;
            var endpoint = $"/comments/{commentId}";
            var updatedComment = new
            {
                id = commentId,
                postId = 1,
                name = "Barbara Smith",
                email = "barbara@example.com",
                body = "Updated comment is tested."
            };

            var content = new StringContent(JObject.FromObject(updatedComment).ToString(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(endpoint, content);

            // Check status code and if the values of updatedComment fields and values of the JSON are equal.
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var returnedComment = JObject.Parse(responseBody);
            Assert.Equal(updatedComment.name, returnedComment["name"]);
            Assert.Equal(updatedComment.email, returnedComment["email"]);
            Assert.Equal(updatedComment.body, returnedComment["body"]);
        }

        // DELETE: /comments/{id}
        [Fact]
        public async Task DeleteComment()
        {
            var commentId = 1;
            var endpoint = $"/comments/{commentId}";

            var response = await _httpClient.DeleteAsync(endpoint);

            // Check status code
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        }
    }
}

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace ForceTestingService.IntegrationTests.ControllerTests
{
    [TestFixture]
    public class QuestionControllerIntegrationTests
    {
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            var factory = new TestingWebAppFactory();
            _client = factory.CreateClient();
        }


        [Test]
        public async Task Index_WhenCalled_ReturnsCorrectForm()
        {
            var response = await _client.GetAsync("/Question?topicid=1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("TaskInfo01");
            responseString.Should().NotContain("TaskInfo02");
        }

        // CREATE

        [Test]
        public async Task Create_WhenCalled_ReturnsCorrectForm()
        {
            var response = await _client.GetAsync("/Question/Create?topicid=1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("Create new Question");
        }

        [Test]
        public async Task Create_SentWrongModel_ReturnsViewWithErrorMessages()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Question/Create?topicid=1");
            var formModel = new Dictionary<string, string>
            {
                { "TaskInfo", "" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("The TaskInfo field is required");
        }

        [Test]
        public async Task Create_WhenPOSTExecuted_ReturnsToIndexViewWithCreatedSubject()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Question/Create?topicid=1");
            var formModel = new Dictionary<string, string>
            {
                { "TaskInfo", "New TaskInfo" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("New TaskInfo");

        }

        // EDIT

        [Test]
        public async Task Edit_WhenCalled_ReturnsCorrectForm()
        {
            var response = await _client.GetAsync("/Question/Edit/1?topicId=1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("Edit Question");
            responseString.Should().Contain("TaskInfo01");

        }

        [Test]
        public async Task Edit_SentWrongModel_ReturnsViewWithErrorMessages()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Question/Edit/1?topicId=1");
            var formModel = new Dictionary<string, string>
            {
                { "TaskInfo", "" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("The TaskInfo field is required");
        }

        [Test]
        public async Task Edit_WhenPOSTExecuted_ReturnsToIndexViewWithCreatedSubject()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Question/Edit/1?topicId=1");
            var formModel = new Dictionary<string, string>
            {
                { "TaskInfo", "Edited TaskInfo" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("Edited TaskInfo");
        }

        // DELETE

        [Test]
        public async Task Delete_WhenCalled_ReturnsCorrectForm()
        {
            var response = await _client.GetAsync("/Question/Delete/1?topicId=1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("Are you sure you want to delete question");
            responseString.Should().Contain("TaskInfo01");

        }

        [Test]
        public async Task Delete_WhenPOSTExecuted_ReturnsToIndexViewWithDeletedSubject()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Question/Delete/1?topicId=1");
            var formModel = new Dictionary<string, string>
            {
                { "id", "1" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().NotContain("TaskInfo01");
            responseString.Should().Contain("TaskInfo03");
        }
    }
}
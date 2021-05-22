using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace ForceTestingService.IntegrationTests.ControllerTests
{
    [TestFixture]
    public class AnswerControllerIntegrationTests
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
            var response = await _client.GetAsync("/Answer?questionid=1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("Text01");
            responseString.Should().NotContain("Text02");
        }

        // CREATE

        [Test]
        public async Task Create_WhenCalled_ReturnsCorrectForm()
        {
            var response = await _client.GetAsync("/Answer/Create?questionId=1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("Create new Answer");
        }

        [Test]
        public async Task Create_SentWrongModel_ReturnsViewWithErrorMessages()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Answer/Create?questionId=1");
            var formModel = new Dictionary<string, string>
            {
                { "Text", "" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("The Text field is required");
        }

        [Test]
        public async Task Create_WhenPOSTExecuted_ReturnsToIndexViewWithCreatedSubject()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Answer/Create?questionId=1");
            var formModel = new Dictionary<string, string>
            {
                { "Text", "New Text" },
                { "IsCorrect", "true" },
                { "QuestionId", "1" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("New Text");

        }

        // EDIT

        [Test]
        public async Task Edit_WhenCalled_ReturnsCorrectForm()
        {
            var response = await _client.GetAsync("/Answer/Edit/1?questionId=1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("Edit Answer");
            responseString.Should().Contain("Text01");

        }

        [Test]
        public async Task Edit_SentWrongModel_ReturnsViewWithErrorMessages()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Answer/Edit/1?questionId=1");
            var formModel = new Dictionary<string, string>
            {
                { "Text", "" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("The Text field is required");
        }

        [Test]
        public async Task Edit_WhenPOSTExecuted_ReturnsToIndexViewWithCreatedSubject()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Answer/Edit/1?questionId=1");
            var formModel = new Dictionary<string, string>
            {
                { "Text", "Edited Text" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("Edited Text");
        }

        // DELETE

        [Test]
        public async Task Delete_WhenCalled_ReturnsCorrectForm()
        {
            var response = await _client.GetAsync("/Answer/Delete/1?questionId=1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("Are you sure you want to delete answer");
            responseString.Should().Contain("Text01");

        }

        [Test]
        public async Task Delete_WhenPOSTExecuted_ReturnsToIndexViewWithDeletedSubject()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Answer/Delete/1?questionId=1");
            var formModel = new Dictionary<string, string>
            {
                { "id", "1" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().NotContain("Text01");
            responseString.Should().Contain("Text03");
        }
    }
}
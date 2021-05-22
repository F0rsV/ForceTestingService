using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace ForceTestingService.IntegrationTests.ControllerTests
{
    [TestFixture]
    public class TestBaseControllerIntegrationTests
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
            var response = await _client.GetAsync("/TestBase");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("Name01");
        }

        [Test]
        public async Task ShowResults_WhenCalled_ReturnsCorrectForm()
        {
            var response = await _client.GetAsync("TestBase/ShowResults/1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("TestFName1");
            responseString.Should().NotContain("TestFName2");
        }

        // CREATE

        [Test]
        public async Task Create_WhenCalled_ReturnsCorrectForm()
        {
            var response = await _client.GetAsync("/TestBase/Create");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("Create new Test");
        }

        [Test]
        public async Task Create_SentWrongModel_ReturnsViewWithErrorMessages()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/TestBase/Create");
            var formModel = new Dictionary<string, string>
            {
                { "Name", "" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("The Name field is required");
        }

        [Test]
        public async Task Create_WhenPOSTExecuted_ReturnsToIndexViewWithCreatedSubject()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/TestBase/Create");
            var formModel = new Dictionary<string, string>
            {
                { "Name", "New Name" },
                { "NumOfQuestions", "5" },
                { "AmountOfSeconds", "50" },
                { "NumOfTries", "3" },
                { "TopicId", "1" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("New Name");

        }

        // EDIT

        [Test]
        public async Task Edit_WhenCalled_ReturnsCorrectForm()
        {
            var response = await _client.GetAsync("/TestBase/Edit/1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("Edit Test");
            responseString.Should().Contain("Name01");

        }

        [Test]
        public async Task Edit_SentWrongModel_ReturnsViewWithErrorMessages()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/TestBase/Edit/1");
            var formModel = new Dictionary<string, string>
            {
                { "Name", "" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("The Name field is required");
        }

        [Test]
        public async Task Edit_WhenPOSTExecuted_ReturnsToIndexViewWithCreatedSubject()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/TestBase/Edit/1");
            var formModel = new Dictionary<string, string>
            {
                { "Name", "Edited Name" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("Edited Name");
        }

        // DELETE

        [Test]
        public async Task Delete_WhenCalled_ReturnsCorrectForm()
        {
            var response = await _client.GetAsync("/TestBase/Delete/1");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().Contain("Are you sure you want to delete this");
            responseString.Should().Contain("Name01");

        }

        [Test]
        public async Task Delete_WhenPOSTExecuted_ReturnsToIndexViewWithDeletedSubject()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/TestBase/Delete/1");
            var formModel = new Dictionary<string, string>
            {
                { "id", "1" }
            };
            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Should().NotContain(" Name01");
            responseString.Should().Contain(" Name03");
        }
    }
}
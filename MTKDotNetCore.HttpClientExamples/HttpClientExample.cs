using Newtonsoft.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace MTKDotNetCore.ConsoleAppHttpClientExamples
{
    internal class HttpClientExample
    {
        private readonly HttpClient _client = new HttpClient() { BaseAddress = new Uri("https://localhost:7051") };
        private readonly string _blogEndpoint = "api/blog";

        public async Task RunAsync()
        {
            await ReadAsync();
            //await EditAsync(1);
            //await DeleteAsync(25);
            //await CreateAsync("http client title", "http client author", "http client content");
            //await UpdateAsync(25, "Put http client title", "Put http client author", "Put http client content");
            //await PatchAsync(25, new BlogDto() { BlogTitle = "Patch http client title"});
        }

        private async Task ReadAsync()
        {
            // we don't need to add base URL anymore since we've already configured it in instance bulding
            HttpResponseMessage response = await _client.GetAsync(_blogEndpoint);

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                // ! means that we assure the value will not be null
                List<BlogDto> lst = JsonConvert.DeserializeObject<List<BlogDto>>(jsonStr)!;

                foreach (var item in lst)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(item));
                    Console.WriteLine($"Title => {item.BlogTitle}");
                    Console.WriteLine($"Author => {item.BlogAuthor}");
                    Console.WriteLine($"Content => {item.BlogContent}");
                    Console.WriteLine("------------------------------------");
                }
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }

        }

        private async Task EditAsync (int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"{_blogEndpoint}/{id}");

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                var item = JsonConvert.DeserializeObject<BlogDto>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(item));
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}");
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        private async Task CreateAsync (string title, string author, string content)
        {
            BlogDto blogDto = new BlogDto()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            string blogJson = JsonConvert.SerializeObject(blogDto);

            // new StringContent(content, data encoding form, content type)
            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            var response = await _client.PostAsync(_blogEndpoint, httpContent);

            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        private async Task UpdateAsync (int id, string title, string author, string content)
        {
            BlogDto blogDto = new BlogDto()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            string blogJson = JsonConvert.SerializeObject(blogDto);

            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            var response = await _client.PutAsync($"{_blogEndpoint}/{id}", httpContent);

            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        private async Task PatchAsync (int id, BlogDto requestModel)
        {
            BlogDto blogDto = new BlogDto()
            {
                BlogTitle = requestModel.BlogTitle,
                BlogAuthor = requestModel.BlogAuthor,
                BlogContent = requestModel.BlogContent
            };

            string blogJson = JsonConvert.SerializeObject(blogDto);

            HttpContent httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
            var response = await _client.PatchAsync($"{_blogEndpoint}/{id}", httpContent);

            if (response.IsSuccessStatusCode)
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }

        private async Task DeleteAsync (int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{_blogEndpoint}/{id}");

            if (response.IsSuccessStatusCode )
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
            else
            {
                string message = await response.Content.ReadAsStringAsync();
                Console.WriteLine(message);
            }
        }
    }
}

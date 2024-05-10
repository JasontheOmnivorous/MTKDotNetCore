using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MTKDotNetCore.RestApiWithNLayer.Features.DreamDictionary
{
    [Route("api/[controller]")]
    [ApiController]
    public class DreamDictionaryController : ControllerBase
    {
        private async Task<DreamDictionary> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("data.json");
            var model = JsonConvert.DeserializeObject<DreamDictionary>(jsonStr);
            return model;
        }

        [HttpGet("headers")]
        public async Task<IActionResult> GetHeaders()
        {
            var model = await GetDataAsync();
            return Ok(model.BlogHeader);
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetDetails()
        {
            var model = await GetDataAsync();
            return Ok(model.BlogDetail);
        }

        [HttpGet("{blogId}/{detailId}")]
        public async Task<IActionResult> GetBlogDetail(int blogId, int detailId)
        {
            var model = await GetDataAsync();
            return Ok(model.BlogDetail.FirstOrDefault(x => x.BlogId == blogId && x.BlogDetailId == detailId));
        }
    }

    public class DreamDictionary
    {
        public Blogheader[] BlogHeader { get; set; }
        public Blogdetail[] BlogDetail { get; set; }
    }

    public class Blogheader
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
    }

    public class Blogdetail
    {
        public int BlogDetailId { get; set; }
        public int BlogId { get; set; }
        public string BlogContent { get; set; }
    }

}

using Blog.Services.Interfaces;
using Markdig;

namespace Blog.Services;

public class MarkdownService : IMarkdownService
{
    private MarkdownPipeline pipeline;
    public MarkdownService()
    {
        pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
    }

    public string TextToHtml(string text)
    {
        return Markdown.ToHtml(text, pipeline);
    }
}

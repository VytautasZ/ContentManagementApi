namespace ContentManagementApi.Contracts;

public record PostDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Banner { get; set; }
    public string Content { get; set; }
}
namespace ChatAI.Application.Common.DTOs;

public abstract class BaseAuditableEntityResponse : BaseEntityResponse
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

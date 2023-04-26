using System.ComponentModel.DataAnnotations.Schema;

namespace FSH.WebApi.Application.Common.Chat;
public interface IChatUser
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [Column(TypeName = "text")]
    public string? ImageUrl { get; set; }

    // public string ProfilePictureDataUrl { get; set; }
}
namespace DTO;

public class AuthorizedUserDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string Login { get; set; }

    public int Status { get; set; }
}

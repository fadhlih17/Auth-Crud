namespace Auth_Crud.Dtos;

public class CommonResponse<T>
{
    public String Message { get; set; }
    public T Data { get; set; }
}
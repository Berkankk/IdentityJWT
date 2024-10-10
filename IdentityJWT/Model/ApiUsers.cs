namespace IdentityJWT.Model
{
    public class ApiUsers
    {
        //Burası api kullanıcılarının veritabanında nasıl saklanacağını gösteriyor ama bu projeyi sql e yansıtmadım static proje bu
        public int ApiUsersID { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}

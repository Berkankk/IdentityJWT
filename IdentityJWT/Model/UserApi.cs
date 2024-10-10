namespace IdentityJWT.Model
{
    public class UserApi
    {
        public static List<ApiUsers> apiUsers = new List<ApiUsers>()
        {
            new ApiUsers {ApiUsersID = 1, Name ="Berkan" , Password="Berkan123*" , Role="Admin" },
            new ApiUsers {ApiUsersID = 2, Name ="Batuhan" , Password="Batuhan123*" , Role="Visitor" },
            new ApiUsers {ApiUsersID = 3, Name ="Ömer" , Password="Ömer123*" , Role="Manager" },
        };
    }
}
//Burası sanki veritabanımız gibi kullanacağımız liste dikkat et 
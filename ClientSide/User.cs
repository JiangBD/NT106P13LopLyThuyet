

namespace ClientSide;

public class User
{
    public string UserName, PasswordHash,FullName, Email;
    public DateTime DoB;
    public User(string un, string ph,string f, string e, DateTime dt)
    {   
        UserName = un; PasswordHash = ph; FullName = f; Email = e; DoB = dt;
    }
    

}


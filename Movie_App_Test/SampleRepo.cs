using Movie_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerMicroserviceTests
{
    public class SampleRepo
    {
        public List<User> UserData()
        {
            List<User> user = new List<User>()
            {
                new User
                {


                 LoginId = "Tripti123" ,
                firstName = "Tripti",
                lastName = "Rautela",
                contactNumber = "8738959033",
                email = "Rautela@gmail.com",
                password = "Tripti99",
                confirmPassword = "Tripti99"
                }
            };
            return user;
        }
    }
}
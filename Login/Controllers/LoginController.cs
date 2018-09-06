using System.Collections.Generic;
using System.Linq;
using Login.Models;
using Microsoft.AspNetCore.Mvc;

namespace Login.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly List<Member> _members
            = new List<Member> {new Member {Username = "Sam", Password = "1234"}};

        // POST api/login
        [HttpPost]
        public ActionResult<Response> LoginMember([FromBody] Member member)
        {
            return _members
                .Where(IsMember)
                .DefaultIfEmpty(DefaultMember())
                .Select(Result)
                .First();

            bool IsMember(Member x)
                => x.Username == member.Username && x.Password == member.Password;

            Member DefaultMember()
                => new Member {Username = string.Empty, Password = string.Empty};

            Response Result(Member x)
                => new Response
                {
                    Success = !string.IsNullOrEmpty(x.Username),
                    Username = x.Username
                };
        }
    }
}
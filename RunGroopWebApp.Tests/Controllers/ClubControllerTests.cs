using FakeItEasy;
using RunGroopWebApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunGroopWebApp.Tests.Controllers
{
    public class ClubControllerTests
    {
        private IClubRepository _clubRepository;

        public ClubControllerTests() 
        {
            // Dependency
            _clubRepository = A.Fake<IClubRepository>();
        }    
    }
}

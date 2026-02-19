using Core.Entity;
using Core.ViewModel;

namespace Application.Mappers
{
    public class UserMapper : AutoMapper.Profile
    {
        public UserMapper()
        {
            CreateMap<User, LoginVM>();
        }
    }
}

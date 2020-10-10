namespace ShelterDemo.Api.Dogs.Profiles
{
    public class DogProfile : AutoMapper.Profile
    {
        public DogProfile()
        {
            CreateMap<Db.Dog, Models.Dog>();
        }
    }
}

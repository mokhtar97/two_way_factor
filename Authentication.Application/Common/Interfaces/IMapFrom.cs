using AutoMapper;

namespace Authentication.Application.Common.Interfaces
{
    public interface IMapFrom<TEntity>
    {
        void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(TEntity), GetType()).ReverseMap();
        }
    }
}
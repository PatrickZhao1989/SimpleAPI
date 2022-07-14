using AutoMapper;
using SampleApi.Entities;
using SampleApi.Models;

namespace SampleApi.Mappers;

public class UserProfile : Profile
{
	public UserProfile()
	{
		CreateMap<Product, ProductDto>().ReverseMap();
	}
}
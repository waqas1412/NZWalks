using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositeries
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}

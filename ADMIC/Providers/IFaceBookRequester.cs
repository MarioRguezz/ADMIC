using System;
using System.Threading.Tasks;
using ADMIC.Data.Models.Facebook;

namespace ADMIC.Providers
{
    public interface IFaceBookRequester
    {
        Task<FacebookResponse> GetPosts();


        Task<FacebookResponse> GetPosts(string padding);
    }
}
namespace BookstoreApp.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using BookstoreApp.Services.Data;
    using BookstoreApp.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : BaseController
    {
        private readonly IVotesService votesService;

        public VotesController(IVotesService votesService)
        {
            this.votesService = votesService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostVoteResponseModel>> PostAsync(PostVoteInputModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.votesService.SetVoteAsync(input.BookId, userId, input.Value);
            var averageVote = this.votesService.GetAverageVote(input.BookId);

            return new PostVoteResponseModel { AverageVote = averageVote };
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swagger.DataAccess.Entities;
using Swagger.DataAccess.Repositories;

namespace Swagger.Controllers.V2
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    [ApiController]
    public class BookReviewsController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly V1.BookReviewsController _bookReviewsController;

        public BookReviewsController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
            _bookReviewsController = new V1.BookReviewsController(reviewRepository);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BookReview>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<BookReview>> Get()
        {
            return _bookReviewsController.Get(); 
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookReview))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BookReview> Get(int id)
        {
            return _bookReviewsController.Get(id);
        }

        [HttpGet("summary")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BookReview>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<BookReview>> Summary()
        {
            return _bookReviewsController.Summary();
        }


        [HttpGet("summaryfortitle/{title}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookReview))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<BookReview> SummaryForTitle(string title)
        {
            var reviews = _reviewRepository.AllReviews.Where(review => review.Title == title);

            if (!reviews.Any())
                return NotFound();

            var summary = new BookReview { Title = title, Rating = reviews.Average(r => r.Rating) };

            return Ok(summary);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<int> Post([FromBody] BookReview review)
        {
            return _bookReviewsController.Post(review);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(int id, [FromBody] BookReview review)
        {
            return _bookReviewsController.Put(id, review);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            return _bookReviewsController.Delete(id);
        }

    }
}

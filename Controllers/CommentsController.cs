using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsAPI.Models;
using NewsAPI.DataContext;

namespace NewsAPI.Controllers
{

    [Route("api/[controller]")]
    public class CommentsController : Controller
    {

        private readonly NewsApiContext _context;

        public CommentsController(NewsApiContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public CommentViewModel GetComment(int id)
        {
            var comment = _context.Comments.Where(c => c.Id == id).FirstOrDefault();
            if (comment == null)
            {
                throw new Exception();
            }

            var commentResult = new CommentViewModel()
            {
                Id = comment.Id,
                Name = comment.Name,
                EmailAddress = comment.EmailAddress,
                CommentText = comment.CommentText,
                ArticleId = comment.ArticleId,
                CreatedDate = comment.CreatedDate,
                UpdatedDate = comment.UpdatedDate
            };

            return commentResult;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, CommentViewModel comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oldComment = await _context.Comments.FindAsync(id);

            oldComment.ArticleId = comment.ArticleId;
            oldComment.CommentText = comment.CommentText;
            oldComment.EmailAddress = comment.EmailAddress;
            oldComment.Name = comment.Name;
            oldComment.Id = comment.Id;
            oldComment.UpdatedDate = DateTime.UtcNow;
            //db.Entry(oldComment).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;
            _context.Update(oldComment);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(CommentViewModel comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newComment = new Comment()
            {
                Id = comment.Id,
                ArticleId = comment.ArticleId,
                Name = comment.Name,
                CommentText = comment.CommentText,
                EmailAddress = comment.EmailAddress
            };
            newComment.CreatedDate = newComment.UpdatedDate = DateTime.UtcNow;
            _context.Comments.Add(newComment);
            await _context.SaveChangesAsync();

            var returnComment = new CommentViewModel()
            {
                Id = newComment.Id,
                ArticleId = newComment.ArticleId,
                Name = newComment.Name,
                CommentText = newComment.CommentText,
                EmailAddress = newComment.EmailAddress,
                CreatedDate = newComment.CreatedDate,
                UpdatedDate = newComment.UpdatedDate
            };

            return Ok(returnComment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            var deletedComment = new CommentViewModel()
            {
                Id = comment.Id,
                ArticleId = comment.ArticleId,
                Name = comment.Name,
                CommentText = comment.CommentText,
                CreatedDate = comment.CreatedDate,
                UpdatedDate = comment.UpdatedDate,
                EmailAddress = comment.EmailAddress
            };

            return Ok(deletedComment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Count(e => e.Id == id) > 0;
        }
    }
}

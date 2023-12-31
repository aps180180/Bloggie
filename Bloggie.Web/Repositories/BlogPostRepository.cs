﻿using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Repositories.Intefaces;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext _bloggieDbContext;
        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
                _bloggieDbContext = bloggieDbContext;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await _bloggieDbContext.AddAsync(blogPost);
            await _bloggieDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingBlogPost = await _bloggieDbContext.BlogPosts.FindAsync(id);
            if (existingBlogPost != null)
            {
                _bloggieDbContext.BlogPosts.Remove(existingBlogPost);
                await _bloggieDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await _bloggieDbContext.BlogPosts.Include(nameof(BlogPost.Tags)).OrderByDescending(x=> x.PublishedDate).ToListAsync();
        }

        public async Task<BlogPost> GetAsync(Guid id)
        {
          return await _bloggieDbContext.BlogPosts.Include(nameof(BlogPost.Tags))
                .FirstAsync(x=> x.Id== id);
                
        }

        public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
        {
            var existingBlogPost = await _bloggieDbContext.BlogPosts.Include(nameof(BlogPost.Tags))
                .FirstOrDefaultAsync(x=> x.Id== blogPost.Id);

            if (existingBlogPost != null)
            {
                existingBlogPost.Heading = blogPost.Heading;
                existingBlogPost.PageTitle = blogPost.PageTitle;
                existingBlogPost.Content = blogPost.Content;
                existingBlogPost.ShortDescription = blogPost.ShortDescription;
                existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlogPost.UrlHandle = blogPost.UrlHandle;
                existingBlogPost.PublishedDate = blogPost.PublishedDate;
                existingBlogPost.Author = blogPost.Author;
                existingBlogPost.Visible = blogPost.Visible;

                // deleta as tags existentes 
                if(blogPost.Tags != null && blogPost.Tags.Any())
                {
                    _bloggieDbContext.Tags.RemoveRange(existingBlogPost.Tags);
                    // adiciona as novas tags
                    blogPost.Tags.ToList().ForEach(x => x.BlogPostId = existingBlogPost.Id);
                    await _bloggieDbContext.Tags.AddRangeAsync(blogPost.Tags);
                }
            }
            await _bloggieDbContext.SaveChangesAsync();
            return existingBlogPost;
        }

        public async Task<BlogPost> GetAsync(string urlHandle)
        {
           return await _bloggieDbContext.BlogPosts.Include(nameof(BlogPost.Tags))
                .FirstOrDefaultAsync(x=> x.UrlHandle == urlHandle);
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync(string tagName)
        {
            return await _bloggieDbContext.BlogPosts.Include(nameof(BlogPost.Tags))
                .Where(x=> x.Tags.Any(t=> t.Name == tagName))
                .ToListAsync();
        }
    }
}

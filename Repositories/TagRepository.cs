using Microsoft.EntityFrameworkCore;
using StealAllTheCatsAPI.Models;

namespace StealAllTheCatsAPI.Repositories;

public class TagRepository : ITagRepository
{
    private readonly StealAlltheCatsDbContext _context;

    public TagRepository(StealAlltheCatsDbContext stealAlltheCatsDbContext)
    {
        this._context = stealAlltheCatsDbContext;
    }

    public async Task<List<TagEntity>> GetOrAddTags(ISet<string> tagNames)
    {
        // Retrieve existing tags from the database that match the input names
        var existingTags = await _context.Tags
            .Where(t => tagNames.Contains(t.Name))
            .ToListAsync();

        // Determine which tags are missing (those not in the database)
        var existingTagNames = existingTags.Select(t => t.Name).ToHashSet();
        var newTagNames = tagNames.Except(existingTagNames);

        // Add new tags to the context if any are missing
        if (newTagNames.Any())
        {
            var newTags = newTagNames.Select(name => new TagEntity { Name = name }).ToList();
            _context.Tags.AddRange(newTags);
            await _context.SaveChangesAsync();

            // Refresh the existingTags list to include newly inserted tags
            existingTags.AddRange(await _context.Tags
                .Where(t => newTagNames.Contains(t.Name))
                .ToListAsync());
        }

        // Return all tags, both existing and newly added
        return existingTags;
    }
}

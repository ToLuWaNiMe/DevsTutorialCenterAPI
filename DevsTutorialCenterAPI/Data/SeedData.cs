using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.Enums;

namespace DevsTutorialCenterAPI.Data;

public class SeedData
{
    private readonly DevsTutorialCenterAPIContext _context;

    public SeedData(DevsTutorialCenterAPIContext context)
    {
        _context = context;
    }
        
    public async Task Run()
    {
        await _context.AppUsers.AddRangeAsync(Authors);
        await _context.Tags.AddRangeAsync(ArticleTags);
        foreach (var article in Articles)
        {
            var randomAuthorId = Authors.GetRandomItem().Id;
            var randomTagId = ArticleTags.GetRandomItem().Id;

            article.AuthorId = randomAuthorId;
            article.TagId = randomTagId;
            await _context.Articles.AddAsync(article);

            await _context.ArticleApprovals.AddAsync(new ArticleApproval
            {
                ArticleId = article.Id,
                Status = SD.is_approved
            });
        }

        await _context.SaveChangesAsync();
    }

    private static IList<AppUser> Authors { get; set; } = new List<AppUser>
    {
        new()
        {
            FirstName = "John",
            LastName = "Doe",
            Stack = "JAVA",
            Squad = "001"
        }
    };

    private static IList<Article> Articles { get; set; } = new List<Article>
    {
        new()
        {
            Title = "Exploring the Evolution of Java: From Past to Present",
            Text =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc vulputate libero et velit interdum, ac aliquet odio mattis.",
            ImageUrl = "https://res.cloudinary.com/dj8irij6m/image/upload/c_pad,b_auto:predominant,fl_preserve_transparency/v1694085406/Sq016/Card_Dummy_Image.jpg?_s=public-apps",
            IsDeleted = false,
            ReadCount = 0,
            ReadTime = "5 minutes"
        },
        new()
        {
            Title = "Exploring the Evolution of Java: From Past to Present",
            Text =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc vulputate libero et velit interdum, ac aliquet odio mattis.",
            ImageUrl = "https://res.cloudinary.com/dj8irij6m/image/upload/c_pad,b_auto:predominant,fl_preserve_transparency/v1694085406/Sq016/Card_Dummy_Image.jpg?_s=public-apps",
            IsDeleted = false,
            ReadCount = 0,
            ReadTime = "5 minutes"
        },
        new()
        {
            Title = "Exploring the Evolution of Java: From Past to Present",
            Text =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc vulputate libero et velit interdum, ac aliquet odio mattis.",
            ImageUrl = "https://res.cloudinary.com/dj8irij6m/image/upload/c_pad,b_auto:predominant,fl_preserve_transparency/v1694085406/Sq016/Card_Dummy_Image.jpg?_s=public-apps",
            IsDeleted = false,
            ReadCount = 0,
            ReadTime = "5 minutes"
        },
        new()
        {
            Title = "Exploring the Evolution of Java: From Past to Present",
            Text =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc vulputate libero et velit interdum, ac aliquet odio mattis.",
            ImageUrl = "https://res.cloudinary.com/dj8irij6m/image/upload/c_pad,b_auto:predominant,fl_preserve_transparency/v1694085406/Sq016/Card_Dummy_Image.jpg?_s=public-apps",
            IsDeleted = false,
            ReadCount = 0,
            ReadTime = "5 minutes"
        },
        new()
        {
            Title = "Exploring the Evolution of Java: From Past to Present",
            Text =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc vulputate libero et velit interdum, ac aliquet odio mattis.",
            ImageUrl = "https://res.cloudinary.com/dj8irij6m/image/upload/c_pad,b_auto:predominant,fl_preserve_transparency/v1694085406/Sq016/Card_Dummy_Image.jpg?_s=public-apps",
            IsDeleted = false,
            ReadCount = 0,
            ReadTime = "5 minutes"
        },
        new()
        {
            Title = "Exploring the Evolution of Java: From Past to Present",
            Text =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc vulputate libero et velit interdum, ac aliquet odio mattis.",
            ImageUrl = "https://res.cloudinary.com/dj8irij6m/image/upload/c_pad,b_auto:predominant,fl_preserve_transparency/v1694085406/Sq016/Card_Dummy_Image.jpg?_s=public-apps",
            IsDeleted = false,
            AuthorId = "92d57368-0133-4fa6-b85c-2c5dd03cd802",
            ReadCount = 0,
            ReadTime = "5 minutes"
        }

    };

    private static IList<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>
    {
        new()
        {
            Name = "JAVA",
            CreatedOn = DateTime.UtcNow,
            UpdatedOn = DateTime.UtcNow,
            DeletedAt = null
        },
        new()
        {
            Name = ".NET",
            CreatedOn = DateTime.UtcNow,
            UpdatedOn = DateTime.UtcNow,
            DeletedAt = null
        },
        new()
        {
            Name = "NODE",
            CreatedOn = DateTime.UtcNow,
            UpdatedOn = DateTime.UtcNow,
            DeletedAt = null
        }
    };
}

public static class Extension
{
    public static T GetRandomItem<T>(this IList<T> items)
    {
        var rnd = new Random();
        var n = rnd.Next(items.Count);
        return items.ElementAt(n);
    }
}
using DevsTutorialCenterAPI.Data.Entities;

namespace DevsTutorialCenterAPI.Data
{
    public static class SeedData
    {
        public static IList<Article> Articles { get; set; } = new List<Article>
        {
            new Article
            {
                Id = "4c139f50-9138-432f-a26d-36bfe1dda7df",
                Title = "Exploring the Evolution of Java: From Past to Present",
                TagId = "65390c8e-5667-496d-9a3a-29e4e1d6f4e1",
                Text =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc vulputate libero et velit interdum, ac aliquet odio mattis.",
                ImageUrl = "https://res.cloudinary.com/dj8irij6m/image/upload/c_pad,b_auto:predominant,fl_preserve_transparency/v1694085406/Sq016/Card_Dummy_Image.jpg?_s=public-apps",
                PublicId = "ilopkbxemhpzrlgy1cpt",
                IsDeleted = false,
                AuthorId = "148d2f6f-af7a-423a-baa2-f01d434d9b3a",
                ReadCount = 0,
                ReadTime = "5 minutes"
            },
            new Article
            {
                Id = "beeea501-6fb8-4a1d-aa04-13834cd9e6c9",
                Title = "Exploring the Evolution of Java: From Past to Present",
                TagId = "65390c8e-5667-496d-9a3a-29e4e1d6f4e1",
                Text =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc vulputate libero et velit interdum, ac aliquet odio mattis.",
                ImageUrl = "https://res.cloudinary.com/dj8irij6m/image/upload/c_pad,b_auto:predominant,fl_preserve_transparency/v1694085406/Sq016/Card_Dummy_Image.jpg?_s=public-apps",
                PublicId = "evzoq4t9inkkn4gl6aqe",
                IsDeleted = false,
                AuthorId = "1db045d1-3844-48a9-856f-b70ab674eafc",
                ReadCount = 0,
                ReadTime = "5 minutes"
            },
            new Article
            {
                Id = "b5782c37-5886-4318-89bd-0b47f7930eb8",
                Title = "Exploring the Evolution of Java: From Past to Present",
                TagId = "65390c8e-5667-496d-9a3a-29e4e1d6f4e1",
                Text =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc vulputate libero et velit interdum, ac aliquet odio mattis.",
                ImageUrl = "https://res.cloudinary.com/dj8irij6m/image/upload/c_pad,b_auto:predominant,fl_preserve_transparency/v1694085406/Sq016/Card_Dummy_Image.jpg?_s=public-apps",
                IsDeleted = false,
                AuthorId = "0ae8ae01-1c9b-4a23-a995-b3f7197b29a3",
                ReadCount = 0,
                ReadTime = "5 minutes"
            },
            new Article
            {
                Id = "c2221445-c439-4ee8-a7e6-8ae3c028cf6b",
                Title = "Exploring the Evolution of Java: From Past to Present",
                TagId = "a6a0826b-6b8a-4b0f-aa8f-41cfd9478b2d",
                Text =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc vulputate libero et velit interdum, ac aliquet odio mattis.",
                ImageUrl = "https://res.cloudinary.com/dj8irij6m/image/upload/c_pad,b_auto:predominant,fl_preserve_transparency/v1694085406/Sq016/Card_Dummy_Image.jpg?_s=public-apps",
                IsDeleted = false,
                AuthorId = "1ebe6087-5788-4281-8401-ee0e4ce5031d",
                ReadCount = 0,
                ReadTime = "5 minutes"
            },
            new Article
            {
                Id = "621f143e-eae4-49de-8833-3e6af791ff21",
                Title = "Exploring the Evolution of Java: From Past to Present",
                TagId = "a6a0826b-6b8a-4b0f-aa8f-41cfd9478b2d",
                Text =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc vulputate libero et velit interdum, ac aliquet odio mattis.",
                ImageUrl = "https://res.cloudinary.com/dj8irij6m/image/upload/c_pad,b_auto:predominant,fl_preserve_transparency/v1694085406/Sq016/Card_Dummy_Image.jpg?_s=public-apps",
                IsDeleted = false,
                AuthorId = "902258f9-b3c2-42d0-b420-59d383a269dc",
                ReadCount = 0,
                ReadTime = "5 minutes"
            },
            new Article
            {
                Id = "2c0803b6-4efc-4e7e-81c1-5817a1b48705",
                Title = "Exploring the Evolution of Java: From Past to Present",
                TagId = "NODE",
                Text =
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc vulputate libero et velit interdum, ac aliquet odio mattis.",
                ImageUrl = "https://res.cloudinary.com/dj8irij6m/image/upload/c_pad,b_auto:predominant,fl_preserve_transparency/v1694085406/Sq016/Card_Dummy_Image.jpg?_s=public-apps",
                IsDeleted = false,
                AuthorId = "92d57368-0133-4fa6-b85c-2c5dd03cd802",
                ReadCount = 0,
                ReadTime = "5 minutes"
            }

        };

        public static IList<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>
        {
            new ArticleTag
            {
                Id = "65390c8e-5667-496d-9a3a-29e4e1d6f4e1",
                Name = "JAVA",
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                DeletedAt = null
            },
            new ArticleTag
            {
                Id = "a6a0826b-6b8a-4b0f-aa8f-41cfd9478b2d",
                Name = ".NET",
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                DeletedAt = null
            },
            new ArticleTag
            {
                Id = "5e72d82a-8274-4b6b-af20-881946c5e228",
                Name = "NODE",
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                DeletedAt = null
            }
        };
    }
}


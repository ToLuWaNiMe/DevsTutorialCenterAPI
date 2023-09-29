using DevsTutorialCenterAPI.Data.Entities;

namespace DevsTutorialCenterAPI.Data;

public static class SeedData
{
    public static IList<Article> Articles { get; set; } = new List<Article>
    {
        new()
        {
            Id = "4c139f50-9138-432f-a26d-36bfe1dda7df",
            Title = "Exploring the Evolution of Java: From Past to Present",
            Tag = "NODE",
            Text =
                "Dorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc vulputate libero et velit interdum, ac aliquet odio mattis.",
            ImageUrl = "https://res.cloudinary.com/dj8irij6m/image/upload/c_pad,b_auto:predominant,fl_preserve_transparency/v1694085406/Sq016/Card_Dummy_Image.jpg?_s=public-apps",
            PublicId = "ilopkbxemhpzrlgy1cpt",
            IsRecommended = true,
            IsPublished = true,
            IsSaved = false,
            IsReported = false,
            IsRead = false,
            UserId = "148d2f6f-af7a-423a-baa2-f01d434d9b3a"
        },
        new()
        {
            Id = "beeea501-6fb8-4a1d-aa04-13834cd9e6c9",
            Title = "Exploring the Evolution of Java: From Past to Present",
            Tag = ".NET",
            Text =
                "Dorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc vulputate libero et velit interdum, ac aliquet odio mattis.",
            ImageUrl = "https://res.cloudinary.com/dj8irij6m/image/upload/c_pad,b_auto:predominant,fl_preserve_transparency/v1694085406/Sq016/Card_Dummy_Image.jpg?_s=public-apps",
            PublicId = "evzoq4t9inkkn4gl6aqe",
            IsRecommended = true,
            IsPublished = true,
            IsSaved = false,
            IsReported = false,
            IsRead = false,
            UserId = "1db045d1-3844-48a9-856f-b70ab674eafc"
        },
        new()
        {
            Id = "b5782c37-5886-4318-89bd-0b47f7930eb8",
            Title = "Exploring the Evolution of Java: From Past to Present",
            Tag = "JAVA",
            Text =
                "Dorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc vulputate libero et velit interdum, ac aliquet odio mattis.",
            ImageUrl = "https://res.cloudinary.com/dj8irij6m/image/upload/c_pad,b_auto:predominant,fl_preserve_transparency/v1694085406/Sq016/Card_Dummy_Image.jpg?_s=public-apps",
            PublicId = "",
            IsRecommended = true,
            IsPublished = true,
            IsSaved = false,
            IsReported = false,
            IsRead = false,
            UserId = "0ae8ae01-1c9b-4a23-a995-b3f7197b29a3"
        },
        new()
        {
            Id = "c2221445-c439-4ee8-a7e6-8ae3c028cf6b",
            Title = "Exploring the Evolution of Java: From Past to Present",
            Tag = ".NET",
            Text =
                "Dorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc vulputate libero et velit interdum, ac aliquet odio mattis.",
            ImageUrl = "https://res.cloudinary.com/dj8irij6m/image/upload/c_pad,b_auto:predominant,fl_preserve_transparency/v1694085406/Sq016/Card_Dummy_Image.jpg?_s=public-apps",
            PublicId = "",
            IsRecommended = true,
            IsPublished = true,
            IsSaved = false,
            IsReported = false,
            IsRead = false,
            UserId = "1ebe6087-5788-4281-8401-ee0e4ce5031d"
        },
        new()
        {
            Id = "621f143e-eae4-49de-8833-3e6af791ff21",
            Title = "Exploring the Evolution of Java: From Past to Present",
            Tag = "JAVA",
            Text =
                "Dorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc vulputate libero et velit interdum, ac aliquet odio mattis.",
            ImageUrl = "https://res.cloudinary.com/dj8irij6m/image/upload/c_pad,b_auto:predominant,fl_preserve_transparency/v1694085406/Sq016/Card_Dummy_Image.jpg?_s=public-apps",
            PublicId = "",
            IsRecommended = true,
            IsPublished = true,
            IsSaved = false,
            IsReported = false,
            IsRead = false,
            UserId = "902258f9-b3c2-42d0-b420-59d383a269dc"
        },
        new()
        {
            Id = "2c0803b6-4efc-4e7e-81c1-5817a1b48705",
            Title = "Exploring the Evolution of Java: From Past to Present",
            Tag = "NODE",
            Text =
                "Dorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc vulputate libero et velit interdum, ac aliquet odio mattis.",
            ImageUrl = "https://res.cloudinary.com/dj8irij6m/image/upload/c_pad,b_auto:predominant,fl_preserve_transparency/v1694085406/Sq016/Card_Dummy_Image.jpg?_s=public-apps",
            PublicId = "",
            IsRecommended = true,
            IsPublished = true,
            IsSaved = false,
            IsReported = false,
            IsRead = false,
            UserId = "92d57368-0133-4fa6-b85c-2c5dd03cd802"
        }
    };
    //public static IList<Comment> Comments { get; set; } = new List<Comment>
    //{
    //    new Comment
    //    {
    //        Text = "This is a good write up",
    //        UserId = "8839CC6B-12E8-4E92-BF2D-1CE683236B57",
    //        ArticleId = "AA8B4DDD-6D50-44DC-B7AE-2949351256C8"
    //    }
    //};
}
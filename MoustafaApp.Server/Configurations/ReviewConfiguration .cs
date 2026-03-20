
namespace MoustafaApp.Server.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {

            builder.HasData(

            new Review
            {
                ReviewId = 37,
                ProductId = 24,
                Rating = 4.5m,
                ReviewText = "I absolutely love this t-shirt! The design is unique and the fabric feels so comfortable." +
                " As a fellow designer, I appreciate the attention to detail. It's become my favorite go-to shirt.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 38,
                ProductId = 24,
                Rating = 4.5m,
                ReviewText = "The t-shirt exceeded my expectations! The colors are vibrant and the print quality is top-notch." +
                " Being a UI/UX designer myself, I'm quite picky about aesthetics, and this t-shirt definitely gets a thumbs up from me.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 39,
                ProductId = 24,
                Rating = 4.5m,
                ReviewText = "This t-shirt is a must-have for anyone who appreciates good design." +
                " The minimalistic yet stylish pattern caught my eye, and the fit is perfect." +
                " I can see the designer's touch in every aspect of this shirt.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 40,
                ProductId = 24,
                Rating = 4.5m,
                ReviewText = "As a UI/UX enthusiast, I value simplicity and functionality." +
                " This t-shirt not only represents those principlesto wear." +
                " It's evident that the designer poured their creativity into making this t-shirt stand out.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 41,
                ProductId = 24,
                Rating = 4.5m,
                ReviewText = "This t-shirt is a fusion of comfort and creativity." +
                " The fabric is soft, and the design speaks volumes about the designer's skill." +
                " It's like wearing a piece of art that reflects my passion for both design and fashion.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 42,
                ProductId = 24,
                Rating = 4.5m,
                ReviewText = "I'm not just wearing a t-shirt; I'm wearing a piece of design philosophy." +
                " The intricate details and thoughtful layout of the design make this shirt a conversation starter.",
                DatePosted = new DateTime(2025, 8, 30)
            },


            new Review
            {
                ReviewId = 7,
                ProductId = 25,
                Rating = 4.5m,
                ReviewText = "I absolutely love this t-shirt! The design is unique and the fabric feels so comfortable." +
                " As a fellow designer, I appreciate the attention to detail. It's become my favorite go-to shirt.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 8,
                ProductId = 25,
                Rating = 4.5m,
                ReviewText = "The t-shirt exceeded my expectations! The colors are vibrant and the print quality is top-notch." +
                " Being a UI/UX designer myself, I'm quite picky about aesthetics, and this t-shirt definitely gets a thumbs up from me.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 9,
                ProductId = 25,
                Rating = 4.5m,
                ReviewText = "This t-shirt is a must-have for anyone who appreciates good design." +
                " The minimalistic yet stylish pattern caught my eye, and the fit is perfect." +
                " I can see the designer's touch in every aspect of this shirt.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 10,
                ProductId = 25,
                Rating = 4.5m,
                ReviewText = "As a UI/UX enthusiast, I value simplicity and functionality." +
                " This t-shirt not only represents those principlesto wear." +
                " It's evident that the designer poured their creativity into making this t-shirt stand out.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 11,
                ProductId = 25,
                Rating = 4.5m,
                ReviewText = "This t-shirt is a fusion of comfort and creativity." +
                " The fabric is soft, and the design speaks volumes about the designer's skill." +
                " It's like wearing a piece of art that reflects my passion for both design and fashion.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 12,
                ProductId = 25,
                Rating = 4.5m,
                ReviewText = "I'm not just wearing a t-shirt; I'm wearing a piece of design philosophy." +
                " The intricate details and thoughtful layout of the design make this shirt a conversation starter.",
                DatePosted = new DateTime(2025, 8, 30)
            },


            new Review
            {
                ReviewId = 13,
                ProductId = 31,
                Rating = 4.5m,
                ReviewText = "I absolutely love this t-shirt! The design is unique and the fabric feels so comfortable." +
                " As a fellow designer, I appreciate the attention to detail. It's become my favorite go-to shirt.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 14,
                ProductId = 31,
                Rating = 4.5m,
                ReviewText = "The t-shirt exceeded my expectations! The colors are vibrant and the print quality is top-notch." +
                " Being a UI/UX designer myself, I'm quite picky about aesthetics, and this t-shirt definitely gets a thumbs up from me.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 15,
                ProductId = 31,
                Rating = 4.5m,
                ReviewText = "This t-shirt is a must-have for anyone who appreciates good design." +
                " The minimalistic yet stylish pattern caught my eye, and the fit is perfect." +
                " I can see the designer's touch in every aspect of this shirt.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 16,
                ProductId = 31,
                Rating = 4.5m,
                ReviewText = "As a UI/UX enthusiast, I value simplicity and functionality." +
                " This t-shirt not only represents those principlesto wear." +
                " It's evident that the designer poured their creativity into making this t-shirt stand out.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 17,
                ProductId = 31,
                Rating = 4.5m,
                ReviewText = "This t-shirt is a fusion of comfort and creativity." +
                " The fabric is soft, and the design speaks volumes about the designer's skill." +
                " It's like wearing a piece of art that reflects my passion for both design and fashion.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 18,
                ProductId = 31,
                Rating = 4.5m,
                ReviewText = "I'm not just wearing a t-shirt; I'm wearing a piece of design philosophy." +
                " The intricate details and thoughtful layout of the design make this shirt a conversation starter.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 19,
                ProductId = 27,
                Rating = 4.5m,
                ReviewText = "I absolutely love this t-shirt! The design is unique and the fabric feels so comfortable." +
                " As a fellow designer, I appreciate the attention to detail. It's become my favorite go-to shirt.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 20,
                ProductId = 27,
                Rating = 4.5m,
                ReviewText = "The t-shirt exceeded my expectations! The colors are vibrant and the print quality is top-notch." +
                " Being a UI/UX designer myself, I'm quite picky about aesthetics, and this t-shirt definitely gets a thumbs up from me.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 21,
                ProductId = 27,
                Rating = 4.5m,
                ReviewText = "This t-shirt is a must-have for anyone who appreciates good design." +
                " The minimalistic yet stylish pattern caught my eye, and the fit is perfect." +
                " I can see the designer's touch in every aspect of this shirt.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 22,
                ProductId = 28,
                Rating = 4.5m,
                ReviewText = "As a UI/UX enthusiast, I value simplicity and functionality." +
                " This t-shirt not only represents those principlesto wear." +
                " It's evident that the designer poured their creativity into making this t-shirt stand out.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 23,
                ProductId = 28,
                Rating = 4.5m,
                ReviewText = "This t-shirt is a fusion of comfort and creativity." +
                " The fabric is soft, and the design speaks volumes about the designer's skill." +
                " It's like wearing a piece of art that reflects my passion for both design and fashion.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 24,
                ProductId = 28,
                Rating = 4.5m,
                ReviewText = "I'm not just wearing a t-shirt; I'm wearing a piece of design philosophy." +
                " The intricate details and thoughtful layout of the design make this shirt a conversation starter.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 25,
                ProductId = 27,
                Rating = 4.5m,
                ReviewText = "I absolutely love this t-shirt! The design is unique and the fabric feels so comfortable." +
                " As a fellow designer, I appreciate the attention to detail. It's become my favorite go-to shirt.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 26,
                ProductId = 28,
                Rating = 4.5m,
                ReviewText = "The t-shirt exceeded my expectations! The colors are vibrant and the print quality is top-notch." +
                " Being a UI/UX designer myself, I'm quite picky about aesthetics, and this t-shirt definitely gets a thumbs up from me.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 27,
                ProductId = 29,
                Rating = 4.5m,
                ReviewText = "This t-shirt is a must-have for anyone who appreciates good design." +
                " The minimalistic yet stylish pattern caught my eye, and the fit is perfect." +
                " I can see the designer's touch in every aspect of this shirt.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 28,
                ProductId = 29,
                Rating = 4.5m,
                ReviewText = "As a UI/UX enthusiast, I value simplicity and functionality." +
                " This t-shirt not only represents those principlesto wear." +
                " It's evident that the designer poured their creativity into making this t-shirt stand out.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 29,
                ProductId = 29,
                Rating = 4.5m,
                ReviewText = "This t-shirt is a fusion of comfort and creativity." +
                " The fabric is soft, and the design speaks volumes about the designer's skill." +
                " It's like wearing a piece of art that reflects my passion for both design and fashion.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 30,
                ProductId = 29,
                Rating = 4.5m,
                ReviewText = "I'm not just wearing a t-shirt; I'm wearing a piece of design philosophy." +
                " The intricate details and thoughtful layout of the design make this shirt a conversation starter.",
                DatePosted = new DateTime(2025, 8, 30)
            },


            new Review
            {
                ReviewId = 31,
                ProductId = 37,
                Rating = 4.5m,
                ReviewText = "I absolutely love this t-shirt! The design is unique and the fabric feels so comfortable." +
                " As a fellow designer, I appreciate the attention to detail. It's become my favorite go-to shirt.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 32,
                ProductId = 37,
                Rating = 4.5m,
                ReviewText = "The t-shirt exceeded my expectations! The colors are vibrant and the print quality is top-notch." +
                " Being a UI/UX designer myself, I'm quite picky about aesthetics, and this t-shirt definitely gets a thumbs up from me.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 33,
                ProductId = 32,
                Rating = 4.5m,
                ReviewText = "This t-shirt is a must-have for anyone who appreciates good design." +
                " The minimalistic yet stylish pattern caught my eye, and the fit is perfect." +
                " I can see the designer's touch in every aspect of this shirt.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 34,
                ProductId = 32,
                Rating = 4.5m,
                ReviewText = "As a UI/UX enthusiast, I value simplicity and functionality." +
                " This t-shirt not only represents those principlesto wear." +
                " It's evident that the designer poured their creativity into making this t-shirt stand out.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 35,
                ProductId = 33,
                Rating = 4.5m,
                ReviewText = "This t-shirt is a fusion of comfort and creativity." +
                " The fabric is soft, and the design speaks volumes about the designer's skill." +
                " It's like wearing a piece of art that reflects my passion for both design and fashion.",
                DatePosted = new DateTime(2025, 8, 30)
            },

            new Review
            {
                ReviewId = 36,
                ProductId = 33,
                Rating = 4.5m,
                ReviewText = "I'm not just wearing a t-shirt; I'm wearing a piece of design philosophy." +
                " The intricate details and thoughtful layout of the design make this shirt a conversation starter.",
                DatePosted = new DateTime(2025, 8, 30)
            }


            );



        }
    }
}

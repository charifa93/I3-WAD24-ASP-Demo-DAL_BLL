using ASP_MVC.Models.Cocktail;
using ASP_MVC.Models.User;
using BLL.Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ASP_MVC.Mappers
{
    internal static class Mapper
    {
        public static UserListItem ToListItem(this User user)
        {
            if(user is null) throw new ArgumentNullException(nameof(user));
            return new UserListItem()
            {
                User_Id = user.User_Id,
                First_Name = user.First_Name,
                Last_Name = user.Last_Name
            };
        }

        public static UserDetails ToDetails(this User user) {
            if (user is null) throw new ArgumentNullException(nameof(user));
            return new UserDetails()
            {
                User_Id = user.User_Id,
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Email = user.Email,
                CreatedAt = DateOnly.FromDateTime(user.CreatedAt)
            };
        }

        public static User ToBLL(this UserCreateForm user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            return new User(
                Guid.Empty,
                user.First_Name,
                user.Last_Name,
                user.Email,
                user.Password,
                DateTime.Now,
                null
                );
            /*return new User(
                user.First_Name,
                user.Last_Name,
                user.Email,
                user.Password);*/
        }

        public static UserEditForm ToEditForm (this User user)
        {
            if(user is null) throw new ArgumentNullException(nameof(user));
            return new UserEditForm() { 
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Email = user.Email
            };
        }

        public static User ToBLL(this UserEditForm user)
        {
            if(user is null) throw new ArgumentNullException(nameof(user));
            /*return new User(
                Guid.Empty,
                user.First_Name,
                user.Last_Name,
                user.Email,
                "********",
                DateTime.Now,
                null
                );*/
            return new User(
                user.First_Name,
                user.Last_Name,
                user.Email);
        }

        public static UserDelete ToDelete(this User user)
        {
            if (user is null) throw new ArgumentNullException(nameof(user));
            return new UserDelete() { 
                First_Name = user.First_Name,
                Last_Name = user.Last_Name,
                Email = user.Email
            };
        }


        public static CocktailListItem ToListItem(this Cocktail cocktail)
        {
            if(cocktail is null) throw new ArgumentNullException(nameof(cocktail));
            return new CocktailListItem()
            {
                Name = cocktail.Name,
                Description = cocktail.Description,
                CreatedBy = cocktail.CreatedBy
            };

        }
        public static CocktailDetails ToDetails(this Cocktail cocktail) 
        {
            return new CocktailDetails()
            {
                Cocktail_Id = cocktail.Cocktail_Id,
                Name = cocktail.Name,
                Description = cocktail.Description,
                Instruction = cocktail.Instructions,
                CreatedAt = DateOnly.FromDateTime(cocktail.CreatedAt),
                CreatedBy = cocktail.CreatedBy

            };
        }

        public static Cocktail ToBLL(this CocktailCreateForm cocktail)
        {
            if (cocktail is null) throw new ArgumentNullException(nameof(cocktail));
            return new Cocktail(

                cocktail.Name,
                cocktail.Description,
                cocktail.Instructions,
                //DateOnly.FromDateTime(cocktail.CreatedAt),
                cocktail?.CreatedBy
            );
        }
    }
}

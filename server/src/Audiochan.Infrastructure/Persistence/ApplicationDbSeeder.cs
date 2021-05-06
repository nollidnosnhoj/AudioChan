﻿using System;
using System.Threading.Tasks;
using Audiochan.Core.Common.Constants;
using Audiochan.Core.Common.Interfaces;
using Audiochan.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Audiochan.Infrastructure.Persistence
{
    public static class ApplicationDbSeeder
    {
        public static async Task UserSeedAsync(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (!await userManager.Users.AnyAsync())
            {
                var superuser = new User("superuser", "superuser@localhost", DateTime.UtcNow);

                // TODO: Do not hardcode superuser password when deploying into production haha
                await userManager.CreateAsync(superuser, "Password1");

                var superUserRole = await roleManager.FindByNameAsync(UserRoleConstants.Admin);

                if (superUserRole == null)
                {
                    await roleManager.CreateAsync(new Role {Name = UserRoleConstants.Admin});
                }

                await userManager.AddToRoleAsync(superuser, UserRoleConstants.Admin);
            }
        }

        public static async Task AddDefaultAudioForDemo(ApplicationDbContext context, UserManager<User> userManager,
            ITagRepository tagRepository)
        {
            if (!await context.Audios.AnyAsync())
            {
                var user = await userManager.FindByNameAsync("superuser");

                var audio1 = new Audio("Dreams", "audio1", "Dreams.mp3", 9335255, 388, user);
                audio1.UpdateTags(await tagRepository.GetListAsync(new[] {"chillout", "lucid-dreams"}));
                audio1.UpdatePublicity(true);

                var audio2 = new Audio("Heaven", "audio2", "Heaven.mp3", 6239211, 194, user);
                audio2.UpdateTags(await tagRepository.GetListAsync(new[] {"newgrounds", "piano", "rave"}));
                audio2.UpdatePublicity(true);

                var audio3 = new Audio("Life Is Beautiful", "audio3", "LifeIsBeautiful.mp3", 1823391, 45, user);
                audio3.UpdateTags(await tagRepository.GetListAsync(new[] {"happy", "anime", "hardcore", "nightcore"}));
                audio3.UpdatePublicity(true);

                var audio4 = new Audio("Beginning of Time", "audio4", "BeginningOfTime.mp3", 3952556, 164, user);
                audio4.UpdateTags(await tagRepository.GetListAsync(new[] {"hard-dance"}));
                audio4.UpdatePublicity(false);

                var audio5 = new Audio("Verity", "audio5", "Verity.mp3", 8788667, 219, user);
                audio5.UpdateTags(await tagRepository.GetListAsync(new[] {"vocals"}));
                audio5.UpdatePublicity(false);

                await context.Audios.AddRangeAsync(audio1, audio2, audio3, audio4, audio5);
                await context.SaveChangesAsync();
            }
        }
    }
}